using System.Collections.Generic;
using UnityEngine;

namespace IFBLibUnity.Meshes
{
    public class CompositeMonoMeshSimplifier : MonoBehaviour, IMeshSimplifier
    {
        private readonly List<IMeshSimplifier> _childSimplifiersList = new(10);
        public void AddSimplifiers(IEnumerable<IMeshSimplifier> simplifiers)
        {
            _childSimplifiersList.AddRange(simplifiers);
        }

        public void AddSimplifier(IMeshSimplifier simplifier)
        {
            _childSimplifiersList.Add(simplifier);
        }

        public void ResetSimplification()
        {
            _childSimplifiersList.ForEach(cs => cs.ResetSimplification());
        }

        public void Simplify(EMeshSimplifyStrength simplifyStrength)
        {
            _childSimplifiersList.ForEach(cs => cs.Simplify(simplifyStrength));
        }

        public void SimplifyLow()
        {
            _childSimplifiersList.ForEach(cs => cs.SimplifyLow());
        }

        public void SimplifyMid()
        {
            _childSimplifiersList.ForEach(cs => cs.SimplifyMid());
        }

        public void SimplifyHigh()
        {
            _childSimplifiersList.ForEach(cs => cs.SimplifyHigh());
        }        
    }
}