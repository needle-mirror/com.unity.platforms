using System;
using Unity.Platforms.Build.Common;
using Unity.Properties;
using UnityEditor;

namespace Unity.Platforms.Build.Classic
{
    [BuildStep(Name = "Build LiveLink Player", Description = "Building LiveLink Player", Category = "Classic")]
    [FormerlySerializedAs("Unity.Build.Common.BuildStepBuildClassicLiveLink, Unity.Build.Common")]
    public sealed class BuildStepBuildClassicLiveLink : BuildStep
    {
        TemporaryFileTracker m_TemporaryFileTracker;

        public override Type[] RequiredComponents => new[]
        {
            typeof(ClassicBuildProfile),
            typeof(SceneList),
            typeof(GeneralSettings)
        };

        public override Type[] OptionalComponents => new[]
        {
            typeof(OutputBuildDirectory),
            typeof(SourceBuildConfiguration)
        };

        public override BuildStepResult RunBuildStep(BuildContext context)
        {
            m_TemporaryFileTracker = new TemporaryFileTracker();
            if (!BuildStepBuildClassicPlayer.Prepare(context, this, m_TemporaryFileTracker, out var failure, out var buildPlayerOptions))
            {
                return failure;
            }

            //@TODO: Allow debugging should be based on profile...
            buildPlayerOptions.options = BuildOptions.Development | BuildOptions.AllowDebugging | BuildOptions.ConnectToHost;

            var report = UnityEditor.BuildPipeline.BuildPlayer(buildPlayerOptions);
            context.SetValue(report);
            return Success();
        }

        public override BuildStepResult CleanupBuildStep(BuildContext context)
        {
            m_TemporaryFileTracker.Dispose();
            return Success();
        }
    }
}
