using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using XRL;
using XRL.Core;
using XRL.Rules;
using XRL.Wish;
using XRL.World;
using XRL.World.Anatomy;
using XRL.World.Parts;

namespace Sheeter
{
	// Token: 0x02000316 RID: 790
	[HasWishCommand]
	public class Sheeter
	{
		// Token: 0x06001CFF RID: 7423 RVA: 0x0008E600 File Offset: 0x0008C800
		public static int CalculateAttackDamage(global::XRL.World.GameObject Attacker)
		{
			int num = 0;
			Attacker.Blueprint == "Barkbiter";
			if (Attacker.Brain == null)
			{
				return 0;
			}
			Attacker.Brain.PerformReequip(false, true, false, null);
			Body body = Attacker.Body;
			if (body == null)
			{
				return 0;
			}
			global::XRL.World.GameObject PrimaryWeapon = null;
			bool PickedFromHand = false;
			body.ForeachPart(delegate(BodyPart pPart)
			{
				if ((pPart.Equipped != null || pPart.DefaultBehavior != null) && (pPart.Primary || PrimaryWeapon == null || (pPart.Type == "Hand" && !PickedFromHand)))
				{
					if (pPart.Equipped != null && pPart.Equipped.HasPart<MeleeWeapon>())
					{
						PrimaryWeapon = pPart.Equipped;
						if (pPart.Type == "Hand")
						{
							PickedFromHand = true;
							return;
						}
					}
					else if (pPart.DefaultBehavior != null && pPart.DefaultBehavior.HasPart<MeleeWeapon>())
					{
						PrimaryWeapon = pPart.DefaultBehavior;
						if (!pPart.DefaultBehavior.HasTag("UndesirableWeapon"))
						{
							PickedFromHand = true;
						}
					}
				}
			});
			if (PrimaryWeapon != null)
			{
				MeleeWeapon part = PrimaryWeapon.GetPart<MeleeWeapon>();
				if (part != null)
				{
					int num2 = Stat.RollMin(part.BaseDamage);
					int num3 = Stat.RollMax(part.BaseDamage);
					num += num2 + (num3 - num2) / 2;
				}
				ElementalDamage part2 = PrimaryWeapon.GetPart<ElementalDamage>();
				if (part2 != null)
				{
					int num4 = Stat.RollMin(part2.Damage);
					int num5 = Stat.RollMax(part2.Damage);
					num += (num4 + (num5 - num4) / 2) * part2.Chance / 100;
				}
			}
			int SecondaryChance = GlobalConfig.GetIntSetting("BaseSecondaryAttackChance", 15);
			global::XRL.World.Event E = new global::XRL.World.Event("CommandAttack");
			if (Attacker.HasSkill("Dual_Wield_Offhand_Strikes"))
			{
				SecondaryChance = GlobalConfig.GetIntSetting("OffhandStrikesSecondaryAttackChance", 35);
			}
			if (Attacker.HasSkill("Dual_Wield_Ambidexterity"))
			{
				SecondaryChance = GlobalConfig.GetIntSetting("AmbidexteritySecondaryAttackChance", 55);
			}
			if (Attacker.HasSkill("Dual_Wield_Two_Weapon_Fighting"))
			{
				SecondaryChance = GlobalConfig.GetIntSetting("TwoWeaponFightingSecondaryAttackChance", 75);
			}
			if (Attacker.HasProperty("AlwaysOffhand"))
			{
				SecondaryChance = 100;
			}
			if (E.HasParameter("AlwaysOffhand"))
			{
				SecondaryChance = E.GetIntParameter("AlwaysOffhand", 0);
			}
			List<global::XRL.World.GameObject> WeaponList = new List<global::XRL.World.GameObject>(8);
			body.ForeachPart(delegate(BodyPart pPart)
			{
				if (pPart.Equipped != null || pPart.DefaultBehavior != null)
				{
					global::XRL.World.GameObject gameObject2 = pPart.Equipped;
					if (gameObject2 == null || gameObject2.GetPart<MeleeWeapon>() == null)
					{
						gameObject2 = pPart.DefaultBehavior;
					}
					if (!WeaponList.Contains(gameObject2))
					{
						MeleeWeapon part5 = gameObject2.GetPart<MeleeWeapon>();
						if ((part5 != null && part5.Slot == null) || pPart.Type == null || part5.Slot.Contains(pPart.Type))
						{
							global::XRL.World.Event @event = global::XRL.World.Event.New("QueryWeaponSecondaryAttackChance", 0, 0, 0);
							@event.AddParameter("Weapon", gameObject2);
							@event.AddParameter("BodyPart", pPart);
							@event.AddParameter("Chance", SecondaryChance);
							@event.AddParameter("Properties", E.GetStringParameter("Properties", null));
							gameObject2.FireEvent(@event);
							if (Attacker != null)
							{
								@event.ID = "AttackerQueryWeaponSecondaryAttackChance";
								Attacker.FireEvent(@event);
								@event.ID = "AttackerQueryWeaponSecondaryAttackChanceMultiplier";
								Attacker.FireEvent(@event);
							}
							int i = @event.GetIntParameter("Chance", 0);
							if (Attacker.HasTag("AttackWithEverything"))
							{
								i = 100;
							}
							if (E.HasParameter("AlwaysOffhand"))
							{
								i = E.GetIntParameter("AlwaysOffhand", 0);
							}
							while (i > 0)
							{
								if (Stat.Random(1, 100) <= i)
								{
									WeaponList.Add(gameObject2);
								}
								i -= 100;
							}
						}
					}
				}
			});
			foreach (global::XRL.World.GameObject gameObject in WeaponList)
			{
				MeleeWeapon part3 = gameObject.GetPart<MeleeWeapon>();
				if (part3 != null)
				{
					int num6 = Stat.RollMin(part3.BaseDamage);
					int num7 = Stat.RollMax(part3.BaseDamage);
					num += num6 + (num7 - num6) / 2;
				}
				ElementalDamage part4 = gameObject.GetPart<ElementalDamage>();
				if (part4 != null)
				{
					int num8 = Stat.RollMin(part4.Damage);
					int num9 = Stat.RollMax(part4.Damage);
					num += (num8 + (num9 - num8) / 2) * part4.Chance / 100;
				}
			}
			return num;
		}

