using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200027A RID: 634
	[Serializable]
	public class ListUsersCharactersResult : PlayFabResultCommon
	{
		// Token: 0x04000D07 RID: 3335
		public List<CharacterResult> Characters;
	}
}
