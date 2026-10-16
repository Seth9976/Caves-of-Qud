using System;
using System.Runtime.InteropServices;

namespace NAudio.Midi
{
	// Token: 0x020000D8 RID: 216
	public class MidiIn : IDisposable
	{
		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060004C2 RID: 1218 RVA: 0x0001021C File Offset: 0x0000E41C
		// (remove) Token: 0x060004C3 RID: 1219 RVA: 0x00010254 File Offset: 0x0000E454
		public event EventHandler<MidiInMessageEventArgs> MessageReceived;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060004C4 RID: 1220 RVA: 0x0001028C File Offset: 0x0000E48C
		// (remove) Token: 0x060004C5 RID: 1221 RVA: 0x000102C4 File Offset: 0x0000E4C4
		public event EventHandler<MidiInMessageEventArgs> ErrorReceived;

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x000102F9 File Offset: 0x0000E4F9
		public static int NumberOfDevices
		{
			get
			{
				return MidiInterop.midiInGetNumDevs();
			}
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00010300 File Offset: 0x0000E500
		public MidiIn(int deviceNo)
		{
			this.callback = new MidiInterop.MidiInCallback(this.Callback);
			MmException.Try(MidiInterop.midiInOpen(out this.hMidiIn, (IntPtr)deviceNo, this.callback, IntPtr.Zero, 196608), "midiInOpen");
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0001035B File Offset: 0x0000E55B
		public void Close()
		{
			this.Dispose();
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00010363 File Offset: 0x0000E563
		public void Dispose()
		{
			GC.KeepAlive(this.callback);
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0001037D File Offset: 0x0000E57D
		public void Start()
		{
			MmException.Try(MidiInterop.midiInStart(this.hMidiIn), "midiInStart");
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00010394 File Offset: 0x0000E594
		public void Stop()
		{
			MmException.Try(MidiInterop.midiInStop(this.hMidiIn), "midiInStop");
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x000103AB File Offset: 0x0000E5AB
		public void Reset()
		{
			MmException.Try(MidiInterop.midiInReset(this.hMidiIn), "midiInReset");
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x000103C4 File Offset: 0x0000E5C4
		private void Callback(IntPtr midiInHandle, MidiInterop.MidiInMessage message, IntPtr userData, IntPtr messageParameter1, IntPtr messageParameter2)
		{
			switch (message)
			{
			case MidiInterop.MidiInMessage.Open:
			case MidiInterop.MidiInMessage.Close:
			case MidiInterop.MidiInMessage.LongData:
			case MidiInterop.MidiInMessage.LongError:
			case (MidiInterop.MidiInMessage)967:
			case (MidiInterop.MidiInMessage)968:
			case (MidiInterop.MidiInMessage)969:
			case (MidiInterop.MidiInMessage)970:
			case (MidiInterop.MidiInMessage)971:
			case MidiInterop.MidiInMessage.MoreData:
				break;
			case MidiInterop.MidiInMessage.Data:
				if (this.MessageReceived != null)
				{
					this.MessageReceived.Invoke(this, new MidiInMessageEventArgs(messageParameter1.ToInt32(), messageParameter2.ToInt32()));
					return;
				}
				break;
			case MidiInterop.MidiInMessage.Error:
				if (this.ErrorReceived != null)
				{
					this.ErrorReceived.Invoke(this, new MidiInMessageEventArgs(messageParameter1.ToInt32(), messageParameter2.ToInt32()));
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00010460 File Offset: 0x0000E660
		public static MidiInCapabilities DeviceInfo(int midiInDeviceNumber)
		{
			MidiInCapabilities midiInCapabilities = default(MidiInCapabilities);
			int num = Marshal.SizeOf(midiInCapabilities);
			MmException.Try(MidiInterop.midiInGetDevCaps((IntPtr)midiInDeviceNumber, out midiInCapabilities, num), "midiInGetDevCaps");
			return midiInCapabilities;
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0001049A File Offset: 0x0000E69A
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				MidiInterop.midiInClose(this.hMidiIn);
			}
			this.disposed = true;
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x000104B8 File Offset: 0x0000E6B8
		~MidiIn()
		{
			this.Dispose(false);
		}

		// Token: 0x04000584 RID: 1412
		private IntPtr hMidiIn = IntPtr.Zero;

		// Token: 0x04000585 RID: 1413
		private bool disposed;

		// Token: 0x04000586 RID: 1414
		private MidiInterop.MidiInCallback callback;
	}
}
