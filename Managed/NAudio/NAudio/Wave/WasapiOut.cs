using System;
using System.Runtime.InteropServices;
using System.Threading;
using NAudio.CoreAudioApi;

namespace NAudio.Wave
{
	// Token: 0x020001BE RID: 446
	public class WasapiOut : IWavePlayer, IDisposable, IWavePosition
	{
		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06000950 RID: 2384 RVA: 0x0001A9DC File Offset: 0x00018BDC
		// (remove) Token: 0x06000951 RID: 2385 RVA: 0x0001AA14 File Offset: 0x00018C14
		public event EventHandler<StoppedEventArgs> PlaybackStopped;

		// Token: 0x06000952 RID: 2386 RVA: 0x0001AA49 File Offset: 0x00018C49
		public WasapiOut(AudioClientShareMode shareMode, int latency)
			: this(WasapiOut.GetDefaultAudioEndpoint(), shareMode, true, latency)
		{
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x0001AA59 File Offset: 0x00018C59
		public WasapiOut(AudioClientShareMode shareMode, bool useEventSync, int latency)
			: this(WasapiOut.GetDefaultAudioEndpoint(), shareMode, useEventSync, latency)
		{
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x0001AA69 File Offset: 0x00018C69
		public WasapiOut(MMDevice device, AudioClientShareMode shareMode, bool useEventSync, int latency)
		{
			this.audioClient = device.AudioClient;
			this.mmDevice = device;
			this.shareMode = shareMode;
			this.isUsingEventSync = useEventSync;
			this.latencyMilliseconds = latency;
			this.syncContext = SynchronizationContext.Current;
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x0001AAA8 File Offset: 0x00018CA8
		private static MMDevice GetDefaultAudioEndpoint()
		{
			if (Environment.OSVersion.Version.Major < 6)
			{
				throw new NotSupportedException("WASAPI supported only on Windows Vista and above");
			}
			MMDeviceEnumerator mmdeviceEnumerator = new MMDeviceEnumerator();
			return mmdeviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x0001AAE0 File Offset: 0x00018CE0
		private void PlayThread()
		{
			ResamplerDmoStream resamplerDmoStream = null;
			IWaveProvider waveProvider = this.sourceProvider;
			Exception ex = null;
			try
			{
				if (this.dmoResamplerNeeded)
				{
					resamplerDmoStream = new ResamplerDmoStream(this.sourceProvider, this.outputFormat);
					waveProvider = resamplerDmoStream;
				}
				this.bufferFrameCount = this.audioClient.BufferSize;
				this.bytesPerFrame = this.outputFormat.Channels * this.outputFormat.BitsPerSample / 8;
				this.readBuffer = new byte[this.bufferFrameCount * this.bytesPerFrame];
				this.FillBuffer(waveProvider, this.bufferFrameCount);
				WaitHandle[] array = new WaitHandle[] { this.frameEventWaitHandle };
				this.audioClient.Start();
				while (this.playbackState != PlaybackState.Stopped)
				{
					int num = 0;
					if (this.isUsingEventSync)
					{
						num = WaitHandle.WaitAny(array, 3 * this.latencyMilliseconds, false);
					}
					else
					{
						Thread.Sleep(this.latencyMilliseconds / 2);
					}
					if (this.playbackState == PlaybackState.Playing && num != 258)
					{
						int num2;
						if (this.isUsingEventSync)
						{
							num2 = ((this.shareMode == AudioClientShareMode.Shared) ? this.audioClient.CurrentPadding : 0);
						}
						else
						{
							num2 = this.audioClient.CurrentPadding;
						}
						int num3 = this.bufferFrameCount - num2;
						if (num3 > 10)
						{
							this.FillBuffer(waveProvider, num3);
						}
					}
				}
				Thread.Sleep(this.latencyMilliseconds / 2);
				this.audioClient.Stop();
				if (this.playbackState == PlaybackState.Stopped)
				{
					this.audioClient.Reset();
				}
			}
			catch (Exception ex2)
			{
				ex = ex2;
			}
			finally
			{
				if (resamplerDmoStream != null)
				{
					resamplerDmoStream.Dispose();
				}
				this.RaisePlaybackStopped(ex);
			}
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x0001ACCC File Offset: 0x00018ECC
		private void RaisePlaybackStopped(Exception e)
		{
			EventHandler<StoppedEventArgs> handler = this.PlaybackStopped;
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

		// Token: 0x06000958 RID: 2392 RVA: 0x0001AD40 File Offset: 0x00018F40
		private void FillBuffer(IWaveProvider playbackProvider, int frameCount)
		{
			IntPtr buffer = this.renderClient.GetBuffer(frameCount);
			int num = frameCount * this.bytesPerFrame;
			int num2 = playbackProvider.Read(this.readBuffer, 0, num);
			if (num2 == 0)
			{
				this.playbackState = PlaybackState.Stopped;
			}
			Marshal.Copy(this.readBuffer, 0, buffer, num2);
			int num3 = num2 / this.bytesPerFrame;
			this.renderClient.ReleaseBuffer(num3, AudioClientBufferFlags.None);
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x0001ADA2 File Offset: 0x00018FA2
		public long GetPosition()
		{
			if (this.playbackState == PlaybackState.Stopped)
			{
				return 0L;
			}
			return (long)this.audioClient.AudioClockClient.AdjustedPosition;
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x0600095A RID: 2394 RVA: 0x0001ADC1 File Offset: 0x00018FC1
		public WaveFormat OutputWaveFormat
		{
			get
			{
				return this.outputFormat;
			}
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x0001ADCC File Offset: 0x00018FCC
		public void Play()
		{
			if (this.playbackState != PlaybackState.Playing)
			{
				if (this.playbackState == PlaybackState.Stopped)
				{
					this.playThread = new Thread(new ThreadStart(this.PlayThread));
					this.playbackState = PlaybackState.Playing;
					this.playThread.Start();
					return;
				}
				this.playbackState = PlaybackState.Playing;
			}
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x0001AE23 File Offset: 0x00019023
		public void Stop()
		{
			if (this.playbackState != PlaybackState.Stopped)
			{
				this.playbackState = PlaybackState.Stopped;
				this.playThread.Join();
				this.playThread = null;
			}
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x0001AE4A File Offset: 0x0001904A
		public void Pause()
		{
			if (this.playbackState == PlaybackState.Playing)
			{
				this.playbackState = PlaybackState.Paused;
			}
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x0001AE60 File Offset: 0x00019060
		public void Init(IWaveProvider waveProvider)
		{
			long num = (long)(this.latencyMilliseconds * 10000);
			this.outputFormat = waveProvider.WaveFormat;
			WaveFormatExtensible waveFormatExtensible;
			if (!this.audioClient.IsFormatSupported(this.shareMode, this.outputFormat, out waveFormatExtensible))
			{
				if (waveFormatExtensible == null)
				{
					WaveFormat waveFormat = this.audioClient.MixFormat;
					if (!this.audioClient.IsFormatSupported(this.shareMode, waveFormat))
					{
						foreach (WaveFormatExtensible waveFormat in new WaveFormatExtensible[]
						{
							new WaveFormatExtensible(this.outputFormat.SampleRate, 32, this.outputFormat.Channels),
							new WaveFormatExtensible(this.outputFormat.SampleRate, 24, this.outputFormat.Channels),
							new WaveFormatExtensible(this.outputFormat.SampleRate, 16, this.outputFormat.Channels)
						})
						{
							if (this.audioClient.IsFormatSupported(this.shareMode, waveFormat))
							{
								break;
							}
							waveFormat = null;
						}
						if (waveFormat == null)
						{
							waveFormat = new WaveFormatExtensible(this.outputFormat.SampleRate, 16, 2);
							if (!this.audioClient.IsFormatSupported(this.shareMode, waveFormat))
							{
								throw new NotSupportedException("Can't find a supported format to use");
							}
						}
					}
					this.outputFormat = waveFormat;
				}
				else
				{
					this.outputFormat = waveFormatExtensible;
				}
				using (new ResamplerDmoStream(waveProvider, this.outputFormat))
				{
				}
				this.dmoResamplerNeeded = true;
			}
			else
			{
				this.dmoResamplerNeeded = false;
			}
			this.sourceProvider = waveProvider;
			if (this.isUsingEventSync)
			{
				if (this.shareMode == AudioClientShareMode.Shared)
				{
					this.audioClient.Initialize(this.shareMode, AudioClientStreamFlags.EventCallback, 0L, 0L, this.outputFormat, Guid.Empty);
					this.latencyMilliseconds = (int)(this.audioClient.StreamLatency / 10000L);
				}
				else
				{
					this.audioClient.Initialize(this.shareMode, AudioClientStreamFlags.EventCallback, num, num, this.outputFormat, Guid.Empty);
				}
				this.frameEventWaitHandle = new EventWaitHandle(false, 0);
				this.audioClient.SetEventHandle(this.frameEventWaitHandle.SafeWaitHandle.DangerousGetHandle());
			}
			else
			{
				this.audioClient.Initialize(this.shareMode, AudioClientStreamFlags.None, num, 0L, this.outputFormat, Guid.Empty);
			}
			this.renderClient = this.audioClient.AudioRenderClient;
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x0600095F RID: 2399 RVA: 0x0001B0C4 File Offset: 0x000192C4
		public PlaybackState PlaybackState
		{
			get
			{
				return this.playbackState;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000960 RID: 2400 RVA: 0x0001B0CE File Offset: 0x000192CE
		// (set) Token: 0x06000961 RID: 2401 RVA: 0x0001B0E0 File Offset: 0x000192E0
		public float Volume
		{
			get
			{
				return this.mmDevice.AudioEndpointVolume.MasterVolumeLevelScalar;
			}
			set
			{
				if (value < 0f)
				{
					throw new ArgumentOutOfRangeException("value", "Volume must be between 0.0 and 1.0");
				}
				if (value > 1f)
				{
					throw new ArgumentOutOfRangeException("value", "Volume must be between 0.0 and 1.0");
				}
				this.mmDevice.AudioEndpointVolume.MasterVolumeLevelScalar = value;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x0001B12E File Offset: 0x0001932E
		public AudioStreamVolume AudioStreamVolume
		{
			get
			{
				if (this.shareMode == AudioClientShareMode.Exclusive)
				{
					throw new InvalidOperationException("AudioStreamVolume is ONLY supported for shared audio streams.");
				}
				return this.audioClient.AudioStreamVolume;
			}
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x0001B14F File Offset: 0x0001934F
		public void Dispose()
		{
			if (this.audioClient != null)
			{
				this.Stop();
				this.audioClient.Dispose();
				this.audioClient = null;
				this.renderClient = null;
			}
		}

		// Token: 0x04000AD2 RID: 2770
		private AudioClient audioClient;

		// Token: 0x04000AD3 RID: 2771
		private readonly MMDevice mmDevice;

		// Token: 0x04000AD4 RID: 2772
		private readonly AudioClientShareMode shareMode;

		// Token: 0x04000AD5 RID: 2773
		private AudioRenderClient renderClient;

		// Token: 0x04000AD6 RID: 2774
		private IWaveProvider sourceProvider;

		// Token: 0x04000AD7 RID: 2775
		private int latencyMilliseconds;

		// Token: 0x04000AD8 RID: 2776
		private int bufferFrameCount;

		// Token: 0x04000AD9 RID: 2777
		private int bytesPerFrame;

		// Token: 0x04000ADA RID: 2778
		private readonly bool isUsingEventSync;

		// Token: 0x04000ADB RID: 2779
		private EventWaitHandle frameEventWaitHandle;

		// Token: 0x04000ADC RID: 2780
		private byte[] readBuffer;

		// Token: 0x04000ADD RID: 2781
		private volatile PlaybackState playbackState;

		// Token: 0x04000ADE RID: 2782
		private Thread playThread;

		// Token: 0x04000ADF RID: 2783
		private WaveFormat outputFormat;

		// Token: 0x04000AE0 RID: 2784
		private bool dmoResamplerNeeded;

		// Token: 0x04000AE1 RID: 2785
		private readonly SynchronizationContext syncContext;
	}
}
