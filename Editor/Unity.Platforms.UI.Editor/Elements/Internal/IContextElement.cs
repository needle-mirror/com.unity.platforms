using Unity.Properties;

namespace Unity.Platforms.UI
{
    interface IContextElement
    {
        PropertyPath Path { get; }

        void SetContext(BindingContextElement root, PropertyPath path);
        void OnContextReady();
    }
}
