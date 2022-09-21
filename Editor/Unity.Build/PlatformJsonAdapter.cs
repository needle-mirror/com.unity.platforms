using Unity.Serialization.Json;
using UnityEditor;

namespace Unity.Build
{
    sealed class PlatformJsonAdapter : IJsonAdapter<Platform>
    {
        [InitializeOnLoadMethod]
        static void Register() => JsonSerialization.AddGlobalAdapter(new PlatformJsonAdapter());

        void IJsonAdapter<Platform>.Serialize(in JsonSerializationContext<Platform> context, Platform value)
        {
            string json = null;
            if (value != null)
            {
                json = value.Name;
            }
            context.Writer.WriteValue(json);
        }

        Platform IJsonAdapter<Platform>.Deserialize(in JsonDeserializationContext<Platform> context)
        {
            if (context.SerializedValue.Type != TokenType.String)
            {
                return null;
            }

            var json = context.SerializedValue.AsStringView().ToString();
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }

            return Platform.GetPlatformByName(json);
        }
    }
}
