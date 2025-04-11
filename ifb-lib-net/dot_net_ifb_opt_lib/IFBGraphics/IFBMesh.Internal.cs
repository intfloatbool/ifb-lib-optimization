using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace IFBOptLib.IFBGraphics
{
    public static partial class IFBMesh
    {
#region PRIVATE_EXTERN

#if FOR_UNITY3D
        [DllImport("ifblib")]
#else
        [DllImport("ifblib.dll")]
#endif
        private static extern IntPtr SimplifyMeshFast(IntPtr vertices, uint verticesCount, IntPtr indices, uint indicesCount, float multiplier);

#if FOR_UNITY3D
        [DllImport("ifblib")]
#else
        [DllImport("ifblib.dll")]
#endif
        private static extern void FreeMeshFastIndicesResult(IntPtr result);

        #endregion

        #region INTERNAL

        internal static void CheckMeshSimplifcationDataGuard(IMesh mm)
        {
            if (mm.IndicesCount <= 0)
            {
                throw new ArgumentException("mm.IndicesCount must be non zero!");
            }

            if (mm.IndicesCount % 3 != 0)
            {
                throw new ArgumentException("mm.IndicesCount must be module of 3! ");
            }

            if (mm.VerticesCount <= 0)
            {
                throw new ArgumentException("mm.VerticesCount must be non zero!");
            }

            if (mm.VerticesCount % 3 != 0)
            {
                throw new ArgumentException("mm.VerticesCount must be module of 3!");
            }
        }

        internal static void CheckOptionalCancelTokenGuard(CancellationToken? token)
        {
            if (!token.HasValue)
                return;

            token.Value.ThrowIfCancellationRequested();
        }

        internal static void CheckNativePtrGuard(IntPtr ptr)
        {
            if(ptr != IntPtr.Zero)
            {
                return;
            }

            throw new Exception("Native IntPTR is NULL_PTR!");
        }

        internal static void FreePointerSafe(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
            {
                return;
            }

            Marshal.FreeHGlobal(ptr);
        }

        #endregion

    }
}
