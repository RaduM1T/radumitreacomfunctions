using System.IO;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;

namespace radumitreacomfunctions;

public class NewtonsoftCosmosSerializer : CosmosSerializer
{
    private static readonly JsonSerializer Serializer = new JsonSerializer();

    public override T FromStream<T>(Stream stream)
    {
        using (stream)
        using (var sr = new StreamReader(stream))
        using (var jtr = new JsonTextReader(sr))
        {
            return Serializer.Deserialize<T>(jtr)!;
        }
    }

    public override Stream ToStream<T>(T input)
    {
        var ms = new MemoryStream();
        using (var sw = new StreamWriter(ms, leaveOpen: true))
        using (var jtw = new JsonTextWriter(sw))
        {
            Serializer.Serialize(jtw, input);
        }
        ms.Position = 0;
        return ms;
    }
}
