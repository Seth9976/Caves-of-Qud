using System;
using System.Collections.Generic;
using System.Linq;
using HistoryKit;
using Qud.API;
using XRL.Language;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	// Token: 0x02000E55 RID: 3669
	[Serializable]
	public class CherubimSpawner : IPart
	{
		// Token: 0x0600947F RID: 38015 RVA: 0x0035723D File Offset: 0x0035543D
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009480 RID: 38016 RVA: 0x00357240 File Offset: 0x00355440
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EnteredCell");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009481 RID: 38017 RVA: 0x00357255 File Offset: 0x00355455
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeObjectCreatedEvent.ID;
		}

		// Token: 0x06009482 RID: 38018 RVA: 0x0035726C File Offset: 0x0035546C
		public override bool HandleEvent(BeforeObjectCreatedEvent E)
		{
			string text = "";
			if (this.Period >= 4)
			{
				text = "Mechanical ";
			}
			HistoricEntitySnapshot sultan = HistoryAPI.GetSultanForPeriod(this.Period);
			string AStateName = "cherubim" + this.Period.ToString() + "Afaction";
			string AFaction = The.Game.RequireGameState<string>(AStateName, delegate
			{
				string text8 = null;
				if (sultan == null)
				{
					MetricsManager.LogError("no sultan found for period " + this.Period.ToString() + " generating " + AStateName);
				}
				else
				{
					List<string> list = sultan.GetList("likedFactions");
					if (list == null || list.Count == 0)
					{
						MetricsManager.LogError("no liked factions found for period " + this.Period.ToString() + " sultan generating " + AStateName);
					}
					else
					{
						IEnumerable<string> enumerable = list.Where<string>((string f) => CherubimSpawner.Factions.Contains(f));
						if (enumerable == null)
						{
							MetricsManager.LogError("no eligible factions found for period " + this.Period.ToString() + " sultan generating " + AStateName);
						}
						else
						{
							text8 = enumerable.GetRandomElement<string>(null);
						}
					}
				}
				if (text8 == null)
				{
					text8 = CherubimSpawner.Factions.GetRandomElement<string>(null);
				}
				return text8;
			});
			string BStateName = "cherubim:" + this.Period.ToString() + "Bfaction";
			Func<string, bool> <>9__5;
			string text2 = The.Game.RequireGameState<string>(BStateName, delegate
			{
				string text9 = null;
				if (sultan == null)
				{
					MetricsManager.LogError("no sultan found for period " + this.Period.ToString() + " generating " + BStateName);
				}
				else
				{
					List<string> list2 = sultan.GetList("likedFactions");
					if (list2 == null || list2.Count == 0)
					{
						MetricsManager.LogError("no liked factions found for period " + this.Period.ToString() + " sultan generating " + BStateName);
					}
					else
					{
						IEnumerable<string> enumerable2 = list2;
						Func<string, bool> func;
						if ((func = <>9__5) == null)
						{
							func = (<>9__5 = (string f) => CherubimSpawner.Factions.Contains(f) && f != AFaction);
						}
						IEnumerable<string> enumerable3 = enumerable2.Where<string>(func);
						if (enumerable3 == null)
						{
							MetricsManager.LogError("no eligible factions found for period " + this.Period.ToString() + " sultan generating " + BStateName);
						}
						else
						{
							text9 = enumerable3.GetRandomElement<string>(null);
						}
					}
				}
				if (text9 == null)
				{
					text9 = AFaction;
				}
				return text9;
			});
			string AElement = The.Game.RequireGameState<string>("cherubim" + this.Period.ToString() + "Aelement", delegate
			{
				HistoricEntitySnapshot sultan3 = sultan;
				string text10;
				if (sultan3 == null)
				{
					text10 = null;
				}
				else
				{
					List<string> list3 = sultan3.GetList("elements");
					text10 = ((list3 != null) ? list3.GetRandomElement<string>(null) : null);
				}
				return text10 ?? CherubimSpawner.Elements.GetRandomElement<string>(null);
			});
			Func<string, bool> <>9__6;
			string text3 = The.Game.RequireGameState<string>("cherubim:" + this.Period.ToString() + "Belement", delegate
			{
				HistoricEntitySnapshot sultan2 = sultan;
				string text11;
				if (sultan2 == null)
				{
					text11 = null;
				}
				else
				{
					List<string> list4 = sultan2.GetList("elements");
					if (list4 == null)
					{
						text11 = null;
					}
					else
					{
						Func<string, bool> func2;
						if ((func2 = <>9__6) == null)
						{
							func2 = (<>9__6 = (string e) => e != AElement);
						}
						IEnumerable<string> enumerable4 = list4.Where<string>(func2);
						text11 = ((enumerable4 != null) ? enumerable4.GetRandomElement<string>(null) : null);
					}
				}
				return text11 ?? AElement;
			});
			string text4 = ((this.Group == "A") ? AFaction : text2);
			string text5 = ((this.Group == "A") ? AElement : text3);
			GameObject gameObject = GameObject.Create(text + text4 + " Cherub");
			gameObject.SetStringProperty("SpawnedFrom", this.ParentObject.Blueprint, false);
			gameObject.Render.RenderString = "\u008f";
			if (text == "Mechanical ")
			{
				gameObject.Render.RenderString = "\u008e";
			}
			gameObject.Render.DetailColor = "W";
			if (text == "Mechanical ")
			{
				gameObject.Render.DisplayName = gameObject.Render.DisplayName.Replace("mechanical ", "");
			}
			if (text == "Mechanical ")
			{
				string text6 = Grammar.InitCap(Grammar.MakeAndList(gameObject.GetxTag("TextFragments", "PoeticFeatures", null).Split(',', StringSplitOptions.None).ToList<string>(), true));
				CherubimSpawner.ReplaceDescription(gameObject, CherubimSpawner.BaseMechanicalDescription, text6);
			}
			else
			{
				string text7 = "the " + string.Join(", the ", gameObject.GetxTag("TextFragments", "PoeticFeatures", null).Split(',', StringSplitOptions.None));
				CherubimSpawner.ReplaceDescription(gameObject, CherubimSpawner.BaseDescription, text7);
			}
			if (text == "Mechanical ")
			{
				gameObject.RequirePart<HeatSelfOnFreeze>(false).HeatAmount = "50";
				gameObject.RequirePart<ReflectProjectiles>(false).Chance = 50;
			}
			else
			{
				gameObject.RequirePart<HeatSelfOnFreeze>(false);
				gameObject.RequirePart<ReflectProjectiles>(false);
			}
			if (text == "Mechanical ")
			{
				gameObject.RequirePart<Robot>(false).EMPable = false;
				gameObject.RequirePart<MentalShield>(false);
				gameObject.RequirePart<Metal>(false);
				if (!gameObject.HasPart<NightVision>())
				{
					gameObject.GetPart<Mutations>().AddMutation(new DarkVision(), 12, true);
				}
				Corpse part = gameObject.GetPart<Corpse>();
				if (part != null)
				{
					part.CorpseChance = 0;
					part.BurntCorpseChance = 0;
					part.VaporizedCorpseChance = 0;
				}
				gameObject.IsOrganic = false;
				gameObject.SetStringProperty("SeveredLimbBlueprint", "RobotLimb", false);
				gameObject.SetStringProperty("SeveredHeadBlueprint", "RobotHead7", false);
				gameObject.SetStringProperty("SeveredFaceBlueprint", "RobotFace", false);
				gameObject.SetStringProperty("SeveredArmBlueprint", "RobotArm", false);
				gameObject.SetStringProperty("SeveredHandBlueprint", "RobotHand", false);
				gameObject.SetStringProperty("SeveredLegBlueprint", "RobotLeg", false);
				gameObject.SetStringProperty("SeveredFootBlueprint", "RobotFoot", false);
				gameObject.SetStringProperty("SeveredFeetBlueprint", "RobotFeet", false);
				gameObject.SetStringProperty("SeveredTailBlueprint", "RobotTail", false);
				gameObject.SetStringProperty("SeveredRootsBlueprint", "RobotRoots", false);
				gameObject.SetStringProperty("SeveredFinBlueprint", "RobotFin", false);
				gameObject.SetIntProperty("Bleeds", 1, false);
				gameObject.SetStringProperty("BleedLiquid", "oil-1000", false);
				gameObject.SetStringProperty("BleedColor", "&K", false);
				gameObject.SetStringProperty("BleedPrefix", "&Koily", false);
				Body body = gameObject.Body;
				if (body != null)
				{
					body.CategorizeAll(7);
				}
				gameObject.SetStringProperty("WaterRitualLiquid", "oil", false);
			}
			if (!this.bDynamic)
			{
				gameObject.Brain.Wanders = false;
				gameObject.Brain.WandersRandomly = false;
				gameObject.Brain.Allegiance.Clear();
				gameObject.Brain.Allegiance.Add("Cherubim", 100);
				gameObject.SetIntProperty("HelpModifier", 500, false);
				gameObject.SetIntProperty("CherubimLock", 1, false);
				gameObject.SetIntProperty("StaysOnZLevel", 1, false);
				gameObject.RequirePart<AIMarkOfDeathGuardian>(false);
			}
			CherubimSpawner.BestowElement(gameObject, text5, true);
			if (text == "Mechanical ")
			{
				gameObject.Render.DisplayName = "mechanical " + gameObject.Render.DisplayName;
			}
			E.ReplacementObject = gameObject;
			return base.HandleEvent(E);
		}

		// Token: 0x06009483 RID: 38019 RVA: 0x003577B0 File Offset: 0x003559B0
		public static void ReplaceDescription(GameObject Object, string Description, string Features)
		{
			string text = (Object.HasTag("AlternateCreatureType") ? Object.GetTag("AlternateCreatureType", null) : Object.Render.DisplayName.Substring(0, Object.Render.DisplayName.IndexOf(' ')));
			Object.GetPart<Description>()._Short = Description.Replace("*skin*", Object.GetxTag("TextFragments", "Skin", null)).Replace("*creatureType*", text).Replace("*features*", Features);
		}

		// Token: 0x06009484 RID: 38020 RVA: 0x0035783C File Offset: 0x00355A3C
		public static void BestowElement(GameObject Object, string Element, bool PrependName = true)
		{
			if (Element == "glass")
			{
				Object.AddPart<ReflectDamage>(true, false).ReflectPercentage = 25;
				Object.AddPart<ModGlazed>(true, false).Chance = 10;
				if (PrependName)
				{
					Object.Render.DisplayName = "glass " + Object.Render.DisplayName;
				}
				Object.Render.ColorString = "&K";
				Object.AddPart<RulesDescription>(true, false).Text = "\nThis creature belongs to the caste of glass cherubim.\n• Attacks have a 10% chance to dismember.\n• Reflects 25% damage back at attackers.";
				return;
			}
			if (Element == "jewels")
			{
				Object.Statistics["Ego"].BaseValue += 10;
				Object.AddPart<ModTransmuteOnHit>(true, false).ChancePerThousand = 50;
				if (PrependName)
				{
					Object.Render.DisplayName = "jeweled " + Object.Render.DisplayName;
				}
				Object.Render.ColorString = "&M";
				Object.AddPart<RulesDescription>(true, false).Text = "\nThis creature belongs to the caste of jeweled cherubim.\n• +10 Ego.\n• Attacks have a small chance to transmute opponents into gemstones.";
				return;
			}
			if (Element == "stars")
			{
				Object.RequirePart<Mutations>(false).AddMutation(new LightManipulation(), 10, true);
				if (PrependName)
				{
					Object.Render.DisplayName = "star " + Object.Render.DisplayName;
				}
				Object.Render.ColorString = "&Y";
				Object.AddPart<RulesDescription>(true, false).Text = "\nThis creature belongs to the caste of star cherubim.\n• Light Manipulation 10";
				return;
			}
			if (Element == "time")
			{
				Object.RequirePart<Mutations>(false).AddMutation(new TemporalFugue(), 10, true);
				if (PrependName)
				{
					Object.Render.DisplayName = "time " + Object.Render.DisplayName;
				}
				Object.Render.ColorString = "&b";
				Object.SetIntProperty("AIAbilityIgnoreCon", 1, false);
				Object.AddPart<RulesDescription>(true, false).Text = "\nThis creature belongs to the caste of time cherubim.\n• Temporal Fugue 10";
				return;
			}
			if (Element == "salt")
			{
				Object.Statistics["Willpower"].BaseValue += 10;
				Object.Statistics["Hitpoints"].BaseValue *= 2;
				if (PrependName)
				{
					Object.Render.DisplayName = "salt " + Object.Render.DisplayName;
				}
				Object.Render.ColorString = "&y";
				Object.AddPart<RulesDescription>(true, false).Text = "\nThis creature belongs to the caste of salt cherubim.\n• +10 Willpower\n• +100% HP";
				return;
			}
			if (Element == "ice")
			{
				Object.Statistics["ColdResistance"].BaseValue += 100;
				Object.RequirePart<Mutations>(false).AddMutation(new IceBreather(), 10, true);
				if (PrependName)
				{
					Object.Render.DisplayName = "ice " + Object.Render.DisplayName;
				}
				Object.Render.ColorString = "&C";
				Object.AddPart<RulesDescription>(true, false).Text = "\nThis creature belongs to the caste of ice cherubim.\n• +100 Cold Resist\n• Ice Breath 10";
				return;
			}
			if (Element == "scholarship")
			{
				Object.Statistics["Intelligence"].BaseValue += 10;
				ModBeetlehost modBeetlehost = Object.AddPart<ModBeetlehost>(true, false);
				modBeetlehost.Chance = 100;
				modBeetlehost.WorksOn(false, false, false, false, false, false, false, false, true, false);
				if (PrependName)
				{
					Object.Render.DisplayName = "learned " + Object.Render.DisplayName;
				}
				Object.Render.ColorString = "&B";
				Object.AddPart<RulesDescription>(true, false).Text = "\nThis creature belongs to the caste of learned cherubim.\n• +10 Intelligence\n• Attacks discharge clockwork beetles.";
				return;
			}
			if (Element == "might")
			{
				Object.Statistics["Strength"].BaseValue += 20;
				if (PrependName)
				{
					Object.Render.DisplayName = "mighty " + Object.Render.DisplayName;
				}
				Object.Render.ColorString = "&r";
				Object.AddPart<RulesDescription>(true, false).Text = "\nThis creature belongs to the caste of mighty cherubim.\n• +20 Strength";
				return;
			}
			if (Element == "chance")
			{
				ModBlinkEscape modBlinkEscape = Object.RequirePart<ModBlinkEscape>(false);
				modBlinkEscape.WorksOnEquipper = false;
				modBlinkEscape.WorksOnSelf = true;
				Object.RegisterPartEvent(modBlinkEscape, "BeforeApplyDamage");
				modBlinkEscape.Tier = 20;
				Object.AddPart<ModFatecaller>(true, false).Chance = 20;
				if (PrependName)
				{
					Object.Render.DisplayName = "chaotic " + Object.Render.DisplayName;
				}
				Object.Render.ColorString = "&m";
				Object.AddPart<RulesDescription>(true, false).Text = "\nThis creature belongs to the caste of chaotic cherubim.\n• Whenever this creature is about to take damage, there's a 25% chance they blink away instead.\n• Whenever this creature attacks, 50% of the time the Fates have their way.";
				return;
			}
			if (Element == "circuitry")
			{
				Object.Statistics["ElectricResistance"].BaseValue += 100;
				Object.RequirePart<Mutations>(false).AddMutation(new ElectricalGeneration(), 10, true);
				if (PrependName)
				{
					Object.Render.DisplayName = "electric " + Object.Render.DisplayName;
				}
				Object.Render.ColorString = "&W";
				Object.AddPart<RulesDescription>(true, false).Text = "\nThis creature belongs to the caste of electric cherubim.\n• +100 Electrical Resist\n• Electrical Generation 10";
				return;
			}
			if (Element == "travel")
			{
				Object.Statistics["Speed"].BaseValue += 5;
				Object.RequirePart<Mutations>(false).AddMutation(new Teleportation(), 10, true);
				if (PrependName)
				{
					Object.Render.DisplayName = "quickened " + Object.Render.DisplayName;
				}
				Object.Render.ColorString = "&g";
				Object.AddPart<RulesDescription>(true, false).Text = "\nThis creature belongs to the caste of quickened cherubim.\n• +5 Quickness\n• Teleportation 10";
			}
		}

		// Token: 0x04003D66 RID: 15718
		public static readonly string BaseDescription = "Gallium veins press against the underside of =pronouns.possessive= crystalline *skin* and gleam warmly. =pronouns.Possessive= body is perfect, and the whole of it is wet with amniotic slick; could =pronouns.subjective= have just now peeled =pronouns.reflexive= off an oil canvas? =verb:Were:afterpronoun= =pronouns.subjective= cast into the material realm by a dreaming, dripping brain? Whatever the embryo, =pronouns.subjective= =verb:are:afterpronoun= now the archetypal *creatureType*; it's all there in impeccable simulacrum: *features*. Perfection is realized.";

		// Token: 0x04003D67 RID: 15719
		public static readonly string BaseMechanicalDescription = "Dials tick and vacuum tubes mantle under synthetic *skin* and inside plastic joints. *features* are wrought from a vast and furcate machinery into the ideal form of the *creatureType*. By the artistry of =pronouns.possessive= construction, =pronouns.subjective= closely =verb:resemble:afterpronoun= =pronouns.possessive= referent, but an exposed cog here and an exhaust valve there betray the truth of =pronouns.possessive= nature. =pronouns.Possessive= movements are short and mimetic; =pronouns.subjective= =verb:inhabit:afterpronoun= the valley between the mountains of life and imagination.";

		// Token: 0x04003D68 RID: 15720
		public static readonly List<string> Factions = new List<string>
		{
			"Prey", "Baboons", "Apes", "Crabs", "Bears", "Winged Mammals", "Birds", "Fish", "Insects", "Swine",
			"Reptiles", "Cannibals", "Arachnids", "Cats", "Dogs", "Mollusks", "Tortoises", "Robots", "Baetyls", "Antelopes",
			"Worms", "Oozes", "Equines", "Newly Sentient Beings", "Hermits", "Frogs", "Strangers", "Flowers", "Roots", "Fungi",
			"Vines", "Urchins", "Succulents", "Trees"
		};

		// Token: 0x04003D69 RID: 15721
		public static readonly List<string> Elements = new List<string>
		{
			"glass", "jewels", "stars", "time", "salt", "ice", "scholarship", "might", "chance", "circuitry",
			"travel"
		};

		// Token: 0x04003D6A RID: 15722
		public string Group = "A";

		// Token: 0x04003D6B RID: 15723
		public int Period = 1;

		// Token: 0x04003D6C RID: 15724
		public bool bDynamic = true;
	}
}
