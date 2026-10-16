using System;
using XRL.Names;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;

namespace XRL.World
{
	// Token: 0x02000955 RID: 2389
	public class HeroMaker
	{
		// Token: 0x060062DE RID: 25310 RVA: 0x001E5152 File Offset: 0x001E3352
		public static GameObject CreateHero(string BaseBlueprint)
		{
			return HeroMaker.MakeHero(GameObject.Create(BaseBlueprint), null, null, -1, "Hero");
		}

		// Token: 0x060062DF RID: 25311 RVA: 0x001E5168 File Offset: 0x001E3368
		public static GameObject MakeHero(GameObject BaseCreature, string[] AdditionalBaseTemplates = null, string[] AdditionalSpecializationTemplates = null, int TierOverride = -1, string SpecialType = "Hero")
		{
			GameObject gameObject;
			try
			{
				if (BaseCreature.GetIntProperty("Hero", 0) > 0)
				{
					gameObject = BaseCreature;
				}
				else
				{
					if (!BaseCreature.HasPart<Brain>())
					{
						AnimateObject.Animate(BaseCreature, null, null);
					}
					string propertyOrTag = BaseCreature.GetPropertyOrTag("Role", null);
					BaseCreature.SetIntProperty("Hero", 1, false);
					BaseCreature.SetStringProperty("Role", "Hero", false);
					string text = HeroMaker.ResolveTemplateTag(BaseCreature, "HeroNameColor", "M", AdditionalBaseTemplates, AdditionalSpecializationTemplates);
					string text2 = HeroMaker.ResolveTemplateTag(BaseCreature, "HeroTileColor", "&M", AdditionalBaseTemplates, AdditionalSpecializationTemplates);
					string text3 = HeroMaker.ResolveTemplateTag(BaseCreature, "HeroColorString", "&M", AdditionalBaseTemplates, AdditionalSpecializationTemplates);
					string text4 = HeroMaker.ResolveTemplateTag(BaseCreature, "HeroDetailColor", "same", AdditionalBaseTemplates, AdditionalSpecializationTemplates);
					if (text2 == "same")
					{
						text2 = null;
					}
					if (text3 == "same")
					{
						text3 = null;
					}
					if (text4 == "same")
					{
						text4 = null;
					}
					if (!text2.IsNullOrEmpty() || !text3.IsNullOrEmpty() || !text4.IsNullOrEmpty())
					{
						BaseCreature.RequirePart<HeroIconColor>(false).Configure(text3, text2.IsNullOrEmpty() ? text3 : text2, text4, null, null, new int?(HeroMaker.ICON_COLOR_PRIORITY), new int?(HeroMaker.ICON_COLOR_PRIORITY), new int?(HeroMaker.ICON_COLOR_PRIORITY), null, null);
					}
					if (BaseCreature.HasStat("Strength"))
					{
						BaseCreature.GetStat("Strength").BoostStat(Convert.ToInt32(HeroMaker.ResolveTemplateTag(BaseCreature, "HeroStrBoost", "1", AdditionalBaseTemplates, AdditionalSpecializationTemplates)));
					}
					if (BaseCreature.HasStat("Intelligence"))
					{
						BaseCreature.GetStat("Intelligence").BoostStat(Convert.ToInt32(HeroMaker.ResolveTemplateTag(BaseCreature, "HeroIntBoost", "1", AdditionalBaseTemplates, AdditionalSpecializationTemplates)));
					}
					if (BaseCreature.HasStat("Toughness"))
					{
						BaseCreature.GetStat("Toughness").BoostStat(Convert.ToInt32(HeroMaker.ResolveTemplateTag(BaseCreature, "HeroTouBoost", "1", AdditionalBaseTemplates, AdditionalSpecializationTemplates)));
					}
					if (BaseCreature.HasStat("Willpower"))
					{
						BaseCreature.GetStat("Willpower").BoostStat(Convert.ToInt32(HeroMaker.ResolveTemplateTag(BaseCreature, "HeroWilBoost", "1", AdditionalBaseTemplates, AdditionalSpecializationTemplates)));
					}
					if (BaseCreature.HasStat("Ego"))
					{
						BaseCreature.GetStat("Ego").BoostStat(Convert.ToInt32(HeroMaker.ResolveTemplateTag(BaseCreature, "HeroEgoBoost", "1", AdditionalBaseTemplates, AdditionalSpecializationTemplates)));
					}
					if (BaseCreature.HasStat("Agility"))
					{
						BaseCreature.GetStat("Agility").BoostStat(Convert.ToInt32(HeroMaker.ResolveTemplateTag(BaseCreature, "HeroAgiBoost", "1", AdditionalBaseTemplates, AdditionalSpecializationTemplates)));
					}
					if (BaseCreature.HasStat("Hitpoints"))
					{
						BaseCreature.GetStat("Hitpoints").BaseValue = (int)((double)BaseCreature.GetStat("Hitpoints").BaseValue * Convert.ToDouble(HeroMaker.ResolveTemplateTag(BaseCreature, "HeroHPBoost", "2", AdditionalBaseTemplates, AdditionalSpecializationTemplates)));
					}
					if (BaseCreature.HasStat("Level"))
					{
						BaseCreature.GetStat("Level").BaseValue = Math.Max((int)((double)BaseCreature.GetStat("Level").Value * Convert.ToDouble(HeroMaker.ResolveTemplateTag(BaseCreature, "HeroLevelMultiplier", "1.5", AdditionalBaseTemplates, AdditionalSpecializationTemplates))), Convert.ToInt32(HeroMaker.ResolveTemplateTag(BaseCreature, "HeroMinLevel", "0", AdditionalBaseTemplates, AdditionalSpecializationTemplates)));
					}
					if (BaseCreature.HasStat("XP") && BaseCreature.HasStat("Level"))
					{
						BaseCreature.GetStat("XP").BaseValue = Leveler.GetXPForLevel(BaseCreature.GetStat("Level").Value);
					}
					if (BaseCreature.HasStat("XPValue") && BaseCreature.HasStat("Level"))
					{
						float num = (float)BaseCreature.GetStat("Level").Value;
						num /= 2f;
						if (propertyOrTag == "Minion")
						{
							BaseCreature.GetStat("XPValue").BaseValue = (int)(num * 100f);
						}
						else
						{
							BaseCreature.GetStat("XPValue").BaseValue = (int)(num * 200f);
						}
					}
					string text5 = HeroMaker.ResolveTemplateTag(BaseCreature, "HeroSkills", null, AdditionalBaseTemplates, AdditionalSpecializationTemplates);
					if (!text5.IsNullOrEmpty())
					{
						foreach (string text6 in text5.CachedCommaExpansion())
						{
							BaseCreature.AddSkill(text6, null, null);
						}
					}
					string text7 = HeroMaker.ResolveTemplateTag(BaseCreature, "HeroSelfPreservationThreshold", null, AdditionalBaseTemplates, AdditionalSpecializationTemplates);
					if (!text7.IsNullOrEmpty())
					{
						BaseCreature.RequirePart<AISelfPreservation>(false).Threshold = text7.RollCached();
					}
					string text8 = HeroMaker.ResolveTemplateTag(BaseCreature, "SimpleConversation", null, AdditionalBaseTemplates, AdditionalSpecializationTemplates);
					string text9 = HeroMaker.ResolveTemplateTag(BaseCreature, "HeroConversation", null, AdditionalBaseTemplates, AdditionalSpecializationTemplates);
					if (!text9.IsNullOrEmpty())
					{
						BaseCreature.RequirePart<ConversationScript>(false).ConversationID = text9;
					}
					else if (!text8.IsNullOrEmpty())
					{
						BaseCreature.RequirePart<ConversationScript>(false);
						BaseCreature.SetStringProperty("SimpleConversation", text8, false);
					}
					int num2 = 0;
					int num3 = 0;
					string text10 = HeroMaker.ResolveTemplateTag(BaseCreature, "HeroGenotype", "none", AdditionalBaseTemplates, AdditionalSpecializationTemplates);
					if (text10 != "True Kin" && !BaseCreature.HasTag("Robot"))
					{
						if (text10 != "Chimera")
						{
							num2 = HeroMaker.ResolveTemplateTag(BaseCreature, "HeroMentalMutations", "0-2", AdditionalBaseTemplates, AdditionalSpecializationTemplates).RollCached();
						}
						if (text10 != "Esper")
						{
							num3 = HeroMaker.ResolveTemplateTag(BaseCreature, "HeroPhysicalMutations", "0-2", AdditionalBaseTemplates, AdditionalSpecializationTemplates).RollCached();
						}
						if (text10 == "Chimera")
						{
							num3++;
						}
						else if (text10 == "Esper")
						{
							num2++;
						}
					}
					if (num2 > 0 || num3 > 0)
					{
						Mutations mutations = BaseCreature.RequirePart<Mutations>(false);
						for (int i = 0; i < num2; i++)
						{
							BaseMutation randomMutation;
							do
							{
								randomMutation = MutationFactory.GetRandomMutation("Mental");
							}
							while (randomMutation != null && mutations.HasMutation(randomMutation));
							if (randomMutation != null)
							{
								mutations.AddMutation(randomMutation, "1d4".RollCached(), true);
							}
						}
						for (int j = 0; j < num3; j++)
						{
							BaseMutation randomMutation2;
							do
							{
								randomMutation2 = MutationFactory.GetRandomMutation("Physical");
							}
							while (randomMutation2 != null && mutations.HasMutation(randomMutation2));
							if (randomMutation2 != null)
							{
								mutations.AddMutation(randomMutation2, "1d4".RollCached(), true);
							}
						}
					}
					string text11 = HeroMaker.ResolveTemplateTag(BaseCreature, "HeroMutationPopulation", null, AdditionalBaseTemplates, AdditionalSpecializationTemplates);
					if (!text11.IsNullOrEmpty())
					{
						BaseCreature.MutateFromPopulationTable(text11, (TierOverride == -1) ? BaseCreature.GetTier() : TierOverride);
					}
					string text12 = NameMaker.MakeHonorific(BaseCreature, null, null, null, null, null, null, null, null, null, SpecialType, null, true, null, null);
					bool flag = !text12.IsNullOrEmpty() || BaseCreature.HasHonorific;
					string text13 = NameMaker.MakeEpithet(BaseCreature, null, null, null, null, null, null, null, null, null, SpecialType, null, true, new bool?(flag), null);
					bool flag2 = !text13.IsNullOrEmpty() || BaseCreature.HasEpithet;
					string text14 = NameMaker.MakeTitle(BaseCreature, null, null, null, null, null, null, null, null, null, SpecialType, null, true, new bool?(flag), new bool?(flag2));
					string text15 = NameMaker.MakeExtraTitle(BaseCreature, null, null, null, null, null, null, null, null, null, SpecialType, null, true, new bool?(flag), new bool?(flag2));
					if (!text12.IsNullOrEmpty())
					{
						if (!text13.IsNullOrEmpty() && text13.HasDelimitedSubstring(' ', text12, StringComparison.CurrentCultureIgnoreCase))
						{
							text13 = null;
						}
						if (!text14.IsNullOrEmpty() && text14.HasDelimitedSubstring(' ', text12, StringComparison.CurrentCultureIgnoreCase))
						{
							text14 = null;
						}
						if (!text15.IsNullOrEmpty() && text15.HasDelimitedSubstring(' ', text12, StringComparison.CurrentCultureIgnoreCase))
						{
							text15 = null;
						}
					}
					if (!text13.IsNullOrEmpty())
					{
						if (!text14.IsNullOrEmpty() && text14.HasDelimitedSubstring(' ', text13, StringComparison.CurrentCultureIgnoreCase))
						{
							text14 = null;
						}
						if (!text15.IsNullOrEmpty() && text15.HasDelimitedSubstring(' ', text13, StringComparison.CurrentCultureIgnoreCase))
						{
							text15 = null;
						}
					}
					BaseCreature.GiveProperName(null, false, SpecialType, true, new bool?(flag), new bool?(flag2), null, null);
					if (!text12.IsNullOrEmpty())
					{
						BaseCreature.RequirePart<Honorifics>(false).AddHonorific(text12, 40);
					}
					if (!text13.IsNullOrEmpty())
					{
						BaseCreature.RequirePart<Epithets>(false).AddEpithet(text13, -40);
					}
					if (!text14.IsNullOrEmpty())
					{
						BaseCreature.RequirePart<Titles>(false).AddTitle(text14, -40);
					}
					if (!text15.IsNullOrEmpty())
					{
						BaseCreature.RequirePart<Titles>(false).AddTitle(text15, 0);
					}
					if (!text.IsNullOrEmpty() && text != "y" && text != "&y")
					{
						BaseCreature.RequirePart<DisplayNameColor>(false).SetColorByPriority(text, 30);
					}
					if (BaseCreature.HasPart<Inventory>())
					{
						int num4;
						if (TierOverride != -1)
						{
							num4 = TierOverride;
							BaseCreature.SetIntProperty("inventoryTier", num4, false);
						}
						else
						{
							num4 = BaseCreature.GetTier();
						}
						string mark = null;
						string markColor = null;
						if (BaseCreature.HasTag("HeroAddMakersMark"))
						{
							mark = MakersMark.Generate(true);
							markColor = HeroMaker.ResolveTemplateTag(BaseCreature, "HeroMakersMarkColor", "R", AdditionalBaseTemplates, AdditionalSpecializationTemplates);
							if (markColor == "random")
							{
								markColor = (60.in100() ? Crayons.GetRandomColor(null) : Crayons.GetRandomDarkColor(null));
							}
							else if (markColor == "randombright")
							{
								markColor = Crayons.GetRandomColor(null);
							}
							HasMakersMark hasMakersMark = BaseCreature.RequirePart<HasMakersMark>(false);
							hasMakersMark.Mark = mark;
							hasMakersMark.Color = markColor;
						}
						string tag = BaseCreature.GetTag("HeroInventory", null);
						if (!tag.IsNullOrEmpty())
						{
							if (!mark.IsNullOrEmpty())
							{
								BaseCreature.EquipFromPopulationTable(tag, num4, delegate(GameObject obj)
								{
									if (obj.HasPart<Description>() && !obj.HasTag("AlwaysStack"))
									{
										obj.RequirePart<MakersMark>(false).AddCrafter(BaseCreature, mark, markColor);
									}
								}, null, false, true);
							}
							else
							{
								BaseCreature.EquipFromPopulationTable(tag, num4, null, null, false, true);
							}
						}
					}
					if (BaseCreature.HasTag("HeroHasGuards"))
					{
						BaseCreature.RemovePart<HasGuards>();
						HasGuards hasGuards = new HasGuards();
						hasGuards.NumberOfGuards = BaseCreature.GetTag("HeroHasGuards", "2-4");
						BaseCreature.AddPart<HasGuards>(hasGuards, true, false);
					}
					if (BaseCreature.HasTag("HeroHasSlaves"))
					{
						string text16 = null;
						HasSlaves hasSlaves;
						if (BaseCreature.TryGetPart<HasSlaves>(out hasSlaves))
						{
							text16 = hasSlaves.SlaveTier;
							BaseCreature.RemovePart(hasSlaves);
						}
						hasSlaves = new HasSlaves();
						hasSlaves.NumberOfSlaves = BaseCreature.GetTag("HeroHasSlaves", "3-4");
						if (!text16.IsNullOrEmpty())
						{
							hasSlaves.SlaveTier = text16;
						}
						BaseCreature.AddPart<HasSlaves>(hasSlaves, true, false);
					}
					if (BaseCreature.HasTag("HeroHasThralls"))
					{
						BaseCreature.RemovePart<HasThralls>();
						HasThralls hasThralls = new HasThralls();
						hasThralls.NumberOfThralls = BaseCreature.GetTag("HeroHasThralls", "4-7");
						BaseCreature.AddPart<HasThralls>(hasThralls, true, false);
					}
					if (!HeroMaker.ResolveTemplateTag(BaseCreature, "HeroNoWaterRitual", "false", AdditionalBaseTemplates, AdditionalSpecializationTemplates).EqualsNoCase("true"))
					{
						BaseCreature.RemovePart<GivesRep>();
						BaseCreature.AddPart<GivesRep>(new GivesRep(), true, false);
						BaseCreature.FireEvent("FactionsAdded");
					}
					string text17 = HeroMaker.ResolveTemplateTag(BaseCreature, "HeroFactionHeirloomChance", null, AdditionalBaseTemplates, AdditionalSpecializationTemplates);
					int num5;
					if (!text17.IsNullOrEmpty() && int.TryParse(text17, out num5) && num5.in100())
					{
						string primaryFaction = BaseCreature.GetPrimaryFaction(false);
						if (!primaryFaction.IsNullOrEmpty())
						{
							Faction ifExists = Factions.GetIfExists(primaryFaction);
							if (ifExists != null)
							{
								BaseCreature.ReceiveObject(ifExists.GenerateHeirloom(), false, null);
							}
						}
					}
					if (BaseCreature.Render != null)
					{
						BaseCreature.Render.RenderLayer++;
					}
					BaseCreature.FireEvent("MadeHero");
					gameObject = BaseCreature;
				}
			}
			catch (Exception ex)
			{
				string text18 = "Error making heroic: ";
				string blueprint = BaseCreature.Blueprint;
				string text19 = " -> ";
				Exception ex2 = ex;
				MetricsManager.LogError(text18 + blueprint + text19 + ((ex2 != null) ? ex2.ToString() : null));
				gameObject = null;
			}
			return gameObject;
		}

