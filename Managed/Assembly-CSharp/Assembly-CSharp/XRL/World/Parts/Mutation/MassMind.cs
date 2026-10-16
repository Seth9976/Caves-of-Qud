using System;
using System.Collections.Generic;
using System.Linq;
using XRL.Messages;
using XRL.Rules;
using XRL.UI;
using XRL.World.Effects;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013F2 RID: 5106
	[Serializable]
	public class MassMind : BaseMutation
	{
		// Token: 0x0600C5BE RID: 50622 RVA: 0x0047FC46 File Offset: 0x0047DE46
		public MassMind()
		{
			base.Type = "Mental";
		}

		// Token: 0x0600C5BF RID: 50623 RVA: 0x0047FC59 File Offset: 0x0047DE59
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CommandMassMind");
			Registrar.Register("BeginTakeAction");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C5C0 RID: 50624 RVA: 0x0047FC79 File Offset: 0x0047DE79
		public override string GetDescription()
		{
			return "You tap into the aggregate mind and steal power from other espers.";
		}

		// Token: 0x0600C5C1 RID: 50625 RVA: 0x0047FC80 File Offset: 0x0047DE80
		public int GetCooldown(int Level)
		{
			return Math.Max(100, 550 - 50 * Level);
		}

		// Token: 0x0600C5C2 RID: 50626 RVA: 0x0047FC94 File Offset: 0x0047DE94
		public override string GetLevelText(int Level)
		{
			string text = "";
			text += "Refreshes all mental mutations\n";
			text = text + "Cooldown: {{rules|" + this.GetCooldown(Level).ToString() + "}} rounds\n";
			text += "Cooldown is not affected by Willpower.\n";
			text += "Each use attracts slightly more attention from psychic interlopers.\n";
			if (Level == base.Level)
			{
				text += "{{rules|Small chance each round for another esper to steal your powers}}\n";
			}
			else
			{
				text += "{{rules|Decreased chance for another esper to steal your powers}}\n";
			}
			return text + "-200 reputation with {{w|the Seekers of the Sightless Way}}";
		}

		// Token: 0x0600C5C3 RID: 50627 RVA: 0x0047FD1B File Offset: 0x0047DF1B
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			stats.Set("CooldownNoWillpower", this.GetCooldown(Level), !stats.mode.Contains("ability"), 0);
		}

		// Token: 0x0600C5C4 RID: 50628 RVA: 0x0047FD43 File Offset: 0x0047DF43
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.ActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600C5C5 RID: 50629 RVA: 0x0047FD67 File Offset: 0x0047DF67
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x0600C5C6 RID: 50630 RVA: 0x0047FD80 File Offset: 0x0047DF80
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeginTakeAction")
			{
				if (this.nDuration > 0)
				{
					this.nDuration--;
				}
				else if ((double)Stat.Random(1, 10000) < (0.13 - 0.005 * (double)this.ParentObject.StatMod("Willpower", 0) - 0.0065 * (double)base.Level) * 100.0 && this.ParentObject.IsPlayer() && this.ParentObject.CurrentCell != null && !this.ParentObject.OnWorldMap())
				{
					IEnumerable<ActivatedAbilityEntry> enumerable = base.MyActivatedAbilities.YieldAbilitiesWithClass("Mental Mutations");
					if (enumerable.Any<ActivatedAbilityEntry>())
					{
						if (this.ParentObject.HasEffect<FungalVisionary>())
						{
							IComponent<GameObject>.AddPlayerMessage("You feel a small ripple in space and time.", null, true);
						}
						else
						{
							this.nDuration = "8d10".RollCached();
							foreach (ActivatedAbilityEntry activatedAbilityEntry in enumerable)
							{
								if (activatedAbilityEntry.Enabled)
								{
									activatedAbilityEntry.AddScaledCooldown(this.nDuration * 10);
								}
							}
							IComponent<GameObject>.AddPlayerMessage("{{R|Someone reaches through the aggregate mind and exhausts your power!}}", null, true);
							this.ParentObject.PlayWorldSound("Sounds/Abilities/sfx_ability_mutation_massMind_cooldownReset", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
						}
					}
				}
			}
			else if (E.ID == "CommandMassMind")
			{
				if (this.ParentObject.HasEffect<FungalVisionary>())
				{
					Popup.Show("Too far! The aggregate mind is stretched to gossamers, and even the closest mind is too far away.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				else
				{
					MessageQueue.AddPlayerMessage("{{G|You innervate your mind at someone's expense.}}", null, true);
					foreach (ActivatedAbilityEntry activatedAbilityEntry2 in base.MyActivatedAbilities.YieldAbilitiesWithClass("Mental Mutations"))
					{
						if (activatedAbilityEntry2.Cooldown > 0 && activatedAbilityEntry2.ID != this.ActivatedAbilityID)
						{
							activatedAbilityEntry2.Refresh(this);
						}
					}
					this.ParentObject.ModIntProperty("GlimmerModifier", 1, false);
					this.ParentObject.SyncMutationLevelAndGlimmer();
					base.UseEnergy(1000, "Mental Mutation MassMind");
					int cooldown = this.GetCooldown(base.Level);
					base.CooldownMyActivatedAbility(this.ActivatedAbilityID, cooldown, null, null, false);
					this.ParentObject.FireEvent("AfterMassMind");
					this.ParentObject.PlayWorldSound("Sounds/Abilities/sfx_ability_mutation_massMind_activate", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C5C7 RID: 50631 RVA: 0x00480058 File Offset: 0x0047E258
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C5C8 RID: 50632 RVA: 0x00480064 File Offset: 0x0047E264
		public override bool Mutate(GameObject GO, int Level)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Tap the Mass Mind", "CommandMassMind", "Mental Mutations", null, "!", null, false, false, false, false, false, false, false, false, true, false, false, -1, null, null, null, null, null, null);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C5C9 RID: 50633 RVA: 0x004800AD File Offset: 0x0047E2AD
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.Unmutate(GO);
		}

		// Token: 0x04004E0F RID: 19983
		public int nDuration;
	}
}
