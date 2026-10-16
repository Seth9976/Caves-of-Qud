using System;
using System.Runtime.InteropServices;

namespace Mono.Posix
{
	// Token: 0x02000094 RID: 148
	[Obsolete("Use Mono.Unix.Catalog")]
	public class Catalog
	{
		// Token: 0x06000704 RID: 1796
		[DllImport("intl")]
		private static extern IntPtr bindtextdomain(IntPtr domainname, IntPtr dirname);

		// Token: 0x06000705 RID: 1797
		[DllImport("intl")]
		private static extern IntPtr bind_textdomain_codeset(IntPtr domainname, IntPtr codeset);

		// Token: 0x06000706 RID: 1798
		[DllImport("intl")]
		private static extern IntPtr textdomain(IntPtr domainname);

		// Token: 0x06000707 RID: 1799 RVA: 0x000100D8 File Offset: 0x0000E2D8
		public static void Init(string package, string localedir)
		{
			IntPtr intPtr = Marshal.StringToHGlobalAuto(package);
			IntPtr intPtr2 = Marshal.StringToHGlobalAuto(localedir);
			IntPtr intPtr3 = Marshal.StringToHGlobalAuto("UTF-8");
			Catalog.bindtextdomain(intPtr, intPtr2);
			Catalog.bind_textdomain_codeset(intPtr, intPtr3);
			Catalog.textdomain(intPtr);
			Marshal.FreeHGlobal(intPtr);
			Marshal.FreeHGlobal(intPtr2);
			Marshal.FreeHGlobal(intPtr3);
		}

		// Token: 0x06000708 RID: 1800
		[DllImport("intl")]
		private static extern IntPtr gettext(IntPtr instring);

		// Token: 0x06000709 RID: 1801 RVA: 0x00010128 File Offset: 0x0000E328
		public static string GetString(string s)
		{
			IntPtr intPtr = Marshal.StringToHGlobalAuto(s);
			string text = Marshal.PtrToStringAuto(Catalog.gettext(intPtr));
			Marshal.FreeHGlobal(intPtr);
			return text;
		}

		// Token: 0x0600070A RID: 1802
		[DllImport("intl")]
		private static extern IntPtr ngettext(IntPtr singular, IntPtr plural, int n);

		// Token: 0x0600070B RID: 1803 RVA: 0x00010150 File Offset: 0x0000E350
		public static string GetPluralString(string s, string p, int n)
		{
			IntPtr intPtr = Marshal.StringToHGlobalAuto(s);
			IntPtr intPtr2 = Marshal.StringToHGlobalAuto(p);
			string text = Marshal.PtrToStringAnsi(Catalog.ngettext(intPtr, intPtr2, n));
			Marshal.FreeHGlobal(intPtr);
			Marshal.FreeHGlobal(intPtr2);
			return text;
		}
	}
}
