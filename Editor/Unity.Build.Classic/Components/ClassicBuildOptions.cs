using Unity.Serialization;
using UnityEditor;
using UnityEngine;

namespace Unity.Build.Classic
{
    /// <summary>
    /// Add <see cref="BuildOptions.ConnectToHost"/> to BuildPlayer options.
    /// </summary>
    public sealed class ConnectToHost : IBuildComponent { }

    /// <summary>
    /// Add <see cref="BuildOptions.EnableHeadlessMode"/> to BuildPlayer options.
    /// </summary>
    public sealed class EnableHeadlessMode : IBuildComponent { }

    /// <summary>
    /// Add <see cref="BuildOptions.IncludeTestAssemblies"/> to BuildPlayer options.
    /// </summary>
    [FormerName("Unity.Build.Common.TestablePlayer, Unity.Build.Common")]
    public sealed class IncludeTestAssemblies : IBuildComponent { }

    /// <summary>
    /// Add <see cref="BuildOptions.InstallInBuildFolder"/> to BuildPlayer options.
    /// </summary>
    [FormerName("Unity.Build.Common.SourceBuildConfiguration, Unity.Build.Common")]
    public sealed class InstallInBuildFolder : IBuildComponent { }

    /// <summary>
    /// Add <see cref="BuildOptions.WaitForPlayerConnection"/> to BuildPlayer options.
    /// </summary>
    public sealed class WaitForPlayerConnection : IBuildComponent { }

    [HideInInspector]
    internal sealed class AutoRunPlayer : IBuildComponent { }
}
