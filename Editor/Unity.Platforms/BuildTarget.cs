﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Unity.Platforms
{
    public abstract class BuildTarget
    {
        static readonly List<BuildTarget> m_AvailableBuildTargets = new List<BuildTarget>();
        static readonly Dictionary<string, BuildTarget> m_UnknownBuildTargets = new Dictionary<string, BuildTarget>();

        static BuildTarget()
        {
#if UNITY_2019_2_OR_NEWER
            var buildTargetTypes = UnityEditor.TypeCache.GetTypesDerivedFrom<BuildTarget>().ToList();
#else
            var buildTargetTypes = new List<Type>();
#endif

            if (buildTargetTypes.Count == 0)
            {
                // If UnityEditor.TypeCache wasn't ready, manually find all BuildTarget types
                var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.GetName().Name.Contains(typeof(BuildTarget).Assembly.GetName().Name));
                foreach (var assembly in assemblies)
                {
                    foreach (var type in assembly.GetLoadableTypes())
                    {
                        if (!typeof(BuildTarget).IsAssignableFrom(type))
                        {
                            continue;
                        }
                        buildTargetTypes.Add(type);
                    }
                }
            }

            foreach (var buildTargetType in buildTargetTypes)
            {
                try
                {
                    if (buildTargetType.IsAbstract)
                    {
                        continue;
                    }

                    if (buildTargetType == typeof(UnknownBuildTarget))
                    {
                        continue;
                    }

                    var buildTarget = (BuildTarget)Activator.CreateInstance(buildTargetType);
                    m_AvailableBuildTargets.Add(buildTarget);
                    if (buildTarget.IsDefaultBuildTarget)
                    {
                        if (DefaultBuildTarget != null)
                        {
                            UnityEngine.Debug.LogError($"Cannot set {nameof(DefaultBuildTarget)} to '{buildTarget.GetType().FullName}' because it is already set to '{DefaultBuildTarget.GetType().FullName}'.");
                            continue;
                        }
                        DefaultBuildTarget = buildTarget;
                    }
                }
                catch (Exception e)
                {
                    UnityEngine.Debug.LogError($"Error instantiating '{buildTargetType.FullName}': " + e.Message);
                }
            }

            m_AvailableBuildTargets = m_AvailableBuildTargets.OrderBy(target => target.GetDisplayName()).ToList();
        }

        public static IReadOnlyList<BuildTarget> AvailableBuildTargets => m_AvailableBuildTargets.Concat(m_UnknownBuildTargets.Values).ToList();
        public static BuildTarget DefaultBuildTarget { get; }

        public virtual bool HideInBuildTargetPopup => false;
        protected virtual bool IsDefaultBuildTarget => false;
        public abstract string GetDisplayName();
        public abstract string GetUnityPlatformName();
        public abstract string GetExecutableExtension();
        public abstract string GetBeeTargetName();
        public abstract bool Run(FileInfo buildTarget);

        public override string ToString() => GetDisplayName();
        public static BuildTarget GetBuildTargetFromUnityPlatformName(string name) => GetBuildTargetFromName(name, (target) => target.GetUnityPlatformName());
        public static BuildTarget GetBuildTargetFromBeeTargetName(string name) => GetBuildTargetFromName(name, (target) => target.GetBeeTargetName());

        static BuildTarget GetBuildTargetFromName(string name, Func<BuildTarget, string> getBuildTargetName)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            var buildTarget = AvailableBuildTargets.FirstOrDefault(target => getBuildTargetName(target) == name);
            if (buildTarget == null)
            {
                if (!m_UnknownBuildTargets.TryGetValue(name, out buildTarget))
                {
                    buildTarget = new UnknownBuildTarget(name);
                    m_UnknownBuildTargets.Add(name, buildTarget);
                }
            }

            return buildTarget;
        }

        // this method requires default implementation to resolve problem with Samples project
        public virtual ShellProcessOutput RunTestMode(string exeName, string workingDirPath, int timeout)
        {
            throw new NotImplementedException();
        }
    }

    internal sealed class UnknownBuildTarget : BuildTarget
    {
        readonly string m_Name;

        public UnknownBuildTarget()
        {
        }

        public UnknownBuildTarget(string name)
        {
            m_Name = name;
        }

        public override string GetDisplayName() => $"Unknown ({m_Name})";
        public override string GetUnityPlatformName() => m_Name;
        public override string GetExecutableExtension() => null;
        public override string GetBeeTargetName() => m_Name;
        public override bool Run(FileInfo buildTarget) => false;
    }

    internal sealed class EditorBuildTarget : BuildTarget
    {
        public override bool HideInBuildTargetPopup => true;
        public override string GetDisplayName() => "Editor";
        public override string GetUnityPlatformName() => UnityEditor.EditorUserBuildSettings.activeBuildTarget.ToString();
        public override string GetExecutableExtension() => null;
        public override string GetBeeTargetName() => null;
        public override bool Run(FileInfo buildTarget) => false;
    }

    static class AssemblyExtensions
    {
        public static IEnumerable<Type> GetLoadableTypes(this Assembly assembly)
        {
            if (assembly == null)
            {
                return Enumerable.Empty<Type>();
            }

            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException exception)
            {
                return exception.Types.Where(type => type != null);
            }
        }
    }
}
