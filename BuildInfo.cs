using System.Reflection;
using System.Runtime.InteropServices;

[assembly: ComVisible(false)]
[assembly: AssemblyTitle(NecronomiconPriority.ModBuildInfo.GUID)]
[assembly: AssemblyProduct(NecronomiconPriority.ModBuildInfo.Name)]
[assembly: AssemblyVersion(NecronomiconPriority.ModBuildInfo.Version)]
[assembly: AssemblyCompany("shinter.dev")]

namespace NecronomiconPriority;
public static class ModBuildInfo
    {
        public const string Name = "Necronomicon Priority";
        public const string Author = "Shin";
        public const string GUID = "dev.shinter.tos2.necronomiconpriority";
        public const string Version = "1.0.0";
        public const string Description = "If you are Coven-Aligned this allows you to know who will obtain the Necronomicon next!";
        public const string DownloadLink = "N/A";
    }