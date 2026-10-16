using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x0200134E RID: 4942
	[Serializable]
	public class LongBladesLunge : LongBladesSkillBase
	{
		// Token: 0x0600C0A5 RID: 49317 RVA: 0x004683F0 File Offset: 0x004665F0
		public override bool AddSkill(GameObject GO)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Lunge", "CommandLunge", "Skills", null, "\u009f", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.AddSkill(GO);
		}

		// Token: 0x0600C0A6 RID: 49318 RVA: 0x00468438 File Offset: 0x00466638
		public override bool RemoveSkill(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.RemoveSkill(GO);
		}

		// Token: 0x0600C0A7 RID: 49319 RVA: 0x0046844F File Offset: 0x0046664F
		public void CollectStats(Templates.StatCollector stats)
		{
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), LongBladesLunge.COOLDOWN, 0);
		}

		// Token: 0x0600C0A8 RID: 49320 RVA: 0x0046846A File Offset: 0x0046666A
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x0600C0A9 RID: 49321 RVA: 0x00468480 File Offset: 0x00466680
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.ActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			return base.HandleEvent(E);
		}

		// Token: 0x04004D52 RID: 19794
		public Guid ActivatedAbilityID = Guid.Empty;

		// Token: 0x04004D53 RID: 19795
		public static readonly int COOLDOWN = 15;
	}
}
