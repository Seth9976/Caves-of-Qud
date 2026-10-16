using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using ConsoleLib.Console;
using HistoryKit;
using Qud.API;
using XRL.Annals;
using XRL.Language;
using XRL.Names;
using XRL.Rules;
using XRL.UI;
using XRL.Wish;
using XRL.World.Anatomy;
using XRL.World.Parts;
using XRL.World.Tinkering;

namespace XRL.World.Capabilities
{
	// Token: 0x020009EC RID: 2540
	[HasWishCommand]
	public static class ItemNaming
	{
		// Token: 0x06006C64 RID: 27748 RVA: 0x002315A8 File Offset: 0x0022F7A8
		public static bool CanBeNamed(GameObject Object, GameObject Owner)
		{
			return !ItemNaming.Suppress && GameObject.Validate(ref Object) && !Object.HasProperName && Object.Physics.IsReal && Object.Render != null && !Object.IsTemporary && !Object.HasPart<NaturalEquipment>() && !Object.HasTag("AlwaysStack") && !Object.HasTag("Creature") && !TinkeringHelpers.ConsiderScrap(Object, Owner) && !Object.HasTagOrProperty("QuestItem") && !(Object.GetInventoryCategory(false) == "Quest Items") && CanBeNamedEvent.Check(Owner, Object);
		}

		// Token: 0x06006C65 RID: 27749 RVA: 0x00231658 File Offset: 0x0022F858
		public static int GetNamingChance(GameObject Object, GameObject Owner)
		{
			if (!ItemNaming.CanBeNamed(Object, Owner))
			{
				return 0;
			}
			if (Object.Count > 1)
			{
				return 0;
			}
			bool flag = Object.Equipped != null || Object.Implantee != null;
			if (Object.HasPart<Armor>() && !flag)
			{
				return 0;
			}
			if (Object.HasPart<Shield>() && !flag)
			{
				return 0;
			}
			long num = The.CurrentTurn - 100L;
			int num2 = Brain.WeaponScore(Object, Owner);
			int num3 = Brain.MissileWeaponScore(Object, Owner);
			if ((num2 >= 5 || num3 >= 5) && !flag)
			{
				if (Owner.IsPlayer())
				{
					if ((long)Object.GetIntProperty("LastKillAsMeleeWeaponByPlayerTurn", 0) < num && (long)Object.GetIntProperty("LastKillAsThrownWeaponByPlayerTurn", 0) < num && (long)Object.GetIntProperty("LastKillAsLauncherByPlayerTurn", 0) < num && (long)Object.GetIntProperty("LastKillAsProjectileByPlayerTurn", 0) < num)
					{
						return 0;
					}
				}
				else if ((long)Object.GetIntProperty("LastKillAsMeleeWeaponTurn", 0) < num && (long)Object.GetIntProperty("LastKillAsThrownWeaponTurn", 0) < num && (long)Object.GetIntProperty("LastKillAsLauncherTurn", 0) < num && (long)Object.GetIntProperty("LastKillAsProjectileTurn", 0) < num)
				{
					return 0;
				}
			}
			double num4 = (double)(Object.GetTier() - 1 + Object.GetModificationCount());
			num4 += (double)Math.Min(Brain.ArmorScore(Object, Owner).DiminishingReturns(0.5), 20);
			num4 += (double)Math.Min(Brain.ShieldScore(Object, Owner).DiminishingReturns(1.0), 20);
			int num5 = Object.GetIntProperty("KillsAsMeleeWeapon", 0) - Object.GetIntProperty("AccidentalKillsAsMeleeWeapon", 0) / 2 + Object.GetIntProperty("KillsAsThrownWeapon", 0) - Object.GetIntProperty("AccidentalKillsAsThrownWeapon", 0) / 2 + Object.GetIntProperty("KillsAsLauncher", 0) - Object.GetIntProperty("AccidentalKillsAsLauncher", 0) / 2 + Object.GetIntProperty("KillsAsProjectile", 0) - Object.GetIntProperty("AccidentalKillsAsProjectile", 0) / 2;
			if (num5 > 0)
			{
				num4 += (double)Math.Min(num5.DiminishingReturns(1.0), 50);
			}
			if (num5 > 0 || flag)
			{
				num4 += (double)Math.Min(num2.DiminishingReturns(1.0), 10);
				num4 += (double)Math.Min(num3.DiminishingReturns(1.0), 10);
			}
			int num6 = Object.GetIntProperty("InventoryActions", 0) + Object.GetIntProperty("ActivatedAbilityCommandsProcessed", 0) + Object.GetIntProperty("SifrahActions", 0);
			if (num6 != 0)
			{
				num4 += (double)Math.Min(num6.DiminishingReturns(1.0) / 4, 20);
			}
			int intProperty = Object.GetIntProperty("ItemNamingBonus", 0);
			if (intProperty != 0)
			{
				num4 += (double)Math.Min(intProperty.DiminishingReturns(1.0), 10);
			}
			Description description;
			if (Object.HasPart<MakersMark>())
			{
				num4 += 1.0;
			}
			else if (Object.TryGetPart<Description>(out description) && !description.Mark.IsNullOrEmpty())
			{
				num4 += 1.0;
			}
			if (num4 <= 0.0)
			{
				if (flag)
				{
					num4 = 1.0;
				}
				else if ((long)Object.GetIntProperty("LastInventoryActionTurn", 0) > num)
				{
					num4 = 1.0;
				}
			}
			num4 = GetNamingChanceEvent.GetFor(Owner, Object, num4, 0.0, 0.0);
			return Math.Max((int)Math.Round(num4, MidpointRounding.AwayFromZero), 0);
		}

