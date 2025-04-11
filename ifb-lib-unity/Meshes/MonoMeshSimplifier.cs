using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

namespace IFBLibUnity.Meshes
{

    public class MonoMeshSimplifier : MonoBehaviour, IMeshSimplifier
    {
        [SerializeField] private bool _isAsync = true;
        [SerializeField] private MeshFilter[] _meshFilters;

        [Space(5f)]
        [SerializeField] private EMeshSimplifyStrength _strengthOnAwake;
        [SerializeField] private bool _isAutoSimplifyOnAwake;

        private Lazy<List<MeshDynamicSimplifier>> _simplifiersList;
        private CancellationTokenSource _currentCts;


#if UNITY_EDITOR
        [ContextMenu("FindMeshFilters()")]
        private void FindMeshFilters()
        {
            _meshFilters = GetComponentsInChildren<MeshFilter>();
        }
#endif

        private void Awake()
        {
            _simplifiersList = new(() =>
            {
                return _meshFilters.Select((mf) => MeshDynamicSimplifier.Create(new MeshFilterUnityMeshHolder(mf))).ToList();
            });
            
            if (!_isAutoSimplifyOnAwake)
            {
                return;
            }

            Simplify(_strengthOnAwake);
        }

        public void SimplifyLow()
        {
            Simplify(EMeshSimplifyStrength.LOW);
        }

        public void SimplifyMid()
        {
            Simplify(EMeshSimplifyStrength.MID);
        }

        public void SimplifyHigh()
        {
            Simplify(EMeshSimplifyStrength.HIGH);
        }

        public void ResetSimplification()
        {
            if(_isAsync)
            {
                ResetCancellation();
            }

            foreach (var mf in _simplifiersList.Value)
            {
                mf.Reset();
            }
        }

        public void Simplify(EMeshSimplifyStrength simplifyStrength)
        {
            if(_isAsync)
            {
                var cts = ResetCancellation();
                foreach (var mf in _simplifiersList.Value)
                {
                    mf.SimplifyAsync(simplifyStrength, cts.Token).Forget();
                }
            }
            else
            {
                foreach (var mf in _simplifiersList.Value)
                {
                    mf.Simplify(simplifyStrength);
                }
            }           
        }

        private CancellationTokenSource ResetCancellation()
        {
            this._currentCts?.Cancel();
            this._currentCts?.Dispose();

            this._currentCts = new CancellationTokenSource();
            return _currentCts;
        }

        private void OnDestroy()
        {
            this._currentCts?.Cancel();
            this._currentCts?.Dispose();

            if (_simplifiersList != null && _simplifiersList.IsValueCreated)
            {
                foreach(var ms in _simplifiersList.Value)
                {
                    ms.Dispose();
                }
            }
        }
    }
}