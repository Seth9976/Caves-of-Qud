using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HistoryKit;
using Newtonsoft.Json.Linq;
using Qud.API;
using XRL.Annals;
using XRL.Language;
using XRL.Names;
using XRL.Rules;
using XRL.UI;
using XRL.World.Capabilities;
using XRL.World.Parts;

namespace XRL.World
{
	/// <summary>
	/// Base game object
	/// </summary>
	// Token: 0x0200096C RID: 2412
	[HasModSensitiveStaticCache]
	public static class RelicGenerator
	{
		// Token: 0x060063A1 RID: 25505 RVA: 0x001E8D83 File Offset: 0x001E6F83
		public static void CheckInit()
		{
			if (RelicGenerator.Types.Count == 0)
			{
				RelicGenerator.Init();
			}
		}

		// Token: 0x060063A2 RID: 25506 RVA: 0x001E8D96 File Offset: 0x001E6F96
		[ModSensitiveCacheInit]
		public static void Init()
		{
			RelicGenerator.Types.Clear();
			RelicGenerator.TypeMap.Clear();
			Loading.LoadTask("Loading Relics.xml", delegate
			{
				foreach (XmlDataHelper xmlDataHelper in DataManager.YieldXMLStreamsWithRoot("relics", true, true))
				{
					xmlDataHelper.HandleNodes(RelicGenerator._outerNodes, null);
				}
			}, true);
		}

		// Token: 0x060063A3 RID: 25507 RVA: 0x001E8DD6 File Offset: 0x001E6FD6
		public static void HandleInnerNode(XmlDataHelper xml)
		{
			xml.HandleNodes(RelicGenerator._innerNodes, null);
		}

		// Token: 0x060063A4 RID: 25508 RVA: 0x001E8DE4 File Offset: 0x001E6FE4
		public static void HandleRelicTypeNode(XmlDataHelper Reader)
		{
			string attribute = Reader.GetAttribute("Name");
			if (attribute.IsNullOrEmpty())
			{
				throw new Exception(Reader.Name + " tag had missing or empty Name attribute");
			}
			if (!RelicGenerator.Types.Contains(attribute))
			{
				RelicGenerator.Types.Add(attribute);
			}
			Reader.DoneWithElement();
		}

		// Token: 0x060063A5 RID: 25509 RVA: 0x001E8E3C File Offset: 0x001E703C
		public static void HandleRemoveRelicTypeNode(XmlDataHelper Reader)
		{
			string attribute = Reader.GetAttribute("Name");
			if (attribute.IsNullOrEmpty())
			{
				throw new Exception(Reader.Name + " tag had missing or empty Name attribute");
			}
			RelicGenerator.Types.Remove(attribute);
			Reader.DoneWithElement();
		}

		// Token: 0x060063A6 RID: 25510 RVA: 0x001E8E88 File Offset: 0x001E7088
		public static void HandleRelicTypeMappingNode(XmlDataHelper Reader)
		{
			string attribute = Reader.GetAttribute("Name");
			if (attribute.IsNullOrEmpty())
			{
				throw new Exception(Reader.Name + " tag had missing or empty Name attribute");
			}
			string attribute2 = Reader.GetAttribute("Type");
			if (attribute2.IsNullOrEmpty())
			{
				throw new Exception(Reader.Name + " tag had missing or empty Type attribute");
			}
			if (!RelicGenerator.Types.Contains(attribute2))
			{
				throw new Exception(Reader.Name + " tag had unknown Type \"" + attribute2 + "\"");
			}
			RelicGenerator.TypeMap[attribute] = attribute2;
			Reader.DoneWithElement();
		}

		// Token: 0x060063A7 RID: 25511 RVA: 0x001E8F24 File Offset: 0x001E7124
		public static void HandleRemoveRelicTypeMappingNode(XmlDataHelper Reader)
		{
			string attribute = Reader.GetAttribute("Name");
			if (attribute.IsNullOrEmpty())
			{
				throw new Exception(Reader.Name + " tag had missing or empty Name attribute");
			}
			RelicGenerator.TypeMap.Remove(attribute);
			Reader.DoneWithElement();
		}

		// Token: 0x060063A8 RID: 25512 RVA: 0x001E8F6D File Offset: 0x001E716D
		public static List<string> GetTypes()
		{
			RelicGenerator.CheckInit();
			return RelicGenerator.Types;
		}

		// Token: 0x060063A9 RID: 25513 RVA: 0x001E8F7C File Offset: 0x001E717C
		public static string GenerateRelicName(string Type, HistoricEntitySnapshot SnapRegion, string Element, out string Article)
		{
			Article = null;
			if (SnapRegion != null)
			{
				return RelicGenerator.GenerateRelicNameByRegion(Type, SnapRegion, Element, out Article);
			}
			RelicGenerator.RelicNameContext["*element*"] = Element;
			RelicGenerator.RelicNameContext["*itemType*"] = Type;
			string text = HistoricStringExpander.ExpandString("<spice.history.relics.names.!random>", null, null, RelicGenerator.RelicNameContext, null);
			if (text.Contains("*personNounPossessive*"))
			{
				string text2 = "<spice.personNouns.!random>";
				string text3 = HistoricStringExpander.ExpandString(text2, null);
				if (text3 == text2)
				{
					text = text.Replace("*personNounPossessive*", "*creatureNamePossessive*");
				}
				else
				{
					text = text.Replace("*personNounPossessive*", Grammar.MakePossessive(text3));
				}
			}
			if (text.Contains("*creatureNamePossessive*"))
			{
				text = text.Replace("*creatureNamePossessive*", Grammar.MakePossessive(NameMaker.MakeName(EncountersAPI.GetACreature(null), null, null, null, null, null, null, null, null, null, "Relic", null, null, false, true, null, null)));
			}
			QudHistoryHelpers.ExtractArticle(ref text, out Article);
			return QudHistoryHelpers.Ansify(Grammar.MakeTitleCase(text), null);
		}

		// Token: 0x060063AA RID: 25514 RVA: 0x001E9078 File Offset: 0x001E7278
		public static string GenerateRelicNameByRegion(string Type, HistoricEntitySnapshot SnapRegion, string Element, out string Article)
		{
			History sultanHistory = The.Game.sultanHistory;
			string text = HistoricStringExpander.ExpandString(string.Concat(new string[]
			{
				"<spice.elements.",
				Element,
				".adjectives.!random> ",
				HistoricStringExpander.ExpandString("<spice.itemTypes." + Type + ".!random>", null, sultanHistory, null, null),
				" of ",
				SnapRegion.GetProperty("newName", "unknown")
			}), null, sultanHistory, null, null);
			Article = "the";
			return QudHistoryHelpers.Ansify(Grammar.MakeTitleCase(text), null);
		}

		// Token: 0x060063AB RID: 25515 RVA: 0x001E9104 File Offset: 0x001E7304
		public static GameObject GenerateSpindleNegotiationRelic(string ItemType, string SparedFaction, string BetrayedFaction, string PlayerNameAndAppositive, int Tier = 4)
		{
			List<string> list = new List<string> { HistoricStringExpander.ExpandString("<spice.elements.!random>", null) };
			Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>
			{
				{
					"likedFactions",
					new List<string> { SparedFaction }
				},
				{
					"hatedFactions",
					new List<string> { BetrayedFaction }
				}
			};
			return RelicGenerator.GenerateRelic(ItemType, Tier, null, list, dictionary, null, null, PlayerNameAndAppositive);
		}

		// Token: 0x060063AC RID: 25516 RVA: 0x001E916A File Offset: 0x001E736A
		public static GameObject GenerateRelic(int Tier, bool RandomName = false)
		{
			return RelicGenerator.GenerateRelic(The.Game.sultanHistory.entities.GetRandomElement<HistoricEntity>(null).GetCurrentSnapshot(), Tier, null, RandomName);
		}

		// Token: 0x060063AD RID: 25517 RVA: 0x001E9190 File Offset: 0x001E7390
		public static GameObject GenerateRelic(HistoricEntitySnapshot Snapshot, int Tier, string Type = null, bool RandomName = false)
		{
			List<string> list = new List<string>();
			list.AddRange(Snapshot.properties.Values);
			foreach (List<string> list2 in Snapshot.listProperties.Values)
			{
				list.AddRange(list2);
			}
			if (Type == null)
			{
				Type = Snapshot.GetProperty("itemType", "unknown");
			}
			return RelicGenerator.GenerateRelic(Type, Tier, Snapshot.GetProperty("type", "unknown").Equals("region") ? Snapshot : null, list, Snapshot.listProperties, RandomName ? null : Snapshot.Name, RandomName ? null : Snapshot.GetProperty("article", null), null);
		}

