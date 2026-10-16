using System;
using System.Collections.Generic;
using Qud.UI;
using XRL.Rules;
using XRL.UI;
using XRL.World.Tinkering;

namespace XRL.World.Parts
{
	/// <remarks>
	/// overload behavior: if <see cref="!:IsPowerLoadSensitive" /> is true,
	/// which it is by default, damage of projectiles that are not set to
	/// cause 0 damage is increased by the standard power load bonus, i.e.
	/// 2 for the standard overload power load of 400.
	/// </remarks>
	// Token: 0x02001104 RID: 4356
	[Serializable]
	public class EnergyAmmoLoader : IPoweredPart
	{
		// Token: 0x0600AC74 RID: 44148 RVA: 0x003F3FAB File Offset: 0x003F21AB
		public EnergyAmmoLoader()
		{
			this.ChargeUse = 0;
			this.WorksOnEquipper = true;
			this.IsPowerLoadSensitive = true;
			this.NameForStatus = "EnergyWeaponSystem";
		}

		// Token: 0x0600AC75 RID: 44149 RVA: 0x003F3FD3 File Offset: 0x003F21D3
		public override bool SameAs(IPart p)
		{
			return !((p as EnergyAmmoLoader).ProjectileObject != this.ProjectileObject) && base.SameAs(p);
		}

		// Token: 0x0600AC76 RID: 44150 RVA: 0x003F3FF8 File Offset: 0x003F21F8
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<AIWantUseWeaponEvent>.ID || ID == PooledEvent<CheckLoadAmmoEvent>.ID || ID == PooledEvent<CheckReadyToFireEvent>.ID || ID == SingletonEvent<CommandReloadEvent>.ID || ID == PooledEvent<GetAmmoCountAvailableEvent>.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || (ID == PooledEvent<GetMissileWeaponPerformanceEvent>.ID && this.IsPowerLoadSensitive) || ID == PooledEvent<GetMissileWeaponProjectileEvent>.ID || ID == PooledEvent<GetMissileWeaponStatusEvent>.ID || ID == PooledEvent<GetNotReadyToFireMessageEvent>.ID || ID == PooledEvent<GetProjectileBlueprintEvent>.ID || ID == PooledEvent<LoadAmmoEvent>.ID || ID == SingletonEvent<NeedsReloadEvent>.ID;
		}

