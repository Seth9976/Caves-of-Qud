using System;
using System.Runtime.InteropServices;

namespace NAudio.Midi
{
	// Token: 0x020000DA RID: 218
	internal class MidiInterop
	{
		// Token: 0x060004D4 RID: 1236
		[DllImport("winmm.dll")]
		public static extern MmResult midiConnect(IntPtr hMidiIn, IntPtr hMidiOut, IntPtr pReserved);

		// Token: 0x060004D5 RID: 1237
		[DllImport("winmm.dll")]
		public static extern MmResult midiDisconnect(IntPtr hMidiIn, IntPtr hMidiOut, IntPtr pReserved);

		// Token: 0x060004D6 RID: 1238
		[DllImport("winmm.dll")]
		public static extern MmResult midiInAddBuffer(IntPtr hMidiIn, [MarshalAs(27)] ref MidiInterop.MIDIHDR lpMidiInHdr, int uSize);

		// Token: 0x060004D7 RID: 1239
		[DllImport("winmm.dll")]
		public static extern MmResult midiInClose(IntPtr hMidiIn);

		// Token: 0x060004D8 RID: 1240
		[DllImport("winmm.dll", CharSet = 4)]
		public static extern MmResult midiInGetDevCaps(IntPtr deviceId, out MidiInCapabilities capabilities, int size);

		// Token: 0x060004D9 RID: 1241
		[DllImport("winmm.dll")]
		public static extern MmResult midiInGetErrorText(int err, string lpText, int uSize);

		// Token: 0x060004DA RID: 1242
		[DllImport("winmm.dll")]
		public static extern MmResult midiInGetID(IntPtr hMidiIn, out int lpuDeviceId);

		// Token: 0x060004DB RID: 1243
		[DllImport("winmm.dll")]
		public static extern int midiInGetNumDevs();

		// Token: 0x060004DC RID: 1244
		[DllImport("winmm.dll")]
		public static extern MmResult midiInMessage(IntPtr hMidiIn, int msg, IntPtr dw1, IntPtr dw2);

		// Token: 0x060004DD RID: 1245
		[DllImport("winmm.dll")]
		public static extern MmResult midiInOpen(out IntPtr hMidiIn, IntPtr uDeviceID, MidiInterop.MidiInCallback callback, IntPtr dwInstance, int dwFlags);

		// Token: 0x060004DE RID: 1246
		[DllImport("winmm.dll", EntryPoint = "midiInOpen")]
		public static extern MmResult midiInOpenWindow(out IntPtr hMidiIn, IntPtr uDeviceID, IntPtr callbackWindowHandle, IntPtr dwInstance, int dwFlags);

		// Token: 0x060004DF RID: 1247
		[DllImport("winmm.dll")]
		public static extern MmResult midiInPrepareHeader(IntPtr hMidiIn, [MarshalAs(27)] ref MidiInterop.MIDIHDR lpMidiInHdr, int uSize);

		// Token: 0x060004E0 RID: 1248
		[DllImport("winmm.dll")]
		public static extern MmResult midiInReset(IntPtr hMidiIn);

		// Token: 0x060004E1 RID: 1249
		[DllImport("winmm.dll")]
		public static extern MmResult midiInStart(IntPtr hMidiIn);

		// Token: 0x060004E2 RID: 1250
		[DllImport("winmm.dll")]
		public static extern MmResult midiInStop(IntPtr hMidiIn);

		// Token: 0x060004E3 RID: 1251
		[DllImport("winmm.dll")]
		public static extern MmResult midiInUnprepareHeader(IntPtr hMidiIn, [MarshalAs(27)] ref MidiInterop.MIDIHDR lpMidiInHdr, int uSize);

		// Token: 0x060004E4 RID: 1252
		[DllImport("winmm.dll")]
		public static extern MmResult midiOutCacheDrumPatches(IntPtr hMidiOut, int uPatch, IntPtr lpKeyArray, int uFlags);

		// Token: 0x060004E5 RID: 1253
		[DllImport("winmm.dll")]
		public static extern MmResult midiOutCachePatches(IntPtr hMidiOut, int uBank, IntPtr lpPatchArray, int uFlags);