		// Token: 0x060063AE RID: 25518 RVA: 0x001E9264 File Offset: 0x001E7464
		public static GameObject GenerateRelic(HistoricEntitySnapshot Snapshot, string Type = null, bool RandomName = false)
		{
			List<string> list = new List<string>();
			list.AddRange(Snapshot.properties.Values);
			foreach (List<string> list2 in Snapshot.listProperties.Values)
			{
				list.AddRange(list2);
			}
			if (Type == null)
			{
				Type = Snapshot.GetProperty("itemType", "unknown");
			}
			int relicTierFromPeriod = RelicGenerator.GetRelicTierFromPeriod(int.Parse(Snapshot.GetProperty("period", "unknown")));
			return RelicGenerator.GenerateRelic(Type, relicTierFromPeriod, Snapshot.GetProperty("type", "unknown").Equals("region") ? Snapshot : null, list, Snapshot.listProperties, RandomName ? null : Snapshot.GetProperty("Name", null), RandomName ? null : Snapshot.GetProperty("article", null), null);
		}

		// Token: 0x060063AF RID: 25519 RVA: 0x001E9358 File Offset: 0x001E7558
		public static int GetRelicTierFromPeriod(int Period)
		{
			if (Period == 5)
			{
				if (!If.CoinFlip(null, null))
				{
					return 2;
				}
				return 1;
			}
			else if (Period == 4)
			{
				if (!If.CoinFlip(null, null))
				{
					return 4;
				}
				return 3;
			}
			else if (Period == 3)
			{
				if (!If.CoinFlip(null, null))
				{
					return 6;
				}
				return 5;
			}
			else
			{
				if (Period == 2)
				{
					return 7;
				}
				if (Period == 1)
				{
					return 8;
				}
				return 4;
			}
		}

		// Token: 0x060063B0 RID: 25520 RVA: 0x001E93A5 File Offset: 0x001E75A5
		public static int GetPeriodFromRelicTier(int Tier)
		{
			if (Tier == 0 || Tier == 1 || Tier == 2)
			{
				return 5;
			}
			if (Tier == 3 || Tier == 4)
			{
				return 4;
			}
			if (Tier == 5 || Tier == 6)
			{
				return 3;
			}
			if (Tier == 7)
			{
				return 2;
			}
			if (Tier == 8)
			{
				return 1;
			}
			return 4;
		}

		// Token: 0x060063B1 RID: 25521 RVA: 0x001E93D5 File Offset: 0x001E75D5
		public static int GetAttributeBonusFromRelicTier(int Tier)
		{
			if (Tier <= 4)
			{
				if (!If.CoinFlip(null, null))
				{
					return 2;
				}
				return 1;
			}
			else
			{
				if (!If.CoinFlip(null, null))
				{
					return 4;
				}
				return 3;
			}
		}

		// Token: 0x060063B2 RID: 25522 RVA: 0x001E93F4 File Offset: 0x001E75F4
		public static int GetResistanceBonusFromRelicTier(int Tier)
		{
			int num = Stat.Random(20, 30);
			if (Tier >= 4)
			{
				num += (Tier - 4) * 20;
			}
			return num;
		}

		// Token: 0x060063B3 RID: 25523 RVA: 0x001E9419 File Offset: 0x001E7619
		public static int GetMoveSpeedBonusFromRelicTier(int Tier)
		{
			return -(8 + (Tier - 1) * 3 + Stat.Random(1, 4));
		}

		// Token: 0x060063B4 RID: 25524 RVA: 0x001E942C File Offset: 0x001E762C
		public static string TranslateAdjective(string Input)
		{
			Input = Input.ToLower();
			if (Input == "none")
			{
				return Input;
			}
			foreach (JProperty jproperty in ((JObject)HistoricSpice.root["elements"]).Properties())
			{
				if (Input.EqualsNoCase(jproperty.Name))
				{
					return Input;
				}
				foreach (JToken jtoken in jproperty.Value["adjectives"].Children())
				{
					if (Input.EqualsNoCase((string)jtoken))
					{
						return jproperty.Name.ToLower();
					}
				}
			}
			return null;
		}

		// Token: 0x060063B5 RID: 25525 RVA: 0x001E9518 File Offset: 0x001E7718
		public static bool GiveRandomMeleeWeaponBestowal(GameObject Object, int Tier = 1, bool Standard = false, bool ShowInShortDescription = false)
		{
			MeleeWeapon part = Object.GetPart<MeleeWeapon>();
			if (part == null)
			{
				return false;
			}
			int num = Stat.Random(0, 4);
			if (num == 0)
			{
				if (part.AdjustDamageDieSize(2))
				{
					if (ShowInShortDescription)
					{
						Object.RequirePart<NoteImprovedDamageDieSize>(false).Amount += 2;
					}
					return true;
				}
			}
			else if (num == 1)
			{
				if (part.AdjustDamage(1))
				{
					if (ShowInShortDescription)
					{
						Object.RequirePart<NoteImprovedDamageRoll>(false).Amount++;
					}
					return true;
				}
			}
			else if (num == 2)
			{
				if (part.AdjustBonusCap(3))
				{
					if (ShowInShortDescription)
					{
						Object.RequirePart<NoteImprovedBonusCap>(false).Amount += 3;
					}
					return true;
				}
			}
			else if (num == 3)
			{
				part.HitBonus += 2;
				return true;
			}
			part.PenBonus++;
			if (ShowInShortDescription)
			{
				Object.RequirePart<NoteImprovedPenetrationBonus>(false).Amount++;
			}
			return true;
		}

		// Token: 0x060063B6 RID: 25526 RVA: 0x001E95E4 File Offset: 0x001E77E4
		public static bool GiveRandomMissileWeaponBestowal(GameObject Object, int Tier = 1, bool Standard = false, bool ShowInShortDescription = false)
		{
			MissileWeapon part = Object.GetPart<MissileWeapon>();
			if (part == null)
			{
				return false;
			}
			int num = Stat.Random(0, 7);
			if (num == 0 && !part.NoWildfire)
			{
				part.NoWildfire = true;
				return true;
			}
			if (num == 1)
			{
				NoteImprovedRateOfFire noteImprovedRateOfFire = (ShowInShortDescription ? Object.RequirePart<NoteImprovedRateOfFire>(false) : null);
				if (part.AmmoPerAction == part.ShotsPerAction)
				{
					part.AmmoPerAction++;
					if (noteImprovedRateOfFire != null)
					{
						noteImprovedRateOfFire.AmmoAmount++;
					}
				}
				part.ShotsPerAction++;
				if (noteImprovedRateOfFire != null)
				{
					noteImprovedRateOfFire.ShotsAmount++;
				}
				MagazineAmmoLoader part2 = Object.GetPart<MagazineAmmoLoader>();
				if (part2 != null && part2.MaxAmmo < part.AmmoPerAction)
				{
					if (noteImprovedRateOfFire != null)
					{
						noteImprovedRateOfFire.AmmoCapacityAmount += part.AmmoPerAction - part2.MaxAmmo;
					}
					part2.MaxAmmo = part.AmmoPerAction;
				}
				return true;
			}
			if (num == 2)
			{
				Object.RequirePart<MissilePerformance>(false).PenetrationModifier++;
				if (ShowInShortDescription)
				{
					Object.RequirePart<MissilePerformance>(false).ShowInShortDescription = true;
				}
				return true;
			}
			if (num == 3)
			{
				Object.RequirePart<MissilePerformance>(false).DamageDieModifier += 2;
				if (ShowInShortDescription)
				{
					Object.RequirePart<MissilePerformance>(false).ShowInShortDescription = true;
				}
				return true;
			}
			if (num == 4)
			{
				Object.RequirePart<MissilePerformance>(false).DamageModifier++;
				if (ShowInShortDescription)
				{
					Object.RequirePart<MissilePerformance>(false).ShowInShortDescription = true;
				}
				return true;
			}
			if (num == 5)
			{
				Object.RequirePart<MissilePerformance>(false).PenetrateCreatures = new bool?(true);
				if (ShowInShortDescription)
				{
					Object.RequirePart<MissilePerformance>(false).ShowInShortDescription = true;
				}
				return true;
			}
			if (num == 6 && !MissileWeapon.IsVorpal(Object))
			{
				Object.RequirePart<MissilePerformance>(false).WantAddAttribute("Vorpal");
				if (ShowInShortDescription)
				{
					Object.RequirePart<MissilePerformance>(false).ShowInShortDescription = true;
				}
				return true;
			}
			if (part.WeaponAccuracy > 0)
			{
				part.WeaponAccuracy = Math.Max(part.WeaponAccuracy - Stat.Random(5, 10), 0);
			}
			else
			{
				part.AimVarianceBonus += Stat.Random(2, 6);
			}
			if (ShowInShortDescription)
			{
				Object.RequirePart<NoteImprovedAccuracy>(false);
			}
			return true;
		}

