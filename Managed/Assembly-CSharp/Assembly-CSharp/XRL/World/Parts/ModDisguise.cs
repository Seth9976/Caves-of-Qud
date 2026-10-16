using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleLib.Console;
using Qud.API;
using XRL.Language;
using XRL.UI;
using XRL.World.Anatomy;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02001131 RID: 4401
	[Serializable]
	public class ModDisguise : IModification
	{
		// Token: 0x0600AE39 RID: 44601 RVA: 0x00405FA6 File Offset: 0x004041A6
		public ModDisguise()
		{
		}

		// Token: 0x0600AE3A RID: 44602 RVA: 0x00405FAE File Offset: 0x004041AE
		public ModDisguise(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600AE3B RID: 44603 RVA: 0x00405FB7 File Offset: 0x004041B7
		public override void Configure()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600AE3C RID: 44604 RVA: 0x00405FC0 File Offset: 0x004041C0
		public override string GetModificationDisplayName()
		{
			string disguiseAppearanceDescription = this.GetDisguiseAppearanceDescription();
			if (disguiseAppearanceDescription == null)
			{
				return "a disguise";
			}
			return "a disguise of " + disguiseAppearanceDescription;
		}

		// Token: 0x0600AE3D RID: 44605 RVA: 0x00405FE8 File Offset: 0x004041E8
		public override bool ModificationApplicable(GameObject Object)
		{
			return IModification.CheckWornSlot(Object, "Body", "Back", null, null, false, true, true, false);
		}

		// Token: 0x0600AE3E RID: 44606 RVA: 0x0040600C File Offset: 0x0040420C
		public override bool BeingAppliedBy(GameObject Object, GameObject Who)
		{
			if (Who.IsPlayer())
			{
				List<GameObjectBlueprint> disguiseBlueprints = ModDisguise.GetDisguiseBlueprints();
				List<string[]> list = new List<string[]>(disguiseBlueprints.Count);
				List<string> list2 = new List<string>(disguiseBlueprints.Count);
				foreach (GameObjectBlueprint gameObjectBlueprint in disguiseBlueprints)
				{
					if (gameObjectBlueprint.HasBeenSeen())
					{
						string partParameter = gameObjectBlueprint.GetPartParameter<string>("Render", "DisplayName", "creature of some kind");
						if (!list2.Contains(partParameter))
						{
							list.Add(new string[]
							{
								gameObjectBlueprint.Name,
								partParameter,
								ColorUtility.StripFormatting(partParameter)
							});
							list2.Add(partParameter);
						}
					}
				}
				if (list.Count == 0)
				{
					Popup.Show("You aren't familiar enough with any creatures to make a disguise.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					return false;
				}
				list.Sort((string[] a, string[] b) => a[2].CompareTo(b[2]));
				List<string> list3 = new List<string>(16);
				List<object> list4 = new List<object>(16);
				List<char> list5 = new List<char>(16);
				char c = 'a';
				foreach (string[] array in list)
				{
					list3.Add(array[1]);
					list4.Add(array[0]);
					List<char> list6 = list5;
					int num;
					if (c > 'z')
					{
						num = 32;
					}
					else
					{
						c = (char)((num = (int)c) + 1);
					}
					list6.Add((char)num);
				}
				int num2 = Popup.PickOption("Choose a disguise to make.", null, "", "Sounds/UI/ui_notification", list3.ToArray(), list5.ToArray(), null, null, null, null, null, 0, 60, 0, -1, true, false, false, true, false, null, null);
				if (num2 < 0)
				{
					return false;
				}
				this.DisguiseBlueprint = list4[num2] as string;
			}
			else
			{
				string Species = Who.GetTag("Species", null) ?? Who.Blueprint;
				GameObjectBlueprint randomElement = (from bp in ModDisguise.GetDisguiseBlueprints()
					where (bp.GetTag("Species", "") ?? bp.Name) != Species
					select bp).GetRandomElement<GameObjectBlueprint>(null);
				if (randomElement != null)
				{
					this.DisguiseBlueprint = randomElement.Name;
				}
			}
			return true;
		}

		// Token: 0x0600AE3F RID: 44607 RVA: 0x00406248 File Offset: 0x00404448
		public override void ApplyModification(GameObject Object)
		{
			GameObjectBlueprint gameObjectBlueprint;
			if (this.DisguiseBlueprint.IsNullOrEmpty() || !GameObjectFactory.Factory.Blueprints.TryGetValue(this.DisguiseBlueprint, out gameObjectBlueprint))
			{
				gameObjectBlueprint = ModDisguise.SelectDisguiseBlueprint();
				this.DisguiseBlueprint = gameObjectBlueprint.Name;
			}
			string partParameter = gameObjectBlueprint.GetPartParameter<string>("Brain", "Factions", null);
			if (partParameter != null)
			{
				foreach (string text in partParameter.CachedCommaExpansion())
				{
					int num = Brain.ExtractFactionMembership(ref text);
					if (num > 0)
					{
						Faction faction = Factions.Get(text);
						if (faction != null && faction.Visible)
						{
							try
							{
								int num2 = this.GetFactionReputationBase() * num / 100;
								AddsRep.AddModifier(Object, text + ":" + num2.ToString() + ":hidden");
							}
							catch
							{
							}
						}
					}
				}
			}
			base.IncreaseDifficultyIfComplex(2, null);
		}

		// Token: 0x0600AE40 RID: 44608 RVA: 0x0040634C File Offset: 0x0040454C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || (Options.AutogetSpecialItems && ID == AutoexploreObjectEvent.ID) || ID == EquippedEvent.ID || ID == PooledEvent<GetApparentSpeciesEvent>.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID || ID == UnequippedEvent.ID;
		}

		// Token: 0x0600AE41 RID: 44609 RVA: 0x0040639C File Offset: 0x0040459C
		public override bool HandleEvent(AutoexploreObjectEvent E)
		{
			if (E.Command == null && Options.AutogetSpecialItems)
			{
				E.Command = "Autoget";
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AE42 RID: 44610 RVA: 0x004063BF File Offset: 0x004045BF
		public override bool HandleEvent(EquippedEvent E)
		{
			if (this.ParentObject.IsWorn(E.Part))
			{
				this.ApplyDisguise(E.Actor);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AE43 RID: 44611 RVA: 0x004063E7 File Offset: 0x004045E7
		public override bool HandleEvent(UnequippedEvent E)
		{
			this.UnapplyDisguise(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600AE44 RID: 44612 RVA: 0x004063FC File Offset: 0x004045FC
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				if (!this.DisguiseBlueprint.IsNullOrEmpty() && GameObjectFactory.Factory.Blueprints.ContainsKey(this.DisguiseBlueprint))
				{
					GameObjectBlueprint gameObjectBlueprint = GameObjectFactory.Factory.Blueprints[this.DisguiseBlueprint];
					E.AddClause("and " + gameObjectBlueprint.GetPartParameter<string>("Render", "DisplayName", "creature") + " disguise", -20);
				}
				else
				{
					E.AddClause("and disguise", -20);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AE45 RID: 44613 RVA: 0x0040649D File Offset: 0x0040469D
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(this.GetInstanceDescription());
			return base.HandleEvent(E);
		}

		// Token: 0x0600AE46 RID: 44614 RVA: 0x004064B8 File Offset: 0x004046B8
		public override bool HandleEvent(GetApparentSpeciesEvent E)
		{
			if (E.Priority < ModDisguise.APPARENT_SPECIES_PRIORITY)
			{
				string disguiseSpecies = this.GetDisguiseSpecies();
				if (disguiseSpecies != null)
				{
					E.ApparentSpecies = disguiseSpecies;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AE47 RID: 44615 RVA: 0x004064EA File Offset: 0x004046EA
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600AE48 RID: 44616 RVA: 0x004064ED File Offset: 0x004046ED
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeginBeingEquipped");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AE49 RID: 44617 RVA: 0x00406504 File Offset: 0x00404704
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeginBeingEquipped")
			{
				BodyPart bodyPart = E.GetParameter("BodyPart") as BodyPart;
				if (this.ParentObject.IsWorn(bodyPart))
				{
					foreach (GameObject gameObject in E.GetGameObjectParameter("EquippingObject").GetEquippedObjects())
					{
						if (gameObject.HasPart<ModDisguise>() && (bodyPart == null || bodyPart.Equipped != gameObject))
						{
							if (!E.IsSilent() && E.GetIntParameter("AutoEquipTry", 0) <= 1)
							{
								Popup.Show("You are already wearing a disguise.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
							}
							return false;
						}
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600AE4A RID: 44618 RVA: 0x004065DC File Offset: 0x004047DC
		private void ApplyDisguise(GameObject who)
		{
			who.ApplyEffect(new Disguised(this.DisguiseBlueprint, this.GetDisguiseAppearanceDescription()), null);
		}

		// Token: 0x0600AE4B RID: 44619 RVA: 0x004065F7 File Offset: 0x004047F7
		private void UnapplyDisguise(GameObject who)
		{
			who.RemoveEffect(typeof(Disguised), delegate(Effect fx)
			{
				Disguised disguised = fx as Disguised;
				return disguised != null && disguised.BlueprintName == this.DisguiseBlueprint;
			});
		}

		// Token: 0x0600AE4C RID: 44620 RVA: 0x00406616 File Offset: 0x00404816
		public static string GetDescription(int Tier)
		{
			return "Disguise: This item changes its wearer's appearance and improves their reputation with the faction of the mimicked creature.";
		}

		// Token: 0x0600AE4D RID: 44621 RVA: 0x0040661D File Offset: 0x0040481D
		public static int GetFactionReputationBase(int Tier)
		{
			return 500;
		}

		// Token: 0x0600AE4E RID: 44622 RVA: 0x00406624 File Offset: 0x00404824
		public int GetFactionReputationBase()
		{
			return ModDisguise.GetFactionReputationBase(this.Tier);
		}

		// Token: 0x0600AE4F RID: 44623 RVA: 0x00406634 File Offset: 0x00404834
		public string GetDisguiseAppearanceDescription()
		{
			if (this.DisguiseBlueprint.IsNullOrEmpty() || !GameObjectFactory.Factory.Blueprints.ContainsKey(this.DisguiseBlueprint))
			{
				return null;
			}
			GameObjectBlueprint gameObjectBlueprint = GameObjectFactory.Factory.Blueprints[this.DisguiseBlueprint];
			string partParameter = gameObjectBlueprint.GetPartParameter<string>("Render", "DisplayName", "creature of some kind");
			string text = gameObjectBlueprint.GetxTag("Grammar", "iArticle", null);
			if (base.HasPropertyOrTag("OverrideIArticle"))
			{
				text = base.GetPropertyOrTag("OverrideIArticle", null);
			}
			if (text == null)
			{
				return Grammar.A(partParameter, false);
			}
			return text + " " + partParameter;
		}

		// Token: 0x0600AE50 RID: 44624 RVA: 0x004066D4 File Offset: 0x004048D4
		public string GetInstanceDescription()
		{
			string disguiseAppearanceDescription = this.GetDisguiseAppearanceDescription();
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			if (disguiseAppearanceDescription != null)
			{
				stringBuilder.Append("Disguise: This item makes its wearer appear to be ").Append(disguiseAppearanceDescription).Append('.');
			}
			else
			{
				stringBuilder.Append(ModDisguise.GetDescription(this.Tier));
			}
			if (!this.DisguiseBlueprint.IsNullOrEmpty() && GameObjectFactory.Factory.Blueprints.ContainsKey(this.DisguiseBlueprint))
			{
				string partParameter = GameObjectFactory.Factory.Blueprints[this.DisguiseBlueprint].GetPartParameter<string>("Brain", "Factions", null);
				if (partParameter != null)
				{
					foreach (string text in partParameter.CachedCommaExpansion())
					{
						int num = Brain.ExtractFactionMembership(ref text);
						if (num > 0)
						{
							Faction faction = Factions.Get(text);
							if (faction != null && faction.Visible)
							{
								try
								{
									int num2 = this.GetFactionReputationBase() * num / 100;
									stringBuilder.Append("\n+").Append(num2).Append(" reputation with ")
										.Append(faction.GetFormattedName());
								}
								catch
								{
								}
							}
						}
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600AE51 RID: 44625 RVA: 0x00406828 File Offset: 0x00404A28
		public static bool IsBlueprintUsableForDisguise(GameObjectBlueprint BP)
		{
			if (!BP.DescendsFrom("Creature"))
			{
				return false;
			}
			if (!BP.HasPart("Combat"))
			{
				return false;
			}
			if (!EncountersAPI.IsEligibleForDynamicEncounters(BP))
			{
				return false;
			}
			if (BP.HasTag("NoDisguise", false))
			{
				return false;
			}
			if (BP.HasProperName())
			{
				return false;
			}
			string partParameter = BP.GetPartParameter<string>("Render", "DisplayName", null);
			return !partParameter.IsNullOrEmpty() && !partParameter.Contains("[") && !partParameter.Contains("]");
		}

		// Token: 0x0600AE52 RID: 44626 RVA: 0x004068B0 File Offset: 0x00404AB0
		public static bool IsBlueprintUsableForDisguise(string BP)
		{
			GameObjectBlueprint blueprintIfExists = GameObjectFactory.Factory.GetBlueprintIfExists(BP);
			return blueprintIfExists != null && ModDisguise.IsBlueprintUsableForDisguise(blueprintIfExists);
		}

		// Token: 0x0600AE53 RID: 44627 RVA: 0x004068D4 File Offset: 0x00404AD4
		private static List<GameObjectBlueprint> GetDisguiseBlueprints()
		{
			if (ModDisguise.DisguiseBlueprints == null)
			{
				ModDisguise.DisguiseBlueprints = new List<GameObjectBlueprint>(256);
				foreach (GameObjectBlueprint gameObjectBlueprint in GameObjectFactory.Factory.BlueprintList)
				{
					if (ModDisguise.IsBlueprintUsableForDisguise(gameObjectBlueprint))
					{
						ModDisguise.DisguiseBlueprints.Add(gameObjectBlueprint);
					}
				}
			}
			return ModDisguise.DisguiseBlueprints;
		}

		// Token: 0x0600AE54 RID: 44628 RVA: 0x00406954 File Offset: 0x00404B54
		private static GameObjectBlueprint SelectDisguiseBlueprint()
		{
			return EncountersAPI.GetACreatureBlueprintModel(new Predicate<GameObjectBlueprint>(ModDisguise.IsBlueprintUsableForDisguise));
		}

		// Token: 0x0600AE55 RID: 44629 RVA: 0x00406968 File Offset: 0x00404B68
		public static string GetDisguiseSpecies(string BP)
		{
			if (BP.IsNullOrEmpty())
			{
				return null;
			}
			GameObjectBlueprint blueprintIfExists = GameObjectFactory.Factory.GetBlueprintIfExists(BP);
			if (blueprintIfExists == null)
			{
				return null;
			}
			return blueprintIfExists.GetPropertyOrTag("Species") ?? BP;
		}

		// Token: 0x0600AE56 RID: 44630 RVA: 0x004069A0 File Offset: 0x00404BA0
		public string GetDisguiseSpecies()
		{
			return ModDisguise.GetDisguiseSpecies(this.DisguiseBlueprint);
		}

		// Token: 0x040047E7 RID: 18407
		public static readonly int APPARENT_SPECIES_PRIORITY = 10;

		// Token: 0x040047E8 RID: 18408
		public string DisguiseBlueprint;

		// Token: 0x040047E9 RID: 18409
		[NonSerialized]
		private static List<GameObjectBlueprint> DisguiseBlueprints = null;
	}
}
