using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleLib.Console;
using Qud.API;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using XRL;
using XRL.Language;
using XRL.UI;
using XRL.UI.Framework;
using XRL.World;
using XRL.World.Capabilities;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;

namespace Qud.UI
{
	// Token: 0x02000460 RID: 1120
	[UIView("CharacterStatusScreen", false, false, false, null, null, false, 0, false, NavCategory = "Menus", UICanvas = "StatusScreens", UICanvasHost = 1)]
	public class CharacterStatusScreen : BaseStatusScreen<CharacterStatusScreen>
	{
		// Token: 0x06002A5C RID: 10844 RVA: 0x000DE586 File Offset: 0x000DC786
		public override string GetNavigationCategory()
		{
			return "StatusScreens:Charsheet";
		}

		// Token: 0x06002A5D RID: 10845 RVA: 0x000DE58D File Offset: 0x000DC78D
		public override IRenderable GetTabIcon()
		{
			global::XRL.World.GameObject player = The.Player;
			if (player == null)
			{
				return null;
			}
			return player.RenderForUI("StatusScreen,Tab", false);
		}

		// Token: 0x06002A5E RID: 10846 RVA: 0x000DE5A5 File Offset: 0x000DC7A5
		public override string GetTabString()
		{
			if (Media.sizeClass < Media.SizeClass.Medium)
			{
				return "Attributes";
			}
			return "Attributes && Powers";
		}

		// Token: 0x06002A5F RID: 10847 RVA: 0x000DE5BC File Offset: 0x000DC7BC
		public void UpdateData()
		{
			CharacterStatusScreen.mutations.Clear();
			CharacterStatusScreen.stats.Clear();
			CharacterStatusScreen.effects.Clear();
			if (this.GO != null)
			{
				CharacterStatusScreen.effects.AddRange(this.GO.Effects.Where<Effect>((Effect e) => e != null && !string.IsNullOrEmpty(e.GetDescription())));
				CharacterStatusScreen.mutations.AddRange(StatusScreen.GetMutationList(StatusScreensScreen.GO));
				int psychicGlimmer = this.GO.GetPsychicGlimmer(null);
				if (PsychicGlimmer.Perceptible(psychicGlimmer))
				{
					this.glimmerProxy.glimmer = psychicGlimmer;
					this.glimmerProxy.SetName("Psychic Glimmer");
					this.glimmerProxy.SetDisplayName("Psychic Glimmer");
					CharacterStatusScreen.mutations.Insert(0, this.glimmerProxy);
				}
				CharacterStatusScreen.stats.AddRange(StatusScreensScreen.GO.Statistics.Values);
				return;
			}
			CharacterStatusScreen.stats.Add(new Statistic("Strength", 0, 9000, 16, null));
			CharacterStatusScreen.stats.Add(new Statistic("Agility", 0, 9000, 16, null));
			CharacterStatusScreen.stats.Add(new Statistic("Toughness", 0, 9000, 16, null));
			CharacterStatusScreen.stats.Add(new Statistic("Intelligence", 0, 9000, 16, null));
			CharacterStatusScreen.stats.Add(new Statistic("Willpower", 0, 9000, 16, null));
			CharacterStatusScreen.stats.Add(new Statistic("Ego", 0, 9000, 16, null));
		}

