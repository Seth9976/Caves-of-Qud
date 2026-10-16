using System;

namespace NAudio.Wave
{
	// Token: 0x020001D9 RID: 473
	public class WaveRecorder : IWaveProvider, IDisposable
	{
		// Token: 0x06000A43 RID: 2627 RVA: 0x0001DF2B File Offset: 0x0001C12B
		public WaveRecorder(IWaveProvider source, string destination)
		{
			this.source = source;
			this.writer = new WaveFileWriter(destination, source.WaveFormat);
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0001DF4C File Offset: 0x0001C14C
		public int Read(byte[] buffer, int offset, int count)
		{
			int num = this.source.Read(buffer, offset, count);
			this.writer.Write(buffer, offset, num);
			return num;
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x0001DF77 File Offset: 0x0001C177
		public WaveFormat WaveFormat
		{
			get
			{
				return this.source.WaveFormat;
			}
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x0001DF84 File Offset: 0x0001C184
		public void Dispose()
		{
			if (this.writer != null)
			{
				this.writer.Dispose();
				this.writer = null;
			}
		}

		// Token: 0x04000B50 RID: 2896
		private WaveFileWriter writer;

		// Token: 0x04000B51 RID: 2897
		private IWaveProvider source;
	}
}
