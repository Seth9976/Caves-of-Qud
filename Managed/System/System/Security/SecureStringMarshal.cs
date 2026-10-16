using System;
using System.Runtime.InteropServices;

namespace System.Security
{
	// Token: 0x02000290 RID: 656
	public static class SecureStringMarshal
	{
		// Token: 0x060014B2 RID: 5298 RVA: 0x00054392 File Offset: 0x00052592
		public static IntPtr SecureStringToCoTaskMemAnsi(SecureString s)
		{
			return Marshal.SecureStringToCoTaskMemAnsi(s);
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x0005439A File Offset: 0x0005259A
		public static IntPtr SecureStringToGlobalAllocAnsi(SecureString s)
		{
			return Marshal.SecureStringToGlobalAllocAnsi(s);
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x000543A2 File Offset: 0x000525A2
		public static IntPtr SecureStringToCoTaskMemUnicode(SecureString s)
		{
			return Marshal.SecureStringToCoTaskMemUnicode(s);
		}

		// Token: 0x060014B5 RID: 5301 RVA: 0x000543AA File Offset: 0x000525AA
		public static IntPtr SecureStringToGlobalAllocUnicode(SecureString s)
		{
			return Marshal.SecureStringToGlobalAllocUnicode(s);
		}
	}
}
