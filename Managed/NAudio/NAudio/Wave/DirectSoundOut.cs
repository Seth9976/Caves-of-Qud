using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace NAudio.Wave
{
	// Token: 0x020001AF RID: 431
	public class DirectSoundOut : IWavePlayer, IDisposable
	{
		// Token: 0x14000010 RID: 16
		// (add) Token: 0x0600090A RID: 2314 RVA: 0x00019F04 File Offset: 0x00018104
		// (remove) Token: 0x0600090B RID: 2315 RVA: 0x00019F3C File Offset: 0x0001813C
		public event EventHandler<StoppedEventArgs> PlaybackStopped;

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x0600090C RID: 2316 RVA: 0x00019F71 File Offset: 0x00018171
		public static IEnumerable<DirectSoundDeviceInfo> Devices
		{
			get
			{
				DirectSoundOut.devices = new List<DirectSoundDeviceInfo>();
				DirectSoundOut.DirectSoundEnumerate(new DirectSoundOut.DSEnumCallback(DirectSoundOut.EnumCallback), IntPtr.Zero);
				return DirectSoundOut.devices;
			}
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x00019F98 File Offset: 0x00018198
		private static bool EnumCallback(IntPtr lpGuid, IntPtr lpcstrDescription, IntPtr lpcstrModule, IntPtr lpContext)
		{
			DirectSoundDeviceInfo directSoundDeviceInfo = new DirectSoundDeviceInfo();
			if (lpGuid == IntPtr.Zero)
			{
				directSoundDeviceInfo.Guid = Guid.Empty;
			}
			else
			{
				byte[] array = new byte[16];
				Marshal.Copy(lpGuid, array, 0, 16);
				directSoundDeviceInfo.Guid = new Guid(array);
			}
			directSoundDeviceInfo.Description = Marshal.PtrToStringAnsi(lpcstrDescription);
			directSoundDeviceInfo.ModuleName = Marshal.PtrToStringAnsi(lpcstrModule);
			DirectSoundOut.devices.Add(directSoundDeviceInfo);
			return true;
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x0001A007 File Offset: 0x00018207
		public DirectSoundOut()
			: this(DirectSoundOut.DSDEVID_DefaultPlayback)
		{
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x0001A014 File Offset: 0x00018214
		public DirectSoundOut(Guid device)
			: this(device, 40)
		{
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x0001A01F File Offset: 0x0001821F
		public DirectSoundOut(int latency)
			: this(DirectSoundOut.DSDEVID_DefaultPlayback, latency)
		{
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x0001A02D File Offset: 0x0001822D
		public DirectSoundOut(Guid device, int latency)
		{
			if (device == Guid.Empty)
			{
				device = DirectSoundOut.DSDEVID_DefaultPlayback;
			}
			this.device = device;
			this.desiredLatency = latency;
			this.syncContext = SynchronizationContext.Current;
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x0001A070 File Offset: 0x00018270
		~DirectSoundOut()
		{
			this.Dispose();
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x0001A09C File Offset: 0x0001829C
		public void Play()
		{
			if (this.playbackState == PlaybackState.Stopped)
			{
				this.notifyThread = new Thread(new ThreadStart(this.PlaybackThreadFunc));
				this.notifyThread.Priority = 2;
				this.notifyThread.IsBackground = true;
				this.notifyThread.Start();
			}
			lock (this.m_LockObject)
			{
				this.playbackState = PlaybackState.Playing;
			}
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x0001A118 File Offset: 0x00018318
		public void Stop()
		{
			if (Monitor.TryEnter(this.m_LockObject, 50))
			{
				this.playbackState = PlaybackState.Stopped;
				Monitor.Exit(this.m_LockObject);
				return;
			}
			if (this.notifyThread != null)
			{
				this.notifyThread.Abort();
				this.notifyThread = null;
			}
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x0001A158 File Offset: 0x00018358
		public void Pause()
		{
			lock (this.m_LockObject)
			{
				this.playbackState = PlaybackState.Paused;
			}
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x0001A194 File Offset: 0x00018394
		public long GetPosition()
		{
			if (this.playbackState != PlaybackState.Stopped)
			{
				DirectSoundOut.IDirectSoundBuffer directSoundBuffer = this.secondaryBuffer;
				if (directSoundBuffer != null)
				{
					uint num;
					uint num2;
					directSoundBuffer.GetCurrentPosition(out num, out num2);
					return (long)((ulong)num + (ulong)this.bytesPlayed);
				}
			}
			return 0L;
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x0001A1CC File Offset: 0x000183CC
		public TimeSpan PlaybackPosition
		{
			get
			{
				long num = this.GetPosition();
				num /= (long)(this.waveFormat.Channels * this.waveFormat.BitsPerSample / 8);
				return TimeSpan.FromMilliseconds((double)num * 1000.0 / (double)this.waveFormat.SampleRate);
			}
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x0001A21B File Offset: 0x0001841B
		public void Init(IWaveProvider waveProvider)
		{
			this.waveStream = waveProvider;
			this.waveFormat = waveProvider.WaveFormat;
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x0001A230 File Offset: 0x00018430
		private void InitializeDirectSound()
		{
			lock (this.m_LockObject)
			{
				this.directSound = null;
				DirectSoundOut.DirectSoundCreate(ref this.device, out this.directSound, IntPtr.Zero);
				if (this.directSound != null)
				{
					this.directSound.SetCooperativeLevel(DirectSoundOut.GetDesktopWindow(), DirectSoundOut.DirectSoundCooperativeLevel.DSSCL_PRIORITY);
					DirectSoundOut.BufferDescription bufferDescription = new DirectSoundOut.BufferDescription();
					bufferDescription.dwSize = Marshal.SizeOf(bufferDescription);
					bufferDescription.dwBufferBytes = 0U;
					bufferDescription.dwFlags = DirectSoundOut.DirectSoundBufferCaps.DSBCAPS_PRIMARYBUFFER;
					bufferDescription.dwReserved = 0;
					bufferDescription.lpwfxFormat = IntPtr.Zero;
					bufferDescription.guidAlgo = Guid.Empty;
					object obj;
					this.directSound.CreateSoundBuffer(bufferDescription, out obj, IntPtr.Zero);
					this.primarySoundBuffer = (DirectSoundOut.IDirectSoundBuffer)obj;
					this.primarySoundBuffer.Play(0U, 0U, DirectSoundOut.DirectSoundPlayFlags.DSBPLAY_LOOPING);
					this.samplesFrameSize = this.MsToBytes(this.desiredLatency);
					DirectSoundOut.BufferDescription bufferDescription2 = new DirectSoundOut.BufferDescription();
					bufferDescription2.dwSize = Marshal.SizeOf(bufferDescription2);
					bufferDescription2.dwBufferBytes = (uint)(this.samplesFrameSize * 2);
					bufferDescription2.dwFlags = DirectSoundOut.DirectSoundBufferCaps.DSBCAPS_CTRLVOLUME | DirectSoundOut.DirectSoundBufferCaps.DSBCAPS_CTRLPOSITIONNOTIFY | DirectSoundOut.DirectSoundBufferCaps.DSBCAPS_STICKYFOCUS | DirectSoundOut.DirectSoundBufferCaps.DSBCAPS_GLOBALFOCUS | DirectSoundOut.DirectSoundBufferCaps.DSBCAPS_GETCURRENTPOSITION2;
					bufferDescription2.dwReserved = 0;
					GCHandle gchandle = GCHandle.Alloc(this.waveFormat, 3);
					bufferDescription2.lpwfxFormat = gchandle.AddrOfPinnedObject();
					bufferDescription2.guidAlgo = Guid.Empty;
					this.directSound.CreateSoundBuffer(bufferDescription2, out obj, IntPtr.Zero);
					this.secondaryBuffer = (DirectSoundOut.IDirectSoundBuffer)obj;
					gchandle.Free();
					DirectSoundOut.BufferCaps bufferCaps = new DirectSoundOut.BufferCaps();
					bufferCaps.dwSize = Marshal.SizeOf(bufferCaps);
					this.secondaryBuffer.GetCaps(bufferCaps);
					this.nextSamplesWriteIndex = 0;
					this.samplesTotalSize = bufferCaps.dwBufferBytes;
					this.samples = new byte[this.samplesTotalSize];
					DirectSoundOut.IDirectSoundNotify directSoundNotify = (DirectSoundOut.IDirectSoundNotify)obj;
					this.frameEventWaitHandle1 = new EventWaitHandle(false, 0);
					this.frameEventWaitHandle2 = new EventWaitHandle(false, 0);
					this.endEventWaitHandle = new EventWaitHandle(false, 0);
					DirectSoundOut.DirectSoundBufferPositionNotify[] array = new DirectSoundOut.DirectSoundBufferPositionNotify[3];
					array[0] = default(DirectSoundOut.DirectSoundBufferPositionNotify);
					array[0].dwOffset = 0U;
					array[0].hEventNotify = this.frameEventWaitHandle1.SafeWaitHandle.DangerousGetHandle();
					array[1] = default(DirectSoundOut.DirectSoundBufferPositionNotify);
					array[1].dwOffset = (uint)this.samplesFrameSize;
					array[1].hEventNotify = this.frameEventWaitHandle2.SafeWaitHandle.DangerousGetHandle();
					array[2] = default(DirectSoundOut.DirectSoundBufferPositionNotify);
					array[2].dwOffset = uint.MaxValue;
					array[2].hEventNotify = this.endEventWaitHandle.SafeWaitHandle.DangerousGetHandle();
					directSoundNotify.SetNotificationPositions(3U, array);
				}
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x0001A4DC File Offset: 0x000186DC
		public PlaybackState PlaybackState
		{
			get
			{
				return this.playbackState;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x0001A4E4 File Offset: 0x000186E4
		// (set) Token: 0x0600091C RID: 2332 RVA: 0x0001A4EB File Offset: 0x000186EB
		public float Volume
		{
			get
			{
				return 1f;
			}
			set
			{
				if (value != 1f)
				{
					throw new InvalidOperationException("Setting volume not supported on DirectSoundOut, adjust the volume on your WaveProvider instead");
				}
			}
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x0001A500 File Offset: 0x00018700
		public void Dispose()
		{
			this.Stop();
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0001A50E File Offset: 0x0001870E
		private bool IsBufferLost()
		{
			return (this.secondaryBuffer.GetStatus() & DirectSoundOut.DirectSoundBufferStatus.DSBSTATUS_BUFFERLOST) != (DirectSoundOut.DirectSoundBufferStatus)0U;
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0001A524 File Offset: 0x00018724
		private int MsToBytes(int ms)
		{
			int num = ms * (this.waveFormat.AverageBytesPerSecond / 1000);
			return num - num % this.waveFormat.BlockAlign;
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0001A558 File Offset: 0x00018758
		private void PlaybackThreadFunc()
		{
			bool flag = false;
			bool flag2 = false;
			this.bytesPlayed = 0L;
			Exception ex = null;
			try
			{
				this.InitializeDirectSound();
				int num = 1;
				if (this.PlaybackState == PlaybackState.Stopped)
				{
					this.secondaryBuffer.SetCurrentPosition(0U);
					this.nextSamplesWriteIndex = 0;
					num = this.Feed(this.samplesTotalSize);
				}
				if (num > 0)
				{
					lock (this.m_LockObject)
					{
						this.playbackState = PlaybackState.Playing;
					}
					this.secondaryBuffer.Play(0U, 0U, DirectSoundOut.DirectSoundPlayFlags.DSBPLAY_LOOPING);
					WaitHandle[] array = new WaitHandle[] { this.frameEventWaitHandle1, this.frameEventWaitHandle2, this.endEventWaitHandle };
					bool flag3 = true;
					while (this.PlaybackState != PlaybackState.Stopped && flag3)
					{
						int num2 = WaitHandle.WaitAny(array, 3 * this.desiredLatency, false);
						if (num2 == 258)
						{
							this.StopPlayback();
							flag = true;
							throw new Exception("DirectSound buffer timeout");
						}
						if (num2 == 2)
						{
							this.StopPlayback();
							flag = true;
							flag3 = false;
						}
						else
						{
							if (num2 == 0)
							{
								if (flag2)
								{
									this.bytesPlayed += (long)(this.samplesFrameSize * 2);
								}
							}
							else
							{
								flag2 = true;
							}
							num2 = ((num2 == 0) ? 1 : 0);
							this.nextSamplesWriteIndex = num2 * this.samplesFrameSize;
							if (this.Feed(this.samplesFrameSize) == 0)
							{
								this.StopPlayback();
								flag = true;
								flag3 = false;
							}
						}
					}
				}
			}
			catch (Exception ex2)
			{
				ex = ex2;
			}
			finally
			{
				if (!flag)
				{
					try
					{
						this.StopPlayback();
					}
					catch (Exception ex3)
					{
						if (ex == null)
						{
							ex = ex3;
						}
					}
				}
				lock (this.m_LockObject)
				{
					this.playbackState = PlaybackState.Stopped;
				}
				this.bytesPlayed = 0L;
				this.RaisePlaybackStopped(ex);
			}
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0001A7A4 File Offset: 0x000189A4
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

		// Token: 0x06000922 RID: 2338 RVA: 0x0001A818 File Offset: 0x00018A18
		private void StopPlayback()
		{
			lock (this.m_LockObject)
			{
				if (this.secondaryBuffer != null)
				{
					this.secondaryBuffer.Stop();
					this.secondaryBuffer = null;
				}
				if (this.primarySoundBuffer != null)
				{
					this.primarySoundBuffer.Stop();
					this.primarySoundBuffer = null;
				}
			}
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0001A880 File Offset: 0x00018A80
		private int Feed(int bytesToCopy)
		{
			int num = bytesToCopy;
			if (this.IsBufferLost())
			{
				this.secondaryBuffer.Restore();
			}
			if (this.playbackState == PlaybackState.Paused)
			{
				Array.Clear(this.samples, 0, this.samples.Length);
			}
			else
			{
				num = this.waveStream.Read(this.samples, 0, bytesToCopy);
				if (num == 0)
				{
					Array.Clear(this.samples, 0, this.samples.Length);
					return 0;
				}
			}
			IntPtr intPtr;
			int num2;
			IntPtr intPtr2;
			int num3;
			this.secondaryBuffer.Lock(this.nextSamplesWriteIndex, (uint)num, out intPtr, out num2, out intPtr2, out num3, DirectSoundOut.DirectSoundBufferLockFlag.None);
			if (intPtr != IntPtr.Zero)
			{
				Marshal.Copy(this.samples, 0, intPtr, num2);
				if (intPtr2 != IntPtr.Zero)
				{
					Marshal.Copy(this.samples, 0, intPtr, num2);
				}
			}
			this.secondaryBuffer.Unlock(intPtr, num2, intPtr2, num3);
			return num;
		}

		// Token: 0x06000924 RID: 2340
		[DllImport("dsound.dll", CallingConvention = 3, CharSet = 3, ExactSpelling = true, SetLastError = true)]
		private static extern void DirectSoundCreate(ref Guid GUID, [MarshalAs(28)] out DirectSoundOut.IDirectSound directSound, IntPtr pUnkOuter);

		// Token: 0x06000925 RID: 2341
		[DllImport("dsound.dll", CallingConvention = 3, CharSet = 3, EntryPoint = "DirectSoundEnumerateA", ExactSpelling = true, SetLastError = true)]
		private static extern void DirectSoundEnumerate(DirectSoundOut.DSEnumCallback lpDSEnumCallback, IntPtr lpContext);

		// Token: 0x06000926 RID: 2342
		[DllImport("user32.dll")]
		private static extern IntPtr GetDesktopWindow();

		// Token: 0x04000A7F RID: 2687
		private PlaybackState playbackState;

		// Token: 0x04000A80 RID: 2688
		private WaveFormat waveFormat;

		// Token: 0x04000A81 RID: 2689
		private int samplesTotalSize;

		// Token: 0x04000A82 RID: 2690
		private int samplesFrameSize;

		// Token: 0x04000A83 RID: 2691
		private int nextSamplesWriteIndex;

		// Token: 0x04000A84 RID: 2692
		private int desiredLatency;

		// Token: 0x04000A85 RID: 2693
		private Guid device;

		// Token: 0x04000A86 RID: 2694
		private byte[] samples;

		// Token: 0x04000A87 RID: 2695
		private IWaveProvider waveStream;

		// Token: 0x04000A88 RID: 2696
		private DirectSoundOut.IDirectSound directSound;

		// Token: 0x04000A89 RID: 2697
		private DirectSoundOut.IDirectSoundBuffer primarySoundBuffer;

		// Token: 0x04000A8A RID: 2698
		private DirectSoundOut.IDirectSoundBuffer secondaryBuffer;

		// Token: 0x04000A8B RID: 2699
		private EventWaitHandle frameEventWaitHandle1;

		// Token: 0x04000A8C RID: 2700
		private EventWaitHandle frameEventWaitHandle2;

		// Token: 0x04000A8D RID: 2701
		private EventWaitHandle endEventWaitHandle;

		// Token: 0x04000A8E RID: 2702
		private Thread notifyThread;

		// Token: 0x04000A8F RID: 2703
		private SynchronizationContext syncContext;

		// Token: 0x04000A90 RID: 2704
		private long bytesPlayed;

		// Token: 0x04000A91 RID: 2705
		private object m_LockObject = new object();

		// Token: 0x04000A92 RID: 2706
		private static List<DirectSoundDeviceInfo> devices;

		// Token: 0x04000A93 RID: 2707
		public static readonly Guid DSDEVID_DefaultPlayback = new Guid("DEF00000-9C6D-47ED-AAF1-4DDA8F2B5C03");

		// Token: 0x04000A94 RID: 2708
		public static readonly Guid DSDEVID_DefaultCapture = new Guid("DEF00001-9C6D-47ED-AAF1-4DDA8F2B5C03");

		// Token: 0x04000A95 RID: 2709
		public static readonly Guid DSDEVID_DefaultVoicePlayback = new Guid("DEF00002-9C6D-47ED-AAF1-4DDA8F2B5C03");

		// Token: 0x04000A96 RID: 2710
		public static readonly Guid DSDEVID_DefaultVoiceCapture = new Guid("DEF00003-9C6D-47ED-AAF1-4DDA8F2B5C03");

		// Token: 0x020001B0 RID: 432
		[StructLayout(0, Pack = 2)]
		internal class BufferDescription
		{
			// Token: 0x04000A97 RID: 2711
			public int dwSize;

			// Token: 0x04000A98 RID: 2712
			[MarshalAs(8)]
			public DirectSoundOut.DirectSoundBufferCaps dwFlags;

			// Token: 0x04000A99 RID: 2713
			public uint dwBufferBytes;

			// Token: 0x04000A9A RID: 2714
			public int dwReserved;

			// Token: 0x04000A9B RID: 2715
			public IntPtr lpwfxFormat;

			// Token: 0x04000A9C RID: 2716
			public Guid guidAlgo;
		}

		// Token: 0x020001B1 RID: 433
		[StructLayout(0, Pack = 2)]
		internal class BufferCaps
		{
			// Token: 0x04000A9D RID: 2717
			public int dwSize;

			// Token: 0x04000A9E RID: 2718
			public int dwFlags;

			// Token: 0x04000A9F RID: 2719
			public int dwBufferBytes;

			// Token: 0x04000AA0 RID: 2720
			public int dwUnlockTransferRate;

			// Token: 0x04000AA1 RID: 2721
			public int dwPlayCpuOverhead;
		}

		// Token: 0x020001B2 RID: 434
		internal enum DirectSoundCooperativeLevel : uint
		{
			// Token: 0x04000AA3 RID: 2723
			DSSCL_NORMAL = 1U,
			// Token: 0x04000AA4 RID: 2724
			DSSCL_PRIORITY,
			// Token: 0x04000AA5 RID: 2725
			DSSCL_EXCLUSIVE,
			// Token: 0x04000AA6 RID: 2726
			DSSCL_WRITEPRIMARY
		}

		// Token: 0x020001B3 RID: 435
		[Flags]
		internal enum DirectSoundPlayFlags : uint
		{
			// Token: 0x04000AA8 RID: 2728
			DSBPLAY_LOOPING = 1U,
			// Token: 0x04000AA9 RID: 2729
			DSBPLAY_LOCHARDWARE = 2U,
			// Token: 0x04000AAA RID: 2730
			DSBPLAY_LOCSOFTWARE = 4U,
			// Token: 0x04000AAB RID: 2731
			DSBPLAY_TERMINATEBY_TIME = 8U,
			// Token: 0x04000AAC RID: 2732
			DSBPLAY_TERMINATEBY_DISTANCE = 16U,
			// Token: 0x04000AAD RID: 2733
			DSBPLAY_TERMINATEBY_PRIORITY = 32U
		}

		// Token: 0x020001B4 RID: 436
		internal enum DirectSoundBufferLockFlag : uint
		{
			// Token: 0x04000AAF RID: 2735
			None,
			// Token: 0x04000AB0 RID: 2736
			FromWriteCursor,
			// Token: 0x04000AB1 RID: 2737
			EntireBuffer
		}

		// Token: 0x020001B5 RID: 437
		[Flags]
		internal enum DirectSoundBufferStatus : uint
		{
			// Token: 0x04000AB3 RID: 2739
			DSBSTATUS_PLAYING = 1U,
			// Token: 0x04000AB4 RID: 2740
			DSBSTATUS_BUFFERLOST = 2U,
			// Token: 0x04000AB5 RID: 2741
			DSBSTATUS_LOOPING = 4U,
			// Token: 0x04000AB6 RID: 2742
			DSBSTATUS_LOCHARDWARE = 8U,
			// Token: 0x04000AB7 RID: 2743
			DSBSTATUS_LOCSOFTWARE = 16U,
			// Token: 0x04000AB8 RID: 2744
			DSBSTATUS_TERMINATED = 32U
		}

		// Token: 0x020001B6 RID: 438
		[Flags]
		internal enum DirectSoundBufferCaps : uint
		{
			// Token: 0x04000ABA RID: 2746
			DSBCAPS_PRIMARYBUFFER = 1U,
			// Token: 0x04000ABB RID: 2747
			DSBCAPS_STATIC = 2U,
			// Token: 0x04000ABC RID: 2748
			DSBCAPS_LOCHARDWARE = 4U,
			// Token: 0x04000ABD RID: 2749
			DSBCAPS_LOCSOFTWARE = 8U,
			// Token: 0x04000ABE RID: 2750
			DSBCAPS_CTRL3D = 16U,
			// Token: 0x04000ABF RID: 2751
			DSBCAPS_CTRLFREQUENCY = 32U,
			// Token: 0x04000AC0 RID: 2752
			DSBCAPS_CTRLPAN = 64U,
			// Token: 0x04000AC1 RID: 2753
			DSBCAPS_CTRLVOLUME = 128U,
			// Token: 0x04000AC2 RID: 2754
			DSBCAPS_CTRLPOSITIONNOTIFY = 256U,
			// Token: 0x04000AC3 RID: 2755
			DSBCAPS_CTRLFX = 512U,
			// Token: 0x04000AC4 RID: 2756
			DSBCAPS_STICKYFOCUS = 16384U,
			// Token: 0x04000AC5 RID: 2757
			DSBCAPS_GLOBALFOCUS = 32768U,
			// Token: 0x04000AC6 RID: 2758
			DSBCAPS_GETCURRENTPOSITION2 = 65536U,
			// Token: 0x04000AC7 RID: 2759
			DSBCAPS_MUTE3DATMAXDISTANCE = 131072U,
			// Token: 0x04000AC8 RID: 2760
			DSBCAPS_LOCDEFER = 262144U
		}

		// Token: 0x020001B7 RID: 439
		internal struct DirectSoundBufferPositionNotify
		{
			// Token: 0x04000AC9 RID: 2761
			public uint dwOffset;

			// Token: 0x04000ACA RID: 2762
			public IntPtr hEventNotify;
		}

		// Token: 0x020001B8 RID: 440
		[SuppressUnmanagedCodeSecurity]
		[Guid("279AFA83-4981-11CE-A521-0020AF0BE560")]
		[InterfaceType(1)]
		[ComImport]
		internal interface IDirectSound
		{
			// Token: 0x0600092A RID: 2346
			void CreateSoundBuffer([In] DirectSoundOut.BufferDescription desc, [MarshalAs(28)] out object dsDSoundBuffer, IntPtr pUnkOuter);

			// Token: 0x0600092B RID: 2347
			void GetCaps(IntPtr caps);

			// Token: 0x0600092C RID: 2348
			void DuplicateSoundBuffer([MarshalAs(28)] [In] DirectSoundOut.IDirectSoundBuffer bufferOriginal, [MarshalAs(28)] [In] DirectSoundOut.IDirectSoundBuffer bufferDuplicate);

			// Token: 0x0600092D RID: 2349
			void SetCooperativeLevel(IntPtr HWND, [MarshalAs(8)] [In] DirectSoundOut.DirectSoundCooperativeLevel dwLevel);

			// Token: 0x0600092E RID: 2350
			void Compact();

			// Token: 0x0600092F RID: 2351
			void GetSpeakerConfig(IntPtr pdwSpeakerConfig);

			// Token: 0x06000930 RID: 2352
			void SetSpeakerConfig(uint pdwSpeakerConfig);

			// Token: 0x06000931 RID: 2353
			void Initialize([MarshalAs(43)] [In] Guid guid);
		}

		// Token: 0x020001B9 RID: 441
		[SuppressUnmanagedCodeSecurity]
		[Guid("279AFA85-4981-11CE-A521-0020AF0BE560")]
		[InterfaceType(1)]
		[ComImport]
		internal interface IDirectSoundBuffer
		{
			// Token: 0x06000932 RID: 2354
			void GetCaps([MarshalAs(43)] DirectSoundOut.BufferCaps pBufferCaps);

			// Token: 0x06000933 RID: 2355
			void GetCurrentPosition(out uint currentPlayCursor, out uint currentWriteCursor);

			// Token: 0x06000934 RID: 2356
			void GetFormat();

			// Token: 0x06000935 RID: 2357
			[return: MarshalAs(7)]
			int GetVolume();

			// Token: 0x06000936 RID: 2358
			void GetPan(out uint pan);

			// Token: 0x06000937 RID: 2359
			[return: MarshalAs(7)]
			int GetFrequency();

			// Token: 0x06000938 RID: 2360
			[return: MarshalAs(8)]
			DirectSoundOut.DirectSoundBufferStatus GetStatus();

			// Token: 0x06000939 RID: 2361
			void Initialize([MarshalAs(28)] [In] DirectSoundOut.IDirectSound directSound, [In] DirectSoundOut.BufferDescription desc);

			// Token: 0x0600093A RID: 2362
			void Lock(int dwOffset, uint dwBytes, out IntPtr audioPtr1, out int audioBytes1, out IntPtr audioPtr2, out int audioBytes2, [MarshalAs(8)] DirectSoundOut.DirectSoundBufferLockFlag dwFlags);

			// Token: 0x0600093B RID: 2363
			void Play(uint dwReserved1, uint dwPriority, [MarshalAs(8)] [In] DirectSoundOut.DirectSoundPlayFlags dwFlags);

			// Token: 0x0600093C RID: 2364
			void SetCurrentPosition(uint dwNewPosition);

			// Token: 0x0600093D RID: 2365
			void SetFormat([In] WaveFormat pcfxFormat);

			// Token: 0x0600093E RID: 2366
			void SetVolume(int volume);

			// Token: 0x0600093F RID: 2367
			void SetPan(uint pan);

			// Token: 0x06000940 RID: 2368
			void SetFrequency(uint frequency);

			// Token: 0x06000941 RID: 2369
			void Stop();

			// Token: 0x06000942 RID: 2370
			void Unlock(IntPtr pvAudioPtr1, int dwAudioBytes1, IntPtr pvAudioPtr2, int dwAudioBytes2);

			// Token: 0x06000943 RID: 2371
			void Restore();
		}

		// Token: 0x020001BA RID: 442
		[InterfaceType(1)]
		[SuppressUnmanagedCodeSecurity]
		[Guid("b0210783-89cd-11d0-af08-00a0c925cd16")]
		[ComImport]
		internal interface IDirectSoundNotify
		{
			// Token: 0x06000944 RID: 2372
			void SetNotificationPositions(uint dwPositionNotifies, [MarshalAs(42)] [In] DirectSoundOut.DirectSoundBufferPositionNotify[] pcPositionNotifies);
		}

		// Token: 0x020001BB RID: 443
		// (Invoke) Token: 0x06000946 RID: 2374
		private delegate bool DSEnumCallback(IntPtr lpGuid, IntPtr lpcstrDescription, IntPtr lpcstrModule, IntPtr lpContext);
	}
}
