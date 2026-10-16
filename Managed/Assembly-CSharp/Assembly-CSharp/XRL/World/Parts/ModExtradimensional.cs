using System;
using Qud.API;
using XRL.Rules;
using XRL.UI;
using XRL.Wish;
using XRL.World.Effects;
using XRL.World.Encounters;

namespace XRL.World.Parts
{
	// Token: 0x02001136 RID: 4406
	[HasWishCommand]
	[Serializable]
	public class ModExtradimensional : IModification
	{
		// Token: 0x0600AE9D RID: 44701 RVA: 0x0040779C File Offset: 0x0040599C
		public ModExtradimensional()
		{
		}

		// Token: 0x0600AE9E RID: 44702 RVA: 0x004077A4 File Offset: 0x004059A4
		public ModExtradimensional(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600AE9F RID: 44703 RVA: 0x004077B0 File Offset: 0x004059B0
		public ModExtradimensional(int WeaponModIndex, int MissileWeaponModIndex, int ArmorModIndex, int ShieldModIndex, int MiscModIndex, string Training, string DimensionName, string SecretID)
		{
			this.WeaponModIndex = WeaponModIndex;
			this.MissileWeaponModIndex = MissileWeaponModIndex;
			this.ArmorModIndex = ArmorModIndex;
			this.ShieldModIndex = ShieldModIndex;
			this.MiscModIndex = MiscModIndex;
			this.Training = Training;
			this.DimensionName = DimensionName;
			this.SecretID = SecretID;
		}

		// Token: 0x0600AEA0 RID: 44704 RVA: 0x00407800 File Offset: 0x00405A00
		public ModExtradimensional(ModExtradimensional Source)
		{
			this.WeaponModIndex = Source.WeaponModIndex;
			this.MissileWeaponModIndex = Source.MissileWeaponModIndex;
			this.ArmorModIndex = Source.ArmorModIndex;
			this.ShieldModIndex = Source.ShieldModIndex;
			this.MiscModIndex = Source.MiscModIndex;
			this.Training = Source.Training;
			this.DimensionName = Source.DimensionName;
			this.SecretID = Source.SecretID;
		}

		// Token: 0x0600AEA1 RID: 44705 RVA: 0x00407874 File Offset: 0x00405A74
		public override void Configure()
		{
			this.WorksOnEquipper = true;
			this.NameForStatus = "***ERROR***";
			if (The.Game == null || !The.Game.HasObjectGameState("DimensionManager"))
			{
				this.WeaponModIndex = 0;
				this.MissileWeaponModIndex = 0;
				this.ArmorModIndex = 0;
				this.ShieldModIndex = 0;
				this.MiscModIndex = 0;
				this.Training = null;
				this.DimensionName = "The Dead Square";
				return;
			}
			DimensionManager dimensionManager = The.Game.GetObjectGameState("DimensionManager") as DimensionManager;
			int num = Stat.Random(0, dimensionManager.ExtraDimensions.Count + dimensionManager.PsychicFactions.Count - 1);
			if (num < dimensionManager.ExtraDimensions.Count)
			{
				ExtraDimension extraDimension = dimensionManager.ExtraDimensions[num];
				this.WeaponModIndex = extraDimension.WeaponIndex;
				this.MissileWeaponModIndex = extraDimension.MissileWeaponIndex;
				this.ArmorModIndex = extraDimension.ArmorIndex;
				this.ShieldModIndex = extraDimension.ShieldIndex;
				this.MiscModIndex = extraDimension.MiscIndex;
				this.Training = extraDimension.Training;
				this.DimensionName = extraDimension.Name.Replace("*DimensionSymbol*", ((char)extraDimension.Symbol).ToString());
				this.SecretID = extraDimension.SecretID;
				return;
			}
			num -= dimensionManager.ExtraDimensions.Count;
			PsychicFaction psychicFaction = dimensionManager.PsychicFactions[num];
			this.WeaponModIndex = psychicFaction.dimensionalWeaponIndex;
			this.MissileWeaponModIndex = psychicFaction.dimensionalMissileWeaponIndex;
			this.ArmorModIndex = psychicFaction.dimensionalArmorIndex;
			this.ShieldModIndex = psychicFaction.dimensionalShieldIndex;
			this.MiscModIndex = psychicFaction.dimensionalMiscIndex;
			this.Training = psychicFaction.dimensionalTraining;
			this.DimensionName = psychicFaction.dimensionName.Replace("*DimensionSymbol*", ((char)psychicFaction.dimensionSymbol).ToString());
			this.SecretID = psychicFaction.dimensionSecretID;
		}

		// Token: 0x0600AEA2 RID: 44706 RVA: 0x00407A4C File Offset: 0x00405C4C
		public override void ApplyModification(GameObject Object)
		{
			this.AddDimensionalBonus(Object);
			base.IncreaseDifficultyAndComplexityIfComplex(5, 1, null);
			Object.RequirePart<AnimatedMaterialExtradimensional>(false);
			Object.SetImportant(true, true, false);
		}

		// Token: 0x0600AEA3 RID: 44707 RVA: 0x00407A6F File Offset: 0x00405C6F
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600AEA4 RID: 44708 RVA: 0x00407A72 File Offset: 0x00405C72
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID || ID == GetShortDescriptionEvent.ID || ID == PooledEvent<RealityStabilizeEvent>.ID || ID == WasDerivedFromEvent.ID;
		}

