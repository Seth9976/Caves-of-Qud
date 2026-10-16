using System;
using System.Collections.Generic;
using Genkit;
using XRL.World;

namespace XRL
{
	// Token: 0x0200053E RID: 1342
	public class ZoneTemplateGenerationContext
	{
		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x0600398C RID: 14732 RVA: 0x0011C1AD File Offset: 0x0011A3AD
		public InfluenceMapRegion currentSector
		{
			get
			{
				return this.Regions.Regions[this.CurrentRegion];
			}
		}

		// Token: 0x04001CF7 RID: 7415
		public Zone Z;

		// Token: 0x04001CF8 RID: 7416
		public InfluenceMap Regions;

		// Token: 0x04001CF9 RID: 7417
		public Dictionary<string, string> Variables = new Dictionary<string, string>();

		// Token: 0x04001CFA RID: 7418
		public List<int> PopulatedRegions = new List<int>();

		// Token: 0x04001CFB RID: 7419
		public int CurrentRegion;
	}
}
