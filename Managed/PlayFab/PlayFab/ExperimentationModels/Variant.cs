using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ExperimentationModels
{
	// Token: 0x02000157 RID: 343
	[Serializable]
	public class Variant : PlayFabBaseModel
	{
		// Token: 0x040006E7 RID: 1767
		public string Description;

		// Token: 0x040006E8 RID: 1768
		public string Id;

		// Token: 0x040006E9 RID: 1769
		public bool IsControl;

		// Token: 0x040006EA RID: 1770
		public string Name;

		// Token: 0x040006EB RID: 1771
		public string TitleDataOverrideLabel;

		// Token: 0x040006EC RID: 1772
		public uint TrafficPercentage;

		// Token: 0x040006ED RID: 1773
		public List<Variable> Variables;
	}
}
