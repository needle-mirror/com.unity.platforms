using System;
using Unity.Build.Common;
using UnityEditor;

namespace Unity.Build.Classic.Private
{
    class ClassicBuildCustomizer : ClassicBuildPipelineCustomizer
    {
        public override Type[] UsedComponents { get; } =
        {
            typeof(ClassicBuildProfile),
            typeof(AutoRunPlayer),
            typeof(EnableHeadlessMode),
            typeof(IncludeTestAssemblies),
            typeof(InstallInBuildFolder),
            typeof(PlayerConnectionSettings),
            typeof(EnableScriptDebugging),
            typeof(PlayerScriptingDefines)
        };

        public override BuildOptions ProvideBuildOptions()
        {
            var options = BuildOptions.None;

            // Build options from build type
            if (Context.TryGetComponent<ClassicBuildProfile>(out var profile))
            {
                switch (profile.Configuration)
                {
                    case BuildType.Debug:
                        options |= BuildOptions.AllowDebugging | BuildOptions.Development;
                        break;
                    case BuildType.Develop:
                        options |= BuildOptions.Development;
                        break;
                }
            }

            // Build options from components
            if (Context.HasComponent<AutoRunPlayer>())
                options |= BuildOptions.AutoRunPlayer;
// DOTS-5792
#pragma warning disable 618
            if (Context.HasComponent<EnableHeadlessMode>())
                options |= BuildOptions.EnableHeadlessMode;
#pragma warning restore 618 
            if (Context.HasComponent<IncludeTestAssemblies>())
                options |= BuildOptions.IncludeTestAssemblies;
            if (Context.HasComponent<InstallInBuildFolder>())
                options |= BuildOptions.InstallInBuildFolder;
            if (Context.TryGetComponent<PlayerConnectionSettings>(out PlayerConnectionSettings value))
            {
                if (value.Mode == PlayerConnectionInitiateMode.Connect)
                    options |= BuildOptions.ConnectToHost;
                if (value.WaitForConnection)
                    options |= BuildOptions.WaitForPlayerConnection;
            }

            if (Context.HasComponent<EnableScriptDebugging>())
                options |= BuildOptions.AllowDebugging;
            return options;
        }

        public override string[] ProvidePlayerScriptingDefines()
        {
            return Context.GetComponentOrDefault<PlayerScriptingDefines>().Defines;
        }
    }
}
