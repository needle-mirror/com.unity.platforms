using System;
using Unity.Serialization;
using BuildTarget = Unity.Build.DotsRuntime.BuildTarget;

namespace Unity.Build.DotsRuntime
{
    [FormerName("Unity.Entities.Runtime.Build.DotsRuntimeBuildPipelineBase, Unity.Entities.Runtime.Build")]
    public abstract class DotsRuntimeBuildPipelineBase : BuildPipelineBase
    {
        public BuildTarget Target { get; set; }

        protected Type[] TargetUsedComponents
        {
            get
            {
                return Target != null ? Target.UsedComponents : new Type[]{};
            }
        }
    }
}
