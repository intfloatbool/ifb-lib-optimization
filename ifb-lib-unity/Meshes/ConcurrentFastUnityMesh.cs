using UnityEngine;

namespace IFBLibUnity.Meshes
{
    public class ConcurrentFastUnityMesh : FastUnityMesh
    {

        public override Vector3[] Vertices
        {
            get
            {
                _verticesLock ??= new object();
                lock(_verticesLock)
                {
                    return _vertices;
                }
            }
        }
        public override Vector3[] Normals
        {
            get
            {
                _normalsLock ??= new object();
                lock (_normalsLock)
                {
                    return _normals;
                }
            }
        }
        public override Vector2[] Uvs
        {
            get
            {
                _uvsLock ??= new object();
                lock (_uvsLock)
                {
                    return _uvs;
                }
            }
        }
        public override int[] Indices
        {
            get
            {
                _indicesLock ??= new object();
                lock (_indicesLock)
                {
                    return _indices;
                }
            }
        }

        private object _verticesLock;
        private object _normalsLock;
        private object _uvsLock;
        private object _indicesLock;
        public ConcurrentFastUnityMesh(Vector3[] vertices, Vector3[] normals, Vector2[] uvs, int[] indices) : base(vertices, normals, uvs, indices)
        {
        }

        public override FastUnityMesh SetIndices(int[] indices)
        {
            _indicesLock ??= new object();
            lock (_indicesLock)
            {
                return base.SetIndices(indices);
            }
        }
    }
}