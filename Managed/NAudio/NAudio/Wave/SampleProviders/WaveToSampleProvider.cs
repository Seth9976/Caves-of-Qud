using System;

namespace NAudio.Wave.SampleProviders
{
	// Token: 0x020001D8 RID: 472
	public class WaveToSampleProvider : SampleProviderConverterBase
	{
		// Token: 0x06000A41 RID: 2625 RVA: 0x0001DEB0 File Offset: 0x0001C0B0
		public WaveToSampleProvider(IWaveProvider source)
			: base(source)
		{
			if (source.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
			{
				throw new ArgumentException("Must be already floating point");
			}
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0001DED4 File Offset: 0x0001C0D4
		public override int Read(float[] buffer, int offset, int count)
		{
			int num = count * 4;
			base.EnsureSourceBuffer(num);
			int num2 = this.source.Read(this.sourceBuffer, 0, num);
			int num3 = num2 / 4;
			int num4 = offset;
			for (int i = 0; i < num2; i += 4)
			{
				buffer[num4++] = BitConverter.ToSingle(this.sourceBuffer, i);
			}
			return num3;
		}
	}
}
