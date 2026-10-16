using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace NAudio.Wave
{
	// Token: 0x020001C2 RID: 450
	public class WaveOut : IWavePlayer, IDisposable, IWavePosition
	{
		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000987 RID: 2439 RVA: 0x0001B438 File Offset: 0x00019638
		// (remove) Token: 0x06000988 RID: 2440 RVA: 0x0001B470 File Offset: 0x00019670
		public event EventHandler<StoppedEventArgs> PlaybackStopped;

		// Token: 0x06000989 RID: 2441 RVA: 0x0001B4A8 File Offset: 0x000196A8
		public static WaveOutCapabilities GetCapabilities(int devNumber)
		{
			WaveOutCapabilities waveOutCapabilities = default(WaveOutCapabilities);
			int num = Marshal.SizeOf(waveOutCapabilities);
			MmException.Try(WaveInterop.waveOutGetDevCaps((IntPtr)devNumber, out waveOutCapabilities, num), "waveOutGetDevCaps");
			return waveOutCapabilities;
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x0600098A RID: 2442 RVA: 0x0001B4E2 File Offset: 0x000196E2
		public static int DeviceCount
		{
			get
			{
				return WaveInterop.waveOutGetNumDevs();
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x0600098B RID: 2443 RVA: 0x0001B4E9 File Offset: 0x000196E9
		// (set) Token: 0x0600098C RID: 2444 RVA: 0x0001B4F1 File Offset: 0x000196F1
		public int DesiredLatency { get; set; }

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x0600098D RID: 2445 RVA: 0x0001B4FA File Offset: 0x000196FA
		// (set) Token: 0x0600098E RID: 2446 RVA: 0x0001B502 File Offset: 0x00019702
		public int NumberOfBuffers { get; set; }

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x0600098F RID: 2447 RVA: 0x0001B50B File Offset: 0x0001970B
		// (set) Token: 0x06000990 RID: 2448 RVA: 0x0001B513 File Offset: 0x00019713
		public int DeviceNumber { get; set; }

		// Token: 0x06000991 RID: 2449 RVA: 0x0001B51C File Offset: 0x0001971C
		public WaveOut()
			: this((SynchronizationContext.Current == null) ? WaveCallbackInfo.FunctionCallback() : WaveCallbackInfo.NewWindow())
		{
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0001B537 File Offset: 0x00019737
		public WaveOut(IntPtr windowHandle)
			: this(WaveCallbackInfo.ExistingWindow(windowHandle))
		{
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x0001B548 File Offset: 0x00019748
		public WaveOut(WaveCallbackInfo callbackInfo)
		{
			this.syncContext = SynchronizationContext.Current;
			this.DeviceNumber = 0;
			this.DesiredLatency = 300;
			this.NumberOfBuffers = 2;
			this.callback = new WaveInterop.WaveCallback(this.Callback);
			this.waveOutLock = new object();
			this.callbackInfo = callbackInfo;
			callbackInfo.Connect(this.callback);
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x0001B5BC File Offset: 0x000197BC
		public void Init(IWaveProvider waveProvider)
		{
			this.waveStream = waveProvider;
			int num = waveProvider.WaveFormat.ConvertLatencyToByteSize((this.DesiredLatency + this.NumberOfBuffers - 1) / this.NumberOfBuffers);
			MmResult mmResult;
			lock (this.waveOutLock)
			{
				mmResult = this.callbackInfo.WaveOutOpen(out this.hWaveOut, this.DeviceNumber, this.waveStream.WaveFormat, this.callback);
			}
			MmException.Try(mmResult, "waveOutOpen");
			this.buffers = new WaveOutBuffer[this.NumberOfBuffers];
			this.playbackState = PlaybackState.Stopped;
			for (int i = 0; i < this.NumberOfBuffers; i++)
			{
				this.buffers[i] = new WaveOutBuffer(this.hWaveOut, num, this.waveStream, this.waveOutLock);
			}
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x0001B698 File Offset: 0x00019898
		public void Play()
		{
			if (this.playbackState == PlaybackState.Stopped)
			{
				this.playbackState = PlaybackState.Playing;
				this.EnqueueBuffers();
				return;
			}
			if (this.playbackState == PlaybackState.Paused)
			{
				this.EnqueueBuffers();
				this.Resume();
				this.playbackState = PlaybackState.Playing;
			}
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0001B6D4 File Offset: 0x000198D4
		private void EnqueueBuffers()
		{
			for (int i = 0; i < this.NumberOfBuffers; i++)
			{
				if (!this.buffers[i].InQueue)
				{
					if (!this.buffers[i].OnDone())
					{
						this.playbackState = PlaybackState.Stopped;
						return;
					}
					Interlocked.Increment(ref this.queuedBuffers);
				}
			}
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x0001B728 File Offset: 0x00019928
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

		// Token: 0x06000998 RID: 2456 RVA: 0x0001B78C File Offset: 0x0001998C
		public void Resume()
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

		// Token: 0x06000999 RID: 2457 RVA: 0x0001B7F0 File Offset: 0x000199F0
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
				if (this.callbackInfo.Strategy == WaveCallbackStrategy.FunctionCallback)
				{
					this.RaisePlaybackStoppedEvent(null);
				}
			}
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x0001B868 File Offset: 0x00019A68
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

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x0600099B RID: 2459 RVA: 0x0001B904 File Offset: 0x00019B04
		public WaveFormat OutputWaveFormat
		{
			get
			{
				return this.waveStream.WaveFormat;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x0600099C RID: 2460 RVA: 0x0001B911 File Offset: 0x00019B11
		public PlaybackState PlaybackState
		{
			get
			{
				return this.playbackState;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x0600099D RID: 2461 RVA: 0x0001B91B File Offset: 0x00019B1B
		// (set) Token: 0x0600099E RID: 2462 RVA: 0x0001B923 File Offset: 0x00019B23
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

		// Token: 0x0600099F RID: 2463 RVA: 0x0001B940 File Offset: 0x00019B40
		internal static void SetWaveOutVolume(float value, IntPtr hWaveOut, object lockObject)
		{
			if (value < 0f)
			{
				throw new ArgumentOutOfRangeException("value", "Volume must be between 0.0 and 1.0");
			}
			if (value > 1f)
			{
				throw new ArgumentOutOfRangeException("value", "Volume must be between 0.0 and 1.0");
			}
			int num = (int)(value * 65535f) + ((int)(value * 65535f) << 16);
			MmResult mmResult;
			lock (lockObject)
			{
				mmResult = WaveInterop.waveOutSetVolume(hWaveOut, num);
			}
			MmException.Try(mmResult, "waveOutSetVolume");
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x0001B9CC File Offset: 0x00019BCC
		public void Dispose()
		{
			GC.SuppressFinalize(this);
			this.Dispose(true);
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x0001B9DC File Offset: 0x00019BDC
		protected void Dispose(bool disposing)
		{
			this.Stop();
			if (disposing && this.buffers != null)
			{
				for (int i = 0; i < this.buffers.Length; i++)
				{
					if (this.buffers[i] != null)
					{
						this.buffers[i].Dispose();
					}
				}
				this.buffers = null;
			}
			lock (this.waveOutLock)
			{
				WaveInterop.waveOutClose(this.hWaveOut);
			}
			if (disposing)
			{
				this.callbackInfo.Disconnect();
			}
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x0001BA6C File Offset: 0x00019C6C
		~WaveOut()
		{
			this.Dispose(false);
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0001BA9C File Offset: 0x00019C9C
		private void Callback(IntPtr hWaveOut, WaveInterop.WaveMessage uMsg, IntPtr dwInstance, WaveHeader wavhdr, IntPtr dwReserved)
		{
			if (uMsg == WaveInterop.WaveMessage.WaveOutDone)
			{
				WaveOutBuffer waveOutBuffer = (WaveOutBuffer)((GCHandle)wavhdr.userData).Target;
				Interlocked.Decrement(ref this.queuedBuffers);
				Exception ex = null;
				if (this.PlaybackState == PlaybackState.Playing)
				{
					lock (this.waveOutLock)
					{
						try
						{
							if (waveOutBuffer.OnDone())
							{
								Interlocked.Increment(ref this.queuedBuffers);
							}
						}
						catch (Exception ex2)
						{
							ex = ex2;
						}
					}
				}
				if (this.queuedBuffers == 0)
				{
					if (this.callbackInfo.Strategy == WaveCallbackStrategy.FunctionCallback && this.playbackState == PlaybackState.Stopped)
					{
						return;
					}
					this.playbackState = PlaybackState.Stopped;
					this.RaisePlaybackStoppedEvent(ex);
				}
			}
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x0001BB8C File Offset: 0x00019D8C
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

		// Token: 0x04000AEF RID: 2799
		private IntPtr hWaveOut;

		// Token: 0x04000AF0 RID: 2800
		private WaveOutBuffer[] buffers;

		// Token: 0x04000AF1 RID: 2801
		private IWaveProvider waveStream;

		// Token: 0x04000AF2 RID: 2802
		private volatile PlaybackState playbackState;

		// Token: 0x04000AF3 RID: 2803
		private WaveInterop.WaveCallback callback;

		// Token: 0x04000AF4 RID: 2804
		private float volume = 1f;

		// Token: 0x04000AF5 RID: 2805
		private WaveCallbackInfo callbackInfo;

		// Token: 0x04000AF6 RID: 2806
		private object waveOutLock;

		// Token: 0x04000AF7 RID: 2807
		private int queuedBuffers;

		// Token: 0x04000AF8 RID: 2808
		private SynchronizationContext syncContext;
	}
}