		// Token: 0x0600AC77 RID: 44151 RVA: 0x003F4084 File Offset: 0x003F2284
		public override bool HandleEvent(AIWantUseWeaponEvent E)
		{
			if (base.GetActivePartStatus(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) == ActivePartStatus.Unpowered)
			{
				EnergyCellSocket socket = E.Object.GetPart<EnergyCellSocket>();
				if (socket != null)
				{
					bool result = false;
					Predicate<IEnergyCell> <>9__1;
					E.Actor.Inventory.ForeachObject(delegate(GameObject GO)
					{
						if (E.Actor.IsPlayer() && !GO.Understood())
						{
							return;
						}
						Predicate<IEnergyCell> predicate;
						if ((predicate = <>9__1) == null)
						{
							predicate = (<>9__1 = delegate(IEnergyCell P)
							{
								if (P.SlotType == socket.SlotType && P.HasCharge(this.ChargeUse))
								{
									result = true;
									return false;
								}
								return true;
							});
						}
						GO.ForeachPartDescendedFrom<IEnergyCell>(predicate);
					});
					if (!result)
					{
						return false;
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC78 RID: 44152 RVA: 0x003F412C File Offset: 0x003F232C
		public override bool HandleEvent(GetAmmoCountAvailableEvent E)
		{
			int activeChargeUse = base.GetActiveChargeUse(null, null);
			if (activeChargeUse > 0)
			{
				E.Register(this.ParentObject.QueryCharge(false, 0L, true, true) / activeChargeUse);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC79 RID: 44153 RVA: 0x003F4178 File Offset: 0x003F2378
		public override bool HandleEvent(CheckLoadAmmoEvent E)
		{
			ActivePartStatus activePartStatus = base.GetActivePartStatus(false, false, false, false, false, false, false, false, E.ActivePartsIgnoreSubject, false, 1, null, false, 0L, null);
			if (activePartStatus != ActivePartStatus.Operational)
			{
				if (E.Message == null)
				{
					E.Message = this.GetStatusMessage(activePartStatus);
				}
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC7A RID: 44154 RVA: 0x003F41D4 File Offset: 0x003F23D4
		public override bool HandleEvent(LoadAmmoEvent E)
		{
			ActivePartStatus activePartStatus = base.GetActivePartStatus(true, false, false, false, false, false, false, false, E.ActivePartsIgnoreSubject, false, 1, null, false, 0L, null);
			if (activePartStatus == ActivePartStatus.Operational)
			{
				if (!this.ProjectileObject.IsNullOrEmpty())
				{
					E.Projectile = GameObject.Create(this.ProjectileObject, 0, 0, null, null, null, "Projectile", null);
				}
				return base.HandleEvent(E);
			}
			if (E.Message == null)
			{
				E.Message = this.GetStatusMessage(activePartStatus);
			}
			return false;
		}

		// Token: 0x0600AC7B RID: 44155 RVA: 0x003F425C File Offset: 0x003F245C
		public override bool HandleEvent(CheckReadyToFireEvent E)
		{
			return !base.IsDisabled(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) && base.HandleEvent(E);
		}

		// Token: 0x0600AC7C RID: 44156 RVA: 0x003F429C File Offset: 0x003F249C
		public override bool HandleEvent(GetNotReadyToFireMessageEvent E)
		{
			ActivePartStatus activePartStatus = base.GetActivePartStatus(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null);
			if (activePartStatus != ActivePartStatus.Operational && E.Message == null)
			{
				E.Message = this.GetStatusMessage(activePartStatus);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC7D RID: 44157 RVA: 0x003F42F4 File Offset: 0x003F24F4
		public override bool HandleEvent(GetMissileWeaponStatusEvent E)
		{
			if (E.Override == null)
			{
				EnergyCellSocket part = this.ParentObject.GetPart<EnergyCellSocket>();
				if (part != null)
				{
					if (E.Status != null)
					{
						this.GetChargePerAction();
						if (part.Cell == null)
						{
							E.Status.text = "[{{K|empty}}]";
						}
						else if (!part.Cell.Understood())
						{
							E.Status.text = "[?]";
						}
						else
						{
							E.Status.text = "";
							part.Cell.ForeachPartDescendedFrom<IEnergyCell>(delegate(IEnergyCell P)
							{
								string text = P.ChargeStatus();
								if (text != null)
								{
									MissileWeaponArea.MissileWeaponAreaWeaponStatus status = E.Status;
									status.text = status.text + "[" + text + "]";
								}
							});
						}
					}
					if (part.Cell == null)
					{
						E.Items.Append(" [{{K|empty}}]");
					}
					else if (!part.Cell.Understood())
					{
						E.Items.Append(" [?]");
					}
					else
					{
						part.Cell.ForeachPartDescendedFrom<IEnergyCell>(delegate(IEnergyCell P)
						{
							string text2 = P.ChargeStatus();
							if (text2 != null)
							{
								E.Items.Append(" [").Append(text2).Append(']');
							}
						});
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC7E RID: 44158 RVA: 0x003F441B File Offset: 0x003F261B
		public override bool HandleEvent(GetProjectileBlueprintEvent E)
		{
			if (!this.ProjectileObject.IsNullOrEmpty())
			{
				E.Blueprint = this.ProjectileObject;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC7F RID: 44159 RVA: 0x003F4440 File Offset: 0x003F2640
		public override bool HandleEvent(NeedsReloadEvent E)
		{
			return (E.Skip == this || (E.Weapon != null && E.Weapon != this.ParentObject) || base.GetActivePartStatus(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) != ActivePartStatus.Unpowered || !this.ParentObject.IsEquippedProperly(null)) && base.HandleEvent(E);
		}

		// Token: 0x0600AC80 RID: 44160 RVA: 0x003F44B0 File Offset: 0x003F26B0
		public override bool HandleEvent(CommandReloadEvent E)
		{
			if (E.Pass >= 3 && (E.Weapon == null || E.Weapon == this.ParentObject) && !E.CheckedForReload.Contains(this))
			{
				E.CheckedForReload.Add(this);
				MissileWeapon part = this.ParentObject.GetPart<MissileWeapon>();
				if ((part == null || part.FiresManually) && (E.Weapon == this.ParentObject || this.ParentObject.IsEquippedProperly(null)))
				{
					bool flag = base.GetActivePartStatus(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) == ActivePartStatus.Unpowered;
					if (flag || (E.NeededReload.Count <= 0 && E.Reloaded.Count <= 0 && !NeedsReloadEvent.Check(E.Actor, this)))
					{
						if (flag)
						{
							E.NeededReload.Add(this);
						}
						if (this.ParentObject.WantEvent(InventoryActionEvent.ID, InventoryActionEvent.CascadeLevel))
						{
							E.TriedToReload.Add(this);
							if (InventoryActionEvent.Check(this.ParentObject, E.Actor, this.ParentObject, EnergyCellSocket.REPLACE_CELL_INTERACTION, false, false, true, false, false, 0, E.MinimumCharge, 0, null, null, null, null))
							{
								E.Reloaded.Add(this);
								if (!E.ObjectsReloaded.Contains(this.ParentObject))
								{
									E.ObjectsReloaded.Add(this.ParentObject);
								}
								E.EnergyCost(1000);
							}
						}
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC81 RID: 44161 RVA: 0x003F463C File Offset: 0x003F283C
		public override bool HandleEvent(GetMissileWeaponPerformanceEvent E)
		{
			if (this.IsPowerLoadSensitive && E.Subject == this.ParentObject && E.BaseDamage != "0")
			{
				int num = this.MyPowerLoadBonus(int.MinValue, 100, 150);
				if (num != 0)
				{
					DieRoll damageRoll = E.GetDamageRoll();
					if (damageRoll != null && damageRoll.Max() > 0)
					{
						damageRoll.AdjustResult(num);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC82 RID: 44162 RVA: 0x003F46A8 File Offset: 0x003F28A8
		public override bool HandleEvent(GetMissileWeaponProjectileEvent E)
		{
			if (!this.ProjectileObject.IsNullOrEmpty())
			{
				E.Blueprint = this.ProjectileObject;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC83 RID: 44163 RVA: 0x003F46CC File Offset: 0x003F28CC
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (!this.ProjectileObject.IsNullOrEmpty() && E.Understood())
			{
				E.AddMissileWeaponDamageTag(GetMissileWeaponPerformanceEvent.GetFor(this.ParentObject.Equipped, this.ParentObject, null, null, null, null, null, null, null, null, null, null, null, false));
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC84 RID: 44164 RVA: 0x003F4745 File Offset: 0x003F2945
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600AC85 RID: 44165 RVA: 0x003F4748 File Offset: 0x003F2948
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("GenerateIntegratedHostInitialAmmo");
			Registrar.Register("PrepIntegratedHostToReceiveAmmo");
			Registrar.Register("SupplyIntegratedHostWithAmmo");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AC86 RID: 44166 RVA: 0x003F4774 File Offset: 0x003F2974
		public string GetStatusMessage(ActivePartStatus Status)
		{
			if (Status == ActivePartStatus.Unpowered)
			{
				return this.ParentObject.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " merely " + this.ParentObject.GetVerb("click", false, false, false, true, false) + ".";
			}
			return this.ParentObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " " + this.GetStatusPhrase(Status) + ".";
		}

		// Token: 0x0600AC87 RID: 44167 RVA: 0x003F481C File Offset: 0x003F2A1C
		public string GetStatusMessage()
		{
			return this.GetStatusMessage(base.GetActivePartStatus(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null));
		}

		// Token: 0x0600AC88 RID: 44168 RVA: 0x003F4858 File Offset: 0x003F2A58
		public override int GetDraw(QueryDrawEvent E = null)
		{
			int num = base.GetDraw(E);
			MissileWeapon part = this.ParentObject.GetPart<MissileWeapon>();
			if (part != null)
			{
				num *= part.AmmoPerAction;
			}
			return num;
		}

		// Token: 0x0600AC89 RID: 44169 RVA: 0x003F4888 File Offset: 0x003F2A88
		public override bool FireEvent(Event E)
		{
			if (E.ID == "PrepIntegratedHostToReceiveAmmo")
			{
				if (this.ChargeUse > 0)
				{
					GameObject gameObjectParameter = E.GetGameObjectParameter("Host");
					if (gameObjectParameter != null && gameObjectParameter.HasPart<Robot>())
					{
						if (!gameObjectParameter.HasPart<ElectricalPowerTransmission>())
						{
							gameObjectParameter.AddPart<ElectricalPowerTransmission>(new ElectricalPowerTransmission
							{
								ChargeRate = ((this.GetChargePerAction() >= 1000) ? 10000 : 1000),
								IsConsumer = true
							}, true, false);
						}
						if (!gameObjectParameter.HasPart<Capacitor>() && !gameObjectParameter.HasTagOrProperty("NoIntegratedHostCapacitor"))
						{
							gameObjectParameter.AddPart<Capacitor>(new Capacitor
							{
								MaxCharge = this.ChargeUse * 10,
								ChargeRate = this.ChargeUse,
								MinimumChargeToExplode = 0,
								ChargeDisplayStyle = null
							}, true, false);
						}
					}
					if (!this.ParentObject.HasPart<IntegratedPowerSystems>() && ItemModding.ModificationApplicable("ModJacked", this.ParentObject, null, null))
					{
						ItemModding.ApplyModification(this.ParentObject, "ModJacked", true, null, false);
					}
				}
			}
			else if (E.ID == "SupplyIntegratedHostWithAmmo")
			{
				if (this.ChargeUse > 0)
				{
					if (E.HasFlag("TrackSupply"))
					{
						E.SetFlag("AnySupplyHandler", true);
					}
					GameObject gameObjectParameter2 = E.GetGameObjectParameter("Host");
					GameObject gameObject = E.GetGameObjectParameter("Actor") ?? E.GetGameObjectParameter("Owner");
					if (gameObjectParameter2 != null && gameObject != null)
					{
						Inventory inventory = gameObject.Inventory;
						if (inventory != null)
						{
							if (gameObject.IsPlayer())
							{
								int num = 0;
								List<GameObject> skip = Event.NewGameObjectList();
								for (;;)
								{
									string SlotType = null;
									EnergyCellSocket part = this.ParentObject.GetPart<EnergyCellSocket>();
									if (part != null)
									{
										SlotType = part.SlotType;
									}
									List<string> OptionStrings = new List<string>(16);
									List<object> options = new List<object>(16);
									List<char> keymap = new List<char>(16);
									OptionStrings.Add("none");
									options.Add(null);
									keymap.Add('-');
									char c = 'a';
									inventory.ForeachObject(delegate(GameObject GO)
									{
										if (skip.Contains(GO))
										{
											return;
										}
										if (!GO.Understood())
										{
											return;
										}
										GO.ForeachPartDescendedFrom<IEnergyCell>(delegate(IEnergyCell P)
										{
											if (SlotType == null || P.SlotType == SlotType)
											{
												OptionStrings.Add(GO.DisplayName);
												options.Add(GO);
												keymap.Add(c);
												char c2 = c;
												c = c2 + '\u0001';
												return false;
											}
											return true;
										});
									});
									if (options.Count <= 1)
									{
										break;
									}
									if (E.HasFlag("TrackSupply"))
									{
										E.SetFlag("AnySupplies", true);
									}
									int num2 = Popup.PickOption(string.Concat(new string[]
									{
										"Select ",
										(num == 0) ? "one" : "another",
										" of your cells to supply ",
										gameObjectParameter2.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
										" with, if desired."
									}), null, "", "Sounds/UI/ui_notification", OptionStrings.ToArray(), keymap.ToArray(), null, null, null, null, null, 0, 60, 0, -1, true, false, false, true, false, null, null);
									if (num2 < 0)
									{
										break;
									}
									GameObject gameObject2 = options[num2] as GameObject;
									if (gameObject2 == null)
									{
										break;
									}
									gameObject2 = gameObject2.RemoveOne();
									if (gameObjectParameter2.ReceiveObject(gameObject2, false, null))
									{
										IComponent<GameObject>.WDidXToYWithZ(gameObject, "transfer", gameObject2, "to", gameObjectParameter2, null, null, null, null, null, null, false, false, true, false, false, true, false, false, null, null, null, null, false, false, false, true, false, null);
										num++;
									}
									else
									{
										Popup.Show(gameObjectParameter2.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " cannot take " + gameObject2.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
										skip.Add(gameObject2);
									}
								}
								if (num > 0)
								{
									gameObject.UseEnergy(1000, "Ammo Magazine Transfer", null, null, false);
								}
							}
							else
							{
								int desiredCharge = this.GetDesiredCharge();
								int num3 = this.GetAccessibleCharge();
								if (num3 < desiredCharge)
								{
									List<GameObject> list = Event.NewGameObjectList();
									inventory.GetObjects(list, new Predicate<GameObject>(this.FindEnergyCells));
									int num4 = 0;
									while (num3 < desiredCharge && list.Count > 0)
									{
										if (++num4 >= 100)
										{
											break;
										}
										GameObject gameObject3 = null;
										int num5 = 0;
										foreach (GameObject gameObject4 in list)
										{
											int num6 = gameObject4.QueryCharge(false, 0L, true, true);
											if (num6 >= this.ChargeUse)
											{
												int num7 = num6 - (desiredCharge - num3);
												if (gameObject3 == null)
												{
													gameObject3 = gameObject4;
													num5 = num7;
												}
												else if (num7 >= 0)
												{
													if (num7 < num5)
													{
														gameObject3 = gameObject4;
														num5 = num7;
													}
												}
												else if (num7 > num5)
												{
													gameObject3 = gameObject4;
													num5 = num7;
												}
											}
										}
										if (gameObject3 != null)
										{
											gameObject3 = gameObject3.RemoveOne();
											IComponent<GameObject>.WDidXToYWithZ(gameObject, "transfer", gameObject3, "to", gameObjectParameter2, null, null, null, null, null, null, false, false, true, false, false, true, false, false, null, null, null, null, false, false, false, true, false, null);
											if (gameObjectParameter2.ReceiveObject(gameObject3, false, null))
											{
												num3 += gameObject3.QueryCharge(false, 0L, true, true);
											}
											else
											{
												gameObject3.CheckStack();
											}
											gameObject.UseEnergy(1000, "Ammo Magazine Transfer", null, null, false);
										}
									}
								}
							}
						}
					}
				}
			}
			else if (E.ID == "GenerateIntegratedHostInitialAmmo" && this.ChargeUse > 0)
			{
				GameObject gameObjectParameter3 = E.GetGameObjectParameter("Host");
				if (gameObjectParameter3 != null && gameObjectParameter3.Inventory != null)
				{
					int desiredCharge2 = this.GetDesiredCharge();
					int num8 = this.GetAccessibleCharge();
					int num9 = 0;
					while (num8 < desiredCharge2 && ++num9 < 100)
					{
						string text = null;
						int num10 = 0;
						foreach (GameObjectBlueprint gameObjectBlueprint in this.GetEnergyCellBlueprints())
						{
							int num11 = 0;
							if (!EnergyAmmoLoader.BlueprintMaxCharge.ContainsKey(gameObjectBlueprint.Name))
							{
								try
								{
									num11 = gameObjectBlueprint.GetPartParameter<int>("EnergyCell", "MaxCharge", 0);
									EnergyAmmoLoader.BlueprintMaxCharge.Add(gameObjectBlueprint.Name, num11);
									goto IL_0674;
								}
								catch
								{
									continue;
								}
								goto IL_0661;
							}
							goto IL_0661;
							IL_0674:
							if (num11 < this.ChargeUse)
							{
								continue;
							}
							int num12 = num11 - (desiredCharge2 - num8);
							if (text == null)
							{
								text = gameObjectBlueprint.Name;
								num10 = num12;
								continue;
							}
							if (num12 >= 0)
							{
								if (num12 < num10)
								{
									text = gameObjectBlueprint.Name;
									num10 = num12;
									continue;
								}
								continue;
							}
							else
							{
								if (num12 > num10)
								{
									text = gameObjectBlueprint.Name;
									num10 = num12;
									continue;
								}
								continue;
							}
							IL_0661:
							num11 = EnergyAmmoLoader.BlueprintMaxCharge[gameObjectBlueprint.Name];
							goto IL_0674;
						}
						if (text != null)
						{
							GameObject gameObject5 = GameObject.Create(text);
							EnergyCell part2 = gameObject5.GetPart<EnergyCell>();
							if (part2 != null)
							{
								part2.Charge = part2.MaxCharge;
								if (gameObjectParameter3.ReceiveObject(gameObject5, false, null))
								{
									num8 += part2.Charge;
								}
								else
								{
									gameObject5.Obliterate(null, false, null);
								}
							}
							else
							{
								gameObject5.Obliterate(null, false, null);
							}
						}
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600AC8A RID: 44170 RVA: 0x003F5018 File Offset: 0x003F3218
		private bool FindEnergyCells(GameObject obj)
		{
			return obj.HasPartDescendedFrom<IEnergyCell>();
		}

		// Token: 0x0600AC8B RID: 44171 RVA: 0x003F5020 File Offset: 0x003F3220
		private List<GameObjectBlueprint> GetEnergyCellBlueprints()
		{
			if (EnergyAmmoLoader.EnergyCellBlueprints == null)
			{
				EnergyAmmoLoader.EnergyCellBlueprints = new List<GameObjectBlueprint>();
				EnergyCellSocket energyCellSocket;
				string text = (this.ParentObject.TryGetPart<EnergyCellSocket>(out energyCellSocket) ? energyCellSocket.SlotType : "EnergyCell");
				foreach (GameObjectBlueprint gameObjectBlueprint in GameObjectFactory.Factory.BlueprintList)
				{
					GamePartBlueprint part = gameObjectBlueprint.GetPart("EnergyCell");
					if (part != null && !gameObjectBlueprint.HasTag("ExcludeFromDynamicEncounters", false) && !gameObjectBlueprint.HasTag("BaseObject", false) && part.GetParameter<int>("MaxCharge", 0) != 0 && part.GetParameter<string>("SlotType", "EnergyCell") == text)
					{
						EnergyAmmoLoader.EnergyCellBlueprints.Add(gameObjectBlueprint);
					}
				}
			}
			return EnergyAmmoLoader.EnergyCellBlueprints;
		}

		// Token: 0x0600AC8C RID: 44172 RVA: 0x003F5108 File Offset: 0x003F3308
		private int GetChargePerAction()
		{
			MissileWeapon part = this.ParentObject.GetPart<MissileWeapon>();
			return this.ChargeUse * part.AmmoPerAction;
		}

		// Token: 0x0600AC8D RID: 44173 RVA: 0x003F512E File Offset: 0x003F332E
		private int GetDesiredCharge()
		{
			return this.GetChargePerAction() * this.ParentObject.GetIntProperty("IntegratedWeaponHostShots", 200);
		}

		// Token: 0x0600AC8E RID: 44174 RVA: 0x003F514C File Offset: 0x003F334C
		private int GetAccessibleCharge()
		{
			int num = this.ParentObject.QueryCharge(false, 0L, true, true);
			GameObject holder = this.ParentObject.Holder;
			Inventory inventory = ((holder != null) ? holder.Inventory : null);
			if (inventory != null)
			{
				foreach (GameObject gameObject in inventory.Objects)
				{
					foreach (IEnergyCell energyCell in gameObject.GetPartsDescendedFrom<IEnergyCell>())
					{
						num += energyCell.GetCharge();
					}
				}
			}
			return num;
		}

		// Token: 0x04004758 RID: 18264
		public string ProjectileObject;

		// Token: 0x04004759 RID: 18265
		[NonSerialized]
		private static List<GameObjectBlueprint> EnergyCellBlueprints = null;

		// Token: 0x0400475A RID: 18266
		[NonSerialized]
		private static Dictionary<string, int> BlueprintMaxCharge = new Dictionary<string, int>(16);
	}
}
