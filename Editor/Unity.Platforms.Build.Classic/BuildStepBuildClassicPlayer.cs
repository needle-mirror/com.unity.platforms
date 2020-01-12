using System;
using System.IO;
using Unity.Platforms.Build.Common;
using Unity.Properties;
using UnityEditor;

namespace Unity.Platforms.Build.Classic
{
    [BuildStep(Name = "Build Player", Description = "Building Player", Category = "Classic")]
    [FormerlySerializedAs("Unity.Build.Common.BuildStepBuildClassicPlayer, Unity.Build.Common")]
    sealed class BuildStepBuildClassicPlayer : BuildStep
    {
        const string k_BootstrapFilePath = "Assets/StreamingAssets/livelink-bootstrap.txt";

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

        /// <summary>
        /// Returns true if we need to use BuildOptions.AutoRunPlayer. 
        /// For ex., when platform doesn't have RunStep implemented yet.
        /// This function should be removed when we'll have run steps implemented for all platforms
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static bool UseAutoRunPlayer(BuildContext context)
        {
            var settings = Unity.Platforms.Build.Internals.BuildContextInternals.GetBuildConfiguration(context);
            var pipeline = settings.GetComponent<IBuildPipelineComponent>().Pipeline;
            var runStep = pipeline.RunStep;

            // RunStep is provided no need to use AutoRunPlayer
            if (runStep != null && runStep.GetType() != typeof(RunStepNotImplemented))
                return false;

            // See dots\Samples\Library\PackageCache\com.unity.build@0.1.0-preview.1\Editor\Unity.Build\BuildSettingsScriptedImporterEditor.cs
            const string k_CurrentActionKey = "BuildAction-CurrentAction";
            if (!EditorPrefs.HasKey(k_CurrentActionKey))
                return false;

            var value = EditorPrefs.GetInt(k_CurrentActionKey);
            return value == 1;
        }

        public static bool Prepare(BuildContext context, BuildStep step, TemporaryFileTracker tracker, out BuildStepResult failure, out BuildPlayerOptions buildPlayerOptions)
        {
            buildPlayerOptions = default;
            var profile = step.GetRequiredComponent<ClassicBuildProfile>(context);
            if (profile.Target <= 0)
            {
                failure = BuildStepResult.Failure(step, $"Invalid build target '{profile.Target.ToString()}'.");
                return false;
            }

            if (profile.Target != EditorUserBuildSettings.activeBuildTarget)
            {
                failure = BuildStepResult.Failure(step, $"{nameof(EditorUserBuildSettings.activeBuildTarget)} must be switched before {nameof(BuildStepBuildClassicPlayer)} step.");
                return false;
            }

            var scenesList = step.GetRequiredComponent<SceneList>(context).GetScenePathsForBuild();
            if (scenesList.Length == 0)
            {
                failure = BuildStepResult.Failure(step, "There are no scenes to build.");
                return false;
            }

            var outputPath = step.GetOutputBuildDirectory(context);
            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }

            var productName = step.GetRequiredComponent<GeneralSettings>(context).ProductName;
            var extension = profile.GetExecutableExtension();
            var locationPathName = Path.Combine(outputPath, productName + extension);

            buildPlayerOptions = new BuildPlayerOptions()
            {
                scenes = scenesList,
                target = profile.Target,
                locationPathName = locationPathName,
                targetGroup = UnityEditor.BuildPipeline.GetBuildTargetGroup(profile.Target),
            };

            buildPlayerOptions.options = BuildOptions.None;
            switch (profile.Configuration)
            {
                case BuildType.Debug:
                    buildPlayerOptions.options |= BuildOptions.AllowDebugging | BuildOptions.Development;
                    break;
                case BuildType.Develop:
                    buildPlayerOptions.options |= BuildOptions.Development;
                    break;
            }

            var sourceBuild = step.GetOptionalComponent<SourceBuildConfiguration>(context);
            if (sourceBuild.Enabled)
            {
                buildPlayerOptions.options |= BuildOptions.InstallInBuildFolder;
            }

            if (UseAutoRunPlayer(context))
            {
                UnityEngine.Debug.Log($"Using BuildOptions.AutoRunPlayer, since RunStep is not provided for {profile.Target}");
                buildPlayerOptions.options |= BuildOptions.AutoRunPlayer;
            }


            failure = default;
            return true;
        }

        public override BuildStepResult RunBuildStep(BuildContext context)
        {
            m_TemporaryFileTracker = new TemporaryFileTracker();
            if (!Prepare(context, this, m_TemporaryFileTracker, out var failure, out var options))
                return failure;
            else
                m_TemporaryFileTracker.EnsureFileDoesntExist(k_BootstrapFilePath);

            var report = UnityEditor.BuildPipeline.BuildPlayer(options);
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