		// Token: 0x06002A60 RID: 10848 RVA: 0x000DE75C File Offset: 0x000DC95C
		public override void UpdateViewFromData()
		{
			CharacterStatusScreen.CP = GetAvailableComputePowerEvent.GetFor(this.GO);
			GetMutationTermEvent.GetFor(this.GO, out this.mutationTerm, out this.mutationColor, null);
			this.mutationsTerm = Grammar.MakeTitleCase(Grammar.Pluralize(this.mutationTerm));
			this.mutationTermCapital = Grammar.MakeTitleCase(this.mutationTerm);
			UITextSkin uitextSkin = this.mutationTermText;
			if (uitextSkin != null)
			{
				uitextSkin.SetText(ConsoleLib.Console.ColorUtility.ToUpperExceptFormatting(this.mutationsTerm));
			}
			this.primaryAttributesController.BeforeShow(from a in CharacterStatusScreen.PrimaryAttributes
				where CharacterStatusScreen.stats.Any<Statistic>((Statistic s) => s.Name == a)
				select new CharacterAttributeLineData
				{
					category = CharacterAttributeLineData.Category.primary,
					go = this.GO,
					data = CharacterStatusScreen.stats.Where<Statistic>((Statistic s) => s.Name == a).FirstOrDefault<Statistic>()
				});
			this.secondaryAttributesController.BeforeShow(from a in (CharacterStatusScreen.CP > 0) ? CharacterStatusScreen.SecondaryAttributesWithCP : CharacterStatusScreen.SecondaryAttributes
				where CharacterStatusScreen.stats.Any<Statistic>((Statistic s) => s.Name == a) || a == "CP"
				select new CharacterAttributeLineData
				{
					category = CharacterAttributeLineData.Category.secondary,
					go = this.GO,
					data = CharacterStatusScreen.stats.Where<Statistic>((Statistic s) => s.Name == a).FirstOrDefault<Statistic>(),
					stat = a
				});
			this.resistanceAttributesController.BeforeShow(from a in CharacterStatusScreen.ResistanceAttributes
				where CharacterStatusScreen.stats.Any<Statistic>((Statistic s) => s.Name == a)
				select new CharacterAttributeLineData
				{
					category = CharacterAttributeLineData.Category.resistance,
					go = this.GO,
					data = CharacterStatusScreen.stats.Where<Statistic>((Statistic s) => s.Name == a).FirstOrDefault<Statistic>()
				});
			this.mutationsController.BeforeShow(CharacterStatusScreen.mutations.Select<BaseMutation, CharacterMutationLineData>((BaseMutation m) => new CharacterMutationLineData
			{
				mutation = m
			}));
			this.effectsController.BeforeShow(CharacterStatusScreen.effects.Select<Effect, CharacterEffectLineData>((Effect e) => new CharacterEffectLineData
			{
				effect = e
			}));
			if (this.GO != null)
			{
				this.playerIcon.FromRenderable(this.GO.RenderForUI("StatusScreen,Character", false), true);
				this.nameText.SetText(this.GO.DisplayName);
				this.classText.SetText(this.GO.GetGenotype() + " " + this.GO.GetSubtype());
				this.levelText.SetText(string.Format("Level: {0} \u00af HP: {1}/{2} \u00af XP: {3}/{4} \u00af Weight: {5}#", new object[]
				{
					this.GO.Level,
					this.GO.Stat("Hitpoints", 0),
					this.GO.GetStat("Hitpoints").BaseValue,
					this.GO.Stat("XP", 0),
					Leveler.GetXPForLevel(this.GO.Stat("Level", 0) + 1),
					this.GO.Weight
				}));
				this.attributePointsText.SetText(string.Format("Attribute Points: {0}{1}}}}}", (this.GO.Stat("AP", 0) > 0) ? "{{G|" : "{{K|", this.GO.Stat("AP", 0)));
				if (Media.sizeClass < Media.SizeClass.Medium)
				{
					this.mutationPointsText.SetText(string.Format("MP: {0}{1}}}}}", (this.GO.Stat("MP", 0) > 0) ? "{{G|" : "{{K|", this.GO.Stat("MP", 0)));
					return;
				}
				this.mutationPointsText.SetText(string.Format("{0} Points: {1}{2}}}}}", this.mutationTermCapital, (this.GO.Stat("MP", 0) > 0) ? "{{G|" : "{{K|", this.GO.Stat("MP", 0)));
			}
		}

