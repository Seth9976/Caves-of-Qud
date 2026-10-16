using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleLib.Console;
using XRL.Collections;
using XRL.Liquids;
using XRL.Wish;
using XRL.World.Capabilities;
using XRL.World.Parts;
using XRL.World.Units;

namespace XRL.World.Quests.GolemQuest
{
	// Token: 0x02000B2D RID: 2861
	[HasWishCommand]
	[Serializable]
	public class GolemCatalystSelection : GolemMaterialSelection<string, string>
	{
		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x060072F6 RID: 29430 RVA: 0x002B7A77 File Offset: 0x002B5C77
		// (set) Token: 0x060072F7 RID: 29431 RVA: 0x002B7A7F File Offset: 0x002B5C7F
		public override string Material
		{
			get
			{
				return this.Liquid;
			}
			set
			{
				this.Liquid = value;
			}
		}

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x060072F8 RID: 29432 RVA: 0x002B7A88 File Offset: 0x002B5C88
		public override string ID
		{
			get
			{
				return "Catalyst";
			}
		}

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x060072F9 RID: 29433 RVA: 0x002B7A8F File Offset: 0x002B5C8F
		public override string DisplayName
		{
			get
			{
				return "catalyst";
			}
		}

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x060072FA RID: 29434 RVA: 0x002B7A96 File Offset: 0x002B5C96
		public override char Key
		{
			get
			{
				return 'c';
			}
		}

		// Token: 0x060072FB RID: 29435 RVA: 0x002B7A9C File Offset: 0x002B5C9C
		public override bool IsValid(string Material)
		{
			return base.IsValid(Material) && GolemMaterialSelection<string, string>.Units.ContainsKey(Material) && base.GetValidHolders().Any<GameObject>((GameObject x) => x.GetFreeDrams(Material, null, null, null, false) >= 3);
		}

		// Token: 0x060072FC RID: 29436 RVA: 0x002B7AEF File Offset: 0x002B5CEF
		public override string GetNameFor(string Material)
		{
			string text = "{0} {1} of {2}";
			object obj = 3;
			object obj2 = "drams";
			BaseLiquid liquid = LiquidVolume.GetLiquid(Material);
			return string.Format(text, obj, obj2, ((liquid != null) ? liquid.Name : null) ?? Material);
		}

		// Token: 0x060072FD RID: 29437 RVA: 0x002B7B24 File Offset: 0x002B5D24
		public override IRenderable GetIconFor(string Material)
		{
			if (this.IconVolume == null)
			{
				this.IconVolume = GameObjectFactory.Factory.CreateSampleObject("DeepFreshWaterPool", null);
			}
			LiquidVolume liquidVolume = this.IconVolume.LiquidVolume;
			liquidVolume.ComponentLiquids.Clear();
			liquidVolume.ComponentLiquids[Material] = 1000;
			liquidVolume.LastPaintMask = -1;
			liquidVolume.RecalculatePrimary();
			liquidVolume.Paint(0);
			return new Renderable(this.IconVolume.Render);
		}

		// Token: 0x060072FE RID: 29438 RVA: 0x002B7B9A File Offset: 0x002B5D9A
		public override IEnumerable<GameObjectUnit> YieldEffectsOf(string Material)
		{
			GolemCatalystSelection.<YieldEffectsOf>d__16 <YieldEffectsOf>d__ = new GolemCatalystSelection.<YieldEffectsOf>d__16(-2);
			<YieldEffectsOf>d__.<>3__Material = Material;
			return <YieldEffectsOf>d__;
		}

