using System;
using System.Collections.Generic;
using System.Text;
using XRL.Language;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02001083 RID: 4227
	[Serializable]
	public class QuickenMind : IActivePart
	{
		// Token: 0x0600A838 RID: 43064 RVA: 0x003D82BE File Offset: 0x003D64BE
		public QuickenMind()
		{
			this.WorksOnEquipper = true;
		}

		// Token: 0x0600A839 RID: 43065 RVA: 0x003D82F8 File Offset: 0x003D64F8
		public void CollectStats(Templates.StatCollector stats)
		{
			if (this.AbilityAmount == "1")
			{
				stats.Set("RefreshDescription", "Refreshes a random cooldown.", false, 0);
			}
			else
			{
				stats.Set("RefreshDescription", "Refreshes all your cooldowns.", false, 0);
			}
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.AbilityID, this.ParentObject.Equipped), this.Cooldown, 0);
		}

		// Token: 0x0600A83A RID: 43066 RVA: 0x003D8364 File Offset: 0x003D6564
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CommandEvent>.ID || (ID == AIGetOffensiveItemListEvent.ID && this.WorksOnEquipper) || (ID == EquippedEvent.ID && this.WorksOnEquipper) || (ID == UnequippedEvent.ID && this.WorksOnEquipper) || (ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID && this.WorksOnEquipper);
		}

		// Token: 0x0600A83B RID: 43067 RVA: 0x003D83C5 File Offset: 0x003D65C5
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.AbilityID, new Action<Templates.StatCollector>(this.CollectStats), this.ParentObject.Equipped);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A83C RID: 43068 RVA: 0x003D83F4 File Offset: 0x003D65F4
		public override bool HandleEvent(AIGetOffensiveItemListEvent E)
		{
			if (this.IsObjectActivePartSubject(E.Actor) && E.Actor.IsActivatedAbilityAIUsable(this.AbilityID) && this.AnyRefreshableFor(E.Actor) && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				E.Add(this.AbilityCommand, 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A83D RID: 43069 RVA: 0x003D8474 File Offset: 0x003D6674
		public override bool HandleEvent(EquippedEvent E)
		{
			if (this.IsObjectActivePartSubject(E.Actor))
			{
				this.AbilityID = E.Actor.AddDynamicCommand(out this.AbilityCommand, "CommandQuickenMind", "Quicken Mind", "Items", null, "\a", null, false, false, false, false, false, false, false, false, true, true, false, false, -1, null, null, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A83E RID: 43070 RVA: 0x003D84D4 File Offset: 0x003D66D4
		public override bool HandleEvent(UnequippedEvent E)
		{
			if (!this.IsObjectActivePartSubject(E.Actor))
			{
				base.RemoveMyActivatedAbility(ref this.AbilityID, E.Actor);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A83F RID: 43071 RVA: 0x003D84FE File Offset: 0x003D66FE
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == this.AbilityCommand && this.Activate(E.Actor))
			{
				E.RequestInterfaceExit();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A840 RID: 43072 RVA: 0x003D8530 File Offset: 0x003D6730
		public bool IsRefreshable(ActivatedAbilityEntry Ability)
		{
			return Ability.Cooldown <= this.MaxAbilityCooldown && Ability.Cooldown >= this.MinAbilityCooldown && Ability.CommandForDescription != "CommandQuickenMind" && (this.IncludeClasses == null || this.IncludeClasses.CachedCommaExpansion().Contains(Ability.Class)) && (this.ExcludeClasses == null || !this.ExcludeClasses.CachedCommaExpansion().Contains(Ability.Class));
		}

		// Token: 0x0600A841 RID: 43073 RVA: 0x003D85B0 File Offset: 0x003D67B0
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600A842 RID: 43074 RVA: 0x003D85B4 File Offset: 0x003D67B4
		public bool AnyRefreshableFor(GameObject Actor)
		{
			Dictionary<Guid, ActivatedAbilityEntry> dictionary;
			if (Actor == null)
			{
				dictionary = null;
			}
			else
			{
				ActivatedAbilities activatedAbilities = Actor.ActivatedAbilities;
				dictionary = ((activatedAbilities != null) ? activatedAbilities.AbilityByGuid : null);
			}
			Dictionary<Guid, ActivatedAbilityEntry> dictionary2 = dictionary;
			if (dictionary2.IsNullOrEmpty<KeyValuePair<Guid, ActivatedAbilityEntry>>())
			{
				return false;
			}
			foreach (KeyValuePair<Guid, ActivatedAbilityEntry> keyValuePair in dictionary2)
			{
				if (this.IsRefreshable(keyValuePair.Value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600A843 RID: 43075 RVA: 0x003D8634 File Offset: 0x003D6834
		public List<ActivatedAbilityEntry> GetRefreshableOf(GameObject Actor)
		{
			Dictionary<Guid, ActivatedAbilityEntry> dictionary;
			if (Actor == null)
			{
				dictionary = null;
			}
			else
			{
				ActivatedAbilities activatedAbilities = Actor.ActivatedAbilities;
				dictionary = ((activatedAbilities != null) ? activatedAbilities.AbilityByGuid : null);
			}
			Dictionary<Guid, ActivatedAbilityEntry> dictionary2 = dictionary;
			if (dictionary2.IsNullOrEmpty<KeyValuePair<Guid, ActivatedAbilityEntry>>())
			{
				return null;
			}
			List<ActivatedAbilityEntry> list = null;
			foreach (KeyValuePair<Guid, ActivatedAbilityEntry> keyValuePair in dictionary2)
			{
				if (this.IsRefreshable(keyValuePair.Value))
				{
					if (list == null)
					{
						list = new List<ActivatedAbilityEntry>();
					}
					list.Add(keyValuePair.Value);
				}
			}
			return list;
		}

		// Token: 0x0600A844 RID: 43076 RVA: 0x003D86C8 File Offset: 0x003D68C8
		public bool Activate(GameObject Actor)
		{
			if (!Actor.IsActivatedAbilityUsable(this.AbilityID) || !this.IsObjectActivePartSubject(Actor))
			{
				return false;
			}
			ActivePartStatus activePartStatus = base.GetActivePartStatus(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null);
			if (activePartStatus != ActivePartStatus.Operational)
			{
				if (activePartStatus == ActivePartStatus.Booting)
				{
					return Actor.Fail(this.ParentObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " still attuning.");
				}
				return Actor.Fail(this.ParentObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " dead still.");
			}
			else
			{
				List<ActivatedAbilityEntry> refreshableOf = this.GetRefreshableOf(Actor);
				if (refreshableOf.IsNullOrEmpty<ActivatedAbilityEntry>())
				{
					return Actor.Fail("None of your abilities need refreshing.");
				}
				base.ConsumeCharge(null, null);
				if (Actor != null)
				{
					Actor.PlayWorldSound("Sounds/Abilities/sfx_ability_mutation_massMind_activate", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				}
				base.CooldownMyActivatedAbility(this.AbilityID, this.Cooldown, Actor, null, false);
				if (this.AbilityAmount == "*" || this.AbilityAmount.EqualsNoCase("All"))
				{
					foreach (ActivatedAbilityEntry activatedAbilityEntry in refreshableOf)
					{
						activatedAbilityEntry.Refresh(this);
					}
					return Actor.ShowSuccess("You are quickened with creative energy! All cooldowns are refreshed.", true);
				}
				int num = Stat.RollCached(this.AbilityAmount);
				List<string> list = new List<string>();
				int num2 = 0;
				while (num2 < num && refreshableOf.Count > 0)
				{
					int num3 = Stat.Rnd.Next(refreshableOf.Count);
					ActivatedAbilityEntry activatedAbilityEntry2 = refreshableOf[num3];
					activatedAbilityEntry2.Refresh(this);
					list.Add("{{W|" + activatedAbilityEntry2.DisplayName + "}}");
					refreshableOf.RemoveAt(num3);
					num2++;
				}
				if (!list.IsNullOrEmpty<string>())
				{
					StringBuilder stringBuilder = Event.NewStringBuilder("You are quickened with creative energy! The ").Append((list.Count == 1) ? "cooldown" : "cooldowns").Append(" on ")
						.Append(Grammar.MakeAndList(list, true))
						.Compound((list.Count == 1) ? "is" : "are")
						.Append(" refreshed.");
					return Actor.ShowSuccess(stringBuilder.ToString(), true);
				}
				return true;
			}
		}

		// Token: 0x0400456E RID: 17774
		public const string ABL_NAME = "Quicken Mind";

		// Token: 0x0400456F RID: 17775
		public const string ABL_CMD = "QuickenMind";

		// Token: 0x04004570 RID: 17776
		public Guid AbilityID;

		// Token: 0x04004571 RID: 17777
		public int Cooldown = 1000;

		// Token: 0x04004572 RID: 17778
		public int MinAbilityCooldown = 1;

		// Token: 0x04004573 RID: 17779
		public int MaxAbilityCooldown = int.MaxValue;

		// Token: 0x04004574 RID: 17780
		public string IncludeClasses;

		// Token: 0x04004575 RID: 17781
		public string ExcludeClasses;

		// Token: 0x04004576 RID: 17782
		public string AbilityAmount = "*";

		// Token: 0x04004577 RID: 17783
		public string AbilityCommand;
	}
}
