using System;
using System.Collections.Generic;
using NAudio.Utils;

namespace NAudio.Wave
{
	// Token: 0x020001CF RID: 463
	public class MultiplexingWaveProvider : IWaveProvider
	{
		// Token: 0x06000A0E RID: 2574 RVA: 0x0001D360 File Offset: 0x0001B560
		public MultiplexingWaveProvider(IEnumerable<IWaveProvider> inputs, int numberOfOutputChannels)
		{
			this.inputs = new List<IWaveProvider>(inputs);
			this.outputChannelCount = numberOfOutputChannels;
			if (this.inputs.Count == 0)
			{
				throw new ArgumentException("You must provide at least one input");
			}
			if (numberOfOutputChannels < 1)
			{
				throw new ArgumentException("You must provide at least one output");
			}
			foreach (IWaveProvider waveProvider in this.inputs)
			{
				if (this.waveFormat == null)
				{
					if (waveProvider.WaveFormat.Encoding == WaveFormatEncoding.Pcm)
					{
						this.waveFormat = new WaveFormat(waveProvider.WaveFormat.SampleRate, waveProvider.WaveFormat.BitsPerSample, numberOfOutputChannels);
					}
					else
					{
						if (waveProvider.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
						{
							throw new ArgumentException("Only PCM and 32 bit float are supported");
						}
						this.waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(waveProvider.WaveFormat.SampleRate, numberOfOutputChannels);
					}
				}
				else
				{
					if (waveProvider.WaveFormat.BitsPerSample != this.waveFormat.BitsPerSample)
					{
						throw new ArgumentException("All inputs must have the same bit depth");
					}
					if (waveProvider.WaveFormat.SampleRate != this.waveFormat.SampleRate)
					{
						throw new ArgumentException("All inputs must have the same sample rate");
					}
				}
				this.inputChannelCount += waveProvider.WaveFormat.Channels;
			}
			this.bytesPerSample = this.waveFormat.BitsPerSample / 8;
			this.mappings = new List<int>();
			for (int i = 0; i < this.outputChannelCount; i++)
			{
				this.mappings.Add(i % this.inputChannelCount);
			}
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x0001D4F8 File Offset: 0x0001B6F8
		public int Read(byte[] buffer, int offset, int count)
		{
			int num = this.bytesPerSample * this.outputChannelCount;
			int num2 = count / num;
			int num3 = 0;
			int num4 = 0;
			foreach (IWaveProvider waveProvider in this.inputs)
			{
				int num5 = this.bytesPerSample * waveProvider.WaveFormat.Channels;
				int num6 = num2 * num5;
				this.inputBuffer = BufferHelpers.Ensure(this.inputBuffer, num6);
				int num7 = waveProvider.Read(this.inputBuffer, 0, num6);
				num4 = Math.Max(num4, num7 / num5);
				for (int i = 0; i < waveProvider.WaveFormat.Channels; i++)
				{
					int num8 = num3 + i;
					for (int j = 0; j < this.outputChannelCount; j++)
					{
						if (this.mappings[j] == num8)
						{
							int num9 = i * this.bytesPerSample;
							int num10 = offset + j * this.bytesPerSample;
							int k;
							for (k = 0; k < num2; k++)
							{
								if (num9 >= num7)
								{
									break;
								}
								Array.Copy(this.inputBuffer, num9, buffer, num10, this.bytesPerSample);
								num10 += num;
								num9 += num5;
							}
							while (k < num2)
							{
								Array.Clear(buffer, num10, this.bytesPerSample);
								num10 += num;
								k++;
							}
						}
					}
				}
				num3 += waveProvider.WaveFormat.Channels;
			}
			return num4 * num;
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000A10 RID: 2576 RVA: 0x0001D690 File Offset: 0x0001B890
		public WaveFormat WaveFormat
		{
			get
			{
				return this.waveFormat;
			}
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x0001D698 File Offset: 0x0001B898
		public void ConnectInputToOutput(int inputChannel, int outputChannel)
		{
			if (inputChannel < 0 || inputChannel >= this.InputChannelCount)
			{
				throw new ArgumentException("Invalid input channel");
			}
			if (outputChannel < 0 || outputChannel >= this.OutputChannelCount)
			{
				throw new ArgumentException("Invalid output channel");
			}
			this.mappings[outputChannel] = inputChannel;
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000A12 RID: 2578 RVA: 0x0001D6D7 File Offset: 0x0001B8D7
		public int InputChannelCount
		{
			get
			{
				return this.inputChannelCount;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000A13 RID: 2579 RVA: 0x0001D6DF File Offset: 0x0001B8DF
		public int OutputChannelCount
		{
			get
			{
				return this.outputChannelCount;
			}
		}

		// Token: 0x04000B31 RID: 2865
		private readonly IList<IWaveProvider> inputs;

		// Token: 0x04000B32 RID: 2866
		private readonly WaveFormat waveFormat;

		// Token: 0x04000B33 RID: 2867
		private readonly int outputChannelCount;

		// Token: 0x04000B34 RID: 2868
		private readonly int inputChannelCount;

		// Token: 0x04000B35 RID: 2869
		private readonly List<int> mappings;

		// Token: 0x04000B36 RID: 2870
		private readonly int bytesPerSample;

		// Token: 0x04000B37 RID: 2871
		private byte[] inputBuffer;
	}
}
