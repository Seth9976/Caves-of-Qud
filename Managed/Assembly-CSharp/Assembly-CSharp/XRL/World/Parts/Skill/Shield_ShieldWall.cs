using System;
using XRL.World.Effects;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001384 RID: 4996
	[Serializable]
	public class Shield_ShieldWall : BaseSkill
	{
		// Token: 0x0600C1BF RID: 49599 RVA: 0x0046CC9D File Offset: 0x0046AE9D
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID || ID == PooledEvent<CommandEvent>.ID;
		}

		// Token: 0x0600C1C0 RID: 49600 RVA: 0x0046CCC3 File Offset: 0x0046AEC3
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.ActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600C1C1 RID: 49601 RVA: 0x0046CCE8 File Offset: 0x0046AEE8
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (E.Distance <= 1 && !E.Actor.IsFrozen() && E.Actor.GetShield(null, null) != null && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null))
			{
				E.Add(Shield_ShieldWall.COMMAND_NAME, 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C1C2 RID: 49602 RVA: 0x0046CD44 File Offset: 0x0046AF44
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == Shield_ShieldWall.COMMAND_NAME)
			{
				GameObject shield = this.ParentObject.GetShield(null, null);
				if (shield == null)
				{
					return this.ParentObject.Fail("You have no shield to raise.");
				}
				if (!this.ParentObject.CanMoveExtremities("ShieldWall", true, false, false))
				{
					return false;
				}
				this.ParentObject.ApplyEffect(new ShieldWall(3, shield), null);
				base.CooldownMyActivatedAbility(this.ActivatedAbilityID, 30, null, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C1C3 RID: 49603 RVA: 0x0046CDCC File Offset: 0x0046AFCC
		public override bool AddSkill(GameObject GO)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Shield Wall", Shield_ShieldWall.COMMAND_NAME, "Skills", null, "\u0004", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.AddSkill(GO);
		}

		// Token: 0x0600C1C4 RID: 49604 RVA: 0x0046CE14 File Offset: 0x0046B014
		public override bool RemoveSkill(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.RemoveSkill(GO);
		}

		// Token: 0x0600C1C5 RID: 49605 RVA: 0x0046CE2B File Offset: 0x0046B02B
		public void CollectStats(Templates.StatCollector stats)
		{
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), 30, 0);
		}

		// Token: 0x04004D7F RID: 19839
		public static readonly string COMMAND_NAME = "CommandShieldWall";

		// Token: 0x04004D80 RID: 19840
		public Guid ActivatedAbilityID = Guid.Empty;
	}
}
