using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace IFBOptLib.IFBGraphics
{
    public static partial class IFBMesh
    {

        /// <returns>simplified array of indices(triangles)</returns>
        public static int[] SimplifyMeshFast(float[] vertices, int[] indices, MeshSimplMagEffortType effortType, CancellationToken? token = null)
        {
            CheckOptionalCancelTokenGuard(token);

            int[] res = null;
            IntPtr? resultPtr = default;

            DisposableIntPtr? inputVerticesPtr = default;
            DisposableIntPtr? inputIndicesPtr = default;

            SimplifyMeshFastIndicesResult? simplifyMeshFastIndicesResult = default;
            try
            {
                
                inputVerticesPtr = DisposableIntPtr.Create(
                    Marshal.AllocHGlobal(vertices.Length * sizeof(float))
                    );
                inputIndicesPtr = DisposableIntPtr.Create(
                    Marshal.AllocHGlobal(indices.Length * sizeof(uint))
                    );
                //TODO: Copy managed to unmanaged arrays!!
                Marshal.Copy(vertices, 0, inputVerticesPtr.Value.Ptr, vertices.Length);
                Marshal.Copy(indices, 0, inputIndicesPtr.Value.Ptr, indices.Length);

                uint verticesCount = (uint) vertices.Length;
                uint indicesCount = (uint) indices.Length;

                float multiplier = effortType.Mult;


                CheckNativePtrGuard(inputVerticesPtr.Value.Ptr);
                CheckNativePtrGuard(inputIndicesPtr.Value.Ptr);

                resultPtr = SimplifyMeshFast(inputVerticesPtr.Value.Ptr, verticesCount, inputIndicesPtr.Value.Ptr, indicesCount, multiplier);

                CheckOptionalCancelTokenGuard(token);

                CheckNativePtrGuard(resultPtr.Value);

                var simplifyFastResult = Marshal.PtrToStructure<SimplifyMeshFastIndicesResult>(resultPtr.Value);

                CheckOptionalCancelTokenGuard(token);

                var newIndicesCount = (int) simplifyFastResult.indicesCount;
                var newIndices = new int[newIndicesCount];
                Marshal.Copy(simplifyFastResult.indices, newIndices, 0, newIndicesCount);
                res = newIndices;
                simplifyMeshFastIndicesResult = simplifyFastResult;
                CheckOptionalCancelTokenGuard(token);
            }
            finally
            {
                if(inputVerticesPtr.HasValue)
                {
                    inputVerticesPtr.Value.Dispose();
                }

                if (inputIndicesPtr.HasValue)
                {
                    inputIndicesPtr.Value.Dispose();
                }

                if (resultPtr.HasValue)
                {
                    FreeMeshFastIndicesResult(resultPtr.Value);
                }
            }

            return res;
        }

        [System.Serializable]
        public class SerializableMeshVertex
        {
            public float PosX;
            public float PosY;
            public float PosZ;

            public float NormalX;
            public float NormalY;
            public float NormalZ;

            public float UvX;
            public float UvY;
        }

        [System.Serializable]
        public class SerializableMesh
        {
            public SerializableMeshVertex[] Vertexes;
            public int[] Indices;
        }

        public static SerializableMesh ManagedMeshToSerializable(ManagedMesh managedMesh)
        {
            var vertCount = managedMesh.VerticesCount;
            var indCount = managedMesh.IndicesCount;

            var serMesh = new SerializableMesh();
            serMesh.Vertexes = new SerializableMeshVertex[vertCount];
            serMesh.Indices = new int[indCount];

            for(int i = 0; i < vertCount; i++)
            {
                ref var vert = ref managedMesh.Vertices[i];
                serMesh.Vertexes[i] = new SerializableMeshVertex
                {
                    PosX = vert.Pos.X,
                    PosY = vert.Pos.Y,
                    PosZ = vert.Pos.Z,

                    NormalX = vert.Normal.X,
                    NormalY = vert.Normal.Y,
                    NormalZ = vert.Normal.Z,

                    UvX = vert.Uv.X,
                    UvY = vert.Uv.Y,
                };
            }

            Array.Copy(managedMesh.Indices, serMesh.Indices, indCount);

            return serMesh;
        }

        public static ManagedMesh SerializableToManagedMesh(SerializableMesh serializableMesh)
        {
            var vertCount = serializableMesh.Vertexes.Length;
            var indCount = serializableMesh.Indices.Length;

            var vertexes = new Vertex[vertCount];
            var indices = new int[indCount];

            for(int i = 0; i < vertCount; i++)
            {
                ref var serVert = ref serializableMesh.Vertexes[i];
                var vert = new Vertex();
                vert.Pos = new Vector3(
                    serVert.PosX, serVert.PosY, serVert.PosZ
                    );
                vert.Normal = new Vector3(
                    serVert.NormalX, serVert.NormalY, serVert.NormalZ
                    );
                vert.Uv = new Vector2(
                    serVert.UvX, serVert.UvY
                    );
                vertexes[i] = vert;
            }

            Array.Copy(serializableMesh.Indices, indices, indCount);

            return new ManagedMesh(vertexes, indices);
        }

        public static string MeshToJson(IMeshJsonSerializer serializer, SerializableMesh serializableMesh)
        {
            return serializer.Serialize(serializableMesh);
        }

        public static SerializableMesh MeshFromJson(IMeshJsonSerializer serializer, string json)
        {
            return serializer.Deserialize(json);
        }

        public static SerializableMesh LoadMeshFromJsonFile(IMeshJsonSerializer serializer, string file)
        {
            if(!File.Exists(file))
            {
                throw new FileNotFoundException();
            }

            string json = null;
            using(var reader = new StreamReader(file))
            {
                json = reader.ReadToEnd();
            }

            if(string.IsNullOrEmpty(json))
            {
                throw new InvalidDataException();
            }

            return MeshFromJson(serializer, json);
        }

        public static bool IsMeshesDeepEquals(ManagedMesh mma, ManagedMesh mmb)
        {
            
            if(mma.IndicesCount != mmb.IndicesCount)
            {
                return false;
            }

            if (mma.VerticesCount != mmb.VerticesCount)
            {
                return false;
            }

            for(int i = 0; i < mma.IndicesCount; i++)
            {
                if (mma.Indices[i] != mmb.Indices[i])
                {
                    return false;
                }
            }

            for (int i = 0; i < mma.VerticesCount; i++)
            {
                ref var vertA = ref mma.Vertices[i];
                ref var vertB = ref mmb.Vertices[i];
                
                if(!vertA.Equals(vertB))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