		// Token: 0x060062E0 RID: 25312 RVA: 0x001E5F14 File Offset: 0x001E4114
		public static GameObject MakeHero(GameObject BaseCreature, string AdditionalSpecializationTemplate, int TierOverride = -1, string SpecialType = "Hero")
		{
			string[] array = null;
			if (!AdditionalSpecializationTemplate.IsNullOrEmpty())
			{
				array = new string[] { AdditionalSpecializationTemplate };
			}
			return HeroMaker.MakeHero(BaseCreature, null, array, TierOverride, SpecialType);
		}

		// Token: 0x060062E1 RID: 25313 RVA: 0x001E5F40 File Offset: 0x001E4140
		public static GameObject MakeHero(GameObject BaseCreature, string AdditionalBaseTemplate, string AdditionalSpecializationTemplate, int TierOverride = -1, string SpecialType = "Hero")
		{
			string[] array = null;
			string[] array2 = null;
			if (!AdditionalBaseTemplate.IsNullOrEmpty())
			{
				array = new string[] { AdditionalBaseTemplate };
			}
			if (!AdditionalSpecializationTemplate.IsNullOrEmpty())
			{
				array2 = new string[] { AdditionalSpecializationTemplate };
			}
			return HeroMaker.MakeHero(BaseCreature, array, array2, TierOverride, SpecialType);
		}

