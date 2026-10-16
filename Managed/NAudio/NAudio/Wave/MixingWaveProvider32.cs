using System;
using System.Collections.Generic;

namespace NAudio.Wave
{
	// Token: 0x020001CE RID: 462
	public class MixingWaveProvider32 : IWaveProvider
	{
		// Token: 0x06000A06 RID: 2566 RVA: 0x0001D0A6 File Offset: 0x0001B2A6
		public MixingWaveProvider32()
		{
			this.waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(44100, 2);
			this.bytesPerSample = 4;
			this.inputs = new List<IWaveProvider>();
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x0001D0D4 File Offset: 0x0001B2D4
		public MixingWaveProvider32(IEnumerable<IWaveProvider> inputs)
			: this()
		{
			foreach (IWaveProvider waveProvider in inputs)
			{
				this.AddInputStream(waveProvider);
			}
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x0001D124 File Offset: 0x0001B324
		public void AddInputStream(IWaveProvider waveProvider)
		{
			if (waveProvider.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
			{
				throw new ArgumentException("Must be IEEE floating point", "waveProvider.WaveFormat");
			}
			if (waveProvider.WaveFormat.BitsPerSample != 32)
			{
				throw new ArgumentException("Only 32 bit audio currently supported", "waveProvider.WaveFormat");
			}
			if (this.inputs.Count == 0)
			{
				int sampleRate = waveProvider.WaveFormat.SampleRate;
				int channels = waveProvider.WaveFormat.Channels;
				this.waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channels);
			}
			else if (!waveProvider.WaveFormat.Equals(this.waveFormat))
			{
				throw new ArgumentException("All incoming channels must have the same format", "waveProvider.WaveFormat");
			}
			lock (this.inputs)
			{
				this.inputs.Add(waveProvider);
			}
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x0001D1F8 File Offset: 0x0001B3F8
		public void RemoveInputStream(IWaveProvider waveProvider)
		{
			lock (this.inputs)
			{
				this.inputs.Remove(waveProvider);
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000A0A RID: 2570 RVA: 0x0001D238 File Offset: 0x0001B438
		public int InputCount
		{
			get
			{
				return this.inputs.Count;
			}
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x0001D248 File Offset: 0x0001B448
		public int Read(byte[] buffer, int offset, int count)
		{
			if (count % this.bytesPerSample != 0)
			{
				throw new ArgumentException("Must read an whole number of samples", "count");
			}
			Array.Clear(buffer, offset, count);
			int num = 0;
			byte[] array = new byte[count];
			lock (this.inputs)
			{
				foreach (IWaveProvider waveProvider in this.inputs)
				{
					int num2 = waveProvider.Read(array, 0, count);
					num = Math.Max(num, num2);
					if (num2 > 0)
					{
						MixingWaveProvider32.Sum32BitAudio(buffer, offset, array, num2);
					}
				}
			}
			return num;
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x0001D304 File Offset: 0x0001B504
		private unsafe static void Sum32BitAudio(byte[] destBuffer, int offset, byte[] sourceBuffer, int bytesRead)
		{
			fixed (byte* ptr = &destBuffer[offset], ptr2 = &sourceBuffer[0])
			{
				float* ptr3 = (float*)ptr;
				float* ptr4 = (float*)ptr2;
				int num = bytesRead / 4;
				for (int i = 0; i < num; i++)
				{
					ptr3[i] += ptr4[i];
				}
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x0001D356 File Offset: 0x0001B556
		public WaveFormat WaveFormat
		{
			get
			{
				return this.waveFormat;
			}
		}

		// Token: 0x04000B2E RID: 2862
		private List<IWaveProvider> inputs;

		// Token: 0x04000B2F RID: 2863
		private WaveFormat waveFormat;

		// Token: 0x04000B30 RID: 2864
		private int bytesPerSample;
	}
}
