using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Qud.API;
using UnityEngine;
using XRL.UI;
using XRL.Wish;
using XRL.World.Capabilities;
using XRL.World.Parts;
using XRL.World.Tinkering;
using XRL.World.Units;

namespace XRL.World.Quests.GolemQuest
{
	// Token: 0x02000B2F RID: 2863
	[HasWishCommand]
	[Serializable]
	public class GolemHamsaSelection : GolemGameObjectSelection
	{
		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x0600732D RID: 29485 RVA: 0x002B84F2 File Offset: 0x002B66F2
		public override string ID
		{
			get
			{
				return "Hamsa";
			}
		}

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x0600732E RID: 29486 RVA: 0x002B84F9 File Offset: 0x002B66F9
		public override string DisplayName
		{
			get
			{
				return "hamsa";
			}
		}

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x0600732F RID: 29487 RVA: 0x002B8500 File Offset: 0x002B6700
		public override char Key
		{
			get
			{
				return 'h';
			}
		}

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x06007330 RID: 29488 RVA: 0x002B8504 File Offset: 0x002B6704
		public override bool IsCarried
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x06007331 RID: 29489 RVA: 0x002B8507 File Offset: 0x002B6707
		public override int Consumed
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06007332 RID: 29490 RVA: 0x002B850A File Offset: 0x002B670A
		public override bool IsValid(GameObject Object)
		{
			return base.IsValid(Object) && Object.WeightEach <= 5;
		}

