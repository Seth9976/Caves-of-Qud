using System;
using System.Collections.Generic;
using NAudio.Utils;

namespace NAudio.Wave.SampleProviders
{
	// Token: 0x02000074 RID: 116
	public class MultiplexingSampleProvider : ISampleProvider
	{
		// Token: 0x06000268 RID: 616 RVA: 0x00007E40 File Offset: 0x00006040
		public MultiplexingSampleProvider(IEnumerable<ISampleProvider> inputs, int numberOfOutputChannels)
		{
			this.inputs = new List<ISampleProvider>(inputs);
			this.outputChannelCount = numberOfOutputChannels;
			if (this.inputs.Count == 0)
			{
				throw new ArgumentException("You must provide at least one input");
			}
			if (numberOfOutputChannels < 1)
			{
				throw new ArgumentException("You must provide at least one output");
			}
			foreach (ISampleProvider sampleProvider in this.inputs)
			{
				if (this.waveFormat == null)
				{
					if (sampleProvider.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
					{
						throw new ArgumentException("Only 32 bit float is supported");
					}
					this.waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(sampleProvider.WaveFormat.SampleRate, numberOfOutputChannels);
				}
				else
				{
					if (sampleProvider.WaveFormat.BitsPerSample != this.waveFormat.BitsPerSample)
					{
						throw new ArgumentException("All inputs must have the same bit depth");
					}
					if (sampleProvider.WaveFormat.SampleRate != this.waveFormat.SampleRate)
					{
						throw new ArgumentException("All inputs must have the same sample rate");
					}
				}
				this.inputChannelCount += sampleProvider.WaveFormat.Channels;
			}
			this.mappings = new List<int>();
			for (int i = 0; i < this.outputChannelCount; i++)
			{
				this.mappings.Add(i % this.inputChannelCount);
			}
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00007F94 File Offset: 0x00006194
		public int Read(float[] buffer, int offset, int count)
		{
			int num = count / this.outputChannelCount;
			int num2 = 0;
			int num3 = 0;
			foreach (ISampleProvider sampleProvider in this.inputs)
			{
				int num4 = num * sampleProvider.WaveFormat.Channels;
				this.inputBuffer = BufferHelpers.Ensure(this.inputBuffer, num4);
				int num5 = sampleProvider.Read(this.inputBuffer, 0, num4);
				num3 = Math.Max(num3, num5 / sampleProvider.WaveFormat.Channels);
				for (int i = 0; i < sampleProvider.WaveFormat.Channels; i++)
				{
					int num6 = num2 + i;
					for (int j = 0; j < this.outputChannelCount; j++)
					{
						if (this.mappings[j] == num6)
						{
							int num7 = i;
							int num8 = offset + j;
							int k;
							for (k = 0; k < num; k++)
							{
								if (num7 >= num5)
								{
									break;
								}
								buffer[num8] = this.inputBuffer[num7];
								num8 += this.outputChannelCount;
								num7 += sampleProvider.WaveFormat.Channels;
							}
							while (k < num)
							{
								buffer[num8] = 0f;
								num8 += this.outputChannelCount;
								k++;
							}
						}
					}
				}
				num2 += sampleProvider.WaveFormat.Channels;
			}
			return num3 * this.outputChannelCount;
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600026A RID: 618 RVA: 0x00008118 File Offset: 0x00006318
		public WaveFormat WaveFormat
		{
			get
			{
				return this.waveFormat;
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00008120 File Offset: 0x00006320
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

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0000815F File Offset: 0x0000635F
		public int InputChannelCount
		{
			get
			{
				return this.inputChannelCount;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600026D RID: 621 RVA: 0x00008167 File Offset: 0x00006367
		public int OutputChannelCount
		{
			get
			{
				return this.outputChannelCount;
			}
		}

		// Token: 0x04000381 RID: 897
		private readonly IList<ISampleProvider> inputs;

		// Token: 0x04000382 RID: 898
		private readonly WaveFormat waveFormat;

		// Token: 0x04000383 RID: 899
		private readonly int outputChannelCount;

		// Token: 0x04000384 RID: 900
		private readonly int inputChannelCount;

		// Token: 0x04000385 RID: 901
		private readonly List<int> mappings;

		// Token: 0x04000386 RID: 902
		private float[] inputBuffer;
	}
}
