using System;
using XRL.UI;
using XRL.World.Effects;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001416 RID: 5142
	[Serializable]
	public class DefensiveChromatophores : BaseMutation
	{
		// Token: 0x0600C7E0 RID: 51168 RVA: 0x0048C8B4 File Offset: 0x0048AAB4
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<CommandTakeActionEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C7E1 RID: 51169 RVA: 0x0048C8D2 File Offset: 0x0048AAD2
		public override bool HandleEvent(CommandTakeActionEvent E)
		{
			return (this.ParentObject.IsPlayer() || !this.AttemptScintillate(true)) && base.HandleEvent(E);
		}

		// Token: 0x0600C7E2 RID: 51170 RVA: 0x0048C8F3 File Offset: 0x0048AAF3
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("jewels", 2);
				E.Add("chance", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C7E3 RID: 51171 RVA: 0x0048C922 File Offset: 0x0048AB22
		public override string GetLevelText(int Level)
		{
			return "You can't act while scintillating.\nConfuses nearby hostile creatures per Confusion rank " + Level.ToString() + ".\nDuration: 5 rounds\nCooldown: 200 rounds";
		}

		// Token: 0x0600C7E4 RID: 51172 RVA: 0x0048C93A File Offset: 0x0048AB3A
		public override string GetDescription()
		{
			return "In stressful situations, you scintillate.";
		}

		// Token: 0x0600C7E5 RID: 51173 RVA: 0x0048C941 File Offset: 0x0048AB41
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			stats.Set("ConfusionRank", this.GetConfusionRank(Level), false, 0);
			stats.Set("Duration", 5, false, 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), 5, 0);
		}

		// Token: 0x0600C7E6 RID: 51174 RVA: 0x0048C97A File Offset: 0x0048AB7A
		public int GetConfusionRank(int Level)
		{
			return Level;
		}

		// Token: 0x0600C7E7 RID: 51175 RVA: 0x0048C97D File Offset: 0x0048AB7D
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600C7E8 RID: 51176 RVA: 0x0048C980 File Offset: 0x0048AB80
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CommandScintillate");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C7E9 RID: 51177 RVA: 0x0048C995 File Offset: 0x0048AB95
		public override bool FireEvent(Event E)
		{
			return (!(E.ID == "CommandScintillate") || this.AttemptScintillate(false)) && base.FireEvent(E);
		}

		// Token: 0x0600C7EA RID: 51178 RVA: 0x0048C9BB File Offset: 0x0048ABBB
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C7EB RID: 51179 RVA: 0x0048C9C4 File Offset: 0x0048ABC4
		public override bool Mutate(GameObject GO, int Level)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Scintillate", "CommandScintillate", "Physical Mutations", null, "\u000f", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C7EC RID: 51180 RVA: 0x0048CA0D File Offset: 0x0048AC0D
		public override bool Unmutate(GameObject GO)
		{
			return base.Unmutate(GO);
		}

		// Token: 0x0600C7ED RID: 51181 RVA: 0x0048CA18 File Offset: 0x0048AC18
		public bool AttemptScintillate(bool Auto = false)
		{
			if (!this.ShouldStartScintillating())
			{
				if (!Auto && this.ParentObject.IsPlayer())
				{
					if (base.IsMyActivatedAbilityCoolingDown(this.ActivatedAbilityID, null))
					{
						Popup.Show("You can't scintillate again so soon.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					else
					{
						Popup.Show("You're not under enough stress to scintillate.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
				}
				return false;
			}
			if (!this.ParentObject.ApplyEffect(new Scintillating(5, base.Level), null))
			{
				return false;
			}
			GameObject parentObject = this.ParentObject;
			if (parentObject != null)
			{
				parentObject.PlayWorldSound("Sounds/Abilities/sfx_ability_mutation_physical_generic_activate", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			base.CooldownMyActivatedAbility(this.ActivatedAbilityID, 200, null, null, false);
			return true;
		}

		// Token: 0x0600C7EE RID: 51182 RVA: 0x0048CAE4 File Offset: 0x0048ACE4
		public bool ShouldStartScintillating()
		{
			return base.IsMyActivatedAbilityVoluntarilyUsable(this.ActivatedAbilityID, null) && (this.ParentObject.GetHPPercent() <= 15 || (this.ParentObject.PartyLeader != null && this.ParentObject.PartyLeader.GetHPPercent() <= 15 && this.ParentObject.HasLOSTo(this.ParentObject.PartyLeader, true, false, false, true, null) && this.ParentObject.DistanceTo(this.ParentObject.PartyLeader) <= 30));
		}
	}
}
