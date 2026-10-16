using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using XRL.Language;
using XRL.Liquids;
using XRL.UI;
using XRL.World.Anatomy;

namespace XRL.World.Parts
{
	// Token: 0x0200114D RID: 4429
	[Serializable]
	public class ModLiquidCooled : IModification
	{
		// Token: 0x0600AFC5 RID: 44997 RVA: 0x0040BF68 File Offset: 0x0040A168
		public ModLiquidCooled()
		{
		}

		// Token: 0x0600AFC6 RID: 44998 RVA: 0x0040BF7B File Offset: 0x0040A17B
		public ModLiquidCooled(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600AFC7 RID: 44999 RVA: 0x0040BF8F File Offset: 0x0040A18F
		public override void Configure()
		{
			this.WorksOnSelf = true;
			base.IsTechScannable = true;
			this.NameForStatus = "CoolantSystem";
			this.ConsumesLiquid = "water";
			this.LiquidConsumptionChanceOneIn = 6;
		}

		// Token: 0x0600AFC8 RID: 45000 RVA: 0x0040BFBC File Offset: 0x0040A1BC
		public override void Attach()
		{
			if (this.ConsumesLiquid == null)
			{
				this.ConsumesLiquid = "water";
			}
			if (this.LiquidConsumptionChanceOneIn == 1)
			{
				this.LiquidConsumptionChanceOneIn = 6 + this.Tier;
			}
			base.Attach();
		}

		// Token: 0x0600AFC9 RID: 45001 RVA: 0x0040BFF0 File Offset: 0x0040A1F0
		public override bool ModificationApplicable(GameObject Object)
		{
			MissileWeapon part = Object.GetPart<MissileWeapon>();
			return part != null && part.ShotsPerAction > 1 && part.ShotsPerAction == part.AmmoPerAction;
		}

		// Token: 0x0600AFCA RID: 45002 RVA: 0x0040C025 File Offset: 0x0040A225
		public override bool BeingAppliedBy(GameObject Object, GameObject Who)
		{
			if (Who.IsPlayer())
			{
				this.EnforceLiquidVolume(Object).Empty(false, false);
			}
			return true;
		}

		// Token: 0x0600AFCB RID: 45003 RVA: 0x0040C040 File Offset: 0x0040A240
		public override void ApplyModification(GameObject Object)
		{
			MissileWeapon part = Object.GetPart<MissileWeapon>();
			if (part != null)
			{
				if (this.AmountBonus == 0)
				{
					if (this.PercentBonus == 0)
					{
						this.PercentBonus = this.PercentBonusRange.RollCached();
					}
					this.AmountBonus = Math.Max(part.ShotsPerAction * this.PercentBonus / 100, 1);
				}
				if (part.ShotsPerAnimation == part.ShotsPerAction)
				{
					part.ShotsPerAnimation += this.AmountBonus;
				}
				part.ShotsPerAction += this.AmountBonus;
				part.AmmoPerAction += this.AmountBonus;
			}
			this.LiquidConsumptionChanceOneIn = 6 + this.Tier;
			this.EnforceLiquidVolume(Object);
			base.IncreaseDifficultyAndComplexity(2, 2, null);
		}

		// Token: 0x0600AFCC RID: 45004 RVA: 0x0040C100 File Offset: 0x0040A300
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<AIWantUseWeaponEvent>.ID || ID == PooledEvent<CheckLoadAmmoEvent>.ID || ID == PooledEvent<CheckReadyToFireEvent>.ID || ID == SingletonEvent<CommandReloadEvent>.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == PooledEvent<GetMissileWeaponStatusEvent>.ID || ID == PooledEvent<GetNotReadyToFireMessageEvent>.ID || ID == GetPreferredLiquidEvent.ID || ID == GetShortDescriptionEvent.ID || ID == PooledEvent<LoadAmmoEvent>.ID || ID == SingletonEvent<NeedsReloadEvent>.ID;
		}

