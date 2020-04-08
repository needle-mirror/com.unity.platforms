using System;
using System.Linq;
using Unity.Build.Classic;
using Unity.BuildSystem.NativeProgramSupport;
using Unity.Properties.Editor;

namespace Unity.Build.Editor
{
    sealed class PlatformInspector : TypeInspector<Platform>
    {
        public override string SearcherTitle => "Select Platform";
        public override Func<Type, bool> TypeFilter => type => TypeCacheHelper.ConstructTypesDerivedFrom<ClassicPipelineBase>(false).Any(pipeline => pipeline.Platform.GetType() == type);
        public override Func<Type, string> TypeNameResolver => type => TypeConstruction.Construct<Platform>(type).DisplayName;
        public override Func<Type, string> TypeCategoryResolver => type => null;
    }
}