		// Token: 0x060062E2 RID: 25314 RVA: 0x001E5F84 File Offset: 0x001E4184
		public static string ResolveTemplateTag(GameObject BaseCreature, string Tag, string Default = null, string[] AdditionalBaseTemplates = null, string[] AdditionalSpecializationTemplates = null)
		{
			string text = Default;
			if (AdditionalBaseTemplates != null)
			{
				foreach (string text2 in AdditionalBaseTemplates)
				{
					GameObjectBlueprint blueprintIfExists = GameObjectFactory.Factory.GetBlueprintIfExists(text2);
					if (blueprintIfExists != null)
					{
						text = blueprintIfExists.GetTag(Tag, text);
					}
				}
			}
			text = BaseCreature.GetTag(Tag, text);
			if (AdditionalSpecializationTemplates != null)
			{
				foreach (string text3 in AdditionalSpecializationTemplates)
				{
					GameObjectBlueprint blueprintIfExists2 = GameObjectFactory.Factory.GetBlueprintIfExists(text3);
					if (blueprintIfExists2 != null)
					{
						text = blueprintIfExists2.GetTag(Tag, text);
					}
				}
			}
			return BaseCreature.GetStringProperty(Tag, text);
		}

		// Token: 0x04002E66 RID: 11878
		public static readonly int ICON_COLOR_PRIORITY = 100;
	}
}
