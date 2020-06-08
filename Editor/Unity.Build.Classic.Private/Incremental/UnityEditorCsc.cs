using Bee.Core;
using System;
using Bee.CSharpSupport;
using Bee.Tools;
using UnityEditor;

namespace Unity.Build.Classic.Private
{
    internal class UnityEditorCsc : Csc
    {
        public UnityEditorCsc()
        {
            CompilerProgram = new NativeRunnableProgram($"{EditorApplication.applicationContentsPath}/Tools/Roslyn/" + (HostPlatform.IsWindows ? "csc.exe" : "csc"));
        }

        protected override RunnableProgram CompilerProgram { get; }

        public override string ActionName { get; } = "UnityCsc";
        public override Func<CSharpCompiler> StaticFunctionToCreateMe { get; } = () => new UnityEditorCsc();
    }
}