		// Token: 0x06002A61 RID: 10849 RVA: 0x000DEB2C File Offset: 0x000DCD2C
		public void HandleHighlightMutation(FrameworkDataElement element)
		{
			if (element != null)
			{
				CharacterMutationLineData characterMutationLineData = element as CharacterMutationLineData;
				if (characterMutationLineData != null && characterMutationLineData.mutation != null)
				{
					this.mutationDetailsObject.SetActive(true);
					StringBuilder stringBuilder = global::XRL.World.Event.NewStringBuilder(characterMutationLineData.mutation.GetDescription());
					int num = ((StatusScreen.TreatAsMutant(this.GO) && characterMutationLineData.mutation.BaseLevel > 0) ? 1 : 0);
					GetMutationTermEvent.GetFor(this.GO, characterMutationLineData.mutation);
					bool flag = !characterMutationLineData.mutation.CanLevel() || characterMutationLineData.mutation.BaseLevel >= characterMutationLineData.mutation.GetMaxLevel();
					this.mutationIcon.FromRenderable(characterMutationLineData.mutation.GetIcon(), true);
					this.mutationNameText.SetText("{{B|" + characterMutationLineData.mutation.GetDisplayName(true) + "}}");
					if (!characterMutationLineData.mutation.ShouldShowLevel() || characterMutationLineData.mutation == this.glimmerProxy)
					{
						this.mutationRankText.SetText("");
					}
					else
					{
						this.mutationRankText.SetText(string.Format("{{{{G|RANK {0}/10}}}}", characterMutationLineData.mutation.Level));
					}
					if (characterMutationLineData.mutation == this.glimmerProxy)
					{
						this.mutationTypeText.SetText("");
					}
					else if (characterMutationLineData.mutation.Name == "Chimera" || characterMutationLineData.mutation.Name == "Esper" || characterMutationLineData.mutation.Name == "Unstable Genome")
					{
						this.mutationTypeText.SetText("{{C|[Morphotype]}}");
					}
					else if (characterMutationLineData.mutation.IsDefect())
					{
						this.mutationTypeText.SetText(characterMutationLineData.mutation.IsPhysical() ? "{{R|[Physical Defect]}}" : "{{r|[Mental Defect]}}");
					}
					else
					{
						this.mutationTypeText.SetText(characterMutationLineData.mutation.IsPhysical() ? ("{{c|[Physical " + this.mutationTermCapital + "]}}") : ("{{c|[Mental " + this.mutationTermCapital + "]}}"));
					}
					if (num == 0 || flag || characterMutationLineData.mutation == this.glimmerProxy)
					{
						stringBuilder.Compound(characterMutationLineData.mutation.GetLevelText(characterMutationLineData.mutation.Level), "\n\n");
					}
					else
					{
						string text = characterMutationLineData.mutation.GetLevelText(characterMutationLineData.mutation.Level);
						string text2 = characterMutationLineData.mutation.GetLevelText(characterMutationLineData.mutation.Level + 1);
						if (!string.IsNullOrEmpty(text))
						{
							stringBuilder.Compound("{{w|This rank}}:\n", "\n\n").Append(characterMutationLineData.mutation.GetLevelText(characterMutationLineData.mutation.Level));
						}
						if (!string.IsNullOrEmpty(text2))
						{
							stringBuilder.Append("\n\n{{w|Next rank}}:\n").Append(characterMutationLineData.mutation.GetLevelText(characterMutationLineData.mutation.Level + 1));
						}
					}
					this.mutationsDetails.SetText(stringBuilder.ToString());
					return;
				}
			}
			this.mutationsDetails.SetText("");
			this.mutationDetailsObject.SetActive(false);
		}

		// Token: 0x06002A62 RID: 10850 RVA: 0x000DEE58 File Offset: 0x000DD058
		public void HandleHighlightEffect(FrameworkDataElement element)
		{
			if (element != null)
			{
				CharacterEffectLineData characterEffectLineData = element as CharacterEffectLineData;
				if (characterEffectLineData != null && characterEffectLineData.effect != null)
				{
					StringBuilder stringBuilder = global::XRL.World.Event.NewStringBuilder(null);
					if (characterEffectLineData.effect.GetDescription() != null)
					{
						stringBuilder.AppendLine(Campfire.ProcessEffectDescription(characterEffectLineData.effect.GetDetails(), this.GO).Replace("\n", "\n  "));
						this.mutationsDetails.SetText(stringBuilder.ToString());
						return;
					}
					return;
				}
			}
			this.mutationsDetails.SetText("");
		}

		// Token: 0x06002A63 RID: 10851 RVA: 0x000DEEE0 File Offset: 0x000DD0E0
		public async void HandleSelectPrimaryAttribute(FrameworkDataElement element)
		{
			CharacterAttributeLineData data = element as CharacterAttributeLineData;
			if (data != null)
			{
				await APIDispatch.RunAndWaitAsync(delegate
				{
					StatusScreen.BuyStat(this.GO, data.data.Name);
				});
				this.UpdateViewFromData();
			}
		}

