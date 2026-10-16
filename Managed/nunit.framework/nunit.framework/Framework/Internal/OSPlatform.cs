using System;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Win32;

namespace NUnit.Framework.Internal
{
	// Token: 0x02000070 RID: 112
	[SecuritySafeCritical]
	public class OSPlatform
	{
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x00008524 File Offset: 0x00006724
		public static OSPlatform CurrentPlatform
		{
			get
			{
				return OSPlatform.currentPlatform.Value;
			}
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00008530 File Offset: 0x00006730
		private static Version GetWindows81PlusVersion(Version version)
		{
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion"))
				{
					if (registryKey != null)
					{
						string text = registryKey.GetValue("CurrentBuildNumber") as string;
						int num = 0;
						int.TryParse(text, out num);
						int? num2 = registryKey.GetValue("CurrentMajorVersionNumber") as int?;
						int? num3 = registryKey.GetValue("CurrentMinorVersionNumber") as int?;
						if (num2 != null && num3 != null)
						{
							return new Version(num2.Value, num3.Value, num);
						}
						if (registryKey.GetValue("CurrentVersion") as string == "6.3")
						{
							return new Version(6, 3, num);
						}
					}
				}
			}
			catch (Exception)
			{
			}
			return version;
		}

		// Token: 0x060003CA RID: 970
		[DllImport("Kernel32.dll")]
		private static extern bool GetVersionEx(ref OSPlatform.OSVERSIONINFOEX osvi);

