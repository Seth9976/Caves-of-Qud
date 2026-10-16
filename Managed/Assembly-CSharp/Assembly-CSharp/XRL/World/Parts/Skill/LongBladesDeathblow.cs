using System;
using XRL.World.Effects;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001349 RID: 4937
	[Serializable]
	public class LongBladesDeathblow : LongBladesSkillBase
	{
		// Token: 0x0600C08E RID: 49294 RVA: 0x00468140 File Offset: 0x00466340
		public override bool AddSkill(GameObject GO)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("En Garde!", "CommandDeathblow", "Skills", null, "\u009f", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.AddSkill(GO);
		}

		// Token: 0x0600C08F RID: 49295 RVA: 0x00468188 File Offset: 0x00466388
		public override bool RemoveSkill(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.RemoveSkill(GO);
		}

		// Token: 0x0600C090 RID: 49296 RVA: 0x0046819F File Offset: 0x0046639F
		public void CollectStats(Templates.StatCollector stats)
		{
			stats.Set("Duration", LongBladesDeathblow.DURATION, false, 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), LongBladesDeathblow.COOLDOWN, 0);
		}

		// Token: 0x0600C091 RID: 49297 RVA: 0x004681CC File Offset: 0x004663CC
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CanRefreshAbilityEvent>.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x0600C092 RID: 49298 RVA: 0x004681EA File Offset: 0x004663EA
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.ActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600C093 RID: 49299 RVA: 0x0046820D File Offset: 0x0046640D
		public override bool HandleEvent(CanRefreshAbilityEvent E)
		{
			return (!(E.Ability.ID == this.ActivatedAbilityID) || !this.ParentObject.HasEffect(typeof(LongbladeEffect_EnGarde))) && base.HandleEvent(E);
		}

		// Token: 0x04004D4E RID: 19790
		public Guid ActivatedAbilityID = Guid.Empty;

		// Token: 0x04004D4F RID: 19791
		public static readonly int COOLDOWN = 100;

		// Token: 0x04004D50 RID: 19792
		public static readonly int DURATION = 10;
	}
}
