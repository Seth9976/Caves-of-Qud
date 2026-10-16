using System;
using System.IO;

namespace NAudio.Wave
{
	// Token: 0x020001A9 RID: 425
	public class CueWaveFileWriter : WaveFileWriter
	{
		// Token: 0x060008D4 RID: 2260 RVA: 0x000195B4 File Offset: 0x000177B4
		public CueWaveFileWriter(string fileName, WaveFormat waveFormat)
			: base(fileName, waveFormat)
		{
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x000195BE File Offset: 0x000177BE
		public void AddCue(int position, string label)
		{
			if (this.cues == null)
			{
				this.cues = new CueList();
			}
			this.cues.Add(new Cue(position, label));
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x000195E8 File Offset: 0x000177E8
		private void WriteCues(BinaryWriter w)
		{
			if (this.cues != null)
			{
				byte[] riffchunks = this.cues.GetRIFFChunks();
				int num = riffchunks.Length;
				w.Seek(0, 2);
				if (w.BaseStream.Length % 2L == 1L)
				{
					w.Write(0);
				}
				w.Write(this.cues.GetRIFFChunks(), 0, num);
				w.Seek(4, 0);
				w.Write((int)(w.BaseStream.Length - 8L));
			}
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x00019660 File Offset: 0x00017860
		protected override void UpdateHeader(BinaryWriter writer)
		{
			base.UpdateHeader(writer);
			this.WriteCues(writer);
		}

		// Token: 0x04000A75 RID: 2677
		private CueList cues;
	}
}
