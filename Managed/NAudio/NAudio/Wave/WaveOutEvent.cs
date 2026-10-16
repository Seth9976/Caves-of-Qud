using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace NAudio.Wave
{
	// Token: 0x020001C3 RID: 451
	public class WaveOutEvent : IWavePlayer, IDisposable, IWavePosition
	{
		// Token: 0x14000013 RID: 19
		// (add) Token: 0x060009A5 RID: 2469 RVA: 0x0001BC00 File Offset: 0x00019E00
		// (remove) Token: 0x060009A6 RID: 2470 RVA: 0x0001BC38 File Offset: 0x00019E38
		public event EventHandler<StoppedEventArgs> PlaybackStopped;

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060009A7 RID: 2471 RVA: 0x0001BC6D File Offset: 0x00019E6D
		// (set) Token: 0x060009A8 RID: 2472 RVA: 0x0001BC75 File Offset: 0x00019E75
		public int DesiredLatency { get; set; }

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x0001BC7E File Offset: 0x00019E7E
		// (set) Token: 0x060009AA RID: 2474 RVA: 0x0001BC86 File Offset: 0x00019E86
		public int NumberOfBuffers { get; set; }

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060009AB RID: 2475 RVA: 0x0001BC8F File Offset: 0x00019E8F
		// (set) Token: 0x060009AC RID: 2476 RVA: 0x0001BC97 File Offset: 0x00019E97
		public int DeviceNumber { get; set; }

		// Token: 0x060009AD RID: 2477 RVA: 0x0001BCA0 File Offset: 0x00019EA0
		public WaveOutEvent()
		{
			this.syncContext = SynchronizationContext.Current;
			if (this.syncContext != null && (this.syncContext.GetType().Name == "LegacyAspNetSynchronizationContext" || this.syncContext.GetType().Name == "AspNetSynchronizationContext"))
			{
				this.syncContext = null;
			}
			this.DeviceNumber = 0;
			this.DesiredLatency = 300;
			this.NumberOfBuffers = 2;
			this.waveOutLock = new object();
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0001BD34 File Offset: 0x00019F34
		public void Init(IWaveProvider waveProvider)
		{
			if (this.playbackState != PlaybackState.Stopped)
			{
				throw new InvalidOperationException("Can't re-initialize during playback");
			}
			if (this.hWaveOut != IntPtr.Zero)
			{
				this.DisposeBuffers();
				this.CloseWaveOut();
			}
			this.callbackEvent = new AutoResetEvent(false);
			this.waveStream = waveProvider;
			int num = waveProvider.WaveFormat.ConvertLatencyToByteSize((this.DesiredLatency + this.NumberOfBuffers - 1) / this.NumberOfBuffers);
			MmResult mmResult;
			lock (this.waveOutLock)
			{
				mmResult = WaveInterop.waveOutOpenWindow(out this.hWaveOut, (IntPtr)this.DeviceNumber, this.waveStream.WaveFormat, this.callbackEvent.SafeWaitHandle.DangerousGetHandle(), IntPtr.Zero, WaveInterop.WaveInOutOpenFlags.CallbackEvent);
			}
			MmException.Try(mmResult, "waveOutOpen");
			this.buffers = new WaveOutBuffer[this.NumberOfBuffers];
			this.playbackState = PlaybackState.Stopped;
			for (int i = 0; i < this.NumberOfBuffers; i++)
			{
				this.buffers[i] = new WaveOutBuffer(this.hWaveOut, num, this.waveStream, this.waveOutLock);
			}
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x0001BE6C File Offset: 0x0001A06C
		public void Play()
		{
			if (this.buffers == null || this.waveStream == null)
			{
				throw new InvalidOperationException("Must call Init first");
			}
			if (this.playbackState == PlaybackState.Stopped)
			{
				this.playbackState = PlaybackState.Playing;
				this.callbackEvent.Set();
				ThreadPool.QueueUserWorkItem(delegate(object state)
				{
					this.PlaybackThread();
				}, null);
				return;
			}
			if (this.playbackState == PlaybackState.Paused)
			{
				this.Resume();
				this.callbackEvent.Set();
			}
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x0001BEEC File Offset: 0x0001A0EC
		private void PlaybackThread()
		{
			Exception ex = null;
			try
			{
				this.DoPlayback();
			}
			catch (Exception ex2)
			{
				ex = ex2;
			}
			finally
			{
				this.playbackState = PlaybackState.Stopped;
				this.RaisePlaybackStoppedEvent(ex);
			}
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x0001BF38 File Offset: 0x0001A138
		private void DoPlayback()
		{
			while (this.playbackState != PlaybackState.Stopped)
			{
				if (!this.callbackEvent.WaitOne(this.DesiredLatency))
				{
					PlaybackState playbackState = this.playbackState;
				}
				if (this.playbackState == PlaybackState.Playing)
				{
					int num = 0;
					foreach (WaveOutBuffer waveOutBuffer in this.buffers)
					{
						if (waveOutBuffer.InQueue || waveOutBuffer.OnDone())
						{
							num++;
						}
					}
					if (num == 0)
					{
						this.playbackState = PlaybackState.Stopped;
						this.callbackEvent.Set();
					}
				}
			}
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x0001BFC4 File Offset: 0x0001A1C4
		public void Pause()
		{
			if (this.playbackState == PlaybackState.Playing)
			{
				MmResult mmResult;
				lock (this.waveOutLock)
				{
					mmResult = WaveInterop.waveOutPause(this.hWaveOut);
				}
				if (mmResult != MmResult.NoError)
				{
					throw new MmException(mmResult, "waveOutPause");
				}
				this.playbackState = PlaybackState.Paused;
			}
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x0001C028 File Offset: 0x0001A228
		private void Resume()
		{
			if (this.playbackState == PlaybackState.Paused)
			{
				MmResult mmResult;
				lock (this.waveOutLock)
				{
					mmResult = WaveInterop.waveOutRestart(this.hWaveOut);
				}
				if (mmResult != MmResult.NoError)
				{
					throw new MmException(mmResult, "waveOutRestart");
				}
				this.playbackState = PlaybackState.Playing;
			}
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0001C08C File Offset: 0x0001A28C
		public void Stop()
		{
			if (this.playbackState != PlaybackState.Stopped)
			{
				this.playbackState = PlaybackState.Stopped;
				MmResult mmResult;
				lock (this.waveOutLock)
				{
					mmResult = WaveInterop.waveOutReset(this.hWaveOut);
				}
				if (mmResult != MmResult.NoError)
				{
					throw new MmException(mmResult, "waveOutReset");
				}
				this.callbackEvent.Set();
			}
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0001C0FC File Offset: 0x0001A2FC
		public long GetPosition()
		{
			long num;
			lock (this.waveOutLock)
			{
				MmTime mmTime = default(MmTime);
				mmTime.wType = 4U;
				MmException.Try(WaveInterop.waveOutGetPosition(this.hWaveOut, out mmTime, Marshal.SizeOf(mmTime)), "waveOutGetPosition");
				if (mmTime.wType != 4U)
				{
					throw new Exception(string.Format("waveOutGetPosition: wType -> Expected {0}, Received {1}", 4, mmTime.wType));
				}
				num = (long)((ulong)mmTime.cb);
			}
			return num;
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060009B6 RID: 2486 RVA: 0x0001C198 File Offset: 0x0001A398
		public WaveFormat OutputWaveFormat
		{
			get
			{
				return this.waveStream.WaveFormat;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060009B7 RID: 2487 RVA: 0x0001C1A5 File Offset: 0x0001A3A5
		public PlaybackState PlaybackState
		{
			get
			{
				return this.playbackState;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060009B8 RID: 2488 RVA: 0x0001C1AF File Offset: 0x0001A3AF
		// (set) Token: 0x060009B9 RID: 2489 RVA: 0x0001C1B7 File Offset: 0x0001A3B7
		[Obsolete]
		public float Volume
		{
			get
			{
				return this.volume;
			}
			set
			{
				WaveOut.SetWaveOutVolume(value, this.hWaveOut, this.waveOutLock);
				this.volume = value;
			}
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x0001C1D2 File Offset: 0x0001A3D2
		public void Dispose()
		{
			GC.SuppressFinalize(this);
			this.Dispose(true);
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x0001C1E1 File Offset: 0x0001A3E1
		protected void Dispose(bool disposing)
		{
			this.Stop();
			if (disposing)
			{
				this.DisposeBuffers();
			}
			this.CloseWaveOut();
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x0001C1F8 File Offset: 0x0001A3F8
		private void CloseWaveOut()
		{
			if (this.callbackEvent != null)
			{
				this.callbackEvent.Close();
				this.callbackEvent = null;
			}
			lock (this.waveOutLock)
			{
				if (this.hWaveOut != IntPtr.Zero)
				{
					WaveInterop.waveOutClose(this.hWaveOut);
					this.hWaveOut = IntPtr.Zero;
				}
			}
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x0001C270 File Offset: 0x0001A470
		private void DisposeBuffers()
		{
			if (this.buffers != null)
			{
				foreach (WaveOutBuffer waveOutBuffer in this.buffers)
				{
					waveOutBuffer.Dispose();
				}
				this.buffers = null;
			}
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x0001C2AC File Offset: 0x0001A4AC
		~WaveOutEvent()
		{
			this.Dispose(false);
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x0001C304 File Offset: 0x0001A504
		private void RaisePlaybackStoppedEvent(Exception e)
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

		// Token: 0x04000AFD RID: 2813
		private readonly object waveOutLock;

		// Token: 0x04000AFE RID: 2814
		private readonly SynchronizationContext syncContext;

		// Token: 0x04000AFF RID: 2815
		private IntPtr hWaveOut;

		// Token: 0x04000B00 RID: 2816
		private WaveOutBuffer[] buffers;

		// Token: 0x04000B01 RID: 2817
		private IWaveProvider waveStream;

		// Token: 0x04000B02 RID: 2818
		private volatile PlaybackState playbackState;

		// Token: 0x04000B03 RID: 2819
		private AutoResetEvent callbackEvent;

		// Token: 0x04000B04 RID: 2820
		private float volume = 1f;
	}
}
