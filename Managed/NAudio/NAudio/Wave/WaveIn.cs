using System;
using System.Runtime.InteropServices;
using System.Threading;
using NAudio.Mixer;

namespace NAudio.Wave
{
	// Token: 0x0200017A RID: 378
	public class WaveIn : IWaveIn, IDisposable
	{
		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000792 RID: 1938 RVA: 0x00015FD0 File Offset: 0x000141D0
		// (remove) Token: 0x06000793 RID: 1939 RVA: 0x00016008 File Offset: 0x00014208
		public event EventHandler<WaveInEventArgs> DataAvailable;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000794 RID: 1940 RVA: 0x00016040 File Offset: 0x00014240
		// (remove) Token: 0x06000795 RID: 1941 RVA: 0x00016078 File Offset: 0x00014278
		public event EventHandler<StoppedEventArgs> RecordingStopped;

		// Token: 0x06000796 RID: 1942 RVA: 0x000160AD File Offset: 0x000142AD
		public WaveIn()
			: this(WaveCallbackInfo.NewWindow())
		{
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x000160BA File Offset: 0x000142BA
		public WaveIn(IntPtr windowHandle)
			: this(WaveCallbackInfo.ExistingWindow(windowHandle))
		{
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x000160C8 File Offset: 0x000142C8
		public WaveIn(WaveCallbackInfo callbackInfo)
		{
			this.syncContext = SynchronizationContext.Current;
			if ((callbackInfo.Strategy == WaveCallbackStrategy.NewWindow || callbackInfo.Strategy == WaveCallbackStrategy.ExistingWindow) && this.syncContext == null)
			{
				throw new InvalidOperationException("Use WaveInEvent to record on a background thread");
			}
			this.DeviceNumber = 0;
			this.WaveFormat = new WaveFormat(8000, 16, 1);
			this.BufferMilliseconds = 100;
			this.NumberOfBuffers = 3;
			this.callback = new WaveInterop.WaveCallback(this.Callback);
			this.callbackInfo = callbackInfo;
			callbackInfo.Connect(this.callback);
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000799 RID: 1945 RVA: 0x00016159 File Offset: 0x00014359
		public static int DeviceCount
		{
			get
			{
				return WaveInterop.waveInGetNumDevs();
			}
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x00016160 File Offset: 0x00014360
		public static WaveInCapabilities GetCapabilities(int devNumber)
		{
			WaveInCapabilities waveInCapabilities = default(WaveInCapabilities);
			int num = Marshal.SizeOf(waveInCapabilities);
			MmException.Try(WaveInterop.waveInGetDevCaps((IntPtr)devNumber, out waveInCapabilities, num), "waveInGetDevCaps");
			return waveInCapabilities;
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x0600079B RID: 1947 RVA: 0x0001619A File Offset: 0x0001439A
		// (set) Token: 0x0600079C RID: 1948 RVA: 0x000161A2 File Offset: 0x000143A2
		public int BufferMilliseconds { get; set; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600079D RID: 1949 RVA: 0x000161AB File Offset: 0x000143AB
		// (set) Token: 0x0600079E RID: 1950 RVA: 0x000161B3 File Offset: 0x000143B3
		public int NumberOfBuffers { get; set; }

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600079F RID: 1951 RVA: 0x000161BC File Offset: 0x000143BC
		// (set) Token: 0x060007A0 RID: 1952 RVA: 0x000161C4 File Offset: 0x000143C4
		public int DeviceNumber { get; set; }

		// Token: 0x060007A1 RID: 1953 RVA: 0x000161D0 File Offset: 0x000143D0
		private void CreateBuffers()
		{
			int num = this.BufferMilliseconds * this.WaveFormat.AverageBytesPerSecond / 1000;
			if (num % this.WaveFormat.BlockAlign != 0)
			{
				num -= num % this.WaveFormat.BlockAlign;
			}
			this.buffers = new WaveInBuffer[this.NumberOfBuffers];
			for (int i = 0; i < this.buffers.Length; i++)
			{
				this.buffers[i] = new WaveInBuffer(this.waveInHandle, num);
			}
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x00016250 File Offset: 0x00014450
		private void Callback(IntPtr waveInHandle, WaveInterop.WaveMessage message, IntPtr userData, WaveHeader waveHeader, IntPtr reserved)
		{
			if (message == WaveInterop.WaveMessage.WaveInData && this.recording)
			{
				WaveInBuffer waveInBuffer = (WaveInBuffer)((GCHandle)waveHeader.userData).Target;
				if (waveInBuffer == null)
				{
					return;
				}
				this.lastReturnedBufferIndex = Array.IndexOf<WaveInBuffer>(this.buffers, waveInBuffer);
				this.RaiseDataAvailable(waveInBuffer);
				try
				{
					waveInBuffer.Reuse();
				}
				catch (Exception ex)
				{
					this.recording = false;
					this.RaiseRecordingStopped(ex);
				}
			}
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x000162D4 File Offset: 0x000144D4
		private void RaiseDataAvailable(WaveInBuffer buffer)
		{
			EventHandler<WaveInEventArgs> dataAvailable = this.DataAvailable;
			if (dataAvailable != null)
			{
				dataAvailable.Invoke(this, new WaveInEventArgs(buffer.Data, buffer.BytesRecorded));
			}
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0001632C File Offset: 0x0001452C
		private void RaiseRecordingStopped(Exception e)
		{
			EventHandler<StoppedEventArgs> handler = this.RecordingStopped;
			if (handler != null)
			{
				if (this.syncContext == null)
				{
					handler.Invoke(this, new StoppedEventArgs(e));
					return;
				}
				this.syncContext.Post(delegate(object state)
				{
					handler.Invoke(this, new StoppedEventArgs(e));
				}, null);
			}
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x000163A0 File Offset: 0x000145A0
		private void OpenWaveInDevice()
		{
			this.CloseWaveInDevice();
			MmResult mmResult = this.callbackInfo.WaveInOpen(out this.waveInHandle, this.DeviceNumber, this.WaveFormat, this.callback);
			MmException.Try(mmResult, "waveInOpen");
			this.CreateBuffers();
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x000163E8 File Offset: 0x000145E8
		public void StartRecording()
		{
			if (this.recording)
			{
				throw new InvalidOperationException("Already recording");
			}
			this.OpenWaveInDevice();
			this.EnqueueBuffers();
			MmException.Try(WaveInterop.waveInStart(this.waveInHandle), "waveInStart");
			this.recording = true;
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x00016434 File Offset: 0x00014634
		private void EnqueueBuffers()
		{
			foreach (WaveInBuffer waveInBuffer in this.buffers)
			{
				if (!waveInBuffer.InQueue)
				{
					waveInBuffer.Reuse();
				}
			}
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x00016468 File Offset: 0x00014668
		public void StopRecording()
		{
			if (this.recording)
			{
				this.recording = false;
				MmException.Try(WaveInterop.waveInStop(this.waveInHandle), "waveInStop");
				for (int i = 0; i < this.buffers.Length; i++)
				{
					int num = (i + this.lastReturnedBufferIndex + 1) % this.buffers.Length;
					WaveInBuffer waveInBuffer = this.buffers[num];
					if (waveInBuffer.Done)
					{
						this.RaiseDataAvailable(waveInBuffer);
					}
				}
				this.RaiseRecordingStopped(null);
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060007A9 RID: 1961 RVA: 0x000164E3 File Offset: 0x000146E3
		// (set) Token: 0x060007AA RID: 1962 RVA: 0x000164EB File Offset: 0x000146EB
		public WaveFormat WaveFormat { get; set; }

		// Token: 0x060007AB RID: 1963 RVA: 0x000164F4 File Offset: 0x000146F4
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.recording)
				{
					this.StopRecording();
				}
				this.CloseWaveInDevice();
				if (this.callbackInfo != null)
				{
					this.callbackInfo.Disconnect();
					this.callbackInfo = null;
				}
			}
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0001652C File Offset: 0x0001472C
		private void CloseWaveInDevice()
		{
			if (this.waveInHandle == IntPtr.Zero)
			{
				return;
			}
			WaveInterop.waveInReset(this.waveInHandle);
			if (this.buffers != null)
			{
				for (int i = 0; i < this.buffers.Length; i++)
				{
					this.buffers[i].Dispose();
				}
				this.buffers = null;
			}
			WaveInterop.waveInClose(this.waveInHandle);
			this.waveInHandle = IntPtr.Zero;
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x000165A0 File Offset: 0x000147A0
		public MixerLine GetMixerLine()
		{
			MixerLine mixerLine;
			if (this.waveInHandle != IntPtr.Zero)
			{
				mixerLine = new MixerLine(this.waveInHandle, 0, MixerFlags.WaveInHandle);
			}
			else
			{
				mixerLine = new MixerLine((IntPtr)this.DeviceNumber, 0, MixerFlags.WaveIn);
			}
			return mixerLine;
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x000165EB File Offset: 0x000147EB
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000908 RID: 2312
		private IntPtr waveInHandle;

		// Token: 0x04000909 RID: 2313
		private volatile bool recording;

		// Token: 0x0400090A RID: 2314
		private WaveInBuffer[] buffers;

		// Token: 0x0400090B RID: 2315
		private readonly WaveInterop.WaveCallback callback;

		// Token: 0x0400090C RID: 2316
		private WaveCallbackInfo callbackInfo;

		// Token: 0x0400090D RID: 2317
		private readonly SynchronizationContext syncContext;

		// Token: 0x0400090E RID: 2318
		private int lastReturnedBufferIndex;
	}
}
