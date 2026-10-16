using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace MonoMod.Utils
{
	// Token: 0x0200044F RID: 1103
	internal static class PlatformHelper
	{
		// Token: 0x0600176A RID: 5994 RVA: 0x00051340 File Offset: 0x0004F540
		private static void DeterminePlatform()
		{
			PlatformHelper._current = Platform.Unknown;
			PropertyInfo property = typeof(Environment).GetProperty("Platform", BindingFlags.Static | BindingFlags.NonPublic);
			string text;
			if (property != null)
			{
				text = property.GetValue(null, new object[0]).ToString();
			}
			else
			{
				text = Environment.OSVersion.Platform.ToString();
			}
			text = text.ToLower(CultureInfo.InvariantCulture);
			if (text.Contains("win"))
			{
				PlatformHelper._current = Platform.Windows;
			}
			else if (text.Contains("mac") || text.Contains("osx"))
			{
				PlatformHelper._current = Platform.MacOS;
			}
			else if (text.Contains("lin") || text.Contains("unix"))
			{
				PlatformHelper._current = Platform.Linux;
			}
			if (PlatformHelper._current != Platform.Unknown)
			{
				if (PlatformHelper.Is(Platform.Linux) && Directory.Exists("/data") && File.Exists("/system/build.prop"))
				{
					PlatformHelper._current = Platform.Android;
				}
				else if (PlatformHelper.Is(Platform.Unix) && Directory.Exists("/Applications") && Directory.Exists("/System") && Directory.Exists("/User") && !Directory.Exists("/Users"))
				{
					PlatformHelper._current = Platform.iOS;
				}
				else if (PlatformHelper.Is(Platform.Windows) && PlatformHelper.CheckWine())
				{
					PlatformHelper._current |= Platform.Wine;
				}
			}
			PropertyInfo property2 = typeof(Environment).GetProperty("Is64BitOperatingSystem");
			MethodInfo methodInfo = ((property2 != null) ? property2.GetGetMethod() : null);
			if (methodInfo != null)
			{
				PlatformHelper._current |= (((bool)methodInfo.Invoke(null, new object[0])) ? Platform.Bits64 : ((Platform)0));
			}
			else
			{
				PlatformHelper._current |= ((IntPtr.Size >= 8) ? Platform.Bits64 : ((Platform)0));
			}
			if (PlatformHelper._current != Platform.Unknown && (PlatformHelper.Is(Platform.Unix) || PlatformHelper.Is(Platform.Unknown)) && ReflectionHelper.IsMono)
			{
				try
				{
					string text2;
					using (Process process = Process.Start(new ProcessStartInfo("uname", "-m")
					{
						UseShellExecute = false,
						RedirectStandardOutput = true
					}))
					{
						text2 = process.StandardOutput.ReadLine().Trim();
					}
					if (text2.StartsWith("aarch", StringComparison.Ordinal) || text2.StartsWith("arm", StringComparison.Ordinal))
					{
						PlatformHelper._current |= Platform.ARM;
					}
					return;
				}
				catch (Exception)
				{
					return;
				}
			}
			PortableExecutableKinds portableExecutableKinds;
			ImageFileMachine imageFileMachine;
			typeof(object).Module.GetPEKind(out portableExecutableKinds, out imageFileMachine);
			if (imageFileMachine == ImageFileMachine.ARM)
			{
				PlatformHelper._current |= Platform.ARM;
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x0600176B RID: 5995 RVA: 0x000515FC File Offset: 0x0004F7FC
		// (set) Token: 0x0600176C RID: 5996 RVA: 0x0005161E File Offset: 0x0004F81E
		public static Platform Current
		{
			get
			{
				if (!PlatformHelper._currentLocked)
				{
					if (PlatformHelper._current == Platform.Unknown)
					{
						PlatformHelper.DeterminePlatform();
					}
					PlatformHelper._currentLocked = true;
				}
				return PlatformHelper._current;
			}
			set
			{
				if (PlatformHelper._currentLocked)
				{
					throw new InvalidOperationException("Cannot set the value of PlatformHelper.Current once it has been accessed.");
				}
				PlatformHelper._current = value;
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x0600176D RID: 5997 RVA: 0x00051638 File Offset: 0x0004F838
		public static string LibrarySuffix
		{
			get
			{
				if (PlatformHelper._librarySuffix == null)
				{
					PlatformHelper._librarySuffix = (PlatformHelper.Is(Platform.MacOS) ? "dylib" : (PlatformHelper.Is(Platform.Unix) ? "so" : "dll"));
				}
				return PlatformHelper._librarySuffix;
			}
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x0005166F File Offset: 0x0004F86F
		public static bool Is(Platform platform)
		{
			return (PlatformHelper.Current & platform) == platform;
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x0005167C File Offset: 0x0004F87C
		private static bool CheckWine()
		{
			string text = Environment.GetEnvironmentVariable("MONOMOD_WINE");
			if (text == "1")
			{
				return true;
			}
			if (text == "0")
			{
				return false;
			}
			string environmentVariable = Environment.GetEnvironmentVariable("XL_WINEONLINUX");
			text = ((environmentVariable != null) ? environmentVariable.ToLower(CultureInfo.InvariantCulture) : null);
			if (text == "true")
			{
				return true;
			}
			if (text == "false")
			{
				return false;
			}
			IntPtr moduleHandle = PlatformHelper.GetModuleHandle("ntdll.dll");
			return moduleHandle != IntPtr.Zero && PlatformHelper.GetProcAddress(moduleHandle, "wine_get_version") != IntPtr.Zero;
		}

		// Token: 0x06001770 RID: 6000
		[DllImport("kernel32", SetLastError = true)]
		private static extern IntPtr GetModuleHandle(string lpModuleName);

		// Token: 0x06001771 RID: 6001
		[DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

		// Token: 0x04001034 RID: 4148
		private static Platform _current = Platform.Unknown;

		// Token: 0x04001035 RID: 4149
		private static bool _currentLocked = false;

		// Token: 0x04001036 RID: 4150
		private static string _librarySuffix;
	}
}