		// Token: 0x060072FF RID: 29439 RVA: 0x002B7BAC File Offset: 0x002B5DAC
		public override List<string> GetValidMaterials()
		{
			this._ValidMaterials.Clear();
			List<GameObject> validHolders = base.GetValidHolders();
			GetFreeDramsEvent getFreeDramsEvent = GetFreeDramsEvent.FromPool();
			foreach (KeyValuePair<string, GolemMaterialSelection<string, string>.UnitGenerator> keyValuePair in GolemMaterialSelection<string, string>.Units)
			{
				foreach (GameObject gameObject in validHolders)
				{
					getFreeDramsEvent.Actor = gameObject;
					getFreeDramsEvent.Drams = 0;
					getFreeDramsEvent.Liquid = keyValuePair.Key;
					gameObject.HandleEvent(getFreeDramsEvent);
					if (getFreeDramsEvent.Drams >= 3)
					{
						this._ValidMaterials.Add(keyValuePair.Key);
						break;
					}
				}
			}
			return this._ValidMaterials;
		}

		// Token: 0x06007300 RID: 29440 RVA: 0x002B7C90 File Offset: 0x002B5E90
		public override void Apply(GameObject Object)
		{
			base.Apply(Object);
			if (LiquidVolume.isValidLiquid(this.Liquid))
			{
				foreach (GameObject gameObject in base.GetValidHolders())
				{
					if (gameObject.GetFreeDrams(this.Liquid, null, null, null, false) >= 3)
					{
						gameObject.UseDrams(3, this.Liquid, null, null, null, null, false);
						break;
					}
				}
				Object.SetStringProperty("CatalystLiquid", this.Liquid, false);
				Object.SetStringProperty("BleedLiquid", "proteangunk-990," + this.Liquid + "-10", false);
			}
		}

		// Token: 0x06007301 RID: 29441 RVA: 0x002B7D50 File Offset: 0x002B5F50
		[WishCommand("golemquest:catalyst", null)]
		private static void Wish()
		{
			GolemCatalystSelection.ReceiveMaterials(The.Player);
		}

		// Token: 0x06007302 RID: 29442 RVA: 0x002B7D5C File Offset: 0x002B5F5C
		public static void ReceiveMaterials(GameObject Object)
		{
			using (StringMap<BaseLiquid>.Enumerator enumerator = LiquidVolume.Liquids.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<string, BaseLiquid> pair = enumerator.Current;
					Object.ReceiveObject(GameObjectFactory.Factory.CreateObject("Phial", delegate(GameObject x)
					{
						LiquidVolume liquidVolume = x.LiquidVolume;
						liquidVolume.InitialLiquid = pair.Key + "-1000";
						liquidVolume.MaxVolume = 3;
						liquidVolume.StartVolume = "3";
					}), false, null);
				}
			}
		}

