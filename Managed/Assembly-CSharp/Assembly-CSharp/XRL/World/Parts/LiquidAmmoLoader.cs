using System;
using System.Collections.Generic;
using XRL.Language;
using XRL.UI;
using XRL.World.Anatomy;

namespace XRL.World.Parts
{
	// Token: 0x02001105 RID: 4357
	[Serializable]
	public class LiquidAmmoLoader : IActivePart
	{
		// Token: 0x0600AC90 RID: 44176 RVA: 0x003F5220 File Offset: 0x003F3420
		public LiquidAmmoLoader()
		{
			this.WorksOnEquipper = true;
			base.IsTechScannable = true;
			this.NameForStatus = "LiquidProjector";
		}

		// Token: 0x0600AC91 RID: 44177 RVA: 0x003F5274 File Offset: 0x003F3474
		public override bool SameAs(IPart p)
		{
			LiquidAmmoLoader liquidAmmoLoader = p as LiquidAmmoLoader;
			return !(liquidAmmoLoader.ID != this.ID) && !(liquidAmmoLoader.Liquid != this.Liquid) && liquidAmmoLoader.ReloadEnergy == this.ReloadEnergy && !(liquidAmmoLoader.ProjectileObject != this.ProjectileObject) && liquidAmmoLoader.ShowDamage == this.ShowDamage && liquidAmmoLoader.ShotsPerDram == this.ShotsPerDram && liquidAmmoLoader.ShotsTakenOnCurrentDram == this.ShotsTakenOnCurrentDram && base.SameAs(p);
		}

		// Token: 0x0600AC92 RID: 44178 RVA: 0x003F5310 File Offset: 0x003F3510
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<AIWantUseWeaponEvent>.ID || ID == AllowLiquidCollectionEvent.ID || ID == PooledEvent<CheckLoadAmmoEvent>.ID || ID == PooledEvent<CheckReadyToFireEvent>.ID || ID == SingletonEvent<CommandReloadEvent>.ID || ID == PooledEvent<GetAmmoCountAvailableEvent>.ID || (ID == PooledEvent<GetDisplayNameEvent>.ID && this.ShowDamage) || ID == PooledEvent<GetMissileWeaponProjectileEvent>.ID || ID == PooledEvent<GetMissileWeaponStatusEvent>.ID || ID == PooledEvent<GetNotReadyToFireMessageEvent>.ID || ID == GetPreferredLiquidEvent.ID || ID == PooledEvent<GetProjectileBlueprintEvent>.ID || ID == PooledEvent<LoadAmmoEvent>.ID || ID == SingletonEvent<NeedsReloadEvent>.ID || ID == WantsLiquidCollectionEvent.ID;
		}