		// Token: 0x06001D00 RID: 7424 RVA: 0x0008E8B0 File Offset: 0x0008CAB0
		public static void FactionSheeter()
		{
			Sheet sheet = new Sheet(DataManager.SavePath("factionsheet.csv"));
			List<Faction> list = new List<Faction>();
			List<int> list2 = new List<int>();
			List<string> list3 = new List<string>();
			sheet.addColumn("Tier");
			foreach (Faction faction in Factions.Loop())
			{
				if (faction.Visible && !faction.Name.Contains("villagers"))
				{
					list.Add(faction);
					list2.Add(0);
					sheet.addColumn(faction.Name);
				}
			}
			sheet.endColumns();
			int num = 0;
			for (int i = 1; i <= 8; i++)
			{
				sheet.writeColumn(i);
				for (int j = 0; j < list.Count; j++)
				{
					foreach (GameObjectBlueprint gameObjectBlueprint in GameObjectFactory.Factory.GetFactionMembers(list[j].Name, false, true))
					{
						if (gameObjectBlueprint.Tier == i)
						{
							if (gameObjectBlueprint.HasTagOrProperty("AggregateWith"))
							{
								if (list3.Contains(gameObjectBlueprint.GetTag("AggregateWith", "")))
								{
									continue;
								}
								list3.Add(gameObjectBlueprint.GetTag("AggregateWith", ""));
							}
							num++;
						}
					}
					sheet.writeColumn(num);
					List<int> list4 = list2;
					int num2 = j;
					list4[num2] += num;
					num = 0;
				}
				sheet.endRow();
			}
			for (int k = 0; k < list.Count + 1; k++)
			{
				sheet.writeColumn("");
			}
			sheet.endRow();
			sheet.writeColumn("");
			for (int l = 0; l < list.Count; l++)
			{
				sheet.writeColumn(list2[l]);
			}
			sheet.finish();
		}