		// Token: 0x060003CB RID: 971 RVA: 0x0000861C File Offset: 0x0000681C
		public OSPlatform(PlatformID platform, Version version)
		{
			this._platform = platform;
			this._version = version;
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00008632 File Offset: 0x00006832
		public OSPlatform(PlatformID platform, Version version, OSPlatform.ProductType product)
			: this(platform, version)
		{
			this._product = product;
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060003CD RID: 973 RVA: 0x00008643 File Offset: 0x00006843
		public PlatformID Platform
		{
			get
			{
				return this._platform;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060003CE RID: 974 RVA: 0x0000864B File Offset: 0x0000684B
		public Version Version
		{
			get
			{
				return this._version;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060003CF RID: 975 RVA: 0x00008653 File Offset: 0x00006853
		public OSPlatform.ProductType Product
		{
			get
			{
				return this._product;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x0000865B File Offset: 0x0000685B
		public bool IsWindows
		{
			get
			{
				return this._platform == PlatformID.Win32NT || this._platform == PlatformID.Win32Windows || this._platform == PlatformID.Win32S || this._platform == PlatformID.WinCE;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x00008682 File Offset: 0x00006882
		public bool IsUnix
		{
			get
			{
				return this._platform == OSPlatform.UnixPlatformID_Microsoft || this._platform == OSPlatform.UnixPlatformID_Mono;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x000086A0 File Offset: 0x000068A0
		public bool IsWin32S
		{
			get
			{
				return this._platform == PlatformID.Win32S;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x000086AB File Offset: 0x000068AB
		public bool IsWin32Windows
		{
			get
			{
				return this._platform == PlatformID.Win32Windows;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x000086B6 File Offset: 0x000068B6
		public bool IsWin32NT
		{
			get
			{
				return this._platform == PlatformID.Win32NT;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x000086C1 File Offset: 0x000068C1
		public bool IsWinCE
		{
			get
			{
				return this._platform == PlatformID.WinCE;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x000086CC File Offset: 0x000068CC
		public bool IsXbox
		{
			get
			{
				return this._platform == OSPlatform.XBoxPlatformID;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x000086DB File Offset: 0x000068DB
		public bool IsMacOSX
		{
			get
			{
				return this._platform == OSPlatform.MacOSXPlatformID;
			}
		}

		// Token: 0x060003D8 RID: 984
		[DllImport("libc")]
		private static extern int uname(IntPtr buf);

		// Token: 0x060003D9 RID: 985 RVA: 0x000086EC File Offset: 0x000068EC
		private static bool CheckIfIsMacOSX(PlatformID platform)
		{
			if (platform == PlatformID.MacOSX)
			{
				return true;
			}
			if (platform != PlatformID.Unix)
			{
				return false;
			}
			IntPtr intPtr = Marshal.AllocHGlobal(8192);
			bool flag = false;
			if (OSPlatform.uname(intPtr) == 0)
			{
				flag = Marshal.PtrToStringAnsi(intPtr).Equals("Darwin");
			}
			Marshal.FreeHGlobal(intPtr);
			return flag;
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060003DA RID: 986 RVA: 0x00008732 File Offset: 0x00006932
		public bool IsWin95
		{
			get
			{
				return this._platform == PlatformID.Win32Windows && this._version.Major == 4 && this._version.Minor == 0;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060003DB RID: 987 RVA: 0x0000875B File Offset: 0x0000695B
		public bool IsWin98
		{
			get
			{
				return this._platform == PlatformID.Win32Windows && this._version.Major == 4 && this._version.Minor == 10;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060003DC RID: 988 RVA: 0x00008785 File Offset: 0x00006985
		public bool IsWinME
		{
			get
			{
				return this._platform == PlatformID.Win32Windows && this._version.Major == 4 && this._version.Minor == 90;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060003DD RID: 989 RVA: 0x000087AF File Offset: 0x000069AF
		public bool IsNT3
		{
			get
			{
				return this._platform == PlatformID.Win32NT && this._version.Major == 3;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060003DE RID: 990 RVA: 0x000087CA File Offset: 0x000069CA
		public bool IsNT4
		{
			get
			{
				return this._platform == PlatformID.Win32NT && this._version.Major == 4;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060003DF RID: 991 RVA: 0x000087E5 File Offset: 0x000069E5
		public bool IsNT5
		{
			get
			{
				return this._platform == PlatformID.Win32NT && this._version.Major == 5;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x00008800 File Offset: 0x00006A00
		public bool IsWin2K
		{
			get
			{
				return this.IsNT5 && this._version.Minor == 0;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x0000881A File Offset: 0x00006A1A
		public bool IsWinXP
		{
			get
			{
				return this.IsNT5 && (this._version.Minor == 1 || (this._version.Minor == 2 && this.Product == OSPlatform.ProductType.WorkStation));
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x0000884F File Offset: 0x00006A4F
		public bool IsWin2003Server
		{
			get
			{
				return this.IsNT5 && this._version.Minor == 2 && this.Product == OSPlatform.ProductType.Server;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x00008872 File Offset: 0x00006A72
		public bool IsNT6
		{
			get
			{
				return this._platform == PlatformID.Win32NT && this._version.Major == 6;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x0000888D File Offset: 0x00006A8D
		public bool IsNT60
		{
			get
			{
				return this.IsNT6 && this._version.Minor == 0;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x000088A7 File Offset: 0x00006AA7
		public bool IsNT61
		{
			get
			{
				return this.IsNT6 && this._version.Minor == 1;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x000088C1 File Offset: 0x00006AC1
		public bool IsNT62
		{
			get
			{
				return this.IsNT6 && this._version.Minor == 2;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x000088DB File Offset: 0x00006ADB
		public bool IsNT63
		{
			get
			{
				return this.IsNT6 && this._version.Minor == 3;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x000088F5 File Offset: 0x00006AF5
		public bool IsVista
		{
			get
			{
				return this.IsNT60 && this.Product == OSPlatform.ProductType.WorkStation;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0000890A File Offset: 0x00006B0A
		public bool IsWin2008Server
		{
			get
			{
				return this.IsWin2008ServerR1 || this.IsWin2008ServerR2;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x0000891C File Offset: 0x00006B1C
		public bool IsWin2008ServerR1
		{
			get
			{
				return this.IsNT60 && this.Product == OSPlatform.ProductType.Server;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x00008931 File Offset: 0x00006B31
		public bool IsWin2008ServerR2
		{
			get
			{
				return this.IsNT61 && this.Product == OSPlatform.ProductType.Server;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x00008946 File Offset: 0x00006B46
		public bool IsWin2012Server
		{
			get
			{
				return this.IsWin2012ServerR1 || this.IsWin2012ServerR2;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x00008958 File Offset: 0x00006B58
		public bool IsWin2012ServerR1
		{
			get
			{
				return this.IsNT62 && this.Product == OSPlatform.ProductType.Server;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x0000896D File Offset: 0x00006B6D
		public bool IsWin2012ServerR2
		{
			get
			{
				return this.IsNT63 && this.Product == OSPlatform.ProductType.Server;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x00008982 File Offset: 0x00006B82
		public bool IsWindows7
		{
			get
			{
				return this.IsNT61 && this.Product == OSPlatform.ProductType.WorkStation;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x00008997 File Offset: 0x00006B97
		public bool IsWindows8
		{
			get
			{
				return this.IsNT62 && this.Product == OSPlatform.ProductType.WorkStation;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x000089AC File Offset: 0x00006BAC
		public bool IsWindows81
		{
			get
			{
				return this.IsNT63 && this.Product == OSPlatform.ProductType.WorkStation;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x000089C1 File Offset: 0x00006BC1
		public bool IsWindows10
		{
			get
			{
				return this._platform == PlatformID.Win32NT && this._version.Major == 10 && this.Product == OSPlatform.ProductType.WorkStation;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x000089E6 File Offset: 0x00006BE6
		public bool IsWindowsServer10
		{
			get
			{
				return this._platform == PlatformID.Win32NT && this._version.Major == 10 && this.Product == OSPlatform.ProductType.Server;
			}
		}

		// Token: 0x0400008B RID: 139
		private readonly PlatformID _platform;

		// Token: 0x0400008C RID: 140
		private readonly Version _version;

		// Token: 0x0400008D RID: 141
		private readonly OSPlatform.ProductType _product;

		// Token: 0x0400008E RID: 142
		private static readonly Lazy<OSPlatform> currentPlatform = new Lazy<OSPlatform>(delegate
		{
			OperatingSystem operatingSystem = Environment.OSVersion;
			OSPlatform osplatform;
			if (operatingSystem.Platform == PlatformID.Win32NT && operatingSystem.Version.Major >= 5)
			{
				OSPlatform.OSVERSIONINFOEX osversioninfoex = default(OSPlatform.OSVERSIONINFOEX);
				osversioninfoex.dwOSVersionInfoSize = (uint)Marshal.SizeOf(osversioninfoex);
				OSPlatform.GetVersionEx(ref osversioninfoex);
				if (operatingSystem.Version.Major == 6 && operatingSystem.Version.Minor >= 2)
				{
					operatingSystem = new OperatingSystem(operatingSystem.Platform, OSPlatform.GetWindows81PlusVersion(operatingSystem.Version));
				}
				osplatform = new OSPlatform(operatingSystem.Platform, operatingSystem.Version, (OSPlatform.ProductType)osversioninfoex.ProductType);
			}
			else if (OSPlatform.CheckIfIsMacOSX(operatingSystem.Platform))
			{
				osplatform = new OSPlatform(PlatformID.MacOSX, operatingSystem.Version);
			}
			else
			{
				osplatform = new OSPlatform(operatingSystem.Platform, operatingSystem.Version);
			}
			return osplatform;
		});

		// Token: 0x0400008F RID: 143
		public static readonly PlatformID UnixPlatformID_Microsoft = PlatformID.Unix;

		// Token: 0x04000090 RID: 144
		public static readonly PlatformID UnixPlatformID_Mono = (PlatformID)128;

		// Token: 0x04000091 RID: 145
		public static readonly PlatformID XBoxPlatformID = PlatformID.Xbox;

		// Token: 0x04000092 RID: 146
		public static readonly PlatformID MacOSXPlatformID = PlatformID.MacOSX;

		// Token: 0x02000174 RID: 372
		public enum ProductType
		{
			// Token: 0x0400025F RID: 607
			Unknown,
			// Token: 0x04000260 RID: 608
			WorkStation,
			// Token: 0x04000261 RID: 609
			DomainController,
			// Token: 0x04000262 RID: 610
			Server
		}

		// Token: 0x02000175 RID: 373
		private struct OSVERSIONINFOEX
		{
			// Token: 0x04000263 RID: 611
			public uint dwOSVersionInfoSize;

			// Token: 0x04000264 RID: 612
			public readonly uint dwMajorVersion;

			// Token: 0x04000265 RID: 613
			public readonly uint dwMinorVersion;

			// Token: 0x04000266 RID: 614
			public readonly uint dwBuildNumber;

			// Token: 0x04000267 RID: 615
			public readonly uint dwPlatformId;

			// Token: 0x04000268 RID: 616
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			public readonly string szCSDVersion;

			// Token: 0x04000269 RID: 617
			public readonly short wServicePackMajor;

			// Token: 0x0400026A RID: 618
			public readonly short wServicePackMinor;

			// Token: 0x0400026B RID: 619
			public readonly short wSuiteMask;

			// Token: 0x0400026C RID: 620
			public readonly byte ProductType;

			// Token: 0x0400026D RID: 621
			public readonly byte Reserved;
		}
	}
}
