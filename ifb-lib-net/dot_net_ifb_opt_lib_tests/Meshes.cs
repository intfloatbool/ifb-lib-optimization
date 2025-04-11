using IFBOptLib.IFBGraphics;
using IFBOptLib_Tests.TestUtils;

namespace IFBOptLib_Tests
{
    public class Meshes
    {

        private IFBMesh.ManagedMesh _mm;
        private IFBMesh.ManagedMesh _parsedMm;

        [SetUp]
        public void Init()
        {
            int testVerticesCount = 300;
            int testIndicesCount = testVerticesCount / 2;

            var vertexes = new IFBMesh.Vertex[testVerticesCount];
            var indices = new int[testIndicesCount];

            var rnd = new Random();
            var multiplier = -1;
            Func<float, float, float> rndFloat = (min, max) => IFBUtils.RandomFloat(rnd, min, max);

            for(int i = 0; i < testVerticesCount; i++)
            {
                multiplier = rnd.NextDouble() > 0.5f ? 1 : -1;

                var posX = rndFloat(-50f, 50f);
                var posY = rndFloat(-50f, 50f);
                var posZ = rndFloat(-50f, 50f);

                var normalX = rndFloat(-1f, 1f);
                var normalY = rndFloat(-1f, 1f);
                var normalZ = rndFloat(-1f, 1f);

                var uvX = rndFloat(0, 1f);
                var uvY = rndFloat(0, 1f);

                vertexes[i] = new IFBMesh.Vertex
                {
                    Pos = new IFBMesh.Vector3(posX, posY, posZ),
                    Normal = new IFBMesh.Vector3(normalX, normalY, normalZ),
                    Uv = new IFBMesh.Vector2(uvX, uvY),
                };
            }

            for(int i = 0; i < testIndicesCount; i++)
            {
                var rndIndex = rnd.Next(testVerticesCount);
                indices[i] = rndIndex;
            }

            _mm = new IFBMesh.ManagedMesh(vertexes, indices);


            var testDir = TestContext.CurrentContext.TestDirectory;
            var jsonPath = Path.Combine(testDir, "..", "..", "..", "resources", "sheep_mesh.json");
            var serializer = new MeshSerializer();

            var serializableMesh = IFBMesh.LoadMeshFromJsonFile(serializer, jsonPath);
            _parsedMm = IFBMesh.SerializableToManagedMesh(serializableMesh);
            Assert.NotNull(_parsedMm);
        }

        [TearDown]
        public void Cleanup()
        { 
        }

        [Test]
        public void ManagedMeshesEquality()
        {
            var mmCopyA = _mm.Clone();
            var mmCopyB = mmCopyA.Clone();
            ref var vert = ref mmCopyB.Vertices[mmCopyB.VerticesCount / 2];
            vert.Pos.X = 99;
            vert.Pos.Y = 133;
            vert.Pos.Z = 144;

            mmCopyB.Indices[mmCopyB.IndicesCount / 2] = mmCopyB.VerticesCount + 100;

            Assert.NotNull(mmCopyA);
            Assert.NotNull(mmCopyB);

            Assert.IsTrue(IFBMesh.IsMeshesDeepEquals(_mm, mmCopyA));
            Assert.IsFalse(IFBMesh.IsMeshesDeepEquals(_mm, mmCopyB));
            Assert.IsFalse(IFBMesh.IsMeshesDeepEquals(mmCopyA, mmCopyB));

        }

        [Test]
        public void ManagedMeshToSerializable()
        {
            var serializable = IFBMesh.ManagedMeshToSerializable(_mm);
            var fromSerialized = IFBMesh.SerializableToManagedMesh(serializable);
            Assert.NotNull(serializable);
            Assert.NotNull(fromSerialized);
            Assert.True(IFBMesh.IsMeshesDeepEquals(_mm, fromSerialized));
        }

        [Test]
        public void MeshSerializationToJson()
        {
            var serializer = new MeshSerializer();
            var serializable = IFBMesh.ManagedMeshToSerializable(_mm);
            var json = IFBMesh.MeshToJson(serializer, serializable);

            Assert.NotNull(_mm);
            Assert.NotNull(serializable);
            Assert.NotNull(json);
            Assert.IsNotEmpty(json);
        }

        [Test]
        public void MeshDeserializationFromJson()
        {
            var serializer = new MeshSerializer();
            var serializable = IFBMesh.ManagedMeshToSerializable(_mm);
            var json = IFBMesh.MeshToJson(serializer, serializable);
            var fromJson = IFBMesh.MeshFromJson(serializer, json);
            var mm = IFBMesh.SerializableToManagedMesh(fromJson);

            Assert.NotNull(fromJson);
            Assert.NotNull(mm);
            Assert.True(IFBMesh.IsMeshesDeepEquals(_mm, mm));
        }

        [Test]
        public void LoadMeshFromFile()
        {
            var testDir = TestContext.CurrentContext.TestDirectory;
            var jsonPath = Path.Combine(testDir, "..", "..", "..", "resources", "sheep_mesh.json");
            var serializer = new MeshSerializer();

            var serializableMesh = IFBMesh.LoadMeshFromJsonFile(serializer, jsonPath);
            var mm = IFBMesh.SerializableToManagedMesh(serializableMesh);
            Assert.NotNull(serializableMesh);
            Assert.NotNull(mm);
            Assert.IsNotEmpty(serializableMesh.Vertexes);
            Assert.IsNotEmpty(serializableMesh.Indices);
            Assert.IsNotEmpty(mm.Vertices);
            Assert.IsNotEmpty(mm.Indices);
            Assert.AreEqual(serializableMesh.Vertexes.Length, mm.Vertices.Length);
            Assert.AreEqual(serializableMesh.Indices.Length, mm.Indices.Length);
        }


        [Test]
        public void SimplifyMeshFast()
        {
            var mm = _parsedMm;
            var vertices = mm.CreateVerticesXYZArray();
            var indices = mm.Indices;

            var simplifiedIndices = IFBMesh.SimplifyMeshFast(vertices, indices, IFBConstants.MeshSimpl.HARD);

            Assert.NotNull(simplifiedIndices);
            Assert.IsNotEmpty(simplifiedIndices);
            Assert.LessOrEqual(simplifiedIndices.Length, indices.Length);
        }

        [Test]
        public async Task SimplifyMeshFastAsync()
        {
            var mm = _parsedMm;
            var vertices = mm.CreateVerticesXYZArray();
            var indices = mm.Indices;

            var simplifiedIndices = await IFBMesh.SimplifyMeshFastAsync(vertices, indices, IFBConstants.MeshSimpl.HARD, CancellationToken.None);

            Assert.NotNull(simplifiedIndices);
            Assert.IsNotEmpty(simplifiedIndices);
            Assert.LessOrEqual(simplifiedIndices.Length, indices.Length);
        }
    }
}