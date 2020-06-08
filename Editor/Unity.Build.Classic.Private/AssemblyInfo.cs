﻿using System.Runtime.CompilerServices;

// Per platform access
[assembly: InternalsVisibleTo("Unity.Build.Android.Classic")]
// TODO: com.unity.platforms.ios\Editor\Unity.Build.iOS.Classic\Unity.Build.iOS.Classic.asmdef set assembly name to Unity.Platforms.iOS.Build, since
//       it's accessing internals of build\iOSSupport\UnityEditor.iOS.Extensions.Xcode.dll and that has InternalsVisibleTo("Unity.Platforms.iOS.Build")
[assembly: InternalsVisibleTo("Unity.Platforms.iOS.Build")]
[assembly: InternalsVisibleTo("Unity.Build.iOS.Classic")]
[assembly: InternalsVisibleTo("Unity.Build.macOS.Classic")]
[assembly: InternalsVisibleTo("Unity.Build.Windows.Classic")]
