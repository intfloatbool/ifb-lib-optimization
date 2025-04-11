using UnityEngine;

namespace IFBLibUnity.Meshes
{
    [RequireComponent(typeof(CompositeMonoMeshSimplifier))]
    public class MonoMeshSimplifierSceneCollectorForComposite : MonoBehaviour
    {
        private void Awake()
        {
            var composite = GetComponent<CompositeMonoMeshSimplifier>();
            composite.AddSimplifiers(FindObjectsOfType<MonoMeshSimplifier>());
        }
    }
}