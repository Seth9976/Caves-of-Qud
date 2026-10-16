using System;
using System.IO;

namespace Roslyn.Utilities
{
	// Token: 0x02000057 RID: 87
	internal static class PlatformInformation
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00006C93 File Offset: 0x00004E93
		public static bool IsWindows
		{
			get
			{
				return Path.DirectorySeparatorChar == '\\';
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x00006C9E File Offset: 0x00004E9E
		public static bool IsUnix
		{
			get
			{
				return Path.DirectorySeparatorChar == '/';
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00006CAC File Offset: 0x00004EAC
		public static bool IsRunningOnMono
		{
			get
			{
				bool flag;
				try
				{
					flag = Type.GetType("Mono.Runtime") != null;
				}
				catch
				{
					flag = false;
				}
				return flag;
			}
		}
	}
}
