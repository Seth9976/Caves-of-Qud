using System;

namespace NAudio.Wave.SampleProviders
{
	// Token: 0x0200007E RID: 126
	public class WaveToSampleProvider64 : SampleProviderConverterBase
	{
		// Token: 0x060002A9 RID: 681 RVA: 0x00008F1E File Offset: 0x0000711E
		public WaveToSampleProvider64(IWaveProvider source)
			: base(source)
		{
			if (source.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
			{
				throw new ArgumentException("Must be already floating point");
			}
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00008F40 File Offset: 0x00007140
		public override int Read(float[] buffer, int offset, int count)
		{
			int num = count * 8;
			base.EnsureSourceBuffer(num);
			int num2 = this.source.Read(this.sourceBuffer, 0, num);
			int num3 = num2 / 8;
			int num4 = offset;
			for (int i = 0; i < num2; i += 8)
			{
				long num5 = BitConverter.ToInt64(this.sourceBuffer, i);
				buffer[num4++] = (float)BitConverter.Int64BitsToDouble(num5);
			}
			return num3;
		}
	}
}
