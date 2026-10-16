using System;
using System.Collections.Generic;
using System.Text;
using XRL.Rules;
using XRL.UI;
using XRL.World.Parts.Mutation;
using XRL.World.Tinkering;

namespace XRL.World.Parts
{
	/// <remarks>
	/// overload behavior: if <see cref="!:IsPowerLoadSensitive" /> is true,
	/// which it is by default, the adjustment to charge used that is
	/// implemented by IActivePart (adjusting by power load as a percentage)
	/// applies to the charge-based damage/voltage calculations here,
	/// multiplied by OverloadFactor; so, for example, if base charge draw
	/// is 100, overloaded draw is 400, and OverloadFactor is the default
	/// of 1/6, effective draw for purposes of damage and voltage calculation
	/// is 100 + ((400 - 100) * (1 / 6)) = 150.
	/// </remarks>
	// Token: 0x02001103 RID: 4355
	[Serializable]
	public class ElectricalDischargeLoader : IPoweredPart
	{
		// Token: 0x0600AC56 RID: 44118 RVA: 0x003F2C78 File Offset: 0x003F0E78
		public ElectricalDischargeLoader()
		{
			this.ChargeUse = 300;
			this.WorksOnEquipper = true;
			this.IsPowerLoadSensitive = true;
			this.NameForStatus = "ElectricalDischargeSystem";
		}

		// Token: 0x0600AC57 RID: 44119 RVA: 0x003F2CC8 File Offset: 0x003F0EC8
		public override bool SameAs(IPart p)
		{
			ElectricalDischargeLoader electricalDischargeLoader = p as ElectricalDischargeLoader;
			return !(electricalDischargeLoader.ProjectileObject != this.ProjectileObject) && electricalDischargeLoader.ChargeFactor == this.ChargeFactor && electricalDischargeLoader.OverloadFactor == this.OverloadFactor && base.SameAs(p);
		}

		// Token: 0x0600AC58 RID: 44120 RVA: 0x003F2D18 File Offset: 0x003F0F18
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<AIWantUseWeaponEvent>.ID || ID == PooledEvent<CheckLoadAmmoEvent>.ID || ID == PooledEvent<CheckReadyToFireEvent>.ID || ID == SingletonEvent<CommandReloadEvent>.ID || ID == PooledEvent<GetAmmoCountAvailableEvent>.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == PooledEvent<GetMissileWeaponProjectileEvent>.ID || ID == PooledEvent<GetMissileWeaponStatusEvent>.ID || ID == PooledEvent<GetNotReadyToFireMessageEvent>.ID || ID == PooledEvent<GetProjectileBlueprintEvent>.ID || ID == PooledEvent<LoadAmmoEvent>.ID || ID == SingletonEvent<NeedsReloadEvent>.ID;
		}

