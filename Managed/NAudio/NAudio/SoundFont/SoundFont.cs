using System;
using System.IO;

namespace NAudio.SoundFont
{
	// Token: 0x020000CA RID: 202
	public class SoundFont
	{
		// Token: 0x06000461 RID: 1121 RVA: 0x0000E959 File Offset: 0x0000CB59
		public SoundFont(string fileName)
			: this(new FileStream(fileName, 3, 1))
		{
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0000E96C File Offset: 0x0000CB6C
		public SoundFont(Stream sfFile)
		{
			try
			{
				RiffChunk topLevelChunk = RiffChunk.GetTopLevelChunk(new BinaryReader(sfFile));
				if (!(topLevelChunk.ChunkID == "RIFF"))
				{
					throw new InvalidDataException("Not a RIFF file");
				}
				string text = topLevelChunk.ReadChunkID();
				if (text != "sfbk")
				{
					throw new InvalidDataException(string.Format("Not a SoundFont ({0})", text));
				}
				RiffChunk nextSubChunk = topLevelChunk.GetNextSubChunk();
				if (!(nextSubChunk.ChunkID == "LIST"))
				{
					throw new InvalidDataException(string.Format("Not info list found ({0})", nextSubChunk.ChunkID));
				}
				this.info = new InfoChunk(nextSubChunk);
				RiffChunk riffChunk = topLevelChunk.GetNextSubChunk();
				this.sampleData = new SampleDataChunk(riffChunk);
				riffChunk = topLevelChunk.GetNextSubChunk();
				this.presetsChunk = new PresetsChunk(riffChunk);
			}
			finally
			{
				if (sfFile != null)
				{
					sfFile.Dispose();
				}
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x0000EA54 File Offset: 0x0000CC54
		public InfoChunk FileInfo
		{
			get
			{
				return this.info;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x0000EA5C File Offset: 0x0000CC5C
		public Preset[] Presets
		{
			get
			{
				return this.presetsChunk.Presets;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x0000EA69 File Offset: 0x0000CC69
		public Instrument[] Instruments
		{
			get
			{
				return this.presetsChunk.Instruments;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x0000EA76 File Offset: 0x0000CC76
		public SampleHeader[] SampleHeaders
		{
			get
			{
				return this.presetsChunk.SampleHeaders;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x0000EA83 File Offset: 0x0000CC83
		public byte[] SampleData
		{
			get
			{
				return this.sampleData.SampleData;
			}
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0000EA90 File Offset: 0x0000CC90
		public override string ToString()
		{
			return string.Format("Info Chunk:\r\n{0}\r\nPresets Chunk:\r\n{1}", this.info, this.presetsChunk);
		}

		// Token: 0x04000533 RID: 1331
		private InfoChunk info;

		// Token: 0x04000534 RID: 1332
		private PresetsChunk presetsChunk;

		// Token: 0x04000535 RID: 1333
		private SampleDataChunk sampleData;
	}
}