		// Token: 0x06007303 RID: 29443 RVA: 0x002B7DD8 File Offset: 0x002B5FD8
		static GolemCatalystSelection()
		{
			GolemMaterialSelection<string, string>.Units = new Dictionary<string, GolemMaterialSelection<string, string>.UnitGenerator>
			{
				{
					"water",
					new GolemMaterialSelection<string, string>.UnitGenerator(GolemCatalystSelection.UnitWater)
				},
				{
					"salt",
					new GolemMaterialSelection<string, string>.UnitGenerator(GolemCatalystSelection.UnitSalt)
				},
				{
					"asphalt",
					new GolemMaterialSelection<string, string>.UnitGenerator(GolemCatalystSelection.UnitAsphalt)
				},
				{
					"lava",
					new GolemMaterialSelection<string, string>.UnitGenerator(GolemCatalystSelection.UnitLava)
				},
				{
					"slime",
					new GolemMaterialSelection<string, string>.UnitGenerator(GolemCatalystSelection.UnitSlime)
				},
				{
					"oil",
					new GolemMaterialSelection<string, string>.UnitGenerator(GolemCatalystSelection.UnitOil)
				},
				{
					"blood",
					new GolemMaterialSelection<string, string>.UnitGenerator(GolemCatalystSelection.UnitBlood)
				},
				{
					"acid",
					new GolemMaterialSelection<string, string>.UnitGenerator(GolemCatalystSelection.UnitAcid)
				},
				{
					"honey",
					new GolemMaterialSelection<string, string>.UnitGenerator(GolemCatalystSelection.UnitHoney)
				},
				{
					"wine",
					new GolemMaterialSelection<string, string>.UnitGenerator(GolemCatalystSelection.UnitWine)
				},
				{
					"sludge",
					new GolemMaterialSelection<string, string>.UnitGenerator(GolemCatalystSelection.UnitSludge)
				},
				{
					"goo",
					new GolemMaterialSelection<string, string>.UnitGenerator(GolemCatalystSelection.UnitGoo)
				},
				{
					"putrid",
					new GolemMaterialSelection<string, string>.UnitGenerator(GolemCatalystSelection.UnitPutrescence)
				},
				{
					"gel",
					new GolemMaterialSelection<string, string>.UnitGenerator(GolemCatalystSelection.UnitGel)
				},
				{
					"ooze",
					new GolemMaterialSelection<string, string>.UnitGenerator(GolemCatalystSelection.UnitOoze)
				},
				{
					"cider",
					new GolemMaterialSelection<string, string>.UnitGenerator(GolemCatalystSelection.UnitCider)
				},
				{
					"convalessence",
					new GolemMaterialSelection<string, string>.UnitGenerator(GolemCatalystSelection.UnitConvalessence)
				},
				{
					"neutronflux",
					new GolemMaterialSelection<string, string>.UnitGenerator(GolemCatalystSelection.UnitNeutronFlux)
				},
				{
					"cloning",
					new GolemMaterialSelection<string, string>.UnitGenerator(GolemCatalystSelection.UnitCloning)
				},
				{
					"wax",
					new GolemMaterialSelection<string, string>.UnitGenerator(GolemCatalystSelection.UnitWax)
				},
				{
					"ink",
					new GolemMaterialSelection<string, string>.UnitGenerator(GolemCatalystSelection.UnitInk)
				},
				{
					"sap",
					new GolemMaterialSelection<string, string>.UnitGenerator(GolemCatalystSelection.UnitSap)
				},
				{
					"brainbrine",
					new GolemMaterialSelection<string, string>.UnitGenerator(GolemCatalystSelection.UnitBrainBrine)
				},
				{
					"algae",
					new GolemMaterialSelection<string, string>.UnitGenerator(GolemCatalystSelection.UnitAlgae)
				},
				{
					"sunslag",
					new GolemMaterialSelection<string, string>.UnitGenerator(GolemCatalystSelection.UnitSunSlag)
				},
				{
					"warmstatic",
					new GolemMaterialSelection<string, string>.UnitGenerator(GolemCatalystSelection.UnitWarmStatic)
				}
			};
		}

		// Token: 0x06007304 RID: 29444 RVA: 0x002B8045 File Offset: 0x002B6245
		private static IEnumerable<GameObjectUnit> UnitWater(string Liquid)
		{
			yield return new GameObjectAttributeUnit
			{
				Attribute = "Hitpoints",
				Value = 30,
				Percent = true
			};
			yield break;
		}

		// Token: 0x06007305 RID: 29445 RVA: 0x002B804E File Offset: 0x002B624E
		private static IEnumerable<GameObjectUnit> UnitSalt(string Liquid)
		{
			yield return new GameObjectAttributeUnit
			{
				Attribute = "Willpower",
				Value = 10
			};
			yield break;
		}

		// Token: 0x06007306 RID: 29446 RVA: 0x002B8057 File Offset: 0x002B6257
		private static IEnumerable<GameObjectUnit> UnitAsphalt(string Liquid)
		{
			yield return new GameObjectSaveModifierUnit
			{
				Vs = "Stuck",
				Value = SavingThrows.IMMUNITY
			};
			yield return new GameObjectPartUnit
			{
				Part = new StickOnHit
				{
					Chance = 100
				},
				Description = "Melee attacks cause enemies to get stuck"
			};
			yield break;
		}

