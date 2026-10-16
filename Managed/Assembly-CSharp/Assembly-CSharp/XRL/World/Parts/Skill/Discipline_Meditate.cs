using System;
using XRL.UI;
using XRL.World.Effects;

namespace XRL.World.Parts.Skill
{
	// Token: 0x0200137F RID: 4991
	[Serializable]
	public class Discipline_Meditate : BaseSkill
	{
		// Token: 0x0600C1AE RID: 49582 RVA: 0x0046CA1C File Offset: 0x0046AC1C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<UseEnergyEvent>.ID || ID == PooledEvent<CommandEvent>.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x0600C1AF RID: 49583 RVA: 0x0046CA42 File Offset: 0x0046AC42
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.ActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600C1B0 RID: 49584 RVA: 0x0046CA68 File Offset: 0x0046AC68
		public void CollectStats(Templates.StatCollector stats)
		{
			stats.Set("DamageThreshold", Math.Max(0, this.ParentObject.GetStat("Willpower").Value * 3 - 60), false, 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), this.GetCooldown(), 0);
		}

		// Token: 0x0600C1B1 RID: 49585 RVA: 0x0046CABC File Offset: 0x0046ACBC
		public int GetCooldown()
		{
			return Math.Max(200 - this.ParentObject.GetIntProperty("Serene", 0) * 40, 5);
		}

		// Token: 0x0600C1B2 RID: 49586 RVA: 0x0046CAE0 File Offset: 0x0046ACE0
		public override bool HandleEvent(UseEnergyEvent E)
		{
			if (E.Passive && E.Type != null && E.Type.Contains("Pass"))
			{
				this.RestCounter++;
				if (this.RestCounter >= 10 && !this.ParentObject.HasEffect<Meditating>() && !this.ParentObject.HasEffect<Asleep>() && !this.ParentObject.HasEffect<Stasis>())
				{
					this.ParentObject.ApplyEffect(new Meditating(1, true), null);
				}
			}
			else
			{
				this.RestCounter = 0;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C1B3 RID: 49587 RVA: 0x0046CB74 File Offset: 0x0046AD74
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == "CommandDisciplineMeditate")
			{
				if (this.ParentObject.HasEffect<Meditating>())
				{
					if (this.ParentObject.IsPlayer())
					{
						Popup.ShowFail("You are already meditating.", true, true, true);
					}
					return false;
				}
				base.CooldownMyActivatedAbility(this.ActivatedAbilityID, this.GetCooldown(), null, null, false);
				this.ParentObject.ApplyEffect(new Meditating(), null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C1B4 RID: 49588 RVA: 0x0046CBEC File Offset: 0x0046ADEC
		public override bool AddSkill(GameObject GO)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Meditate", "CommandDisciplineMeditate", "Skills", null, "\u0001", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.AddSkill(GO);
		}

		// Token: 0x0600C1B5 RID: 49589 RVA: 0x0046CC34 File Offset: 0x0046AE34
		public override bool RemoveSkill(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.RemoveSkill(GO);
		}

		// Token: 0x04004D7D RID: 19837
		public Guid ActivatedAbilityID = Guid.Empty;

		// Token: 0x04004D7E RID: 19838
		public int RestCounter;
	}
}
