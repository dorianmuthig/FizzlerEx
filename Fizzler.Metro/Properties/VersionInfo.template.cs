using System.Reflection;
[assembly: AssemblyVersion(_ThisAssemblyVersionInfo.BaseVersion)]
[assembly: AssemblyFileVersion(_ThisAssemblyVersionInfo.VersionNumber)]
[assembly: AssemblyInformationalVersion(_ThisAssemblyVersionInfo.VersionNumber + " ($REVID$)")]

internal static class _ThisAssemblyVersionInfo
{
    public const string BaseVersion = "1.0";
    public const string VersionNumber = BaseVersion + ".$REVNUM$.$DIRTY$";
}
