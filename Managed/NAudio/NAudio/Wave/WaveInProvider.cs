using System;

namespace NAudio.Wave
{
	// Token: 0x020001D5 RID: 469
	public class WaveInProvider : IWaveProvider
	{
		// Token: 0x06000A31 RID: 2609 RVA: 0x0001DD7D File Offset: 0x0001BF7D
		public WaveInProvider(IWaveIn waveIn)
		{
			this.waveIn = waveIn;
			waveIn.DataAvailable += new EventHandler<WaveInEventArgs>(this.waveIn_DataAvailable);
			this.bufferedWaveProvider = new BufferedWaveProvider(this.WaveFormat);
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x0001DDAF File Offset: 0x0001BFAF
		private void waveIn_DataAvailable(object sender, WaveInEventArgs e)
		{
			this.bufferedWaveProvider.AddSamples(e.Buffer, 0, e.BytesRecorded);
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x0001DDC9 File Offset: 0x0001BFC9
		public int Read(byte[] buffer, int offset, int count)
		{
			return this.bufferedWaveProvider.Read(buffer, 0, count);
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000A34 RID: 2612 RVA: 0x0001DDD9 File Offset: 0x0001BFD9
		public WaveFormat WaveFormat
		{
			get
			{
				return this.waveIn.WaveFormat;
			}
		}

		// Token: 0x04000B4C RID: 2892
		private IWaveIn waveIn;

		// Token: 0x04000B4D RID: 2893
		private BufferedWaveProvider bufferedWaveProvider;
	}
}
