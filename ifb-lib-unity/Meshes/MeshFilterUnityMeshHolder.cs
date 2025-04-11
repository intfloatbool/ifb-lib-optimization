using UnityEngine;

namespace IFBLibUnity.Meshes
{
    public class MeshFilterUnityMeshHolder : IUnityMeshHolder
    {
        private MeshFilter _meshFilter;

        public MeshFilterUnityMeshHolder(MeshFilter meshFilter)
        {
           
            _meshFilter = meshFilter;
        }
        public Mesh GetSharedMesh()
        {
            return _meshFilter.sharedMesh;
        }

        public void SetMesh(Mesh mesh)
        {
            _meshFilter.mesh = mesh;
        }
    }
}