		// Token: 0x06002A64 RID: 10852 RVA: 0x000DEF20 File Offset: 0x000DD120
		public async void HandleSelectMutation(FrameworkDataElement element)
		{
			CharacterMutationLineData data = element as CharacterMutationLineData;
			if (data != null)
			{
				await APIDispatch.RunAndWaitAsync(delegate
				{
					StatusScreen.ShowMutationPopup(this.GO, data.mutation);
				});
				this.UpdateData();
				this.UpdateViewFromData();
			}
		}

		// Token: 0x06002A65 RID: 10853 RVA: 0x000DEF60 File Offset: 0x000DD160
		public void HandleHighlightAttribute(FrameworkDataElement element)
		{
			CharacterAttributeLineData characterAttributeLineData = element as CharacterAttributeLineData;
			if (characterAttributeLineData != null)
			{
				UITextSkin uitextSkin = null;
				if (Media.sizeClass > Media.SizeClass.Small)
				{
					if (characterAttributeLineData.category == CharacterAttributeLineData.Category.primary)
					{
						uitextSkin = this.primaryAttributesDetails;
					}
					if (characterAttributeLineData.category == CharacterAttributeLineData.Category.secondary)
					{
						uitextSkin = this.secondaryAttributesDetails;
					}
					if (characterAttributeLineData.category == CharacterAttributeLineData.Category.resistance)
					{
						uitextSkin = this.resistanceAttributesDetails;
					}
				}
				else
				{
					uitextSkin = this.resistanceAttributesDetails;
				}
				if (uitextSkin != this.primaryAttributesDetails)
				{
					this.primaryAttributesDetails.SetText("");
				}
				if (uitextSkin != this.secondaryAttributesDetails)
				{
					this.secondaryAttributesDetails.SetText("");
				}
				if (uitextSkin != this.resistanceAttributesDetails)
				{
					this.resistanceAttributesDetails.SetText("");
				}
				if (uitextSkin != null)
				{
					if (characterAttributeLineData.stat == "CP")
					{
						uitextSkin.SetText("Your {{W|Compute Power (CP)}} scales the bonuses of certain compute-enabled items and cybernetic implants. Your base compute power is 0.");
						return;
					}
					uitextSkin.SetText(characterAttributeLineData.data.GetHelpText());
				}
			}
		}

		// Token: 0x06002A66 RID: 10854 RVA: 0x000DF054 File Offset: 0x000DD254
		public async void HandleBuyMutation()
		{
			await APIDispatch.RunAndWaitAsync(delegate
			{
				MutationsAPI.BuyRandomMutation(this.GO, 4, true, null);
			});
			this.UpdateData();
			this.UpdateViewFromData();
		}

		// Token: 0x06002A67 RID: 10855 RVA: 0x000DF08C File Offset: 0x000DD28C
		public async void HandleShowEffects()
		{
			await APIDispatch.RunAndWaitAsync(delegate
			{
				The.Player.ShowActiveEffects();
			});
		}

