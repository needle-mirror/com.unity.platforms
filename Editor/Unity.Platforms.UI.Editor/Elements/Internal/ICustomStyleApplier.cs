using Unity.Properties;

namespace Unity.Platforms.UI
{
    interface ICustomStyleApplier
    {
        void ApplyStyleAtPath(PropertyPath propertyPath);
    }
}