		// Token: 0x06001D01 RID: 7425 RVA: 0x0008EACC File Offset: 0x0008CCCC
		public static void MonsterSheeter()
		{
			Sheet sheet = new Sheet(DataManager.SavePath("monstersheet.csv"));
			sheet.addColumn("Name");
			sheet.addColumn("Level");
			sheet.addColumn("Tier");
			sheet.addColumn("Role");
			sheet.addStatColumn("Melee");
			sheet.addStatColumn("StrMod");
			sheet.addStatColumn("Hitpoints");
			sheet.addStatColumn("AV");
			sheet.addStatColumn("DV");
			sheet.addStatColumn("MA");
			sheet.addColumn("Elec");
			sheet.addColumn("Heat");
			sheet.addColumn("Cold");
			sheet.addColumn("Acid");
			sheet.addStatColumn("QN");
			sheet.addStatColumn("MS");
			sheet.addColumn("XP");
			sheet.endColumns();
			Sheet sheet2 = new Sheet(DataManager.SavePath("monstersheet_short.csv"));
			sheet2.addColumn("Name");
			sheet2.addColumn("Level");
			sheet2.addColumn("Tier");
			sheet2.addColumn("Role");
			sheet2.addShortStatColumn("Melee");
			sheet2.addShortStatColumn("StrMod");
			sheet2.addShortStatColumn("Hitpoints");
			sheet2.addShortStatColumn("AV");
			sheet2.addShortStatColumn("DV");
			sheet2.addShortStatColumn("MA");
			sheet2.addColumn("Elec");
			sheet2.addColumn("Heat");
			sheet2.addColumn("Cold");
			sheet2.addColumn("Acid");
			sheet2.addShortStatColumn("QN");
			sheet2.addShortStatColumn("MS");
			sheet2.addColumn("XP");
			sheet2.endColumns();
			int num = 0;
			foreach (GameObjectBlueprint gameObjectBlueprint in GameObjectFactory.Factory.BlueprintList)
			{
				global::XRL.World.Event.ResetPool(true);
				try
				{
					num++;
					if (num % 10 == 0)
					{
						Debug.Log(num.ToString() + " of " + GameObjectFactory.Factory.BlueprintList.Count.ToString() + " sheeted out...");
					}
					if (!gameObjectBlueprint.Name.Contains("Trader") && !gameObjectBlueprint.HasPart("Tier1Wares") && !gameObjectBlueprint.HasPart("Tier2Wares") && !gameObjectBlueprint.HasPart("Tier3Wares") && !gameObjectBlueprint.HasPart("Tier4Wares") && !gameObjectBlueprint.HasPart("Tier5Wares") && !gameObjectBlueprint.HasPart("Tier6Wares") && !gameObjectBlueprint.HasPart("Tier7Wares") && !gameObjectBlueprint.HasPart("Tier8Wares") && !gameObjectBlueprint.HasPart("YurlWares") && !gameObjectBlueprint.Builders.ContainsKey("DataDiskWares") && !gameObjectBlueprint.HasPart("DataDiskWares") && !gameObjectBlueprint.HasPart("GlowpadOasisWares") && !gameObjectBlueprint.HasPart("ScrapWares") && !gameObjectBlueprint.Builders.ContainsKey("Tier1Wares") && !gameObjectBlueprint.Builders.ContainsKey("Tier2Wares") && !gameObjectBlueprint.Builders.ContainsKey("Tier3Wares") && !gameObjectBlueprint.Builders.ContainsKey("Tier4Wares") && !gameObjectBlueprint.Builders.ContainsKey("Tier5Wares") && !gameObjectBlueprint.Builders.ContainsKey("Tier6Wares") && !gameObjectBlueprint.Builders.ContainsKey("Tier7Wares") && !gameObjectBlueprint.Builders.ContainsKey("Tier8Wares") && !gameObjectBlueprint.Builders.ContainsKey("YurlWares") && !gameObjectBlueprint.Builders.ContainsKey("GlowpadOasisWares") && !gameObjectBlueprint.Builders.ContainsKey("ScrapWares") && gameObjectBlueprint.HasStat("ElectricResistance") && gameObjectBlueprint.HasStat("Level") && gameObjectBlueprint.HasPart("Combat"))
					{
						global::XRL.World.Event.ResetPool(true);
						int tier = gameObjectBlueprint.Tier;
						sheet.writeColumn(gameObjectBlueprint.Name);
						sheet2.writeColumn(gameObjectBlueprint.Name);
						if (gameObjectBlueprint.HasStat("Level"))
						{
							sheet.writeColumn(gameObjectBlueprint.GetStat("Level", new Statistic()).Value);
							sheet2.writeColumn(gameObjectBlueprint.Name);
						}
						else
						{
							sheet.writeColumn("-1");
							sheet2.writeColumn("-1");
						}
						sheet.writeColumn(tier);
						sheet2.writeColumn(tier);
						string text = "unassiged";
						if (gameObjectBlueprint.Tags.ContainsKey("Role") && !string.IsNullOrEmpty(gameObjectBlueprint.Tags["Role"]))
						{
							text = gameObjectBlueprint.Tags["Role"];
						}
						if (gameObjectBlueprint.Props.ContainsKey("Role") && !string.IsNullOrEmpty(gameObjectBlueprint.Props["Role"]))
						{
							text = gameObjectBlueprint.Props["Role"];
						}
						sheet.writeColumn(text);
						sheet2.writeColumn(text);
						List<global::XRL.World.GameObject> list = new List<global::XRL.World.GameObject>();
						for (int i = 0; i < 100; i++)
						{
							list.Add(gameObjectBlueprint.createOne());
						}
						sheet.writeStats(list.Map<global::XRL.World.GameObject, int>((global::XRL.World.GameObject o) => Sheeter.CalculateAttackDamage(o)));
						sheet.writeStats(list.Map<global::XRL.World.GameObject, int>((global::XRL.World.GameObject o) => o.StatMod("Strength", 0)));
						sheet.writeStats(list.Map<global::XRL.World.GameObject, int>((global::XRL.World.GameObject o) => o.GetStatValue("Hitpoints", 0)));
						sheet.writeStats(list.Map<global::XRL.World.GameObject, int>((global::XRL.World.GameObject o) => Stats.GetCombatAV(o)));
						sheet.writeStats(list.Map<global::XRL.World.GameObject, int>((global::XRL.World.GameObject o) => Stats.GetCombatDV(o)));
						sheet.writeStats(list.Map<global::XRL.World.GameObject, int>((global::XRL.World.GameObject o) => Stats.GetCombatMA(o)));
						sheet2.writeShortStats(list.Map<global::XRL.World.GameObject, int>((global::XRL.World.GameObject o) => Sheeter.CalculateAttackDamage(o)));
						sheet2.writeShortStats(list.Map<global::XRL.World.GameObject, int>((global::XRL.World.GameObject o) => o.StatMod("Strength", 0)));
						sheet2.writeShortStats(list.Map<global::XRL.World.GameObject, int>((global::XRL.World.GameObject o) => o.GetStatValue("Hitpoints", 0)));
						sheet2.writeShortStats(list.Map<global::XRL.World.GameObject, int>((global::XRL.World.GameObject o) => Stats.GetCombatAV(o)));
						sheet2.writeShortStats(list.Map<global::XRL.World.GameObject, int>((global::XRL.World.GameObject o) => Stats.GetCombatDV(o)));
						sheet2.writeShortStats(list.Map<global::XRL.World.GameObject, int>((global::XRL.World.GameObject o) => Stats.GetCombatMA(o)));
						sheet.writeColumn(gameObjectBlueprint.GetStat("ElectricResistance", new Statistic()).Value);
						sheet.writeColumn(gameObjectBlueprint.GetStat("HeatResistance", new Statistic()).Value);
						sheet.writeColumn(gameObjectBlueprint.GetStat("ColdResistance", new Statistic()).Value);
						sheet.writeColumn(gameObjectBlueprint.GetStat("AcidResistance", new Statistic()).Value);
						sheet2.writeColumn(gameObjectBlueprint.GetStat("ElectricResistance", new Statistic()).Value);
						sheet2.writeColumn(gameObjectBlueprint.GetStat("HeatResistance", new Statistic()).Value);
						sheet2.writeColumn(gameObjectBlueprint.GetStat("ColdResistance", new Statistic()).Value);
						sheet2.writeColumn(gameObjectBlueprint.GetStat("AcidResistance", new Statistic()).Value);
						sheet.writeStats(list.Map<global::XRL.World.GameObject, int>((global::XRL.World.GameObject o) => o.GetStatValue("Speed", 0)));
						sheet.writeStats(list.Map<global::XRL.World.GameObject, int>((global::XRL.World.GameObject o) => o.GetStatValue("MoveSpeed", 0)));
						sheet2.writeShortStats(list.Map<global::XRL.World.GameObject, int>((global::XRL.World.GameObject o) => o.GetStatValue("Speed", 0)));
						sheet2.writeShortStats(list.Map<global::XRL.World.GameObject, int>((global::XRL.World.GameObject o) => o.GetStatValue("MoveSpeed", 0)));
						int num2;
						if (gameObjectBlueprint.GetStat("XPValue", new Statistic()).sValue == "*XPValue")
						{
							num2 = Convert.ToInt32(gameObjectBlueprint.GetProp("*XPValue", "0"));
						}
						else
						{
							string valueOrSValue = gameObjectBlueprint.GetStat("XPValue", new Statistic()).ValueOrSValue;
							if (gameObjectBlueprint.GetStat("XPValue", new Statistic()).sValue == "*XP")
							{
								float num3 = (float)Convert.ToInt32(gameObjectBlueprint.GetStat("Level", new Statistic()).Value);
								num3 /= 2f;
								if (text == "Minion")
								{
									num2 = (int)(num3 * 20f);
								}
								else if (text == "Leader")
								{
									num2 = (int)(num3 * 100f);
								}
								else if (text == "Hero")
								{
									num2 = (int)(num3 * 200f);
								}
								else
								{
									num2 = (int)(num3 * 50f);
								}
							}
							else if (valueOrSValue != "")
							{
								num2 = Convert.ToInt32(valueOrSValue);
							}
							else
							{
								num2 = -1;
							}
						}
						sheet.writeColumn(num2);
						sheet2.writeColumn(num2);
						sheet.endRow();
						sheet2.endRow();
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("error blueprint=" + gameObjectBlueprint.Name);
					throw ex;
				}
			}
			sheet.finish();
			sheet2.finish();
		}

		// Token: 0x06001D02 RID: 7426 RVA: 0x0008F590 File Offset: 0x0008D790
		private static void AddUnique(Dictionary<string, Sheeter.TagRecord> Records, IDictionary StringMap, string Type)
		{
			foreach (object obj in StringMap)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				string text = dictionaryEntry.Key.ToString();
				object key = dictionaryEntry.Key;
				string text2 = Type + ((key != null) ? key.ToString() : null);
				Sheeter.TagRecord tagRecord;
				if (!Records.TryGetValue(text2, out tagRecord))
				{
					string text3 = text2;
					Sheeter.TagRecord tagRecord2 = new Sheeter.TagRecord();
					tagRecord2.Tag = text;
					tagRecord2.Values = new Dictionary<string, int>();
					tagRecord2.Type = Type;
					tagRecord = tagRecord2;
					Records[text3] = tagRecord2;
				}
				tagRecord.Score++;
				if (dictionaryEntry.Value != null && (dictionaryEntry.Value is string || dictionaryEntry.Value.GetType().IsPrimitive))
				{
					string text4 = dictionaryEntry.Value.ToString();
					if (!text4.IsNullOrEmpty())
					{
						int num;
						tagRecord.Values.TryGetValue(text4, out num);
						tagRecord.Values[text4] = num + 1;
					}
				}
			}
		}

