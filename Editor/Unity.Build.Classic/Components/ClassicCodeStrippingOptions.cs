using Unity.Properties;
using Unity.Serialization;
using UnityEditor;

namespace Unity.Build.Classic
{
    public sealed class ClassicCodeStrippingOptions : IBuildComponent
    {
        [CreateProperty]
        public bool StripEngineCode { get; set; } = true;

        [CreateProperty]
        public ManagedStrippingLevel ManagedStrippingLevel { get; set; } = ManagedStrippingLevel.Low;
    }
}
