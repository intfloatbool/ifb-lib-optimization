using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace IFBOptLib.IFBGraphics
{
    public static partial class IFBMesh
    {

        public static Task<int[]> SimplifyMeshFastAsync(float[] vertices, int[] indices, MeshSimplMagEffortType effortType, CancellationToken token)
        {
            return Task.Run<int[]>(() =>
            {
                return SimplifyMeshFast(vertices, indices, effortType, token);
            }, token);
        }
    }
}