		// Token: 0x06006C66 RID: 27750 RVA: 0x002319A8 File Offset: 0x0022FBA8
		public static Dictionary<GameObject, int> GetNamingChances(GameObject Owner)
		{
			List<GameObject> wholeInventoryReadonly = Owner.GetWholeInventoryReadonly();
			Dictionary<GameObject, int> dictionary = null;
			int i = 0;
			int count = wholeInventoryReadonly.Count;
			while (i < count)
			{
				int namingChance = ItemNaming.GetNamingChance(wholeInventoryReadonly[i], Owner);
				if (namingChance > 0)
				{
					if (dictionary == null)
					{
						dictionary = new Dictionary<GameObject, int>((wholeInventoryReadonly.Count >= 32) ? 16 : 8);
					}
					dictionary.Add(wholeInventoryReadonly[i], namingChance);
				}
				i++;
			}
			return dictionary;
		}

		// Token: 0x06006C67 RID: 27751 RVA: 0x00231A10 File Offset: 0x0022FC10
		public static bool Opportunity(GameObject Owner, GameObject Kill = null, GameObject InfluencedBy = null, string ZoneID = null, string OpportunityType = "General", int SuppressedByAnyTypeForLevels = 0, int SuppressedBySameTypeForLevels = 0, int SuppressedBySameTypeOnlyIfAtLeast = 0, int ChanceToBypassSuppression = 0, bool Force = false)
		{
			if (Popup.Suppress)
			{
				return false;
			}
			bool flag = false;
			if (SuppressedByAnyTypeForLevels > 0)
			{
				int intProperty = Owner.GetIntProperty("LastItemNamingDoneAtLevel", 0);
				if (intProperty > 0 && Owner.Stat("Level", 0) < intProperty + SuppressedByAnyTypeForLevels)
				{
					flag = true;
				}
			}
			if (!flag && SuppressedBySameTypeForLevels > 0)
			{
				int intProperty2 = Owner.GetIntProperty("Last" + OpportunityType + "ItemNamingDoneAtLevel", 0);
				if (intProperty2 > 0 && Owner.Stat("Level", 0) < intProperty2 + SuppressedBySameTypeForLevels && (SuppressedBySameTypeOnlyIfAtLeast == 0 || Owner.GetIntProperty(OpportunityType + "ItemNamingDoneAtLevel", 0) < SuppressedBySameTypeOnlyIfAtLeast))
				{
					flag = true;
				}
			}
			if (flag && !ChanceToBypassSuppression.in100())
			{
				return false;
			}
			if (!GameObject.Validate(ref Owner))
			{
				return false;
			}
			Dictionary<GameObject, int> namingChances = ItemNaming.GetNamingChances(Owner);
			if (namingChances == null || namingChances.Count <= 0)
			{
				return false;
			}
			List<GameObject> list = Event.NewGameObjectList();
			int num = 0;
			for (;;)
			{
				foreach (KeyValuePair<GameObject, int> keyValuePair in namingChances)
				{
					if (Owner.IsPlayer() ? keyValuePair.Value.in1000(Stat.NamingRnd) : keyValuePair.Value.in1000())
					{
						list.Add(keyValuePair.Key);
					}
				}
				if (list.Count > 0)
				{
					break;
				}
				if (!Force || ++num >= 1000)
				{
					return false;
				}
			}
			bool? flag2;
			do
			{
				GameObject gameObject = null;
				if (list.Count > 1)
				{
					if (Owner.IsPlayer())
					{
						List<string> list2 = new List<string>(list.Count);
						List<object> list3 = new List<object>(list.Count);
						List<char> list4 = new List<char>(list.Count);
						list2.Add("nothing");
						list3.Add(null);
						list4.Add('-');
						char c = 'a';
						int i = 0;
						int count = list.Count;
						while (i < count)
						{
							list2.Add(list[i].DisplayName);
							list3.Add(list[i]);
							list4.Add(c);
							c += '\u0001';
							i++;
						}
						int num2 = Popup.PickOption("You swell with the inspiration to name an item.", "What would you like to name?", "", "Sounds/UI/ui_notification_question", list2.ToArray(), list4.ToArray(), null, null, null, null, null, 0, 60, 0, -1, false, false, false, true, false, null, null);
						if (num2 >= 0)
						{
							gameObject = list3[num2] as GameObject;
						}
					}
					else
					{
						Dictionary<GameObject, int> dictionary = new Dictionary<GameObject, int>(list.Count);
						int j = 0;
						int count2 = list.Count;
						while (j < count2)
						{
							dictionary.Add(list[j], namingChances[list[j]]);
							j++;
						}
						gameObject = dictionary.GetRandomElement<GameObject>(null);
					}
				}
				else if (Owner.IsPlayer())
				{
					if (Popup.ShowYesNo("You swell with the inspiration to name your " + list[0].DisplayNameOnly + ". Do you wish to?", "Sounds/UI/ui_notification_question", false, DialogResult.Yes, null) == DialogResult.Yes)
					{
						gameObject = list[0];
					}
				}
				else
				{
					gameObject = list[0];
				}
				flag2 = ItemNaming.NameItem(gameObject, Owner, Kill, InfluencedBy, ZoneID, OpportunityType, true);
			}
			while (flag2 == null);
			return flag2.GetValueOrDefault();
		}

