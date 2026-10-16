using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000C86 RID: 3206
	[Serializable]
	public class CookingDomainCold_CryokinesisCharge_ProceduralCookingTriggeredAction : ProceduralCookingTriggeredAction
	{
		// Token: 0x06008162 RID: 33122 RVA: 0x002F6329 File Offset: 0x002F4529
		public override string GetDescription()
		{
			return "@they freeze an area per Cryokinesis at level 5-6.";
		}

		// Token: 0x06008163 RID: 33123 RVA: 0x002F6330 File Offset: 0x002F4530
		public override string GetNotification()
		{
			return "@they freeze an area with cryokinesis.";
		}

		// Token: 0x06008164 RID: 33124 RVA: 0x002F6338 File Offset: 0x002F4538
		public override void Apply(GameObject go)
		{
			if (go.IsPlayer())
			{
				Cryokinesis.Cast(null, "5-6", null, null, null, 0);
			}
		}
	}
}