		// Token: 0x060004E6 RID: 1254
		[DllImport("winmm.dll")]
		public static extern MmResult midiOutClose(IntPtr hMidiOut);

		// Token: 0x060004E7 RID: 1255
		[DllImport("winmm.dll", CharSet = 4)]
		public static extern MmResult midiOutGetDevCaps(IntPtr deviceNumber, out MidiOutCapabilities caps, int uSize);

		// Token: 0x060004E8 RID: 1256
		[DllImport("winmm.dll")]
		public static extern MmResult midiOutGetErrorText(IntPtr err, string lpText, int uSize);

		// Token: 0x060004E9 RID: 1257
		[DllImport("winmm.dll")]
		public static extern MmResult midiOutGetID(IntPtr hMidiOut, out int lpuDeviceID);

		// Token: 0x060004EA RID: 1258
		[DllImport("winmm.dll")]
		public static extern int midiOutGetNumDevs();

		// Token: 0x060004EB RID: 1259
		[DllImport("winmm.dll")]
		public static extern MmResult midiOutGetVolume(IntPtr uDeviceID, ref int lpdwVolume);

		// Token: 0x060004EC RID: 1260
		[DllImport("winmm.dll")]
		public static extern MmResult midiOutLongMsg(IntPtr hMidiOut, [MarshalAs(27)] ref MidiInterop.MIDIHDR lpMidiOutHdr, int uSize);

		// Token: 0x060004ED RID: 1261
		[DllImport("winmm.dll")]
		public static extern MmResult midiOutMessage(IntPtr hMidiOut, int msg, IntPtr dw1, IntPtr dw2);

		// Token: 0x060004EE RID: 1262
		[DllImport("winmm.dll")]
		public static extern MmResult midiOutOpen(out IntPtr lphMidiOut, IntPtr uDeviceID, MidiInterop.MidiOutCallback dwCallback, IntPtr dwInstance, int dwFlags);

		// Token: 0x060004EF RID: 1263
		[DllImport("winmm.dll")]
		public static extern MmResult midiOutPrepareHeader(IntPtr hMidiOut, [MarshalAs(27)] ref MidiInterop.MIDIHDR lpMidiOutHdr, int uSize);

		// Token: 0x060004F0 RID: 1264
		[DllImport("winmm.dll")]
		public static extern MmResult midiOutReset(IntPtr hMidiOut);

		// Token: 0x060004F1 RID: 1265
		[DllImport("winmm.dll")]
		public static extern MmResult midiOutSetVolume(IntPtr hMidiOut, int dwVolume);

		// Token: 0x060004F2 RID: 1266
		[DllImport("winmm.dll")]
		public static extern MmResult midiOutShortMsg(IntPtr hMidiOut, int dwMsg);

		// Token: 0x060004F3 RID: 1267
		[DllImport("winmm.dll")]
		public static extern MmResult midiOutUnprepareHeader(IntPtr hMidiOut, [MarshalAs(27)] ref MidiInterop.MIDIHDR lpMidiOutHdr, int uSize);

		// Token: 0x060004F4 RID: 1268
		[DllImport("winmm.dll")]
		public static extern MmResult midiStreamClose(IntPtr hMidiStream);

		// Token: 0x060004F5 RID: 1269
		[DllImport("winmm.dll")]
		public static extern MmResult midiStreamOpen(out IntPtr hMidiStream, IntPtr puDeviceID, int cMidi, IntPtr dwCallback, IntPtr dwInstance, int fdwOpen);

		// Token: 0x060004F6 RID: 1270
		[DllImport("winmm.dll")]
		public static extern MmResult midiStreamOut(IntPtr hMidiStream, [MarshalAs(27)] ref MidiInterop.MIDIHDR pmh, int cbmh);

		// Token: 0x060004F7 RID: 1271
		[DllImport("winmm.dll")]
		public static extern MmResult midiStreamPause(IntPtr hMidiStream);

		// Token: 0x060004F8 RID: 1272
		[DllImport("winmm.dll")]
		public static extern MmResult midiStreamPosition(IntPtr hMidiStream, [MarshalAs(27)] ref MidiInterop.MMTIME lpmmt, int cbmmt);

