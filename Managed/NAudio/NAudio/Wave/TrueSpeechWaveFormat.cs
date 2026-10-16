using System;
using System.IO;
using System.Runtime.InteropServices;

namespace NAudio.Wave
{
	// Token: 0x0200019D RID: 413
	[StructLayout(0, Pack = 2)]
	public class TrueSpeechWaveFormat : WaveFormat
	{
		// Token: 0x06000841 RID: 2113 RVA: 0x00017A70 File Offset: 0x00015C70
		public TrueSpeechWaveFormat()
		{
			this.waveFormatTag = WaveFormatEncoding.DspGroupTrueSpeech;
			this.channels = 1;
			this.averageBytesPerSecond = 1067;
			this.bitsPerSample = 1;
			this.blockAlign = 32;
			this.sampleRate = 8000;
			this.extraSize = 32;
			this.unknown = new short[16];
			this.unknown[0] = 1;
			this.unknown[1] = 240;
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x00017AE4 File Offset: 0x00015CE4
		public override void Serialize(BinaryWriter writer)
		{
			base.Serialize(writer);
			foreach (short num in this.unknown)
			{
				writer.Write(num);
			}
		}

		// Token: 0x040009A2 RID: 2466
		[MarshalAs(30, SizeConst = 16)]
		private short[] unknown;
	}
}
