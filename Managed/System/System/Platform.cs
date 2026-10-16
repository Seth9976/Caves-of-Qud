using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x02000177 RID: 375
	internal static class Platform
	{
		// Token: 0x060009E4 RID: 2532
		[DllImport("libc")]
		private static extern int uname(IntPtr buf);

		// Token: 0x060009E5 RID: 2533 RVA: 0x0002B980 File Offset: 0x00029B80
		private static void CheckOS()
		{
			if (Environment.OSVersion.Platform != PlatformID.Unix)
			{
				Platform.checkedOS = true;
				return;
			}
			IntPtr intPtr = Marshal.AllocHGlobal(8192);
			if (Platform.uname(intPtr) == 0)
			{
				string text = Marshal.PtrToStringAnsi(intPtr);
				if (!(text == "Darwin"))
				{
					if (!(text == "FreeBSD"))
					{
						if (!(text == "AIX"))
						{
							if (!(text == "OS400"))
							{
								if (text == "OpenBSD")
								{
									Platform.isOpenBSD = true;
								}
							}
							else
							{
								Platform.isIBMi = true;
							}
						}
						else
						{
							Platform.isAix = true;
						}
					}
					else
					{
						Platform.isFreeBSD = true;
					}
				}
				else
				{
					Platform.isMacOS = true;
				}
			}
			Marshal.FreeHGlobal(intPtr);
			Platform.checkedOS = true;
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x0002BA30 File Offset: 0x00029C30
		public static bool IsMacOS
		{
			get
			{
				if (!Platform.checkedOS)
				{
					try
					{
						Platform.CheckOS();
					}
					catch (DllNotFoundException)
					{
						Platform.isMacOS = false;
					}
				}
				return Platform.isMacOS;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060009E7 RID: 2535 RVA: 0x0002BA6C File Offset: 0x00029C6C
		public static bool IsFreeBSD
		{
			get
			{
				if (!Platform.checkedOS)
				{
					Platform.CheckOS();
				}
				return Platform.isFreeBSD;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x0002BA7F File Offset: 0x00029C7F
		public static bool IsOpenBSD
		{
			get
			{
				if (!Platform.checkedOS)
				{
					Platform.CheckOS();
				}
				return Platform.isOpenBSD;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060009E9 RID: 2537 RVA: 0x0002BA92 File Offset: 0x00029C92
		public static bool IsIBMi
		{
			get
			{
				if (!Platform.checkedOS)
				{
					Platform.CheckOS();
				}
				return Platform.isIBMi;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060009EA RID: 2538 RVA: 0x0002BAA5 File Offset: 0x00029CA5
		public static bool IsAix
		{
			get
			{
				if (!Platform.checkedOS)
				{
					Platform.CheckOS();
				}
				return Platform.isAix;
			}
		}

		// Token: 0x040006D2 RID: 1746
		private static bool checkedOS;

		// Token: 0x040006D3 RID: 1747
		private static bool isMacOS;

		// Token: 0x040006D4 RID: 1748
		private static bool isAix;

		// Token: 0x040006D5 RID: 1749
		private static bool isIBMi;

		// Token: 0x040006D6 RID: 1750
		private static bool isFreeBSD;

		// Token: 0x040006D7 RID: 1751
		private static bool isOpenBSD;
	}
}
