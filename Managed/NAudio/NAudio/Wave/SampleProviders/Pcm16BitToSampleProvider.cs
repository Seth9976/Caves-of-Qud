using System;

namespace NAudio.Wave.SampleProviders
{
	// Token: 0x020001E9 RID: 489
	public class Pcm16BitToSampleProvider : SampleProviderConverterBase
	{
		// Token: 0x06000AD9 RID: 2777 RVA: 0x000202CF File Offset: 0x0001E4CF
		public Pcm16BitToSampleProvider(IWaveProvider source)
			: base(source)
		{
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x000202D8 File Offset: 0x0001E4D8
		public override int Read(float[] buffer, int offset, int count)
		{
			int num = count * 2;
			base.EnsureSourceBuffer(num);
			int num2 = this.source.Read(this.sourceBuffer, 0, num);
			int num3 = offset;
			for (int i = 0; i < num2; i += 2)
			{
				buffer[num3++] = (float)BitConverter.ToInt16(this.sourceBuffer, i) / 32768f;
			}
			return num2 / 2;
		}
	}
}
