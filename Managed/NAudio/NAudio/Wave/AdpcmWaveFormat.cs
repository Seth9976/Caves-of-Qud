using System;
using System.IO;
using System.Runtime.InteropServices;

namespace NAudio.Wave
{
	// Token: 0x0200019E RID: 414
	[StructLayout(0, Pack = 2)]
	public class AdpcmWaveFormat : WaveFormat
	{
		// Token: 0x06000843 RID: 2115 RVA: 0x00017B18 File Offset: 0x00015D18
		private AdpcmWaveFormat()
			: this(8000, 1)
		{
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000844 RID: 2116 RVA: 0x00017B26 File Offset: 0x00015D26
		public int SamplesPerBlock
		{
			get
			{
				return (int)this.samplesPerBlock;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x00017B2E File Offset: 0x00015D2E
		public int NumCoefficients
		{
			get
			{
				return (int)this.numCoeff;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000846 RID: 2118 RVA: 0x00017B36 File Offset: 0x00015D36
		public short[] Coefficients
		{
			get
			{
				return this.coefficients;
			}
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00017B5C File Offset: 0x00015D5C
		public AdpcmWaveFormat(int sampleRate, int channels)
			: base(sampleRate, 0, channels)
		{
			this.waveFormatTag = WaveFormatEncoding.Adpcm;
			this.extraSize = 32;
			int sampleRate2 = this.sampleRate;
			if (sampleRate2 <= 11025)
			{
				if (sampleRate2 == 8000 || sampleRate2 == 11025)
				{
					this.blockAlign = 256;
					goto IL_0070;
				}
			}
			else
			{
				if (sampleRate2 == 22050)
				{
					this.blockAlign = 512;
					goto IL_0070;
				}
				if (sampleRate2 != 44100)
				{
				}
			}
			this.blockAlign = 1024;
			IL_0070:
			this.bitsPerSample = 4;
			this.samplesPerBlock = (short)(((int)this.blockAlign - 7 * channels) * 8 / ((int)this.bitsPerSample * channels) + 2);
			this.averageBytesPerSecond = base.SampleRate * (int)this.blockAlign / (int)this.samplesPerBlock;
			this.numCoeff = 7;
			this.coefficients = new short[]
			{
				256, 0, 512, -256, 0, 0, 192, 64, 240, 0,
				460, -208, 392, -232
			};
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00017C38 File Offset: 0x00015E38
		public override void Serialize(BinaryWriter writer)
		{
			base.Serialize(writer);
			writer.Write(this.samplesPerBlock);
			writer.Write(this.numCoeff);
			foreach (short num in this.coefficients)
			{
				writer.Write(num);
			}
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x00017C84 File Offset: 0x00015E84
		public override string ToString()
		{
			return string.Format("Microsoft ADPCM {0} Hz {1} channels {2} bits per sample {3} samples per block", new object[] { base.SampleRate, this.channels, this.bitsPerSample, this.samplesPerBlock });
		}

		// Token: 0x040009A3 RID: 2467
		private short samplesPerBlock;

		// Token: 0x040009A4 RID: 2468
		private short numCoeff;

		// Token: 0x040009A5 RID: 2469
		[MarshalAs(30, SizeConst = 14)]
		private short[] coefficients;
	}
}
