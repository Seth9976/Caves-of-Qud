using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// Token: 0x02000007 RID: 7
internal static class Interop
{
	// Token: 0x0200018B RID: 395
	[NullableContext(1)]
	[Nullable(0)]
	internal static class Libraries
	{
		// Token: 0x04000982 RID: 2434
		internal const string Advapi32 = "advapi32.dll";

		// Token: 0x04000983 RID: 2435
		internal const string BCrypt = "BCrypt.dll";

		// Token: 0x04000984 RID: 2436
		internal const string Crypt32 = "crypt32.dll";

		// Token: 0x04000985 RID: 2437
		internal const string CryptUI = "cryptui.dll";

		// Token: 0x04000986 RID: 2438
		internal const string Gdi32 = "gdi32.dll";

		// Token: 0x04000987 RID: 2439
		internal const string HttpApi = "httpapi.dll";

		// Token: 0x04000988 RID: 2440
		internal const string IpHlpApi = "iphlpapi.dll";

		// Token: 0x04000989 RID: 2441
		internal const string Kernel32 = "kernel32.dll";

		// Token: 0x0400098A RID: 2442
		internal const string Mswsock = "mswsock.dll";

		// Token: 0x0400098B RID: 2443
		internal const string NCrypt = "ncrypt.dll";

		// Token: 0x0400098C RID: 2444
		internal const string NtDll = "ntdll.dll";

		// Token: 0x0400098D RID: 2445
		internal const string Odbc32 = "odbc32.dll";

		// Token: 0x0400098E RID: 2446
		internal const string Ole32 = "ole32.dll";

		// Token: 0x0400098F RID: 2447
		internal const string OleAut32 = "oleaut32.dll";

		// Token: 0x04000990 RID: 2448
		internal const string PerfCounter = "perfcounter.dll";

		// Token: 0x04000991 RID: 2449
		internal const string Secur32 = "secur32.dll";

		// Token: 0x04000992 RID: 2450
		internal const string Shell32 = "shell32.dll";

		// Token: 0x04000993 RID: 2451
		internal const string SspiCli = "sspicli.dll";

		// Token: 0x04000994 RID: 2452
		internal const string User32 = "user32.dll";

		// Token: 0x04000995 RID: 2453
		internal const string Version = "version.dll";

		// Token: 0x04000996 RID: 2454
		internal const string WebSocket = "websocket.dll";

		// Token: 0x04000997 RID: 2455
		internal const string WinHttp = "winhttp.dll";

		// Token: 0x04000998 RID: 2456
		internal const string WinMM = "winmm.dll";

		// Token: 0x04000999 RID: 2457
		internal const string Wldap32 = "wldap32.dll";

		// Token: 0x0400099A RID: 2458
		internal const string Ws2_32 = "ws2_32.dll";

		// Token: 0x0400099B RID: 2459
		internal const string Wtsapi32 = "wtsapi32.dll";

		// Token: 0x0400099C RID: 2460
		internal const string CompressionNative = "clrcompression.dll";

		// Token: 0x0400099D RID: 2461
		internal const string MsQuic = "msquic.dll";

		// Token: 0x0400099E RID: 2462
		internal const string HostPolicy = "hostpolicy.dll";
	}

	// Token: 0x0200018C RID: 396
	internal static class Kernel32
	{
		// Token: 0x06000C18 RID: 3096
		[DllImport("kernel32.dll", SetLastError = true)]
		internal unsafe static extern int ReadFile([Nullable(1)] SafeHandle handle, byte* bytes, int numBytesToRead, out int numBytesRead, IntPtr mustBeZero);
	}
}
