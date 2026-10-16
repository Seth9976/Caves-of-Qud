using System;
using System.IO;

namespace NAudio.Wave
{
	// Token: 0x020001EC RID: 492
	public class RawSourceWaveStream : WaveStream
	{
		// Token: 0x06000ADF RID: 2783 RVA: 0x00020405 File Offset: 0x0001E605
		public RawSourceWaveStream(Stream sourceStream, WaveFormat waveFormat)
		{
			this.sourceStream = sourceStream;
			this.waveFormat = waveFormat;
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x0002041B File Offset: 0x0001E61B
		public override WaveFormat WaveFormat
		{
			get
			{
				return this.waveFormat;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x00020423 File Offset: 0x0001E623
		public override long Length
		{
			get
			{
				return this.sourceStream.Length;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000AE2 RID: 2786 RVA: 0x00020430 File Offset: 0x0001E630
		// (set) Token: 0x06000AE3 RID: 2787 RVA: 0x0002043D File Offset: 0x0001E63D
		public override long Position
		{
			get
			{
				return this.sourceStream.Position;
			}
			set
			{
				this.sourceStream.Position = value;
			}
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x0002044C File Offset: 0x0001E64C
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num;
			try
			{
				num = this.sourceStream.Read(buffer, offset, count);
			}
			catch (EndOfStreamException)
			{
				num = 0;
			}
			return num;
		}

		// Token: 0x04000B9D RID: 2973
		private Stream sourceStream;

		// Token: 0x04000B9E RID: 2974
		private WaveFormat waveFormat;
	}
}
