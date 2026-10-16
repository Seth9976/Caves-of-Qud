using System;
using System.Runtime.InteropServices;

namespace NAudio.Utils
{
	// Token: 0x0200006C RID: 108
	internal class NativeMethods
	{
		// Token: 0x06000251 RID: 593
		[DllImport("kernel32.dll")]
		public static extern IntPtr LoadLibrary(string dllToLoad);

		// Token: 0x06000252 RID: 594
		[DllImport("kernel32.dll")]
		public static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

		// Token: 0x06000253 RID: 595
		[DllImport("kernel32.dll")]
		public static extern bool FreeLibrary(IntPtr hModule);
	}
}