		// Token: 0x0600AC59 RID: 44121 RVA: 0x003F2D94 File Offset: 0x003F0F94
		public override bool HandleEvent(AIWantUseWeaponEvent E)
		{
			EnergyCellSocket socket;
			if (base.GetActivePartStatus(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) == ActivePartStatus.Unpowered && E.Object.TryGetPart<EnergyCellSocket>(out socket))
			{
				bool result = false;
				Predicate<IEnergyCell> <>9__1;
				E.Actor.Inventory.ForeachObject(delegate(GameObject obj)
				{
					if (E.Actor.IsPlayer() && !obj.Understood())
					{
						return;
					}
					Predicate<IEnergyCell> predicate;
					if ((predicate = <>9__1) == null)
					{
						predicate = (<>9__1 = delegate(IEnergyCell part)
						{
							if (part.SlotType == socket.SlotType && part.HasCharge(this.ChargeUse))
							{
								result = true;
								return false;
							}
							return true;
						});
					}
					obj.ForeachPartDescendedFrom<IEnergyCell>(predicate);
				});
				if (!result)
				{
					return false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC5A RID: 44122 RVA: 0x003F2E38 File Offset: 0x003F1038
		public override bool HandleEvent(GetAmmoCountAvailableEvent E)
		{
			int activeChargeUse = base.GetActiveChargeUse(null, null);
			if (activeChargeUse > 0)
			{
				E.Register(this.ParentObject.QueryCharge(false, 0L, true, true) / activeChargeUse);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC5B RID: 44123 RVA: 0x003F2E84 File Offset: 0x003F1084
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

		// Token: 0x0600AC5C RID: 44124 RVA: 0x003F2EE0 File Offset: 0x003F10E0
		public override bool HandleEvent(LoadAmmoEvent E)
		{
			int num = this.MyPowerLoadLevel();
			bool flag = true;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			bool flag7 = false;
			bool flag8 = false;
			int? num2 = new int?(num);
			ActivePartStatus activePartStatus = base.GetActivePartStatus(flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, E.ActivePartsIgnoreSubject, false, 1, null, false, 0L, num2);
			if (activePartStatus == ActivePartStatus.Operational)
			{
				if (!this.ProjectileObject.IsNullOrEmpty())
				{
					num2 = new int?(num);
					int activeChargeUse = base.GetActiveChargeUse(null, num2);
					GameObject gameObject = GameObject.Create(this.ProjectileObject, 0, 0, null, null, null, "Projectile", null);
					DischargeOnHit dischargeOnHit = gameObject.RequirePart<DischargeOnHit>(false);
					dischargeOnHit.Voltage = this.GetVoltage(new int?(activeChargeUse), new int?(num)).ToString();
					dischargeOnHit.DamageRange = this.GetDamageRoll(new int?(activeChargeUse), new int?(num));
					E.Projectile = gameObject;
				}
				return base.HandleEvent(E);
			}
			if (E.Message == null)
			{
				E.Message = this.GetStatusMessage(activePartStatus);
			}
			return false;
		}

		// Token: 0x0600AC5D RID: 44125 RVA: 0x003F2FD8 File Offset: 0x003F11D8
		public override bool HandleEvent(CheckReadyToFireEvent E)
		{
			return !base.IsDisabled(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) && base.HandleEvent(E);
		}

		// Token: 0x0600AC5E RID: 44126 RVA: 0x003F3018 File Offset: 0x003F1218
		public override bool HandleEvent(GetNotReadyToFireMessageEvent E)
		{
			ActivePartStatus activePartStatus = base.GetActivePartStatus(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null);
			if (activePartStatus != ActivePartStatus.Operational)
			{
				E.Message = this.GetStatusMessage(activePartStatus);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC5F RID: 44127 RVA: 0x003F3064 File Offset: 0x003F1264
		public override bool HandleEvent(GetMissileWeaponStatusEvent E)
		{
			if (E.Override == null)
			{
				if (E.Status != null)
				{
					E.Status.ammoTotal = 1;
					E.Status.ammoRemaining = (base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) ? 1 : 0);
				}
				EnergyCellSocket part2 = this.ParentObject.GetPart<EnergyCellSocket>();
				if (part2 != null)
				{
					if (part2.Cell == null)
					{
						E.Items.Append(" [{{K|empty}}]");
					}
					else if (!part2.Cell.Understood())
					{
						E.Items.Append(" [?]");
					}
					else
					{
						part2.Cell.ForeachPartDescendedFrom<IEnergyCell>(delegate(IEnergyCell part)
						{
							string text = part.ChargeStatus();
							if (text != null)
							{
								E.Items.Append(" [").Append(text).Append("]");
							}
						});
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC60 RID: 44128 RVA: 0x003F315F File Offset: 0x003F135F
		public override bool HandleEvent(GetProjectileBlueprintEvent E)
		{
			if (!this.ProjectileObject.IsNullOrEmpty())
			{
				E.Blueprint = this.ProjectileObject;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC61 RID: 44129 RVA: 0x003F3184 File Offset: 0x003F1384
		public override bool HandleEvent(NeedsReloadEvent E)
		{
			return (E.Skip == this || (E.Weapon != null && E.Weapon != this.ParentObject) || base.GetActivePartStatus(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) != ActivePartStatus.Unpowered || !this.ParentObject.IsEquippedProperly(null)) && base.HandleEvent(E);
		}

		// Token: 0x0600AC62 RID: 44130 RVA: 0x003F31F4 File Offset: 0x003F13F4
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

		// Token: 0x0600AC63 RID: 44131 RVA: 0x003F3380 File Offset: 0x003F1580
		public override bool HandleEvent(GetMissileWeaponProjectileEvent E)
		{
			if (!this.ProjectileObject.IsNullOrEmpty())
			{
				E.Blueprint = this.ProjectileObject;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC64 RID: 44132 RVA: 0x003F33A4 File Offset: 0x003F15A4
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (!this.ProjectileObject.IsNullOrEmpty() && E.Understood())
			{
				StringBuilder stringBuilder = Event.NewStringBuilder(null);
				stringBuilder.Append("{{W|").Append('\u0003').Append("}}")
					.Append(this.GetDamageRoll(null, null));
				E.AddTag(stringBuilder.ToString(), -20);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC65 RID: 44133 RVA: 0x003F341B File Offset: 0x003F161B
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600AC66 RID: 44134 RVA: 0x003F341E File Offset: 0x003F161E
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("GenerateIntegratedHostInitialAmmo");
			Registrar.Register("PrepIntegratedHostToReceiveAmmo");
			Registrar.Register("SupplyIntegratedHostWithAmmo");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AC67 RID: 44135 RVA: 0x003F344C File Offset: 0x003F164C
		public string GetStatusMessage(ActivePartStatus Status)
		{
			if (Status == ActivePartStatus.Unpowered)
			{
				return this.ParentObject.Does("click", int.MaxValue, null, null, "merely", false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + ".";
			}
			return this.ParentObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " " + this.GetStatusPhrase(Status) + ".";
		}

		// Token: 0x0600AC68 RID: 44136 RVA: 0x003F34E8 File Offset: 0x003F16E8
		public string GetStatusMessage()
		{
			return this.GetStatusMessage(base.GetActivePartStatus(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null));
		}

		// Token: 0x0600AC69 RID: 44137 RVA: 0x003F3524 File Offset: 0x003F1724
		public override int GetDraw(QueryDrawEvent E = null)
		{
			int num = base.GetDraw(E);
			MissileWeapon missileWeapon;
			if (this.ParentObject.TryGetPart<MissileWeapon>(out missileWeapon))
			{
				num *= missileWeapon.AmmoPerAction;
			}
			return num;
		}

		// Token: 0x0600AC6A RID: 44138 RVA: 0x003F3554 File Offset: 0x003F1754
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
										GO.ForeachPartDescendedFrom<IEnergyCell>(delegate(IEnergyCell part)
										{
											if (SlotType == null || part.SlotType == SlotType)
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
										gameObjectParameter2.the,
										gameObjectParameter2.ShortDisplayName,
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
										Popup.Show(string.Concat(new string[] { gameObjectParameter2.The, gameObjectParameter2.ShortDisplayName, " cannot take ", gameObject2.the, gameObject2.ShortDisplayName, "." }), null, "Sounds/UI/ui_notification", true, true, true, true, null);
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
							if (!ElectricalDischargeLoader.BlueprintMaxCharge.ContainsKey(gameObjectBlueprint.Name))
							{
								try
								{
									num11 = gameObjectBlueprint.GetPartParameter<int>("EnergyCell", "MaxCharge", 0);
									ElectricalDischargeLoader.BlueprintMaxCharge.Add(gameObjectBlueprint.Name, num11);
									goto IL_0642;
								}
								catch
								{
									continue;
								}
								goto IL_062F;
							}
							goto IL_062F;
							IL_0642:
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
							IL_062F:
							num11 = ElectricalDischargeLoader.BlueprintMaxCharge[gameObjectBlueprint.Name];
							goto IL_0642;
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

		// Token: 0x0600AC6B RID: 44139 RVA: 0x003F3CB4 File Offset: 0x003F1EB4
		private bool FindEnergyCells(GameObject obj)
		{
			return obj.HasPartDescendedFrom<IEnergyCell>();
		}

		// Token: 0x0600AC6C RID: 44140 RVA: 0x003F3CBC File Offset: 0x003F1EBC
		private List<GameObjectBlueprint> GetEnergyCellBlueprints()
		{
			if (ElectricalDischargeLoader.EnergyCellBlueprints == null)
			{
				ElectricalDischargeLoader.EnergyCellBlueprints = new List<GameObjectBlueprint>();
				foreach (GameObjectBlueprint gameObjectBlueprint in GameObjectFactory.Factory.BlueprintList)
				{
					if (gameObjectBlueprint.HasPart("EnergyCell") && !gameObjectBlueprint.HasTag("ExcludeFromDynamicEncounters", false) && !gameObjectBlueprint.HasTag("BaseObject", false) && gameObjectBlueprint.GetPartParameter<int>("EnergyCell", "MaxCharge", -1) != -1)
					{
						ElectricalDischargeLoader.EnergyCellBlueprints.Add(gameObjectBlueprint);
					}
				}
			}
			return ElectricalDischargeLoader.EnergyCellBlueprints;
		}

		// Token: 0x0600AC6D RID: 44141 RVA: 0x003F3D70 File Offset: 0x003F1F70
		private int GetChargePerAction()
		{
			MissileWeapon part = this.ParentObject.GetPart<MissileWeapon>();
			return this.ChargeUse * part.AmmoPerAction;
		}

		// Token: 0x0600AC6E RID: 44142 RVA: 0x003F3D96 File Offset: 0x003F1F96
		private int GetDesiredCharge()
		{
			return this.GetChargePerAction() * this.ParentObject.GetIntProperty("IntegratedWeaponHostShots", 100);
		}

		// Token: 0x0600AC6F RID: 44143 RVA: 0x003F3DB4 File Offset: 0x003F1FB4
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

		// Token: 0x0600AC70 RID: 44144 RVA: 0x003F3E74 File Offset: 0x003F2074
		public string GetDamageRoll(int? ChargeUse = null, int? PowerLoadLevel = null)
		{
			string text = ElectricalGeneration.GetDischargeDamageRoll(this.GetEffectiveCharge(ChargeUse, PowerLoadLevel));
			if (text == null)
			{
				return null;
			}
			MissilePerformance missilePerformance;
			if (this.ParentObject.TryGetPart<MissilePerformance>(out missilePerformance) && missilePerformance.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				if (missilePerformance.DamageDieModifier != 0)
				{
					text = DieRoll.AdjustDieSize(text, missilePerformance.DamageDieModifier);
				}
				if (missilePerformance.DamageModifier != 0)
				{
					text = DieRoll.AdjustResult(text, missilePerformance.DamageModifier);
				}
			}
			if (this.ParentObject.HasPart<ModGigantic>())
			{
				text = DieRoll.AdjustResult(text, 3);
			}
			return text;
		}

		// Token: 0x0600AC71 RID: 44145 RVA: 0x003F3F0B File Offset: 0x003F210B
		public int GetVoltage(int? ChargeUse = null, int? PowerLoadLevel = null)
		{
			return ElectricalGeneration.GetDischargeVoltage(this.GetEffectiveCharge(ChargeUse, PowerLoadLevel));
		}

		// Token: 0x0600AC72 RID: 44146 RVA: 0x003F3F1C File Offset: 0x003F211C
		public int GetEffectiveCharge(int? ChargeUse = null, int? PowerLoadLevel = null)
		{
			int num = PowerLoadLevel ?? this.MyPowerLoadLevel();
			int? num2 = ChargeUse;
			float num3 = (float)(num2 ?? base.GetActiveChargeUse(ChargeUse, PowerLoadLevel));
			if (num != 100)
			{
				num2 = new int?(100);
				int activeChargeUse = base.GetActiveChargeUse(null, num2);
				num3 = (float)activeChargeUse + (num3 - (float)activeChargeUse) * this.OverloadFactor;
			}
			return (int)Math.Round((double)(num3 * this.ChargeFactor));
		}

		// Token: 0x04004753 RID: 18259
		public string ProjectileObject;

		// Token: 0x04004754 RID: 18260
		public float ChargeFactor = 15f;

		// Token: 0x04004755 RID: 18261
		public float OverloadFactor = 0.16666667f;

		// Token: 0x04004756 RID: 18262
		[NonSerialized]
		private static List<GameObjectBlueprint> EnergyCellBlueprints;

		// Token: 0x04004757 RID: 18263
		[NonSerialized]
		private static Dictionary<string, int> BlueprintMaxCharge = new Dictionary<string, int>(16);
	}
}