		// Token: 0x060004F9 RID: 1273
		[DllImport("winmm.dll")]
		public static extern MmResult midiStreamProperty(IntPtr hMidiStream, IntPtr lppropdata, int dwProperty);

		// Token: 0x060004FA RID: 1274
		[DllImport("winmm.dll")]
		public static extern MmResult midiStreamRestart(IntPtr hMidiStream);

		// Token: 0x060004FB RID: 1275
		[DllImport("winmm.dll")]
		public static extern MmResult midiStreamStop(IntPtr hMidiStream);

		// Token: 0x0400058F RID: 1423
		public const int CALLBACK_FUNCTION = 196608;

		// Token: 0x04000590 RID: 1424
		public const int CALLBACK_NULL = 0;

		// Token: 0x020000DB RID: 219
		public enum MidiInMessage
		{
			// Token: 0x04000592 RID: 1426
			Open = 961,
			// Token: 0x04000593 RID: 1427
			Close,
			// Token: 0x04000594 RID: 1428
			Data,
			// Token: 0x04000595 RID: 1429
			LongData,
			// Token: 0x04000596 RID: 1430
			Error,
			// Token: 0x04000597 RID: 1431
			LongError,
			// Token: 0x04000598 RID: 1432
			MoreData = 972
		}

		// Token: 0x020000DC RID: 220
		public enum MidiOutMessage
		{
			// Token: 0x0400059A RID: 1434
			Open = 967,
			// Token: 0x0400059B RID: 1435
			Close,
			// Token: 0x0400059C RID: 1436
			Done
		}

		// Token: 0x020000DD RID: 221
		// (Invoke) Token: 0x060004FE RID: 1278
		public delegate void MidiInCallback(IntPtr midiInHandle, MidiInterop.MidiInMessage message, IntPtr userData, IntPtr messageParameter1, IntPtr messageParameter2);

		// Token: 0x020000DE RID: 222
		// (Invoke) Token: 0x06000502 RID: 1282
		public delegate void MidiOutCallback(IntPtr midiInHandle, MidiInterop.MidiOutMessage message, IntPtr userData, IntPtr messageParameter1, IntPtr messageParameter2);

		// Token: 0x020000DF RID: 223
		public struct MMTIME
		{
			// Token: 0x0400059D RID: 1437
			public int wType;

			// Token: 0x0400059E RID: 1438
			public int u;
		}

		// Token: 0x020000E0 RID: 224
		public struct MIDIEVENT
		{
			// Token: 0x0400059F RID: 1439
			public int dwDeltaTime;

			// Token: 0x040005A0 RID: 1440
			public int dwStreamID;

			// Token: 0x040005A1 RID: 1441
			public int dwEvent;

			// Token: 0x040005A2 RID: 1442
			[MarshalAs(30, SizeConst = 1)]
			public int dwParms;
		}

		// Token: 0x020000E1 RID: 225
		public struct MIDIHDR
		{
			// Token: 0x040005A3 RID: 1443
			public IntPtr lpData;

			// Token: 0x040005A4 RID: 1444
			public int dwBufferLength;

			// Token: 0x040005A5 RID: 1445
			public int dwBytesRecorded;

			// Token: 0x040005A6 RID: 1446
			public IntPtr dwUser;

			// Token: 0x040005A7 RID: 1447
			public int dwFlags;

			// Token: 0x040005A8 RID: 1448
			public IntPtr lpNext;

			// Token: 0x040005A9 RID: 1449
			public IntPtr reserved;

			// Token: 0x040005AA RID: 1450
			public int dwOffset;

			// Token: 0x040005AB RID: 1451
			[MarshalAs(30, SizeConst = 8)]
			public IntPtr[] dwReserved;
		}

		// Token: 0x020000E2 RID: 226
		public struct MIDIPROPTEMPO
		{
			// Token: 0x040005AC RID: 1452
			public int cbStruct;

			// Token: 0x040005AD RID: 1453
			public int dwTempo;
		}
	}
}
