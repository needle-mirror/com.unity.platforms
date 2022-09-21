using System;

namespace Unity.Build.Common
{
    public sealed partial class GeneralSettings
    {
        public string ProductName = "Product Name";
        public string CompanyName = "Company Name";
#if UNITY_EDITOR
        [Unity.Platforms.UI.SystemVersionUsage(Unity.Platforms.UI.SystemVersionUsage.MajorMinor)]
#endif
        public Version Version = new Version(1, 0);
    }
}
