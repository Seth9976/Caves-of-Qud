using System;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000CBB RID: 3259
	[Serializable]
	public class CookingDomainHP_IncreaseHP_ProceduralCookingTriggeredAction : ProceduralCookingTriggeredAction
	{
		// Token: 0x06008226 RID: 33318 RVA: 0x002F7440 File Offset: 0x002F5640
		public override void Init(GameObject target)
		{
			this.Tier = Stat.Random(30, 40);
			base.Init(target);
		}

		// Token: 0x06008227 RID: 33319 RVA: 0x002F7458 File Offset: 0x002F5658
		public override string GetDescription()
		{
			return "@they get +" + this.Tier.ToString() + "% max HP for 1 hour.";
		}

		// Token: 0x06008228 RID: 33320 RVA: 0x002F7474 File Offset: 0x002F5674
		public override string GetTemplatedDescription()
		{
			return "@they get +30-40% max HP for 1 hour.";
		}

		// Token: 0x06008229 RID: 33321 RVA: 0x002F747B File Offset: 0x002F567B
		public override string GetNotification()
		{
			return "@they become heartier.";
		}

		// Token: 0x0600822A RID: 33322 RVA: 0x002F7482 File Offset: 0x002F5682
		public override void Apply(GameObject go)
		{
			if (go.IsPlayer() && !go.HasEffect<CookingDomainHP_IncreaseHP_ProceduralCookingTriggeredActionEffect>())
			{
				go.ApplyEffect(new CookingDomainHP_IncreaseHP_ProceduralCookingTriggeredActionEffect(this.Tier), null);
			}
		}

		// Token: 0x0400392E RID: 14638
		public int Tier;
	}
}
