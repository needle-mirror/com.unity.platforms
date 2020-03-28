using System;
using Unity.Serialization.Json;
using Unity.Serialization.Json.Adapters.Contravariant;
using UnityEditor;

namespace Unity.Build
{
    sealed class RunStepJsonAdapter : IJsonAdapter<RunStep>
    {
        [InitializeOnLoadMethod]
        static void Register() => JsonSerialization.AddGlobalAdapter(new RunStepJsonAdapter());

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

            if (TypeConstructionHelper.TryConstructFromAssemblyQualifiedTypeName<RunStep>(json, out var step))
            {
                return step;
            }

            throw new ArgumentException($"Failed to construct type. Could not resolve type from TypeName=[{json}].");
        }

        public void Serialize(JsonStringBuffer writer, RunStep value)
        {
            string json = null;
            if (value != null)
            {
                json = value.GetType().GetQualifedAssemblyTypeName();
            }
            writer.WriteEncodedJsonString(json);
        }
    }
}
