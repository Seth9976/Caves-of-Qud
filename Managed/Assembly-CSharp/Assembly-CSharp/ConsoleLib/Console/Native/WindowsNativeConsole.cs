using System;
using System.Runtime.InteropServices;

namespace ConsoleLib.Console.Native
{
	// Token: 0x02001551 RID: 5457
	public static class WindowsNativeConsole
	{
		// Token: 0x0600D4CC RID: 54476
		[DllImport("Kernel32.dll")]
		private static extern bool AllocConsole();

		// Token: 0x0600D4CD RID: 54477
		[DllImport("msvcrt.dll")]
		public static extern int system(string cmd);

		// Token: 0x0600D4CE RID: 54478 RVA: 0x004F16DA File Offset: 0x004EF8DA
		public static void Open()
		{
			WindowsNativeConsole.AllocConsole();
		}

		// Token: 0x0600D4CF RID: 54479 RVA: 0x004F16E2 File Offset: 0x004EF8E2
		public static void ClearAllocConsole()
		{
			WindowsNativeConsole.system("CLS");
		}
	}
}
