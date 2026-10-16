using System;
using System.Collections.Generic;

namespace XRL.CharacterBuilds.Qud
{
	// Token: 0x02000595 RID: 1429
	public class QudAttributesModuleData : AbstractEmbarkBuilderModuleData
	{
		// Token: 0x04001E76 RID: 7798
		public Dictionary<string, int> PointsPurchased = new Dictionary<string, int>();

		// Token: 0x04001E77 RID: 7799
		public int apSpent;

		// Token: 0x04001E78 RID: 7800
		public int apRemaining;

		// Token: 0x04001E79 RID: 7801
		public int baseAp;
	}
}
