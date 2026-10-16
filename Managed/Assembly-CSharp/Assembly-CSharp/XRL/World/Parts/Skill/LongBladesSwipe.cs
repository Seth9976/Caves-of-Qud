using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001351 RID: 4945
	[Serializable]
	public class LongBladesSwipe : LongBladesSkillBase
	{
		// Token: 0x0600C0B3 RID: 49331 RVA: 0x00468534 File Offset: 0x00466734
		public override bool AddSkill(GameObject GO)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Swipe", "CommandSwipe", "Skills", null, "\u009f", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.AddSkill(GO);
		}

		// Token: 0x0600C0B4 RID: 49332 RVA: 0x0046857C File Offset: 0x0046677C
		public override bool RemoveSkill(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.RemoveSkill(GO);
		}

		// Token: 0x0600C0B5 RID: 49333 RVA: 0x00468593 File Offset: 0x00466793
		public void CollectStats(Templates.StatCollector stats)
		{
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), LongBladesSwipe.COOLDOWN, 0);
		}

		// Token: 0x0600C0B6 RID: 49334 RVA: 0x004685AE File Offset: 0x004667AE
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x0600C0B7 RID: 49335 RVA: 0x004685C4 File Offset: 0x004667C4
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.ActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			return base.HandleEvent(E);
		}

		// Token: 0x04004D54 RID: 19796
		public Guid ActivatedAbilityID = Guid.Empty;

		// Token: 0x04004D55 RID: 19797
		public static readonly int COOLDOWN = 15;
	}
}
