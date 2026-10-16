using System;

namespace NAudio.Wave.SampleProviders
{
	// Token: 0x020001CB RID: 459
	public class MonoToStereoSampleProvider : ISampleProvider
	{
		// Token: 0x060009E8 RID: 2536 RVA: 0x0001C8BC File Offset: 0x0001AABC
		public MonoToStereoSampleProvider(ISampleProvider source)
		{
			if (source.WaveFormat.Channels != 1)
			{
				throw new ArgumentException("Source must be mono");
			}
			this.source = source;
			this.waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(source.WaveFormat.SampleRate, 2);
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060009E9 RID: 2537 RVA: 0x0001C8FB File Offset: 0x0001AAFB
		public WaveFormat WaveFormat
		{
			get
			{
				return this.waveFormat;
			}
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x0001C904 File Offset: 0x0001AB04
		public int Read(float[] buffer, int offset, int count)
		{
			int num = count / 2;
			int num2 = offset;
			this.EnsureSourceBuffer(num);
			int num3 = this.source.Read(this.sourceBuffer, 0, num);
			for (int i = 0; i < num3; i++)
			{
				buffer[num2++] = this.sourceBuffer[i];
				buffer[num2++] = this.sourceBuffer[i];
			}
			return num3 * 2;
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x0001C95F File Offset: 0x0001AB5F
		private void EnsureSourceBuffer(int count)
		{
			if (this.sourceBuffer == null || this.sourceBuffer.Length < count)
			{
				this.sourceBuffer = new float[count];
			}
		}

		// Token: 0x04000B1C RID: 2844
		private readonly ISampleProvider source;

		// Token: 0x04000B1D RID: 2845
		private readonly WaveFormat waveFormat;

		// Token: 0x04000B1E RID: 2846
		private float[] sourceBuffer;
	}
}