		// Token: 0x06006C68 RID: 27752 RVA: 0x00231D2C File Offset: 0x0022FF2C
		private static string FindKillBlueprint(GameObject Object)
		{
			string text = null;
			long num = 0L;
			int i = 0;
			int num2 = ItemNaming.KillBlueprintSources.Length;
			while (i < num2)
			{
				string text2 = ItemNaming.KillBlueprintSources[i];
				string text3 = ItemNaming.KillBlueprintSources[i + 1];
				string stringProperty = Object.GetStringProperty(text2, null);
				if (!stringProperty.IsNullOrEmpty())
				{
					long longProperty = Object.GetLongProperty(text3, 0L);
					if (longProperty > num)
					{
						text = stringProperty;
						num = longProperty;
					}
				}
				i += 2;
			}
			return text;
		}

		// Token: 0x06006C69 RID: 27753 RVA: 0x00231D94 File Offset: 0x0022FF94
		private static string FindZoneName(GameObject Owner, string ZoneID = null)
		{
			if (ZoneID == null)
			{
				string text;
				if (Owner == null)
				{
					text = null;
				}
				else
				{
					Zone currentZone = Owner.GetCurrentZone();
					text = ((currentZone != null) ? currentZone.ZoneID : null);
				}
				ZoneID = text;
			}
			if (ZoneID == null)
			{
				return null;
			}
			if (The.ZoneManager.GetZoneHasProperName(ZoneID))
			{
				string text2 = The.ZoneManager.GetZoneBaseDisplayName(ZoneID, true);
				if (!text2.IsNullOrEmpty())
				{
					string zoneDefiniteArticle = The.ZoneManager.GetZoneDefiniteArticle(ZoneID);
					if (!zoneDefiniteArticle.IsNullOrEmpty())
					{
						text2 = zoneDefiniteArticle + " " + text2;
					}
					return text2;
				}
			}
			return The.ZoneManager.GetZoneNameContext(ZoneID) ?? ("the " + The.ZoneManager.GetZoneDisplayName(ZoneID, false, false, false, true));
		}

