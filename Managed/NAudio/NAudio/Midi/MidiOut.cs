using System;
using System.Runtime.InteropServices;

namespace NAudio.Midi
{
	// Token: 0x020000E4 RID: 228
	public class MidiOut : IDisposable
	{
		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x0001057F File Offset: 0x0000E77F
		public static int NumberOfDevices
		{
			get
			{
				return MidiInterop.midiOutGetNumDevs();
			}
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00010588 File Offset: 0x0000E788
		public static MidiOutCapabilities DeviceInfo(int midiOutDeviceNumber)
		{
			MidiOutCapabilities midiOutCapabilities = default(MidiOutCapabilities);
			int num = Marshal.SizeOf(midiOutCapabilities);
			MmException.Try(MidiInterop.midiOutGetDevCaps((IntPtr)midiOutDeviceNumber, out midiOutCapabilities, num), "midiOutGetDevCaps");
			return midiOutCapabilities;
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x000105C4 File Offset: 0x0000E7C4
		public MidiOut(int deviceNo)
		{
			this.callback = new MidiInterop.MidiOutCallback(this.Callback);
			MmException.Try(MidiInterop.midiOutOpen(out this.hMidiOut, (IntPtr)deviceNo, this.callback, IntPtr.Zero, 196608), "midiOutOpen");
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0001061F File Offset: 0x0000E81F
		public void Close()
		{
			this.Dispose();
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00010627 File Offset: 0x0000E827
		public void Dispose()
		{
			GC.KeepAlive(this.callback);
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000511 RID: 1297 RVA: 0x00010644 File Offset: 0x0000E844
		// (set) Token: 0x06000512 RID: 1298 RVA: 0x0001066B File Offset: 0x0000E86B
		public int Volume
		{
			get
			{
				int num = 0;
				MmException.Try(MidiInterop.midiOutGetVolume(this.hMidiOut, ref num), "midiOutGetVolume");
				return num;
			}
			set
			{
				MmException.Try(MidiInterop.midiOutSetVolume(this.hMidiOut, value), "midiOutSetVolume");
			}
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00010683 File Offset: 0x0000E883
		public void Reset()
		{
			MmException.Try(MidiInterop.midiOutReset(this.hMidiOut), "midiOutReset");
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0001069A File Offset: 0x0000E89A
		public void SendDriverMessage(int message, int param1, int param2)
		{
			MmException.Try(MidiInterop.midiOutMessage(this.hMidiOut, message, (IntPtr)param1, (IntPtr)param2), "midiOutMessage");
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x000106BE File Offset: 0x0000E8BE
		public void Send(int message)
		{
			MmException.Try(MidiInterop.midiOutShortMsg(this.hMidiOut, message), "midiOutShortMsg");
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x000106D6 File Offset: 0x0000E8D6
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				MidiInterop.midiOutClose(this.hMidiOut);
			}
			this.disposed = true;
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x000106F3 File Offset: 0x0000E8F3
		private void Callback(IntPtr midiInHandle, MidiInterop.MidiOutMessage message, IntPtr userData, IntPtr messageParameter1, IntPtr messageParameter2)
		{
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x000106F8 File Offset: 0x0000E8F8
		public void SendBuffer(byte[] byteBuffer)
		{
			MidiInterop.MIDIHDR midihdr = default(MidiInterop.MIDIHDR);
			midihdr.lpData = Marshal.AllocHGlobal(byteBuffer.Length);
			Marshal.Copy(byteBuffer, 0, midihdr.lpData, byteBuffer.Length);
			midihdr.dwBufferLength = byteBuffer.Length;
			midihdr.dwBytesRecorded = byteBuffer.Length;
			int num = Marshal.SizeOf(midihdr);
			MidiInterop.midiOutPrepareHeader(this.hMidiOut, ref midihdr, num);
			MmResult mmResult = MidiInterop.midiOutLongMsg(this.hMidiOut, ref midihdr, num);
			if (mmResult != MmResult.NoError)
			{
				MidiInterop.midiOutUnprepareHeader(this.hMidiOut, ref midihdr, num);
			}
			Marshal.FreeHGlobal(midihdr.lpData);
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0001078C File Offset: 0x0000E98C
		~MidiOut()
		{
			this.Dispose(false);
		}

		// Token: 0x040005AF RID: 1455
		private IntPtr hMidiOut = IntPtr.Zero;

		// Token: 0x040005B0 RID: 1456
		private bool disposed;

		// Token: 0x040005B1 RID: 1457
		private MidiInterop.MidiOutCallback callback;
	}
}