		// Token: 0x06001D03 RID: 7427 RVA: 0x0008F6B4 File Offset: 0x0008D8B4
		[WishCommand("blueprintsheeter", null)]
		public static void BlueprintSheetWish()
		{
			Sheeter.GenerateBlueprintSheet(typeof(RowBlueprint));
		}

		// Token: 0x06001D04 RID: 7428 RVA: 0x0008F6C5 File Offset: 0x0008D8C5
		[WishCommand("blueprintsheeter", null)]
		public static void BlueprintSheetWish(string Parameter)
		{
			if (Parameter.EqualsNoCase("all"))
			{
				Sheeter.GenerateAllBlueprintSheet();
				return;
			}
			Sheeter.GenerateBlueprintSheet(ModManager.ResolveType(Parameter, false, true, true));
		}

		// Token: 0x06001D05 RID: 7429 RVA: 0x0008F6E8 File Offset: 0x0008D8E8
		public static void GenerateAllBlueprintSheet()
		{
			string[] allTags = Sheeter.GetAllTags();
			int num = allTags.Length;
			List<GameObjectBlueprint> list = new List<GameObjectBlueprint>(GameObjectFactory.Factory.BlueprintList);
			list.Sort((GameObjectBlueprint a, GameObjectBlueprint b) => string.Compare(a.Name, b.Name, StringComparison.Ordinal));
			string text = XRLCore.SavePath + "/Trash/Text/";
			Directory.CreateDirectory(text);
			using (StreamWriter streamWriter = new StreamWriter(text + "AllTagBlueprintSheet.csv"))
			{
				streamWriter.Write("\"Blueprint\",\"DisplayName\"");
				for (int i = 0; i < num; i++)
				{
					streamWriter.Write(',');
					streamWriter.Write('"');
					streamWriter.Write(allTags[i].Replace("\"", "\"\""));
					streamWriter.Write('"');
				}
				foreach (GameObjectBlueprint gameObjectBlueprint in list)
				{
					streamWriter.Write("\n");
					streamWriter.Write('"');
					streamWriter.Write(gameObjectBlueprint.Name);
					streamWriter.Write('"');
					streamWriter.Write(',');
					streamWriter.Write('"');
					streamWriter.Write(gameObjectBlueprint.CachedDisplayNameStripped);
					streamWriter.Write('"');
					for (int j = 0; j < num; j++)
					{
						streamWriter.Write(",");
						streamWriter.Write('"');
						if (gameObjectBlueprint.Tags.ContainsKey(allTags[j]))
						{
							streamWriter.Write("true");
						}
						streamWriter.Write('"');
					}
				}
			}
		}

