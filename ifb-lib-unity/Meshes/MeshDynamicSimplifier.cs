using Cysharp.Threading.Tasks;
using IFBOptLib.IFBGraphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

namespace IFBLibUnity.Meshes
{
    public class MeshDynamicSimplifier : IDisposable
    {
        private readonly Dictionary<EMeshSimplifyStrength, FastUnityMesh> _meshMap = new(3);
        private readonly Action<Mesh> _setMeshAction;
        private readonly Lazy<Mesh> _defaultMesh;
        private MeshDynamicSimplifier(IUnityMeshHolder meshHolder)
        {
            if(meshHolder == null)
            {
                throw new ArgumentNullException("meshFilter");
            }
            var sharedMesh = meshHolder.GetSharedMesh();

            if(!sharedMesh)
            {
                throw new ArgumentNullException("meshFilter.sharedMesh");
            }

            if(!sharedMesh.isReadable)
            {
                throw new ArgumentException($"sharedMesh '{sharedMesh.name}' is not readable.");
            }

            _defaultMesh = new Lazy<Mesh>(() => sharedMesh.CloneUnityMesh());
            _setMeshAction = (m) =>
            {
                if (meshHolder == null) return;
                meshHolder.SetMesh(m);
            };
        }

        public async UniTask SimplifyAsync(EMeshSimplifyStrength simplifyStrength, CancellationToken token)
        {
            if (_meshMap.TryGetValue(simplifyStrength, out var fmesh))
            {
                _setMeshAction?.Invoke(fmesh.Mesh);
                return;
            }

            await UniTask.NextFrame(cancellationToken: token);

            var inputMesh = _defaultMesh.Value;

            await UniTask.NextFrame(cancellationToken: token);

            
            var newFmesh = new ConcurrentFastUnityMesh(inputMesh.vertices, inputMesh.normals, inputMesh.uv, inputMesh.triangles);

            await UniTask.SwitchToThreadPool();
  
            newFmesh.GuardMeshBeforeNative();

            var rawVertices = newFmesh.Vertices.SelectRawVerticesXYZ().ToArray();
            var updatedIndices = await IFBMesh.SimplifyMeshFastAsync(rawVertices, newFmesh.Indices, simplifyStrength.ToEffortType(), token);

           
            await UniTask.SwitchToMainThread();
 
            newFmesh.SetIndices(updatedIndices);
            _meshMap.Add(simplifyStrength, newFmesh);
            _setMeshAction.Invoke(newFmesh.Mesh);
        }

        public void Simplify(EMeshSimplifyStrength simplifyStrength)
        {
            if(_meshMap.TryGetValue(simplifyStrength, out var fmesh))
            {
                _setMeshAction?.Invoke(fmesh.Mesh); 
                return;
            }
            var inputMesh = _defaultMesh.Value;
            var newFmesh = new FastUnityMesh(inputMesh.vertices, inputMesh.normals, inputMesh.uv, inputMesh.triangles);

            newFmesh.GuardMeshBeforeNative();

            var rawVertices = newFmesh.Vertices.SelectRawVerticesXYZ().ToArray();
            var updatedIndices = IFBMesh.SimplifyMeshFast(rawVertices, newFmesh.Indices, simplifyStrength.ToEffortType());
            newFmesh.SetIndices(updatedIndices);
            _meshMap.Add(simplifyStrength, newFmesh);
            _setMeshAction.Invoke(newFmesh.Mesh);
 
        }

        public void Reset()
        {
            _setMeshAction.Invoke(_defaultMesh.Value);
        }

        public void Dispose()
        {
            foreach(var kvp in _meshMap)
            {
                kvp.Value.Dispose();
            }
            _meshMap.Clear();
        }

        public static MeshDynamicSimplifier Create(IUnityMeshHolder meshHolder)
        {
            return new MeshDynamicSimplifier(meshHolder);
        }
    }
}
