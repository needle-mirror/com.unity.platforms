namespace Unity.Platforms.UI.Tests
{
    internal class DrawerAttribute : UnityEngine.PropertyAttribute {}

    internal interface IUserInspectorTag {}
    internal interface IAnotherUserInspectorTag {}

    internal class NoInspectorType {}

    internal class SingleInspectorType {}
    class SingleInspectorTypeInspector : PropertyInspector<SingleInspectorType> {}

    internal class MultipleInspectorsType {}
    internal class MultipleInspectorsTypeInspector : PropertyInspector<MultipleInspectorsType> {}
    internal class MultipleInspectorsTypeInspectorWithTag : PropertyInspector<MultipleInspectorsType>, IUserInspectorTag {}


    internal class NoInspectorButDrawerType {}
    internal class NoInspectorButDrawerTypeDrawer : PropertyInspector<NoInspectorButDrawerType, DrawerAttribute> {}

    internal class InspectorAndDrawerType {}
    internal class InspectorAndDrawerTypeInspector : PropertyInspector<InspectorAndDrawerType> {}

    internal class InspectorAndDrawerTypeTypeDrawer : PropertyInspector<InspectorAndDrawerType, DrawerAttribute>, IAnotherUserInspectorTag {}

    internal class InspectorAndDrawerTypeTypeDrawerWithTag : PropertyInspector<InspectorAndDrawerType, DrawerAttribute>, IUserInspectorTag {}
}
