using System;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000CF4 RID: 3316
	[Serializable]
	public class CookingDomainHightierRegen_Heal40to50_ProceduralCookingTriggeredAction : ProceduralCookingTriggeredAction
	{
		// Token: 0x0600835A RID: 33626 RVA: 0x002FAB1B File Offset: 0x002F8D1B
		public override string GetDescription()
		{
			return "@they heal 40-50 HP.";
		}

		// Token: 0x0600835B RID: 33627 RVA: 0x002FAB22 File Offset: 0x002F8D22
		public override string GetNotification()
		{
			return "@their wounds heal significantly.";
		}

		// Token: 0x0600835C RID: 33628 RVA: 0x002FAB29 File Offset: 0x002F8D29
		public override void Apply(GameObject go)
		{
			go.Statistics["Hitpoints"].Penalty -= Stat.Roll("40-50", null);
		}
	}
}
