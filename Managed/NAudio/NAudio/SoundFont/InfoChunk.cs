using System;
using System.Collections.Generic;

namespace NAudio.SoundFont
{
	// Token: 0x020000B6 RID: 182
	public class InfoChunk
	{
		// Token: 0x060003F5 RID: 1013 RVA: 0x0000D7F4 File Offset: 0x0000B9F4
		internal InfoChunk(RiffChunk chunk)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			if (chunk.ReadChunkID() != "INFO")
			{
				throw new InvalidDataException("Not an INFO chunk");
			}
			RiffChunk nextSubChunk;
			while ((nextSubChunk = chunk.GetNextSubChunk()) != null)
			{
				string chunkID;
				if ((chunkID = nextSubChunk.ChunkID) != null)
				{
					if (<PrivateImplementationDetails>{FA168FAF-488C-4AC3-8A7C-7C759315A688}.$$method0x60003e2-1 == null)
					{
						Dictionary<string, int> dictionary = new Dictionary<string, int>(11);
						dictionary.Add("ifil", 0);
						dictionary.Add("isng", 1);
						dictionary.Add("INAM", 2);
						dictionary.Add("irom", 3);
						dictionary.Add("iver", 4);
						dictionary.Add("ICRD", 5);
						dictionary.Add("IENG", 6);
						dictionary.Add("IPRD", 7);
						dictionary.Add("ICOP", 8);
						dictionary.Add("ICMT", 9);
						dictionary.Add("ISFT", 10);
						<PrivateImplementationDetails>{FA168FAF-488C-4AC3-8A7C-7C759315A688}.$$method0x60003e2-1 = dictionary;
					}
					int num;
					if (<PrivateImplementationDetails>{FA168FAF-488C-4AC3-8A7C-7C759315A688}.$$method0x60003e2-1.TryGetValue(chunkID, ref num))
					{
						switch (num)
						{
						case 0:
							flag = true;
							this.verSoundFont = nextSubChunk.GetDataAsStructure<SFVersion>(new SFVersionBuilder());
							continue;
						case 1:
							flag2 = true;
							this.waveTableSoundEngine = nextSubChunk.GetDataAsString();
							continue;
						case 2:
							flag3 = true;
							this.bankName = nextSubChunk.GetDataAsString();
							continue;
						case 3:
							this.dataROM = nextSubChunk.GetDataAsString();
							continue;
						case 4:
							this.verROM = nextSubChunk.GetDataAsStructure<SFVersion>(new SFVersionBuilder());
							continue;
						case 5:
							this.creationDate = nextSubChunk.GetDataAsString();
							continue;
						case 6:
							this.author = nextSubChunk.GetDataAsString();
							continue;
						case 7:
							this.targetProduct = nextSubChunk.GetDataAsString();
							continue;
						case 8:
							this.copyright = nextSubChunk.GetDataAsString();
							continue;
						case 9:
							this.comments = nextSubChunk.GetDataAsString();
							continue;
						case 10:
							this.tools = nextSubChunk.GetDataAsString();
							continue;
						}
					}
				}
				throw new InvalidDataException(string.Format("Unknown chunk type {0}", nextSubChunk.ChunkID));
			}
			if (!flag)
			{
				throw new InvalidDataException("Missing SoundFont version information");
			}
			if (!flag2)
			{
				throw new InvalidDataException("Missing wavetable sound engine information");
			}
			if (!flag3)
			{
				throw new InvalidDataException("Missing SoundFont name information");
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x0000DA28 File Offset: 0x0000BC28
		public SFVersion SoundFontVersion
		{
			get
			{
				return this.verSoundFont;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x0000DA30 File Offset: 0x0000BC30
		// (set) Token: 0x060003F8 RID: 1016 RVA: 0x0000DA38 File Offset: 0x0000BC38
		public string WaveTableSoundEngine
		{
			get
			{
				return this.waveTableSoundEngine;
			}
			set
			{
				this.waveTableSoundEngine = value;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x0000DA41 File Offset: 0x0000BC41
		// (set) Token: 0x060003FA RID: 1018 RVA: 0x0000DA49 File Offset: 0x0000BC49
		public string BankName
		{
			get
			{
				return this.bankName;
			}
			set
			{
				this.bankName = value;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x0000DA52 File Offset: 0x0000BC52
		// (set) Token: 0x060003FC RID: 1020 RVA: 0x0000DA5A File Offset: 0x0000BC5A
		public string DataROM
		{
			get
			{
				return this.dataROM;
			}
			set
			{
				this.dataROM = value;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x0000DA63 File Offset: 0x0000BC63
		// (set) Token: 0x060003FE RID: 1022 RVA: 0x0000DA6B File Offset: 0x0000BC6B
		public string CreationDate
		{
			get
			{
				return this.creationDate;
			}
			set
			{
				this.creationDate = value;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x0000DA74 File Offset: 0x0000BC74
		// (set) Token: 0x06000400 RID: 1024 RVA: 0x0000DA7C File Offset: 0x0000BC7C
		public string Author
		{
			get
			{
				return this.author;
			}
			set
			{
				this.author = value;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x0000DA85 File Offset: 0x0000BC85
		// (set) Token: 0x06000402 RID: 1026 RVA: 0x0000DA8D File Offset: 0x0000BC8D
		public string TargetProduct
		{
			get
			{
				return this.targetProduct;
			}
			set
			{
				this.targetProduct = value;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x0000DA96 File Offset: 0x0000BC96
		// (set) Token: 0x06000404 RID: 1028 RVA: 0x0000DA9E File Offset: 0x0000BC9E
		public string Copyright
		{
			get
			{
				return this.copyright;
			}
			set
			{
				this.copyright = value;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x0000DAA7 File Offset: 0x0000BCA7
		// (set) Token: 0x06000406 RID: 1030 RVA: 0x0000DAAF File Offset: 0x0000BCAF
		public string Comments
		{
			get
			{
				return this.comments;
			}
			set
			{
				this.comments = value;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x0000DAB8 File Offset: 0x0000BCB8
		// (set) Token: 0x06000408 RID: 1032 RVA: 0x0000DAC0 File Offset: 0x0000BCC0
		public string Tools
		{
			get
			{
				return this.tools;
			}
			set
			{
				this.tools = value;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x0000DAC9 File Offset: 0x0000BCC9
		// (set) Token: 0x0600040A RID: 1034 RVA: 0x0000DAD1 File Offset: 0x0000BCD1
		public SFVersion ROMVersion
		{
			get
			{
				return this.verROM;
			}
			set
			{
				this.verROM = value;
			}
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000DADC File Offset: 0x0000BCDC
		public override string ToString()
		{
			return string.Format("Bank Name: {0}\r\nAuthor: {1}\r\nCopyright: {2}\r\nCreation Date: {3}\r\nTools: {4}\r\nComments: {5}\r\nSound Engine: {6}\r\nSoundFont Version: {7}\r\nTarget Product: {8}\r\nData ROM: {9}\r\nROM Version: {10}", new object[]
			{
				this.BankName, this.Author, this.Copyright, this.CreationDate, this.Tools, "TODO-fix comments", this.WaveTableSoundEngine, this.SoundFontVersion, this.TargetProduct, this.DataROM,
				this.ROMVersion
			});
		}

		// Token: 0x040004D7 RID: 1239
		private SFVersion verSoundFont;

		// Token: 0x040004D8 RID: 1240
		private string waveTableSoundEngine;

		// Token: 0x040004D9 RID: 1241
		private string bankName;

		// Token: 0x040004DA RID: 1242
		private string dataROM;

		// Token: 0x040004DB RID: 1243
		private string creationDate;

		// Token: 0x040004DC RID: 1244
		private string author;

		// Token: 0x040004DD RID: 1245
		private string targetProduct;

		// Token: 0x040004DE RID: 1246
		private string copyright;

		// Token: 0x040004DF RID: 1247
		private string comments;

		// Token: 0x040004E0 RID: 1248
		private string tools;

		// Token: 0x040004E1 RID: 1249
		private SFVersion verROM;
	}
}
