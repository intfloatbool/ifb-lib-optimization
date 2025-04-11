using System.Collections.Generic;
using System.Linq;

namespace IFBOptLib.IFBGraphics
{
    
    public static partial class IFBMesh
    {
        public static float[] CreateVerticesXYZArray(this IMesh managedMesh)
        {
            return managedMesh.Vertices.SelectVerticesXYZSequence().ToArray();
        }

        private static IEnumerable<float> SelectVerticesXYZSequence(this Vertex[] vertices)
        {
            for(int i = 0; i < vertices.Length; i++)
            {
                var vertPos = vertices[i].Pos;
                yield return vertPos.X;
                yield return vertPos.Y;
                yield return vertPos.Z;
            }
        }
    }
}
