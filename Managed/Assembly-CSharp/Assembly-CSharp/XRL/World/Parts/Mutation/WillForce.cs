using System;
using XRL.Rules;
using XRL.World.Effects;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001403 RID: 5123
	[Serializable]
	public class WillForce : BaseMutation
	{
		// Token: 0x0600C6EB RID: 50923 RVA: 0x00488550 File Offset: 0x00486750
		public WillForce()
		{
			base.Type = "Mental";
		}

		// Token: 0x0600C6EC RID: 50924 RVA: 0x00488584 File Offset: 0x00486784
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == PooledEvent<GetItemElementsEvent>.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x0600C6ED RID: 50925 RVA: 0x004885AC File Offset: 0x004867AC
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.StrengthActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			base.DescribeMyActivatedAbility(this.AgilityActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			base.DescribeMyActivatedAbility(this.ToughnessActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600C6EE RID: 50926 RVA: 0x00488614 File Offset: 0x00486814
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (E.Distance <= 4)
			{
				if (base.IsMyActivatedAbilityAIUsable(this.StrengthActivatedAbilityID, null))
				{
					E.Add("CommandWillForceStrength", (E.Actor.BaseStat("Strength", 0) > E.Actor.BaseStat("Agility", 0) && E.Actor.BaseStat("Strength", 0) > E.Actor.BaseStat("Toughness", 0)) ? 3 : 1, null, false, false, null, null);
				}
				if (base.IsMyActivatedAbilityAIUsable(this.AgilityActivatedAbilityID, null))
				{
					E.Add("CommandWillForceAgility", (E.Actor.BaseStat("Agility", 0) > E.Actor.BaseStat("Strength", 0) && E.Actor.BaseStat("Agility", 0) > E.Actor.BaseStat("Toughness", 0)) ? 3 : 1, null, false, false, null, null);
				}
				if (base.IsMyActivatedAbilityAIUsable(this.ToughnessActivatedAbilityID, null))
				{
					E.Add("CommandWillForceToughness", 1, null, false, false, null, null);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C6EF RID: 50927 RVA: 0x0048872A File Offset: 0x0048692A
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("jewels", this.BaseElementWeight);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C6F0 RID: 50928 RVA: 0x00488752 File Offset: 0x00486952
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CommandWillForceAgility");
			Registrar.Register("CommandWillForceStrength");
			Registrar.Register("CommandWillForceToughness");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C6F1 RID: 50929 RVA: 0x0048877D File Offset: 0x0048697D
		public override string GetDescription()
		{
			return "Through sheer force of will, you perform uncanny physical feats.";
		}

		// Token: 0x0600C6F2 RID: 50930 RVA: 0x00488784 File Offset: 0x00486984
		public int GetLowDuration(int Level)
		{
			return 16 + 2 * Level;
		}

		// Token: 0x0600C6F3 RID: 50931 RVA: 0x0048878C File Offset: 0x0048698C
		public int GetHighDuration(int Level)
		{
			return 20 + 2 * Level;
		}

		// Token: 0x0600C6F4 RID: 50932 RVA: 0x00488794 File Offset: 0x00486994
		public override string GetLevelText(int Level)
		{
			string text = "Augments one physical attribute by an amount equal to twice your Ego bonus\n";
			text = string.Concat(new string[]
			{
				text,
				"Duration: {{rules|",
				this.GetLowDuration(Level).ToString(),
				"-",
				this.GetHighDuration(Level).ToString(),
				"}} rounds\n"
			});
			return text + "Cooldown: 200 rounds";
		}

		// Token: 0x0600C6F5 RID: 50933 RVA: 0x00488800 File Offset: 0x00486A00
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			stats.Set("Bonus", Math.Max(this.ParentObject.StatMod("Ego", 0) * 2, 1), false, 0);
			stats.Set("Duration", this.GetLowDuration(Level).ToString() + "-" + this.GetHighDuration(Level).ToString(), false, 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.StrengthActivatedAbilityID, null), 200, 0);
		}

		// Token: 0x0600C6F6 RID: 50934 RVA: 0x00488884 File Offset: 0x00486A84
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CommandWillForceStrength")
			{
				if (!this.ActivateWillForce("Strength"))
				{
					return false;
				}
			}
			else if (E.ID == "CommandWillForceAgility")
			{
				if (!this.ActivateWillForce("Agility"))
				{
					return false;
				}
			}
			else if (E.ID == "CommandWillForceToughness" && !this.ActivateWillForce("Toughness"))
			{
				return false;
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C6F7 RID: 50935 RVA: 0x004888FC File Offset: 0x00486AFC
		public bool ActivateWillForce(string Stat)
		{
			this.ParentObject.UseEnergy(1000, "Mental Mutation EgoProjection", null, null, false);
			base.CooldownMyActivatedAbility(this.StrengthActivatedAbilityID, 200, null, null, false);
			base.CooldownMyActivatedAbility(this.AgilityActivatedAbilityID, 200, null, null, false);
			base.CooldownMyActivatedAbility(this.ToughnessActivatedAbilityID, 200, null, null, false);
			int num = Math.Max(this.ParentObject.StatMod("Ego", 0) * 2, 1);
			int num2 = Stat.Random(this.GetLowDuration(base.Level), this.GetHighDuration(base.Level));
			foreach (Effect effect in this.ParentObject.Effects)
			{
				BoostStatistic boostStatistic = effect as BoostStatistic;
				if (boostStatistic != null && boostStatistic.Statistic == Stat)
				{
					if (boostStatistic.Bonus < num)
					{
						boostStatistic.Duration = 0;
						this.ParentObject.CleanEffects();
						break;
					}
					if (boostStatistic.Duration < num2)
					{
						boostStatistic.Duration = num2;
					}
					return true;
				}
			}
			this.ParentObject.ApplyEffect(new BoostStatistic(num2, Stat, num), null);
			return true;
		}

		// Token: 0x0600C6F8 RID: 50936 RVA: 0x00488A50 File Offset: 0x00486C50
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C6F9 RID: 50937 RVA: 0x00488A5C File Offset: 0x00486C5C
		public override bool Mutate(GameObject GO, int Level)
		{
			this.StrengthActivatedAbilityID = base.AddMyActivatedAbility("Boost Strength", "CommandWillForceStrength", "Mental Mutations", null, "¾", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			this.AgilityActivatedAbilityID = base.AddMyActivatedAbility("Boost Agility", "CommandWillForceAgility", "Mental Mutations", null, "\u00af", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			this.ToughnessActivatedAbilityID = base.AddMyActivatedAbility("Boost Toughness", "CommandWillForceToughness", "Mental Mutations", null, "\u0003", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C6FA RID: 50938 RVA: 0x00488B0D File Offset: 0x00486D0D
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.StrengthActivatedAbilityID, null);
			base.RemoveMyActivatedAbility(ref this.AgilityActivatedAbilityID, null);
			base.RemoveMyActivatedAbility(ref this.ToughnessActivatedAbilityID, null);
			return base.Unmutate(GO);
		}

		// Token: 0x04004E4B RID: 20043
		public Guid StrengthActivatedAbilityID = Guid.Empty;

		// Token: 0x04004E4C RID: 20044
		public Guid AgilityActivatedAbilityID = Guid.Empty;

		// Token: 0x04004E4D RID: 20045
		public Guid ToughnessActivatedAbilityID = Guid.Empty;
	}
}