		// Token: 0x06001D06 RID: 7430 RVA: 0x0008F8AC File Offset: 0x0008DAAC
		public static string[] GetAllTags()
		{
			HashSet<string> hashSet = new HashSet<string>();
			foreach (GameObjectBlueprint gameObjectBlueprint in GameObjectFactory.Factory.BlueprintList)
			{
				foreach (KeyValuePair<string, string> keyValuePair in gameObjectBlueprint.Tags)
				{
					hashSet.Add(keyValuePair.Key);
				}
			}
			string[] array = hashSet.ToArray<string>();
			Array.Sort<string>(array, StringComparer.Ordinal);
			return array;
		}

		// Token: 0x06001D07 RID: 7431 RVA: 0x0008F95C File Offset: 0x0008DB5C
		public static void GenerateBlueprintSheet(Type Type)
		{
			Sheeter.BlueprintColumn[] sheetColumns = Sheeter.GetSheetColumns(Type);
			List<string[]> list = new List<string[]>();
			GameObjectFactory factory = GameObjectFactory.Factory;
			int num = sheetColumns.Length;
			foreach (GameObjectBlueprint gameObjectBlueprint in factory.BlueprintList)
			{
				string[] array = new string[num];
				for (int i = 0; i < num; i++)
				{
					string text = "";
					BlueprintElement[] attributes = sheetColumns[i].Attributes;
					for (int j = 0; j < attributes.Length; j++)
					{
						string from = attributes[j].GetFrom(gameObjectBlueprint);
						if (!from.IsNullOrEmpty() && (text.IsNullOrEmpty() || from != "true"))
						{
							text = from;
						}
					}
					array[i] = text;
				}
				list.Add(array);
			}
			list.Sort((string[] a, string[] b) => string.Compare(a[0], b[0], StringComparison.Ordinal));
			string text2 = XRLCore.SavePath + "/Trash/Text/";
			Directory.CreateDirectory(text2);
			using (StreamWriter streamWriter = new StreamWriter(text2 + "BlueprintSheet.csv"))
			{
				for (int k = 0; k < num; k++)
				{
					if (k != 0)
					{
						streamWriter.Write(",");
					}
					streamWriter.Write('"');
					streamWriter.Write(sheetColumns[k].Header.Replace("\"", "\"\""));
					streamWriter.Write('"');
				}
				foreach (string[] array2 in list)
				{
					streamWriter.Write("\n");
					for (int l = 0; l < num; l++)
					{
						if (l != 0)
						{
							streamWriter.Write(",");
						}
						streamWriter.Write('"');
						streamWriter.Write(array2[l].Replace("\"", "\"\""));
						streamWriter.Write('"');
					}
				}
			}
		}

