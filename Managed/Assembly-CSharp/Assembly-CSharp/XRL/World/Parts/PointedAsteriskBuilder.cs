using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HistoryKit;
using Qud.API;
using XRL.Language;
using XRL.Rules;
using XRL.Wish;

namespace XRL.World.Parts
{
	// Token: 0x0200107D RID: 4221
	[HasWishCommand]
	[Serializable]
	public class PointedAsteriskBuilder : IPart
	{
		// Token: 0x0600A7F8 RID: 43000 RVA: 0x003D678B File Offset: 0x003D498B
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AfterObjectCreatedEvent.ID;
		}

		// Token: 0x0600A7F9 RID: 43001 RVA: 0x003D67A4 File Offset: 0x003D49A4
		public override bool HandleEvent(AfterObjectCreatedEvent E)
		{
			Quest quest;
			if (The.Game.TryGetQuest("Landing Pads", out quest) && E.Context != "Wish" && E.ReplacementObject == null)
			{
				string property = quest.GetProperty("Faction", "");
				int num = Math.Min(quest.GetProperty("Count", 0), 10);
				if (!property.IsNullOrEmpty() && num > 0)
				{
					PointedAsteriskBuilder.BuildAsterisk(this.ParentObject, property, num);
					GameUnique gameUnique = new GameUnique
					{
						Replace = false
					};
					this.ParentObject.AddPart<GameUnique>(gameUnique, true, false);
					gameUnique.OnCreated(E.Context);
					JournalAPI.AddAccomplishment("Thah gifted you a miraculous sculpture for helping the slynth.", "In a chivalric rite of thanks to their creator, the first slynth transformed into a star and gifted itself to =name=.", string.Concat(new string[]
					{
						"Atop an overgrown recoming pad deep in the wilds of the Palladium Reef, =name= met with an artisan plant and commissioned a token that looked like an asterisk of ",
						Grammar.Cardinal(num),
						" points. They called it ",
						this.ParentObject.t(int.MaxValue, null, null, true, false, false, false, true, true, true, false, null, false, true, false, null, true, null, false),
						"."
					}), null, "general", MuralCategory.CreatesSomething, MuralWeight.Medium, null, -1L, true);
					if (num >= 10)
					{
						Achievement.GIFTED_10ASTERISK.Unlock();
					}
				}
			}
			this.ParentObject.RemovePart(this);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A7FA RID: 43002 RVA: 0x003D68EC File Offset: 0x003D4AEC
		public static GameObject BuildAsterisk(GameObject Object, string Faction, int Points)
		{
			HistoricEntitySnapshot villageSnapshot = HistoryAPI.GetVillageSnapshot(Faction);
			PointedAsteriskBuilder.FactionEffect randomElement;
			PointedAsteriskBuilder.FactionData factionData;
			if (PointedAsteriskBuilder.FactionEffects.TryGetValue(Faction.ToUpper(), out randomElement))
			{
				factionData = randomElement(Object, Points);
			}
			else
			{
				randomElement = PointedAsteriskBuilder.FactionEffects.Values.GetRandomElement<PointedAsteriskBuilder.FactionEffect>(null);
				randomElement(Object, Math.Min(10, Points + 1));
				PointedAsteriskBuilder.FactionData factionData2 = new PointedAsteriskBuilder.FactionData();
				factionData2.Name = ((villageSnapshot != null) ? villageSnapshot.Name : null);
				string text;
				if (villageSnapshot == null)
				{
					text = null;
				}
				else
				{
					List<string> list = villageSnapshot.GetList("palette");
					text = ((list != null) ? list.FirstOrDefault<string>() : null);
				}
				factionData2.Color = text ?? Crayons.GetRandomColor(null);
				factionData2.Postfix = HistoryAPI.ExpandVillageText("It's engraved with icons dedicated to =village.sacred=.", null, villageSnapshot);
				factionData = factionData2;
			}
			Object.DisplayName = PointedAsteriskBuilder.BuildAsteriskName(Faction, Points, factionData);
			Object.RequirePart<Commerce>(false).Value += (double)(Points * 500);
			Description description = Object.RequirePart<Description>(false);
			description._Short = description._Short + " " + factionData.Postfix;
			if (Points == 3)
			{
				Object.Render.Tile = "Items/sw_asterisk_3.bmp";
			}
			else if (Points == 4)
			{
				Object.Render.Tile = "Items/sw_asterisk_4.bmp";
			}
			else if (Points == 5)
			{
				Object.Render.Tile = "Items/sw_asterisk_5.bmp";
			}
			else
			{
				Object.Render.Tile = "Items/sw_asterisk_6plus.bmp";
			}
			Object.MakeUnderstood(false);
			return Object;
		}

		// Token: 0x0600A7FB RID: 43003 RVA: 0x003D6A40 File Offset: 0x003D4C40
		[WishCommand(null, null, Command = "slynthasterisk")]
		public static void AsteriskWish()
		{
			GameObject gameObject = GameObjectFactory.Factory.CreateObject("n-Pointed Asterisk", 0, 0, null, null, null, "Wish", null);
			foreach (PointedAsteriskBuilder.FactionEffect factionEffect in PointedAsteriskBuilder.FactionEffects.Values)
			{
				factionEffect(gameObject, 10);
			}
			gameObject.DisplayName = "{{paisley|The 10-Pointed Asterisk of the Ensemble}}";
			gameObject.MakeUnderstood(false);
			The.Player.ReceiveObject(gameObject, false, null);
		}

		// Token: 0x0600A7FC RID: 43004 RVA: 0x003D6AD4 File Offset: 0x003D4CD4
		[WishCommand(null, null, Command = "slynthasterisk")]
		public static void AsteriskWish(string Value)
		{
			string text = Value;
			int num = 3;
			if (Value.Contains(':'))
			{
				string[] array = Value.Split(':', StringSplitOptions.None);
				text = array[0];
				if (!int.TryParse(array[1], out num))
				{
					num = Stat.Roll(3, 10);
				}
			}
			else
			{
				num = Stat.Roll(3, 10);
			}
			GameObject gameObject = GameObjectFactory.Factory.CreateObject("n-Pointed Asterisk", 0, 0, null, null, null, "Wish", null);
			The.Player.ReceiveObject(PointedAsteriskBuilder.BuildAsterisk(gameObject, text, num), false, null);
		}

		// Token: 0x0600A7FD RID: 43005 RVA: 0x003D6B4C File Offset: 0x003D4D4C
		private static string BuildAsteriskName(string Faction, int Points, PointedAsteriskBuilder.FactionData Data)
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null).Append(Points).Append("-pointed asterisk of ");
			if (Data.Name == null)
			{
				Faction faction = Factions.Loop().FirstOrDefault<Faction>((Faction x) => x.Name.EqualsNoCase(Faction));
				if (faction != null)
				{
					if (faction.FormatWithArticle)
					{
						stringBuilder.Append("the ");
					}
					stringBuilder.Append('{', 2);
					stringBuilder.Append(Data.Color);
					stringBuilder.Append("|");
					stringBuilder.Append(faction.DisplayName);
				}
				else
				{
					stringBuilder.Append('{', 2);
					stringBuilder.Append(Data.Color);
					stringBuilder.Append("|");
					stringBuilder.Append(Faction);
				}
			}
			else
			{
				if (Data.Article)
				{
					stringBuilder.Append("the ");
				}
				stringBuilder.Append('{', 2);
				stringBuilder.Append(Data.Color);
				stringBuilder.Append("|");
				stringBuilder.Append(Data.Name);
			}
			return stringBuilder.Append('}', 2).ToString();
		}

		// Token: 0x0600A7FE RID: 43006 RVA: 0x003D6C6C File Offset: 0x003D4E6C
		private static PointedAsteriskBuilder.FactionData AddJoppaEffect(GameObject Object, int Points)
		{
			Object.AddPart<DrinkMagnifier>(new DrinkMagnifier
			{
				Chance = 4 + 2 * Points,
				Percent = 200
			}, true, false);
			return new PointedAsteriskBuilder.FactionData
			{
				Name = "Joppa",
				Color = "g",
				Postfix = "It's carved in relief with watervine crawling along each point."
			};
		}

		// Token: 0x0600A7FF RID: 43007 RVA: 0x003D6CC4 File Offset: 0x003D4EC4
		private static PointedAsteriskBuilder.FactionData AddBarathrumitesEffect(GameObject Object, int Points)
		{
			Object.AddPart<HighBitBonus>(new HighBitBonus
			{
				Chance = 2 * Points,
				Amount = 1
			}, true, false);
			return new PointedAsteriskBuilder.FactionData
			{
				Name = "Grit Gate",
				Color = "c",
				Postfix = "It's embossed with angular lines meant to evoke schematics and circuitry."
			};
		}

		// Token: 0x0600A800 RID: 43008 RVA: 0x003D6D18 File Offset: 0x003D4F18
		private static PointedAsteriskBuilder.FactionData AddHindrenEffect(GameObject Object, int Points)
		{
			Object.AddPart<AilingQuickness>(new AilingQuickness
			{
				SpeedBonus = 5 + 2 * Points
			}, true, false);
			return new PointedAsteriskBuilder.FactionData
			{
				Name = "Bey Lah",
				Color = "beylah",
				Postfix = "It's engraved with cervine hoofprints leading from the spokes to the center."
			};
		}

		// Token: 0x0600A801 RID: 43009 RVA: 0x003D6D64 File Offset: 0x003D4F64
		private static PointedAsteriskBuilder.FactionData AddKyakukyaEffect(GameObject Object, int Points)
		{
			Object.AddPart<AddsRep>(new AddsRep
			{
				Faction = "Fungi",
				Value = 200,
				WorksOnCarrier = true
			}, true, false);
			int num = Points / 3;
			Object.AddPart<FungalFortitude>(new FungalFortitude
			{
				AVBonus = num,
				ResistBonus = 4 * num
			}, true, false);
			return new PointedAsteriskBuilder.FactionData
			{
				Name = "Kyakukya",
				Color = "r",
				Postfix = "It's etched with tiny mushrooms all over."
			};
		}

		// Token: 0x0600A802 RID: 43010 RVA: 0x003D6DE4 File Offset: 0x003D4FE4
		private static PointedAsteriskBuilder.FactionData AddEzraEffect(GameObject Object, int Points)
		{
			Object.AddPart<AddsRep>(new AddsRep
			{
				Faction = "*alloldfactions",
				Value = 10 + 10 * Points,
				WorksOnCarrier = true
			}, true, false);
			return new PointedAsteriskBuilder.FactionData
			{
				Name = "Ezra",
				Color = "W",
				Postfix = "It's carved in relief with various types of musa."
			};
		}

		// Token: 0x0600A803 RID: 43011 RVA: 0x003D6E44 File Offset: 0x003D5044
		private static PointedAsteriskBuilder.FactionData AddYdFreeholdEffect(GameObject Object, int Points)
		{
			Object.AddPart<WaterRitualDiscount>(new WaterRitualDiscount
			{
				Types = "Join",
				Percent = 17 + 3 * Points
			}, true, false);
			return new PointedAsteriskBuilder.FactionData
			{
				Name = "Yd Freehold",
				Color = "ydfreehold",
				Postfix = "It's carved in relief with figures of svardym hopping from the spokes toward the hub.",
				Article = true
			};
		}

		// Token: 0x0600A804 RID: 43012 RVA: 0x003D6EA4 File Offset: 0x003D50A4
		private static PointedAsteriskBuilder.FactionData AddMechanimistsEffect(GameObject Object, int Points)
		{
			Object.AddPart<CompanionCapacity>(new CompanionCapacity
			{
				Proselytized = ((Points >= 3) ? 1 : 0),
				Beguiled = ((Points >= 7) ? 1 : 0)
			}, true, false);
			return new PointedAsteriskBuilder.FactionData
			{
				Name = "Six Day Stilt",
				Color = "C",
				Postfix = "It's engraved with a preacher, hand outstretched, on each spoke.",
				Article = true
			};
		}

		// Token: 0x0600A805 RID: 43013 RVA: 0x003D6F08 File Offset: 0x003D5108
		private static PointedAsteriskBuilder.FactionData AddMopangoEffect(GameObject Object, int Points)
		{
			Object.AddPart<ArtifactDetection>(new ArtifactDetection
			{
				Radius = ((Points >= 10) ? 80 : (2 * (Points - 1) + 1)),
				IdentifyChance = 0
			}, true, false);
			return new PointedAsteriskBuilder.FactionData
			{
				Color = "Y",
				Postfix = "It's embossed with starbursts along each spoke and a circular rondure around the hub."
			};
		}

		// Token: 0x0600A806 RID: 43014 RVA: 0x003D6F5C File Offset: 0x003D515C
		private static PointedAsteriskBuilder.FactionData AddPariahsEffect(GameObject Object, int Points)
		{
			Object.AddPart<FeelingOnTarget>(new FeelingOnTarget
			{
				Chance = Points,
				Feeling = 25,
				CalmHate = false
			}, true, false);
			return new PointedAsteriskBuilder.FactionData
			{
				Color = "m",
				Postfix = "It's engraved with winding roads, a single lone figure walking them."
			};
		}

		// Token: 0x0600A807 RID: 43015 RVA: 0x003D6FA8 File Offset: 0x003D51A8
		private static PointedAsteriskBuilder.FactionData AddChavvahEffect(GameObject Object, int Points)
		{
			Object.AddPart<AdjustingAura>(new AdjustingAura
			{
				AdjustSpec = string.Format("&mtouchkept|suppress|Stat:Willpower:{0}", 3 + (Points - 3) / 2),
				Radius = 9999999,
				AdjustSubject = true,
				WorksOnSelf = false,
				WorksOnHolder = true,
				WorksOnCarrier = true
			}, true, false);
			return new PointedAsteriskBuilder.FactionData
			{
				Name = "Chavvah",
				Color = "m",
				Postfix = "It's engraved with crystalline roots entwining each spoke."
			};
		}

		// Token: 0x04004559 RID: 17753
		private static readonly Dictionary<string, PointedAsteriskBuilder.FactionEffect> FactionEffects = new Dictionary<string, PointedAsteriskBuilder.FactionEffect>
		{
			{
				"JOPPA",
				new PointedAsteriskBuilder.FactionEffect(PointedAsteriskBuilder.AddJoppaEffect)
			},
			{
				"BARATHRUMITES",
				new PointedAsteriskBuilder.FactionEffect(PointedAsteriskBuilder.AddBarathrumitesEffect)
			},
			{
				"HINDREN",
				new PointedAsteriskBuilder.FactionEffect(PointedAsteriskBuilder.AddHindrenEffect)
			},
			{
				"KYAKUKYA",
				new PointedAsteriskBuilder.FactionEffect(PointedAsteriskBuilder.AddKyakukyaEffect)
			},
			{
				"EZRA",
				new PointedAsteriskBuilder.FactionEffect(PointedAsteriskBuilder.AddEzraEffect)
			},
			{
				"YDFREEHOLD",
				new PointedAsteriskBuilder.FactionEffect(PointedAsteriskBuilder.AddYdFreeholdEffect)
			},
			{
				"MECHANIMISTS",
				new PointedAsteriskBuilder.FactionEffect(PointedAsteriskBuilder.AddMechanimistsEffect)
			},
			{
				"MOPANGO",
				new PointedAsteriskBuilder.FactionEffect(PointedAsteriskBuilder.AddMopangoEffect)
			},
			{
				"PARIAHS",
				new PointedAsteriskBuilder.FactionEffect(PointedAsteriskBuilder.AddPariahsEffect)
			},
			{
				"CHAVVAH",
				new PointedAsteriskBuilder.FactionEffect(PointedAsteriskBuilder.AddChavvahEffect)
			}
		};

		// Token: 0x02001CD2 RID: 7378
		private class FactionData
		{
			// Token: 0x0400711F RID: 28959
			public string Name;

			// Token: 0x04007120 RID: 28960
			public string Color;

			// Token: 0x04007121 RID: 28961
			public string Postfix;

			// Token: 0x04007122 RID: 28962
			public bool Article;
		}

		// Token: 0x02001CD3 RID: 7379
		// (Invoke) Token: 0x0600F253 RID: 62035
		private delegate PointedAsteriskBuilder.FactionData FactionEffect(GameObject Object, int Points);
	}
}
