using System;
using Unity.Serialization.Json;
using UnityEditor;

namespace Unity.Build
{
    sealed class BuildPipelineBaseJsonAdapter : IJsonAdapter<BuildPipelineBase>
    {
        [InitializeOnLoadMethod]
        static void Register() => JsonSerialization.AddGlobalAdapter(new BuildPipelineBaseJsonAdapter());

        void IJsonAdapter<BuildPipelineBase>.Serialize(in JsonSerializationContext<BuildPipelineBase> context, BuildPipelineBase value)
        {
            string json = null;
            if (value != null)
            {
                json = value.GetType().GetAssemblyQualifiedTypeName();
            }
            context.Writer.WriteValue(json);
        }

        BuildPipelineBase IJsonAdapter<BuildPipelineBase>.Deserialize(in JsonDeserializationContext<BuildPipelineBase> context)
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

            if (TypeConstructionUtility.TryConstructFromAssemblyQualifiedTypeName<BuildPipelineBase>(json, out var step))
            {
                return step;
            }

            throw new ArgumentException($"Failed to construct type. Could not resolve type from TypeName=[{json}].");
        }
    }
}
