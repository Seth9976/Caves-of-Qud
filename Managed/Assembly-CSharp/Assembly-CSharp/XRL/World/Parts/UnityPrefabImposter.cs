using System;

namespace XRL.World.Parts
{
	// Token: 0x020012F0 RID: 4848
	[Obsolete]
	[Serializable]
	public class UnityPrefabImposter : IPartWithPrefabImposter
	{
		// Token: 0x0600BE61 RID: 48737 RVA: 0x0045BC96 File Offset: 0x00459E96
		public override bool SameAs(IPart p)
		{
			return !((p as UnityPrefabImposter).PrefabID != this.PrefabID) && base.SameAs(p);
		}

		// Token: 0x0600BE62 RID: 48738 RVA: 0x0045BCB9 File Offset: 0x00459EB9
		public override void Initialize()
		{
			this.prefabID = this.PrefabID;
			base.Initialize();
		}

		// Token: 0x04004CA7 RID: 19623
		public string PrefabID;
	}
}
