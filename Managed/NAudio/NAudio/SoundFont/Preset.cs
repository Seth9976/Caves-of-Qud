using System;

namespace NAudio.SoundFont
{
	// Token: 0x020000BF RID: 191
	public class Preset
	{
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x0000DEBE File Offset: 0x0000C0BE
		// (set) Token: 0x0600042C RID: 1068 RVA: 0x0000DEC6 File Offset: 0x0000C0C6
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x0000DECF File Offset: 0x0000C0CF
		// (set) Token: 0x0600042E RID: 1070 RVA: 0x0000DED7 File Offset: 0x0000C0D7
		public ushort PatchNumber
		{
			get
			{
				return this.patchNumber;
			}
			set
			{
				this.patchNumber = value;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x0000DEE0 File Offset: 0x0000C0E0
		// (set) Token: 0x06000430 RID: 1072 RVA: 0x0000DEE8 File Offset: 0x0000C0E8
		public ushort Bank
		{
			get
			{
				return this.bank;
			}
			set
			{
				this.bank = value;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x0000DEF1 File Offset: 0x0000C0F1
		// (set) Token: 0x06000432 RID: 1074 RVA: 0x0000DEF9 File Offset: 0x0000C0F9
		public Zone[] Zones
		{
			get
			{
				return this.zones;
			}
			set
			{
				this.zones = value;
			}
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0000DF02 File Offset: 0x0000C102
		public override string ToString()
		{
			return string.Format("{0}-{1} {2}", this.bank, this.patchNumber, this.name);
		}

		// Token: 0x04000501 RID: 1281
		private string name;

		// Token: 0x04000502 RID: 1282
		private ushort patchNumber;

		// Token: 0x04000503 RID: 1283
		private ushort bank;

		// Token: 0x04000504 RID: 1284
		internal ushort startPresetZoneIndex;

		// Token: 0x04000505 RID: 1285
		internal ushort endPresetZoneIndex;

		// Token: 0x04000506 RID: 1286
		internal uint library;

		// Token: 0x04000507 RID: 1287
		internal uint genre;

		// Token: 0x04000508 RID: 1288
		internal uint morphology;

		// Token: 0x04000509 RID: 1289
		private Zone[] zones;
	}
}