		// Token: 0x06007307 RID: 29447 RVA: 0x002B8060 File Offset: 0x002B6260
		private static IEnumerable<GameObjectUnit> UnitLava(string Liquid)
		{
			yield return new GameObjectAttributeUnit
			{
				Attribute = "HeatResistance",
				Value = 200
			};
			yield return new GameObjectPartUnit
			{
				Part = new AttackerElementalAmplifier
				{
					Attribute = "Heat",
					Amplification = 60
				}
			};
			yield break;
		}

		// Token: 0x06007308 RID: 29448 RVA: 0x002B8069 File Offset: 0x002B6269
		private static IEnumerable<GameObjectUnit> UnitSlime(string Liquid)
		{
			yield return new GameObjectSaveModifierUnit
			{
				Vs = "Slip",
				Value = SavingThrows.IMMUNITY
			};
			yield return new GameObjectPartUnit
			{
				Part = new ProneOnHit()
			};
			yield break;
		}

		// Token: 0x06007309 RID: 29449 RVA: 0x002B8072 File Offset: 0x002B6272
		private static IEnumerable<GameObjectUnit> UnitOil(string Liquid)
		{
			yield return new GameObjectSaveModifierUnit
			{
				Vs = "Slip",
				Value = SavingThrows.IMMUNITY
			};
			yield return new GameObjectPartUnit
			{
				Part = new AttackerElementalAmplifier
				{
					Attribute = "Heat",
					Amplification = 60
				}
			};
			yield break;
		}

		// Token: 0x0600730A RID: 29450 RVA: 0x002B807B File Offset: 0x002B627B
		private static IEnumerable<GameObjectUnit> UnitBlood(string Liquid)
		{
			yield return new GameObjectPartUnit
			{
				Part = new VampiricWeapon
				{
					Percent = "10",
					WorksThrown = false,
					WorksAsProjectile = false,
					WorksInMelee = false,
					WorksAsAttacker = true,
					RequiresLivingTarget = true
				}
			};
			yield break;
		}

		// Token: 0x0600730B RID: 29451 RVA: 0x002B8084 File Offset: 0x002B6284
		private static IEnumerable<GameObjectUnit> UnitAcid(string Liquid)
		{
			yield return new GameObjectAttributeUnit
			{
				Attribute = "AcidResistance",
				Value = 200
			};
			yield return new GameObjectPartUnit
			{
				Part = new AttackerElementalAmplifier
				{
					Attribute = "Acid",
					Amplification = 75
				}
			};
			yield break;
		}

		// Token: 0x0600730C RID: 29452 RVA: 0x002B808D File Offset: 0x002B628D
		private static IEnumerable<GameObjectUnit> UnitHoney(string Liquid)
		{
			yield return new GameObjectMutationUnit
			{
				Name = "Regeneration",
				Level = 5
			};
			yield return new GameObjectPartUnit
			{
				Part = new StickOnHit
				{
					Chance = 100
				},
				Description = "Melee attacks cause enemies to get stuck"
			};
			yield break;
		}

		// Token: 0x0600730D RID: 29453 RVA: 0x002B8096 File Offset: 0x002B6296
		private static IEnumerable<GameObjectUnit> UnitWine(string Liquid)
		{
			yield return new GameObjectMutationUnit
			{
				Name = "Confusion",
				Level = 10
			};
			yield return new GameObjectPartUnit
			{
				Part = new EffectResistance(new string[] { "Confusion" }),
				Description = "Immune to confusion"
			};
			yield break;
		}

		// Token: 0x0600730E RID: 29454 RVA: 0x002B809F File Offset: 0x002B629F
		private static IEnumerable<GameObjectUnit> UnitSludge(string Liquid)
		{
			yield return new GameObjectPartUnit
			{
				Part = new Galvanized
				{
					Inventory = false,
					Equipment = true
				}
			};
			yield return new GameObjectPartUnit
			{
				Part = new RustOnHit
				{
					Chance = 100,
					Amount = "2-4",
					PreferPartType = "Hand",
					AffectInventory = false
				},
				Description = "Melee attacks cause 2-4 equipped enemy items to rust, starting with held items"
			};
			yield break;
		}

