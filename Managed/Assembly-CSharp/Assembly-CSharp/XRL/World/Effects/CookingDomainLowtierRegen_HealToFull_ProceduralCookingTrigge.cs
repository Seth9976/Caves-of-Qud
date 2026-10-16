using System;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000CF3 RID: 3315
	[Serializable]
	public class CookingDomainLowtierRegen_HealToFull_ProceduralCookingTriggeredAction : ProceduralCookingTriggeredAction
	{
		// Token: 0x06008356 RID: 33622 RVA: 0x002FAADC File Offset: 0x002F8CDC
		public override string GetDescription()
		{
			return "@they heal 15-20 HP.";
		}

		// Token: 0x06008357 RID: 33623 RVA: 0x002FAAE3 File Offset: 0x002F8CE3
		public override string GetNotification()
		{
			return "@their wounds heal a bit.";
		}

		// Token: 0x06008358 RID: 33624 RVA: 0x002FAAEA File Offset: 0x002F8CEA
		public override void Apply(GameObject go)
		{
			go.Statistics["Hitpoints"].Penalty -= Stat.Roll("15-20", null);
		}
	}
}
