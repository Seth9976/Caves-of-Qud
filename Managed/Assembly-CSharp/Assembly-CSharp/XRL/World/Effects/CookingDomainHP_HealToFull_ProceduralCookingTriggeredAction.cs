using System;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000CBA RID: 3258
	[Serializable]
	public class CookingDomainHP_HealToFull_ProceduralCookingTriggeredAction : ProceduralCookingTriggeredAction
	{
		// Token: 0x06008222 RID: 33314 RVA: 0x002F73ED File Offset: 0x002F55ED
		public override string GetDescription()
		{
			return "@they heal to full 15% of the time.";
		}

		// Token: 0x06008223 RID: 33315 RVA: 0x002F73F4 File Offset: 0x002F55F4
		public override string GetNotification()
		{
			if (this.didFire)
			{
				return "@they heal to full.";
			}
			return null;
		}

		// Token: 0x06008224 RID: 33316 RVA: 0x002F7405 File Offset: 0x002F5605
		public override void Apply(GameObject go)
		{
			if (Stat.Random(1, 100) <= 15)
			{
				this.didFire = true;
				go.Statistics["Hitpoints"].Penalty = 0;
				return;
			}
			this.didFire = false;
		}

		// Token: 0x0400392D RID: 14637
		private bool didFire;
	}
}