		// Token: 0x06001D08 RID: 7432 RVA: 0x0008FB88 File Offset: 0x0008DD88
		public static Sheeter.BlueprintColumn[] GetSheetColumns(Type Type)
		{
			FieldInfo[] fields = Type.GetFields();
			Sheeter.BlueprintColumn[] array = new Sheeter.BlueprintColumn[fields.Length];
			for (int i = 0; i < fields.Length; i++)
			{
				array[i] = new Sheeter.BlueprintColumn
				{
					Field = fields[i],
					Attributes = fields[i].GetCustomAttributes<BlueprintElement>().ToArray<BlueprintElement>()
				};
				foreach (BlueprintElement blueprintElement in array[i].Attributes)
				{
					if (blueprintElement.Key.IsNullOrEmpty())
					{
						blueprintElement.Key = fields[i].Name;
					}
				}
			}
			return array;
		}

		// Token: 0x06001D09 RID: 7433 RVA: 0x0008FC18 File Offset: 0x0008DE18
		[WishCommand("toptags", null)]
		public static void TopTagWish()
		{
			GameObjectFactory factory = GameObjectFactory.Factory;
			Dictionary<string, Sheeter.TagRecord> dictionary = new Dictionary<string, Sheeter.TagRecord>();
			foreach (GameObjectBlueprint gameObjectBlueprint in factory.BlueprintList)
			{
				Sheeter.AddUnique(dictionary, gameObjectBlueprint.Tags, "Tag");
				Sheeter.AddUnique(dictionary, gameObjectBlueprint.Props, "Property");
				Sheeter.AddUnique(dictionary, gameObjectBlueprint.IntProps, "IntProperty");
				Sheeter.AddUnique(dictionary, gameObjectBlueprint.Parts, "Part");
				Sheeter.AddUnique(dictionary, gameObjectBlueprint.xTags, "XTag");
			}
			string text = XRLCore.SavePath + "/Trash/Text/";
			Directory.CreateDirectory(text);
			StringBuilder stringBuilder = new StringBuilder();
			List<Sheeter.TagRecord> list = new List<Sheeter.TagRecord>(dictionary.Values);
			List<ValueTuple<string, int>> list2 = new List<ValueTuple<string, int>>();
			list.Sort((Sheeter.TagRecord a, Sheeter.TagRecord b) => b.Score.CompareTo(a.Score));
			foreach (Sheeter.TagRecord tagRecord in list)
			{
				stringBuilder.Compound(tagRecord.Tag, '\n').Append(" [").Append(tagRecord.Score)
					.Append(']')
					.Append('[')
					.Append(tagRecord.Type)
					.Append(']');
				foreach (KeyValuePair<string, int> keyValuePair in tagRecord.Values)
				{
					list2.Add(new ValueTuple<string, int>(keyValuePair.Key, keyValuePair.Value));
				}
				list2.Sort((ValueTuple<string, int> a, ValueTuple<string, int> b) => b.Item2.CompareTo(a.Item2));
				foreach (ValueTuple<string, int> valueTuple in list2)
				{
					stringBuilder.Compound(" -- ", '\n').Append(valueTuple.Item1).Append(" [")
						.Append(valueTuple.Item2)
						.Append(']');
				}
				list2.Clear();
			}
			File.WriteAllText(text + "TopTags.txt", stringBuilder.ToString());
		}

		// Token: 0x02001710 RID: 5904
		private class TagRecord
		{
			// Token: 0x04005CAF RID: 23727
			public string Tag;

			// Token: 0x04005CB0 RID: 23728
			public string Type;

			// Token: 0x04005CB1 RID: 23729
			public int Score;

			// Token: 0x04005CB2 RID: 23730
			public Dictionary<string, int> Values;
		}

		// Token: 0x02001711 RID: 5905
		public class BlueprintColumn
		{
			// Token: 0x17000DE9 RID: 3561
			// (get) Token: 0x0600DBA1 RID: 56225 RVA: 0x00506C6E File Offset: 0x00504E6E
			public string Header
			{
				get
				{
					return this.Field.Name;
				}
			}

			// Token: 0x04005CB3 RID: 23731
			public FieldInfo Field;

			// Token: 0x04005CB4 RID: 23732
			public BlueprintElement[] Attributes;
		}
	}
}