		// Token: 0x06007333 RID: 29491 RVA: 0x002B8523 File Offset: 0x002B6723
		private static IEnumerable<string> GetSemanticTagsOf(GameObjectBlueprint Blueprint)
		{
			foreach (KeyValuePair<string, string> keyValuePair in Blueprint.Tags)
			{
				if (keyValuePair.Key.StartsWith("Semantic"))
				{
					yield return keyValuePair.Key.Substring(8);
				}
			}
			Dictionary<string, string>.Enumerator enumerator = default(Dictionary<string, string>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06007334 RID: 29492 RVA: 0x002B8533 File Offset: 0x002B6733
		public override IEnumerable<GameObjectUnit> YieldEffectsOf(GameObject Object)
		{
			bool yielded = false;
			foreach (string text in GolemHamsaSelection.GetSemanticTagsOf(Object.GetBlueprint(true)))
			{
				GolemMaterialSelection<GameObject, string>.UnitGenerator unitGenerator;
				if (GolemMaterialSelection<GameObject, string>.Units.TryGetValue(text, out unitGenerator))
				{
					using (IEnumerator<GameObjectUnit> numr = unitGenerator(Object).GetEnumerator())
					{
						if (!numr.MoveNext())
						{
							continue;
						}
						yielded = true;
						do
						{
							yield return numr.Current;
						}
						while (numr.MoveNext());
					}
					IEnumerator<GameObjectUnit> numr = null;
				}
			}
			IEnumerator<string> enumerator = null;
			if (!yielded)
			{
				yield return GolemHamsaSelection.UnitTrinket(Object).First<GameObjectUnit>();
			}
			yield break;
			yield break;
		}

		// Token: 0x06007335 RID: 29493 RVA: 0x002B8543 File Offset: 0x002B6743
		[WishCommand("golemquest:hamsa", null)]
		private static void Wish()
		{
			GolemHamsaSelection.ReceiveMaterials(The.Player);
		}

		// Token: 0x06007336 RID: 29494 RVA: 0x002B8550 File Offset: 0x002B6750
		public static void ReceiveMaterials(GameObject Object)
		{
			List<string> list = GolemMaterialSelection<GameObject, string>.Units.Keys.Select<string, string>((string x) => "Semantic" + x).ToList<string>();
			GameObjectFactory factory = GameObjectFactory.Factory;
			Dictionary<string, ValueTuple<GameObjectBlueprint, int>> dictionary = new Dictionary<string, ValueTuple<GameObjectBlueprint, int>>();
			List<string> list2 = new List<string>();
			foreach (GameObjectBlueprint gameObjectBlueprint in factory.BlueprintList)
			{
				int num;
				bool flag;
				if (EncountersAPI.IsEligibleForDynamicEncounters(gameObjectBlueprint) && !gameObjectBlueprint.IsNatural() && !gameObjectBlueprint.CachedDisplayNameStripped.StartsWith("[") && gameObjectBlueprint.TryGetPartParameter<int>("Physics", "Weight", out num) && num <= 20 && (!gameObjectBlueprint.TryGetPartParameter<bool>("Physics", "Takeable", out flag) || flag))
				{
					if (gameObjectBlueprint.Tags.Any<KeyValuePair<string, string>>((KeyValuePair<string, string> x) => x.Key.StartsWith("Semantic")))
					{
						if (num > 5)
						{
							GameObject gameObject = gameObjectBlueprint.createSample();
							bool flag2 = ItemModding.ModificationApplicable("ModWillowy", gameObject, null, null);
							gameObject.Pool();
							if (!flag2)
							{
								continue;
							}
						}
						list2.Clear();
						foreach (string text in list)
						{
							if (gameObjectBlueprint.Tags.ContainsKey(text))
							{
								list2.Add(text);
							}
						}
						foreach (string text2 in list2)
						{
							ValueTuple<GameObjectBlueprint, int> valueTuple;
							if (!dictionary.TryGetValue(text2, out valueTuple) || list2.Count < valueTuple.Item2)
							{
								dictionary[text2] = new ValueTuple<GameObjectBlueprint, int>(gameObjectBlueprint, list2.Count);
							}
						}
					}
				}
			}
			foreach (KeyValuePair<string, ValueTuple<GameObjectBlueprint, int>> keyValuePair in dictionary)
			{
				try
				{
					GameObject gameObject2 = factory.CreateUnmodifiedObject(keyValuePair.Value.Item1, null, null);
					if (gameObject2 != null)
					{
						gameObject2.Physics.Owner = null;
						gameObject2.MakeUnderstood(false);
						Object.ReceiveObject(gameObject2, false, null);
					}
				}
				catch (Exception ex)
				{
					MetricsManager.LogException("golemquest:hamsa", ex, "game_exception");
				}
			}
		}

		// Token: 0x06007337 RID: 29495 RVA: 0x002B8840 File Offset: 0x002B6A40
		[WishCommand("golemquest:hamsa:tree", null)]
		public static void WishList()
		{
			Dictionary<GameObjectBlueprint, List<GameObjectBlueprint>> dictionary = new Dictionary<GameObjectBlueprint, List<GameObjectBlueprint>>();
			HashSet<GameObjectBlueprint> hashSet = new HashSet<GameObjectBlueprint>();
			foreach (GameObjectBlueprint gameObjectBlueprint in GameObjectFactory.Factory.BlueprintList)
			{
				int partParameter = gameObjectBlueprint.GetPartParameter<int>("Physics", "Weight", 10);
				if (!gameObjectBlueprint.IsBaseBlueprint() && partParameter <= 5)
				{
					if (!gameObjectBlueprint.Tags.Any<KeyValuePair<string, string>>((KeyValuePair<string, string> x) => x.Key.Contains("Semantic")) || !GolemHamsaSelection.GetSemanticTagsOf(gameObjectBlueprint).Any<string>(new Func<string, bool>(GolemMaterialSelection<GameObject, string>.Units.ContainsKey)))
					{
						GameObjectBlueprint gameObjectBlueprint2 = gameObjectBlueprint;
						GameObjectBlueprint gameObjectBlueprint3 = gameObjectBlueprint.ShallowParent;
						while (gameObjectBlueprint3 != null)
						{
							List<GameObjectBlueprint> list;
							if (dictionary.TryGetValue(gameObjectBlueprint3, out list))
							{
								if (!list.Contains(gameObjectBlueprint2))
								{
									list.Add(gameObjectBlueprint2);
									break;
								}
								break;
							}
							else
							{
								dictionary[gameObjectBlueprint3] = new List<GameObjectBlueprint> { gameObjectBlueprint2 };
								gameObjectBlueprint2 = gameObjectBlueprint3;
								gameObjectBlueprint3 = gameObjectBlueprint2.ShallowParent;
								if (gameObjectBlueprint2.IsBaseBlueprint())
								{
									hashSet.Add(gameObjectBlueprint2);
									break;
								}
							}
						}
					}
				}
			}
			StringBuilder stringBuilder = new StringBuilder("Hamsa without defined effects\n-----");
			foreach (GameObjectBlueprint gameObjectBlueprint4 in hashSet)
			{
				stringBuilder.Append("\n\n").Append(gameObjectBlueprint4.Name);
				foreach (GameObjectBlueprint gameObjectBlueprint5 in dictionary[gameObjectBlueprint4])
				{
					GolemHamsaSelection.WishAppendChild(stringBuilder, gameObjectBlueprint5, dictionary, 1);
				}
			}
			string text = stringBuilder.ToString();
			ClipboardHelper.SetClipboardData(text);
			Popup.Show(text, null, "Sounds/UI/ui_notification", true, true, true, true, null);
		}

		// Token: 0x06007338 RID: 29496 RVA: 0x002B8A44 File Offset: 0x002B6C44
		private static void WishAppendChild(StringBuilder SB, GameObjectBlueprint Blueprint, Dictionary<GameObjectBlueprint, List<GameObjectBlueprint>> Tree, int Indent = 1)
		{
			SB.Append('\n').Append('-', Indent).Append(' ')
				.Append(Blueprint.Name);
			List<GameObjectBlueprint> list;
			if (Tree.TryGetValue(Blueprint, out list))
			{
				foreach (GameObjectBlueprint gameObjectBlueprint in list)
				{
					GolemHamsaSelection.WishAppendChild(SB, gameObjectBlueprint, Tree, Indent + 1);
				}
			}
		}

		// Token: 0x06007339 RID: 29497 RVA: 0x002B8AC4 File Offset: 0x002B6CC4
		static GolemHamsaSelection()
		{
			GolemMaterialSelection<GameObject, string>.Units = new Dictionary<string, GolemMaterialSelection<GameObject, string>.UnitGenerator>
			{
				{
					"Melee",
					new GolemMaterialSelection<GameObject, string>.UnitGenerator(GolemHamsaSelection.UnitMelee)
				},
				{
					"Missile",
					new GolemMaterialSelection<GameObject, string>.UnitGenerator(GolemHamsaSelection.UnitMissile)
				},
				{
					"Thrown",
					new GolemMaterialSelection<GameObject, string>.UnitGenerator(GolemHamsaSelection.UnitThrown)
				},
				{
					"Protection",
					new GolemMaterialSelection<GameObject, string>.UnitGenerator(GolemHamsaSelection.UnitProtection)
				},
				{
					"Death",
					new GolemMaterialSelection<GameObject, string>.UnitGenerator(GolemHamsaSelection.UnitDeath)
				},
				{
					"Cybernetics",
					new GolemMaterialSelection<GameObject, string>.UnitGenerator(GolemHamsaSelection.UnitCybernetics)
				},
				{
					"Scholarly",
					new GolemMaterialSelection<GameObject, string>.UnitGenerator(GolemHamsaSelection.UnitSkillPoints)
				},
				{
					"Tool",
					new GolemMaterialSelection<GameObject, string>.UnitGenerator(GolemHamsaSelection.UnitSkillPoints)
				},
				{
					"DataDisk",
					new GolemMaterialSelection<GameObject, string>.UnitGenerator(GolemHamsaSelection.UnitDataDisk)
				},
				{
					"Energy",
					new GolemMaterialSelection<GameObject, string>.UnitGenerator(GolemHamsaSelection.UnitElectricalGeneration)
				},
				{
					"Conduit",
					new GolemMaterialSelection<GameObject, string>.UnitGenerator(GolemHamsaSelection.UnitElectricalGeneration)
				},
				{
					"Power",
					new GolemMaterialSelection<GameObject, string>.UnitGenerator(GolemHamsaSelection.UnitElectricalGeneration)
				},
				{
					"LightSource",
					new GolemMaterialSelection<GameObject, string>.UnitGenerator(GolemHamsaSelection.UnitLightManipulation)
				},
				{
					"Medical",
					new GolemMaterialSelection<GameObject, string>.UnitGenerator(GolemHamsaSelection.UnitRegeneration)
				},
				{
					"Seating",
					new GolemMaterialSelection<GameObject, string>.UnitGenerator(GolemHamsaSelection.UnitRegeneration)
				},
				{
					"Scrap",
					new GolemMaterialSelection<GameObject, string>.UnitGenerator(GolemHamsaSelection.UnitPsychometry)
				},
				{
					"Precious",
					new GolemMaterialSelection<GameObject, string>.UnitGenerator(GolemHamsaSelection.UnitEgo)
				},
				{
					"Music",
					new GolemMaterialSelection<GameObject, string>.UnitGenerator(GolemHamsaSelection.UnitEgo)
				},
				{
					"Beauty",
					new GolemMaterialSelection<GameObject, string>.UnitGenerator(GolemHamsaSelection.UnitEgo)
				},
				{
					"Trinket",
					new GolemMaterialSelection<GameObject, string>.UnitGenerator(GolemHamsaSelection.UnitTrinket)
				},
				{
					"Storage",
					new GolemMaterialSelection<GameObject, string>.UnitGenerator(GolemHamsaSelection.UnitStorage)
				},
				{
					"Tonic",
					new GolemMaterialSelection<GameObject, string>.UnitGenerator(GolemHamsaSelection.UnitTonic)
				},
				{
					"Key",
					new GolemMaterialSelection<GameObject, string>.UnitGenerator(GolemHamsaSelection.UnitPhasing)
				},
				{
					"Garbage",
					new GolemMaterialSelection<GameObject, string>.UnitGenerator(GolemHamsaSelection.UnitGarbage)
				},
				{
					"Food",
					new GolemMaterialSelection<GameObject, string>.UnitGenerator(GolemHamsaSelection.UnitFood)
				},
				{
					"Sleep",
					new GolemMaterialSelection<GameObject, string>.UnitGenerator(GolemHamsaSelection.UnitSleep)
				},
				{
					"Corrosion",
					new GolemMaterialSelection<GameObject, string>.UnitGenerator(GolemHamsaSelection.UnitCorrosion)
				},
				{
					"Poison",
					new GolemMaterialSelection<GameObject, string>.UnitGenerator(GolemHamsaSelection.UnitPoison)
				},
				{
					"Normality",
					new GolemMaterialSelection<GameObject, string>.UnitGenerator(GolemHamsaSelection.UnitNormality)
				},
				{
					"Random",
					new GolemMaterialSelection<GameObject, string>.UnitGenerator(GolemHamsaSelection.UnitRandom)
				},
				{
					"Travel",
					new GolemMaterialSelection<GameObject, string>.UnitGenerator(GolemHamsaSelection.UnitTravel)
				},
				{
					"Confusion",
					new GolemMaterialSelection<GameObject, string>.UnitGenerator(GolemHamsaSelection.UnitConfusion)
				},
				{
					"Duplication",
					new GolemMaterialSelection<GameObject, string>.UnitGenerator(GolemHamsaSelection.UnitDuplication)
				},
				{
					"Movement",
					new GolemMaterialSelection<GameObject, string>.UnitGenerator(GolemHamsaSelection.UnitMovement)
				},
				{
					"Floatation",
					new GolemMaterialSelection<GameObject, string>.UnitGenerator(GolemHamsaSelection.UnitFloatation)
				},
				{
					"Maps",
					new GolemMaterialSelection<GameObject, string>.UnitGenerator(GolemHamsaSelection.UnitMaps)
				},
				{
					"Fire",
					new GolemMaterialSelection<GameObject, string>.UnitGenerator(GolemHamsaSelection.UnitHeat)
				},
				{
					"Heat",
					new GolemMaterialSelection<GameObject, string>.UnitGenerator(GolemHamsaSelection.UnitHeat)
				},
				{
					"Ice",
					new GolemMaterialSelection<GameObject, string>.UnitGenerator(GolemHamsaSelection.UnitCold)
				},
				{
					"Cold",
					new GolemMaterialSelection<GameObject, string>.UnitGenerator(GolemHamsaSelection.UnitCold)
				},
				{
					"Time",
					new GolemMaterialSelection<GameObject, string>.UnitGenerator(GolemHamsaSelection.UnitTime)
				},
				{
					"Flight",
					new GolemMaterialSelection<GameObject, string>.UnitGenerator(GolemHamsaSelection.UnitFlight)
				},
				{
					"Figurine",
					new GolemMaterialSelection<GameObject, string>.UnitGenerator(GolemHamsaSelection.UnitFigurine)
				},
				{
					"Explosive",
					new GolemMaterialSelection<GameObject, string>.UnitGenerator(GolemHamsaSelection.UnitExplosive)
				},
				{
					"Stun",
					new GolemMaterialSelection<GameObject, string>.UnitGenerator(GolemHamsaSelection.UnitStun)
				},
				{
					"Electromagnetic",
					new GolemMaterialSelection<GameObject, string>.UnitGenerator(GolemHamsaSelection.UnitElectromagnetic)
				},
				{
					"Repulsive",
					new GolemMaterialSelection<GameObject, string>.UnitGenerator(GolemHamsaSelection.UnitRepulsive)
				}
			};
		}

		// Token: 0x0600733A RID: 29498 RVA: 0x002B8F14 File Offset: 0x002B7114
		private static IEnumerable<GameObjectUnit> UnitMelee(GameObject Object)
		{
			yield return new GameObjectPartUnit
			{
				Part = new ReduceEnergyCosts
				{
					IncludeTypes = "Melee",
					PercentageReduction = "15",
					WorksOnSelf = true,
					WorksOnEquipper = false,
					IsPowered = false
				}
			};
			yield break;
		}

		// Token: 0x0600733B RID: 29499 RVA: 0x002B8F1D File Offset: 0x002B711D
		private static IEnumerable<GameObjectUnit> UnitMissile(GameObject Object)
		{
			yield return new GameObjectPartUnit
			{
				Part = new ReduceEnergyCosts
				{
					IncludeTypes = "Missile",
					ExcludeTypes = "Throw",
					PercentageReduction = "25",
					WorksOnSelf = true,
					WorksOnEquipper = false,
					IsPowered = false
				}
			};
			yield break;
		}

		// Token: 0x0600733C RID: 29500 RVA: 0x002B8F26 File Offset: 0x002B7126
		private static IEnumerable<GameObjectUnit> UnitThrown(GameObject Object)
		{
			yield return new GameObjectPartUnit
			{
				Part = new ReduceEnergyCosts
				{
					IncludeTypes = "Throw",
					PercentageReduction = "50",
					WorksOnSelf = true,
					WorksOnEquipper = false,
					IsPowered = false
				}
			};
			yield break;
		}

		// Token: 0x0600733D RID: 29501 RVA: 0x002B8F2F File Offset: 0x002B712F
		private static IEnumerable<GameObjectUnit> UnitProtection(GameObject Object)
		{
			Armor armor = ((Object != null) ? Object.GetPart<Armor>() : null);
			bool flag = armor != null && armor.DV > armor.AV;
			yield return new GameObjectAttributeUnit
			{
				Attribute = (flag ? "DV" : "AV"),
				Value = (flag ? 6 : 6)
			};
			yield break;
		}

		// Token: 0x0600733E RID: 29502 RVA: 0x002B8F3F File Offset: 0x002B713F
		private static IEnumerable<GameObjectUnit> UnitDeath(GameObject Object)
		{
			yield return new GameObjectPartUnit
			{
				Part = new FungalFortitude
				{
					WorksOnSelf = true,
					WorksOnHolder = false,
					WorksOnCarrier = false,
					AVBonus = 1,
					ResistBonus = 4
				}
			};
			yield break;
		}

		// Token: 0x0600733F RID: 29503 RVA: 0x002B8F48 File Offset: 0x002B7148
		private static IEnumerable<GameObjectUnit> UnitCybernetics(GameObject Object)
		{
			if (Object != null && Object.HasPart<CyberneticsBaseItem>())
			{
				yield return new GameObjectCyberneticsUnit
				{
					Blueprint = Object.Blueprint,
					LicenseStat = "Toughness",
					Removable = false
				};
			}
			yield break;
		}

		// Token: 0x06007340 RID: 29504 RVA: 0x002B8F58 File Offset: 0x002B7158
		private static IEnumerable<GameObjectUnit> UnitSkillPoints(GameObject Object)
		{
			yield return new GameObjectAttributeUnit
			{
				Attribute = "SP",
				Value = 800
			};
			yield break;
		}

		// Token: 0x06007341 RID: 29505 RVA: 0x002B8F61 File Offset: 0x002B7161
		private static IEnumerable<GameObjectUnit> UnitDataDisk(GameObject Object)
		{
			DataDisk dataDisk;
			if (Object != null && Object.TryGetPart<DataDisk>(out dataDisk))
			{
				TinkerData data = dataDisk.Data;
				GameObjectBlueprint blueprint;
				if (((data != null) ? data.Blueprint : null) != null && GameObjectFactory.Factory.Blueprints.TryGetValue(dataDisk.Data.Blueprint, out blueprint))
				{
					Object = null;
					foreach (string text in GolemHamsaSelection.GetSemanticTagsOf(blueprint))
					{
						GolemMaterialSelection<GameObject, string>.UnitGenerator unitGenerator;
						if (GolemMaterialSelection<GameObject, string>.Units.TryGetValue(text, out unitGenerator))
						{
							GolemMaterialSelection<GameObject, string>.UnitGenerator unitGenerator2 = unitGenerator;
							GameObject gameObject;
							if ((gameObject = Object) == null)
							{
								gameObject = (Object = blueprint.createSample());
							}
							foreach (GameObjectUnit gameObjectUnit in unitGenerator2(gameObject))
							{
								yield return gameObjectUnit;
							}
							IEnumerator<GameObjectUnit> enumerator2 = null;
						}
					}
					IEnumerator<string> enumerator = null;
					GameObject gameObject2 = Object;
					if (gameObject2 != null)
					{
						gameObject2.Obliterate(null, false, null);
					}
				}
			}
			yield break;
			yield break;
		}

		// Token: 0x06007342 RID: 29506 RVA: 0x002B8F71 File Offset: 0x002B7171
		private static IEnumerable<GameObjectUnit> UnitElectricalGeneration(GameObject Object)
		{
			yield return new GameObjectMutationUnit
			{
				Name = "Electrical Generation",
				Level = 10
			};
			yield break;
		}

		// Token: 0x06007343 RID: 29507 RVA: 0x002B8F7A File Offset: 0x002B717A
		private static IEnumerable<GameObjectUnit> UnitLightManipulation(GameObject Object)
		{
			yield return new GameObjectMutationUnit
			{
				Name = "Light Manipulation",
				Level = 10
			};
			yield break;
		}

		// Token: 0x06007344 RID: 29508 RVA: 0x002B8F83 File Offset: 0x002B7183
		private static IEnumerable<GameObjectUnit> UnitRegeneration(GameObject Object)
		{
			yield return new GameObjectMutationUnit
			{
				Name = "Regeneration",
				Level = 10
			};
			yield break;
		}

		// Token: 0x06007345 RID: 29509 RVA: 0x002B8F8C File Offset: 0x002B718C
		private static IEnumerable<GameObjectUnit> UnitPsychometry(GameObject Object)
		{
			yield return new GameObjectMutationUnit
			{
				Name = "Psychometry",
				Level = 10
			};
			yield break;
		}

		// Token: 0x06007346 RID: 29510 RVA: 0x002B8F95 File Offset: 0x002B7195
		private static IEnumerable<GameObjectUnit> UnitPhasing(GameObject Object)
		{
			yield return new GameObjectMutationUnit
			{
				Name = "Phasing",
				Level = 10
			};
			yield break;
		}

		// Token: 0x06007347 RID: 29511 RVA: 0x002B8F9E File Offset: 0x002B719E
		private static IEnumerable<GameObjectUnit> UnitEgo(GameObject Object)
		{
			yield return new GameObjectAttributeUnit
			{
				Attribute = "Ego",
				Value = 8
			};
			yield break;
		}

		// Token: 0x06007348 RID: 29512 RVA: 0x002B8FA7 File Offset: 0x002B71A7
		private static IEnumerable<GameObjectUnit> UnitTrinket(GameObject Object)
		{
			yield return new GameObjectAttributeUnit
			{
				Attribute = "All",
				Value = 2
			};
			yield break;
		}

		// Token: 0x06007349 RID: 29513 RVA: 0x002B8FB0 File Offset: 0x002B71B0
		private static IEnumerable<GameObjectUnit> UnitStorage(GameObject Object)
		{
			yield return new GameObjectPartUnit
			{
				Part = new CarryBonus
				{
					WorksOnSelf = true,
					WorksOnEquipper = false,
					Amount = 1000,
					Style = "Percent"
				}
			};
			yield break;
		}

		// Token: 0x0600734A RID: 29514 RVA: 0x002B8FB9 File Offset: 0x002B71B9
		private static IEnumerable<GameObjectUnit> UnitTonic(GameObject Object)
		{
			yield return new GameObjectSaveModifierUnit
			{
				Vs = "Overdosing",
				Value = SavingThrows.IMMUNITY
			};
			yield break;
		}

		// Token: 0x0600734B RID: 29515 RVA: 0x002B8FC2 File Offset: 0x002B71C2
		private static IEnumerable<GameObjectUnit> UnitGarbage(GameObject Object)
		{
			yield return new GameObjectPartUnit
			{
				Part = new TrashOracle
				{
					Magnitude = 10
				}
			};
			yield break;
		}

		// Token: 0x0600734C RID: 29516 RVA: 0x002B8FCB File Offset: 0x002B71CB
		private static IEnumerable<GameObjectUnit> UnitFood(GameObject Object)
		{
			yield return new GameObjectAttributeUnit
			{
				Attribute = "Hitpoints",
				Value = 25,
				Percent = true
			};
			yield break;
		}

		// Token: 0x0600734D RID: 29517 RVA: 0x002B8FD4 File Offset: 0x002B71D4
		private static IEnumerable<GameObjectUnit> UnitSleep(GameObject Object)
		{
			yield return new GameObjectMutationUnit
			{
				Name = "Sleep Gas Generation",
				Level = 10
			};
			yield break;
		}

		// Token: 0x0600734E RID: 29518 RVA: 0x002B8FDD File Offset: 0x002B71DD
		private static IEnumerable<GameObjectUnit> UnitPoison(GameObject Object)
		{
			yield return new GameObjectMutationUnit
			{
				Name = "Poison Gas Generation",
				Level = 10
			};
			yield break;
		}

		// Token: 0x0600734F RID: 29519 RVA: 0x002B8FE6 File Offset: 0x002B71E6
		private static IEnumerable<GameObjectUnit> UnitCorrosion(GameObject Object)
		{
			yield return new GameObjectMutationUnit
			{
				Name = "Corrosive Gas Generation",
				Level = 10
			};
			yield break;
		}

		// Token: 0x06007350 RID: 29520 RVA: 0x002B8FEF File Offset: 0x002B71EF
		private static IEnumerable<GameObjectUnit> UnitNormality(GameObject Object)
		{
			yield return new GameObjectMutationUnit
			{
				Class = "NormalityGasGeneration",
				Level = 10
			};
			yield break;
		}

		// Token: 0x06007351 RID: 29521 RVA: 0x002B8FF8 File Offset: 0x002B71F8
		private static IEnumerable<GameObjectUnit> UnitRandom(GameObject Object)
		{
			yield return new GameObjectGolemQuestRandomUnit
			{
				SelectionID = "Hamsa",
				Amount = 2
			};
			yield break;
		}

		// Token: 0x06007352 RID: 29522 RVA: 0x002B9001 File Offset: 0x002B7201
		private static IEnumerable<GameObjectUnit> UnitTravel(GameObject Object)
		{
			yield return new GameObjectMutationUnit
			{
				Name = "Teleportation",
				Level = 6
			};
			yield break;
		}

		// Token: 0x06007353 RID: 29523 RVA: 0x002B900A File Offset: 0x002B720A
		private static IEnumerable<GameObjectUnit> UnitConfusion(GameObject Object)
		{
			yield return new GameObjectMutationUnit
			{
				Name = "Confusion",
				Level = 10
			};
			yield break;
		}

		// Token: 0x06007354 RID: 29524 RVA: 0x002B9013 File Offset: 0x002B7213
		private static IEnumerable<GameObjectUnit> UnitDuplication(GameObject Object)
		{
			yield return new GameObjectMutationUnit
			{
				Name = "Temporal Fugue",
				Level = 6
			};
			yield break;
		}

		// Token: 0x06007355 RID: 29525 RVA: 0x002B901C File Offset: 0x002B721C
		private static IEnumerable<GameObjectUnit> UnitMovement(GameObject Object)
		{
			yield return new GameObjectAttributeUnit
			{
				Attribute = "MoveSpeed",
				Value = -50
			};
			yield break;
		}

		// Token: 0x06007356 RID: 29526 RVA: 0x002B9025 File Offset: 0x002B7225
		private static IEnumerable<GameObjectUnit> UnitFloatation(GameObject Object)
		{
			yield return new GameObjectBodyPartUnit
			{
				Type = "Floating Nearby",
				InsertAfter = "Floating Nearby",
				Manager = "Golem::Hamsa"
			};
			yield break;
		}

		// Token: 0x06007357 RID: 29527 RVA: 0x002B902E File Offset: 0x002B722E
		private static IEnumerable<GameObjectUnit> UnitMaps(GameObject Object)
		{
			yield return new GameObjectMutationUnit
			{
				Name = "Clairvoyance",
				Level = 10
			};
			yield break;
		}

		// Token: 0x06007358 RID: 29528 RVA: 0x002B9037 File Offset: 0x002B7237
		private static IEnumerable<GameObjectUnit> UnitHeat(GameObject Object)
		{
			return GolemHamsaSelection.UnitAmp(Object, 75, 0);
		}

		// Token: 0x06007359 RID: 29529 RVA: 0x002B9042 File Offset: 0x002B7242
		private static IEnumerable<GameObjectUnit> UnitCold(GameObject Object)
		{
			return GolemHamsaSelection.UnitAmp(Object, 0, 75);
		}

		// Token: 0x0600735A RID: 29530 RVA: 0x002B904D File Offset: 0x002B724D
		private static IEnumerable<GameObjectUnit> UnitAmp(GameObject Object, int Heat, int Cold)
		{
			yield return new GameObjectPartUnit
			{
				Part = new ThermalAmp
				{
					ColdDamage = Cold,
					ModifyCold = Cold,
					HeatDamage = Heat,
					ModifyHeat = Heat,
					WorksOnSelf = true,
					WorksOnWearer = false,
					IsPowered = false,
					NameForStatus = null
				}
			};
			yield break;
		}

		// Token: 0x0600735B RID: 29531 RVA: 0x002B9064 File Offset: 0x002B7264
		private static IEnumerable<GameObjectUnit> UnitTime(GameObject Object)
		{
			yield return new GameObjectMutationUnit
			{
				Name = "Time Dilation",
				Level = 8
			};
			yield break;
		}

		// Token: 0x0600735C RID: 29532 RVA: 0x002B906D File Offset: 0x002B726D
		private static IEnumerable<GameObjectUnit> UnitFlight(GameObject Object)
		{
			yield return new GameObjectMutationUnit
			{
				Name = "Wings",
				Level = 10
			};
			yield break;
		}

		// Token: 0x0600735D RID: 29533 RVA: 0x002B9076 File Offset: 0x002B7276
		private static IEnumerable<GameObjectUnit> UnitFigurine(GameObject Object)
		{
			int num = 1000;
			Dictionary<string, GameObjectBlueprint> blueprints = GameObjectFactory.Factory.Blueprints;
			string text;
			if (Object == null)
			{
				text = null;
			}
			else
			{
				RandomFigurine part = Object.GetPart<RandomFigurine>();
				text = ((part != null) ? part.Creature : null);
			}
			GameObjectBlueprint value = blueprints.GetValue(text, null);
			if (value == null)
			{
				yield break;
			}
			string partParameter = value.GetPartParameter<string>("Brain", "Factions", null);
			if (partParameter.IsNullOrEmpty())
			{
				yield break;
			}
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			Brain.FillFactionMembership(dictionary, partParameter);
			if (dictionary.IsNullOrEmpty<KeyValuePair<string, int>>())
			{
				yield break;
			}
			float num2 = 0f;
			int num3 = 0;
			foreach (KeyValuePair<string, int> keyValuePair in dictionary)
			{
				Faction ifExists = Factions.GetIfExists(keyValuePair.Key);
				if (ifExists != null && ifExists.Visible)
				{
					num2 += (float)keyValuePair.Value;
					num3++;
				}
			}
			if (num3 == 0)
			{
				yield break;
			}
			GameObjectUnitAggregate gameObjectUnitAggregate = null;
			foreach (KeyValuePair<string, int> keyValuePair2 in dictionary)
			{
				Faction ifExists2 = Factions.GetIfExists(keyValuePair2.Key);
				if (ifExists2 != null && ifExists2.Visible)
				{
					GameObjectReputationUnit gameObjectReputationUnit = new GameObjectReputationUnit
					{
						Entry = ifExists2,
						Value = Mathf.RoundToInt((float)keyValuePair2.Value / num2 * (float)num / 5f) * 5,
						Type = "Figurine"
					};
					if (num3 == 1)
					{
						yield return gameObjectReputationUnit;
						yield break;
					}
					if (gameObjectUnitAggregate == null)
					{
						gameObjectUnitAggregate = new GameObjectUnitAggregate();
					}
					gameObjectUnitAggregate.Units.Add(gameObjectReputationUnit);
				}
			}
			Dictionary<string, int>.Enumerator enumerator2 = default(Dictionary<string, int>.Enumerator);
			yield return gameObjectUnitAggregate;
			yield break;
			yield break;
		}

		// Token: 0x0600735E RID: 29534 RVA: 0x002B9086 File Offset: 0x002B7286
		private static IEnumerable<GameObjectUnit> UnitExplosive(GameObject Object)
		{
			yield return new GameObjectMutationUnit
			{
				Name = "Stunning Force",
				Level = 10
			};
			yield break;
		}

		// Token: 0x0600735F RID: 29535 RVA: 0x002B908F File Offset: 0x002B728F
		private static IEnumerable<GameObjectUnit> UnitStun(GameObject Object)
		{
			yield return new GameObjectMutationUnit
			{
				Name = "Stunning Force",
				Level = 10
			};
			yield break;
		}

		// Token: 0x06007360 RID: 29536 RVA: 0x002B9098 File Offset: 0x002B7298
		private static IEnumerable<GameObjectUnit> UnitElectromagnetic(GameObject Object)
		{
			yield return new GameObjectMutationUnit
			{
				Name = "Electromagnetic Pulse",
				Level = 10
			};
			yield break;
		}

		// Token: 0x06007361 RID: 29537 RVA: 0x002B90A1 File Offset: 0x002B72A1
		private static IEnumerable<GameObjectUnit> UnitRepulsive(GameObject Object)
		{
			yield return new GameObjectMutationUnit
			{
				Name = "Socially Repugnant",
				Level = 1
			};
			yield break;
		}

		// Token: 0x0400353C RID: 13628
		public const string BASE_ID = "Hamsa";
	}
}
