using System;

namespace NAudio.SoundFont
{
	// Token: 0x020000CB RID: 203
	public class Zone
	{
		// Token: 0x06000469 RID: 1129 RVA: 0x0000EAA8 File Offset: 0x0000CCA8
		public override string ToString()
		{
			return string.Format("Zone {0} Gens:{1} {2} Mods:{3}", new object[] { this.generatorCount, this.generatorIndex, this.modulatorCount, this.modulatorIndex });
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x0000EAFF File Offset: 0x0000CCFF
		// (set) Token: 0x0600046B RID: 1131 RVA: 0x0000EB07 File Offset: 0x0000CD07
		public Modulator[] Modulators
		{
			get
			{
				return this.modulators;
			}
			set
			{
				this.modulators = value;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x0000EB10 File Offset: 0x0000CD10
		// (set) Token: 0x0600046D RID: 1133 RVA: 0x0000EB18 File Offset: 0x0000CD18
		public Generator[] Generators
		{
			get
			{
				return this.generators;
			}
			set
			{
				this.generators = value;
			}
		}

		// Token: 0x04000536 RID: 1334
		internal ushort generatorIndex;

		// Token: 0x04000537 RID: 1335
		internal ushort modulatorIndex;

		// Token: 0x04000538 RID: 1336
		internal ushort generatorCount;

		// Token: 0x04000539 RID: 1337
		internal ushort modulatorCount;

		// Token: 0x0400053A RID: 1338
		private Modulator[] modulators;

		// Token: 0x0400053B RID: 1339
		private Generator[] generators;
	}
}
