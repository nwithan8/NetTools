using System.Diagnostics;
using System.Runtime.InteropServices;

namespace NetTools;

public static class RuntimeInfo
{
    public struct ApplicationInfo
    {
        /// <summary>
        ///     Get the version of the application as a string.
        /// </summary>
        /// <returns>The version of the application as a string.</returns>
        public static string ApplicationVersion
        {
            get
            {
                try
                {
                    var assembly = typeof(ApplicationInfo).Assembly;
                    var info = FileVersionInfo.GetVersionInfo(assembly.Location);
                    return info.FileVersion ?? "Unknown";
                }
                catch (Exception)
                {
                    return "Unknown";
                }
            }
        }

        /// <summary>
        ///     Get the .NET framework version as a string.
        /// </summary>
        /// <returns>The .NET framework version as a string.</returns>
        public static string DotNetVersion => Environment.Version.ToString();
    }

    public struct OperationSystemInfo
    {
        /// <summary>
        ///     Get details about the operating system.
        /// </summary>
        /// <returns>Details about the operating system.</returns>
        public static OperatingSystem OperatingSystem => Environment.OSVersion;

        public static bool IsWindows => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        public static bool IsLinux => RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

        public static bool IsMacOs => RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

        /// <summary>
        ///     Get the name of the operating system.
        /// </summary>
        /// <returns>Name of the operating system.</returns>
        public static string Name
        {
            get
            {
#if NETCOREAPP3_0_OR_GREATER
                var name = "Unknown";
                var @switch = new SwitchCase
                {
                    { IsLinux, () => name = "Linux" },
                    { IsMacOs, () => name = "MacOS" },
                    { IsWindows, () => name = "Windows" },
                    { Scenario.Default, () => name = "Unknown" }
                };

                @switch.MatchFirst(true);
                return name;
#else
                var name = "Unknown";
                var @switch = new SwitchCase
                {
                    { PlatformID.Win32S, () => name = "Windows" },
                    { PlatformID.Win32Windows, () => name = "Windows" },
                    { PlatformID.Win32NT, () => name = "Windows" },
                    { PlatformID.WinCE, () => name = "Windows" },
                    { PlatformID.Unix, () => name = "Linux" },
                    { PlatformID.MacOSX, () => name = "Darwin" }, // in newer versions, Mac OS X is PlatformID.Unix unfortunately
                    { Scenario.Default, () => name = "Unknown" }
                };

                @switch.MatchFirst(OperatingSystem.Platform);
                return name;
#endif
            }
        }

        /// <summary>
        ///     Get the version of the operating system.
        /// </summary>
        /// <returns>Version of the operating system.</returns>
        public static string Version => OperatingSystem.Version.ToString();

        /// <summary>
        ///     Get the architecture of the operating system.
        /// </summary>
        /// <returns>Architecture of the operating system.</returns>
        public static string Architecture
        {
            get
            {
#if NET462
                // Sorry, Windows ARM users (if you exist), best we can do is determine if we are running on a 64-bit or 32-bit
                return Environment.Is64BitOperatingSystem ? "x64" : "x86";
#else
                var name = "Unknown";
                var @switch = new SwitchCase
                {
                    { System.Runtime.InteropServices.Architecture.Arm, () => name = "arm" },
                    { System.Runtime.InteropServices.Architecture.Arm64, () => name = "arm64" },
                    { System.Runtime.InteropServices.Architecture.X64, () => name = "x64" },
                    { System.Runtime.InteropServices.Architecture.X86, () => name = "x86" },
                    { Scenario.Default, () => name = "Unknown" }
                };

                @switch.MatchFirst(RuntimeInformation.OSArchitecture);
                return name;
#endif
            }
        }
    }
}
