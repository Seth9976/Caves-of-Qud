using System;
using System.Collections.Generic;
using XRL.Language;
using XRL.UI;
using XRL.World.Anatomy;

namespace XRL.World.Parts
{
	// Token: 0x02001101 RID: 4353
	[Serializable]
	public class BioAmmoLoader : IActivePart
	{
		// Token: 0x0600AC2D RID: 44077 RVA: 0x003F184C File Offset: 0x003EFA4C
		public BioAmmoLoader()
		{
			this.WorksOnSelf = true;
			base.IsBioScannable = true;
		}

		// Token: 0x0600AC2E RID: 44078 RVA: 0x003F1884 File Offset: 0x003EFA84
		public override bool SameAs(IPart p)
		{
			BioAmmoLoader bioAmmoLoader = p as BioAmmoLoader;
			return bioAmmoLoader.MaxCapacity == this.MaxCapacity && bioAmmoLoader.Available == this.Available && bioAmmoLoader.TurnsToGenerate == this.TurnsToGenerate && !(bioAmmoLoader.ProjectileObject != this.ProjectileObject) && !(bioAmmoLoader.StartAvailable != this.StartAvailable) && !(bioAmmoLoader.VariableTurnsToGenerate != this.VariableTurnsToGenerate) && bioAmmoLoader.TurnsToGenerateComputePowerFactor == this.TurnsToGenerateComputePowerFactor && base.SameAs(p);
		}

		// Token: 0x0600AC2F RID: 44079 RVA: 0x003F1920 File Offset: 0x003EFB20
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<AIWantUseWeaponEvent>.ID || (ID == AllowLiquidCollectionEvent.ID && !this.ConsumesLiquid.IsNullOrEmpty()) || ID == PooledEvent<CheckLoadAmmoEvent>.ID || ID == PooledEvent<CheckReadyToFireEvent>.ID || ID == SingletonEvent<CommandReloadEvent>.ID || ID == SingletonEvent<EndTurnEvent>.ID || ID == PooledEvent<GetAmmoCountAvailableEvent>.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == PooledEvent<GetMissileWeaponProjectileEvent>.ID || ID == PooledEvent<GetMissileWeaponStatusEvent>.ID || ID == PooledEvent<GetNotReadyToFireMessageEvent>.ID || ID == PooledEvent<GetProjectileBlueprintEvent>.ID || ID == GetShortDescriptionEvent.ID || ID == PooledEvent<LoadAmmoEvent>.ID || ID == SingletonEvent<NeedsReloadEvent>.ID || ID == ObjectCreatedEvent.ID;
		}