		// Token: 0x06002A68 RID: 10856 RVA: 0x000DF0BC File Offset: 0x000DD2BC
		public override NavigationContext ShowScreen(global::XRL.World.GameObject GO, StatusScreensScreen parent)
		{
			this.GO = GO;
			if (Media.sizeClass < Media.SizeClass.Medium)
			{
				if (this.attributesTopLayoutGroup.spacing != -8f)
				{
					this.attributesTopLayoutGroup.spacing = -8f;
				}
				this.primaryAttributesHeaderText.SetText("ATTRIBUTES");
			}
			else
			{
				if (this.attributesTopLayoutGroup.spacing != 0f)
				{
					this.attributesTopLayoutGroup.spacing = 0f;
				}
				this.primaryAttributesHeaderText.SetText("MAIN ATTRIBUTES");
			}
			this.vertNav.contexts.Clear();
			this.vertNav.SetAxis(InputAxisTypes.NavigationYAxis, false);
			this.vertNav.contexts.Add(this.primaryAttributesController.scrollContext);
			this.vertNav.contexts.Add(this.secondaryAttributesController.scrollContext);
			this.vertNav.contexts.Add(this.resistanceAttributesController.scrollContext);
			this.vertNav.wraps = false;
			this.vertNav.Setup();
			this.horizNav.contexts.Clear();
			this.horizNav.SetAxis(InputAxisTypes.NavigationXAxis, false);
			this.horizNav.contexts.Add(this.vertNav);
			this.horizNav.contexts.Add(this.mutationsController.scrollContext);
			this.horizNav.Setup();
			this.primaryAttributesController.onSelected.RemoveAllListeners();
			this.primaryAttributesController.onHighlight.RemoveAllListeners();
			this.primaryAttributesController.onHighlight.AddListener(new UnityAction<FrameworkDataElement>(this.HandleHighlightAttribute));
			this.primaryAttributesController.onSelected.AddListener(new UnityAction<FrameworkDataElement>(this.HandleSelectPrimaryAttribute));
			this.primaryAttributesController.scrollContext.wraps = false;
			this.secondaryAttributesController.onSelected.RemoveAllListeners();
			this.secondaryAttributesController.onHighlight.RemoveAllListeners();
			this.secondaryAttributesController.onHighlight.AddListener(new UnityAction<FrameworkDataElement>(this.HandleHighlightAttribute));
			this.secondaryAttributesController.scrollContext.wraps = false;
			this.resistanceAttributesController.onSelected.RemoveAllListeners();
			this.resistanceAttributesController.onHighlight.RemoveAllListeners();
			this.resistanceAttributesController.onHighlight.AddListener(new UnityAction<FrameworkDataElement>(this.HandleHighlightAttribute));
			this.resistanceAttributesController.scrollContext.wraps = false;
			this.mutationsController.onSelected.RemoveAllListeners();
			this.mutationsController.onHighlight.RemoveAllListeners();
			this.mutationsController.onHighlight.AddListener(new UnityAction<FrameworkDataElement>(this.HandleHighlightMutation));
			this.mutationsController.onSelected.AddListener(new UnityAction<FrameworkDataElement>(this.HandleSelectMutation));
			this.effectsController.onSelected.RemoveAllListeners();
			this.effectsController.onHighlight.RemoveAllListeners();
			this.effectsController.onHighlight.AddListener(new UnityAction<FrameworkDataElement>(this.HandleHighlightEffect));
			this.UpdateData();
			this.UpdateViewFromData();
			this.primaryAttributesController.selectedPosition = 0;
			this.primaryAttributesController.scrollContext.ActivateAndEnable();
			this.HandleHighlightMutation(new CharacterMutationLineData
			{
				mutation = CharacterStatusScreen.mutations.FirstOrDefault<BaseMutation>()
			});
			ScrollContext<NavigationContext> scrollContext = this.horizNav;
			if (scrollContext.commandHandlers == null)
			{
				scrollContext.commandHandlers = new Dictionary<string, Action>();
			}
			this.horizNav.commandHandlers.Clear();
			scrollContext = this.horizNav;
			if (scrollContext.menuOptionDescriptions == null)
			{
				scrollContext.menuOptionDescriptions = new List<MenuOption>();
			}
			this.horizNav.menuOptionDescriptions.Clear();
			if (GO != null && GO.IsTrueKin() && CharacterStatusScreen.mutations.Count == 0)
			{
				this.mutationsPanelCanvas.enabled = false;
				this.horizNav.commandHandlers.Add(CharacterStatusScreen.SHOW_EFFECTS.InputCommand, global::XRL.UI.Framework.Event.Helpers.Handle(delegate
				{
					this.HandleShowEffects();
				}));
				this.horizNav.menuOptionDescriptions.Add(CharacterStatusScreen.SHOW_EFFECTS);
			}
			else
			{
				this.mutationsPanelCanvas.enabled = true;
				this.horizNav.commandHandlers.Add(CharacterStatusScreen.BUY_MUTATION.InputCommand, global::XRL.UI.Framework.Event.Helpers.Handle(delegate
				{
					this.HandleBuyMutation();
				}));
				this.horizNav.commandHandlers.Add(CharacterStatusScreen.SHOW_EFFECTS.InputCommand, global::XRL.UI.Framework.Event.Helpers.Handle(delegate
				{
					this.HandleShowEffects();
				}));
				this.horizNav.menuOptionDescriptions.Add(CharacterStatusScreen.BUY_MUTATION);
				this.horizNav.menuOptionDescriptions.Add(CharacterStatusScreen.SHOW_EFFECTS);
			}
			NavigationContext navigationContext = parent.screenGlobalContext;
			if (navigationContext.menuOptionDescriptions == null)
			{
				navigationContext.menuOptionDescriptions = new List<MenuOption>();
			}
			parent.screenGlobalContext.menuOptionDescriptions.Add(CharacterStatusScreen.SHOW_EFFECTS);
			parent.screenGlobalContext.menuOptionDescriptions.Add(CharacterStatusScreen.BUY_MUTATION);
			navigationContext = parent.screenGlobalContext;
			if (navigationContext.commandHandlers == null)
			{
				navigationContext.commandHandlers = new Dictionary<string, Action>();
			}
			parent.screenGlobalContext.commandHandlers["CmdStatusShowEffects"] = new Action(this.HandleShowEffects);
			parent.screenGlobalContext.commandHandlers["CmdStatusBuyMutation"] = new Action(this.HandleBuyMutation);
			base.ShowScreen(GO, parent);
			return this.horizNav;
		}

