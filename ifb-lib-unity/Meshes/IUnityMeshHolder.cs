using UnityEngine;

namespace IFBLibUnity.Meshes
{
    public interface IUnityMeshHolder
    {
        void SetMesh(Mesh mesh);
        Mesh GetSharedMesh();
    }
}
