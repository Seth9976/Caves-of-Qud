using System;
using System.IO;
using System.Runtime.InteropServices;

namespace NAudio.Wave
{
	// Token: 0x02000197 RID: 407
	[StructLayout(0, Pack = 2)]
	public class Gsm610WaveFormat : WaveFormat
	{
		// Token: 0x0600083A RID: 2106 RVA: 0x00017928 File Offset: 0x00015B28
		public Gsm610WaveFormat()
		{
			this.waveFormatTag = WaveFormatEncoding.Gsm610;
			this.channels = 1;
			this.averageBytesPerSecond = 1625;
			this.bitsPerSample = 0;
			this.blockAlign = 65;
			this.sampleRate = 8000;
			this.extraSize = 2;
			this.samplesPerBlock = 320;
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x0600083B RID: 2107 RVA: 0x00017981 File Offset: 0x00015B81
		public short SamplesPerBlock
		{
			get
			{
				return this.samplesPerBlock;
			}
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x00017989 File Offset: 0x00015B89
		public override void Serialize(BinaryWriter writer)
		{
			base.Serialize(writer);
			writer.Write(this.samplesPerBlock);
		}

		// Token: 0x04000990 RID: 2448
		private short samplesPerBlock;
	}
}
