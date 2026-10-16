using System;

namespace NAudio.Wave.SampleProviders
{
	// Token: 0x02000077 RID: 119
	public class Pcm32BitToSampleProvider : SampleProviderConverterBase
	{
		// Token: 0x06000287 RID: 647 RVA: 0x0000858B File Offset: 0x0000678B
		public Pcm32BitToSampleProvider(IWaveProvider source)
			: base(source)
		{
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00008594 File Offset: 0x00006794
		public override int Read(float[] buffer, int offset, int count)
		{
			int num = count * 4;
			base.EnsureSourceBuffer(num);
			int num2 = this.source.Read(this.sourceBuffer, 0, num);
			int num3 = offset;
			for (int i = 0; i < num2; i += 4)
			{
				buffer[num3++] = (float)(((int)((sbyte)this.sourceBuffer[i + 3]) << 24) | ((int)this.sourceBuffer[i + 2] << 16) | ((int)this.sourceBuffer[i + 1] << 8) | (int)this.sourceBuffer[i]) / 2.1474836E+09f;
			}
			return num2 / 4;
		}
	}
}