		// Token: 0x06002A69 RID: 10857 RVA: 0x000DF5F5 File Offset: 0x000DD7F5
		public override void Prewarm()
		{
			this.UpdateViewFromData();
		}

		// Token: 0x04001636 RID: 5686
		public ScrollContext<NavigationContext> attributesNav = new ScrollContext<NavigationContext>();

		// Token: 0x04001637 RID: 5687
		public ScrollContext<NavigationContext> vertNav = new ScrollContext<NavigationContext>();

		// Token: 0x04001638 RID: 5688
		public ScrollContext<NavigationContext> horizNav = new ScrollContext<NavigationContext>();

		// Token: 0x04001639 RID: 5689
		public FrameworkScroller primaryAttributesController;

		// Token: 0x0400163A RID: 5690
		public FrameworkScroller secondaryAttributesController;

		// Token: 0x0400163B RID: 5691
		public FrameworkScroller resistanceAttributesController;

		// Token: 0x0400163C RID: 5692
		public FrameworkScroller mutationsController;

		// Token: 0x0400163D RID: 5693
		public FrameworkScroller effectsController;

		// Token: 0x0400163E RID: 5694
		public global::UnityEngine.GameObject primaryAttributesHeader;

		// Token: 0x0400163F RID: 5695
		public global::UnityEngine.GameObject secondaryAttributesHeader;

		// Token: 0x04001640 RID: 5696
		public global::UnityEngine.GameObject resistanceAttributesHeader;

		// Token: 0x04001641 RID: 5697
		public UITextSkin primaryAttributesHeaderText;

		// Token: 0x04001642 RID: 5698
		public VerticalLayoutGroup attributesTopLayoutGroup;

		// Token: 0x04001643 RID: 5699
		public UITextSkin primaryAttributesDetails;

		// Token: 0x04001644 RID: 5700
		public UITextSkin secondaryAttributesDetails;

		// Token: 0x04001645 RID: 5701
		public UITextSkin resistanceAttributesDetails;

		// Token: 0x04001646 RID: 5702
		public UITextSkin mutationsDetails;

		// Token: 0x04001647 RID: 5703
		public UITextSkin nameText;

		// Token: 0x04001648 RID: 5704
		public UITextSkin classText;

		// Token: 0x04001649 RID: 5705
		public UITextSkin levelText;

		// Token: 0x0400164A RID: 5706
		public UIThreeColorProperties playerIcon;

		// Token: 0x0400164B RID: 5707
		public UITextSkin attributePointsText;

		// Token: 0x0400164C RID: 5708
		public UITextSkin mutationPointsText;

		// Token: 0x0400164D RID: 5709
		private static List<Effect> effects = new List<Effect>(64);

		// Token: 0x0400164E RID: 5710
		private static List<BaseMutation> mutations = new List<BaseMutation>(64);

