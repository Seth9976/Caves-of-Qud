using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001389 RID: 5001
	[Serializable]
	public class ShortBlades_Expertise : BaseSkill
	{
		// Token: 0x0600C1E0 RID: 49632 RVA: 0x0046D7ED File Offset: 0x0046B9ED
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetToHitModifierEvent>.ID;
		}

		// Token: 0x0600C1E1 RID: 49633 RVA: 0x0046D804 File Offset: 0x0046BA04
		public override bool HandleEvent(GetToHitModifierEvent E)
		{
			if (E.Actor == this.ParentObject && E.Checking == "Actor" && E.Skill == "ShortBlades")
			{
				E.Modifier += this.HitBonus;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C1E2 RID: 49634 RVA: 0x0046D85D File Offset: 0x0046BA5D
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x04004D85 RID: 19845
		public int HitBonus = 1;
	}
}
