namespace IFBOptLib.IFBGraphics
{
    public static partial class IFBMesh
    {
        /// <summary>
        /// In Unity - JsonUtility, in TestFramework - Newtonsoft.JSON
        /// </summary>
        public interface IMeshJsonSerializer
        {
            string Serialize(SerializableMesh mesh);
            SerializableMesh Deserialize(string json);
        }

        public interface IMesh
        {
            Vertex[] Vertices { get; }
            int[] Indices { get; }
            int VerticesCount { get; }
            int IndicesCount { get; }
        }
    }
}