		// Token: 0x06006C6A RID: 27754 RVA: 0x00231E30 File Offset: 0x00230030
		private static string GenerateRelicStyleName(GameObject Object, GameObject Owner, GameObject Kill, GameObject InfluencedBy, string ZoneID, ref string Element, ref string Type)
		{
			if (Element == null)
			{
				Element = RelicGenerator.SelectElement(Object, Owner, Kill, InfluencedBy);
			}
			if (Type == null)
			{
				Type = RelicGenerator.GetType(Object);
			}
			string text = ItemNaming.FindZoneName(Owner, ZoneID);
			string text2;
			if (!text.IsNullOrEmpty() && 50.in100())
			{
				text2 = "the " + HistoricStringExpander.ExpandString(string.Concat(new string[]
				{
					"<spice.elements.",
					Element,
					".adjectives.!random> ",
					HistoricStringExpander.ExpandString("<spice.itemTypes." + Type + ".!random>", null),
					" of ",
					text
				}), null);
				text2 = Grammar.MakeTitleCase(text2);
			}
			else
			{
				string text3 = ((Kill != null && Kill.HasProperName && 30.in100()) ? Kill.an(int.MaxValue, null, null, false, false, false, false, true, false, true, false, false, false, false, null, false, null, false) : ((InfluencedBy != null && InfluencedBy.HasProperName && 50.in100()) ? InfluencedBy.an(int.MaxValue, null, null, false, false, false, false, true, false, true, false, false, false, false, null, false, null, false) : Owner.an(int.MaxValue, null, null, false, false, false, false, true, false, true, false, false, false, false, null, false, null, false)));
				Dictionary<string, string> dictionary = new Dictionary<string, string>
				{
					{ "*element*", Element },
					{ "*itemType*", Type },
					{
						"*personNounPossessive*",
						Grammar.MakePossessive(HistoricStringExpander.ExpandString("<spice.personNouns.!random>", null))
					},
					{
						"*creatureNamePossessive*",
						Grammar.MakePossessive(text3)
					}
				};
				text2 = HistoricStringExpander.ExpandString("<spice.history.relics.names.!random>", null, null, dictionary, null);
				if (text2.Contains(text3))
				{
					text2 = text2.Replace(text3, "------------");
					text2 = Grammar.MakeTitleCase(text2);
					text2 = text2.Replace("------------", text3);
				}
				else
				{
					text2 = Grammar.MakeTitleCase(text2);
				}
			}
			return text2;
		}

