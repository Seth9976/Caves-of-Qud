using System;
using System.Collections.Generic;
using XRL.Language;
using XRL.Messages;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02001106 RID: 4358
	[Serializable]
	public class MagazineAmmoLoader : IPoweredPart
	{
		// Token: 0x0600ACAB RID: 44203 RVA: 0x003F63D0 File Offset: 0x003F45D0
		public override IPart DeepCopy(GameObject Parent, Func<GameObject, GameObject> MapInv)
		{
			MagazineAmmoLoader magazineAmmoLoader = new MagazineAmmoLoader();
			magazineAmmoLoader.MaxAmmo = this.MaxAmmo;
			magazineAmmoLoader.ID = this.ID;
			if (this.Ammo != null)
			{
				magazineAmmoLoader.Ammo = ((MapInv != null) ? MapInv(this.Ammo) : null) ?? this.Ammo.DeepCopy(false, false, MapInv);
				if (magazineAmmoLoader.Ammo != null)
				{
					magazineAmmoLoader.Ammo.ForeachPartDescendedFrom<IAmmo>(delegate(IAmmo p)
					{
						p.LoadedIn = Parent;
					});
				}
			}
			magazineAmmoLoader.ReloadEnergy = this.ReloadEnergy;
			magazineAmmoLoader.AmmoPart = this.AmmoPart;
			magazineAmmoLoader.ProjectileObject = this.ProjectileObject;
			magazineAmmoLoader.ParentObject = Parent;
			return magazineAmmoLoader;
		}

		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x0600ACAC RID: 44204 RVA: 0x003F648A File Offset: 0x003F468A
		// (set) Token: 0x0600ACAD RID: 44205 RVA: 0x003F648D File Offset: 0x003F468D
		public new int ChargeUse
		{
			get
			{
				return 0;
			}
			set
			{
				throw new Exception("cannot set ChargeUse on a MagazineAmmoLoader");
			}
		}

		// Token: 0x0600ACAE RID: 44206 RVA: 0x003F649C File Offset: 0x003F469C
		public MagazineAmmoLoader()
		{
			this.ChargeUse = 0;
			this.IsBootSensitive = false;
			this.IsEMPSensitive = false;
			this.IsPowerSwitchSensitive = false;
			this.NameForStatus = "FiringMechanism";
			this.WorksOnEquipper = true;
		}

		// Token: 0x0600ACAF RID: 44207 RVA: 0x003F6508 File Offset: 0x003F4708
		public override bool SameAs(IPart p)
		{
			MagazineAmmoLoader magazineAmmoLoader = p as MagazineAmmoLoader;
			return magazineAmmoLoader.MaxAmmo == this.MaxAmmo && !(magazineAmmoLoader.ID != this.ID) && magazineAmmoLoader.Ammo == this.Ammo && magazineAmmoLoader.ReloadEnergy == this.ReloadEnergy && !(magazineAmmoLoader.AmmoPart != this.AmmoPart) && !(magazineAmmoLoader.ProjectileObject != this.ProjectileObject) && base.SameAs(p);
		}

		// Token: 0x0600ACB0 RID: 44208 RVA: 0x003F6594 File Offset: 0x003F4794
		public void SetAmmo(GameObject Object)
		{
			if (Object == this.Ammo)
			{
				return;
			}
			if (this.Ammo != null)
			{
				this.Ammo.ForeachPartDescendedFrom<IAmmo>(delegate(IAmmo p)
				{
					p.LoadedIn = null;
				});
			}
			this.Ammo = Object;
			if (Object != null)
			{
				Object.ForeachPartDescendedFrom<IAmmo>(delegate(IAmmo p)
				{
					p.LoadedIn = this.ParentObject;
				});
			}
			if (Sidebar.CurrentTarget == Object)
			{
				Sidebar.CurrentTarget = null;
			}
			base.FlushTransientCaches();
		}

		// Token: 0x0600ACB1 RID: 44209 RVA: 0x003F660D File Offset: 0x003F480D
		private bool AmmoWantsEvent(int ID, int cascade)
		{
			return GameObject.Validate(ref this.Ammo) && MinEvent.CascadeTo(cascade, 4) && this.Ammo.WantEvent(ID, cascade);
		}

		// Token: 0x0600ACB2 RID: 44210 RVA: 0x003F6638 File Offset: 0x003F4838
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<AIWantUseWeaponEvent>.ID || ID == PooledEvent<CheckLoadAmmoEvent>.ID || ID == PooledEvent<CheckReadyToFireEvent>.ID || ID == SingletonEvent<CommandReloadEvent>.ID || ID == PooledEvent<GetAmmoCountAvailableEvent>.ID || ID == PooledEvent<GetContentsEvent>.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetExtrinsicValueEvent.ID || ID == GetExtrinsicWeightEvent.ID || ID == GetInventoryActionsEvent.ID || ID == PooledEvent<GetMissileWeaponProjectileEvent>.ID || ID == PooledEvent<GetMissileWeaponStatusEvent>.ID || ID == PooledEvent<GetProjectileBlueprintEvent>.ID || ID == InventoryActionEvent.ID || ID == PooledEvent<LoadAmmoEvent>.ID || ID == SingletonEvent<NeedsReloadEvent>.ID || ID == PooledEvent<ShotCompleteEvent>.ID || ID == PooledEvent<StripContentsEvent>.ID || ID == AutoexploreObjectEvent.ID || this.AmmoWantsEvent(ID, cascade);
		}

		// Token: 0x0600ACB3 RID: 44211 RVA: 0x003F6700 File Offset: 0x003F4900
		public override bool HandleEvent(AIWantUseWeaponEvent E)
		{
			if (base.IsDisabled(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				return false;
			}
			if (this.Ammo == null)
			{
				GameObject actor = E.Actor;
				Inventory inventory = ((actor != null) ? actor.Inventory : null);
				bool flag = false;
				foreach (GameObject gameObject in inventory.GetObjectsDirect())
				{
					if (this.IsValidAmmo(gameObject))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600ACB4 RID: 44212 RVA: 0x003F67AC File Offset: 0x003F49AC
		public override bool HandleEvent(ShotCompleteEvent E)
		{
			if (this.ReloadEnergy == 0)
			{
				GameObject equipped = this.ParentObject.Equipped;
				if (equipped != null && equipped.CanMoveExtremities(null, false, false, true))
				{
					CommandReloadEvent.Execute(equipped, this.ParentObject, E.LoadedAmmo, false, false, 0);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600ACB5 RID: 44213 RVA: 0x003F67F9 File Offset: 0x003F49F9
		public override bool HandleEvent(GetAmmoCountAvailableEvent E)
		{
			if (this.Ammo != null)
			{
				E.Register(this.Ammo.Count);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600ACB6 RID: 44214 RVA: 0x003F681C File Offset: 0x003F4A1C
		public override bool HandleEvent(CheckLoadAmmoEvent E)
		{
			if (this.Ammo == null)
			{
				if (E.Message == null)
				{
					E.Message = this.ParentObject.Does("have", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " no more ammo!";
				}
				return false;
			}
			if (base.GetActivePartStatus(false, false, false, false, false, false, false, false, E.ActivePartsIgnoreSubject, false, 1, null, false, 0L, null) != ActivePartStatus.Operational)
			{
				if (E.Message == null)
				{
					E.Message = this.ParentObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " jammed!";
				}
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600ACB7 RID: 44215 RVA: 0x003F6900 File Offset: 0x003F4B00
		public override bool HandleEvent(LoadAmmoEvent E)
		{
			if (this.Ammo == null)
			{
				if (E.Message == null)
				{
					E.Message = this.ParentObject.Does("have", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " no more ammo!";
				}
				return false;
			}
			if (base.GetActivePartStatus(true, false, false, false, false, false, false, false, E.ActivePartsIgnoreSubject, false, 1, null, false, 0L, null) != ActivePartStatus.Operational)
			{
				if (E.Message == null)
				{
					E.Message = this.ParentObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " jammed!";
				}
				return false;
			}
			E.LoadedAmmo = this.Ammo.RemoveOne();
			if (E.LoadedAmmo == this.Ammo)
			{
				this.SetAmmo(null);
			}
			if (this.ProjectileObject.IsNullOrEmpty())
			{
				E.Projectile = GetProjectileObjectEvent.GetFor(E.LoadedAmmo, this.ParentObject);
			}
			else
			{
				E.Projectile = GameObject.Create(this.ProjectileObject, 0, 0, null, null, null, "Projectile", null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600ACB8 RID: 44216 RVA: 0x003F6A4C File Offset: 0x003F4C4C
		public override bool HandleEvent(CheckReadyToFireEvent E)
		{
			return this.Ammo != null && !base.IsDisabled(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) && base.HandleEvent(E);
		}

		// Token: 0x0600ACB9 RID: 44217 RVA: 0x003F6A92 File Offset: 0x003F4C92
		public override bool HandleEvent(AutoexploreObjectEvent E)
		{
			if (GameObject.Validate(ref this.Ammo))
			{
				this.Ammo.HandleEvent(E);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600ACBA RID: 44218 RVA: 0x003F6AB8 File Offset: 0x003F4CB8
		public override bool HandleEvent(GetMissileWeaponStatusEvent E)
		{
			if (E.Override == null)
			{
				if (E.Status != null)
				{
					E.Status.ammoRemaining = ((this.Ammo == null) ? 0 : this.Ammo.Count);
					E.Status.ammoTotal = this.MaxAmmo;
				}
				if (this.Ammo == null)
				{
					E.Items.Append(" [{{K|empty}}]");
				}
				else
				{
					int count = this.Ammo.Count;
					string @for = GetMissileStatusColorEvent.GetFor(this.Ammo, null);
					E.Items.Append(" [");
					if (!@for.IsNullOrEmpty())
					{
						E.Items.Append("{{").Append(@for).Append('|');
					}
					E.Items.Append(count);
					if (!@for.IsNullOrEmpty())
					{
						E.Items.Append("}}");
					}
					E.Items.Append("]");
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600ACBB RID: 44219 RVA: 0x003F6BB6 File Offset: 0x003F4DB6
		public override bool HandleEvent(GetProjectileBlueprintEvent E)
		{
			if (!this.ProjectileObject.IsNullOrEmpty())
			{
				E.Blueprint = this.ProjectileObject;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600ACBC RID: 44220 RVA: 0x003F6BD8 File Offset: 0x003F4DD8
		public override bool HandleEvent(NeedsReloadEvent E)
		{
			return (E.Skip == this || (E.Weapon != null && E.Weapon != this.ParentObject) || (this.Ammo != null && this.Ammo.Count >= this.MaxAmmo) || !this.ParentObject.IsEquippedProperly(null)) && base.HandleEvent(E);
		}

		// Token: 0x0600ACBD RID: 44221 RVA: 0x003F6C38 File Offset: 0x003F4E38
		public override bool HandleEvent(CommandReloadEvent E)
		{
			if (E.Pass >= 2 && (E.Weapon == null || E.Weapon == this.ParentObject) && !E.CheckedForReload.Contains(this))
			{
				E.CheckedForReload.Add(this);
				MissileWeapon part = this.ParentObject.GetPart<MissileWeapon>();
				if ((part == null || part.FiresManually) && E.MinimumCharge <= 0 && (E.Weapon == this.ParentObject || this.ParentObject.IsEquippedProperly(null)))
				{
					bool flag = this.Ammo == null || this.Ammo.Count < this.MaxAmmo;
					if (flag)
					{
						E.NeededReload.Add(this);
					}
					if (flag || (E.NeededReload.Count <= 0 && !NeedsReloadEvent.Check(E.Actor, this)))
					{
						List<GameObject> list = Event.NewGameObjectList();
						foreach (GameObject gameObject in E.Actor.GetInventory())
						{
							if (this.IsValidAmmo(gameObject))
							{
								list.Add(gameObject);
							}
						}
						if (list.Count == 0)
						{
							if (E.Actor.IsPlayer())
							{
								E.Actor.PlayWorldSound("sfx_missile_reloadFail_noAmmo", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
								IComponent<GameObject>.EmitMessage(E.Actor, "You have no more ammo for " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", ' ', E.FromDialog, false, false, null, null);
							}
							return true;
						}
						if (!flag && list.Count == 1 && this.Ammo != null && this.Ammo.Blueprint == list[0].Blueprint)
						{
							if (E.Actor.IsPlayer())
							{
								IComponent<GameObject>.EmitMessage(E.Actor, this.ParentObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " already fully loaded.", ' ', E.FromDialog, false, false, null, null);
							}
							return true;
						}
						GameObject gameObject2 = null;
						if (GameObject.Validate(ref E.LastAmmo))
						{
							for (int i = 0; i < list.Count; i++)
							{
								if (list[i].Blueprint == E.LastAmmo.Blueprint)
								{
									gameObject2 = list[i];
									break;
								}
							}
							if (gameObject2 == null)
							{
								return false;
							}
						}
						if (gameObject2 == null)
						{
							if (list.Count > 1)
							{
								if (E.Actor.IsPlayer())
								{
									gameObject2 = PickItem.ShowPicker(list, null, PickItem.PickItemDialogStyle.SelectItemDialog, E.Actor, null, null, null, false, null, false, true, false);
									if (gameObject2 == null)
									{
										return false;
									}
								}
								else
								{
									List<GameObject> list2 = Event.NewGameObjectList();
									foreach (GameObject gameObject3 in list)
									{
										if (!gameObject3.IsImportant())
										{
											int j = 0;
											int num = Math.Min(gameObject3.Count, 10);
											while (j < num)
											{
												list2.Add(gameObject3);
												j++;
											}
										}
									}
									gameObject2 = list2.GetRandomElement<GameObject>(null);
								}
							}
							else
							{
								gameObject2 = list[0];
							}
						}
						if (gameObject2.ConfirmUseImportant(E.Actor, "load", null, -1))
						{
							E.TriedToReload.Add(this);
							this.Unload(E.Actor);
							if (this.Load(E.Actor, gameObject2, E.FromDialog))
							{
								E.Reloaded.Add(this);
								if (!E.ObjectsReloaded.Contains(this.ParentObject))
								{
									E.ObjectsReloaded.Add(this.ParentObject);
								}
								E.EnergyCost(this.ReloadEnergy);
							}
						}
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600ACBE RID: 44222 RVA: 0x003F7050 File Offset: 0x003F5250
		public override bool HandleEvent(MinEvent E)
		{
			return base.HandleEvent(E) && (!E.CascadeTo(4) || !GameObject.Validate(ref this.Ammo) || E.Dispatch(this.Ammo));
		}

		// Token: 0x0600ACBF RID: 44223 RVA: 0x003F7084 File Offset: 0x003F5284
		public override bool HandleEvent(GetMissileWeaponProjectileEvent E)
		{
			if (!this.ProjectileObject.IsNullOrEmpty())
			{
				E.Blueprint = this.ProjectileObject;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600ACC0 RID: 44224 RVA: 0x003F70A8 File Offset: 0x003F52A8
		public override bool HandleEvent(GetExtrinsicValueEvent E)
		{
			if (GameObject.Validate(ref this.Ammo))
			{
				E.Value += this.Ammo.Value;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600ACC1 RID: 44225 RVA: 0x003F70D6 File Offset: 0x003F52D6
		public override bool HandleEvent(GetExtrinsicWeightEvent E)
		{
			if (GameObject.Validate(ref this.Ammo))
			{
				E.Weight += (double)this.Ammo.Weight;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600ACC2 RID: 44226 RVA: 0x003F7105 File Offset: 0x003F5305
		public override bool HandleEvent(StripContentsEvent E)
		{
			if (GameObject.Validate(ref this.Ammo) && (!E.KeepNatural || !this.Ammo.IsNatural()))
			{
				this.Ammo.Obliterate(null, false, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600ACC3 RID: 44227 RVA: 0x003F713F File Offset: 0x003F533F
		public override bool HandleEvent(GetContentsEvent E)
		{
			if (GameObject.Validate(ref this.Ammo))
			{
				E.Objects.Add(this.Ammo);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600ACC4 RID: 44228 RVA: 0x003F7168 File Offset: 0x003F5368
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			E.AddAction("Load Ammo", "load", "LoadMagazineAmmo", null, 'o', false, 0, 0, false, false, false, false, true, null, false);
			if (GameObject.Validate(ref this.Ammo))
			{
				E.AddAction("Unload Ammo", "unload", "UnloadMagazineAmmo", null, 'u', false, 0, 0, false, false, false, false, true, null, false);
				GetSlottedInventoryActionsEvent.Send(this.Ammo, E);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600ACC5 RID: 44229 RVA: 0x003F71DC File Offset: 0x003F53DC
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "LoadMagazineAmmo")
			{
				CommandReloadEvent.Execute(E.Actor, this.ParentObject, null, false, true, 0);
			}
			else if ((E.Command == "UnloadMagazineAmmo" || E.Command == "EmptyForDisassemble") && GameObject.Validate(ref this.Ammo))
			{
				this.ParentObject.PlayWorldSoundTag("UnloadSound", null, null, 0.5f);
				GameObject gameObject;
				Cell cell;
				this.ParentObject.GetContext(out gameObject, out cell);
				if (gameObject != null)
				{
					gameObject.ReceiveObject(this.Ammo, false, null);
				}
				else if (cell != null)
				{
					cell.AddObject(this.Ammo, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				}
				else
				{
					E.Actor.ReceiveObject(this.Ammo, false, null);
					E.Generated.Add(this.Ammo);
				}
				this.SetAmmo(null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600ACC6 RID: 44230 RVA: 0x003F72DC File Offset: 0x003F54DC
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood())
			{
				if (E.Cutoff >= 1100)
				{
					GameObject equipped = this.ParentObject.Equipped;
					GameObject parentObject = this.ParentObject;
					GameObject gameObject;
					if (this.Ammo == null || !this.ProjectileObject.IsNullOrEmpty())
					{
						gameObject = null;
					}
					else
					{
						GameObject parentObject2 = this.ParentObject;
						gameObject = GetProjectileObjectEvent.GetFor(this.Ammo, parentObject2);
					}
					E.AddMissileWeaponDamageTag(GetMissileWeaponPerformanceEvent.GetFor(equipped, parentObject, gameObject, null, null, null, null, null, null, null, null, null, null, false));
				}
				if (!E.Reference)
				{
					if (GameObject.Validate(ref this.Ammo))
					{
						E.AddTag("{{y|[" + this.Ammo.DisplayNameOnly + "]}}", -5);
					}
					else
					{
						E.AddTag("{{y|[{{K|empty}}]}}", -5);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600ACC7 RID: 44231 RVA: 0x003F73C8 File Offset: 0x003F55C8
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("GenerateIntegratedHostInitialAmmo");
			Registrar.Register("SupplyIntegratedHostWithAmmo");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600ACC8 RID: 44232 RVA: 0x003F73E8 File Offset: 0x003F55E8
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (GameObject.Validate(ref this.Ammo))
			{
				this.Ammo.TurnTick(TimeTick, Amount);
			}
		}

		// Token: 0x0600ACC9 RID: 44233 RVA: 0x003F7404 File Offset: 0x003F5604
		public void Unload(GameObject Loader)
		{
			try
			{
				this.ParentObject.SplitFromStack();
				MessageQueue.Suppress = true;
				InventoryActionEvent.Check(this.ParentObject, Loader, this.ParentObject, "UnloadMagazineAmmo", false, false, false, false, false, 0, 0, 0, null, null, null, null);
				MessageQueue.Suppress = false;
			}
			catch
			{
				MessageQueue.Suppress = false;
			}
		}

		// Token: 0x0600ACCA RID: 44234 RVA: 0x003F7468 File Offset: 0x003F5668
		public bool Load(GameObject Loader, GameObject ChosenAmmo, bool FromDialog = false)
		{
			bool flag;
			try
			{
				this.ParentObject.SplitFromStack();
				MessageQueue.Suppress = true;
				ChosenAmmo.SplitStack(this.MaxAmmo, Loader, false);
				Event @event = Event.New("CommandRemoveObject", 0, 0, 0);
				@event.SetParameter("Object", ChosenAmmo);
				@event.SetFlag("ForEquip", true);
				if (Loader.FireEvent(@event))
				{
					this.SetAmmo(ChosenAmmo);
					this.ParentObject.FireEvent("MagazineAmmoLoaderReloaded");
					MessageQueue.Suppress = false;
					base.PlayWorldSound(this.ParentObject.GetPropertyOrTag("ReloadSound", null), 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
					if (Loader.IsPlayer())
					{
						IComponent<GameObject>.EmitMessage(Loader, string.Concat(new string[]
						{
							"You reload ",
							this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
							" with ",
							(this.Ammo.Count == 1) ? this.Ammo.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false) : this.Ammo.ShortDisplayName,
							"."
						}), ' ', FromDialog, false, false, null, null);
					}
					flag = true;
				}
				else
				{
					flag = false;
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogError(ex);
				flag = false;
			}
			finally
			{
				MessageQueue.Suppress = false;
			}
			return flag;
		}

		// Token: 0x0600ACCB RID: 44235 RVA: 0x003F761C File Offset: 0x003F581C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "SupplyIntegratedHostWithAmmo")
			{
				if (E.HasFlag("TrackSupply"))
				{
					E.SetFlag("AnySupplyHandler", true);
				}
				GameObject gameObjectParameter = E.GetGameObjectParameter("Host");
				GameObject gameObject = E.GetGameObjectParameter("Actor") ?? E.GetGameObjectParameter("Owner");
				if (gameObjectParameter == null || gameObject == null)
				{
					goto IL_03F6;
				}
				int desiredAmmoCount = this.GetDesiredAmmoCount();
				int num = this.GetAccessibleAmmoCount();
				Inventory inventory = gameObject.Inventory;
				if (inventory == null)
				{
					goto IL_03F6;
				}
				List<GameObject> list = Event.NewGameObjectList();
				inventory.GetObjects(list, new Predicate<GameObject>(this.IsValidAmmo));
				using (List<GameObject>.Enumerator enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						GameObject gameObject2 = enumerator.Current;
						int count = gameObject2.Count;
						int num2 = 0;
						if (gameObject.IsPlayer())
						{
							if (E.HasFlag("TrackSupply"))
							{
								E.SetFlag("AnySupplies", true);
							}
							Math.Min(desiredAmmoCount - num, count);
							int? num3 = Popup.AskNumber(string.Concat(new string[]
							{
								"Supply ",
								gameObjectParameter.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
								" with how many ",
								gameObject2.HasProperName ? ("of " + gameObject2.DisplayNameSingle) : Grammar.Pluralize(gameObject2.DisplayNameSingle),
								"? (max=",
								count.ToString(),
								")"
							}), "Sounds/UI/ui_notification", "", count, 0, count);
							int num4 = 0;
							try
							{
								num4 = Convert.ToInt32(num3);
							}
							catch
							{
								break;
							}
							if (num4 > count)
							{
								num4 = count;
							}
							if (num4 < 0)
							{
								num4 = 0;
							}
							num2 = num4;
						}
						else if (desiredAmmoCount > num)
						{
							num2 = Math.Min(desiredAmmoCount - num, count);
						}
						if (num2 > 0)
						{
							IComponent<GameObject>.XDidYToZ(gameObject, "transfer", gameObject2.HasProperName ? gameObject2.DisplayNameOnly : (((num2 == 1) ? gameObject2.an(int.MaxValue, null, null, false, true, false, false, false, false, true, false, false, true, false, null, false, null, false) : (Grammar.Cardinal(num2) + " " + Grammar.Pluralize(gameObject2.ShortDisplayNameSingle))) + " to"), gameObjectParameter, null, null, null, null, null, null, false, false, false, true, false, null, null, null, false, false, false, true, false, null);
							if (num2 < count)
							{
								gameObject2.Split(num2, false);
							}
							gameObjectParameter.ReceiveObject(gameObject2, false, null);
							gameObject.UseEnergy(1000, "Ammo Magazine Transfer", null, null, false);
							num += num2;
						}
					}
					goto IL_03F6;
				}
			}
			if (E.ID == "GenerateIntegratedHostInitialAmmo")
			{
				GameObject gameObjectParameter2 = E.GetGameObjectParameter("Host");
				if (gameObjectParameter2 != null && gameObjectParameter2.Inventory != null)
				{
					int desiredAmmoCount2 = this.GetDesiredAmmoCount();
					int num5 = this.GetAccessibleAmmoCount();
					if (num5 < desiredAmmoCount2)
					{
						List<GameObjectBlueprint> ammoBlueprints = this.GetAmmoBlueprints();
						if (ammoBlueprints.Count == 1)
						{
							num5 += gameObjectParameter2.ReceiveObject(ammoBlueprints[0].Name, desiredAmmoCount2 - num5, false, 0, 0, null, null, null, null);
						}
						else if (ammoBlueprints.Count > 1)
						{
							int num6 = (desiredAmmoCount2 - num5) / ammoBlueprints.Count;
							if (num6 > 0)
							{
								foreach (GameObjectBlueprint gameObjectBlueprint in ammoBlueprints)
								{
									num5 += gameObjectParameter2.ReceiveObject(gameObjectBlueprint.Name, num6, false, 0, 0, null, null, null, null);
								}
							}
							if (num5 < desiredAmmoCount2)
							{
								int num7 = 0;
								int num8 = desiredAmmoCount2 - num5 + 100;
								while (num5 < desiredAmmoCount2 && ++num7 < num8)
								{
									if (gameObjectParameter2.ReceiveObject(ammoBlueprints.GetRandomElement<GameObjectBlueprint>(null).Name, false, 0, 0, null, null, null, null))
									{
										num5++;
									}
								}
							}
						}
					}
				}
			}
			IL_03F6:
			return base.FireEvent(E);
		}

		// Token: 0x0600ACCC RID: 44236 RVA: 0x003F7A74 File Offset: 0x003F5C74
		private int GetDesiredAmmoCount()
		{
			MissileWeapon part = this.ParentObject.GetPart<MissileWeapon>();
			int num = this.ParentObject.GetIntProperty("IntegratedWeaponHostShots", 0);
			if (num <= 0)
			{
				if (this.MaxAmmo == 1)
				{
					num = 50;
				}
				else if (this.MaxAmmo == 2)
				{
					num = 100;
				}
				else
				{
					num = 200;
				}
			}
			return part.AmmoPerAction * num;
		}

		// Token: 0x0600ACCD RID: 44237 RVA: 0x003F7ACC File Offset: 0x003F5CCC
		private int GetAccessibleAmmoCount()
		{
			int num = ((this.Ammo == null) ? 0 : this.Ammo.Count);
			GameObject holder = this.ParentObject.Holder;
			Inventory inventory = ((holder != null) ? holder.Inventory : null);
			if (inventory != null)
			{
				foreach (GameObject gameObject in inventory.Objects)
				{
					if (gameObject.HasPart(this.AmmoPart))
					{
						num += gameObject.Count;
					}
				}
			}
			return num;
		}

		// Token: 0x0600ACCE RID: 44238 RVA: 0x003F7B64 File Offset: 0x003F5D64
		private static List<GameObjectBlueprint> GetAmmoBlueprints(string ForAmmoPart)
		{
			if (!MagazineAmmoLoader.AmmoBlueprints.ContainsKey(ForAmmoPart))
			{
				bool flag = false;
				List<GameObjectBlueprint> list = new List<GameObjectBlueprint>();
				foreach (GameObjectBlueprint gameObjectBlueprint in GameObjectFactory.Factory.BlueprintList)
				{
					if (gameObjectBlueprint.HasPart(ForAmmoPart) && !gameObjectBlueprint.HasTag("ExcludeFromDynamicEncounters", false) && !gameObjectBlueprint.HasTag("BaseObject", false) && !gameObjectBlueprint.HasTag("ExcludeFromTurretStock", false))
					{
						if (flag)
						{
							if (gameObjectBlueprint.HasTag("TurretStockExclusive", false))
							{
								MagazineAmmoLoader.AddAmmoBlueprint(list, gameObjectBlueprint);
							}
						}
						else if (gameObjectBlueprint.HasTag("TurretStockExclusive", false))
						{
							list.Clear();
							MagazineAmmoLoader.AddAmmoBlueprint(list, gameObjectBlueprint);
							flag = true;
						}
						else
						{
							MagazineAmmoLoader.AddAmmoBlueprint(list, gameObjectBlueprint);
						}
					}
				}
				return list;
			}
			return MagazineAmmoLoader.AmmoBlueprints[ForAmmoPart];
		}

		// Token: 0x0600ACCF RID: 44239 RVA: 0x003F7C50 File Offset: 0x003F5E50
		private static void AddAmmoBlueprint(List<GameObjectBlueprint> List, GameObjectBlueprint BP)
		{
			string tag = BP.GetTag("TurretStockWeight", "");
			if (tag.IsNullOrEmpty())
			{
				List.Add(BP);
				return;
			}
			try
			{
				int num = Convert.ToInt32(tag);
				for (int i = 0; i < num; i++)
				{
					List.Add(BP);
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600ACD0 RID: 44240 RVA: 0x003F7CB0 File Offset: 0x003F5EB0
		private List<GameObjectBlueprint> GetAmmoBlueprints()
		{
			return MagazineAmmoLoader.GetAmmoBlueprints(this.AmmoPart);
		}

		// Token: 0x0600ACD1 RID: 44241 RVA: 0x003F7CBD File Offset: 0x003F5EBD
		public bool IsValidAmmo(GameObject Object)
		{
			return Object.HasPart(this.AmmoPart) || Object.HasTagOrProperty(this.AmmoPart);
		}

		// Token: 0x0600ACD2 RID: 44242 RVA: 0x003F7CDB File Offset: 0x003F5EDB
		public bool IsValidAmmo(GameObjectBlueprint Blueprint)
		{
			return Blueprint.HasPart(this.AmmoPart) || Blueprint.HasTagOrProperty(this.AmmoPart);
		}

		// Token: 0x04004762 RID: 18274
		public int MaxAmmo = 6;

		// Token: 0x04004763 RID: 18275
		public string ID = "";

		// Token: 0x04004764 RID: 18276
		public GameObject Ammo;

		// Token: 0x04004765 RID: 18277
		public int ReloadEnergy = 1000;

		// Token: 0x04004766 RID: 18278
		public string AmmoPart = "";

		// Token: 0x04004767 RID: 18279
		public string ProjectileObject;

		// Token: 0x04004768 RID: 18280
		[NonSerialized]
		private static Dictionary<string, List<GameObjectBlueprint>> AmmoBlueprints = new Dictionary<string, List<GameObjectBlueprint>>();
	}
}
