using System;
using Unity.Serialization.Json;
using Unity.Serialization.Json.Adapters.Contravariant;
using UnityEditor;

namespace Unity.Build
{
    sealed class BuildStepJsonAdapter : IJsonAdapter<IBuildStep>
    {
        static readonly string s_EmptyGlobalObjectId = new GlobalObjectId().ToString();

        [InitializeOnLoadMethod]
        static void Register() => JsonSerialization.AddGlobalAdapter(new BuildStepJsonAdapter());

        public object Deserialize(SerializedValueView view)
        {
            if (view.Type != TokenType.String)
            {
                return null;
            }

            var json = view.AsStringView().ToString();
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }

            // Workaround issue where GlobalObjectId.TryParse returns false for empty GlobalObjectId
            if (json == s_EmptyGlobalObjectId)
            {
                return null;
            }

            if (GlobalObjectId.TryParse(json, out var id))
            {
                if (id.assetGUID.Empty())
                {
                    return null;
                }

                var obj = GlobalObjectId.GlobalObjectIdentifierToObjectSlow(id);
                if (obj == null || !obj)
                {
                    throw new InvalidOperationException($"An error occured while deserializing asset reference GUID=[{id.assetGUID.ToString()}]. Asset is not yet loaded and will result in a null reference.");
                }

                if (obj is BuildPipeline pipeline)
                {
                    return pipeline;
                }

                throw new InvalidOperationException($"An error occured while deserializing asset reference GUID=[{id.assetGUID.ToString()}]. Asset is not a {nameof(BuildPipeline)}.");
            }
            else
            {
                if (TypeConstructionHelper.TryConstructFromAssemblyQualifiedTypeName<IBuildStep>(json, out var step))
                {
                    return step;
                }

                throw new ArgumentException($"Failed to construct type. Could not resolve type from TypeName=[{json}].");
            }
        }

        public void Serialize(JsonStringBuffer writer, IBuildStep value)
        {
            string json = null;
            if (value != null)
            {
                if (value is BuildPipeline pipeline)
                {
                    json = GlobalObjectId.GetGlobalObjectIdSlow(pipeline).ToString();
                }
                else
                {
                    json = value.GetType().GetQualifedAssemblyTypeName();
                }
            }
            writer.WriteEncodedJsonString(json);
        }
    }
}
