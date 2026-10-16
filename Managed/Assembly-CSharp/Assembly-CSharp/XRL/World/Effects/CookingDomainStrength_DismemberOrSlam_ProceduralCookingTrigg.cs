using System;
using XRL.Rules;
using XRL.World.Parts.Skill;

namespace XRL.World.Effects
{
	// Token: 0x02000D0E RID: 3342
	[Serializable]
	public class CookingDomainStrength_DismemberOrSlam_ProceduralCookingTriggeredAction : ProceduralCookingTriggeredAction
	{
		// Token: 0x060083D4 RID: 33748 RVA: 0x002FB931 File Offset: 0x002F9B31
		public override string GetDescription()
		{
			return "@they have a 50% chance to Dismember or Slam @their opponent.";
		}

		// Token: 0x060083D5 RID: 33749 RVA: 0x002FB938 File Offset: 0x002F9B38
		public override string GetNotification()
		{
			if (this.didFire)
			{
				return "@they perform@s an act of brutal violence.";
			}
			return null;
		}

		// Token: 0x060083D6 RID: 33750 RVA: 0x002FB94C File Offset: 0x002F9B4C
		public override void Apply(GameObject go)
		{
			if (go.IsPlayer())
			{
				if (Stat.Random(1, 100) <= 50)
				{
					if (Stat.Random(1, 100) <= 50)
					{
						Cudgel_Slam.Cast(go, null, null, null, true, int.MinValue, null);
					}
					else
					{
						Axe_Dismember.CastForceSuccess(go, null, null);
					}
					this.didFire = true;
					return;
				}
				this.didFire = false;
			}
		}

		// Token: 0x0400396C RID: 14700
		private bool didFire;
	}
}