		// Token: 0x060063B7 RID: 25527 RVA: 0x001E97DC File Offset: 0x001E79DC
		public static bool GiveRandomArmorBestowal(GameObject Object, int Tier = 1, bool Standard = false, bool ShowInShortDescription = false)
		{
			Armor part = Object.GetPart<Armor>();
			if (part == null)
			{
				return false;
			}
			int num = Stat.Random(0, 14);
			if (num == 0)
			{
				Armor armor = part;
				int num2 = armor.DV;
				armor.DV = num2 + 1;
				if (ShowInShortDescription)
				{
					Object.RequirePart<NoteImprovedDodgeValue>(false).Amount++;
				}
			}
			else if (num == 1)
			{
				Armor armor2 = part;
				int num2 = armor2.AV;
				armor2.AV = num2 + 1;
				if (ShowInShortDescription)
				{
					Object.RequirePart<NoteImprovedArmorValue>(false).Amount++;
				}
			}
			else if (num == 2)
			{
				part.MA += 2;
			}
			else if (num == 3)
			{
				part.Acid += 10;
			}
			else if (num == 4)
			{
				part.Cold += 10;
			}
			else if (num == 5)
			{
				part.Heat += 10;
			}
			else if (num == 6)
			{
				part.Elec += 10;
			}
			else if (num == 7)
			{
				part.Strength++;
			}
			else if (num == 8)
			{
				part.Agility++;
			}
			else if (num == 9)
			{
				part.Toughness++;
			}
			else if (num == 10)
			{
				part.Intelligence++;
			}
			else if (num == 11)
			{
				part.Willpower++;
			}
			else if (num == 12)
			{
				part.Ego++;
			}
			else if (num == 13)
			{
				part.ToHit += 2;
			}
			else if (num == 14)
			{
				if (part.SpeedPenalty > 0)
				{
					int num3 = Math.Max(part.SpeedPenalty - Stat.Random(5, 10), 0);
					if (ShowInShortDescription)
					{
						Object.RequirePart<NoteImprovedArmorValue>(false).Amount += num3 - part.SpeedPenalty;
					}
					part.SpeedPenalty = num3;
				}
				else
				{
					part.SpeedBonus += Stat.Random(1, 5);
				}
			}
			return true;
		}

		// Token: 0x060063B8 RID: 25528 RVA: 0x001E99DC File Offset: 0x001E7BDC
		public static bool GiveRandomShieldBestowal(GameObject Object, int Tier = 1, bool Standard = false, bool ShowInShortDescription = false)
		{
			Shield part = Object.GetPart<Shield>();
			if (part == null)
			{
				return false;
			}
			int num = (Standard ? 0 : Stat.Random(0, 3));
			if (num == 0)
			{
				if (!Object.HasPart<ModImprovedBlock>())
				{
					Object.AddPart<ModImprovedBlock>(new ModImprovedBlock(Tier), true, false);
					return true;
				}
				if (Standard)
				{
					if (part.AV < 1)
					{
						if (ShowInShortDescription)
						{
							Object.RequirePart<NoteImprovedArmorValue>(false).Amount += 1 - part.AV;
						}
						part.AV = 1;
					}
					return true;
				}
			}
			else if (num == 1)
			{
				if (part.DV < 0)
				{
					part.DV++;
					if (ShowInShortDescription)
					{
						Object.RequirePart<NoteImprovedDodgeValue>(false).Amount++;
					}
					if (part.DV < 0 && 50.in100())
					{
						part.DV++;
						if (ShowInShortDescription)
						{
							Object.RequirePart<NoteImprovedDodgeValue>(false).Amount++;
						}
					}
					return true;
				}
			}
			else if (num == 2 && part.SpeedPenalty > 0)
			{
				int num2 = Math.Max(part.SpeedPenalty - Stat.Random(5, 10), 0);
				if (ShowInShortDescription)
				{
					Object.RequirePart<NoteImprovedArmorValue>(false).Amount += num2 - part.SpeedPenalty;
				}
				part.SpeedPenalty = num2;
				return true;
			}
			part.AV++;
			if (ShowInShortDescription)
			{
				Object.RequirePart<NoteImprovedArmorValue>(false).Amount++;
			}
			return true;
		}