		// Token: 0x0600730F RID: 29455 RVA: 0x002B80A8 File Offset: 0x002B62A8
		private static IEnumerable<GameObjectUnit> UnitGoo(string Liquid)
		{
			yield return new GameObjectPartUnit
			{
				Part = new EffectResistance(new string[] { "Poison", "PoisonGasPoison" }),
				Description = "Immune to poison"
			};
			yield return new GameObjectPartUnit
			{
				Part = new AttackerElementalAmplifier
				{
					Attribute = "Poison",
					Amplification = 100
				}
			};
			yield break;
		}

		// Token: 0x06007310 RID: 29456 RVA: 0x002B80B1 File Offset: 0x002B62B1
		private static IEnumerable<GameObjectUnit> UnitPutrescence(string Liquid)
		{
			yield return new GameObjectPartUnit
			{
				Part = new SapOnPenetration
				{
					Chance = 100,
					Stat = "Toughness",
					Amount = "1",
					NaturalOnly = false
				},
				Description = "Melee attacks drain 1 Toughness"
			};
			yield break;
		}

		// Token: 0x06007311 RID: 29457 RVA: 0x002B80BA File Offset: 0x002B62BA
		private static IEnumerable<GameObjectUnit> UnitGel(string Liquid)
		{
			yield return new GameObjectUnitAggregate("+50 to all resists", new GameObjectUnit[]
			{
				new GameObjectAttributeUnit
				{
					Attribute = "HeatResistance",
					Value = 50
				},
				new GameObjectAttributeUnit
				{
					Attribute = "ColdResistance",
					Value = 50
				},
				new GameObjectAttributeUnit
				{
					Attribute = "ElectricResistance",
					Value = 50
				},
				new GameObjectAttributeUnit
				{
					Attribute = "AcidResistance",
					Value = 50
				}
			});
			yield return new GameObjectAttributeUnit
			{
				Attribute = "AV",
				Value = 3
			};
			yield break;
		}

		// Token: 0x06007312 RID: 29458 RVA: 0x002B80C3 File Offset: 0x002B62C3
		private static IEnumerable<GameObjectUnit> UnitOoze(string Liquid)
		{
			yield return new GameObjectSaveModifierUnit
			{
				Vs = "Disease",
				Value = SavingThrows.IMMUNITY
			};
			yield return new GameObjectMutationUnit
			{
				Name = "Corrosive Gas Generation",
				Level = 5
			};
			yield break;
		}

		// Token: 0x06007313 RID: 29459 RVA: 0x002B80CC File Offset: 0x002B62CC
		private static IEnumerable<GameObjectUnit> UnitCider(string Liquid)
		{
			yield return new GameObjectAttributeUnit
			{
				Attribute = "Speed",
				Value = 10
			};
			yield return new GameObjectMutationUnit
			{
				Name = "Confusion",
				Level = 5
			};
			yield break;
		}

		// Token: 0x06007314 RID: 29460 RVA: 0x002B80D5 File Offset: 0x002B62D5
		private static IEnumerable<GameObjectUnit> UnitConvalessence(string Liquid)
		{
			yield return new GameObjectMutationUnit
			{
				Name = "Regeneration",
				Level = 10
			};
			yield break;
		}

		// Token: 0x06007315 RID: 29461 RVA: 0x002B80DE File Offset: 0x002B62DE
		private static IEnumerable<GameObjectUnit> UnitNeutronFlux(string Liquid)
		{
			yield return new GameObjectPartUnit
			{
				Part = new DetonateOnHit
				{
					Chance = 5,
					Force = 1200,
					Damage = "1d6"
				}
			};
			yield break;
		}