		// Token: 0x0400164F RID: 5711
		private static List<Statistic> stats = new List<Statistic>(64);

		// Token: 0x04001650 RID: 5712
		private CharacterStatusScreen.PsychicGlimmerProxyBaseMutation glimmerProxy = new CharacterStatusScreen.PsychicGlimmerProxyBaseMutation();

		// Token: 0x04001651 RID: 5713
		public global::UnityEngine.GameObject mutationDetailsObject;

		// Token: 0x04001652 RID: 5714
		public Canvas mutationsPanelCanvas;

		// Token: 0x04001653 RID: 5715
		private static string[] PrimaryAttributes = new string[] { "Strength", "Agility", "Toughness", "Intelligence", "Willpower", "Ego" };

		// Token: 0x04001654 RID: 5716
		private static string[] SecondaryAttributes = new string[] { "Speed", "MoveSpeed", "AV", "DV", "MA" };

		// Token: 0x04001655 RID: 5717
		private static string[] SecondaryAttributesWithCP = new string[] { "Speed", "MoveSpeed", "AV", "DV", "MA", "CP" };

		// Token: 0x04001656 RID: 5718
		private static string[] ResistanceAttributes = new string[] { "AcidResistance", "ElectricResistance", "ColdResistance", "HeatResistance" };

		// Token: 0x04001657 RID: 5719
		public string mutationsTerm;

		// Token: 0x04001658 RID: 5720
		public string mutationTerm;

		// Token: 0x04001659 RID: 5721
		public string mutationTermCapital;

		// Token: 0x0400165A RID: 5722
		public string mutationColor;

		// Token: 0x0400165B RID: 5723
		public static int CP = int.MinValue;

		// Token: 0x0400165C RID: 5724
		public UITextSkin mutationTermText;

		// Token: 0x0400165D RID: 5725
		public UIThreeColorProperties mutationIcon;

		// Token: 0x0400165E RID: 5726
		public UITextSkin mutationTypeText;

		// Token: 0x0400165F RID: 5727
		public UITextSkin mutationNameText;

		// Token: 0x04001660 RID: 5728
		public UITextSkin mutationRankText;

		// Token: 0x04001661 RID: 5729
		public static readonly MenuOption BUY_MUTATION = new MenuOption
		{
			Id = "BUY_MUTATION",
			KeyDescription = "Buy Mutation",
			InputCommand = "CmdStatusBuyMutation",
			Description = "Buy Mutation"
		};

		// Token: 0x04001662 RID: 5730
		public static readonly MenuOption SHOW_EFFECTS = new MenuOption
		{
			Id = "SHOW_EFFECTS",
			KeyDescription = "Show Effects",
			InputCommand = "CmdStatusShowEffects",
			Description = "Show Effects"
		};

		// Token: 0x04001663 RID: 5731
		private global::XRL.World.GameObject GO;

		// Token: 0x02001873 RID: 6259
		private class PsychicGlimmerProxyBaseMutation : BaseMutation
		{
			// Token: 0x0600E0A0 RID: 57504 RVA: 0x0051A7AE File Offset: 0x005189AE
			public override IRenderable GetIcon()
			{
				if (this.renderable == null)
				{
					this.renderable = new Renderable("Tiles2/psychic-glimmer.png", " ", "&M", null, 'b');
				}
				return this.renderable;
			}

			// Token: 0x0600E0A1 RID: 57505 RVA: 0x0051A7DB File Offset: 0x005189DB
			public override string GetDescription()
			{
				return PsychicHunterSystem.GetPsychicGlimmerDescription(base.Level);
			}

			// Token: 0x0600E0A2 RID: 57506 RVA: 0x0051A7E8 File Offset: 0x005189E8
			public override string GetLevelText(int Level)
			{
				return "";
			}

			// Token: 0x0600E0A3 RID: 57507 RVA: 0x0051A7EF File Offset: 0x005189EF
			public override int GetUIDisplayLevel()
			{
				return this.glimmer;
			}

			// Token: 0x04006273 RID: 25203
			public int glimmer;

			// Token: 0x04006274 RID: 25204
			private IRenderable renderable;
		}
	}
}