		// Token: 0x0600AEA5 RID: 44709 RVA: 0x00407AA8 File Offset: 0x00405CA8
		public override bool HandleEvent(WasDerivedFromEvent E)
		{
			E.ApplyToEach(delegate(GameObject obj)
			{
				obj.AddPart<ModExtradimensional>(new ModExtradimensional(this), true, false);
			});
			return base.HandleEvent(E);
		}

		// Token: 0x0600AEA6 RID: 44710 RVA: 0x00407AC3 File Offset: 0x00405CC3
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("{{extradimensional|extradimensional}}", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AEA7 RID: 44711 RVA: 0x00407AED File Offset: 0x00405CED
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(this.GetDescription(this.Tier));
			return base.HandleEvent(E);
		}

		// Token: 0x0600AEA8 RID: 44712 RVA: 0x00407B10 File Offset: 0x00405D10
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				E.Add("chance", 3);
				E.Add("travel", 2);
				E.Add("might", 1);
				E.Add("stars", 1);
				E.Add("time", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AEA9 RID: 44713 RVA: 0x00407B70 File Offset: 0x00405D70
		public override bool HandleEvent(RealityStabilizeEvent E)
		{
			if (!E.Projecting && E.Check(false) && 1.in1000())
			{
				this.ParentObject.ApplyEffect(new Rusted(), null);
				if (this.ParentObject == null || this.ParentObject.IsInvalid())
				{
					return false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AEAA RID: 44714 RVA: 0x00407BC6 File Offset: 0x00405DC6
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600AEAB RID: 44715 RVA: 0x00407BC9 File Offset: 0x00405DC9
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterLookedAt");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AEAC RID: 44716 RVA: 0x00407BDE File Offset: 0x00405DDE
		public string GetDescription(int Tier)
		{
			return "Extradimensional: This item recently materialized in this dimension having inherited some properties from its home dimension, {{O|" + this.DimensionName + "}}.\n";
		}

		// Token: 0x0600AEAD RID: 44717 RVA: 0x00407BF8 File Offset: 0x00405DF8
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AfterLookedAt" && !this.LookedAt && !string.IsNullOrEmpty(this.SecretID) && this.ParentObject.Understood())
			{
				this.LookedAt = true;
				JournalAPI.RevealObservation(this.SecretID, true);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600AEAE RID: 44718 RVA: 0x00407C54 File Offset: 0x00405E54
		public void AddDimensionalBonus(GameObject obj = null)
		{
			if (obj == null)
			{
				obj = this.ParentObject;
			}
			if (obj.HasTag("MeleeWeapon"))
			{
				if (this.WeaponModIndex == 0)
				{
					obj.GetPart<MeleeWeapon>().PenBonus++;
				}
				else if (this.WeaponModIndex == 1)
				{
					obj.GetPart<MeleeWeapon>().AdjustDamageDieSize(2);
				}
				else if (this.WeaponModIndex == 2)
				{
					obj.GetPart<MeleeWeapon>().AdjustDamage(1);
				}
				else if (this.WeaponModIndex == 3)
				{
					obj.GetPart<MeleeWeapon>().AdjustBonusCap(3);
				}
				else if (this.WeaponModIndex == 4)
				{
					obj.GetPart<MeleeWeapon>().HitBonus += 2;
				}
				else if (this.WeaponModIndex == 5)
				{
					EquipStatBoost.AppendBoostOnEquip(obj, "MA:1", null, false);
				}
				else if (this.WeaponModIndex == 6)
				{
					EquipStatBoost.AppendBoostOnEquip(obj, "Strength:1", null, false);
				}
				else if (this.WeaponModIndex == 7)
				{
					EquipStatBoost.AppendBoostOnEquip(obj, "Agility:1", null, false);
				}
				else if (this.WeaponModIndex == 8)
				{
					EquipStatBoost.AppendBoostOnEquip(obj, "Toughness:1", null, false);
				}
				else if (this.WeaponModIndex == 9)
				{
					EquipStatBoost.AppendBoostOnEquip(obj, "Intelligence:1", null, false);
				}
				else if (this.WeaponModIndex == 10)
				{
					EquipStatBoost.AppendBoostOnEquip(obj, "Willpower:1", null, false);
				}
				else if (this.WeaponModIndex == 11)
				{
					EquipStatBoost.AppendBoostOnEquip(obj, "Ego:1", null, false);
				}
				else if (this.WeaponModIndex == 12)
				{
					if (!obj.HasPart<ModGlazed>())
					{
						obj.AddPart<ModGlazed>(new ModGlazed(1), true, false);
					}
				}
				else if (this.WeaponModIndex == 13)
				{
					if (!obj.HasPart<ModImprovedLightManipulation>())
					{
						obj.AddPart<ModImprovedLightManipulation>(new ModImprovedLightManipulation(1), true, false);
					}
				}
				else if (this.WeaponModIndex == 14)
				{
					if (!obj.HasPart<ModImprovedTemporalFugue>())
					{
						obj.AddPart<ModImprovedTemporalFugue>(new ModImprovedTemporalFugue(1), true, false);
					}
				}
				else if (this.WeaponModIndex == 15)
				{
					if (!obj.HasPart<ModBeetlehost>())
					{
						obj.AddPart<ModBeetlehost>(new ModBeetlehost(1), true, false);
					}
				}
				else if (this.WeaponModIndex == 16)
				{
					if (!obj.HasPart<ModFatecaller>())
					{
						obj.AddPart<ModFatecaller>(new ModFatecaller(1), true, false);
					}
				}
				else if (this.WeaponModIndex == 17 && !obj.HasPart<ModImprovedElectricalGeneration>())
				{
					obj.AddPart<ModImprovedElectricalGeneration>(new ModImprovedElectricalGeneration(1), true, false);
				}
			}
			else if (obj.HasPart<MissileWeapon>())
			{
				MissileWeapon part = obj.GetPart<MissileWeapon>();
				if (this.MissileWeaponModIndex == 0)
				{
					part.NoWildfire = true;
				}
				else if (this.MissileWeaponModIndex == 1)
				{
					if (part.AmmoPerAction == part.ShotsPerAction)
					{
						part.AmmoPerAction++;
					}
					part.ShotsPerAction++;
					MagazineAmmoLoader part2 = obj.GetPart<MagazineAmmoLoader>();
					if (part2 != null && part2.MaxAmmo < part.AmmoPerAction)
					{
						part2.MaxAmmo = part.AmmoPerAction;
					}
				}
				else if (this.MissileWeaponModIndex == 2)
				{
					obj.RequirePart<MissilePerformance>(false).PenetrationModifier++;
				}
				else if (this.MissileWeaponModIndex == 3)
				{
					obj.RequirePart<MissilePerformance>(false).DamageDieModifier += 2;
				}
				else if (this.MissileWeaponModIndex == 4)
				{
					obj.RequirePart<MissilePerformance>(false).DamageModifier++;
				}
				else if (this.MissileWeaponModIndex == 5)
				{
					obj.RequirePart<MissilePerformance>(false).PenetrateCreatures = new bool?(true);
				}
				else if (this.MissileWeaponModIndex == 6)
				{
					obj.RequirePart<MissilePerformance>(false).WantAddAttribute("Vorpal");
				}
				else if (this.MissileWeaponModIndex == 7)
				{
					if (part.WeaponAccuracy > 0)
					{
						part.WeaponAccuracy = Math.Max(part.WeaponAccuracy - Stat.Random(5, 10), 0);
					}
					else
					{
						part.AimVarianceBonus += Stat.Random(2, 6);
					}
				}
				else if (this.MissileWeaponModIndex == 8)
				{
					EquipStatBoost.AppendBoostOnEquip(obj, "MA:1", null, false);
				}
				else if (this.MissileWeaponModIndex == 9)
				{
					EquipStatBoost.AppendBoostOnEquip(obj, "Strength:1", null, false);
				}
				else if (this.MissileWeaponModIndex == 10)
				{
					EquipStatBoost.AppendBoostOnEquip(obj, "Agility:1", null, false);
				}
				else if (this.MissileWeaponModIndex == 11)
				{
					EquipStatBoost.AppendBoostOnEquip(obj, "Toughness:1", null, false);
				}
				else if (this.MissileWeaponModIndex == 12)
				{
					EquipStatBoost.AppendBoostOnEquip(obj, "Intelligence:1", null, false);
				}
				else if (this.MissileWeaponModIndex == 13)
				{
					EquipStatBoost.AppendBoostOnEquip(obj, "Willpower:1", null, false);
				}
				else if (this.MissileWeaponModIndex == 14)
				{
					EquipStatBoost.AppendBoostOnEquip(obj, "Ego:1", null, false);
				}
				else if (this.MissileWeaponModIndex == 15)
				{
					if (!obj.HasPart<ModImprovedLightManipulation>())
					{
						obj.AddPart<ModImprovedLightManipulation>(new ModImprovedLightManipulation(1), true, false);
					}
				}
				else if (this.MissileWeaponModIndex == 16)
				{
					if (!obj.HasPart<ModImprovedTemporalFugue>())
					{
						obj.AddPart<ModImprovedTemporalFugue>(new ModImprovedTemporalFugue(1), true, false);
					}
				}
				else if (this.MissileWeaponModIndex == 17 && !obj.HasPart<ModImprovedElectricalGeneration>())
				{
					obj.AddPart<ModImprovedElectricalGeneration>(new ModImprovedElectricalGeneration(1), true, false);
				}
			}
			else if (obj.HasPart<Armor>())
			{
				Armor part3 = obj.GetPart<Armor>();
				if (this.ArmorModIndex == 0)
				{
					Armor armor = part3;
					int num = armor.DV;
					armor.DV = num + 1;
				}
				else if (this.ArmorModIndex == 1)
				{
					Armor armor2 = part3;
					int num = armor2.AV;
					armor2.AV = num + 1;
				}
				else if (this.ArmorModIndex == 2)
				{
					part3.MA += 2;
				}
				else if (this.ArmorModIndex == 3)
				{
					part3.Acid += 10;
				}
				else if (this.ArmorModIndex == 4)
				{
					part3.Cold += 10;
				}
				else if (this.ArmorModIndex == 5)
				{
					part3.Heat += 10;
				}
				else if (this.ArmorModIndex == 6)
				{
					part3.Elec += 10;
				}
				else if (this.ArmorModIndex == 7)
				{
					part3.Strength++;
				}
				else if (this.ArmorModIndex == 8)
				{
					part3.Agility++;
				}
				else if (this.ArmorModIndex == 9)
				{
					part3.Toughness++;
				}
				else if (this.ArmorModIndex == 10)
				{
					part3.Intelligence++;
				}
				else if (this.ArmorModIndex == 11)
				{
					part3.Willpower++;
				}
				else if (this.ArmorModIndex == 12)
				{
					part3.Ego++;
				}
				else if (this.ArmorModIndex == 13)
				{
					part3.ToHit += 2;
				}
				else if (this.ArmorModIndex == 14)
				{
					if (part3.SpeedPenalty > 0)
					{
						part3.SpeedPenalty = Math.Max(part3.SpeedPenalty - Stat.Random(5, 10), 0);
					}
					else
					{
						part3.SpeedBonus += Stat.Random(1, 5);
					}
				}
				else if (this.ArmorModIndex == 15)
				{
					if (!obj.HasPart<ModGlassArmor>())
					{
						obj.AddPart<ModGlassArmor>(new ModGlassArmor(1), true, false);
					}
				}
				else if (this.ArmorModIndex == 16)
				{
					if (!obj.HasPart<ModImprovedLightManipulation>())
					{
						obj.AddPart<ModImprovedLightManipulation>(new ModImprovedLightManipulation(1), true, false);
					}
				}
				else if (this.ArmorModIndex == 17)
				{
					if (!obj.HasPart<ModImprovedTemporalFugue>())
					{
						obj.AddPart<ModImprovedTemporalFugue>(new ModImprovedTemporalFugue(1), true, false);
					}
				}
				else if (this.ArmorModIndex == 18)
				{
					if (!obj.HasPart<ModBlinkEscape>())
					{
						obj.AddPart<ModBlinkEscape>(new ModBlinkEscape(1), true, false);
					}
				}
				else if (this.ArmorModIndex == 19 && !obj.HasPart<ModImprovedElectricalGeneration>())
				{
					obj.AddPart<ModImprovedElectricalGeneration>(new ModImprovedElectricalGeneration(1), true, false);
				}
			}
			else if (obj.HasPart<Shield>())
			{
				Shield part4 = obj.GetPart<Shield>();
				if (this.ShieldModIndex == 0)
				{
					if (!obj.HasPart<ModImprovedBlock>())
					{
						obj.AddPart<ModImprovedBlock>(new ModImprovedBlock(1), true, false);
					}
				}
				else if (this.ShieldModIndex == 1)
				{
					if (part4.DV < 0)
					{
						part4.DV++;
						if (part4.DV < 0 && 50.in100())
						{
							part4.DV++;
						}
					}
				}
				else if (this.ShieldModIndex == 2)
				{
					if (part4.SpeedPenalty > 0)
					{
						part4.SpeedPenalty = Math.Max(part4.SpeedPenalty - Stat.Random(5, 10), 0);
					}
				}
				else if (this.ShieldModIndex == 3)
				{
					part4.AV++;
				}
				else if (this.ShieldModIndex == 4)
				{
					EquipStatBoost.AppendBoostOnEquip(obj, "MA:1", null, false);
				}
				else if (this.ShieldModIndex == 5)
				{
					EquipStatBoost.AppendBoostOnEquip(obj, "Strength:1", null, false);
				}
				else if (this.ShieldModIndex == 6)
				{
					EquipStatBoost.AppendBoostOnEquip(obj, "Agility:1", null, false);
				}
				else if (this.ShieldModIndex == 7)
				{
					EquipStatBoost.AppendBoostOnEquip(obj, "Toughness:1", null, false);
				}
				else if (this.ShieldModIndex == 8)
				{
					EquipStatBoost.AppendBoostOnEquip(obj, "Willpower:1", null, false);
				}
				else if (this.ShieldModIndex == 9)
				{
					EquipStatBoost.AppendBoostOnEquip(obj, "Intelligence:1", null, false);
				}
				else if (this.ShieldModIndex == 10)
				{
					EquipStatBoost.AppendBoostOnEquip(obj, "Ego:1", null, false);
				}
				else if (this.ShieldModIndex == 11)
				{
					EquipStatBoost.AppendBoostOnEquip(obj, "AcidResistance:10", null, false);
				}
				else if (this.ShieldModIndex == 12)
				{
					EquipStatBoost.AppendBoostOnEquip(obj, "ColdResistance:10", null, false);
				}
				else if (this.ShieldModIndex == 13)
				{
					EquipStatBoost.AppendBoostOnEquip(obj, "HeatResistance:10", null, false);
				}
				else if (this.ShieldModIndex == 14)
				{
					EquipStatBoost.AppendBoostOnEquip(obj, "ElectricResistance:10", null, false);
				}
				else if (this.ShieldModIndex == 15)
				{
					if (!obj.HasPart<ModGlassArmor>())
					{
						obj.AddPart<ModGlassArmor>(new ModGlassArmor(1), true, false);
					}
				}
				else if (this.ShieldModIndex == 16)
				{
					if (!obj.HasPart<ModImprovedLightManipulation>())
					{
						obj.AddPart<ModImprovedLightManipulation>(new ModImprovedLightManipulation(1), true, false);
					}
				}
				else if (this.ShieldModIndex == 17)
				{
					if (!obj.HasPart<ModImprovedTemporalFugue>())
					{
						obj.AddPart<ModImprovedTemporalFugue>(new ModImprovedTemporalFugue(1), true, false);
					}
				}
				else if (this.ShieldModIndex == 18 && !obj.HasPart<ModImprovedElectricalGeneration>())
				{
					obj.AddPart<ModImprovedElectricalGeneration>(new ModImprovedElectricalGeneration(1), true, false);
				}
			}
			else if (obj.GetInventoryCategory(false) == "Books")
			{
				TrainingBook trainingBook = obj.RequirePart<TrainingBook>(false);
				if (Statistic.Attributes.Contains(this.Training))
				{
					trainingBook.Attribute = this.Training;
					trainingBook.Skill = null;
				}
				else
				{
					trainingBook.Attribute = null;
					trainingBook.Skill = this.Training;
				}
			}
			Commerce part5 = obj.GetPart<Commerce>();
			if (part5 != null)
			{
				part5.Value *= 2.0;
			}
		}

		// Token: 0x0600AEAF RID: 44719 RVA: 0x0040876C File Offset: 0x0040696C
		[WishCommand(null, null)]
		public static void MakeExtradimensional()
		{
			GameObject gameObject = Popup.PickGameObject("Pick item", The.Player.GetInventoryAndEquipment(), true, true, true, null, 0, false);
			if (gameObject == null)
			{
				return;
			}
			gameObject.AddPart<ModExtradimensional>(new ModExtradimensional(), true, false);
		}

		// Token: 0x040047F1 RID: 18417
		public int WeaponModIndex;

		// Token: 0x040047F2 RID: 18418
		public int MissileWeaponModIndex;

		// Token: 0x040047F3 RID: 18419
		public int ArmorModIndex;

		// Token: 0x040047F4 RID: 18420
		public int ShieldModIndex;

		// Token: 0x040047F5 RID: 18421
		public int MiscModIndex;

		// Token: 0x040047F6 RID: 18422
		public string Training;

		// Token: 0x040047F7 RID: 18423
		public string DimensionName;

		// Token: 0x040047F8 RID: 18424
		public string SecretID;

		// Token: 0x040047F9 RID: 18425
		public bool LookedAt;
	}
}
