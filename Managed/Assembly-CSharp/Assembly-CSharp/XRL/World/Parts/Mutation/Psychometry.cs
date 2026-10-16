using System;
using System.Collections.Generic;
using System.Text;
using XRL.Language;
using XRL.UI;
using XRL.World.Effects;
using XRL.World.Parts.Skill;
using XRL.World.Tinkering;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013F5 RID: 5109
	[Serializable]
	public class Psychometry : BaseMutation
	{
		// Token: 0x0600C5FA RID: 50682 RVA: 0x004811BB File Offset: 0x0047F3BB
		public Psychometry()
		{
			base.Type = "Mental";
		}

		// Token: 0x0600C5FB RID: 50683 RVA: 0x004811D8 File Offset: 0x0047F3D8
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			bool flag = stats.mode.Contains("ability");
			stats.Set("IdentifyComplexity", Psychometry.GetIdentifiableComplexity(Level), !flag, 0);
			stats.Set("ConstructComplexity", Psychometry.GetLearnableComplexity(Level), !flag, 0);
		}

		// Token: 0x0600C5FC RID: 50684 RVA: 0x00481222 File Offset: 0x0047F422
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CommandEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID || ID == PooledEvent<GetRitualSifrahSetupEvent>.ID || ID == GetTinkeringBonusEvent.ID || ID == InventoryActionEvent.ID || ID == OwnerGetInventoryActionsEvent.ID;
		}

		// Token: 0x0600C5FD RID: 50685 RVA: 0x00481260 File Offset: 0x0047F460
		public override bool HandleEvent(GetTinkeringBonusEvent E)
		{
			if (E.Type == "Examine" || E.Type == "ReverseEngineerTurns" || E.Type == "Hacking")
			{
				if (base.UsePsychometry(E.Actor, E.Item))
				{
					E.Bonus += 1 + base.Level / 10;
					E.SecondaryBonus += base.Level;
					E.PsychometryApplied = true;
				}
				else
				{
					if (E.Actor.IsInvalid() || E.Actor.HasEffect<Stun>())
					{
						return false;
					}
					if (Popup.ShowYesNo("Do you want to continue despite being unable to use Psychometry?", "Sounds/UI/ui_notification", true, DialogResult.Yes, null) != DialogResult.Yes)
					{
						return false;
					}
				}
			}
			else if (E.Type == "Repair" || E.Type == "Disarming" || E.Type == "ItemModding")
			{
				if (base.UsePsychometry(E.Actor, E.Item))
				{
					E.SecondaryBonus += base.Level;
					E.PsychometryApplied = true;
				}
				else
				{
					if (!E.Actor.IsValid() || E.Actor.HasEffect<Stun>())
					{
						return false;
					}
					if (Popup.ShowYesNo("Do you want to continue despite being unable to use Psychometry?", "Sounds/UI/ui_notification", true, DialogResult.Yes, null) != DialogResult.Yes)
					{
						return false;
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C5FE RID: 50686 RVA: 0x004813C8 File Offset: 0x0047F5C8
		public override bool HandleEvent(GetRitualSifrahSetupEvent E)
		{
			if (E.Type == "Item Naming")
			{
				if (base.UsePsychometry(E.Actor, E.Subject))
				{
					E.PsychometryApplied = true;
				}
				else if (!E.Actor.IsValid() || E.Actor.HasEffect<Stun>())
				{
					return false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C5FF RID: 50687 RVA: 0x00481428 File Offset: 0x0047F628
		public override bool HandleEvent(OwnerGetInventoryActionsEvent E)
		{
			if (E.Object.GetEpistemicStatus() != 2)
			{
				Examiner examiner;
				if (!E.Actor.IsConfused && !Options.SifrahExamine && E.Object.TryGetPart<Examiner>(out examiner) && examiner.Complexity > 0)
				{
					E.AddAction("Psychometry", "read history with Psychometry", Psychometry.COMMAND_NAME, null, 'i', true, 0, 0, false, false, false, false, true, null, false);
				}
			}
			else if (this.ParentObject.HasPart<Tinkering>())
			{
				TinkerItem part = E.Object.GetPart<TinkerItem>();
				string text = ((part != null) ? part.ActiveBlueprint : null) ?? E.Object.Blueprint;
				if (!this.LearnedBlueprints.Contains(text))
				{
					bool flag = false;
					using (List<TinkerData>.Enumerator enumerator = TinkerData.TinkerRecipes.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current.Blueprint == text)
							{
								flag = true;
								break;
							}
						}
					}
					if (flag)
					{
						flag = false;
						using (List<TinkerData>.Enumerator enumerator = TinkerData.KnownRecipes.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								if (enumerator.Current.Blueprint == text)
								{
									flag = true;
									break;
								}
							}
						}
						if (!flag)
						{
							E.AddAction("Psychometry", "read early history with Psychometry", Psychometry.COMMAND_NAME, null, 'i', true, 0, 0, false, false, false, false, true, null, false);
						}
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C600 RID: 50688 RVA: 0x004815B8 File Offset: 0x0047F7B8
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == Psychometry.COMMAND_NAME)
			{
				if (!this.Activate(E.Item))
				{
					return true;
				}
				Examiner part = E.Item.GetPart<Examiner>();
				TinkerItem part2 = E.Item.GetPart<TinkerItem>();
				GameObject parentObject = this.ParentObject;
				if (parentObject != null)
				{
					parentObject.PlayWorldSound("Sounds/Abilities/sfx_ability_mutation_psychometry_activate", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				}
				if (!E.Item.Understood())
				{
					if (Options.SifrahExamine)
					{
						InventoryActionEvent.Check(E.Item, E.Actor, E.Item, "Examine", false, false, false, false, false, 0, 0, 0, null, null, null, null);
						return true;
					}
					if (part.Complexity > this.GetIdentifiableComplexity())
					{
						Popup.ShowFail(string.Concat(new string[]
						{
							E.Item.IndicativeProximal,
							E.Item.IsPlural ? " artifacts" : " artifact",
							E.Item.Is,
							" too complex for you to decipher ",
							E.Item.its,
							" function."
						}), true, true, true);
						return true;
					}
					part.MakeUnderstood(false);
					Popup.ShowFail(string.Concat(new string[]
					{
						"You flush with understanding of the ",
						E.Item.IsPlural ? "artifacts'" : "artifact's",
						" past and determine ",
						E.Item.them,
						" to be ",
						E.Item.an(int.MaxValue, null, null, false, true, false, false, false, false, true, false, false, true, false, null, false, null, false),
						"."
					}), true, true, true);
					base.UseEnergy(1000, "Mental Mutation Psychometry");
				}
				else
				{
					if (Options.SifrahReverseEngineer && part2 != null && part2.CanDisassemble)
					{
						if (E.Actor.HasSkill("Tinkering_ReverseEngineer"))
						{
							Popup.ShowFail(string.Concat(new string[]
							{
								"You must disassemble ",
								E.Item.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
								" in order to unlock ",
								E.Item.its,
								" secrets."
							}), true, true, true);
						}
						else
						{
							Popup.ShowFail(string.Concat(new string[]
							{
								"You must learn the way of the Reverse Engineer and disassemble ",
								E.Item.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
								" in order to unlock ",
								E.Item.its,
								" secrets."
							}), true, true, true);
						}
						return true;
					}
					if (part != null && part.Complexity > this.GetLearnableComplexity())
					{
						Popup.ShowFail(string.Concat(new string[]
						{
							E.Item.IndicativeProximal,
							E.Item.IsPlural ? " artifacts" : " artifact",
							E.Item.Is,
							" too complex for you to decipher ",
							E.Item.its,
							" method of construction."
						}), true, true, true);
						return true;
					}
					string text = ((part2 != null) ? part2.ActiveBlueprint : null) ?? E.Item.Blueprint;
					this.LearnedBlueprints.Add(text);
					foreach (TinkerData tinkerData in TinkerData.TinkerRecipes)
					{
						if (tinkerData.Blueprint == text)
						{
							GameObject gameObject = GameObject.CreateSample(tinkerData.Blueprint);
							gameObject.MakeUnderstood(false);
							try
							{
								E.Actor.PlayWorldOrUISound("sfx_characterMod_tinkerSchematic_learn", null);
								tinkerData.DisplayName = gameObject.DisplayNameOnlyDirect;
								Popup.Show(string.Concat(new string[]
								{
									"You abide the memory of ",
									(E.Item.Count > 1) ? E.Item.a : E.Item.the,
									Grammar.MakePossessive(E.Item.ShortDisplayNameSingle),
									" creation. You learn to build ",
									gameObject.IsPlural ? gameObject.ShortDisplayNameSingle : Grammar.Pluralize(gameObject.ShortDisplayNameSingle),
									"."
								}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
								TinkerData.KnownRecipes.Add(tinkerData);
							}
							finally
							{
								gameObject.Obliterate(null, false, null);
							}
							return true;
						}
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C601 RID: 50689 RVA: 0x00481AB0 File Offset: 0x0047FCB0
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == Psychometry.MENU_COMMAND_NAME)
			{
				return E.Actor.Fail("To use Psychometry, interact with an artifact and then choose 'read history with Psychometry.");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C602 RID: 50690 RVA: 0x00481ADC File Offset: 0x0047FCDC
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("scholarship", this.BaseElementWeight);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C603 RID: 50691 RVA: 0x00481B04 File Offset: 0x0047FD04
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600C604 RID: 50692 RVA: 0x00481B07 File Offset: 0x0047FD07
		public override string GetDescription()
		{
			return "You read the history of artifacts by touching them, learning what they do and how they were made.";
		}

		// Token: 0x0600C605 RID: 50693 RVA: 0x00481B0E File Offset: 0x0047FD0E
		public static int GetIdentifiableComplexity(int Level)
		{
			return 4 + Level / 2;
		}

		// Token: 0x0600C606 RID: 50694 RVA: 0x00481B15 File Offset: 0x0047FD15
		public int GetIdentifiableComplexity()
		{
			return Psychometry.GetIdentifiableComplexity(base.Level);
		}

		// Token: 0x0600C607 RID: 50695 RVA: 0x00481B22 File Offset: 0x0047FD22
		public static int GetLearnableComplexity(int Level)
		{
			return 2 + (Level - 1) / 2;
		}

		// Token: 0x0600C608 RID: 50696 RVA: 0x00481B2B File Offset: 0x0047FD2B
		public int GetLearnableComplexity()
		{
			return Psychometry.GetLearnableComplexity(base.Level);
		}

		// Token: 0x0600C609 RID: 50697 RVA: 0x00481B38 File Offset: 0x0047FD38
		public override string GetLevelText(int Level)
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			if (Options.SifrahExamine)
			{
				stringBuilder.Compound("Adds a bonus turn and is useful in many tinkering and some ritual Sifrah games.");
			}
			else
			{
				stringBuilder.Compound("Unerringly identify artifacts up to complexity tier {{rules|", "\n").Append(Psychometry.GetIdentifiableComplexity(Level)).Append("}}.");
				stringBuilder.Compound("Learn how to construct identified artifacts up to complexity tier {{rules|", "\n").Append(Psychometry.GetLearnableComplexity(Level)).Append("}} (must have the appropriate Tinker skill).");
			}
			stringBuilder.Compound("You may open security doors and use some secure devices by touching them.", "\n");
			return stringBuilder.ToString();
		}

		// Token: 0x0600C60A RID: 50698 RVA: 0x00481BC4 File Offset: 0x0047FDC4
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C60B RID: 50699 RVA: 0x00481BD0 File Offset: 0x0047FDD0
		public override bool Mutate(GameObject GO, int Level)
		{
			this.ActivatedAbilityID = GO.AddActivatedAbility("Psychometry", Psychometry.MENU_COMMAND_NAME, "Mental Mutations", null, "\a", null, false, false, false, false, this.RealityDistortionBased, false, false, false, true, true, false, false, -1, null, null, null, null, null);
			if (this.LearnedBlueprints == null)
			{
				this.LearnedBlueprints = new List<string>();
			}
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C60C RID: 50700 RVA: 0x00481C31 File Offset: 0x0047FE31
		public override bool Unmutate(GameObject GO)
		{
			GO.RemoveActivatedAbility(ref this.ActivatedAbilityID);
			return base.Unmutate(GO);
		}

		// Token: 0x0600C60D RID: 50701 RVA: 0x00481C48 File Offset: 0x0047FE48
		public bool Advisable(GameObject Subject)
		{
			return GameObject.Validate(ref Subject) && (!this.RealityDistortionBased || IComponent<GameObject>.CheckRealityDistortionAdvisability(this.ParentObject, Subject.GetCurrentCell() ?? this.ParentObject.GetCurrentCell(), this.ParentObject, null, this, null, 0));
		}

		// Token: 0x0600C60E RID: 50702 RVA: 0x00481CA4 File Offset: 0x0047FEA4
		public bool Activate(GameObject Subject)
		{
			if (!GameObject.Validate(ref Subject))
			{
				return false;
			}
			if (this.ParentObject.IsConfused)
			{
				return this.ParentObject.Fail("You strain to part the veil of time in order to use psychometry on " + Subject.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ", but you are too confused.");
			}
			if (!base.IsMyActivatedAbilityVoluntarilyUsable(this.ActivatedAbilityID, null))
			{
				if (base.IsMyActivatedAbilityCoolingDown(this.ActivatedAbilityID, null))
				{
					this.ParentObject.Fail("You strain to part the veil of time in order to use psychometry on " + Subject.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ", but your psyche is too exhausted.");
				}
				return false;
			}
			if (!this.RealityDistortionBased)
			{
				return true;
			}
			Cell cell = Subject.GetCurrentCell() ?? this.ParentObject.GetCurrentCell();
			Event @event = Event.New("InitiateRealityDistortionTransit", 0, 0, 0);
			@event.SetParameter("Object", this.ParentObject);
			@event.SetParameter("Mutation", this);
			@event.SetParameter("Cell", cell);
			@event.SetParameter("Purpose", "use psychometry on " + Subject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false));
			return this.ParentObject.FireEvent(@event) && cell.FireEvent(@event);
		}

		// Token: 0x04004E1D RID: 19997
		public static readonly string COMMAND_NAME = "Psychometry";

		// Token: 0x04004E1E RID: 19998
		public static readonly string MENU_COMMAND_NAME = "CommandPsychometryMenu";

		// Token: 0x04004E1F RID: 19999
		public bool RealityDistortionBased = true;

		// Token: 0x04004E20 RID: 20000
		public List<string> LearnedBlueprints;
	}
}
