using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleLib.Console;
using Genkit;
using Qud.API;
using XRL.Wish;
using XRL.World.Parts;
using XRL.World.Units;

namespace XRL.World.Quests.GolemQuest
{
	// Token: 0x02000B30 RID: 2864
	[Serializable]
	public class GolemIncantationSelection : GolemMaterialSelection<JournalAccomplishment, MuralCategory>
	{
		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x06007363 RID: 29539 RVA: 0x002B90B2 File Offset: 0x002B72B2
		// (set) Token: 0x06007364 RID: 29540 RVA: 0x002B90BA File Offset: 0x002B72BA
		public override JournalAccomplishment Material
		{
			get
			{
				return this._Material;
			}
			set
			{
				this._Material = value;
			}
		}

		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x06007365 RID: 29541 RVA: 0x002B90C3 File Offset: 0x002B72C3
		public override string ID
		{
			get
			{
				return "Incantation";
			}
		}

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x06007366 RID: 29542 RVA: 0x002B90CA File Offset: 0x002B72CA
		public override string DisplayName
		{
			get
			{
				return "incantation";
			}
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x06007367 RID: 29543 RVA: 0x002B90D1 File Offset: 0x002B72D1
		public override char Key
		{
			get
			{
				return 'i';
			}
		}

		// Token: 0x06007368 RID: 29544 RVA: 0x002B90D5 File Offset: 0x002B72D5
		public override bool IsValid(JournalAccomplishment Accomplishment)
		{
			return base.IsValid(Accomplishment) && !Accomplishment.Text.IsNullOrEmpty() && Accomplishment.MuralCategory > MuralCategory.Generic;
		}

		// Token: 0x06007369 RID: 29545 RVA: 0x002B90F8 File Offset: 0x002B72F8
		public override List<JournalAccomplishment> GetValidMaterials()
		{
			return JournalAPI.Accomplishments.Where<JournalAccomplishment>(new Func<JournalAccomplishment, bool>(this.IsValid)).ToList<JournalAccomplishment>();
		}

		// Token: 0x0600736A RID: 29546 RVA: 0x002B9116 File Offset: 0x002B7316
		public override string GetNameFor(JournalAccomplishment Material)
		{
			return Material.Text;
		}

		// Token: 0x0600736B RID: 29547 RVA: 0x002B9120 File Offset: 0x002B7320
		public override IRenderable GetIconFor(JournalAccomplishment Material)
		{
			Random random = new Random((int)Hash.FNV1A32(Material.Text, 2166136261U));
			Renderable renderable = new Renderable();
			renderable.ColorString = "&Y";
			Renderable renderable2 = renderable;
			string text;
			switch (random.Next(4))
			{
			case 0:
				text = "items/sw_unfurled_scroll1.bmp";
				break;
			case 1:
				text = "items/sw_unfurled_scroll2.bmp";
				break;
			case 2:
				text = "items/sw_scroll1.bmp";
				break;
			default:
				text = "items/sw_scroll2.bmp";
				break;
			}
			renderable2.Tile = text;
			renderable.TileColor = "&Y";
			do
			{
				renderable.DetailColor = Crayons.GetRandomColorAll(random)[0];
			}
			while (renderable.DetailColor == renderable.TileColor[1]);
			return renderable;
		}

		// Token: 0x0600736C RID: 29548 RVA: 0x002B91CB File Offset: 0x002B73CB
		public override IEnumerable<GameObjectUnit> YieldEffectsOf(JournalAccomplishment Material)
		{
			GolemIncantationSelection.<YieldEffectsOf>d__15 <YieldEffectsOf>d__ = new GolemIncantationSelection.<YieldEffectsOf>d__15(-2);
			<YieldEffectsOf>d__.<>3__Material = Material;
			return <YieldEffectsOf>d__;
		}

		// Token: 0x0600736D RID: 29549 RVA: 0x002B91DB File Offset: 0x002B73DB
		[WishCommand("golemquest:incantation", null)]
		private static void Wish()
		{
			GolemIncantationSelection.CreateAccomplishments();
		}

		// Token: 0x0600736E RID: 29550 RVA: 0x002B91E4 File Offset: 0x002B73E4
		public static void CreateAccomplishments()
		{
			List<MuralCategory> list = GolemMaterialSelection<JournalAccomplishment, MuralCategory>.Units.Keys.ToList<MuralCategory>();
			foreach (JournalAccomplishment journalAccomplishment in JournalAPI.Accomplishments)
			{
				list.Remove(journalAccomplishment.MuralCategory);
			}
			foreach (MuralCategory muralCategory in list)
			{
				JournalAPI.AddAccomplishment(GameText.GenerateMarkovMessageSentence(), "", null, null, "general", muralCategory, MuralWeight.Nil, null, -1L, true);
			}
		}

		// Token: 0x0600736F RID: 29551 RVA: 0x002B92A0 File Offset: 0x002B74A0
		static GolemIncantationSelection()
		{
			GolemMaterialSelection<JournalAccomplishment, MuralCategory>.Units = new Dictionary<MuralCategory, GolemMaterialSelection<JournalAccomplishment, MuralCategory>.UnitGenerator>
			{
				{
					MuralCategory.IsBorn,
					new GolemMaterialSelection<JournalAccomplishment, MuralCategory>.UnitGenerator(GolemIncantationSelection.UnitIsBorn)
				},
				{
					MuralCategory.HasInspiringExperience,
					new GolemMaterialSelection<JournalAccomplishment, MuralCategory>.UnitGenerator(GolemIncantationSelection.UnitLevelsSix)
				},
				{
					MuralCategory.CommitsFolly,
					new GolemMaterialSelection<JournalAccomplishment, MuralCategory>.UnitGenerator(GolemIncantationSelection.UnitLevelsSix)
				},
				{
					MuralCategory.EnduresHardship,
					new GolemMaterialSelection<JournalAccomplishment, MuralCategory>.UnitGenerator(GolemIncantationSelection.UnitLevelsSix)
				},
				{
					MuralCategory.Treats,
					new GolemMaterialSelection<JournalAccomplishment, MuralCategory>.UnitGenerator(GolemIncantationSelection.UnitTreats)
				},
				{
					MuralCategory.Trysts,
					new GolemMaterialSelection<JournalAccomplishment, MuralCategory>.UnitGenerator(GolemIncantationSelection.UnitTrysts)
				},
				{
					MuralCategory.CreatesSomething,
					new GolemMaterialSelection<JournalAccomplishment, MuralCategory>.UnitGenerator(GolemIncantationSelection.Unit4ArmorPieces)
				},
				{
					MuralCategory.FindsObject,
					new GolemMaterialSelection<JournalAccomplishment, MuralCategory>.UnitGenerator(GolemIncantationSelection.Unit4ArmorPieces)
				},
				{
					MuralCategory.WieldsItemInBattle,
					new GolemMaterialSelection<JournalAccomplishment, MuralCategory>.UnitGenerator(GolemIncantationSelection.Unit4ArmorPieces)
				},
				{
					MuralCategory.WeirdThingHappens,
					new GolemMaterialSelection<JournalAccomplishment, MuralCategory>.UnitGenerator(GolemIncantationSelection.UnitWeirdThingHappens)
				},
				{
					MuralCategory.BodyExperienceBad,
					new GolemMaterialSelection<JournalAccomplishment, MuralCategory>.UnitGenerator(GolemIncantationSelection.UnitRandomBodyParts)
				},
				{
					MuralCategory.BodyExperienceGood,
					new GolemMaterialSelection<JournalAccomplishment, MuralCategory>.UnitGenerator(GolemIncantationSelection.UnitRandomBodyParts)
				},
				{
					MuralCategory.BodyExperienceNeutral,
					new GolemMaterialSelection<JournalAccomplishment, MuralCategory>.UnitGenerator(GolemIncantationSelection.UnitRandomBodyParts)
				},
				{
					MuralCategory.VisitsLocation,
					new GolemMaterialSelection<JournalAccomplishment, MuralCategory>.UnitGenerator(GolemIncantationSelection.UnitVisitsLocation)
				},
				{
					MuralCategory.DoesBureaucracy,
					new GolemMaterialSelection<JournalAccomplishment, MuralCategory>.UnitGenerator(GolemIncantationSelection.UnitDoesBureaucracy)
				},
				{
					MuralCategory.LearnsSecret,
					new GolemMaterialSelection<JournalAccomplishment, MuralCategory>.UnitGenerator(GolemIncantationSelection.UnitLearnsSecret)
				},
				{
					MuralCategory.DoesSomethingRad,
					new GolemMaterialSelection<JournalAccomplishment, MuralCategory>.UnitGenerator(GolemIncantationSelection.UnitDoesSomethingRad)
				},
				{
					MuralCategory.DoesSomethingHumble,
					new GolemMaterialSelection<JournalAccomplishment, MuralCategory>.UnitGenerator(GolemIncantationSelection.UnitDoesSomethingHumble)
				},
				{
					MuralCategory.DoesSomethingDestructive,
					new GolemMaterialSelection<JournalAccomplishment, MuralCategory>.UnitGenerator(GolemIncantationSelection.UnitDoesSomethingDestructive)
				},
				{
					MuralCategory.BecomesLoved,
					new GolemMaterialSelection<JournalAccomplishment, MuralCategory>.UnitGenerator(GolemIncantationSelection.UnitAdjustingAura)
				},
				{
					MuralCategory.MeetsWithCounselors,
					new GolemMaterialSelection<JournalAccomplishment, MuralCategory>.UnitGenerator(GolemIncantationSelection.UnitAdjustingAura)
				},
				{
					MuralCategory.Slays,
					new GolemMaterialSelection<JournalAccomplishment, MuralCategory>.UnitGenerator(GolemIncantationSelection.UnitSlays)
				},
				{
					MuralCategory.Resists,
					new GolemMaterialSelection<JournalAccomplishment, MuralCategory>.UnitGenerator(GolemIncantationSelection.UnitResists)
				},
				{
					MuralCategory.AppeasesBaetyl,
					new GolemMaterialSelection<JournalAccomplishment, MuralCategory>.UnitGenerator(GolemIncantationSelection.UnitAppeasesBaetyl)
				},
				{
					MuralCategory.CrownedSultan,
					new GolemMaterialSelection<JournalAccomplishment, MuralCategory>.UnitGenerator(GolemIncantationSelection.UnitCrownedSultan)
				},
				{
					MuralCategory.Dies,
					new GolemMaterialSelection<JournalAccomplishment, MuralCategory>.UnitGenerator(GolemIncantationSelection.UnitDies)
				}
			};
		}

		// Token: 0x06007370 RID: 29552 RVA: 0x002B94B7 File Offset: 0x002B76B7
		private static IEnumerable<GameObjectUnit> UnitIsBorn(JournalAccomplishment Entry)
		{
			yield return new GameObjectAttributeUnit
			{
				Attribute = "AP",
				Value = 6
			};
			yield break;
		}

		// Token: 0x06007371 RID: 29553 RVA: 0x002B94C0 File Offset: 0x002B76C0
		private static IEnumerable<GameObjectUnit> UnitLevelsSix(JournalAccomplishment Entry)
		{
			yield return new GameObjectExperienceUnit
			{
				Levels = 6
			};
			yield break;
		}

		// Token: 0x06007372 RID: 29554 RVA: 0x002B94C9 File Offset: 0x002B76C9
		private static IEnumerable<GameObjectUnit> UnitTreats(JournalAccomplishment Entry)
		{
			yield return new GameObjectPartUnit
			{
				Part = new VehiclePairBonus
				{
					Stat = "Speed",
					Unpaired = 15
				}
			};
			yield break;
		}

		// Token: 0x06007373 RID: 29555 RVA: 0x002B94D2 File Offset: 0x002B76D2
		private static IEnumerable<GameObjectUnit> UnitTrysts(JournalAccomplishment Entry)
		{
			yield return new GameObjectPartUnit
			{
				Part = new VehiclePairBonus
				{
					Stat = "Speed",
					Paired = 15
				}
			};
			yield break;
		}

		// Token: 0x06007374 RID: 29556 RVA: 0x002B94DB File Offset: 0x002B76DB
		private static IEnumerable<GameObjectUnit> UnitRelicHigh(JournalAccomplishment Entry)
		{
			yield return new GameObjectRelicUnit
			{
				Tier = "7-8"
			};
			yield break;
		}

		// Token: 0x06007375 RID: 29557 RVA: 0x002B94E4 File Offset: 0x002B76E4
		private static IEnumerable<GameObjectUnit> Unit4ArmorPieces(JournalAccomplishment Entry)
		{
			yield return new GameObjectTieredArmorUnit
			{
				Tier = "4-7",
				Amount = 4,
				Gigantic = true,
				Equippable = true
			};
			yield break;
		}

		// Token: 0x06007376 RID: 29558 RVA: 0x002B94ED File Offset: 0x002B76ED
		private static IEnumerable<GameObjectUnit> UnitWeirdThingHappens(JournalAccomplishment Entry)
		{
			yield return new GameObjectGolemQuestRandomUnit
			{
				SelectionID = "Incantation",
				Amount = 2
			};
			yield break;
		}

		// Token: 0x06007377 RID: 29559 RVA: 0x002B94F6 File Offset: 0x002B76F6
		private static IEnumerable<GameObjectUnit> UnitRandomBodyParts(JournalAccomplishment Entry)
		{
			GameObjectBodyPartUnit gameObjectBodyPartUnit = new GameObjectBodyPartUnit
			{
				Type = "Random",
				Manager = "Golem::Incantation",
				Metachromed = true
			};
			yield return new GameObjectUnitAggregate("3 random body parts", new GameObjectUnit[] { gameObjectBodyPartUnit, gameObjectBodyPartUnit, gameObjectBodyPartUnit });
			yield break;
		}

		// Token: 0x06007378 RID: 29560 RVA: 0x002B94FF File Offset: 0x002B76FF
		private static IEnumerable<GameObjectUnit> UnitVisitsLocation(JournalAccomplishment Entry)
		{
			yield return new GameObjectAttributeUnit
			{
				Attribute = "MoveSpeed",
				Value = -100
			};
			yield break;
		}

		// Token: 0x06007379 RID: 29561 RVA: 0x002B9508 File Offset: 0x002B7708
		private static IEnumerable<GameObjectUnit> UnitDoesBureaucracy(JournalAccomplishment Entry)
		{
			yield return new GameObjectAttributeUnit
			{
				Attribute = "Intelligence",
				Value = 5
			};
			yield return new GameObjectAttributeUnit
			{
				Attribute = "Willpower",
				Value = 5
			};
			yield return new GameObjectAttributeUnit
			{
				Attribute = "Ego",
				Value = 5
			};
			yield break;
		}

		// Token: 0x0600737A RID: 29562 RVA: 0x002B9511 File Offset: 0x002B7711
		private static IEnumerable<GameObjectUnit> UnitLearnsSecret(JournalAccomplishment Entry)
		{
			yield return new GameObjectSecretUnit
			{
				Amount = 20
			};
			yield break;
		}

		// Token: 0x0600737B RID: 29563 RVA: 0x002B951A File Offset: 0x002B771A
		private static IEnumerable<GameObjectUnit> UnitDoesSomethingRad(JournalAccomplishment Entry)
		{
			yield return new GameObjectAttributeUnit
			{
				Attribute = "Ego",
				Value = 10
			};
			yield break;
		}

		// Token: 0x0600737C RID: 29564 RVA: 0x002B9523 File Offset: 0x002B7723
		private static IEnumerable<GameObjectUnit> UnitDoesSomethingHumble(JournalAccomplishment Entry)
		{
			yield return new GameObjectAttributeUnit
			{
				Attribute = "Ego",
				Value = -10
			};
			yield return new GameObjectExperienceUnit
			{
				Levels = 12
			};
			yield break;
		}

		// Token: 0x0600737D RID: 29565 RVA: 0x002B952C File Offset: 0x002B772C
		private static IEnumerable<GameObjectUnit> UnitDoesSomethingDestructive(JournalAccomplishment Entry)
		{
			yield return new GameObjectPartUnit
			{
				Part = new DoubleEdge
				{
					Received = 10,
					Dealt = 20
				}
			};
			yield break;
		}

		// Token: 0x0600737E RID: 29566 RVA: 0x002B9535 File Offset: 0x002B7735
		private static IEnumerable<GameObjectUnit> UnitAdjustingAura(JournalAccomplishment Entry)
		{
			yield return new GameObjectPartUnit
			{
				Description = "+5 quickness, +20% HP, and +3 to all attributes for nearby followers",
				Part = new AdjustingAura("&Cmotivated|Stat:Speed:5|Mult:Hitpoints:20|Stat:Strength:3|Stat:Agility:3|Stat:Toughness:3|Stat:Intelligence:3|Stat:Willpower:3|Stat:Ego:3")
			};
			yield break;
		}

		// Token: 0x0600737F RID: 29567 RVA: 0x002B953E File Offset: 0x002B773E
		private static IEnumerable<GameObjectUnit> UnitSlays(JournalAccomplishment Entry)
		{
			yield return new GameObjectPartUnit
			{
				Part = new CriticalThreshold
				{
					Value = 2
				}
			};
			yield break;
		}

		// Token: 0x06007380 RID: 29568 RVA: 0x002B9547 File Offset: 0x002B7747
		private static IEnumerable<GameObjectUnit> UnitResists(JournalAccomplishment Entry)
		{
			yield return new GameObjectUnitAggregate("+75 to all resists", new GameObjectUnit[]
			{
				new GameObjectAttributeUnit
				{
					Attribute = "HeatResistance",
					Value = 75
				},
				new GameObjectAttributeUnit
				{
					Attribute = "ColdResistance",
					Value = 75
				},
				new GameObjectAttributeUnit
				{
					Attribute = "ElectricResistance",
					Value = 75
				},
				new GameObjectAttributeUnit
				{
					Attribute = "AcidResistance",
					Value = 75
				}
			});
			yield break;
		}

		// Token: 0x06007381 RID: 29569 RVA: 0x002B9550 File Offset: 0x002B7750
		private static IEnumerable<GameObjectUnit> UnitAppeasesBaetyl(JournalAccomplishment Entry)
		{
			yield return new GameObjectBaetylUnit
			{
				Amount = "3",
				Tier = "7-8",
				Delay = true
			};
			yield break;
		}

		// Token: 0x06007382 RID: 29570 RVA: 0x002B9559 File Offset: 0x002B7759
		private static IEnumerable<GameObjectUnit> UnitCrownedSultan(JournalAccomplishment Entry)
		{
			yield return new GameObjectPartUnit
			{
				Part = new LeaderShiftShare
				{
					RequiresPart = "SultanMask"
				},
				Description = "Share effects of worn sultan mask"
			};
			yield break;
		}

		// Token: 0x06007383 RID: 29571 RVA: 0x002B9562 File Offset: 0x002B7762
		private static IEnumerable<GameObjectUnit> UnitDies(JournalAccomplishment Entry)
		{
			yield return new GameObjectPartUnit
			{
				Part = new RestoreOnDeath
				{
					Amount = 1,
					Health = 50
				}
			};
			yield break;
		}

		// Token: 0x0400353D RID: 13629
		public const string BASE_ID = "Incantation";

		// Token: 0x0400353E RID: 13630
		public JournalAccomplishment _Material;
	}
}
