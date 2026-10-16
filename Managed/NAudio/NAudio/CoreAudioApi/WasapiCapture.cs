using System;
using System.Runtime.InteropServices;
using System.Threading;
using NAudio.Wave;

namespace NAudio.CoreAudioApi
{
	// Token: 0x02000083 RID: 131
	public class WasapiCapture : IWaveIn, IDisposable
	{
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060002D3 RID: 723 RVA: 0x00009678 File Offset: 0x00007878
		// (remove) Token: 0x060002D4 RID: 724 RVA: 0x000096B0 File Offset: 0x000078B0
		public event EventHandler<WaveInEventArgs> DataAvailable;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060002D5 RID: 725 RVA: 0x000096E8 File Offset: 0x000078E8
		// (remove) Token: 0x060002D6 RID: 726 RVA: 0x00009720 File Offset: 0x00007920
		public event EventHandler<StoppedEventArgs> RecordingStopped;

		// Token: 0x060002D7 RID: 727 RVA: 0x00009755 File Offset: 0x00007955
		public WasapiCapture()
			: this(WasapiCapture.GetDefaultCaptureDevice())
		{
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00009762 File Offset: 0x00007962
		public WasapiCapture(MMDevice captureDevice)
			: this(captureDevice, false)
		{
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000976C File Offset: 0x0000796C
		public WasapiCapture(MMDevice captureDevice, bool useEventSync)
			: this(captureDevice, useEventSync, 100)
		{
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00009778 File Offset: 0x00007978
		public WasapiCapture(MMDevice captureDevice, bool useEventSync, int audioBufferMillisecondsLength)
		{
			this.syncContext = SynchronizationContext.Current;
			this.audioClient = captureDevice.AudioClient;
			this.ShareMode = AudioClientShareMode.Shared;
			this.isUsingEventSync = useEventSync;
			this.audioBufferMillisecondsLength = audioBufferMillisecondsLength;
			this.waveFormat = this.audioClient.MixFormat;
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060002DB RID: 731 RVA: 0x000097C8 File Offset: 0x000079C8
		// (set) Token: 0x060002DC RID: 732 RVA: 0x000097D0 File Offset: 0x000079D0
		public AudioClientShareMode ShareMode { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060002DD RID: 733 RVA: 0x000097DC File Offset: 0x000079DC
		// (set) Token: 0x060002DE RID: 734 RVA: 0x0000981C File Offset: 0x00007A1C
		public virtual WaveFormat WaveFormat
		{
			get
			{
				WaveFormatExtensible waveFormatExtensible = this.waveFormat as WaveFormatExtensible;
				if (waveFormatExtensible != null)
				{
					try
					{
						return waveFormatExtensible.ToStandardWaveFormat();
					}
					catch (InvalidOperationException)
					{
					}
				}
				return this.waveFormat;
			}
			set
			{
				this.waveFormat = value;
			}
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00009828 File Offset: 0x00007A28
		public static MMDevice GetDefaultCaptureDevice()
		{
			MMDeviceEnumerator mmdeviceEnumerator = new MMDeviceEnumerator();
			return mmdeviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Console);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00009844 File Offset: 0x00007A44
		private void InitializeCaptureDevice()
		{
			if (this.initialized)
			{
				return;
			}
			long num = 10000L * (long)this.audioBufferMillisecondsLength;
			if (!this.audioClient.IsFormatSupported(this.ShareMode, this.waveFormat))
			{
				throw new ArgumentException("Unsupported Wave Format");
			}
			AudioClientStreamFlags audioClientStreamFlags = this.GetAudioClientStreamFlags();
			if (this.isUsingEventSync)
			{
				if (this.ShareMode == AudioClientShareMode.Shared)
				{
					this.audioClient.Initialize(this.ShareMode, AudioClientStreamFlags.EventCallback, num, 0L, this.waveFormat, Guid.Empty);
				}
				else
				{
					this.audioClient.Initialize(this.ShareMode, AudioClientStreamFlags.EventCallback, num, num, this.waveFormat, Guid.Empty);
				}
				this.frameEventWaitHandle = new EventWaitHandle(false, 0);
				this.audioClient.SetEventHandle(this.frameEventWaitHandle.SafeWaitHandle.DangerousGetHandle());
			}
			else
			{
				this.audioClient.Initialize(this.ShareMode, audioClientStreamFlags, num, 0L, this.waveFormat, Guid.Empty);
			}
			int bufferSize = this.audioClient.BufferSize;
			this.bytesPerFrame = this.waveFormat.Channels * this.waveFormat.BitsPerSample / 8;
			this.recordBuffer = new byte[bufferSize * this.bytesPerFrame];
			this.initialized = true;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000997C File Offset: 0x00007B7C
		protected virtual AudioClientStreamFlags GetAudioClientStreamFlags()
		{
			return AudioClientStreamFlags.None;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00009990 File Offset: 0x00007B90
		public void StartRecording()
		{
			if (this.captureThread != null)
			{
				throw new InvalidOperationException("Previous recording still in progress");
			}
			this.InitializeCaptureDevice();
			ThreadStart threadStart = delegate
			{
				this.CaptureThread(this.audioClient);
			};
			this.captureThread = new Thread(threadStart);
			this.requestStop = false;
			this.captureThread.Start();
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x000099E3 File Offset: 0x00007BE3
		public void StopRecording()
		{
			this.requestStop = true;
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x000099F0 File Offset: 0x00007BF0
		private void CaptureThread(AudioClient client)
		{
			Exception ex = null;
			try
			{
				this.DoRecording(client);
			}
			catch (Exception ex2)
			{
				ex = ex2;
			}
			finally
			{
				client.Stop();
			}
			this.captureThread = null;
			this.RaiseRecordingStopped(ex);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00009A40 File Offset: 0x00007C40
		private void DoRecording(AudioClient client)
		{
			int bufferSize = client.BufferSize;
			long num = (long)(10000000.0 * (double)bufferSize / (double)this.waveFormat.SampleRate);
			int num2 = (int)(num / 10000L / 2L);
			int num3 = (int)(3L * num / 10000L);
			AudioCaptureClient audioCaptureClient = client.AudioCaptureClient;
			client.Start();
			if (this.isUsingEventSync)
			{
			}
			while (!this.requestStop)
			{
				bool flag = true;
				if (this.isUsingEventSync)
				{
					flag = this.frameEventWaitHandle.WaitOne(num3, false);
				}
				else
				{
					Thread.Sleep(num2);
				}
				if (!this.requestStop && flag)
				{
					this.ReadNextPacket(audioCaptureClient);
				}
			}
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00009B0C File Offset: 0x00007D0C
		private void RaiseRecordingStopped(Exception e)
		{
			EventHandler<StoppedEventArgs> handler = this.RecordingStopped;
			if (handler == null)
			{
				return;
			}
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

		// Token: 0x060002E7 RID: 743 RVA: 0x00009B84 File Offset: 0x00007D84
		private void ReadNextPacket(AudioCaptureClient capture)
		{
			int num = capture.GetNextPacketSize();
			int num2 = 0;
			while (num != 0)
			{
				int num3;
				AudioClientBufferFlags audioClientBufferFlags;
				IntPtr buffer = capture.GetBuffer(out num3, out audioClientBufferFlags);
				int num4 = num3 * this.bytesPerFrame;
				int num5 = Math.Max(0, this.recordBuffer.Length - num2);
				if (num5 < num4 && num2 > 0)
				{
					if (this.DataAvailable != null)
					{
						this.DataAvailable.Invoke(this, new WaveInEventArgs(this.recordBuffer, num2));
					}
					num2 = 0;
				}
				if ((audioClientBufferFlags & AudioClientBufferFlags.Silent) != AudioClientBufferFlags.Silent)
				{
					Marshal.Copy(buffer, this.recordBuffer, num2, num4);
				}
				else
				{
					Array.Clear(this.recordBuffer, num2, num4);
				}
				num2 += num4;
				capture.ReleaseBuffer(num3);
				num = capture.GetNextPacketSize();
			}
			if (this.DataAvailable != null)
			{
				this.DataAvailable.Invoke(this, new WaveInEventArgs(this.recordBuffer, num2));
			}
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00009C52 File Offset: 0x00007E52
		public void Dispose()
		{
			this.StopRecording();
			if (this.captureThread != null)
			{
				this.captureThread.Join();
				this.captureThread = null;
			}
			if (this.audioClient != null)
			{
				this.audioClient.Dispose();
				this.audioClient = null;
			}
		}

		// Token: 0x040003BC RID: 956
		private const long REFTIMES_PER_SEC = 10000000L;

		// Token: 0x040003BD RID: 957
		private const long REFTIMES_PER_MILLISEC = 10000L;

		// Token: 0x040003BE RID: 958
		private volatile bool requestStop;

		// Token: 0x040003BF RID: 959
		private byte[] recordBuffer;

		// Token: 0x040003C0 RID: 960
		private Thread captureThread;

		// Token: 0x040003C1 RID: 961
		private AudioClient audioClient;

		// Token: 0x040003C2 RID: 962
		private int bytesPerFrame;

		// Token: 0x040003C3 RID: 963
		private WaveFormat waveFormat;

		// Token: 0x040003C4 RID: 964
		private bool initialized;

		// Token: 0x040003C5 RID: 965
		private readonly SynchronizationContext syncContext;

		// Token: 0x040003C6 RID: 966
		private readonly bool isUsingEventSync;

		// Token: 0x040003C7 RID: 967
		private EventWaitHandle frameEventWaitHandle;

		// Token: 0x040003C8 RID: 968
		private int audioBufferMillisecondsLength;
	}
}