		// Token: 0x06007316 RID: 29462 RVA: 0x002B80E7 File Offset: 0x002B62E7
		private static IEnumerable<GameObjectUnit> UnitCloning(string Liquid)
		{
			yield return new GameObjectUnitAggregate("Two golems, each with one-third HP", new GameObjectUnit[]
			{
				new GameObjectAttributeUnit
				{
					Attribute = "Hitpoints",
					Value = -66,
					Percent = true
				},
				new GameObjectCloneUnit()
			});
			yield break;
		}

		// Token: 0x06007317 RID: 29463 RVA: 0x002B80F0 File Offset: 0x002B62F0
		private static IEnumerable<GameObjectUnit> UnitWax(string Liquid)
		{
			yield return new GameObjectAttributeUnit
			{
				Attribute = "HeatResistance",
				Value = 50
			};
			yield return new GameObjectPartUnit
			{
				Part = new AttackerElementalAmplifier
				{
					Attribute = "Heat",
					Amplification = 25
				}
			};
			yield return new GameObjectPartUnit
			{
				Part = new StickOnHit
				{
					Chance = 100
				},
				Description = "Melee attacks cause enemies to get stuck"
			};
			yield break;
		}

		// Token: 0x06007318 RID: 29464 RVA: 0x002B80F9 File Offset: 0x002B62F9
		private static IEnumerable<GameObjectUnit> UnitInk(string Liquid)
		{
			yield return new GameObjectSaveModifierUnit
			{
				Vs = "Slip",
				Value = SavingThrows.IMMUNITY
			};
			yield return new GameObjectAttributeUnit
			{
				Attribute = "SP",
				Value = 500
			};
			yield break;
		}

		// Token: 0x06007319 RID: 29465 RVA: 0x002B8102 File Offset: 0x002B6302
		private static IEnumerable<GameObjectUnit> UnitSap(string Liquid)
		{
			yield return new GameObjectMutationUnit
			{
				Name = "Burgeoning",
				Level = 5
			};
			yield return new GameObjectPartUnit
			{
				Part = new StickOnHit
				{
					Chance = 100
				},
				Description = "Melee attacks cause enemies to get stuck"
			};
			yield break;
		}

		// Token: 0x0600731A RID: 29466 RVA: 0x002B810B File Offset: 0x002B630B
		private static IEnumerable<GameObjectUnit> UnitBrainBrine(string Liquid)
		{
			yield return new GameObjectAttributeUnit
			{
				Attribute = "MP",
				Value = 6
			};
			yield break;
		}

		// Token: 0x0600731B RID: 29467 RVA: 0x002B8114 File Offset: 0x002B6314
		private static IEnumerable<GameObjectUnit> UnitAlgae(string Liquid)
		{
			yield return new GameObjectMutationUnit
			{
				Name = "Burgeoning",
				Level = 10
			};
			yield break;
		}

		// Token: 0x0600731C RID: 29468 RVA: 0x002B811D File Offset: 0x002B631D
		private static IEnumerable<GameObjectUnit> UnitSunSlag(string Liquid)
		{
			yield return new GameObjectAttributeUnit
			{
				Attribute = "Speed",
				Value = 15
			};
			yield break;
		}

		// Token: 0x0600731D RID: 29469 RVA: 0x002B8126 File Offset: 0x002B6326
		private static IEnumerable<GameObjectUnit> UnitWarmStatic(string Liquid)
		{
			yield return new GameObjectGolemQuestRandomUnit
			{
				SelectionID = "Catalyst",
				Amount = 2
			};
			yield break;
		}

		// Token: 0x04003531 RID: 13617
		public const string BASE_ID = "Catalyst";

		// Token: 0x04003532 RID: 13618
		public const int DRAMS = 3;

		// Token: 0x04003533 RID: 13619
		public string Liquid;

		// Token: 0x04003534 RID: 13620
		[NonSerialized]
		private GameObject IconVolume;

		// Token: 0x04003535 RID: 13621
		[NonSerialized]
		private List<string> _ValidMaterials = new List<string>(8);
	}
}
