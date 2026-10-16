using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000980 RID: 2432
	public class ZoneEncounterBlueprint
	{
		// Token: 0x060066EC RID: 26348 RVA: 0x00200029 File Offset: 0x001FE229
		public ZoneEncounterBlueprint()
		{
			this.Table = "";
			this.Amount = "";
		}

		// Token: 0x060066ED RID: 26349 RVA: 0x0020005D File Offset: 0x001FE25D
		public ZoneEncounterBlueprint(string Table)
		{
			this.Table = Table;
			this.Amount = "medium";
		}

		// Token: 0x060066EE RID: 26350 RVA: 0x0020008D File Offset: 0x001FE28D
		public override string ToString()
		{
			return "ZoneEncounter<" + this.Table + ">";
		}

		// Token: 0x04002F71 RID: 12145
		public string Table;

		// Token: 0x04002F72 RID: 12146
		public string Amount;

		// Token: 0x04002F73 RID: 12147
		public Dictionary<string, string> Parameters = new Dictionary<string, string>();

		// Token: 0x04002F74 RID: 12148
		public List<ZoneFeature> Features = new List<ZoneFeature>();
	}
}
