using System;

namespace NAudio.SoundFont
{
	// Token: 0x020000B7 RID: 183
	public class Instrument
	{
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x0000DB60 File Offset: 0x0000BD60
		// (set) Token: 0x0600040D RID: 1037 RVA: 0x0000DB68 File Offset: 0x0000BD68
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

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x0000DB71 File Offset: 0x0000BD71
		// (set) Token: 0x0600040F RID: 1039 RVA: 0x0000DB79 File Offset: 0x0000BD79
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

		// Token: 0x06000410 RID: 1040 RVA: 0x0000DB82 File Offset: 0x0000BD82
		public override string ToString()
		{
			return this.name;
		}

		// Token: 0x040004E2 RID: 1250
		private string name;

		// Token: 0x040004E3 RID: 1251
		internal ushort startInstrumentZoneIndex;

		// Token: 0x040004E4 RID: 1252
		internal ushort endInstrumentZoneIndex;

		// Token: 0x040004E5 RID: 1253
		private Zone[] zones;
	}
}
