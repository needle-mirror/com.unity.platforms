using System;
using System.Collections.Generic;
using Unity.BuildSystem.NativeProgramSupport;

namespace Unity.Build.Classic
{
    static class KnownPlatforms
    {
        public static Dictionary<Type, string> All { get; } = new Dictionary<Type, string>
        {
            {typeof(WindowsPlatform), "com.platform.windows"},
            {typeof(MacOSXPlatform), "com.platform.macos"},
            {typeof(LinuxPlatform), "com.platform.linux"},
            {typeof(IosPlatform), "com.platform.ios"},
            {typeof(AndroidPlatform), "com.platform.android"}
        };
    }
}
