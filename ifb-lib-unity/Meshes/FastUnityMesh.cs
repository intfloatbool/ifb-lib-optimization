using System;
using UnityEngine;

namespace IFBLibUnity.Meshes
{

    /// <summary>
    /// Default UnityEngine.Mesh always returns copies of this getters (vertices, normals etc). So we need this wrapper!
    /// </summary>
    public class FastUnityMesh : IDisposable
    {
        public Mesh Mesh { get; private set; }

        public virtual Vector3[] Vertices => _vertices;
        public virtual Vector3[] Normals => _normals;
        public virtual Vector2[] Uvs => _uvs;
        public virtual int[] Indices => _indices;

        protected Vector3[] _vertices;
        protected Vector3[] _normals;
        protected Vector2[] _uvs;
        protected int[] _indices;

        public FastUnityMesh(Vector3[] vertices, Vector3[] normals, Vector2[] uvs, int[] indices)
        {

            _vertices = vertices;
            _normals = normals;
            _uvs = uvs;
            _indices = indices;

            var mesh = new Mesh();
            mesh.vertices = Vertices;
            mesh.normals = Normals;
            mesh.uv = Uvs;
            mesh.triangles = Indices;
            this.Mesh = mesh;
        }

        public virtual FastUnityMesh SetIndices(int[] indices)
        {
            this._indices = indices;
            this.Mesh.triangles = this._indices;
            return this;
            
        }

        public FastUnityMesh Clone()
        {
            return new FastUnityMesh(Vertices, Normals, Uvs, Indices);
        }

        public ConcurrentFastUnityMesh CloneAsConurrent()
        {
            return new ConcurrentFastUnityMesh(Vertices, Normals, Uvs, Indices);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(this.Mesh);
        }
    }
}