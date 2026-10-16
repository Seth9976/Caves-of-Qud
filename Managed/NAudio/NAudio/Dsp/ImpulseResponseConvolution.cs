using System;

namespace NAudio.Dsp
{
	// Token: 0x0200009E RID: 158
	public class ImpulseResponseConvolution
	{
		// Token: 0x06000366 RID: 870 RVA: 0x0000BAE0 File Offset: 0x00009CE0
		public float[] Convolve(float[] input, float[] impulseResponse)
		{
			float[] array = new float[input.Length + impulseResponse.Length];
			for (int i = 0; i < array.Length; i++)
			{
				for (int j = 0; j < impulseResponse.Length; j++)
				{
					if (i >= j && i - j < input.Length)
					{
						array[i] += impulseResponse[j] * input[i - j];
					}
				}
			}
			this.Normalize(array);
			return array;
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000BB48 File Offset: 0x00009D48
		public void Normalize(float[] data)
		{
			float num = 0f;
			for (int i = 0; i < data.Length; i++)
			{
				num = Math.Max(num, Math.Abs(data[i]));
			}
			if ((double)num > 1.0)
			{
				for (int j = 0; j < data.Length; j++)
				{
					data[j] /= num;
				}
			}
		}
	}
}
