using System;
using System.Threading;
using NAudio.Wave.Asio;

namespace NAudio.Wave
{
	// Token: 0x020001A7 RID: 423
	public class AsioOut : IWavePlayer, IDisposable
	{
		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06000890 RID: 2192 RVA: 0x000188D4 File Offset: 0x00016AD4
		// (remove) Token: 0x06000891 RID: 2193 RVA: 0x0001890C File Offset: 0x00016B0C
		public event EventHandler<StoppedEventArgs> PlaybackStopped;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000892 RID: 2194 RVA: 0x00018944 File Offset: 0x00016B44
		// (remove) Token: 0x06000893 RID: 2195 RVA: 0x0001897C File Offset: 0x00016B7C
		public event EventHandler<AsioAudioAvailableEventArgs> AudioAvailable;

		// Token: 0x06000894 RID: 2196 RVA: 0x000189B1 File Offset: 0x00016BB1
		public AsioOut()
			: this(0)
		{
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x000189BA File Offset: 0x00016BBA
		public AsioOut(string driverName)
		{
			this.syncContext = SynchronizationContext.Current;
			this.InitFromName(driverName);
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x000189D4 File Offset: 0x00016BD4
		public AsioOut(int driverIndex)
		{
			this.syncContext = SynchronizationContext.Current;
			string[] driverNames = AsioOut.GetDriverNames();
			if (driverNames.Length == 0)
			{
				throw new ArgumentException("There is no ASIO Driver installed on your system");
			}
			if (driverIndex < 0 || driverIndex > driverNames.Length)
			{
				throw new ArgumentException(string.Format("Invalid device number. Must be in the range [0,{0}]", driverNames.Length));
			}
			this.driverName = driverNames[driverIndex];
			this.InitFromName(this.driverName);
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x00018A40 File Offset: 0x00016C40
		~AsioOut()
		{
			this.Dispose();
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x00018A6C File Offset: 0x00016C6C
		public void Dispose()
		{
			if (this.driver != null)
			{
				if (this.playbackState != PlaybackState.Stopped)
				{
					this.driver.Stop();
				}
				this.driver.ReleaseDriver();
				this.driver = null;
			}
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x00018A9B File Offset: 0x00016C9B
		public static string[] GetDriverNames()
		{
			return ASIODriver.GetASIODriverNames();
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x00018AA2 File Offset: 0x00016CA2
		public static bool isSupported()
		{
			return AsioOut.GetDriverNames().Length > 0;
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x00018AB0 File Offset: 0x00016CB0
		private void InitFromName(string driverName)
		{
			ASIODriver asiodriverByName = ASIODriver.GetASIODriverByName(driverName);
			this.driver = new ASIODriverExt(asiodriverByName);
			this.ChannelOffset = 0;
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x00018AD7 File Offset: 0x00016CD7
		public void ShowControlPanel()
		{
			this.driver.ShowControlPanel();
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x00018AE4 File Offset: 0x00016CE4
		public void Play()
		{
			if (this.playbackState != PlaybackState.Playing)
			{
				this.playbackState = PlaybackState.Playing;
				this.driver.Start();
			}
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x00018B01 File Offset: 0x00016D01
		public void Stop()
		{
			this.playbackState = PlaybackState.Stopped;
			this.driver.Stop();
			this.RaisePlaybackStopped(null);
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x00018B1C File Offset: 0x00016D1C
		public void Pause()
		{
			this.playbackState = PlaybackState.Paused;
			this.driver.Stop();
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x00018B30 File Offset: 0x00016D30
		public void Init(IWaveProvider waveProvider)
		{
			this.InitRecordAndPlayback(waveProvider, 0, -1);
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x00018B3C File Offset: 0x00016D3C
		public void InitRecordAndPlayback(IWaveProvider waveProvider, int recordChannels, int recordOnlySampleRate)
		{
			if (this.sourceStream != null)
			{
				throw new InvalidOperationException("Already initialised this instance of AsioOut - dispose and create a new one");
			}
			int num = ((waveProvider != null) ? waveProvider.WaveFormat.SampleRate : recordOnlySampleRate);
			if (waveProvider != null)
			{
				this.sourceStream = waveProvider;
				this.NumberOfOutputChannels = waveProvider.WaveFormat.Channels;
				this.convertor = ASIOSampleConvertor.SelectSampleConvertor(waveProvider.WaveFormat, this.driver.Capabilities.OutputChannelInfos[0].type);
			}
			else
			{
				this.NumberOfOutputChannels = 0;
			}
			if (!this.driver.IsSampleRateSupported((double)num))
			{
				throw new ArgumentException("SampleRate is not supported");
			}
			if (this.driver.Capabilities.SampleRate != (double)num)
			{
				this.driver.SetSampleRate((double)num);
			}
			this.driver.FillBufferCallback = new ASIOFillBufferCallback(this.driver_BufferUpdate);
			this.NumberOfInputChannels = recordChannels;
			this.nbSamples = this.driver.CreateBuffers(this.NumberOfOutputChannels, this.NumberOfInputChannels, false);
			this.driver.SetChannelOffset(this.ChannelOffset, this.InputChannelOffset);
			if (waveProvider != null)
			{
				this.waveBuffer = new byte[this.nbSamples * this.NumberOfOutputChannels * waveProvider.WaveFormat.BitsPerSample / 8];
			}
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x00018C78 File Offset: 0x00016E78
		private unsafe void driver_BufferUpdate(IntPtr[] inputChannels, IntPtr[] outputChannels)
		{
			if (this.NumberOfInputChannels > 0)
			{
				EventHandler<AsioAudioAvailableEventArgs> audioAvailable = this.AudioAvailable;
				if (audioAvailable != null)
				{
					AsioAudioAvailableEventArgs e = new AsioAudioAvailableEventArgs(inputChannels, outputChannels, this.nbSamples, this.driver.Capabilities.InputChannelInfos[0].type);
					audioAvailable.Invoke(this, e);
					if (e.WrittenToOutputBuffers)
					{
						return;
					}
				}
			}
			if (this.NumberOfOutputChannels > 0)
			{
				int num = this.sourceStream.Read(this.waveBuffer, 0, this.waveBuffer.Length);
				int num2 = this.waveBuffer.Length;
				fixed (IntPtr* ptr = (IntPtr*)(&this.waveBuffer[0]))
				{
					this.convertor(new IntPtr((void*)ptr), outputChannels, this.NumberOfOutputChannels, this.nbSamples);
				}
				if (num == 0)
				{
					this.Stop();
				}
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060008A3 RID: 2211 RVA: 0x00018D3C File Offset: 0x00016F3C
		public int PlaybackLatency
		{
			get
			{
				int num;
				int num2;
				this.driver.Driver.GetLatencies(out num, out num2);
				return num2;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060008A4 RID: 2212 RVA: 0x00018D5F File Offset: 0x00016F5F
		public PlaybackState PlaybackState
		{
			get
			{
				return this.playbackState;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060008A5 RID: 2213 RVA: 0x00018D67 File Offset: 0x00016F67
		public string DriverName
		{
			get
			{
				return this.driverName;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060008A6 RID: 2214 RVA: 0x00018D6F File Offset: 0x00016F6F
		// (set) Token: 0x060008A7 RID: 2215 RVA: 0x00018D77 File Offset: 0x00016F77
		public int NumberOfOutputChannels { get; private set; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060008A8 RID: 2216 RVA: 0x00018D80 File Offset: 0x00016F80
		// (set) Token: 0x060008A9 RID: 2217 RVA: 0x00018D88 File Offset: 0x00016F88
		public int NumberOfInputChannels { get; private set; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060008AA RID: 2218 RVA: 0x00018D91 File Offset: 0x00016F91
		public int DriverInputChannelCount
		{
			get
			{
				return this.driver.Capabilities.NbInputChannels;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060008AB RID: 2219 RVA: 0x00018DA3 File Offset: 0x00016FA3
		public int DriverOutputChannelCount
		{
			get
			{
				return this.driver.Capabilities.NbOutputChannels;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060008AC RID: 2220 RVA: 0x00018DB5 File Offset: 0x00016FB5
		// (set) Token: 0x060008AD RID: 2221 RVA: 0x00018DBD File Offset: 0x00016FBD
		public int ChannelOffset { get; set; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060008AE RID: 2222 RVA: 0x00018DC6 File Offset: 0x00016FC6
		// (set) Token: 0x060008AF RID: 2223 RVA: 0x00018DCE File Offset: 0x00016FCE
		public int InputChannelOffset { get; set; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060008B0 RID: 2224 RVA: 0x00018DD7 File Offset: 0x00016FD7
		// (set) Token: 0x060008B1 RID: 2225 RVA: 0x00018DDE File Offset: 0x00016FDE
		[Obsolete("this function will be removed in a future NAudio as ASIO does not support setting the volume on the device")]
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
					throw new InvalidOperationException("AsioOut does not support setting the device volume");
				}
			}
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x00018E1C File Offset: 0x0001701C
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

		// Token: 0x060008B3 RID: 2227 RVA: 0x00018E90 File Offset: 0x00017090
		public string AsioInputChannelName(int channel)
		{
			if (channel <= this.DriverInputChannelCount)
			{
				return this.driver.Capabilities.InputChannelInfos[channel].name;
			}
			return "";
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x00018EBC File Offset: 0x000170BC
		public string AsioOutputChannelName(int channel)
		{
			if (channel <= this.DriverOutputChannelCount)
			{
				return this.driver.Capabilities.OutputChannelInfos[channel].name;
			}
			return "";
		}

		// Token: 0x04000A5F RID: 2655
		private ASIODriverExt driver;

		// Token: 0x04000A60 RID: 2656
		private IWaveProvider sourceStream;

		// Token: 0x04000A61 RID: 2657
		private PlaybackState playbackState;

		// Token: 0x04000A62 RID: 2658
		private int nbSamples;

		// Token: 0x04000A63 RID: 2659
		private byte[] waveBuffer;

		// Token: 0x04000A64 RID: 2660
		private ASIOSampleConvertor.SampleConvertor convertor;

		// Token: 0x04000A65 RID: 2661
		private readonly string driverName;

		// Token: 0x04000A66 RID: 2662
		private readonly SynchronizationContext syncContext;
	}
}
