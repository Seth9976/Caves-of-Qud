using System;
using System.IO;

namespace NAudio.SoundFont
{
	// Token: 0x020000CC RID: 204
	internal class ZoneBuilder : StructureBuilder<Zone>
	{
		// Token: 0x0600046F RID: 1135 RVA: 0x0000EB2C File Offset: 0x0000CD2C
		public override Zone Read(BinaryReader br)
		{
			Zone zone = new Zone();
			zone.generatorIndex = br.ReadUInt16();
			zone.modulatorIndex = br.ReadUInt16();
			if (this.lastZone != null)
			{
				this.lastZone.generatorCount = zone.generatorIndex - this.lastZone.generatorIndex;
				this.lastZone.modulatorCount = zone.modulatorIndex - this.lastZone.modulatorIndex;
			}
			this.data.Add(zone);
			this.lastZone = zone;
			return zone;
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0000EBAF File Offset: 0x0000CDAF
		public override void Write(BinaryWriter bw, Zone zone)
		{
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0000EBB4 File Offset: 0x0000CDB4
		public void Load(Modulator[] modulators, Generator[] generators)
		{
			for (int i = 0; i < this.data.Count - 1; i++)
			{
				Zone zone = this.data[i];
				zone.Generators = new Generator[(int)zone.generatorCount];
				Array.Copy(generators, (int)zone.generatorIndex, zone.Generators, 0, (int)zone.generatorCount);
				zone.Modulators = new Modulator[(int)zone.modulatorCount];
				Array.Copy(modulators, (int)zone.modulatorIndex, zone.Modulators, 0, (int)zone.modulatorCount);
			}
			this.data.RemoveAt(this.data.Count - 1);
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x0000EC52 File Offset: 0x0000CE52
		public Zone[] Zones
		{
			get
			{
				return this.data.ToArray();
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x0000EC5F File Offset: 0x0000CE5F
		public override int Length
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x0400053C RID: 1340
		private Zone lastZone;
	}
}