		// Token: 0x06006C6B RID: 27755 RVA: 0x00232008 File Offset: 0x00230208
		public static bool? NameItem(GameObject Object, GameObject Owner, GameObject Kill = null, GameObject InfluencedBy = null, string ZoneID = null, string OpportunityType = "General", bool CanBestow = true)
		{
			if (!GameObject.Validate(ref Object))
			{
				return new bool?(false);
			}
			string text = null;
			string text2 = null;
			bool flag = false;
			int num = 0;
			bool flag2 = false;
			bool flag3 = false;
			if (CanBestow)
			{
				GameObject gameObject = Object;
				string text3;
				if ((text3 = text) == null)
				{
					text3 = (text = RelicGenerator.GetType(Object));
				}
				string text4;
				if ((text4 = text2) == null)
				{
					text4 = (text2 = RelicGenerator.SelectElement(Object, Owner, Kill, InfluencedBy));
				}
				ItemNaming.CheckBestowals(Owner, gameObject, text3, text4, Kill, InfluencedBy, OpportunityType, out flag3, out num, out flag2);
				flag = true;
			}
			if (Kill == null)
			{
				string text5 = ItemNaming.FindKillBlueprint(Object);
				Kill = ((text5 != null) ? GameObject.CreateSample(text5) : null);
				if (Object.DisplayNameOnly.Contains("["))
				{
					Kill = null;
				}
			}
			string text6 = null;
			string text7 = null;
			bool flag4 = false;
			if (Owner.IsPlayer())
			{
				List<string> list = new List<string>();
				List<char> list2 = new List<char>();
				List<int> list3 = new List<int>();
				char c = 'a';
				list3.Add(0);
				List<char> list4 = list2;
				char c2 = c;
				c = c2 + '\u0001';
				list4.Add(c2);
				list.Add("Enter a name.");
				list3.Add(1);
				List<char> list5 = list2;
				char c3 = c;
				c = c3 + '\u0001';
				list5.Add(c3);
				list.Add(string.Concat(new string[] { "Name ", Object.them, " based on ", Object.its, " qualities." }));
				list3.Add(2);
				List<char> list6 = list2;
				char c4 = c;
				c = c4 + '\u0001';
				list6.Add(c4);
				list.Add("Choose a random name from your own culture.");
				if (Kill != null)
				{
					list3.Add(3);
					List<char> list7 = list2;
					char c5 = c;
					c = c5 + '\u0001';
					list7.Add(c5);
					list.Add("Choose a random name from " + Kill.poss("culture", true) + ".");
				}
				if (GameObject.Validate(ref InfluencedBy) && InfluencedBy.HasTag("Creature"))
				{
					list3.Add(4);
					List<char> list8 = list2;
					char c6 = c;
					c = c6 + '\u0001';
					list8.Add(c6);
					list.Add("Choose a random name from " + InfluencedBy.poss("culture", true) + ".");
				}
				int num3;
				for (;;)
				{
					int num2 = Popup.PickOption("", "Rename " + Object.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", "", "Sounds/UI/ui_notification", list.ToArray(), list2.ToArray(), null, null, null, null, null, 1, 60, 0, -1, !flag3, false, false, true, false, null, null);
					if (num2 < 0)
					{
						break;
					}
					num3 = list3[num2];
					if (num3 != 0)
					{
						goto IL_02E4;
					}
					text6 = Popup.AskString("Enter a new name for " + Object.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", "", "Sounds/UI/ui_notification", "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 -+/#()!@$%*<>'", null, 30, 0, false, true, null);
					if (!text6.IsNullOrEmpty())
					{
						text6 = text6.Trim();
						if (!text6.IsNullOrEmpty())
						{
							goto IL_039C;
						}
					}
				}
				return null;
				IL_02E4:
				if (num3 == 1)
				{
					text6 = ItemNaming.GenerateRelicStyleName(Object, Owner, Kill, InfluencedBy, ZoneID, ref text2, ref text);
				}
				else if (num3 == 2)
				{
					text6 = NameMaker.MakeName(Owner, null, null, null, null, null, null, null, null, null, "Item", null, null, false, true, null, null);
				}
				else if (num3 == 3)
				{
					text6 = NameMaker.MakeName(Kill, null, null, null, null, null, null, null, null, null, "Item", null, null, false, true, null, null);
				}
				else if (num3 == 4)
				{
					text6 = NameMaker.MakeName(InfluencedBy, null, null, null, null, null, null, null, null, null, "Item", null, null, false, true, null, null);
				}
				IL_039C:
				text7 = Popup.ShowColorPicker("", 0, string.Concat(new string[]
				{
					"You select the name '",
					text6,
					"' for ",
					Object.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
					". Choose a color for ",
					Object.them,
					"."
				}), 60, false, !flag3, null, "", true, true, false, text6);
				if (text7 == null)
				{
					return null;
				}
			}
			else
			{
				List<int> list9 = new List<int>(8);
				list9.Add(0);
				list9.Add(1);
				list9.Add(1);
				list9.Add(1);
				list9.Add(2);
				if (Kill != null)
				{
					list9.Add(3);
				}
				if (InfluencedBy != null)
				{
					list9.Add(4);
				}
				int randomElement = list9.GetRandomElement<int>(null);
				if (randomElement == 0 || randomElement == 2)
				{
					text6 = NameMaker.MakeName(Owner, null, null, null, null, null, null, null, null, null, "Item", null, null, false, true, null, null);
				}
				else if (randomElement == 1)
				{
					text6 = ItemNaming.GenerateRelicStyleName(Object, Owner, Kill, InfluencedBy, ZoneID, ref text2, ref text);
				}
				else if (randomElement == 3)
				{
					text6 = NameMaker.MakeName(Kill, null, null, null, null, null, null, null, null, null, "Item", null, null, false, true, null, null);
				}
				else if (randomElement == 4)
				{
					text6 = NameMaker.MakeName(InfluencedBy, null, null, null, null, null, null, null, null, null, "Item", null, null, false, true, null, null);
				}
				flag4 = true;
			}
			return new bool?(ItemNaming.NameItem(Object, Owner, text6, text7, text2, text, flag4, CanBestow, Kill, InfluencedBy, OpportunityType, flag, num, flag2));
		}

		// Token: 0x06006C6C RID: 27756 RVA: 0x00232574 File Offset: 0x00230774
		public static bool NameItem(GameObject Object, GameObject Owner, string Name, string Color = null, string Element = null, string Type = null, bool UseAnsify = false, bool CanBestow = false, GameObject Kill = null, GameObject InfluencedBy = null, string OpportunityType = "General", bool BestowalsChecked = false, int DidBasicBestowals = 0, bool DidElementBestowal = false)
		{
			if (!ItemNaming.CanBeNamed(Object, Owner))
			{
				return false;
			}
			string text;
			QudHistoryHelpers.ExtractArticle(ref Name, out text);
			if (UseAnsify)
			{
				Name = QudHistoryHelpers.Ansify(Name, null);
			}
			else
			{
				Name = Name.Color(Color);
			}
			string displayName = Object.GetDisplayName(int.MaxValue, null, null, false, false, false, false, true, false, true, false, false, true, false, false, false, null, false, false, false, false, null, false, null, false, true);
			string text2 = Object.an(int.MaxValue, null, null, false, false, false, false, true, false, true, false, false, true, false, null, false, null, false);
			if (Owner.IsPlayer())
			{
				Popup.Show(string.Concat(new string[]
				{
					"You name ",
					Object.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
					" '",
					Name,
					"'."
				}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			Object.RequirePart<OriginalItemType>(false);
			Object.SplitStack(1, Owner, false);
			Object.GiveProperName(Name, true, null, false, null, null, null, null);
			Object.Render.SetForegroundColor(ColorUtility.GetMainForegroundColor(Name));
			Object.SetImportant(true, true, false);
			if (!text.IsNullOrEmpty())
			{
				Object.SetStringProperty("IndefiniteArticle", text, false);
				Object.SetStringProperty("DefiniteArticle", text, false);
			}
			Object.SetIntProperty("Renamed", 1, false);
			int num = Owner.Stat("Level", 0);
			int intProperty = Owner.GetIntProperty("LastItemNamingDoneAtLevel", 0);
			int intProperty2 = Owner.GetIntProperty("Last" + OpportunityType + "ItemNamingDoneAtLevel", 0);
			Owner.ModIntProperty("ItemNamingDone", 1, false);
			if (Owner.IsPlayer())
			{
				The.Game.ModIntGameState("PlayerItemNamingDone", 1);
			}
			Owner.SetIntProperty("LastItemNamingDoneAtLevel", num, false);
			Owner.SetIntProperty("Last" + OpportunityType + "ItemNamingDoneAtLevel", num, false);
			if (intProperty == num)
			{
				Owner.ModIntProperty("ItemNamingDoneAtLevel", 1, false);
			}
			else
			{
				Owner.SetIntProperty("ItemNamingDoneAtLevel", 1, false);
			}
			if (intProperty2 == num || intProperty2 < intProperty)
			{
				Owner.ModIntProperty(OpportunityType + "ItemNamingDoneAtLevel", 1, false);
			}
			else
			{
				Owner.SetIntProperty(OpportunityType + "ItemNamingDoneAtLevel", 1, false);
			}
			if (CanBestow && !BestowalsChecked)
			{
				bool flag;
				ItemNaming.CheckBestowals(Owner, Object, Type, Element, Kill, InfluencedBy, OpportunityType, out flag, out DidBasicBestowals, out DidElementBestowal);
			}
			if (Owner.IsPlayer())
			{
				string text3 = HistoricStringExpander.ExpandString("<spice.professions.!random>", null);
				string text4 = string.Concat(new string[] { "You named ", text2, " '", Name, "'." });
				string text5 = string.Concat(new string[] { "Blessed by divine beings, =name= discovered the legendary ", displayName, " known as '", Name, "'." });
				MuralWeight muralWeight = ((DidElementBestowal || DidBasicBestowals > 0) ? MuralWeight.High : MuralWeight.Medium);
				JournalAPI.AddAccomplishment(text4, text5, string.Concat(new string[]
				{
					"At a remote <spice.professions.",
					text3,
					".guildhall> near ",
					JournalAPI.GetLandmarkNearestPlayer().Text,
					", =name= met with a group of <spice.professions.",
					text3,
					".plural> and commissed ",
					text2,
					" named ",
					Name,
					"."
				}), null, "general", MuralCategory.DoesSomethingRad, muralWeight, null, -1L, true);
			}
			return true;
		}

		// Token: 0x06006C6D RID: 27757 RVA: 0x002328D4 File Offset: 0x00230AD4
		private static void CheckBestowals(GameObject Owner, GameObject Object, string Type, string Element, GameObject Kill, GameObject InfluencedBy, string OpportunityType, out bool BestowalsPossible, out int DidBasicBestowals, out bool DidElementBestowal)
		{
			BestowalsPossible = false;
			DidBasicBestowals = 0;
			DidElementBestowal = false;
			int num = 0;
			bool flag = false;
			if (Options.SifrahItemNaming && Owner.IsPlayer())
			{
				BestowalsPossible = true;
				int num2 = Owner.StatMod("Ego", 0) + Owner.StatMod("Willpower", 0);
				int num3 = Tier.Constrain(Object.GetTier());
				ItemNamingSifrah itemNamingSifrah = new ItemNamingSifrah(Object, num2, num3);
				itemNamingSifrah.Play(Object);
				num = itemNamingSifrah.BasicBestowals;
				flag = itemNamingSifrah.ElementBestowal;
			}
			else
			{
				int @for = GetNamingBestowalChanceEvent.GetFor(Owner, Object, GlobalConfig.GetIntSetting("ItemNamingBestowalBaseChance", 0), 0, 0);
				if (@for > 0)
				{
					BestowalsPossible = true;
				}
				if (Owner.IsPlayer() ? @for.in100(Stat.NamingRnd) : @for.in100())
				{
					num++;
					if (Owner.IsPlayer() ? @for.in100(Stat.NamingRnd) : @for.in100())
					{
						flag = true;
					}
				}
			}
			int num4 = -1;
			string text = null;
			if (num > 0 || flag)
			{
				if (Type == null)
				{
					Type = RelicGenerator.GetType(Object);
				}
				if (text == null)
				{
					text = RelicGenerator.GetSubtype(Type);
				}
				if (num4 == -1)
				{
					int num5 = Owner.Stat("Level", 0);
					num4 = Tier.Constrain(Stat.Random((num5 >= 20) ? 2 : 1, 1 + num5 / 5));
				}
				if (Type != null && text != null)
				{
					BodyPart bodyPart = null;
					GameObject gameObject = Object.Equipped;
					if (Object.Implantee != null)
					{
						gameObject = null;
					}
					if (gameObject != null)
					{
						bodyPart = gameObject.FindEquippedObject(Object);
					}
					bool flag2 = false;
					if (gameObject != null)
					{
						Event @event = Event.New("CommandUnequipObject", 0, 0, 0);
						@event.SetParameter("BodyPart", bodyPart);
						@event.SetFlag("NoStack", true);
						flag2 = gameObject.FireEvent(@event);
					}
					try
					{
						for (int i = 0; i < num; i++)
						{
							if (RelicGenerator.ApplyBasicBestowal(Object, Type, num4, text, false, true))
							{
								DidBasicBestowals++;
							}
						}
						if (DidBasicBestowals == 0)
						{
							flag = true;
						}
						if (flag)
						{
							if (Element == null)
							{
								Element = RelicGenerator.SelectElement(Object, Owner, Kill, InfluencedBy);
							}
							if (!Element.IsNullOrEmpty())
							{
								DidElementBestowal = RelicGenerator.ApplyElementBestowal(Object, Element, Type, num4, text);
							}
						}
						if ((DidBasicBestowals > 0) | DidElementBestowal)
						{
							if (DidElementBestowal)
							{
								Object.SetStringProperty("Mods", "None", false);
							}
							if (Owner.IsPlayer())
							{
								Popup.Show(Object.Does("seem", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " to have taken on new qualities.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
								InventoryActionEvent.Check(Object, Owner, Object, "Look", false, false, false, false, false, 0, 0, 0, null, null, null, null);
							}
							AfterPseudoRelicGeneratedEvent.Send(Object, Element, Type, text, num4);
						}
					}
					finally
					{
						if (GameObject.Validate(ref Object) && (bodyPart != null && bodyPart.Equipped == null && flag2) && GameObject.Validate(ref gameObject))
						{
							gameObject.FireEvent(Event.New("CommandEquipObject", "Object", Object, "BodyPart", bodyPart));
						}
						if (Object.Equipped == null && Object.InInventory == null && Object.Implantee == null && Object.CurrentCell == null)
						{
							Owner.ReceiveObject(Object, false, null);
						}
					}
				}
			}
		}

		// Token: 0x06006C6E RID: 27758 RVA: 0x00232BE0 File Offset: 0x00230DE0
		public static GameObject GetBaseVersion(GameObject Object)
		{
			if (Object.GetIntProperty("Renamed", 0) > 0 && !Object.IsCombatObject())
			{
				return GameObject.Create(Object.Blueprint);
			}
			return null;
		}

		// Token: 0x06006C6F RID: 27759 RVA: 0x00232C08 File Offset: 0x00230E08
		[WishCommand(null, null, Regex = "^itemnaming(?::([^:]*?)\\s*(?::\\s*([^:]*?))?\\s*)?$")]
		public static bool HandleItemNamingWish(Match match)
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			GameObject gameObject = null;
			GameObject gameObject2 = null;
			string value = match.Groups[1].Value;
			string value2 = match.Groups[2].Value;
			if (!value.IsNullOrEmpty())
			{
				gameObject = GameObject.Create(value);
				stringBuilder.Append("[Debug: Created " + gameObject.DebugName + " as kill.]\n");
			}
			if (!value2.IsNullOrEmpty())
			{
				gameObject2 = GameObject.Create(value2);
				stringBuilder.Append("[Debug: Created " + gameObject2.DebugName + " as InfluencedBy.]\n");
			}
			if (stringBuilder.Length > 0)
			{
				Popup.Show(stringBuilder.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			if (!ItemNaming.Opportunity(The.Player, gameObject, gameObject2, null, "Wish", 0, 0, 0, 0, true))
			{
				Popup.Show("[Debug: Naming failed.]", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			return true;
		}

		// Token: 0x0400319D RID: 12701
		private const int CHOICE_ENTERED = 0;

		// Token: 0x0400319E RID: 12702
		private const int CHOICE_RELIC_STYLE = 1;

		// Token: 0x0400319F RID: 12703
		private const int CHOICE_OWN_CULTURE = 2;

		// Token: 0x040031A0 RID: 12704
		private const int CHOICE_KILL_CULTURE = 3;

		// Token: 0x040031A1 RID: 12705
		private const int CHOICE_INFLUENCER_CULTURE = 4;

		// Token: 0x040031A2 RID: 12706
		public static bool Suppress;

		// Token: 0x040031A3 RID: 12707
		[NonSerialized]
		private static string[] KillBlueprintSources = new string[] { "LastKillAsMeleeWeaponBlueprint", "LastKillAsMeleeWeaponTurn", "LastKillAsLauncherBlueprint", "LastKillAsLauncherTurn", "LastKillAsProjectileBlueprint", "LastKillAsProjectileTurn", "LastKillAsThrownWeaponBlueprint", "LastKillAsThrownWeaponTurn" };
	}
}
