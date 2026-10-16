using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020001CF RID: 463
	[Serializable]
	public class CollectionFilter : PlayFabBaseModel
	{
		// Token: 0x040009BC RID: 2492
		public List<Container_Dictionary_String_String> Excludes;

		// Token: 0x040009BD RID: 2493
		public List<Container_Dictionary_String_String> Includes;
	}
}