		// Token: 0x0600AC93 RID: 44179 RVA: 0x003F53AC File Offset: 0x003F35AC
		public override bool HandleEvent(AIWantUseWeaponEvent E)
		{
			LiquidVolume liquidVolume = this.ParentObject.LiquidVolume;
			if (!liquidVolume.IsPureLiquid(this.Liquid, false) || liquidVolume.Volume <= 0)
			{
				bool flag = false;
				GameObject actor = E.Actor;
				Inventory inventory = ((actor != null) ? actor.Inventory : null);
				if (inventory != null)
				{
					foreach (GameObject gameObject in inventory.GetObjectsDirect())
					{
						LiquidVolume liquidVolume2 = gameObject.LiquidVolume;
						if (liquidVolume2 != null && liquidVolume2.IsPureLiquid(this.Liquid, false) && liquidVolume2.Volume > 0 && !liquidVolume2.EffectivelySealed())
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

		// Token: 0x0600AC94 RID: 44180 RVA: 0x003F5474 File Offset: 0x003F3674
		public override bool HandleEvent(GetAmmoCountAvailableEvent E)
		{
			if (this.IsFuelGood())
			{
				E.Register(this.ParentObject.LiquidVolume.Volume * this.ShotsPerDram - this.ShotsTakenOnCurrentDram);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC95 RID: 44181 RVA: 0x003F54AC File Offset: 0x003F36AC
		public override bool HandleEvent(CheckLoadAmmoEvent E)
		{
			if (!this.IsFuelGood() || base.IsDisabled(false, false, false, false, false, false, false, false, E.ActivePartsIgnoreSubject, false, 1, null, false, 0L, null))
			{
				if (E.Message == null)
				{
					E.Message = this.GetStatusMessage();
				}
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC96 RID: 44182 RVA: 0x003F5510 File Offset: 0x003F3710
		public override bool HandleEvent(LoadAmmoEvent E)
		{
			if (!this.IsFuelGood())
			{
				if (E.Message == null)
				{
					E.Message = this.GetStatusMessage();
				}
				return false;
			}
			ActivePartStatus activePartStatus = base.GetActivePartStatus(true, false, false, false, false, false, false, false, E.ActivePartsIgnoreSubject, false, 1, null, false, 0L, null);
			if (activePartStatus != ActivePartStatus.Operational)
			{
				if (E.Message == null)
				{
					E.Message = this.GetStatusMessage(activePartStatus);
				}
				return false;
			}
			int num = this.ShotsTakenOnCurrentDram + 1;
			this.ShotsTakenOnCurrentDram = num;
			if (num >= this.ShotsPerDram)
			{
				LiquidVolume liquidVolume = this.ParentObject.LiquidVolume;
				if (liquidVolume != null)
				{
					liquidVolume.UseDram();
				}
				this.ShotsTakenOnCurrentDram = 0;
			}
			if (!this.ProjectileObject.IsNullOrEmpty())
			{
				E.Projectile = GameObject.Create(this.ProjectileObject, 0, 0, null, null, null, "Projectile", null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC97 RID: 44183 RVA: 0x003F55EC File Offset: 0x003F37EC
		public override bool HandleEvent(CheckReadyToFireEvent E)
		{
			LiquidVolume liquidVolume = this.ParentObject.LiquidVolume;
			return liquidVolume.IsPureLiquid(this.Liquid, false) && liquidVolume.Volume > 0 && base.HandleEvent(E);
		}

		// Token: 0x0600AC98 RID: 44184 RVA: 0x003F5628 File Offset: 0x003F3828
		public override bool HandleEvent(GetNotReadyToFireMessageEvent E)
		{
			if (!this.IsFuelGood() || base.IsDisabled(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				E.Message = this.GetStatusMessage();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC99 RID: 44185 RVA: 0x003F5678 File Offset: 0x003F3878
		public override bool HandleEvent(GetMissileWeaponStatusEvent E)
		{
			if (E.Override == null)
			{
				LiquidVolume liquidVolume = this.ParentObject.LiquidVolume;
				if (E.Status != null)
				{
					if (!liquidVolume.IsPureLiquid(this.Liquid, false))
					{
						E.Status.ammoRemaining = 0;
						E.Status.ammoTotal = 0;
					}
					else
					{
						E.Status.ammoTotal = liquidVolume.MaxVolume * this.ShotsPerDram;
						E.Status.ammoRemaining = liquidVolume.Volume * this.ShotsPerDram;
					}
				}
				string primaryLiquidColor = liquidVolume.GetPrimaryLiquidColor();
				if (liquidVolume.Volume == 0)
				{
					if (!primaryLiquidColor.IsNullOrEmpty())
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
				else if (liquidVolume.IsPureLiquid(this.Liquid, false))
				{
					if (!primaryLiquidColor.IsNullOrEmpty())
					{
						E.Items.Append(" [{{").Append(primaryLiquidColor).Append('|')
							.Append(liquidVolume.Volume * this.ShotsPerDram)
							.Append("}}]");
					}
					else
					{
						E.Items.Append(" [").Append(liquidVolume.Volume * this.ShotsPerDram).Append("]");
					}
				}
				else if (!primaryLiquidColor.IsNullOrEmpty())
				{
					E.Items.Append(" [{{").Append(primaryLiquidColor).Append('|')
						.Append("?")
						.Append("}}]");
				}
				else
				{
					E.Items.Append(" [?]");
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC9A RID: 44186 RVA: 0x003F5837 File Offset: 0x003F3A37
		public override bool HandleEvent(GetProjectileBlueprintEvent E)
		{
			if (!this.ProjectileObject.IsNullOrEmpty())
			{
				E.Blueprint = this.ProjectileObject;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC9B RID: 44187 RVA: 0x003F585C File Offset: 0x003F3A5C
		public override bool HandleEvent(NeedsReloadEvent E)
		{
			if (E.Skip != this && (E.Weapon == null || E.Weapon == this.ParentObject))
			{
				LiquidVolume liquidVolume = this.ParentObject.LiquidVolume;
				if (liquidVolume != null && (!liquidVolume.IsPureLiquid(this.Liquid, false) || liquidVolume.Volume < liquidVolume.MaxVolume) && this.ParentObject.IsEquippedProperly(null))
				{
					return false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC9C RID: 44188 RVA: 0x003F58CC File Offset: 0x003F3ACC
		public override bool HandleEvent(CommandReloadEvent E)
		{
			if ((E.Weapon == null || E.Weapon == this.ParentObject) && !E.CheckedForReload.Contains(this))
			{
				E.CheckedForReload.Add(this);
				MissileWeapon part = this.ParentObject.GetPart<MissileWeapon>();
				if ((part == null || part.FiresManually) && E.MinimumCharge <= 0 && (E.Weapon == this.ParentObject || this.ParentObject.IsEquippedProperly(null)))
				{
					LiquidVolume liquidVolume = this.ParentObject.LiquidVolume;
					if (liquidVolume.IsPureLiquid(this.Liquid, false) && liquidVolume.Volume >= liquidVolume.MaxVolume)
					{
						if (E.Actor.IsPlayer())
						{
							IComponent<GameObject>.AddPlayerMessage(this.ParentObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " already full of " + liquidVolume.GetLiquidName() + ".", null, true);
						}
						return true;
					}
					E.NeededReload.Add(this);
					int freeDrams = E.Actor.GetFreeDrams(this.Liquid, this.ParentObject, null, null, false);
					if (freeDrams <= 0)
					{
						if (E.Actor.IsPlayer())
						{
							E.Actor.PlayWorldSound("sfx_missile_reloadFail_noAmmo", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
							IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
							{
								"You have no ",
								this.Liquid,
								" for ",
								this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
								"."
							}), 'r', true);
						}
						return true;
					}
					E.TriedToReload.Add(this);
					string text = this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false);
					if (liquidVolume.Volume > 0 && !liquidVolume.IsPureLiquid(this.Liquid, false))
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
					E.Actor.UseDrams(num2, this.Liquid, this.ParentObject, null, null, null, false);
					liquidVolume.MixWith(new LiquidVolume(this.Liquid, num2), null, null, null, false);
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
							this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
							" with ",
							liquidVolume.GetLiquidName(),
							"."
						}), null, true);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC9D RID: 44189 RVA: 0x003F5CDE File Offset: 0x003F3EDE
		public override bool HandleEvent(GetMissileWeaponProjectileEvent E)
		{
			if (!string.IsNullOrEmpty(this.ProjectileObject))
			{
				E.Blueprint = this.ProjectileObject;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC9E RID: 44190 RVA: 0x003F5D04 File Offset: 0x003F3F04
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (this.ShowDamage && E.Understood())
			{
				E.AddMissileWeaponDamageTag(GetMissileWeaponPerformanceEvent.GetFor(this.ParentObject.Equipped, this.ParentObject, null, null, null, null, null, null, null, null, null, null, null, false));
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC9F RID: 44191 RVA: 0x003F5D78 File Offset: 0x003F3F78
		public override bool HandleEvent(AllowLiquidCollectionEvent E)
		{
			return this.IsLiquidCollectionCompatible(E.Liquid) && base.HandleEvent(E);
		}

		// Token: 0x0600ACA0 RID: 44192 RVA: 0x003F5D91 File Offset: 0x003F3F91
		public override bool HandleEvent(GetPreferredLiquidEvent E)
		{
			if (E.Liquid == null)
			{
				E.Liquid = this.Liquid;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600ACA1 RID: 44193 RVA: 0x003F5DAE File Offset: 0x003F3FAE
		public override bool HandleEvent(WantsLiquidCollectionEvent E)
		{
			return !this.IsLiquidCollectionCompatible(E.Liquid) && base.HandleEvent(E);
		}

		// Token: 0x0600ACA2 RID: 44194 RVA: 0x003F5DC7 File Offset: 0x003F3FC7
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600ACA3 RID: 44195 RVA: 0x003F5DCA File Offset: 0x003F3FCA
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("GenerateIntegratedHostInitialAmmo");
			Registrar.Register("PrepIntegratedHostToReceiveAmmo");
			Registrar.Register("SupplyIntegratedHostWithAmmo");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600ACA4 RID: 44196 RVA: 0x003F5DF8 File Offset: 0x003F3FF8
		public bool IsFuelGood()
		{
			LiquidVolume liquidVolume = this.ParentObject.LiquidVolume;
			return liquidVolume != null && liquidVolume.IsPureLiquid(this.Liquid, false) && liquidVolume.Volume > 0;
		}

		// Token: 0x0600ACA5 RID: 44197 RVA: 0x003F5E34 File Offset: 0x003F4034
		public string GetStatusMessage(ActivePartStatus Status)
		{
			if (Status == ActivePartStatus.ProcessInputMissing)
			{
				return this.ParentObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " empty.";
			}
			if (Status == ActivePartStatus.ProcessInputInvalid)
			{
				return this.ParentObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " not loaded with the correct liquid.";
			}
			return this.ParentObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " " + base.GetStatusPhrase() + ".";
		}

		// Token: 0x0600ACA6 RID: 44198 RVA: 0x003F5F0C File Offset: 0x003F410C
		public string GetStatusMessage()
		{
			LiquidVolume liquidVolume = this.ParentObject.LiquidVolume;
			if (liquidVolume.Volume <= 0)
			{
				return this.GetStatusMessage(ActivePartStatus.ProcessInputMissing);
			}
			if (!liquidVolume.IsPureLiquid(this.Liquid, false))
			{
				return this.GetStatusMessage(ActivePartStatus.ProcessInputInvalid);
			}
			return this.GetStatusMessage(base.GetActivePartStatus(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null));
		}

		// Token: 0x0600ACA7 RID: 44199 RVA: 0x003F5F7B File Offset: 0x003F417B
		public bool IsLiquidCollectionCompatible(string LiquidType)
		{
			return this.Liquid == LiquidType;
		}

		// Token: 0x0600ACA8 RID: 44200 RVA: 0x003F5F8C File Offset: 0x003F418C
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
					int freeDrams = gameObject.GetFreeDrams(this.Liquid, this.ParentObject, null, null, false);
					int storableDrams = gameObjectParameter2.GetStorableDrams(this.Liquid, null, null, null, true, null);
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
								this.Liquid,
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
								this.Liquid,
								" to supply ",
								gameObjectParameter2.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
								" with."
							}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
						}
						else if (storableDrams <= 0)
						{
							Popup.Show(gameObjectParameter2.Does("have", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " no room for more " + this.Liquid + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
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
							this.Liquid,
							" to"
						}), gameObjectParameter2, null, null, null, null, null, null, false, false, false, true, false, null, null, null, false, false, false, true, false, null);
						gameObject.UseDrams(num, this.Liquid, null, null, null, null, false);
						gameObjectParameter2.GiveDrams(num, this.Liquid, false, null, null, null, null, true, null);
						gameObject.UseEnergy(1000, "Ammo Liquid Transfer", null, null, false);
					}
				}
			}
			else if (E.ID == "GenerateIntegratedHostInitialAmmo")
			{
				GameObject gameObjectParameter3 = E.GetGameObjectParameter("Host");
				if (gameObjectParameter3 != null)
				{
					int storableDrams2 = gameObjectParameter3.GetStorableDrams(this.Liquid, null, null, null, true, null);
					if (storableDrams2 > 0)
					{
						gameObjectParameter3.GiveDrams(storableDrams2, this.Liquid, false, null, null, null, null, true, null);
					}
				}
			}
			IL_03E6:
			return base.FireEvent(E);
		}

		// Token: 0x0600ACA9 RID: 44201 RVA: 0x003F63A4 File Offset: 0x003F45A4
		public override bool GetActivePartLocallyDefinedFailure()
		{
			return !this.IsFuelGood();
		}

		// Token: 0x0600ACAA RID: 44202 RVA: 0x003F63AF File Offset: 0x003F45AF
		public override string GetActivePartLocallyDefinedFailureDescription()
		{
			if (this.ParentObject.LiquidVolume.Volume > 0)
			{
				return "ProcessInputInvalid";
			}
			return "ProcessInputMissing";
		}

		// Token: 0x0400475B RID: 18267
		public string ID = "";

		// Token: 0x0400475C RID: 18268
		public string Liquid = "water";

		// Token: 0x0400475D RID: 18269
		public int ReloadEnergy = 1000;

		// Token: 0x0400475E RID: 18270
		public string ProjectileObject;

		// Token: 0x0400475F RID: 18271
		public bool ShowDamage;

		// Token: 0x04004760 RID: 18272
		public int ShotsPerDram = 1;

		// Token: 0x04004761 RID: 18273
		public int ShotsTakenOnCurrentDram;
	}
}
