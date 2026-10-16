using System;

namespace NAudio.Wave.SampleProviders
{
	// Token: 0x020001EA RID: 490
	public class Pcm24BitToSampleProvider : SampleProviderConverterBase
	{
		// Token: 0x06000ADB RID: 2779 RVA: 0x0002032F File Offset: 0x0001E52F
		public Pcm24BitToSampleProvider(IWaveProvider source)
			: base(source)
		{
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x00020338 File Offset: 0x0001E538
		public override int Read(float[] buffer, int offset, int count)
		{
			int num = count * 3;
			base.EnsureSourceBuffer(num);
			int num2 = this.source.Read(this.sourceBuffer, 0, num);
			int num3 = offset;
			for (int i = 0; i < num2; i += 3)
			{
				buffer[num3++] = (float)(((int)((sbyte)this.sourceBuffer[i + 2]) << 16) | ((int)this.sourceBuffer[i + 1] << 8) | (int)this.sourceBuffer[i]) / 8388608f;
			}
			return num2 / 3;
		}
	}
}
