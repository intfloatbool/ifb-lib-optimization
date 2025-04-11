using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace IFBOptLib.IFBGraphics
{
    public static partial class IFBMesh
    {
        public struct Vector3
        {
            public float X;
            public float Y;
            public float Z;

            public Vector3(float x, float y, float z)
            {
                X = x;
                Y = y;
                Z = z;
            }
        }

        public struct Vector2
        {
            public float X;
            public float Y;

            public Vector2(float x, float y)
            {
                X = x;
                Y = y;
            }
        }
        public struct MeshSimplMagEffortType
        {
            public readonly float Mult;
            internal MeshSimplMagEffortType(float effortMult)
            {
                Mult = effortMult;
            }
        }

        // layout Pack = 1 should never be done with IntPtr ! It causes problems!
        [StructLayout(LayoutKind.Sequential)]
        public struct SimplifyMeshFastIndicesResult
        {
            public uint indicesCount;
            public IntPtr indices;
            public int isAllocated;
        }

        public struct Vertex : IEquatable<Vertex>
        {
            public Vector3 Pos;
            public Vector3 Normal;
            public Vector2 Uv;

            public bool Equals(Vertex other)
            {
                return Pos.Equals(other.Pos)
                    && Normal.Equals(other.Normal)
                    && Uv.Equals(other.Uv);
            }

            public override bool Equals(object obj)
            {
                return obj is Vertex && Equals((Vertex)obj);
            }

            public override int GetHashCode()
            {
                int hash = Pos.GetHashCode();
                hash = (hash * 397) ^ (Normal.GetHashCode());
                hash = (hash * 397) ^ (Uv.GetHashCode());
                return hash;
            }


        }
        public class ManagedMesh : IMesh
        {
            private readonly Vertex[] _vertices;
            public Vertex[] Vertices => _vertices;

            private readonly int[] _indices;
            public int[] Indices => _indices;

            public int VerticesCount => _vertices.Length;
            public int IndicesCount => _indices.Length;

            public ManagedMesh(Vertex[] vertices, int[] indices)
            {
                _vertices = vertices;
                _indices = indices;
            }

            public ManagedMesh Clone()
            {
                var verts = new Vertex[VerticesCount];
                var inds = new int[IndicesCount];

                Array.Copy(_vertices, verts, VerticesCount);
                Array.Copy(_indices, inds, IndicesCount);

                return new ManagedMesh(verts, inds);
            }

            public ConcurrentManagedMesh CloneAsConcurrent()
            {
                var verts = new Vertex[VerticesCount];
                var inds = new int[IndicesCount];

                Array.Copy(_vertices, verts, VerticesCount);
                Array.Copy(_indices, inds, IndicesCount);

                return new ConcurrentManagedMesh(verts, inds);
            }
        }

        public class ConcurrentManagedMesh : IMesh
        {      
            public Vertex[] Vertices
            {
                get
                {
                    lock (_vertArrLock)
                    {
                        return _mesh.Vertices;
                    }             
                }
            }

            public int[] Indices
            {
                get
                {
                    lock(_indArrLock)
                    {
                        return _mesh.Indices;
                    }
                }
            }

            public int VerticesCount => Vertices.Length;

            public int IndicesCount => Indices.Length;

            private readonly IMesh _mesh;

            private readonly object _vertArrLock = new object();
            private readonly object _indArrLock = new object();

            public ConcurrentManagedMesh(Vertex[] vertices, int[] indices)
            {
                _mesh = new ManagedMesh(vertices, indices);
            }
        }
    }
}