		// Token: 0x0600AFCD RID: 45005 RVA: 0x0040C174 File Offset: 0x0040A374
		public override bool HandleEvent(AIWantUseWeaponEvent E)
		{
			ActivePartStatus activePartStatus = base.GetActivePartStatus(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null);
			if (activePartStatus == ActivePartStatus.ProcessInputMissing || activePartStatus == ActivePartStatus.ProcessInputInvalid)
			{
				bool flag = false;
				GameObject actor = E.Actor;
				Inventory inventory = ((actor != null) ? actor.Inventory : null);
				if (inventory != null)
				{
					foreach (GameObject gameObject in inventory.GetObjectsDirect())
					{
						LiquidVolume liquidVolume = gameObject.LiquidVolume;
						if (liquidVolume != null && liquidVolume.Volume > 0 && (this.LiquidMustBePure ? liquidVolume.IsPureLiquid(this.ConsumesLiquid, false) : (liquidVolume.Amount(this.ConsumesLiquid) > 0)) && !liquidVolume.EffectivelySealed())
						{
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AFCE RID: 45006 RVA: 0x0040C264 File Offset: 0x0040A464
		public override bool HandleEvent(CheckLoadAmmoEvent E)
		{
			ActivePartStatus activePartStatus = base.GetActivePartStatus(false, false, false, false, false, false, false, false, E.ActivePartsIgnoreSubject, false, 1, null, false, 0L, null);
			if (activePartStatus != ActivePartStatus.Operational)
			{
				if (activePartStatus == ActivePartStatus.ProcessInputMissing || activePartStatus == ActivePartStatus.ProcessInputInvalid)
				{
					E.Message = this.GetStatusMessage(activePartStatus);
				}
				else if (E.Message == null)
				{
					E.Message = this.GetStatusMessage(activePartStatus);
				}
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AFCF RID: 45007 RVA: 0x0040C2DC File Offset: 0x0040A4DC
		public override bool HandleEvent(LoadAmmoEvent E)
		{
			ActivePartStatus activePartStatus = base.GetActivePartStatus(true, false, false, false, false, false, false, false, E.ActivePartsIgnoreSubject, false, 1, null, false, 0L, null);
			if (activePartStatus != ActivePartStatus.Operational)
			{
				if (activePartStatus == ActivePartStatus.ProcessInputMissing || activePartStatus == ActivePartStatus.ProcessInputInvalid)
				{
					E.Message = this.GetStatusMessage(activePartStatus);
				}
				else if (E.Message == null)
				{
					E.Message = this.GetStatusMessage(activePartStatus);
				}
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AFD0 RID: 45008 RVA: 0x0040C354 File Offset: 0x0040A554
		public override bool HandleEvent(CheckReadyToFireEvent E)
		{
			return !base.IsDisabled(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) && base.HandleEvent(E);
		}

		// Token: 0x0600AFD1 RID: 45009 RVA: 0x0040C394 File Offset: 0x0040A594
		public override bool HandleEvent(GetNotReadyToFireMessageEvent E)
		{
			ActivePartStatus activePartStatus = base.GetActivePartStatus(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null);
			if (activePartStatus != ActivePartStatus.Operational)
			{
				E.Message = this.GetStatusMessage(activePartStatus);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AFD2 RID: 45010 RVA: 0x0040C3E0 File Offset: 0x0040A5E0
		public override bool HandleEvent(GetMissileWeaponStatusEvent E)
		{
			if (E.Override == null)
			{
				LiquidVolume liquidVolume = this.ParentObject.LiquidVolume;
				string primaryLiquidColor = liquidVolume.GetPrimaryLiquidColor();
				if (liquidVolume.Volume == 0)
				{
					if (primaryLiquidColor != null)
					{
						E.Items.Append(" [{{").Append(primaryLiquidColor).Append('|')
							.Append("empty")
							.Append("}}]");
					}
					else
					{
						E.Items.Append(" [empty]");
					}
				}
				else if (!this.LiquidMustBePure || liquidVolume.IsPureLiquid(this.ConsumesLiquid, false))
				{
					if (primaryLiquidColor != null)
					{
						E.Items.Append(" [{{").Append(primaryLiquidColor).Append('|')
							.Append(liquidVolume.Volume)
							.Append("}}]");
					}
					else
					{
						E.Items.Append(" [").Append(liquidVolume.Volume).Append("]");
					}
				}
				else if (primaryLiquidColor != null)
				{
					E.Items.Append(" [{{").Append(primaryLiquidColor).Append("|?}}]");
				}
				else
				{
					E.Items.Append(" [?]");
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AFD3 RID: 45011 RVA: 0x0040C518 File Offset: 0x0040A718
		public override bool HandleEvent(NeedsReloadEvent E)
		{
			if (E.Skip != this && (E.Weapon == null || E.Weapon == this.ParentObject))
			{
				LiquidVolume liquidVolume = this.ParentObject.LiquidVolume;
				if (liquidVolume != null && (liquidVolume.Volume < liquidVolume.MaxVolume || (this.LiquidMustBePure && !liquidVolume.IsPureLiquid(this.ConsumesLiquid, false))) && this.ParentObject.IsEquippedProperly(null))
				{
					return false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AFD4 RID: 45012 RVA: 0x0040C590 File Offset: 0x0040A790
		public override bool HandleEvent(CommandReloadEvent E)
		{
			if ((E.Weapon == null || E.Weapon == this.ParentObject) && !E.CheckedForReload.Contains(this))
			{
				E.CheckedForReload.Add(this);
				if (!this.ParentObject.IsEquippedProperly(null) || E.MinimumCharge > 0)
				{
					return true;
				}
				LiquidVolume liquidVolume = this.ParentObject.LiquidVolume;
				if (liquidVolume.Volume >= liquidVolume.MaxVolume && (!this.LiquidMustBePure || liquidVolume.IsPureLiquid(this.ConsumesLiquid, false)))
				{
					if (E.Actor.IsPlayer())
					{
						IComponent<GameObject>.AddPlayerMessage(this.ParentObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " already full of " + liquidVolume.GetLiquidName() + ".", null, true);
					}
					return true;
				}
				E.NeededReload.Add(this);
				int freeDrams = E.Actor.GetFreeDrams(this.ConsumesLiquid, this.ParentObject, null, (GameObject o) => !o.HasPart<ModLiquidCooled>(), false);
				if (freeDrams <= 0)
				{
					if (E.Actor.IsPlayer())
					{
						IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
						{
							"You have no ",
							this.ConsumesLiquid,
							" for ",
							this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
							"."
						}), 'r', true);
					}
					return true;
				}
				E.TriedToReload.Add(this);
				string text = this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false);
				if (liquidVolume.Volume > 0 && this.LiquidMustBePure && !liquidVolume.IsPureLiquid(this.ConsumesLiquid, false))
				{
					if (E.Actor.IsPlayer())
					{
						IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
						{
							"You dump the ",
							liquidVolume.GetLiquidName(),
							" out of ",
							text,
							"."
						}), null, true);
					}
					liquidVolume.EmptyIntoCell(null, null);
					text = this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false);
				}
				int num = liquidVolume.MaxVolume - liquidVolume.Volume;
				int num2 = Math.Min(freeDrams, num);
				E.Actor.UseDrams(num2, this.ConsumesLiquid, this.ParentObject, null, (GameObject o) => !o.HasPart<ModLiquidCooled>(), null, false);
				liquidVolume.MixWith(new LiquidVolume(this.ConsumesLiquid, num2), null, null, null, false);
				E.Reloaded.Add(this);
				if (!E.ObjectsReloaded.Contains(this.ParentObject))
				{
					E.ObjectsReloaded.Add(this.ParentObject);
				}
				E.EnergyCost(1000);
				if (E.Actor.IsPlayer())
				{
					IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
					{
						"You ",
						(liquidVolume.Volume < liquidVolume.MaxVolume) ? "partially " : "",
						"fill ",
						text,
						" with ",
						liquidVolume.GetLiquidName(),
						"."
					}), null, true);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AFD5 RID: 45013 RVA: 0x0040C92C File Offset: 0x0040AB2C
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				string text = "{{";
				BaseLiquid liquid = LiquidVolume.GetLiquid(this.ConsumesLiquid);
				E.AddAdjective(text + (((liquid != null) ? liquid.GetColor() : null) ?? "K") + "|liquid-cooled}}", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AFD6 RID: 45014 RVA: 0x0040C990 File Offset: 0x0040AB90
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Base.Compound(this.GetPhysicalDescription());
			E.Postfix.AppendRules(this.GetInstanceDescription());
			return base.HandleEvent(E);
		}

		// Token: 0x0600AFD7 RID: 45015 RVA: 0x0040C9BD File Offset: 0x0040ABBD
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600AFD8 RID: 45016 RVA: 0x0040C9C0 File Offset: 0x0040ABC0
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("GenerateIntegratedHostInitialAmmo");
			Registrar.Register("PrepIntegratedHostToReceiveAmmo");
			Registrar.Register("SupplyIntegratedHostWithAmmo");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AFD9 RID: 45017 RVA: 0x0040C9EC File Offset: 0x0040ABEC
		public override bool FireEvent(Event E)
		{
			if (E.ID == "PrepIntegratedHostToReceiveAmmo")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Host");
				if (gameObjectParameter == null)
				{
					goto IL_03E6;
				}
				Body body = gameObjectParameter.Body;
				if (body == null)
				{
					goto IL_03E6;
				}
				using (List<BodyPart>.Enumerator enumerator = body.GetPart("Arm").GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						BodyPart bodyPart = enumerator.Current;
						if (bodyPart.Equipped == null)
						{
							gameObjectParameter.ForceEquipObject(GameObject.Create("StorageTank"), bodyPart, true, new int?(0));
						}
					}
					goto IL_03E6;
				}
			}
			if (E.ID == "SupplyIntegratedHostWithAmmo")
			{
				if (E.HasFlag("TrackSupply"))
				{
					E.SetFlag("AnySupplyHandler", true);
				}
				GameObject gameObjectParameter2 = E.GetGameObjectParameter("Host");
				GameObject gameObject = E.GetGameObjectParameter("Actor") ?? E.GetGameObjectParameter("Owner");
				if (gameObjectParameter2 != null && gameObject != null)
				{
					int freeDrams = gameObject.GetFreeDrams(this.ConsumesLiquid, this.ParentObject, null, null, false);
					int storableDrams = gameObjectParameter2.GetStorableDrams(this.ConsumesLiquid, null, null, null, true, null);
					int num = Math.Min(freeDrams, storableDrams);
					if (gameObject.IsPlayer())
					{
						if (num > 0)
						{
							if (E.HasFlag("TrackSupply"))
							{
								E.SetFlag("AnySupplies", true);
							}
							num.ToString();
							int? num2 = Popup.AskNumber(string.Concat(new string[]
							{
								"Supply ",
								gameObjectParameter2.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
								" with how many drams of your ",
								this.ConsumesLiquid,
								"? (max=",
								num.ToString(),
								")"
							}), "Sounds/UI/ui_notification", "", num, 0, num);
							int num3 = 0;
							try
							{
								num3 = Convert.ToInt32(num2);
							}
							catch
							{
								goto IL_03E6;
							}
							if (num3 > num)
							{
								num3 = num;
							}
							if (num3 < 0)
							{
								num3 = 0;
							}
							num = num3;
						}
						else if (freeDrams <= 0)
						{
							Popup.Show(string.Concat(new string[]
							{
								"You have no ",
								this.ConsumesLiquid,
								" to supply ",
								gameObjectParameter2.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
								" with."
							}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
						}
						else if (storableDrams <= 0)
						{
							Popup.Show(gameObjectParameter2.Does("have", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " no room for more " + this.ConsumesLiquid + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
						}
					}
					if (num > 0)
					{
						IComponent<GameObject>.XDidYToZ(gameObject, "transfer", string.Concat(new string[]
						{
							Grammar.Cardinal(num),
							" ",
							(num == 1) ? "dram" : "drams",
							" of ",
							this.ConsumesLiquid,
							" to"
						}), gameObjectParameter2, null, null, null, null, null, null, false, false, false, true, false, null, null, null, false, false, false, true, false, null);
						gameObject.UseDrams(num, this.ConsumesLiquid, null, null, null, null, false);
						gameObjectParameter2.GiveDrams(num, this.ConsumesLiquid, false, null, null, null, null, true, null);
						gameObject.UseEnergy(1000, "Ammo Liquid Transfer", null, null, false);
					}
				}
			}
			else if (E.ID == "GenerateIntegratedHostInitialAmmo")
			{
				GameObject gameObjectParameter3 = E.GetGameObjectParameter("Host");
				if (gameObjectParameter3 != null)
				{
					int storableDrams2 = gameObjectParameter3.GetStorableDrams(this.ConsumesLiquid, null, null, null, true, null);
					if (storableDrams2 > 0)
					{
						gameObjectParameter3.GiveDrams(storableDrams2, this.ConsumesLiquid, false, null, null, null, null, true, null);
					}
				}
			}
			IL_03E6:
			return base.FireEvent(E);
		}

		// Token: 0x0600AFDA RID: 45018 RVA: 0x0040CE04 File Offset: 0x0040B004
		public string GetPhysicalDescription()
		{
			return "";
		}

		// Token: 0x0600AFDB RID: 45019 RVA: 0x0040CE0C File Offset: 0x0040B00C
		public static string GetDescription(int Tier)
		{
			return "Liquid-cooled: This weapon's rate of fire is increased, but it requires pure water to function. When fired, there's a one in " + (6 + Tier).ToString() + " chance that 1 dram is consumed.";
		}

		// Token: 0x0600AFDC RID: 45020 RVA: 0x0040CE34 File Offset: 0x0040B034
		public string GetInstanceDescription()
		{
			return string.Concat(new string[]
			{
				"Liquid-cooled: This weapon's rate of fire is increased by ",
				this.AmountBonus.ToString(),
				", but ",
				this.ParentObject.it,
				" requires ",
				this.LiquidMustBePure ? "pure " : "",
				" ",
				ColorUtility.StripFormatting(LiquidVolume.GetLiquid(this.ConsumesLiquid).GetName()),
				" to function. When fired, there's a one in ",
				this.LiquidConsumptionChanceOneIn.ToString(),
				" chance that 1 dram is consumed."
			});
		}

		// Token: 0x0600AFDD RID: 45021 RVA: 0x0040CEE0 File Offset: 0x0040B0E0
		public LiquidVolume EnforceLiquidVolume(GameObject Object)
		{
			int liquidVolumeSize = ModLiquidCooled.GetLiquidVolumeSize(Object);
			if (liquidVolumeSize == 0)
			{
				return null;
			}
			LiquidVolume liquidVolume = Object.LiquidVolume;
			if (liquidVolume == null)
			{
				liquidVolume = new LiquidVolume();
				liquidVolume.MaxVolume = liquidVolumeSize;
				liquidVolume.Volume = liquidVolumeSize;
				liquidVolume.SetComponent(this.ConsumesLiquid, 1000);
				Object.AddPart<LiquidVolume>(liquidVolume, true, false);
			}
			else if (liquidVolume.MaxVolume < liquidVolumeSize)
			{
				liquidVolume.MaxVolume = liquidVolumeSize;
			}
			return liquidVolume;
		}

		// Token: 0x0600AFDE RID: 45022 RVA: 0x0040CF45 File Offset: 0x0040B145
		public LiquidVolume EnforceLiquidVolume()
		{
			return this.EnforceLiquidVolume(this.ParentObject);
		}

		// Token: 0x0600AFDF RID: 45023 RVA: 0x0040CF54 File Offset: 0x0040B154
		public static int GetLiquidVolumeSizeForShotsPerAction(int Shots)
		{
			if (Shots <= 1)
			{
				return 0;
			}
			int num = Shots * 2;
			int num2 = num % 8;
			if (num2 != 0)
			{
				num += 8 - num2;
			}
			return num;
		}

		// Token: 0x0600AFE0 RID: 45024 RVA: 0x0040CF7C File Offset: 0x0040B17C
		public static int GetLiquidVolumeSize(GameObject Object)
		{
			MissileWeapon missileWeapon;
			if (Object.TryGetPart<MissileWeapon>(out missileWeapon))
			{
				return ModLiquidCooled.GetLiquidVolumeSizeForShotsPerAction(missileWeapon.ShotsPerAction);
			}
			return 0;
		}

		// Token: 0x0600AFE1 RID: 45025 RVA: 0x0040CFA0 File Offset: 0x0040B1A0
		public int GetLiquidVolumeSize()
		{
			return ModLiquidCooled.GetLiquidVolumeSize(this.ParentObject);
		}

		// Token: 0x0600AFE2 RID: 45026 RVA: 0x0040CFB0 File Offset: 0x0040B1B0
		public string GetStatusMessage(ActivePartStatus Status)
		{
			if (Status == ActivePartStatus.ProcessInputMissing || Status == ActivePartStatus.ProcessInputInvalid)
			{
				return this.ParentObject.Does("emit", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " a grinding noise.";
			}
			return this.ParentObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " " + this.GetStatusPhrase(Status) + ".";
		}

		// Token: 0x0600AFE3 RID: 45027 RVA: 0x0040D04C File Offset: 0x0040B24C
		public string GetStatusMessage()
		{
			return this.GetStatusMessage(base.GetActivePartStatus(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null));
		}

		// Token: 0x04004804 RID: 18436
		public const int LIQUID_CONSUMPTION_CHANCE_BASE = 6;

		// Token: 0x04004805 RID: 18437
		public string PercentBonusRange = "30-60";

		// Token: 0x04004806 RID: 18438
		public int PercentBonus;

		// Token: 0x04004807 RID: 18439
		public int AmountBonus;
	}
}
