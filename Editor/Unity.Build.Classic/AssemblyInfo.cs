using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("Unity.Build.Classic.Private")]

// Per platform access
[assembly: InternalsVisibleTo("Unity.Build.Android.Classic")]
[assembly: InternalsVisibleTo("Unity.Build.iOS.Classic")]
[assembly: InternalsVisibleTo("Unity.Build.macOS.Classic")]
[assembly: InternalsVisibleTo("Unity.Build.Windows.Classic")]

// For GetBuildTarget extension
[assembly: InternalsVisibleTo("Unity.Build.Android")]

// For ClassicBuildProfile.Platform property
[assembly: InternalsVisibleTo("Unity.Entities.Editor")]
[assembly: InternalsVisibleTo("Unity.Entities.Editor.Tests")]
[assembly: InternalsVisibleTo("Unity.Scenes.Editor")]
