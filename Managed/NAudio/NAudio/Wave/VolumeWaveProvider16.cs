using System;

namespace NAudio.Wave
{
	// Token: 0x020001D2 RID: 466
	public class VolumeWaveProvider16 : IWaveProvider
	{
		// Token: 0x06000A22 RID: 2594 RVA: 0x0001D9C0 File Offset: 0x0001BBC0
		public VolumeWaveProvider16(IWaveProvider sourceProvider)
		{
			this.Volume = 1f;
			this.sourceProvider = sourceProvider;
			if (sourceProvider.WaveFormat.Encoding != WaveFormatEncoding.Pcm)
			{
				throw new ArgumentException("Expecting PCM input");
			}
			if (sourceProvider.WaveFormat.BitsPerSample != 16)
			{
				throw new ArgumentException("Expecting 16 bit");
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000A23 RID: 2595 RVA: 0x0001DA18 File Offset: 0x0001BC18
		// (set) Token: 0x06000A24 RID: 2596 RVA: 0x0001DA20 File Offset: 0x0001BC20
		public float Volume
		{
			get
			{
				return this.volume;
			}
			set
			{
				this.volume = value;
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000A25 RID: 2597 RVA: 0x0001DA29 File Offset: 0x0001BC29
		public WaveFormat WaveFormat
		{
			get
			{
				return this.sourceProvider.WaveFormat;
			}
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0001DA38 File Offset: 0x0001BC38
		public int Read(byte[] buffer, int offset, int count)
		{
			int num = this.sourceProvider.Read(buffer, offset, count);
			if (this.volume == 0f)
			{
				for (int i = 0; i < num; i++)
				{
					buffer[offset++] = 0;
				}
			}
			else if (this.volume != 1f)
			{
				for (int j = 0; j < num; j += 2)
				{
					short num2 = (short)(((int)buffer[offset + 1] << 8) | (int)buffer[offset]);
					float num3 = (float)num2 * this.volume;
					num2 = (short)num3;
					if (this.Volume > 1f)
					{
						if (num3 > 32767f)
						{
							num2 = short.MaxValue;
						}
						else if (num3 < -32768f)
						{
							num2 = short.MinValue;
						}
					}
					buffer[offset++] = (byte)(num2 & 255);
					buffer[offset++] = (byte)(num2 >> 8);
				}
			}
			return num;
		}

		// Token: 0x04000B42 RID: 2882
		private readonly IWaveProvider sourceProvider;

		// Token: 0x04000B43 RID: 2883
		private float volume;
	}
}
