using IFBOptLib.IFBGraphics;
using Newtonsoft.Json;
using static IFBOptLib.IFBGraphics.IFBMesh;

namespace IFBOptLib_Tests.TestUtils
{
    internal class MeshSerializer : IMeshJsonSerializer
    {
        public SerializableMesh Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<SerializableMesh>(json);
        }

        public string Serialize(IFBMesh.SerializableMesh mesh)
        {
            return JsonConvert.SerializeObject(mesh);
        }
    }
}
