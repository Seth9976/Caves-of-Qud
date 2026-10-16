using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000C85 RID: 3205
	[Serializable]
	public class CookingDomainCold_FreezingHandsCharge_ProceduralCookingTriggeredAction : ProceduralCookingTriggeredAction
	{
		// Token: 0x0600815E RID: 33118 RVA: 0x002F62FD File Offset: 0x002F44FD
		public override string GetDescription()
		{
			return "@they emit a ray of frost per Freezing Hands at level 5-6.";
		}

		// Token: 0x0600815F RID: 33119 RVA: 0x002F6304 File Offset: 0x002F4504
		public override string GetNotification()
		{
			return "@they emit a powerful ray of frost.";
		}

		// Token: 0x06008160 RID: 33120 RVA: 0x002F630B File Offset: 0x002F450B
		public override void Apply(GameObject go)
		{
			if (go.IsPlayer())
			{
				FreezingRay.Cast(null, "5-6");
			}
		}
	}
}