		// Token: 0x0600AC30 RID: 44080 RVA: 0x003F19CC File Offset: 0x003EFBCC
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			if (!this.StartAvailable.IsNullOrEmpty())
			{
				this.Available = this.StartAvailable.RollCached();
			}
			if (!this.VariableTurnsToGenerate.IsNullOrEmpty())
			{
				this.TurnsToGenerate = this.VariableTurnsToGenerate.RollCached();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC31 RID: 44081 RVA: 0x003F1A1C File Offset: 0x003EFC1C
		public override bool HandleEvent(AIWantUseWeaponEvent E)
		{
			return this.Available > 0 && base.HandleEvent(E);
		}

		// Token: 0x0600AC32 RID: 44082 RVA: 0x003F1A30 File Offset: 0x003EFC30
		public override bool HandleEvent(GetAmmoCountAvailableEvent E)
		{
			E.Register(this.Available);
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC33 RID: 44083 RVA: 0x003F1A48 File Offset: 0x003EFC48
		public override bool HandleEvent(CheckLoadAmmoEvent E)
		{
			if (this.Available <= 0)
			{
				if (E.Message == null)
				{
					E.Message = this.ParentObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " exhausted!";
				}
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC34 RID: 44084 RVA: 0x003F1AB4 File Offset: 0x003EFCB4
		public override bool HandleEvent(LoadAmmoEvent E)
		{
			if (this.Available > 0)
			{
				if (!this.ProjectileObject.IsNullOrEmpty())
				{
					E.Projectile = GameObject.Create(this.ProjectileObject, 0, 0, null, null, null, "Projectile", null);
				}
				this.Available--;
				return base.HandleEvent(E);
			}
			if (E.Message == null)
			{
				E.Message = this.ParentObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " exhausted!";
			}
			return false;
		}

		// Token: 0x0600AC35 RID: 44085 RVA: 0x003F1B57 File Offset: 0x003EFD57
		public override bool HandleEvent(CheckReadyToFireEvent E)
		{
			return this.Available > 0 && base.HandleEvent(E);
		}

		// Token: 0x0600AC36 RID: 44086 RVA: 0x003F1B6C File Offset: 0x003EFD6C
		public override bool HandleEvent(GetNotReadyToFireMessageEvent E)
		{
			if (this.Available <= 0)
			{
				E.Message = this.ParentObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " exhausted!";
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC37 RID: 44087 RVA: 0x003F1BCC File Offset: 0x003EFDCC
		public override bool HandleEvent(GetMissileWeaponStatusEvent E)
		{
			if (E.Override == null)
			{
				if (E.Status != null)
				{
					E.Status.ammoTotal = this.MaxCapacity;
					E.Status.ammoRemaining = this.Available;
				}
				if (this.Available <= 0)
				{
					E.Items.Append(" [{{K|empty}}]");
				}
				else
				{
					E.Items.Append(" [").Append(this.Available).Append(']');
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC38 RID: 44088 RVA: 0x003F1C51 File Offset: 0x003EFE51
		public override bool HandleEvent(GetProjectileBlueprintEvent E)
		{
			if (!this.ProjectileObject.IsNullOrEmpty())
			{
				E.Blueprint = this.ProjectileObject;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC39 RID: 44089 RVA: 0x003F1C74 File Offset: 0x003EFE74
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.TurnsToGenerateComputePowerFactor > 0f)
			{
				E.Postfix.AppendRules("Compute power on the local lattice decreases the time needed for this item to generate ammunition.");
			}
			else if (this.TurnsToGenerateComputePowerFactor < 0f)
			{
				E.Postfix.AppendRules("Compute power on the local lattice increases the time needed for this item to generate ammunition.");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC3A RID: 44090 RVA: 0x003F1CC8 File Offset: 0x003EFEC8
		public override bool HandleEvent(NeedsReloadEvent E)
		{
			if (!this.ConsumesLiquid.IsNullOrEmpty() && E.Skip != this && (E.Weapon == null || E.Weapon == this.ParentObject))
			{
				LiquidVolume liquidVolume = this.ParentObject.LiquidVolume;
				if (liquidVolume != null && (!this.CorrectLiquid(liquidVolume) || liquidVolume.Volume < liquidVolume.MaxVolume) && this.ParentObject.IsEquippedProperly(null))
				{
					return false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC3B RID: 44091 RVA: 0x003F1D40 File Offset: 0x003EFF40
		public override bool HandleEvent(CommandReloadEvent E)
		{
			if (!this.ConsumesLiquid.IsNullOrEmpty() && (E.Weapon == null || E.Weapon == this.ParentObject) && !E.CheckedForReload.Contains(this))
			{
				E.CheckedForReload.Add(this);
				if (this.ParentObject.IsEquippedProperly(null) && E.MinimumCharge <= 0)
				{
					LiquidVolume liquidVolume = this.ParentObject.LiquidVolume;
					if (liquidVolume != null)
					{
						if (this.CorrectLiquid(liquidVolume) && liquidVolume.Volume >= liquidVolume.MaxVolume)
						{
							if (E.Actor.IsPlayer())
							{
								IComponent<GameObject>.AddPlayerMessage(this.ParentObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " already full.", null, true);
							}
						}
						else
						{
							E.NeededReload.Add(this);
							int freeDrams = E.Actor.GetFreeDrams(this.ConsumesLiquid, this.ParentObject, null, null, false);
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
							}
							else
							{
								E.TriedToReload.Add(this);
								string text = this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false);
								if (liquidVolume.Volume > 0 && !this.CorrectLiquid(liquidVolume))
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
								E.Actor.UseDrams(num2, this.ConsumesLiquid, this.ParentObject, null, null, null, false);
								liquidVolume.MixWith(new LiquidVolume(this.ConsumesLiquid, num2), null, null, null, false);
								E.Reloaded.Add(this);
								if (!E.ObjectsReloaded.Contains(this.ParentObject))
								{
									E.ObjectsReloaded.Add(this.ParentObject);
								}
								E.EnergyCost(this.ReloadEnergy);
								base.PlayWorldSound(this.ParentObject.GetTag("ReloadSound", null), 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
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
						}
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC3C RID: 44092 RVA: 0x003F20D1 File Offset: 0x003F02D1
		public override bool HandleEvent(GetMissileWeaponProjectileEvent E)
		{
			if (!this.ProjectileObject.IsNullOrEmpty())
			{
				E.Blueprint = this.ProjectileObject;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC3D RID: 44093 RVA: 0x003F20F5 File Offset: 0x003F02F5
		public override bool HandleEvent(AllowLiquidCollectionEvent E)
		{
			return this.IsLiquidCollectionCompatible(E.Liquid) && base.HandleEvent(E);
		}

		// Token: 0x0600AC3E RID: 44094 RVA: 0x003F2110 File Offset: 0x003F0310
		public override bool HandleEvent(EndTurnEvent E)
		{
			if (this.Available < this.MaxCapacity && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				this.TurnsGenerating++;
				int num = GetAvailableComputePowerEvent.AdjustDown(this, this.TurnsToGenerate, this.TurnsToGenerateComputePowerFactor, 2);
				if (this.TurnsGenerating >= num)
				{
					this.Available++;
					this.TurnsGenerating = 0;
					if (!this.VariableTurnsToGenerate.IsNullOrEmpty())
					{
						this.TurnsToGenerate = this.VariableTurnsToGenerate.RollCached();
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC3F RID: 44095 RVA: 0x003F21BC File Offset: 0x003F03BC
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood())
			{
				E.AddMissileWeaponDamageTag(GetMissileWeaponPerformanceEvent.GetFor(this.ParentObject.Equipped, this.ParentObject, null, null, null, null, null, null, null, null, null, null, null, false));
				if (!E.Reference)
				{
					if (this.Available <= 0)
					{
						E.AddTag("{{y|[{{K|empty}}]}}", -5);
					}
					else
					{
						E.AddTag("{{y|[" + this.Available.ToString() + "]}}", -5);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC40 RID: 44096 RVA: 0x003F226D File Offset: 0x003F046D
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600AC41 RID: 44097 RVA: 0x003F2270 File Offset: 0x003F0470
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("GenerateIntegratedHostInitialAmmo");
			Registrar.Register("PrepIntegratedHostToReceiveAmmo");
			Registrar.Register("SupplyIntegratedHostWithAmmo");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AC42 RID: 44098 RVA: 0x003F229B File Offset: 0x003F049B
		private bool CorrectLiquid(LiquidVolume Volume)
		{
			if (!this.ConsumesLiquid.IsNullOrEmpty())
			{
				if (!Volume.ComponentLiquids.ContainsKey(this.ConsumesLiquid))
				{
					return false;
				}
				if (this.LiquidMustBePure && !Volume.IsPureLiquid(this.ConsumesLiquid, false))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600AC43 RID: 44099 RVA: 0x003F22D9 File Offset: 0x003F04D9
		public bool IsLiquidCollectionCompatible(string LiquidType)
		{
			return this.ConsumesLiquid.IsNullOrEmpty() || this.WantsLiquidCollection(LiquidType);
		}

		// Token: 0x0600AC44 RID: 44100 RVA: 0x003F22F4 File Offset: 0x003F04F4
		public override bool FireEvent(Event E)
		{
			if (E.ID == "PrepIntegratedHostToReceiveAmmo")
			{
				if (this.ConsumesLiquid.IsNullOrEmpty())
				{
					goto IL_0416;
				}
				GameObject gameObjectParameter = E.GetGameObjectParameter("Host");
				if (gameObjectParameter == null)
				{
					goto IL_0416;
				}
				Body body = gameObjectParameter.Body;
				if (body == null)
				{
					goto IL_0416;
				}
				using (List<BodyPart>.Enumerator enumerator = body.GetPart("Arm").GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						BodyPart bodyPart = enumerator.Current;
						if (bodyPart.Equipped == null)
						{
							gameObjectParameter.ForceEquipObject(GameObject.Create("Gourd"), bodyPart, true, new int?(0));
						}
					}
					goto IL_0416;
				}
			}
			if (E.ID == "SupplyIntegratedHostWithAmmo")
			{
				if (!this.ConsumesLiquid.IsNullOrEmpty())
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
									goto IL_0416;
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
			}
			else if (E.ID == "GenerateIntegratedHostInitialAmmo" && !this.ConsumesLiquid.IsNullOrEmpty())
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
			IL_0416:
			return base.FireEvent(E);
		}

		// Token: 0x04004745 RID: 18245
		public int MaxCapacity = 12;

		// Token: 0x04004746 RID: 18246
		public int Available = 12;

		// Token: 0x04004747 RID: 18247
		public int TurnsToGenerate = 5;

		// Token: 0x04004748 RID: 18248
		public int ReloadEnergy = 1000;

		// Token: 0x04004749 RID: 18249
		public string ProjectileObject;

		// Token: 0x0400474A RID: 18250
		public string StartAvailable;

		// Token: 0x0400474B RID: 18251
		public string VariableTurnsToGenerate;

		// Token: 0x0400474C RID: 18252
		public float TurnsToGenerateComputePowerFactor;

		// Token: 0x0400474D RID: 18253
		public int TurnsGenerating;
	}
}
