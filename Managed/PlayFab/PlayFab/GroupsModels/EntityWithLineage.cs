using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	// Token: 0x0200011F RID: 287
	[Serializable]
	public class EntityWithLineage : PlayFabBaseModel
	{
		// Token: 0x0400062B RID: 1579
		public EntityKey Key;

		// Token: 0x0400062C RID: 1580
		public Dictionary<string, EntityKey> Lineage;
	}
}
