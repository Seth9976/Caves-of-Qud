using System;
using System.IO;
using System.Runtime.InteropServices;
using NAudio.Dmo;

namespace NAudio.Wave
{
	// Token: 0x020001A1 RID: 417
	[StructLayout(0, Pack = 2)]
	public class WaveFormatExtensible : WaveFormat
	{
		// Token: 0x06000852 RID: 2130 RVA: 0x00017D23 File Offset: 0x00015F23
		private WaveFormatExtensible()
		{
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x00017D2C File Offset: 0x00015F2C
		public WaveFormatExtensible(int rate, int bits, int channels)
			: base(rate, bits, channels)
		{
			this.waveFormatTag = WaveFormatEncoding.Extensible;
			this.extraSize = 22;
			this.wValidBitsPerSample = (short)bits;
			for (int i = 0; i < channels; i++)
			{
				this.dwChannelMask |= 1 << i;
			}
			if (bits == 32)
			{
				this.subFormat = AudioMediaSubtypes.MEDIASUBTYPE_IEEE_FLOAT;
				return;
			}
			this.subFormat = AudioMediaSubtypes.MEDIASUBTYPE_PCM;
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x00017D98 File Offset: 0x00015F98
		public WaveFormat ToStandardWaveFormat()
		{
			if (this.subFormat == AudioMediaSubtypes.MEDIASUBTYPE_IEEE_FLOAT && this.bitsPerSample == 32)
			{
				return WaveFormat.CreateIeeeFloatWaveFormat(this.sampleRate, (int)this.channels);
			}
			if (this.subFormat == AudioMediaSubtypes.MEDIASUBTYPE_PCM)
			{
				return new WaveFormat(this.sampleRate, (int)this.bitsPerSample, (int)this.channels);
			}
			throw new InvalidOperationException("Not a recognised PCM or IEEE float format");
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000855 RID: 2133 RVA: 0x00017E07 File Offset: 0x00016007
		public Guid SubFormat
		{
			get
			{
				return this.subFormat;
			}
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x00017E10 File Offset: 0x00016010
		public override void Serialize(BinaryWriter writer)
		{
			base.Serialize(writer);
			writer.Write(this.wValidBitsPerSample);
			writer.Write(this.dwChannelMask);
			byte[] array = this.subFormat.ToByteArray();
			writer.Write(array, 0, array.Length);
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x00017E54 File Offset: 0x00016054
		public override string ToString()
		{
			return string.Format("{0} wBitsPerSample:{1} dwChannelMask:{2} subFormat:{3} extraSize:{4}", new object[]
			{
				base.ToString(),
				this.wValidBitsPerSample,
				this.dwChannelMask,
				this.subFormat,
				this.extraSize
			});
		}

		// Token: 0x04000A48 RID: 2632
		private short wValidBitsPerSample;

		// Token: 0x04000A49 RID: 2633
		private int dwChannelMask;

		// Token: 0x04000A4A RID: 2634
		private Guid subFormat;
	}
}