		// Token: 0x060063B9 RID: 25529 RVA: 0x001E9B34 File Offset: 0x001E7D34
		public static bool GiveRandomImplantBestowal(GameObject Object, int Tier = 1, bool Standard = false, bool ShowInShortDescription = false)
		{
			Object.ModIntProperty("CyberneticRejectionSyndromeModifier", -Stat.Random(1, Tier * 2), true);
			CyberneticsBaseItem part = Object.GetPart<CyberneticsBaseItem>();
			if (part == null)
			{
				return false;
			}
			int num = Stat.Random(0, 5);
			if (num == 0 && part.Cost > 1)
			{
				NoteReducedLicensePoints noteReducedLicensePoints = (ShowInShortDescription ? Object.RequirePart<NoteReducedLicensePoints>(false) : null);
				part.Cost--;
				if (noteReducedLicensePoints != null)
				{
					noteReducedLicensePoints.Amount++;
				}
				if (part.Cost > 2 && Tier >= 4)
				{
					part.Cost--;
					if (noteReducedLicensePoints != null)
					{
						noteReducedLicensePoints.Amount++;
					}
					if (part.Cost > 3 && Tier >= 8)
					{
						part.Cost--;
						if (noteReducedLicensePoints != null)
						{
							noteReducedLicensePoints.Amount++;
						}
					}
				}
				return true;
			}
			if (num <= 1 && !part.Slots.HasDelimitedSubstring(',', "Hand", StringComparison.Ordinal) && (part.Slots.HasDelimitedSubstring(',', "Hands", StringComparison.Ordinal) || part.Slots.HasDelimitedSubstring(',', "Arm", StringComparison.Ordinal)) && !Object.HasTag("CyberneticsModifiesAnatomy") && !Object.HasTag("CyberneticsUsesEqSlot"))
			{
				CyberneticsBaseItem cyberneticsBaseItem = part;
				cyberneticsBaseItem.Slots += ",Hand";
				if (ShowInShortDescription)
				{
					Object.RequirePart<NoteExpandedSlots>(false).AddSlot("Hand");
				}
				return true;
			}
			if (num <= 1 && !part.Slots.HasDelimitedSubstring(',', "Arm", StringComparison.Ordinal) && (part.Slots.HasDelimitedSubstring(',', "Body", StringComparison.Ordinal) || part.Slots.HasDelimitedSubstring(',', "Back", StringComparison.Ordinal) || part.Slots.HasDelimitedSubstring(',', "Head", StringComparison.Ordinal) || part.Slots.HasDelimitedSubstring(',', "Face", StringComparison.Ordinal)) && !Object.HasTag("CyberneticsModifiesAnatomy") && !Object.HasTag("CyberneticsUsesEqSlot"))
			{
				CyberneticsBaseItem cyberneticsBaseItem2 = part;
				cyberneticsBaseItem2.Slots += ",Arm";
				if (ShowInShortDescription)
				{
					Object.RequirePart<NoteExpandedSlots>(false).AddSlot("Arm");
				}
				return true;
			}
			if (num <= 1 && !part.Slots.HasDelimitedSubstring(',', "Face", StringComparison.Ordinal) && (part.Slots.HasDelimitedSubstring(',', "Head", StringComparison.Ordinal) || part.Slots.HasDelimitedSubstring(',', "Arm", StringComparison.Ordinal) || part.Slots.HasDelimitedSubstring(',', "Back", StringComparison.Ordinal)) && !Object.HasTag("CyberneticsModifiesAnatomy") && !Object.HasTag("CyberneticsUsesEqSlot"))
			{
				CyberneticsBaseItem cyberneticsBaseItem3 = part;
				cyberneticsBaseItem3.Slots += ",Face";
				if (ShowInShortDescription)
				{
					Object.RequirePart<NoteExpandedSlots>(false).AddSlot("Face");
				}
				return true;
			}
			if (num <= 1 && !part.Slots.HasDelimitedSubstring(',', "Tail", StringComparison.Ordinal) && (part.Slots.HasDelimitedSubstring(',', "Hands", StringComparison.Ordinal) || part.Slots.HasDelimitedSubstring(',', "Hand", StringComparison.Ordinal) || part.Slots.HasDelimitedSubstring(',', "Arm", StringComparison.Ordinal) || part.Slots.HasDelimitedSubstring(',', "Back", StringComparison.Ordinal)) && !Object.HasTag("CyberneticsModifiesAnatomy") && !Object.HasTag("CyberneticsUsesEqSlot"))
			{
				CyberneticsBaseItem cyberneticsBaseItem4 = part;
				cyberneticsBaseItem4.Slots += ",Tail";
				if (ShowInShortDescription)
				{
					Object.RequirePart<NoteExpandedSlots>(false).AddSlot("Tail");
				}
				return true;
			}
			if (num <= 2 && !Object.WantEvent(SingletonEvent<GetAvailableComputePowerEvent>.ID, GetAvailableComputePowerEvent.CascadeLevel))
			{
				ComputeNode computeNode = new ComputeNode();
				computeNode.ChargeUse = 0;
				computeNode.IsPowerLoadSensitive = false;
				computeNode.WorksOn(false, false, false, false, false, false, true, false, false, false);
				computeNode.Power = Stat.Random(2, (Tier + 2) * 2);
				Object.AddPart<ComputeNode>(computeNode, true, false);
				RelicGenerator.SyncImplantPart(Object, computeNode);
				return true;
			}
			if (num <= 3 && RelicGenerator.AddImplantAttributeBoost(Object, Statistic.Attributes.GetRandomElement<string>(null), Tier, true))
			{
				return true;
			}
			if (num <= 4 && !Object.HasPart<CyberneticsSingleSkillsoft>() && (part.Slots.HasDelimitedSubstring(',', "Head", StringComparison.Ordinal) || part.Slots.HasDelimitedSubstring(',', "Face", StringComparison.Ordinal) || part.Slots.HasDelimitedSubstring(',', "Arm", StringComparison.Ordinal) || part.Slots.HasDelimitedSubstring(',', "Back", StringComparison.Ordinal) || part.Slots.HasDelimitedSubstring(',', "Body", StringComparison.Ordinal)))
			{
				CyberneticsSingleSkillsoft cyberneticsSingleSkillsoft = new CyberneticsSingleSkillsoft();
				cyberneticsSingleSkillsoft.AddOn = true;
				Object.AddPart<CyberneticsSingleSkillsoft>(cyberneticsSingleSkillsoft, true, false);
				RelicGenerator.SyncImplantPart(Object, cyberneticsSingleSkillsoft);
				return true;
			}
			if (Object.Weight > 0)
			{
				int weight = Object.Physics._Weight;
				if (weight > 0)
				{
					int i = 0;
					int num2 = 1 + Tier / 4;
					while (i < num2)
					{
						if (Object.Physics._Weight >= 8)
						{
							Object.Physics._Weight /= 2;
						}
						else if (Object.Physics._Weight > 0)
						{
							Object.Physics._Weight--;
						}
						i++;
					}
					if (ShowInShortDescription)
					{
						Object.RequirePart<NoteReducedWeight>(false).Amount += weight - Object.Physics.Weight;
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x060063BA RID: 25530 RVA: 0x001EA068 File Offset: 0x001E8268
		private static GameObject GenerateBaseRelic(ref string Type, int Tier, bool AllowAlternate = false)
		{
			RelicGenerator.CheckInit();
			bool flag = 20.in100();
			if (Type.IsNullOrEmpty() || Type == "unknown")
			{
				Type = RelicGenerator.Types.GetRandomElement<string>(null);
			}
			else if (!RelicGenerator.Types.Contains(Type))
			{
				string text;
				if (RelicGenerator.TypeMap.TryGetValue(Type, out text))
				{
					Type = text;
				}
				else if (Type.Any<char>(new Func<char, bool>(char.IsUpper)))
				{
					string text2 = Type.ToLower();
					string text3;
					if (RelicGenerator.TypeMap.TryGetValue(text2, out text3))
					{
						Type = text3;
					}
				}
			}
			if (Type == "Curio")
			{
				Type = "Artifact";
			}
			Tier.Constrain(ref Tier);
			string text4 = null;
			string text5 = "BaseRelic_" + Type;
			string text6 = text5 + Tier.ToString();
			if (flag)
			{
				string text7 = text6 + "th";
				if (PopulationManager.TableExists(text7))
				{
					PopulationResult populationResult = PopulationManager.RollOneFrom(text7, null, null);
					if (populationResult != null)
					{
						text4 = populationResult.Blueprint;
					}
				}
			}
			if (text4 == null && PopulationManager.TableExists(text6))
			{
				PopulationResult populationResult2 = PopulationManager.RollOneFrom(text6, null, null);
				if (populationResult2 != null)
				{
					text4 = populationResult2.Blueprint;
				}
			}
			if (text4 == null && PopulationManager.TableExists(text5))
			{
				PopulationResult populationResult3 = PopulationManager.RollOneFrom(text5, null, null);
				if (populationResult3 != null)
				{
					text4 = populationResult3.Blueprint;
				}
			}
			bool flag2 = false;
			if (text4 == null)
			{
				int num = Tier;
				List<string> list = new List<string>(8);
				string text8 = "BaseTier" + Type + num.ToString();
				foreach (GameObjectBlueprint gameObjectBlueprint in GameObjectFactory.Factory.BlueprintList)
				{
					if (gameObjectBlueprint.Name.StartsWith(text8))
					{
						list.Add(gameObjectBlueprint.Name);
					}
				}
				if (list.Count > 0)
				{
					text4 = list.GetRandomElement<string>(null);
					flag2 = true;
				}
			}
			if (text4 == null)
			{
				MetricsManager.LogError("Unknown relic type: " + Type + ", tier: " + Tier.ToString());
				Type = null;
				return RelicGenerator.GenerateBaseRelic(ref Type, Tier, AllowAlternate);
			}
			GameObject gameObject = GameObject.CreateUnmodified(text4);
			if (!flag2)
			{
				gameObject.SetStringProperty("Mods", "None", false);
			}
			gameObject.SetImportant(true, false, false);
			return gameObject;
		}

		// Token: 0x060063BB RID: 25531 RVA: 0x001EA29C File Offset: 0x001E849C
		public static string GetType(GameObject Object)
		{
			string inventoryCategory = Object.GetInventoryCategory(false);
			if (inventoryCategory == "Cybernetic Implants")
			{
				return "Implant";
			}
			if (inventoryCategory == "Food")
			{
				return "Food";
			}
			if (inventoryCategory == "Books")
			{
				return "Book";
			}
			Armor part = Object.GetPart<Armor>();
			if (part != null)
			{
				string wornOn = part.WornOn;
				if (wornOn == "Head" || wornOn == "Face" || wornOn == "Arm" || wornOn == "Feet" || wornOn == "Hands")
				{
					return wornOn;
				}
				if (wornOn == "Floating Nearby")
				{
					return "Floating";
				}
				return "Body";
			}
			else
			{
				if (inventoryCategory == "Shields")
				{
					return "Shield";
				}
				MissileWeapon part2 = Object.GetPart<MissileWeapon>();
				if (part2 != null)
				{
					string skill = part2.Skill;
					if (skill == "Rifle" || skill == "Bow")
					{
						return "Rifle";
					}
					if (skill == "Pistol")
					{
						return "Pistol";
					}
				}
				MeleeWeapon part3 = Object.GetPart<MeleeWeapon>();
				if (part3 != null && !part3.IsImprovisedWeapon())
				{
					string skill2 = part3.Skill;
					if (skill2 == "Axe")
					{
						return "Axe";
					}
					if (skill2 == "ShortBlades")
					{
						return "ShortBlade";
					}
					if (skill2 == "LongBlades")
					{
						return "LongBlade";
					}
					if (skill2 == "Cudgel")
					{
						return "Cudgel";
					}
				}
				if (!60.in100())
				{
					return "Curio";
				}
				return "Artifact";
			}
		}

		// Token: 0x060063BC RID: 25532 RVA: 0x001EA438 File Offset: 0x001E8638
		public static string GetSubtype(string type)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(type);
			if (num <= 2600161877U)
			{
				if (num <= 996264821U)
				{
					if (num <= 678699352U)
					{
						if (num != 535110145U)
						{
							if (num != 678699352U)
							{
								goto IL_02CA;
							}
							if (!(type == "Book"))
							{
								goto IL_02CA;
							}
							return "book";
						}
						else
						{
							if (!(type == "Curio"))
							{
								goto IL_02CA;
							}
							goto IL_02B8;
						}
					}
					else if (num != 731742070U)
					{
						if (num != 996264821U)
						{
							goto IL_02CA;
						}
						if (!(type == "Feet"))
						{
							goto IL_02CA;
						}
						goto IL_02A0;
					}
					else
					{
						if (!(type == "Pistol"))
						{
							goto IL_02CA;
						}
						goto IL_02A6;
					}
				}
				else if (num <= 1951447125U)
				{
					if (num != 1116344469U)
					{
						if (num != 1951447125U)
						{
							goto IL_02CA;
						}
						if (!(type == "Cudgel"))
						{
							goto IL_02CA;
						}
					}
					else
					{
						if (!(type == "Body"))
						{
							goto IL_02CA;
						}
						goto IL_02A0;
					}
				}
				else if (num != 2039097040U)
				{
					if (num != 2484219132U)
					{
						if (num != 2600161877U)
						{
							goto IL_02CA;
						}
						if (!(type == "Arm"))
						{
							goto IL_02CA;
						}
						goto IL_02A0;
					}
					else
					{
						if (!(type == "Face"))
						{
							goto IL_02CA;
						}
						goto IL_02A0;
					}
				}
				else
				{
					if (!(type == "Shield"))
					{
						goto IL_02CA;
					}
					return "shield";
				}
			}
			else if (num <= 2996251363U)
			{
				if (num <= 2804011457U)
				{
					if (num != 2665595067U)
					{
						if (num != 2804011457U)
						{
							goto IL_02CA;
						}
						if (!(type == "ShortBlade"))
						{
							goto IL_02CA;
						}
					}
					else if (!(type == "Axe"))
					{
						goto IL_02CA;
					}
				}
				else if (num != 2849239369U)
				{
					if (num != 2977241873U)
					{
						if (num != 2996251363U)
						{
							goto IL_02CA;
						}
						if (!(type == "Head"))
						{
							goto IL_02CA;
						}
						goto IL_02A0;
					}
					else
					{
						if (!(type == "Rifle"))
						{
							goto IL_02CA;
						}
						goto IL_02A6;
					}
				}
				else
				{
					if (!(type == "Hands"))
					{
						goto IL_02CA;
					}
					goto IL_02A0;
				}
			}
			else if (num <= 3463768387U)
			{
				if (num != 3179069417U)
				{
					if (num != 3463768387U)
					{
						goto IL_02CA;
					}
					if (!(type == "Artifact"))
					{
						goto IL_02CA;
					}
					goto IL_02B8;
				}
				else
				{
					if (!(type == "Food"))
					{
						goto IL_02CA;
					}
					return "food";
				}
			}
			else if (num != 3645406427U)
			{
				if (num != 4123813584U)
				{
					if (num != 4150303695U)
					{
						goto IL_02CA;
					}
					if (!(type == "LongBlade"))
					{
						goto IL_02CA;
					}
				}
				else
				{
					if (!(type == "Implant"))
					{
						goto IL_02CA;
					}
					return "implant";
				}
			}
			else
			{
				if (!(type == "Floating"))
				{
					goto IL_02CA;
				}
				goto IL_02A0;
			}
			return "weapon";
			IL_02A0:
			return "armor";
			IL_02A6:
			return "ranged";
			IL_02B8:
			return "curio";
			IL_02CA:
			return "curio";
		}

		// Token: 0x060063BD RID: 25533 RVA: 0x001EA714 File Offset: 0x001E8914
		public static string SelectElement(GameObject Item, GameObject Owner = null, GameObject Killed = null, GameObject InfluencedBy = null)
		{
			GetItemElementsEvent getItemElementsEvent = PooledEvent<GetItemElementsEvent>.FromPool();
			getItemElementsEvent.HandleFor(Owner);
			getItemElementsEvent.HandleFor(Item);
			getItemElementsEvent.HandleFor(Killed);
			getItemElementsEvent.HandleFor(InfluencedBy);
			Dictionary<string, int> weights = getItemElementsEvent.Weights;
			string text = HistoricStringExpander.ExpandString("<spice.elements.!random>", null) ?? "might";
			if (weights == null)
			{
				return text;
			}
			if (weights.ContainsKey(text))
			{
				Dictionary<string, int> dictionary = weights;
				string text2 = text;
				int num = dictionary[text2];
				dictionary[text2] = num + 1;
			}
			else
			{
				weights.Add(text, 1);
			}
			string randomElement = weights.GetRandomElement<string>(null);
			PooledEvent<GetItemElementsEvent>.ResetTo(ref getItemElementsEvent);
			return randomElement;
		}

		// Token: 0x060063BE RID: 25534 RVA: 0x001EA7A0 File Offset: 0x001E89A0
		private static void AddAttributeBoost(GameObject Object, string Attribute, int Tier, ref bool result)
		{
			EquipStatBoost.AppendBoostOnEquip(Object, Attribute + ":" + RelicGenerator.GetAttributeBonusFromRelicTier(Tier).ToString(), Attribute + "Boost", true);
			result = true;
		}

		// Token: 0x060063BF RID: 25535 RVA: 0x001EA7DC File Offset: 0x001E89DC
		private static void AddResistanceBoost(GameObject Object, string Resist, int Tier, ref bool result)
		{
			EquipStatBoost.AppendBoostOnEquip(Object, Resist + ":" + RelicGenerator.GetResistanceBonusFromRelicTier(Tier).ToString(), Resist + "System", true);
			result = true;
		}

		// Token: 0x060063C0 RID: 25536 RVA: 0x001EA818 File Offset: 0x001E8A18
		private static void AddMoveSpeedBoost(GameObject Object, int Tier, ref bool result)
		{
			EquipStatBoost.AppendBoostOnEquip(Object, "MoveSpeed:" + RelicGenerator.GetMoveSpeedBonusFromRelicTier(Tier).ToString(), "KineticDriver", true);
			result = true;
		}

		// Token: 0x060063C1 RID: 25537 RVA: 0x001EA84C File Offset: 0x001E8A4C
		private static void SyncImplantPart(GameObject Object, IPart Part)
		{
			GameObject implantee = Object.Implantee;
			if (implantee != null)
			{
				ImplantedEvent.Send(implantee, Object, Part, implantee.FindCybernetics(Object), null, false, false);
			}
		}

		// Token: 0x060063C2 RID: 25538 RVA: 0x001EA878 File Offset: 0x001E8A78
		private static bool AddImplantAttributeBoost(GameObject Object, string Attribute, int Tier, bool Minor = false)
		{
			if (Object.HasPart<CyberneticsStatModifier>())
			{
				return false;
			}
			CyberneticsStatModifier cyberneticsStatModifier = new CyberneticsStatModifier();
			cyberneticsStatModifier.AddOn = true;
			cyberneticsStatModifier.Stats = Attribute + ":" + (Minor ? 1 : RelicGenerator.GetAttributeBonusFromRelicTier(Tier)).ToString();
			Object.AddPart<CyberneticsStatModifier>(cyberneticsStatModifier, true, false);
			RelicGenerator.SyncImplantPart(Object, cyberneticsStatModifier);
			return true;
		}

		// Token: 0x060063C3 RID: 25539 RVA: 0x001EA8D4 File Offset: 0x001E8AD4
		private static bool AddImplantResistanceBoost(GameObject Object, string Resistance, int Tier, bool Minor = false)
		{
			if (Object.HasPart<CyberneticsStatModifier>())
			{
				return false;
			}
			CyberneticsStatModifier cyberneticsStatModifier = new CyberneticsStatModifier();
			cyberneticsStatModifier.AddOn = true;
			cyberneticsStatModifier.Stats = Resistance + ":" + (Minor ? Stat.Random(4, 6) : RelicGenerator.GetResistanceBonusFromRelicTier(Tier)).ToString();
			Object.AddPart<CyberneticsStatModifier>(cyberneticsStatModifier, true, false);
			RelicGenerator.SyncImplantPart(Object, cyberneticsStatModifier);
			return true;
		}

		// Token: 0x060063C4 RID: 25540 RVA: 0x001EA938 File Offset: 0x001E8B38
		private static bool AddImplantMoveSpeedBoost(GameObject Object, int Tier, bool Minor = false)
		{
			if (Object.HasPart<CyberneticsStatModifier>())
			{
				return false;
			}
			CyberneticsStatModifier cyberneticsStatModifier = new CyberneticsStatModifier();
			cyberneticsStatModifier.AddOn = true;
			cyberneticsStatModifier.Stats = "MoveSpeed:" + (Minor ? (-Stat.Random(2, 4)) : RelicGenerator.GetMoveSpeedBonusFromRelicTier(Tier)).ToString();
			Object.AddPart<CyberneticsStatModifier>(cyberneticsStatModifier, true, false);
			RelicGenerator.SyncImplantPart(Object, cyberneticsStatModifier);
			return true;
		}

		// Token: 0x060063C5 RID: 25541 RVA: 0x001EA99C File Offset: 0x001E8B9C
		public static bool ApplyBasicBestowal(GameObject Object, string Type = null, int Tier = 1, string Subtype = null, bool Standard = false, bool ShowInShortDescription = false)
		{
			if (Type == null)
			{
				Type = RelicGenerator.GetType(Object);
			}
			if (Subtype == null)
			{
				Subtype = RelicGenerator.GetSubtype(Type);
			}
			bool flag = false;
			if (Subtype == "ranged")
			{
				flag = RelicGenerator.GiveRandomMissileWeaponBestowal(Object, Tier, Standard, ShowInShortDescription);
			}
			else if (Subtype == "weapon")
			{
				flag = RelicGenerator.GiveRandomMeleeWeaponBestowal(Object, Tier, Standard, ShowInShortDescription);
			}
			else if (Subtype == "armor")
			{
				flag = RelicGenerator.GiveRandomArmorBestowal(Object, Tier, Standard, ShowInShortDescription);
			}
			else if (Subtype == "shield")
			{
				flag = RelicGenerator.GiveRandomShieldBestowal(Object, Tier, Standard, ShowInShortDescription);
			}
			else if (Subtype == "implant")
			{
				flag = RelicGenerator.GiveRandomImplantBestowal(Object, Tier, Standard, ShowInShortDescription);
			}
			else if (Subtype == "book")
			{
				flag = Object.RequirePart<TrainingBook>(false).AssignRandomTraining();
			}
			if (flag && Object.HasStat("Hitpoints"))
			{
				Object.GetStat("Hitpoints").BaseValue += 100;
			}
			AfterBasicBestowalEvent.Send(Object, Type, Subtype, Tier, Standard);
			return flag;
		}

		// Token: 0x060063C6 RID: 25542 RVA: 0x001EAA9C File Offset: 0x001E8C9C
		public static bool ApplyElementBestowal(GameObject Object, string Element, string Type, int Tier = 1, string Subtype = null)
		{
			bool flag = false;
			if (Subtype == null)
			{
				Subtype = RelicGenerator.GetSubtype(Type);
			}
			if (Subtype == "ranged")
			{
				if (Element == "glass")
				{
					if (!Object.HasPart<ModImprovedClairvoyance>())
					{
						Object.AddPart<ModImprovedClairvoyance>(new ModImprovedClairvoyance(Tier), true, false);
						flag = true;
					}
				}
				else if (Element == "jewels")
				{
					RelicGenerator.AddAttributeBoost(Object, "Ego", Tier, ref flag);
				}
				else if (Element == "stars")
				{
					if (!Object.HasPart<ModImprovedLightManipulation>())
					{
						Object.AddPart<ModImprovedLightManipulation>(new ModImprovedLightManipulation(Tier), true, false);
						flag = true;
					}
				}
				else if (Element == "time")
				{
					if (!Object.HasPart<ModImprovedTemporalFugue>())
					{
						Object.AddPart<ModImprovedTemporalFugue>(new ModImprovedTemporalFugue(Tier), true, false);
						flag = true;
					}
				}
				else if (Element == "salt")
				{
					RelicGenerator.AddAttributeBoost(Object, "Willpower", Tier, ref flag);
				}
				else if (Element == "ice")
				{
					RelicGenerator.AddResistanceBoost(Object, "ColdResistance", Tier, ref flag);
				}
				else if (Element == "scholarship")
				{
					RelicGenerator.AddAttributeBoost(Object, "Intelligence", Tier, ref flag);
				}
				else if (Element == "might")
				{
					if (MutationFactory.HasMutation("Telekinesis") && 20.in100() && !Object.HasPart<ModImprovedTelekinesis>())
					{
						Object.AddPart<ModImprovedTelekinesis>(new ModImprovedTelekinesis(Tier), true, false);
						flag = true;
					}
					else
					{
						RelicGenerator.AddAttributeBoost(Object, "Strength", Tier, ref flag);
					}
				}
				else if (Element == "travel")
				{
					if (50.in100() && !Object.HasPart<ModImprovedTeleportation>())
					{
						Object.AddPart<ModImprovedTeleportation>(new ModImprovedTeleportation(Tier), true, false);
						flag = true;
					}
					else
					{
						RelicGenerator.AddMoveSpeedBoost(Object, Tier, ref flag);
					}
				}
				else if (Element == "chance")
				{
					RelicGenerator.AddAttributeBoost(Object, "Agility", Tier, ref flag);
				}
				else if (Element == "circuitry" && !Object.HasPart<ModImprovedElectricalGeneration>())
				{
					Object.AddPart<ModImprovedElectricalGeneration>(new ModImprovedElectricalGeneration(Tier), true, false);
					flag = true;
				}
			}
			else if (Subtype == "weapon")
			{
				if (Element == "glass")
				{
					if (!Object.HasPart<ModGlazed>())
					{
						Object.AddPart<ModGlazed>(new ModGlazed(Tier), true, false);
						flag = true;
					}
				}
				else if (Element == "jewels")
				{
					if (50.in100() && !Object.HasPart<ModTransmuteOnHit>())
					{
						Object.AddPart<ModTransmuteOnHit>(new ModTransmuteOnHit(Tier * 4, "Gemstones"), true, false);
						flag = true;
					}
					else
					{
						RelicGenerator.AddAttributeBoost(Object, "Ego", Tier, ref flag);
					}
				}
				else if (Element == "stars")
				{
					if (!Object.HasPart<ModImprovedLightManipulation>())
					{
						Object.AddPart<ModImprovedLightManipulation>(new ModImprovedLightManipulation(Tier), true, false);
						flag = true;
					}
				}
				else if (Element == "time")
				{
					if (!Object.HasPart<ModImprovedTemporalFugue>())
					{
						Object.AddPart<ModImprovedTemporalFugue>(new ModImprovedTemporalFugue(Tier), true, false);
						flag = true;
					}
				}
				else if (Element == "salt")
				{
					RelicGenerator.AddAttributeBoost(Object, "Willpower", Tier, ref flag);
				}
				else if (Element == "ice")
				{
					if (!Object.HasPart<ModRelicFreezing>())
					{
						Object.AddPart<ModRelicFreezing>(new ModRelicFreezing(Tier), true, false);
						flag = true;
					}
				}
				else if (Element == "scholarship")
				{
					if (!Object.HasPart<ModBeetlehost>())
					{
						Object.AddPart<ModBeetlehost>(new ModBeetlehost(Tier), true, false);
						flag = true;
					}
				}
				else if (Element == "might")
				{
					if (MutationFactory.HasMutation("Telekinesis") && 20.in100() && !Object.HasPart<ModImprovedTelekinesis>())
					{
						Object.AddPart<ModImprovedTelekinesis>(new ModImprovedTelekinesis(Tier), true, false);
						flag = true;
					}
					else
					{
						RelicGenerator.AddAttributeBoost(Object, "Strength", Tier, ref flag);
					}
				}
				else if (Element == "travel")
				{
					if (!Object.HasPart<ModImprovedTeleportation>())
					{
						Object.AddPart<ModImprovedTeleportation>(new ModImprovedTeleportation(Tier), true, false);
						flag = true;
					}
				}
				else if (Element == "chance")
				{
					if (!Object.HasPart<ModFatecaller>())
					{
						Object.AddPart<ModFatecaller>(new ModFatecaller(Tier), true, false);
						flag = true;
					}
				}
				else if (Element == "circuitry" && !Object.HasPart<ModImprovedElectricalGeneration>())
				{
					Object.AddPart<ModImprovedElectricalGeneration>(new ModImprovedElectricalGeneration(Tier), true, false);
					flag = true;
				}
			}
			else if (Subtype == "armor")
			{
				if (Element == "glass")
				{
					if (50.in100() && !Object.HasPart<ModGlassArmor>())
					{
						Object.AddPart<ModGlassArmor>(new ModGlassArmor(Tier), true, false);
						flag = true;
					}
					else if (!Object.HasPart<ModImprovedClairvoyance>())
					{
						Object.AddPart<ModImprovedClairvoyance>(new ModImprovedClairvoyance(Tier), true, false);
						flag = true;
					}
				}
				else if (Element == "jewels")
				{
					Object.GetPart<Armor>().Ego += RelicGenerator.GetAttributeBonusFromRelicTier(Tier);
					flag = true;
				}
				else if (Element == "stars")
				{
					if (!Object.HasPart<ModImprovedLightManipulation>())
					{
						Object.AddPart<ModImprovedLightManipulation>(new ModImprovedLightManipulation(Tier), true, false);
						flag = true;
					}
				}
				else if (Element == "time")
				{
					if (!Object.HasPart<ModImprovedTemporalFugue>())
					{
						Object.AddPart<ModImprovedTemporalFugue>(new ModImprovedTemporalFugue(Tier), true, false);
						flag = true;
					}
				}
				else if (Element == "salt")
				{
					Object.GetPart<Armor>().Willpower += RelicGenerator.GetAttributeBonusFromRelicTier(Tier);
					flag = true;
				}
				else if (Element == "ice")
				{
					Object.GetPart<Armor>().Cold += RelicGenerator.GetResistanceBonusFromRelicTier(Tier);
					flag = true;
				}
				else if (Element == "scholarship")
				{
					Object.GetPart<Armor>().Intelligence += RelicGenerator.GetAttributeBonusFromRelicTier(Tier);
					flag = true;
				}
				else if (Element == "might")
				{
					if (MutationFactory.HasMutation("Telekinesis") && 20.in100() && !Object.HasPart<ModImprovedTelekinesis>())
					{
						Object.AddPart<ModImprovedTelekinesis>(new ModImprovedTelekinesis(Tier), true, false);
						flag = true;
					}
					else
					{
						Object.GetPart<Armor>().Strength += RelicGenerator.GetAttributeBonusFromRelicTier(Tier);
						flag = true;
					}
				}
				else if (Element == "travel")
				{
					if (50.in100() && !Object.HasPart<ModImprovedTeleportation>())
					{
						Object.AddPart<ModImprovedTeleportation>(new ModImprovedTeleportation(Tier), true, false);
						flag = true;
					}
					else
					{
						RelicGenerator.AddMoveSpeedBoost(Object, Tier, ref flag);
					}
				}
				else if (Element == "chance")
				{
					if (!Object.HasPart<ModBlinkEscape>())
					{
						Object.AddPart<ModBlinkEscape>(new ModBlinkEscape(Tier), true, false);
						flag = true;
					}
				}
				else if (Element == "circuitry" && !Object.HasPart<ModImprovedElectricalGeneration>())
				{
					Object.AddPart<ModImprovedElectricalGeneration>(new ModImprovedElectricalGeneration(Tier), true, false);
					flag = true;
				}
			}
			else if (Subtype == "shield")
			{
				if (Element == "glass")
				{
					if (50.in100() && !Object.HasPart<ModGlassArmor>())
					{
						Object.AddPart<ModGlassArmor>(new ModGlassArmor(Tier), true, false);
						flag = true;
					}
					else if (!Object.HasPart<ModImprovedClairvoyance>())
					{
						Object.AddPart<ModImprovedClairvoyance>(new ModImprovedClairvoyance(Tier), true, false);
						flag = true;
					}
				}
				else if (Element == "jewels")
				{
					RelicGenerator.AddAttributeBoost(Object, "Ego", Tier, ref flag);
				}
				else if (Element == "stars")
				{
					if (!Object.HasPart<ModImprovedLightManipulation>())
					{
						Object.AddPart<ModImprovedLightManipulation>(new ModImprovedLightManipulation(Tier), true, false);
						flag = true;
					}
				}
				else if (Element == "time")
				{
					if (!Object.HasPart<ModImprovedTemporalFugue>())
					{
						Object.AddPart<ModImprovedTemporalFugue>(new ModImprovedTemporalFugue(Tier), true, false);
						flag = true;
					}
				}
				else if (Element == "salt")
				{
					RelicGenerator.AddAttributeBoost(Object, "Willpower", Tier, ref flag);
				}
				else if (Element == "ice")
				{
					RelicGenerator.AddResistanceBoost(Object, "ColdResistance", Tier, ref flag);
				}
				else if (Element == "scholarship")
				{
					RelicGenerator.AddAttributeBoost(Object, "Intelligence", Tier, ref flag);
				}
				else if (Element == "might")
				{
					if (MutationFactory.HasMutation("Telekinesis") && 20.in100() && !Object.HasPart<ModImprovedTelekinesis>())
					{
						Object.AddPart<ModImprovedTelekinesis>(new ModImprovedTelekinesis(Tier), true, false);
						flag = true;
					}
					else
					{
						RelicGenerator.AddAttributeBoost(Object, "Strength", Tier, ref flag);
					}
				}
				else if (Element == "chance")
				{
					if (!Object.HasPart<ModBlinkEscape>())
					{
						Object.AddPart<ModBlinkEscape>(new ModBlinkEscape(Tier), true, false);
						flag = true;
					}
				}
				else if (Element == "circuitry")
				{
					if (!Object.HasPart<ModImprovedElectricalGeneration>())
					{
						Object.AddPart<ModImprovedElectricalGeneration>(new ModImprovedElectricalGeneration(Tier), true, false);
						flag = true;
					}
				}
				else if (Element == "travel")
				{
					if (50.in100() && !Object.HasPart<ModImprovedTeleportation>())
					{
						Object.AddPart<ModImprovedTeleportation>(new ModImprovedTeleportation(Tier), true, false);
						flag = true;
					}
					else
					{
						RelicGenerator.AddMoveSpeedBoost(Object, Tier, ref flag);
					}
				}
			}
			else if (Subtype == "implant")
			{
				if (Element == "glass")
				{
					if (!Object.HasPart<ModImprovedClairvoyance>())
					{
						RelicGenerator.SyncImplantPart(Object, Object.AddPart<ModImprovedClairvoyance>(new ModImprovedClairvoyance(Tier), true, false));
						flag = true;
					}
				}
				else if (Element == "jewels")
				{
					if (RelicGenerator.AddImplantAttributeBoost(Object, "Ego", Tier, false))
					{
						flag = true;
					}
				}
				else if (Element == "stars")
				{
					if (!Object.HasPart<ModImprovedLightManipulation>())
					{
						RelicGenerator.SyncImplantPart(Object, Object.AddPart<ModImprovedLightManipulation>(new ModImprovedLightManipulation(Tier), true, false));
						flag = true;
					}
				}
				else if (Element == "time")
				{
					if (!Object.HasPart<ModImprovedTemporalFugue>())
					{
						RelicGenerator.SyncImplantPart(Object, Object.AddPart<ModImprovedTemporalFugue>(new ModImprovedTemporalFugue(Tier), true, false));
						flag = true;
					}
				}
				else if (Element == "salt")
				{
					if (RelicGenerator.AddImplantAttributeBoost(Object, "Willpower", Tier, false))
					{
						flag = true;
					}
				}
				else if (Element == "ice")
				{
					if (RelicGenerator.AddImplantResistanceBoost(Object, "ColdResistance", Tier, false))
					{
						flag = true;
					}
				}
				else if (Element == "scholarship")
				{
					if (RelicGenerator.AddImplantAttributeBoost(Object, "Intelligence", Tier, false))
					{
						flag = true;
					}
				}
				else if (Element == "might")
				{
					if (MutationFactory.HasMutation("Telekinesis") && 20.in100() && !Object.HasPart<ModImprovedTelekinesis>())
					{
						RelicGenerator.SyncImplantPart(Object, Object.AddPart<ModImprovedTelekinesis>(new ModImprovedTelekinesis(Tier), true, false));
						flag = true;
					}
					else if (RelicGenerator.AddImplantAttributeBoost(Object, "Strength", Tier, false))
					{
						flag = true;
					}
				}
				else if (Element == "travel")
				{
					if (RelicGenerator.AddImplantMoveSpeedBoost(Object, Tier, false))
					{
						flag = true;
					}
				}
				else if (Element == "chance")
				{
					if (RelicGenerator.AddImplantAttributeBoost(Object, "Agility", Tier, false))
					{
						flag = true;
					}
				}
				else if (Element == "circuitry" && !Object.HasPart<ModImprovedElectricalGeneration>())
				{
					RelicGenerator.SyncImplantPart(Object, Object.AddPart<ModImprovedElectricalGeneration>(new ModImprovedElectricalGeneration(Tier), true, false));
					flag = true;
				}
			}
			if (Element == "travel" && (Subtype == "armor" || Subtype == "shield" || Subtype == "implant") && !Object.HasPart<CarryBonus>())
			{
				CarryBonus carryBonus = new CarryBonus(Stat.Random(0, 4) * 5 + (Tier - 1) * 10 + 20, "Flat");
				if (Subtype == "implant")
				{
					carryBonus.WorksOn(false, false, false, false, false, false, true, false, false, false);
					carryBonus.IsTechScannable = true;
					carryBonus.IsEMPSensitive = true;
					carryBonus.NameForStatus = "LiftAmplifier";
				}
				Object.AddPart<CarryBonus>(carryBonus, true, false);
				if (Subtype == "implant")
				{
					RelicGenerator.SyncImplantPart(Object, carryBonus);
				}
				flag = true;
			}
			if (flag && Object.HasStat("Hitpoints"))
			{
				Object.GetStat("Hitpoints").BaseValue += 200;
			}
			AfterElementBestowalEvent.Send(Object, Element, Type, Subtype, Tier);
			return flag;
		}

		// Token: 0x060063C7 RID: 25543 RVA: 0x001EB694 File Offset: 0x001E9894
		public static GameObject GenerateRelic(string Type, int Tier, HistoricEntitySnapshot SnapRegion = null, List<string> Adjectives = null, Dictionary<string, List<string>> ListProperties = null, string Name = null, string Article = null, string LikedFactionDescriptionAddendum = null)
		{
			History sultanHistory = The.Game.sultanHistory;
			GameObject gameObject = RelicGenerator.GenerateBaseRelic(ref Type, Tier, false);
			gameObject.Render.ColorString = "&M";
			gameObject.Render.TileColor = "&M";
			Description part = gameObject.GetPart<Description>();
			int targetPeriod = RelicGenerator.GetPeriodFromRelicTier(Tier);
			HistoricEntityList entitiesByDelegate = sultanHistory.GetEntitiesByDelegate((HistoricEntity entity) => entity.GetSnapshotAtYear(entity.lastYear).GetProperty("type", "unknown").Equals("sultan") && int.Parse(entity.GetSnapshotAtYear(entity.lastYear).GetProperty("period", "unknown")) == targetPeriod);
			if (entitiesByDelegate.Count > 0)
			{
				string property = entitiesByDelegate.GetRandomElement(null).GetCurrentSnapshot().GetProperty("name", "unknown");
				part._Short = part._Short.Replace("*sultan*", property);
			}
			else
			{
				MetricsManager.LogError("Could not find a sultan for period " + targetPeriod.ToString() + " from tier " + Tier.ToString());
				part._Short = part._Short.Replace("*sultan*", "the sultan");
			}
			List<string> list = new List<string>();
			for (int i = 0; i < Adjectives.Count; i++)
			{
				string text = RelicGenerator.TranslateAdjective(Adjectives[i]);
				if (!text.IsNullOrEmpty() && !list.Contains(text))
				{
					list.Add(text);
				}
			}
			if (list.Count == 0)
			{
				list.Add(HistoricStringExpander.ExpandString("<spice.elements.!random>", null, sultanHistory, null, null));
			}
			string subtype = RelicGenerator.GetSubtype(Type);
			RelicGenerator.ApplyBasicBestowal(gameObject, Type, Tier, subtype, true, false);
			List<string> list2 = null;
			foreach (string text2 in list)
			{
				RelicGenerator.ApplyElementBestowal(gameObject, text2, Type, Tier, subtype);
				if (text2 != "none")
				{
					if (list2 == null)
					{
						list2 = new List<string>();
					}
					if (!list2.Contains(text2))
					{
						list2.Add(text2);
					}
					string text3 = HistoricStringExpander.ExpandString("<spice.elements." + text2 + ".nounsPlural.!random>", null, sultanHistory, null, null);
					Description description = part;
					description._Short = string.Concat(new string[]
					{
						description._Short,
						" ",
						gameObject.Itis,
						" ",
						HistoricStringExpander.ExpandString("<spice.instancesOf.stamped_VAR.!random>", null, sultanHistory, null, null).Replace("*var*", text3),
						"."
					});
				}
			}
			if (!list2.IsNullOrEmpty<string>())
			{
				ItemElements itemElements = new ItemElements();
				StringBuilder stringBuilder = Event.NewStringBuilder(null);
				foreach (string text4 in list2)
				{
					stringBuilder.Compound(text4, ";;").Append("::").Append(4 + Tier * 2);
				}
				itemElements._Elements = Event.FinalizeString(stringBuilder);
				gameObject.AddPart<ItemElements>(itemElements, true, false);
			}
			if (ListProperties != null)
			{
				foreach (KeyValuePair<string, List<string>> keyValuePair in ListProperties)
				{
					if (keyValuePair.Key == "likedFactions")
					{
						using (List<string>.Enumerator enumerator = keyValuePair.Value.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								string text5 = enumerator.Current;
								AddsRep.AddModifier(gameObject, text5 + ":200");
								Description description = part;
								description._Short = string.Concat(new string[]
								{
									description._Short,
									" There's an engraving of ",
									Faction.GetFormattedName(text5),
									" being ",
									HistoricStringExpander.ExpandString("<spice.instancesOf.venerated.!random>", null, sultanHistory, null, null)
								});
								if (!LikedFactionDescriptionAddendum.IsNullOrEmpty())
								{
									Description description2 = part;
									description2._Short += LikedFactionDescriptionAddendum;
								}
								Description description3 = part;
								description3._Short += ".";
							}
							continue;
						}
					}
					if (keyValuePair.Key == "lovedFactions")
					{
						using (List<string>.Enumerator enumerator = keyValuePair.Value.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								string text6 = enumerator.Current;
								AddsRep.AddModifier(gameObject, text6 + ":400");
								Description description = part;
								description._Short = string.Concat(new string[]
								{
									description._Short,
									" There's an engraving of ",
									Faction.GetFormattedName(text6),
									" being ",
									HistoricStringExpander.ExpandString("<spice.instancesOf.venerated.!random>", null, sultanHistory, null, null),
									"."
								});
							}
							continue;
						}
					}
					if (keyValuePair.Key == "hatedFactions")
					{
						foreach (string text7 in keyValuePair.Value)
						{
							AddsRep.AddModifier(gameObject, text7 + ":-200");
							if (!gameObject.HasPart<ModFactionSlayer>())
							{
								gameObject.AddPart<ModFactionSlayer>(new ModFactionSlayer(Tier, text7), true, false);
								Description description = part;
								description._Short = string.Concat(new string[]
								{
									description._Short,
									" There's an engraving of ",
									Faction.GetFormattedName(text7),
									" being ",
									HistoricStringExpander.ExpandString("<spice.instancesOf.disparaged.!random>", null, sultanHistory, null, null),
									"."
								});
							}
						}
					}
				}
			}
			if (subtype == "curio")
			{
				int num = Stat.Random(1, 2);
				if (num == 1)
				{
					string text8 = "Curio_SummoningRelic" + Tier.ToString();
					int num2 = 0;
					string blueprint;
					Faction faction;
					do
					{
						blueprint = PopulationManager.RollOneFrom(text8, null, null).Blueprint;
						string primaryFaction = GameObjectFactory.Factory.GetBlueprint(blueprint).GetPrimaryFaction();
						faction = ((primaryFaction == null) ? null : Factions.Get(primaryFaction));
					}
					while ((faction == null || !faction.Old) && ++num2 < 1000);
					gameObject.RequirePart<SummoningCurio>(false).Creature = blueprint;
				}
				else if (num == 2)
				{
					gameObject.RequirePart<GenocideCurio>(false);
				}
			}
			if (Type == "Book")
			{
				gameObject.GetPart<Commerce>().Value = (double)(200 + 100 * Tier);
			}
			else
			{
				gameObject.GetPart<Commerce>().Value = (double)(500 + 150 * Tier);
			}
			string randomElement = list.GetRandomElement<string>(null);
			if (Name == null)
			{
				Name = RelicGenerator.GenerateRelicName(Type, SnapRegion, randomElement, out Article);
			}
			gameObject.GiveProperName(Name, true, null, false, null, null, null, null);
			if (!Article.IsNullOrEmpty())
			{
				gameObject.SetStringProperty("IndefiniteArticle", Article, false);
				gameObject.SetStringProperty("DefiniteArticle", Article, false);
				gameObject.SetStringProperty("RelicName", Name, false);
			}
			else
			{
				gameObject.SetStringProperty("RelicName", Name, false);
			}
			gameObject.RequirePart<DisplayNameColor>(false).SetColor("M");
			MarkovBook markovBook;
			if (subtype == "book" && gameObject.TryGetPart<MarkovBook>(out markovBook))
			{
				markovBook.Title = Name;
			}
			gameObject.AddPart<TakenWXU>(new TakenWXU(2), true, false);
			gameObject.SetStringProperty("EquipLayerSound", "sfx_equip_armor_addLayer_relic", false);
			AfterRelicGeneratedEvent.Send(gameObject, list, randomElement, Type, subtype, Tier);
			string text9 = (string.IsNullOrEmpty(gameObject.Render.DetailColor) ? "y" : gameObject.Render.DetailColor);
			gameObject.SetStringProperty("EquipmentFrameColors", text9 + "M" + text9 + "M", false);
			return gameObject;
		}

		// Token: 0x04002ED9 RID: 11993
		public static List<string> Types = new List<string>();

		// Token: 0x04002EDA RID: 11994
		public static Dictionary<string, string> TypeMap = new Dictionary<string, string>();

		// Token: 0x04002EDB RID: 11995
		private static readonly Dictionary<string, Action<XmlDataHelper>> _outerNodes = new Dictionary<string, Action<XmlDataHelper>> { 
		{
			"relics",
			new Action<XmlDataHelper>(RelicGenerator.HandleInnerNode)
		} };

		// Token: 0x04002EDC RID: 11996
		private static readonly Dictionary<string, Action<XmlDataHelper>> _innerNodes = new Dictionary<string, Action<XmlDataHelper>>
		{
			{
				"relictypes",
				new Action<XmlDataHelper>(RelicGenerator.HandleInnerNode)
			},
			{
				"relictypemappings",
				new Action<XmlDataHelper>(RelicGenerator.HandleInnerNode)
			},
			{
				"relictype",
				new Action<XmlDataHelper>(RelicGenerator.HandleRelicTypeNode)
			},
			{
				"removerelictype",
				new Action<XmlDataHelper>(RelicGenerator.HandleRemoveRelicTypeNode)
			},
			{
				"relictypemapping",
				new Action<XmlDataHelper>(RelicGenerator.HandleRelicTypeMappingNode)
			},
			{
				"removerelictypemapping",
				new Action<XmlDataHelper>(RelicGenerator.HandleRemoveRelicTypeMappingNode)
			}
		};

		// Token: 0x04002EDD RID: 11997
		private static Dictionary<string, string> RelicNameContext = new Dictionary<string, string>();
	}
}
