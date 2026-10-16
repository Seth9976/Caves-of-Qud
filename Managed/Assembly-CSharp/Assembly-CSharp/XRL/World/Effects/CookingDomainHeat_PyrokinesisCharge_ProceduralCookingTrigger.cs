using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000CA9 RID: 3241
	[Serializable]
	public class CookingDomainHeat_PyrokinesisCharge_ProceduralCookingTriggeredAction : ProceduralCookingTriggeredAction
	{
		// Token: 0x060081E6 RID: 33254 RVA: 0x002F6EF6 File Offset: 0x002F50F6
		public override string GetDescription()
		{
			return "@they toast an area per Pyrokinesis at level 5-6.";
		}

		// Token: 0x060081E7 RID: 33255 RVA: 0x002F6EFD File Offset: 0x002F50FD
		public override string GetNotification()
		{
			return "@they toast an area with pyrokinesis.";
		}

		// Token: 0x060081E8 RID: 33256 RVA: 0x002F6F04 File Offset: 0x002F5104
		public override void Apply(GameObject go)
		{
			if (go.IsPlayer())
			{
				Pyrokinesis.Cast(null, "5-6", null, null, null, 0);
			}
		}
	}
}
