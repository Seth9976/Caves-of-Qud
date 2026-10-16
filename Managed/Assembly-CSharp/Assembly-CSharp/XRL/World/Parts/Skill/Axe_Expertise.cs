using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001320 RID: 4896
	[Serializable]
	public class Axe_Expertise : BaseSkill
	{
		// Token: 0x0600BF78 RID: 49016 RVA: 0x004623E8 File Offset: 0x004605E8
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetToHitModifierEvent>.ID;
		}

		// Token: 0x0600BF79 RID: 49017 RVA: 0x00462400 File Offset: 0x00460600
		public override bool HandleEvent(GetToHitModifierEvent E)
		{
			if (E.Actor == this.ParentObject && E.Checking == "Actor" && E.Skill == "Axe" && E.Melee)
			{
				E.Modifier += this.HitBonus;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BF7A RID: 49018 RVA: 0x00462461 File Offset: 0x00460661
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x04004D1E RID: 19742
		public int HitBonus = 2;
	}
}
