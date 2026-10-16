using System;

namespace NAudio.Wave
{
	// Token: 0x020001E2 RID: 482
	public class CueWaveFileReader : WaveFileReader
	{
		// Token: 0x06000A98 RID: 2712 RVA: 0x0001F4FA File Offset: 0x0001D6FA
		public CueWaveFileReader(string fileName)
			: base(fileName)
		{
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000A99 RID: 2713 RVA: 0x0001F503 File Offset: 0x0001D703
		public CueList Cues
		{
			get
			{
				if (this.cues == null)
				{
					this.cues = CueList.FromChunks(this);
				}
				return this.cues;
			}
		}

		// Token: 0x04000B73 RID: 2931
		private CueList cues;
	}
}
