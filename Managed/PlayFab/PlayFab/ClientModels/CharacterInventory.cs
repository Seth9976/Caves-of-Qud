using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020001CB RID: 459
	[Serializable]
	public class CharacterInventory : PlayFabBaseModel
	{
		// Token: 0x040009AC RID: 2476
		public string CharacterId;

		// Token: 0x040009AD RID: 2477
		public List<ItemInstance> Inventory;
	}
}
