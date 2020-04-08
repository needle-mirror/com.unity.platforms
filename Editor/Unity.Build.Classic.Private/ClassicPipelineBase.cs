using System;
using System.IO;
using System.Linq;
using Unity.Build.Common;
using Unity.BuildSystem.NativeProgramSupport;
using UnityEditor;

namespace Unity.Build.Classic
{
    public abstract class ClassicPipelineBase : BuildPipelineBase
    {
        Type[] CustomizersUsedComponents =>
            TypeCacheHelper.ConstructTypesDerivedFrom<ClassicBuildPipelineCustomizer>()
            .SelectMany(customizer => customizer.UsedComponents)
            .Distinct().ToArray();

        public override Type[] UsedComponents => base.UsedComponents.Concat(CustomizersUsedComponents).Distinct().ToArray();

        protected abstract BuildTarget BuildTarget { get; }
        public abstract Platform Platform { get; }

        protected virtual void PrepareContext(BuildContext context)
        {
            var buildType = context.GetComponentOrDefault<ClassicBuildProfile>().Configuration;
            bool isDevelopment = buildType == BuildType.Debug || buildType == BuildType.Develop;
            var playbackEngineDirectory = UnityEditor.BuildPipeline.GetPlaybackEngineDirectory(BuildTarget, isDevelopment ? BuildOptions.Development : BuildOptions.None);

            var customizers = TypeCacheHelper.ConstructTypesDerivedFrom<ClassicBuildPipelineCustomizer>().ToArray();
            foreach (var customizer in customizers)
                customizer.m_Info = new CustomizerInfoImpl(context);

            context.SetValue(new ClassicSharedData()
            {
                BuildTarget = BuildTarget,
                PlaybackEngineDirectory = playbackEngineDirectory,
                BuildToolsDirectory = Path.Combine(playbackEngineDirectory, "Tools"),
                DevelopmentPlayer = isDevelopment,
                Customizers = customizers
            });

            var scenes = context.GetComponentOrDefault<SceneList>().GetScenePathsForBuild();
            foreach (var modifier in customizers)
                scenes = modifier.ModifyEmbeddedScenes(scenes);
            context.SetValue(new EmbeddedScenesValue() { Scenes = scenes });

            foreach (var customizer in customizers)
                customizer.OnBeforeBuild();
        }
    }
}
