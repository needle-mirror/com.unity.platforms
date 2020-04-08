using System;
using UnityEditor;

namespace Unity.Build.Classic
{
    class ClassicBuildOptionsCustomizer : ClassicBuildPipelineCustomizer
    {
        public override Type[] UsedComponents { get; } =
        {
            typeof(ClassicBuildProfile),
            typeof(AutoRunPlayer),
            typeof(ConnectToHost),
            typeof(EnableHeadlessMode),
            typeof(IncludeTestAssemblies),
            typeof(InstallInBuildFolder),
            typeof(WaitForPlayerConnection),
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
            if (Context.HasComponent<ConnectToHost>())
                options |= BuildOptions.ConnectToHost;
            if (Context.HasComponent<EnableHeadlessMode>())
                options |= BuildOptions.EnableHeadlessMode;
            if (Context.HasComponent<IncludeTestAssemblies>())
                options |= BuildOptions.IncludeTestAssemblies;
            if (Context.HasComponent<InstallInBuildFolder>())
                options |= BuildOptions.InstallInBuildFolder;
            if (Context.HasComponent<WaitForPlayerConnection>())
                options |= BuildOptions.WaitForPlayerConnection;

            return options;
        }
    }
}
