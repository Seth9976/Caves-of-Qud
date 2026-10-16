using System;
using PlayFab.SharedModels;

namespace PlayFab.ProfilesModels
{
	// Token: 0x02000041 RID: 65
	[Serializable]
	public class EntityPermissionStatement : PlayFabBaseModel
	{
		// Token: 0x040002FA RID: 762
		public string Action;

		// Token: 0x040002FB RID: 763
		public string Comment;

		// Token: 0x040002FC RID: 764
		public object Condition;

		// Token: 0x040002FD RID: 765
		public EffectType Effect;

		// Token: 0x040002FE RID: 766
		public object Principal;

		// Token: 0x040002FF RID: 767
		public string Resource;
	}
}
