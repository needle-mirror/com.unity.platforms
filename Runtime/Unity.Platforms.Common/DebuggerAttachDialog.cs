#if !UNITY_EDITOR
    using System.Runtime.InteropServices;
namespace Unity.Platforms
{
    public static class DebuggerAttachDialog
    {
        [DllImport("lib_unity_platforms_common")]
        private static extern void ShowDebuggerAttachDialog(string message);

        public static void Show()
        {
            ShowDebuggerAttachDialog("You can attach a managed debugger now if you want");
        }
    }
}
#endif
