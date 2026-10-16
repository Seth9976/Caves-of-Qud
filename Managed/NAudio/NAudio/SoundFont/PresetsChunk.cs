using System;
using System.Collections.Generic;
using System.Text;

namespace NAudio.SoundFont
{
	// Token: 0x020000C1 RID: 193
	public class PresetsChunk
	{
		// Token: 0x0600043B RID: 1083 RVA: 0x0000E090 File Offset: 0x0000C290
		internal PresetsChunk(RiffChunk chunk)
		{
			string text = chunk.ReadChunkID();
			if (text != "pdta")
			{
				throw new InvalidDataException(string.Format("Not a presets data chunk ({0})", text));
			}
			RiffChunk nextSubChunk;
			while ((nextSubChunk = chunk.GetNextSubChunk()) != null)
			{
				string chunkID;
				if ((chunkID = nextSubChunk.ChunkID) != null)
				{
					if (<PrivateImplementationDetails>{FA168FAF-488C-4AC3-8A7C-7C759315A688}.$$method0x6000428-1 == null)
					{
						Dictionary<string, int> dictionary = new Dictionary<string, int>(18);
						dictionary.Add("PHDR", 0);
						dictionary.Add("phdr", 1);
						dictionary.Add("PBAG", 2);
						dictionary.Add("pbag", 3);
						dictionary.Add("PMOD", 4);
						dictionary.Add("pmod", 5);
						dictionary.Add("PGEN", 6);
						dictionary.Add("pgen", 7);
						dictionary.Add("INST", 8);
						dictionary.Add("inst", 9);
						dictionary.Add("IBAG", 10);
						dictionary.Add("ibag", 11);
						dictionary.Add("IMOD", 12);
						dictionary.Add("imod", 13);
						dictionary.Add("IGEN", 14);
						dictionary.Add("igen", 15);
						dictionary.Add("SHDR", 16);
						dictionary.Add("shdr", 17);
						<PrivateImplementationDetails>{FA168FAF-488C-4AC3-8A7C-7C759315A688}.$$method0x6000428-1 = dictionary;
					}
					int num;
					if (<PrivateImplementationDetails>{FA168FAF-488C-4AC3-8A7C-7C759315A688}.$$method0x6000428-1.TryGetValue(chunkID, ref num))
					{
						switch (num)
						{
						case 0:
						case 1:
							nextSubChunk.GetDataAsStructureArray<Preset>(this.presetHeaders);
							continue;
						case 2:
						case 3:
							nextSubChunk.GetDataAsStructureArray<Zone>(this.presetZones);
							continue;
						case 4:
						case 5:
							nextSubChunk.GetDataAsStructureArray<Modulator>(this.presetZoneModulators);
							continue;
						case 6:
						case 7:
							nextSubChunk.GetDataAsStructureArray<Generator>(this.presetZoneGenerators);
							continue;
						case 8:
						case 9:
							nextSubChunk.GetDataAsStructureArray<Instrument>(this.instruments);
							continue;
						case 10:
						case 11:
							nextSubChunk.GetDataAsStructureArray<Zone>(this.instrumentZones);
							continue;
						case 12:
						case 13:
							nextSubChunk.GetDataAsStructureArray<Modulator>(this.instrumentZoneModulators);
							continue;
						case 14:
						case 15:
							nextSubChunk.GetDataAsStructureArray<Generator>(this.instrumentZoneGenerators);
							continue;
						case 16:
						case 17:
							nextSubChunk.GetDataAsStructureArray<SampleHeader>(this.sampleHeaders);
							continue;
						}
					}
				}
				throw new InvalidDataException(string.Format("Unknown chunk type {0}", nextSubChunk.ChunkID));
			}
			this.instrumentZoneGenerators.Load(this.sampleHeaders.SampleHeaders);
			this.instrumentZones.Load(this.instrumentZoneModulators.Modulators, this.instrumentZoneGenerators.Generators);
			this.instruments.LoadZones(this.instrumentZones.Zones);
			this.presetZoneGenerators.Load(this.instruments.Instruments);
			this.presetZones.Load(this.presetZoneModulators.Modulators, this.presetZoneGenerators.Generators);
			this.presetHeaders.LoadZones(this.presetZones.Zones);
			this.sampleHeaders.RemoveEOS();
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x0000E3EF File Offset: 0x0000C5EF
		public Preset[] Presets
		{
			get
			{
				return this.presetHeaders.Presets;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x0000E3FC File Offset: 0x0000C5FC
		public Instrument[] Instruments
		{
			get
			{
				return this.instruments.Instruments;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x0000E409 File Offset: 0x0000C609
		public SampleHeader[] SampleHeaders
		{
			get
			{
				return this.sampleHeaders.SampleHeaders;
			}
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0000E418 File Offset: 0x0000C618
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Preset Headers:\r\n");
			foreach (Preset preset in this.presetHeaders.Presets)
			{
				stringBuilder.AppendFormat("{0}\r\n", preset);
			}
			stringBuilder.Append("Instruments:\r\n");
			foreach (Instrument instrument in this.instruments.Instruments)
			{
				stringBuilder.AppendFormat("{0}\r\n", instrument);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400050B RID: 1291
		private PresetBuilder presetHeaders = new PresetBuilder();

		// Token: 0x0400050C RID: 1292
		private ZoneBuilder presetZones = new ZoneBuilder();

		// Token: 0x0400050D RID: 1293
		private ModulatorBuilder presetZoneModulators = new ModulatorBuilder();

		// Token: 0x0400050E RID: 1294
		private GeneratorBuilder presetZoneGenerators = new GeneratorBuilder();

		// Token: 0x0400050F RID: 1295
		private InstrumentBuilder instruments = new InstrumentBuilder();

		// Token: 0x04000510 RID: 1296
		private ZoneBuilder instrumentZones = new ZoneBuilder();

		// Token: 0x04000511 RID: 1297
		private ModulatorBuilder instrumentZoneModulators = new ModulatorBuilder();

		// Token: 0x04000512 RID: 1298
		private GeneratorBuilder instrumentZoneGenerators = new GeneratorBuilder();

		// Token: 0x04000513 RID: 1299
		private SampleHeaderBuilder sampleHeaders = new SampleHeaderBuilder();
	}
}
