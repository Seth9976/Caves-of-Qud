using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave
{
	// Token: 0x0200017E RID: 382
	internal class WaveInterop
	{
		// Token: 0x060007BA RID: 1978
		[DllImport("winmm.dll")]
		public static extern int mmioStringToFOURCC([MarshalAs(20)] string s, int flags);

		// Token: 0x060007BB RID: 1979
		[DllImport("winmm.dll")]
		public static extern int waveOutGetNumDevs();

		// Token: 0x060007BC RID: 1980
		[DllImport("winmm.dll")]
		public static extern MmResult waveOutPrepareHeader(IntPtr hWaveOut, WaveHeader lpWaveOutHdr, int uSize);

		// Token: 0x060007BD RID: 1981
		[DllImport("winmm.dll")]
		public static extern MmResult waveOutUnprepareHeader(IntPtr hWaveOut, WaveHeader lpWaveOutHdr, int uSize);

		// Token: 0x060007BE RID: 1982
		[DllImport("winmm.dll")]
		public static extern MmResult waveOutWrite(IntPtr hWaveOut, WaveHeader lpWaveOutHdr, int uSize);

		// Token: 0x060007BF RID: 1983
		[DllImport("winmm.dll")]
		public static extern MmResult waveOutOpen(out IntPtr hWaveOut, IntPtr uDeviceID, WaveFormat lpFormat, WaveInterop.WaveCallback dwCallback, IntPtr dwInstance, WaveInterop.WaveInOutOpenFlags dwFlags);

		// Token: 0x060007C0 RID: 1984
		[DllImport("winmm.dll", EntryPoint = "waveOutOpen")]
		public static extern MmResult waveOutOpenWindow(out IntPtr hWaveOut, IntPtr uDeviceID, WaveFormat lpFormat, IntPtr callbackWindowHandle, IntPtr dwInstance, WaveInterop.WaveInOutOpenFlags dwFlags);

		// Token: 0x060007C1 RID: 1985
		[DllImport("winmm.dll")]
		public static extern MmResult waveOutReset(IntPtr hWaveOut);

		// Token: 0x060007C2 RID: 1986
		[DllImport("winmm.dll")]
		public static extern MmResult waveOutClose(IntPtr hWaveOut);

		// Token: 0x060007C3 RID: 1987
		[DllImport("winmm.dll")]
		public static extern MmResult waveOutPause(IntPtr hWaveOut);

		// Token: 0x060007C4 RID: 1988
		[DllImport("winmm.dll")]
		public static extern MmResult waveOutRestart(IntPtr hWaveOut);

		// Token: 0x060007C5 RID: 1989
		[DllImport("winmm.dll")]
		public static extern MmResult waveOutGetPosition(IntPtr hWaveOut, out MmTime mmTime, int uSize);

		// Token: 0x060007C6 RID: 1990
		[DllImport("winmm.dll")]
		public static extern MmResult waveOutSetVolume(IntPtr hWaveOut, int dwVolume);

		// Token: 0x060007C7 RID: 1991
		[DllImport("winmm.dll")]
		public static extern MmResult waveOutGetVolume(IntPtr hWaveOut, out int dwVolume);

		// Token: 0x060007C8 RID: 1992
		[DllImport("winmm.dll", CharSet = 4)]
		public static extern MmResult waveOutGetDevCaps(IntPtr deviceID, out WaveOutCapabilities waveOutCaps, int waveOutCapsSize);

		// Token: 0x060007C9 RID: 1993
		[DllImport("winmm.dll")]
		public static extern int waveInGetNumDevs();

		// Token: 0x060007CA RID: 1994
		[DllImport("winmm.dll", CharSet = 4)]
		public static extern MmResult waveInGetDevCaps(IntPtr deviceID, out WaveInCapabilities waveInCaps, int waveInCapsSize);

		// Token: 0x060007CB RID: 1995
		[DllImport("winmm.dll")]
		public static extern MmResult waveInAddBuffer(IntPtr hWaveIn, WaveHeader pwh, int cbwh);

		// Token: 0x060007CC RID: 1996
		[DllImport("winmm.dll")]
		public static extern MmResult waveInClose(IntPtr hWaveIn);

		// Token: 0x060007CD RID: 1997
		[DllImport("winmm.dll")]
		public static extern MmResult waveInOpen(out IntPtr hWaveIn, IntPtr uDeviceID, WaveFormat lpFormat, WaveInterop.WaveCallback dwCallback, IntPtr dwInstance, WaveInterop.WaveInOutOpenFlags dwFlags);

		// Token: 0x060007CE RID: 1998
		[DllImport("winmm.dll", EntryPoint = "waveInOpen")]
		public static extern MmResult waveInOpenWindow(out IntPtr hWaveIn, IntPtr uDeviceID, WaveFormat lpFormat, IntPtr callbackWindowHandle, IntPtr dwInstance, WaveInterop.WaveInOutOpenFlags dwFlags);

		// Token: 0x060007CF RID: 1999
		[DllImport("winmm.dll")]
		public static extern MmResult waveInPrepareHeader(IntPtr hWaveIn, WaveHeader lpWaveInHdr, int uSize);

		// Token: 0x060007D0 RID: 2000
		[DllImport("winmm.dll")]
		public static extern MmResult waveInUnprepareHeader(IntPtr hWaveIn, WaveHeader lpWaveInHdr, int uSize);

		// Token: 0x060007D1 RID: 2001
		[DllImport("winmm.dll")]
		public static extern MmResult waveInReset(IntPtr hWaveIn);

		// Token: 0x060007D2 RID: 2002
		[DllImport("winmm.dll")]
		public static extern MmResult waveInStart(IntPtr hWaveIn);

		// Token: 0x060007D3 RID: 2003
		[DllImport("winmm.dll")]
		public static extern MmResult waveInStop(IntPtr hWaveIn);

		// Token: 0x0200017F RID: 383
		[Flags]
		public enum WaveInOutOpenFlags
		{
			// Token: 0x04000926 RID: 2342
			CallbackNull = 0,
			// Token: 0x04000927 RID: 2343
			CallbackFunction = 196608,
			// Token: 0x04000928 RID: 2344
			CallbackEvent = 327680,
			// Token: 0x04000929 RID: 2345
			CallbackWindow = 65536,
			// Token: 0x0400092A RID: 2346
			CallbackThread = 131072
		}

		// Token: 0x02000180 RID: 384
		public enum WaveMessage
		{
			// Token: 0x0400092C RID: 2348
			WaveInOpen = 958,
			// Token: 0x0400092D RID: 2349
			WaveInClose,
			// Token: 0x0400092E RID: 2350
			WaveInData,
			// Token: 0x0400092F RID: 2351
			WaveOutClose = 956,
			// Token: 0x04000930 RID: 2352
			WaveOutDone,
			// Token: 0x04000931 RID: 2353
			WaveOutOpen = 955
		}

		// Token: 0x02000181 RID: 385
		// (Invoke) Token: 0x060007D6 RID: 2006
		public delegate void WaveCallback(IntPtr hWaveOut, WaveInterop.WaveMessage message, IntPtr dwInstance, WaveHeader wavhdr, IntPtr dwReserved);
	}
}
