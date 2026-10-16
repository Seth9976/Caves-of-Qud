using System;

namespace XRL.World.Effects
{
	// Token: 0x02000C4D RID: 3149
	[Serializable]
	public class CookingDomainAgility_LargeAgilityBuff_ProceduralCookingTriggeredAction : ProceduralCookingTriggeredAction
	{
		// Token: 0x0600808F RID: 32911 RVA: 0x002F4CC9 File Offset: 0x002F2EC9
		public override string GetDescription()
		{
			return "@they gain@s +8 Agility for 50 turns.";
		}

		// Token: 0x06008090 RID: 32912 RVA: 0x002F4CD0 File Offset: 0x002F2ED0
		public override string GetNotification()
		{
			return "@they assume a limber pose.";
		}

		// Token: 0x06008091 RID: 32913 RVA: 0x002F4CD7 File Offset: 0x002F2ED7
		public override void Apply(GameObject go)
		{
			if (!go.HasEffect<CookingDomainAgility_LargeAgilityBuff_ProceduralCookingTriggeredActionEffect>())
			{
				go.ApplyEffect(new CookingDomainAgility_LargeAgilityBuff_ProceduralCookingTriggeredActionEffect(), null);
			}
		}
	}
}
