using System;
using System.Collections.Generic;
using NAudio.Utils;

namespace NAudio.Wave.SampleProviders
{
	// Token: 0x0200018E RID: 398
	public class MixingSampleProvider : ISampleProvider
	{
		// Token: 0x06000803 RID: 2051 RVA: 0x00016E52 File Offset: 0x00015052
		public MixingSampleProvider(WaveFormat waveFormat)
		{
			if (waveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
			{
				throw new ArgumentException("Mixer wave format must be IEEE float");
			}
			this.sources = new List<ISampleProvider>();
			this.waveFormat = waveFormat;
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x00016E80 File Offset: 0x00015080
		public MixingSampleProvider(IEnumerable<ISampleProvider> sources)
		{
			this.sources = new List<ISampleProvider>();
			foreach (ISampleProvider sampleProvider in sources)
			{
				this.AddMixerInput(sampleProvider);
			}
			if (this.sources.Count == 0)
			{
				throw new ArgumentException("Must provide at least one input in this constructor");
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000805 RID: 2053 RVA: 0x00016EF4 File Offset: 0x000150F4
		// (set) Token: 0x06000806 RID: 2054 RVA: 0x00016EFC File Offset: 0x000150FC
		public bool ReadFully { get; set; }

		// Token: 0x06000807 RID: 2055 RVA: 0x00016F05 File Offset: 0x00015105
		public void AddMixerInput(IWaveProvider mixerInput)
		{
			this.AddMixerInput(SampleProviderConverters.ConvertWaveProviderIntoSampleProvider(mixerInput));
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x00016F14 File Offset: 0x00015114
		public void AddMixerInput(ISampleProvider mixerInput)
		{
			lock (this.sources)
			{
				if (this.sources.Count >= 1024)
				{
					throw new InvalidOperationException("Too many mixer inputs");
				}
				this.sources.Add(mixerInput);
			}
			if (this.waveFormat == null)
			{
				this.waveFormat = mixerInput.WaveFormat;
				return;
			}
			if (this.WaveFormat.SampleRate != mixerInput.WaveFormat.SampleRate || this.WaveFormat.Channels != mixerInput.WaveFormat.Channels)
			{
				throw new ArgumentException("All mixer inputs must have the same WaveFormat");
			}
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x00016FC0 File Offset: 0x000151C0
		public void RemoveMixerInput(ISampleProvider mixerInput)
		{
			lock (this.sources)
			{
				this.sources.Remove(mixerInput);
			}
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x00017000 File Offset: 0x00015200
		public void RemoveAllMixerInputs()
		{
			lock (this.sources)
			{
				this.sources.Clear();
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x0600080B RID: 2059 RVA: 0x00017040 File Offset: 0x00015240
		public WaveFormat WaveFormat
		{
			get
			{
				return this.waveFormat;
			}
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x00017048 File Offset: 0x00015248
		public int Read(float[] buffer, int offset, int count)
		{
			int num = 0;
			this.sourceBuffer = BufferHelpers.Ensure(this.sourceBuffer, count);
			lock (this.sources)
			{
				for (int i = this.sources.Count - 1; i >= 0; i--)
				{
					ISampleProvider sampleProvider = this.sources[i];
					int num2 = sampleProvider.Read(this.sourceBuffer, 0, count);
					int num3 = offset;
					for (int j = 0; j < num2; j++)
					{
						if (j >= num)
						{
							buffer[num3++] = this.sourceBuffer[j];
						}
						else
						{
							buffer[num3++] += this.sourceBuffer[j];
						}
					}
					num = Math.Max(num2, num);
					if (num2 == 0)
					{
						this.sources.RemoveAt(i);
					}
				}
			}
			if (this.ReadFully && num < count)
			{
				int k = offset + num;
				while (k < offset + count)
				{
					buffer[k++] = 0f;
				}
				num = count;
			}
			return num;
		}

		// Token: 0x0400097A RID: 2426
		private const int maxInputs = 1024;

		// Token: 0x0400097B RID: 2427
		private List<ISampleProvider> sources;

		// Token: 0x0400097C RID: 2428
		private WaveFormat waveFormat;

		// Token: 0x0400097D RID: 2429
		private float[] sourceBuffer;
	}
}
