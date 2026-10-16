using System;

namespace NAudio.Wave.Asio
{
	// Token: 0x02000146 RID: 326
	internal class ASIODriverExt
	{
		// Token: 0x060006EB RID: 1771 RVA: 0x00014468 File Offset: 0x00012668
		public ASIODriverExt(ASIODriver driver)
		{
			this.driver = driver;
			if (!driver.init(IntPtr.Zero))
			{
				throw new InvalidOperationException(driver.getErrorMessage());
			}
			this.callbacks = default(ASIOCallbacks);
			this.callbacks.pasioMessage = new ASIOCallbacks.ASIOAsioMessageCallBack(this.AsioMessageCallBack);
			this.callbacks.pbufferSwitch = new ASIOCallbacks.ASIOBufferSwitchCallBack(this.BufferSwitchCallBack);
			this.callbacks.pbufferSwitchTimeInfo = new ASIOCallbacks.ASIOBufferSwitchTimeInfoCallBack(this.BufferSwitchTimeInfoCallBack);
			this.callbacks.psampleRateDidChange = new ASIOCallbacks.ASIOSampleRateDidChangeCallBack(this.SampleRateDidChangeCallBack);
			this.BuildCapabilities();
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0001450C File Offset: 0x0001270C
		public void SetChannelOffset(int outputChannelOffset, int inputChannelOffset)
		{
			if (outputChannelOffset + this.numberOfOutputChannels > this.Capabilities.NbOutputChannels)
			{
				throw new ArgumentException("Invalid channel offset");
			}
			this.outputChannelOffset = outputChannelOffset;
			if (inputChannelOffset + this.numberOfInputChannels <= this.Capabilities.NbInputChannels)
			{
				this.inputChannelOffset = inputChannelOffset;
				return;
			}
			throw new ArgumentException("Invalid channel offset");
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060006ED RID: 1773 RVA: 0x00014569 File Offset: 0x00012769
		public ASIODriver Driver
		{
			get
			{
				return this.driver;
			}
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x00014571 File Offset: 0x00012771
		public void Start()
		{
			this.driver.start();
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0001457E File Offset: 0x0001277E
		public void Stop()
		{
			this.driver.stop();
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x0001458C File Offset: 0x0001278C
		public void ShowControlPanel()
		{
			this.driver.controlPanel();
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x0001459C File Offset: 0x0001279C
		public void ReleaseDriver()
		{
			try
			{
				this.driver.disposeBuffers();
			}
			catch (Exception ex)
			{
				Console.Out.WriteLine(ex.ToString());
			}
			this.driver.ReleaseComASIODriver();
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x000145E8 File Offset: 0x000127E8
		public bool IsSampleRateSupported(double sampleRate)
		{
			return this.driver.canSampleRate(sampleRate);
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x000145F6 File Offset: 0x000127F6
		public void SetSampleRate(double sampleRate)
		{
			this.driver.setSampleRate(sampleRate);
			this.BuildCapabilities();
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x0001460A File Offset: 0x0001280A
		// (set) Token: 0x060006F5 RID: 1781 RVA: 0x00014612 File Offset: 0x00012812
		public ASIOFillBufferCallback FillBufferCallback
		{
			get
			{
				return this.fillBufferCallback;
			}
			set
			{
				this.fillBufferCallback = value;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x0001461B File Offset: 0x0001281B
		public AsioDriverCapability Capabilities
		{
			get
			{
				return this.capability;
			}
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x00014624 File Offset: 0x00012824
		public unsafe int CreateBuffers(int numberOfOutputChannels, int numberOfInputChannels, bool useMaxBufferSize)
		{
			if (numberOfOutputChannels < 0 || numberOfOutputChannels > this.capability.NbOutputChannels)
			{
				throw new ArgumentException(string.Format("Invalid number of channels {0}, must be in the range [0,{1}]", numberOfOutputChannels, this.capability.NbOutputChannels));
			}
			if (numberOfInputChannels < 0 || numberOfInputChannels > this.capability.NbInputChannels)
			{
				throw new ArgumentException("numberOfInputChannels", string.Format("Invalid number of input channels {0}, must be in the range [0,{1}]", numberOfInputChannels, this.capability.NbInputChannels));
			}
			this.numberOfOutputChannels = numberOfOutputChannels;
			this.numberOfInputChannels = numberOfInputChannels;
			int num = this.capability.NbInputChannels + this.capability.NbOutputChannels;
			this.bufferInfos = new ASIOBufferInfo[num];
			this.currentOutputBuffers = new IntPtr[numberOfOutputChannels];
			this.currentInputBuffers = new IntPtr[numberOfInputChannels];
			int num2 = 0;
			int i = 0;
			while (i < this.capability.NbInputChannels)
			{
				this.bufferInfos[num2].isInput = true;
				this.bufferInfos[num2].channelNum = i;
				this.bufferInfos[num2].pBuffer0 = IntPtr.Zero;
				this.bufferInfos[num2].pBuffer1 = IntPtr.Zero;
				i++;
				num2++;
			}
			int j = 0;
			while (j < this.capability.NbOutputChannels)
			{
				this.bufferInfos[num2].isInput = false;
				this.bufferInfos[num2].channelNum = j;
				this.bufferInfos[num2].pBuffer0 = IntPtr.Zero;
				this.bufferInfos[num2].pBuffer1 = IntPtr.Zero;
				j++;
				num2++;
			}
			if (useMaxBufferSize)
			{
				this.bufferSize = this.capability.BufferMaxSize;
			}
			else
			{
				this.bufferSize = this.capability.BufferPreferredSize;
			}
			fixed (ASIOBufferInfo* ptr = &this.bufferInfos[0])
			{
				IntPtr intPtr;
				intPtr..ctor((void*)ptr);
				this.driver.createBuffers(intPtr, num, this.bufferSize, ref this.callbacks);
			}
			this.isOutputReadySupported = this.driver.outputReady() == ASIOError.ASE_OK;
			return this.bufferSize;
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x00014840 File Offset: 0x00012A40
		private void BuildCapabilities()
		{
			this.capability = new AsioDriverCapability();
			this.capability.DriverName = this.driver.getDriverName();
			this.driver.getChannels(out this.capability.NbInputChannels, out this.capability.NbOutputChannels);
			this.capability.InputChannelInfos = new ASIOChannelInfo[this.capability.NbInputChannels];
			this.capability.OutputChannelInfos = new ASIOChannelInfo[this.capability.NbOutputChannels];
			for (int i = 0; i < this.capability.NbInputChannels; i++)
			{
				this.capability.InputChannelInfos[i] = this.driver.getChannelInfo(i, true);
			}
			for (int j = 0; j < this.capability.NbOutputChannels; j++)
			{
				this.capability.OutputChannelInfos[j] = this.driver.getChannelInfo(j, false);
			}
			this.capability.SampleRate = this.driver.getSampleRate();
			ASIOError latencies = this.driver.GetLatencies(out this.capability.InputLatency, out this.capability.OutputLatency);
			if (latencies != ASIOError.ASE_OK && latencies != ASIOError.ASE_NotPresent)
			{
				throw new ASIOException("ASIOgetLatencies")
				{
					Error = latencies
				};
			}
			this.driver.getBufferSize(out this.capability.BufferMinSize, out this.capability.BufferMaxSize, out this.capability.BufferPreferredSize, out this.capability.BufferGranularity);
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x000149C8 File Offset: 0x00012BC8
		private void BufferSwitchCallBack(int doubleBufferIndex, bool directProcess)
		{
			for (int i = 0; i < this.numberOfInputChannels; i++)
			{
				this.currentInputBuffers[i] = this.bufferInfos[i + this.inputChannelOffset].Buffer(doubleBufferIndex);
			}
			for (int j = 0; j < this.numberOfOutputChannels; j++)
			{
				this.currentOutputBuffers[j] = this.bufferInfos[j + this.outputChannelOffset + this.capability.NbInputChannels].Buffer(doubleBufferIndex);
			}
			if (this.fillBufferCallback != null)
			{
				this.fillBufferCallback(this.currentInputBuffers, this.currentOutputBuffers);
			}
			if (this.isOutputReadySupported)
			{
				this.driver.outputReady();
			}
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x00014A8A File Offset: 0x00012C8A
		private void SampleRateDidChangeCallBack(double sRate)
		{
			this.capability.SampleRate = sRate;
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x00014A98 File Offset: 0x00012C98
		private int AsioMessageCallBack(ASIOMessageSelector selector, int value, IntPtr message, IntPtr opt)
		{
			switch (selector)
			{
			case ASIOMessageSelector.kAsioSelectorSupported:
				switch ((ASIOMessageSelector)Enum.ToObject(typeof(ASIOMessageSelector), value))
				{
				case ASIOMessageSelector.kAsioEngineVersion:
					return 1;
				case ASIOMessageSelector.kAsioResetRequest:
					return 0;
				case ASIOMessageSelector.kAsioBufferSizeChange:
					return 0;
				case ASIOMessageSelector.kAsioResyncRequest:
					return 0;
				case ASIOMessageSelector.kAsioLatenciesChanged:
					return 0;
				case ASIOMessageSelector.kAsioSupportsTimeInfo:
					return 0;
				case ASIOMessageSelector.kAsioSupportsTimeCode:
					return 0;
				}
				break;
			case ASIOMessageSelector.kAsioEngineVersion:
				return 2;
			case ASIOMessageSelector.kAsioResetRequest:
				return 1;
			case ASIOMessageSelector.kAsioBufferSizeChange:
				return 0;
			case ASIOMessageSelector.kAsioResyncRequest:
				return 0;
			case ASIOMessageSelector.kAsioLatenciesChanged:
				return 0;
			case ASIOMessageSelector.kAsioSupportsTimeInfo:
				return 0;
			case ASIOMessageSelector.kAsioSupportsTimeCode:
				return 0;
			}
			return 0;
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00014B2C File Offset: 0x00012D2C
		private IntPtr BufferSwitchTimeInfoCallBack(IntPtr asioTimeParam, int doubleBufferIndex, bool directProcess)
		{
			return IntPtr.Zero;
		}

		// Token: 0x04000739 RID: 1849
		private ASIODriver driver;

		// Token: 0x0400073A RID: 1850
		private ASIOCallbacks callbacks;

		// Token: 0x0400073B RID: 1851
		private AsioDriverCapability capability;

		// Token: 0x0400073C RID: 1852
		private ASIOBufferInfo[] bufferInfos;

		// Token: 0x0400073D RID: 1853
		private bool isOutputReadySupported;

		// Token: 0x0400073E RID: 1854
		private IntPtr[] currentOutputBuffers;

		// Token: 0x0400073F RID: 1855
		private IntPtr[] currentInputBuffers;

		// Token: 0x04000740 RID: 1856
		private int numberOfOutputChannels;

		// Token: 0x04000741 RID: 1857
		private int numberOfInputChannels;

		// Token: 0x04000742 RID: 1858
		private ASIOFillBufferCallback fillBufferCallback;

		// Token: 0x04000743 RID: 1859
		private int bufferSize;

		// Token: 0x04000744 RID: 1860
		private int outputChannelOffset;

		// Token: 0x04000745 RID: 1861
		private int inputChannelOffset;
	}
}
