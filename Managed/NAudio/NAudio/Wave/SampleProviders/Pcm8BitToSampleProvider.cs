using System;

namespace NAudio.Wave.SampleProviders
{
	// Token: 0x020001EB RID: 491
	public class Pcm8BitToSampleProvider : SampleProviderConverterBase
	{
		// Token: 0x06000ADD RID: 2781 RVA: 0x000203A7 File Offset: 0x0001E5A7
		public Pcm8BitToSampleProvider(IWaveProvider source)
			: base(source)
		{
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x000203B0 File Offset: 0x0001E5B0
		public override int Read(float[] buffer, int offset, int count)
		{
			base.EnsureSourceBuffer(count);
			int num = this.source.Read(this.sourceBuffer, 0, count);
			int num2 = offset;
			for (int i = 0; i < num; i++)
			{
				buffer[num2++] = (float)this.sourceBuffer[i] / 128f - 1f;
			}
			return num;
		}
	}
}
