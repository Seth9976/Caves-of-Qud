using System;
using System.Collections.Generic;
using UnityEngine;
using XRL.UI;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	/// <summary>
	///     The socket that holds the energy cell powering a device.
	/// </summary>
	// Token: 0x020011C4 RID: 4548
	[Serializable]
	public class EnergyCellSocket : IPoweredPart
	{
		// Token: 0x0600B4B6 RID: 46262 RVA: 0x0041E8BC File Offset: 0x0041CABC
		public EnergyCellSocket()
		{
			this.ChargeUse = 0;
			this.IsBootSensitive = false;
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B4B7 RID: 46263 RVA: 0x0041E940 File Offset: 0x0041CB40
		public override bool CanGenerateStacked()
		{
			if (this.ChanceSlotted != 0)
			{
				if (this.ChanceSlotted < 100)
				{
					return false;
				}
				if (this.SlottedType.Contains("#") || this.SlottedType.Contains("@") || this.SlottedType.Contains("*"))
				{
					return false;
				}
			}
			return base.CanGenerateStacked();
		}

		// Token: 0x0600B4B8 RID: 46264 RVA: 0x0041E9A0 File Offset: 0x0041CBA0
		public override IPart DeepCopy(GameObject Parent, Func<GameObject, GameObject> MapInv)
		{
			EnergyCellSocket energyCellSocket = new EnergyCellSocket();
			energyCellSocket.SlotType = this.SlotType;
			energyCellSocket.ChanceSlotted = this.ChanceSlotted;
			if (GameObject.Validate(ref this.Cell))
			{
				energyCellSocket.Cell = ((MapInv != null) ? MapInv(this.Cell) : null) ?? this.Cell.DeepCopy(false, false, MapInv);
				if (energyCellSocket.Cell != null)
				{
					energyCellSocket.Cell.ForeachPartDescendedFrom<IEnergyCell>(delegate(IEnergyCell p)
					{
						p.SlottedIn = Parent;
					});
				}
			}
			energyCellSocket.SlottedType = this.SlottedType;
			energyCellSocket.ChanceFullCell = this.ChanceFullCell;
			energyCellSocket.ChanceDestroyCellOnForcedUnequip = this.ChanceDestroyCellOnForcedUnequip;
			energyCellSocket.RemoveCellUnpoweredSound = this.RemoveCellUnpoweredSound;
			energyCellSocket.RemoveCellPoweredSound = this.RemoveCellPoweredSound;
			energyCellSocket.SlotCellUnpoweredSound = this.SlotCellUnpoweredSound;
			energyCellSocket.SlotCellPoweredSound = this.SlotCellPoweredSound;
			energyCellSocket.ParentObject = Parent;
			return energyCellSocket;
		}

		// Token: 0x0600B4B9 RID: 46265 RVA: 0x0041EA90 File Offset: 0x0041CC90
		public override bool SameAs(IPart Part)
		{
			EnergyCellSocket energyCellSocket = Part as EnergyCellSocket;
			return !(energyCellSocket.SlotType != this.SlotType) && energyCellSocket.Cell == null && this.Cell == null && energyCellSocket.ChanceDestroyCellOnForcedUnequip == this.ChanceDestroyCellOnForcedUnequip && !(energyCellSocket.RemoveCellUnpoweredSound != this.RemoveCellUnpoweredSound) && !(energyCellSocket.RemoveCellPoweredSound != this.RemoveCellPoweredSound) && !(energyCellSocket.SlotCellUnpoweredSound != this.SlotCellUnpoweredSound) && !(energyCellSocket.SlotCellPoweredSound != this.SlotCellPoweredSound) && energyCellSocket.VisibleInDisplayName == this.VisibleInDisplayName && energyCellSocket.VisibleInDescription == this.VisibleInDescription && base.SameAs(Part);
		}

		// Token: 0x0600B4BA RID: 46266 RVA: 0x0041EB56 File Offset: 0x0041CD56
		private bool CellWantsEvent(int ID, int cascade)
		{
			return GameObject.Validate(this.Cell) && MinEvent.CascadeTo(cascade, 4) && this.Cell.WantEvent(ID, cascade);
		}

		// Token: 0x0600B4BB RID: 46267 RVA: 0x0041EB80 File Offset: 0x0041CD80
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<BeginBeingUnequippedEvent>.ID || ID == SingletonEvent<EndTurnEvent>.ID || ID == PooledEvent<GetContentsEvent>.ID || (ID == PooledEvent<GetDisplayNameEvent>.ID && this.VisibleInDisplayName) || (ID == GetShortDescriptionEvent.ID && this.VisibleInDescription) || ID == GetExtrinsicValueEvent.ID || ID == GetExtrinsicWeightEvent.ID || ID == GetInventoryActionsEvent.ID || ID == PooledEvent<GetReplaceCellInteractionsEvent>.ID || ID == InventoryActionEvent.ID || (ID == ObjectCreatedEvent.ID && this.ChanceSlotted > 0) || ID == PooledEvent<StripContentsEvent>.ID || this.CellWantsEvent(ID, cascade);
		}

		// Token: 0x0600B4BC RID: 46268 RVA: 0x0041EC1D File Offset: 0x0041CE1D
		public override bool HandleEvent(MinEvent E)
		{
			return base.HandleEvent(E) && (!E.CascadeTo(4) || !GameObject.Validate(this.Cell) || E.Dispatch(this.Cell));
		}

		// Token: 0x0600B4BD RID: 46269 RVA: 0x0041EC51 File Offset: 0x0041CE51
		public override bool HandleEvent(GetReplaceCellInteractionsEvent E)
		{
			E.Add(this.ParentObject, this.GetReplaceCellPriority(), EnergyCellSocket.REPLACE_CELL_INTERACTION);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B4BE RID: 46270 RVA: 0x0041EC74 File Offset: 0x0041CE74
		public override bool HandleEvent(BeginBeingUnequippedEvent E)
		{
			if (E.Forced && this.ChanceDestroyCellOnForcedUnequip > 0)
			{
				GameObject cell = this.Cell;
				if (GameObject.Validate(ref cell) && this.ChanceDestroyCellOnForcedUnequip.in100() && cell.FireEvent("CanForcedUnequipDestroy"))
				{
					this.SetCell(null);
					cell.Destroy(null, false, false, null);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B4BF RID: 46271 RVA: 0x0041ECD8 File Offset: 0x0041CED8
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (this.VisibleInDisplayName && !E.Reference && E.Understood())
			{
				if (!GameObject.Validate(ref this.Cell))
				{
					E.AddTag("{{y|[{{K|no cell}}]}}", -5);
				}
				else
				{
					E.AddTag("{{y|[" + this.Cell.DisplayName + "]}}", -5);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B4C0 RID: 46272 RVA: 0x0041ED44 File Offset: 0x0041CF44
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			IEnergyCell energyCell;
			if (this.VisibleInDescription && E.Understood() && GameObject.Validate(ref this.Cell) && this.Cell.TryGetPartDescendedFrom<IEnergyCell>(out energyCell))
			{
				E.Postfix.Append("\n{{rules|").AppendCase(this.Cell.ShortDisplayNameStripped, true).Append(": ")
					.Append(energyCell.ChargeStatus())
					.Append("}}");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B4C1 RID: 46273 RVA: 0x0041EDC5 File Offset: 0x0041CFC5
		public override bool HandleEvent(EndTurnEvent E)
		{
			return (!GameObject.Validate(ref this.Cell) || !E.CascadeTo(4) || E.Dispatch(this.Cell)) && base.HandleEvent(E);
		}

		// Token: 0x0600B4C2 RID: 46274 RVA: 0x0041EDF4 File Offset: 0x0041CFF4
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (GameObject.Validate(ref this.Cell))
			{
				E.AddAction("Replace Cell", "replace cell", EnergyCellSocket.REPLACE_CELL_INTERACTION, "cell", 'c', false, 0, 0, false, false, false, false, true, null, false);
				GetSlottedInventoryActionsEvent.Send(this.Cell, E);
			}
			else
			{
				E.AddAction("Replace Cell", "install cell", EnergyCellSocket.REPLACE_CELL_INTERACTION, null, 'c', false, 15, 0, false, false, false, false, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B4C3 RID: 46275 RVA: 0x0041EE70 File Offset: 0x0041D070
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == EnergyCellSocket.REPLACE_CELL_INTERACTION)
			{
				return this.AttemptReplaceCell(E.Actor, E, E.MinimumCharge, E.ObjectTarget);
			}
			if (E.Command == "EmptyForDisassemble" && GameObject.Validate(ref this.Cell))
			{
				GameObject gameObject;
				Cell cell;
				this.ParentObject.GetContext(out gameObject, out cell);
				if (gameObject != null)
				{
					gameObject.ReceiveObject(this.Cell, false, null);
				}
				else if (cell != null)
				{
					cell.AddObject(this.Cell, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				}
				else
				{
					E.Generated.Add(this.Cell);
					E.Actor.ReceiveObject(this.Cell, false, null);
				}
				this.SetCell(null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B4C4 RID: 46276 RVA: 0x0041EF3F File Offset: 0x0041D13F
		public override bool HandleEvent(GetExtrinsicValueEvent E)
		{
			if (GameObject.Validate(ref this.Cell))
			{
				E.Value += this.Cell.Value;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B4C5 RID: 46277 RVA: 0x0041EF6D File Offset: 0x0041D16D
		public override bool HandleEvent(GetExtrinsicWeightEvent E)
		{
			if (GameObject.Validate(ref this.Cell))
			{
				E.Weight += this.Cell.GetWeight();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B4C6 RID: 46278 RVA: 0x0041EF9C File Offset: 0x0041D19C
		public override bool HandleEvent(StripContentsEvent E)
		{
			if (GameObject.Validate(ref this.Cell) && (!E.KeepNatural || !this.Cell.IsNatural()))
			{
				GameObject cell = this.Cell;
				this.SetCell(null);
				cell.Obliterate(null, false, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B4C7 RID: 46279 RVA: 0x0041EFE8 File Offset: 0x0041D1E8
		public override bool HandleEvent(GetContentsEvent E)
		{
			if (GameObject.Validate(ref this.Cell))
			{
				E.Objects.Add(this.Cell);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B4C8 RID: 46280 RVA: 0x0041F010 File Offset: 0x0041D210
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			if (this.ChanceSlotted.in100())
			{
				GameObjectFactory.ProcessSpecification(this.SlottedType, new Action<GameObject>(this.LoadCell), null, 1, 0, 0, null, null, null, null, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B4C9 RID: 46281 RVA: 0x0041F052 File Offset: 0x0041D252
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B4CA RID: 46282 RVA: 0x0041F058 File Offset: 0x0041D258
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("InductionCharge");
			Registrar.Register("QueryCharge");
			Registrar.Register("QueryChargeStorage");
			Registrar.Register("QueryRechargeStorage");
			Registrar.Register("RechargeAvailable");
			Registrar.Register("TestCharge");
			Registrar.Register("UseCharge");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B4CB RID: 46283 RVA: 0x0041F0BA File Offset: 0x0041D2BA
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (GameObject.Validate(ref this.Cell))
			{
				this.Cell.TurnTick(TimeTick, Amount);
			}
		}

		// Token: 0x0600B4CC RID: 46284 RVA: 0x0041F0D8 File Offset: 0x0041D2D8
		public void SetCell(GameObject Object)
		{
			if (Object == this.Cell)
			{
				return;
			}
			if (this.Cell != null)
			{
				this.Cell.ForeachPartDescendedFrom<IEnergyCell>(delegate(IEnergyCell p)
				{
					p.SlottedIn = null;
				});
			}
			this.Cell = Object;
			if (Object != null)
			{
				Object.ForeachPartDescendedFrom<IEnergyCell>(delegate(IEnergyCell p)
				{
					p.SlottedIn = this.ParentObject;
				});
			}
			if (Sidebar.CurrentTarget == Object)
			{
				Sidebar.CurrentTarget = null;
			}
			base.FlushTransientCaches();
		}

		// Token: 0x0600B4CD RID: 46285 RVA: 0x0041F154 File Offset: 0x0041D354
		private void LoadCell(GameObject Object)
		{
			if (Object == null)
			{
				Debug.LogError("Unknown cell type: " + this.SlottedType);
				return;
			}
			IEnergyCell partDescendedFrom = Object.GetPartDescendedFrom<IEnergyCell>();
			if (partDescendedFrom == null)
			{
				Debug.LogError("Cell type has no IEnergyCell part: " + Object.Blueprint);
				return;
			}
			if (this.Cell != null)
			{
				Debug.LogError("Multiple cells generated: " + this.Cell.Blueprint + ", " + Object.Blueprint);
				return;
			}
			this.SetCell(Object);
			if (this.ChanceFullCell.in100())
			{
				partDescendedFrom.MaximizeCharge();
				return;
			}
			if (!this.CellStartChargePercentage.IsNullOrEmpty())
			{
				partDescendedFrom.SetChargePercentage(this.CellStartChargePercentage.RollCached());
				return;
			}
			partDescendedFrom.RandomizeCharge();
		}

		// Token: 0x0600B4CE RID: 46286 RVA: 0x0041F208 File Offset: 0x0041D408
		public bool AttemptRemoveCell(GameObject Owner, InventoryActionEvent E, bool CheckStack = true)
		{
			GameObject cell = this.Cell;
			bool flag = cell.GetIntProperty("NeverStack", 0) > 0;
			if (!flag)
			{
				cell.SetIntProperty("NeverStack", 1, false);
			}
			try
			{
				Event @event = Event.New("BeforeRemoveCell", "Object", cell);
				bool flag2 = this.ParentObject.FireEvent(@event);
				@event.ID = "CommandTakeObject";
				if (flag2 && E.OverrideEnergyCost)
				{
					@event.SetParameter("EnergyCost", E.EnergyCostOverride);
				}
				if (!flag2 || Owner.Inventory == null || !Owner.Inventory.FireEvent(@event))
				{
					Owner.Fail("You can't remove " + this.Cell.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + "!");
					return false;
				}
				Owner.PlayWorldOrUISound("Sounds/Missile/Special/sfx_missile_energyWeapon_cellRemoved", null);
				IComponent<GameObject>.WDidXToYWithZ(Owner, "pop", cell, "out of", this.ParentObject, null, null, null, null, null, null, false, false, false, false, false, false, false, false, null, null, (this.ParentObject.Equipped == Owner || this.ParentObject.InInventory == Owner) ? Owner : null, null, false, false, false, false, false, null);
			}
			finally
			{
				if (!flag)
				{
					cell.RemoveIntProperty("NeverStack");
				}
			}
			cell.RemoveIntProperty("StoredByPlayer");
			this.SetCell(null);
			if (CheckStack)
			{
				cell.CheckStack();
			}
			return true;
		}

		// Token: 0x0600B4CF RID: 46287 RVA: 0x0041F398 File Offset: 0x0041D598
		public bool AttemptReplaceCell(GameObject Actor, InventoryActionEvent E, int MinimumCharge = 0, GameObject NewCell = null)
		{
			GameObject.Validate(ref this.Cell);
			if (Actor == null)
			{
				Actor = this.ParentObject.Holder;
				if (Actor == null)
				{
					return false;
				}
			}
			if (this.ParentObject.HasRegisteredEvent("BeforeReplaceCell"))
			{
				Event @event = Event.New("BeforeReplaceCell", "Actor", Actor, "Cell", this.Cell, "NewCell", NewCell);
				if (!this.ParentObject.FireEvent(@event, E))
				{
					return false;
				}
			}
			bool flag = false;
			if (this.Cell != null && this.Cell.GetIntProperty("StoredByPlayer", 0) < 1 && Actor.IsPlayer() && this.ParentObject.Owner != null && this.ParentObject.Equipped != Actor && this.ParentObject.InInventory != Actor)
			{
				if (Popup.ShowYesNoCancel(string.Concat(new string[]
				{
					this.ParentObject.IndicativeDistal,
					this.ParentObject.Is,
					" not owned by you. Are you sure you want to access ",
					this.ParentObject.its,
					" energy cell?"
				}), "Sounds/UI/ui_notification", true, DialogResult.Cancel) != DialogResult.Yes)
				{
					return false;
				}
				flag = true;
			}
			Inventory inventory = Actor.Inventory;
			if (inventory == null)
			{
				return false;
			}
			GameObject cell = this.Cell;
			bool flag2 = this.Cell != null && this.Cell.QueryCharge(false, 0L, true, true) > 0 && !base.IsBroken() && !base.IsRusted() && !base.IsEMPed();
			bool flag3 = false;
			bool flag4 = false;
			int num = 0;
			if (NewCell == null)
			{
				bool flag5 = this.ParentObject.GetObjectContext() == Actor;
				List<string> list = new List<string>(16);
				List<object> list2 = new List<object>(16);
				List<char> list3 = new List<char>(16);
				if (this.Cell != null)
				{
					list.Add("remove cell: " + cell.DisplayName);
					list2.Add(null);
					list3.Add('-');
					if (flag5)
					{
						num++;
					}
					TinkerItem part = this.Cell.GetPart<TinkerItem>();
					if (part != null && part.CanBeDisassembled(Actor))
					{
						list.Add("disassemble cell");
						list2.Add(-1);
						list3.Add('/');
						if (flag5)
						{
							num++;
						}
					}
				}
				List<GameObject> objs = Event.NewGameObjectList();
				Dictionary<GameObject, int> charge = new Dictionary<GameObject, int>();
				Dictionary<GameObject, string> names = new Dictionary<GameObject, string>();
				inventory.ForeachObject(delegate(GameObject GO)
				{
					if (Actor.IsPlayer() && !GO.Understood())
					{
						return;
					}
					GO.ForeachPartDescendedFrom<IEnergyCell>(delegate(IEnergyCell P)
					{
						if (P.SlotType == this.SlotType && CanBeSlottedEvent.Check(this.ParentObject, GO, P))
						{
							objs.Add(GO);
							charge[GO] = P.GetCharge();
							names[GO] = GO.DisplayName;
							return false;
						}
						return true;
					});
				});
				if (objs.Count > 1)
				{
					objs.Sort(delegate(GameObject a, GameObject b)
					{
						int num4 = charge[a].CompareTo(charge[b]);
						if (num4 != 0)
						{
							return -num4;
						}
						return names[a].CompareTo(names[b]);
					});
				}
				else if (objs.Count == 0)
				{
					num = 0;
				}
				char c = 'a';
				foreach (GameObject gameObject in objs)
				{
					if (this.CompatibleCell(gameObject))
					{
						list.Add(names[gameObject]);
						list2.Add(gameObject);
						if (c <= 'z')
						{
							List<char> list4 = list3;
							char c2 = c;
							c = c2 + '\u0001';
							list4.Add(c2);
						}
						else
						{
							list3.Add(' ');
						}
					}
				}
				if (list2.Count == 0)
				{
					Actor.Fail("You have no cells that fit.");
					return false;
				}
				int choice = -1;
				if (Actor.IsPlayer())
				{
					choice = Popup.PickOption("Choose a cell for " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false), null, "", "Sounds/UI/ui_notification", list.ToArray(), list3.ToArray(), null, null, null, null, null, 0, 60, num, -1, true, false, false, true, false, null, null);
				}
				else
				{
					GameObject cell2 = this.Cell;
					if (cell2 != null)
					{
						cell2.ForeachPartDescendedFrom<IEnergyCell>(delegate(IEnergyCell P)
						{
							MinimumCharge = Math.Max(MinimumCharge, P.GetCharge() + 1);
						});
					}
					int maxCharge = -1;
					int num2;
					int x;
					Action<IEnergyCell> <>9__4;
					for (x = 0; x < list2.Count; x = num2 + 1)
					{
						GameObject gameObject2 = list2[x] as GameObject;
						if (gameObject2 != null)
						{
							GameObject gameObject3 = gameObject2;
							Action<IEnergyCell> action;
							if ((action = <>9__4) == null)
							{
								action = (<>9__4 = delegate(IEnergyCell P)
								{
									int charge2 = P.GetCharge();
									if (charge2 > maxCharge && charge2 > 0 && charge2 >= MinimumCharge)
									{
										choice = x;
										maxCharge = charge2;
									}
								});
							}
							gameObject3.ForeachPartDescendedFrom<IEnergyCell>(action);
						}
						num2 = x;
					}
				}
				if (choice < 0)
				{
					return false;
				}
				if (list2[choice] == null)
				{
					if (this.AttemptRemoveCell(Actor, E, true))
					{
						flag3 = true;
					}
				}
				else
				{
					int? num3 = list2[choice] as int?;
					int num2 = -1;
					if ((num3.GetValueOrDefault() == num2) & (num3 != null))
					{
						Cell cell3;
						if ((cell3 = this.ParentObject.GetCellContext()) == null)
						{
							GameObject objectContext = this.ParentObject.GetObjectContext();
							cell3 = ((objectContext != null) ? objectContext.GetCellContext() : null);
						}
						Cell cell4 = cell3;
						GameObject cell5 = this.Cell;
						if (this.AttemptRemoveCell(Actor, E, false))
						{
							flag3 = true;
							if (cell4 != null)
							{
								cell5.RemoveFromContext(null);
								cell4.AddObject(cell5, false, false, true, false, false, true, true, null, null, null, null, null, null, null, null);
							}
							InventoryActionEvent.Check(cell5, Actor, cell5, "Disassemble", false, false, false, false, false, 0, 0, 0, null, null, null, null);
							base.FlushWeightCaches();
							if (GameObject.Validate(ref cell5))
							{
								cell5.CheckStack();
							}
						}
					}
					else
					{
						NewCell = list2[choice] as GameObject;
					}
				}
			}
			else if (!this.CompatibleCell(NewCell))
			{
				Actor.Fail("That won't fit!");
				return false;
			}
			if (NewCell != null)
			{
				bool flag6 = false;
				if (this.Cell == null)
				{
					Actor.UseEnergy(E.OverrideEnergyCost ? E.EnergyCostOverride : 1000, "Reload Energy Cell", null, null, false);
					flag6 = true;
				}
				else if (this.AttemptRemoveCell(Actor, E, true))
				{
					flag6 = true;
					flag3 = true;
				}
				if (flag6)
				{
					this.ParentObject.SplitFromStack();
					NewCell = NewCell.RemoveOne();
					Event event2 = Event.New("CommandRemoveObject", 0, 0, 0);
					event2.SetParameter("Object", NewCell);
					event2.SetFlag("ForEquip", true);
					event2.SetSilent(true);
					if (!inventory.FireEvent(event2))
					{
						Actor.Fail("You can't take the new cell out of your inventory!");
						NewCell.CheckStack();
					}
					else
					{
						flag4 = true;
						NewCell.RemoveFromContext(null);
						this.SetCell(NewCell);
						IComponent<GameObject>.WDidXToYWithZ(Actor, "slot", this.Cell, "into", this.ParentObject, null, null, null, null, null, null, false, false, true, false, false, false, false, false, null, null, (this.ParentObject.Equipped == Actor || this.ParentObject.InInventory == Actor) ? Actor : null, null, false, false, false, false, false, null);
					}
				}
				else
				{
					Actor.Fail("You can't remove the old cell!");
				}
			}
			bool flag7 = this.Cell != null && this.Cell.QueryCharge(false, 0L, true, true) > 0 && !base.IsBroken() && !base.IsRusted() && !base.IsEMPed();
			if (flag3 || flag4)
			{
				string tag = this.ParentObject.GetTag("ReloadSound", null);
				if (flag4 && !tag.IsNullOrEmpty())
				{
					base.PlayWorldSound(tag, 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
				}
				else if (flag4 && !(flag7 ? this.SlotCellPoweredSound : this.SlotCellUnpoweredSound).IsNullOrEmpty())
				{
					base.PlayWorldSound(flag7 ? this.SlotCellPoweredSound : this.SlotCellUnpoweredSound, 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
				}
				else if (flag3 && !(flag2 ? this.RemoveCellPoweredSound : this.RemoveCellUnpoweredSound).IsNullOrEmpty())
				{
					base.PlayWorldSound(flag2 ? this.RemoveCellPoweredSound : this.RemoveCellUnpoweredSound, 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
				}
				CellChangedEvent.Send(Actor, this.ParentObject, cell, this.Cell);
			}
			if (this.Cell != null && Actor.IsPlayer())
			{
				this.Cell.SetIntProperty("StoredByPlayer", 1, false);
			}
			if (flag3 && flag)
			{
				this.ParentObject.Physics.BroadcastForHelp(Actor);
			}
			this.ParentObject.CheckStack();
			return flag3 || flag4;
		}

		// Token: 0x0600B4D0 RID: 46288 RVA: 0x0041FCF4 File Offset: 0x0041DEF4
		public bool CompatibleCell(GameObject Object)
		{
			if (GameObject.Validate(ref Object))
			{
				int i = 0;
				int count = Object.PartsList.Count;
				while (i < count)
				{
					IEnergyCell energyCell = Object.PartsList[i] as IEnergyCell;
					if (energyCell != null && energyCell.SlotType == this.SlotType)
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x0600B4D1 RID: 46289 RVA: 0x0041FD50 File Offset: 0x0041DF50
		public IEnergyCell CompatibleCellPart(GameObject Object)
		{
			if (GameObject.Validate(ref Object))
			{
				int i = 0;
				int count = Object.PartsList.Count;
				while (i < count)
				{
					IEnergyCell energyCell = Object.PartsList[i] as IEnergyCell;
					if (energyCell != null && energyCell.SlotType == this.SlotType)
					{
						return energyCell;
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x0600B4D2 RID: 46290 RVA: 0x0041FDAC File Offset: 0x0041DFAC
		public override bool FireEvent(Event E)
		{
			if (E.ID == "TestCharge" || E.ID == "QueryCharge" || E.ID == "RechargeAvailable" || E.ID == "InductionCharge" || E.ID == "QueryRechargeStorage" || E.ID == "QueryChargeStorage")
			{
				if (GameObject.Validate(ref this.Cell) && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) && !this.Cell.FireEvent(E))
				{
					return false;
				}
			}
			else if (E.ID == "UseCharge" && GameObject.Validate(ref this.Cell) && base.IsReady(E.GetIntParameter("Charge", 0) > 0, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) && !this.Cell.FireEvent(E))
			{
				return false;
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600B4D3 RID: 46291 RVA: 0x0041FEDC File Offset: 0x0041E0DC
		public int GetReplaceCellPriority()
		{
			int num = 100;
			IEnergyCell energyCell = this.CompatibleCellPart(this.Cell);
			if (energyCell != null)
			{
				num -= energyCell.GetChargePercentage();
			}
			if (this.ParentObject.Equipped != null || this.ParentObject.Implantee != null)
			{
				num += 200;
			}
			if ((this.ParentObject.HasPart<EnergyAmmoLoader>() || this.ParentObject.HasPart<ElectricalDischargeLoader>()) && !this.ParentObject.HasPart<PointDefense>())
			{
				num -= 100;
			}
			if (this.ParentObject.IsBroken() || this.ParentObject.IsRusted() || this.ParentObject.IsEMPed())
			{
				num -= 100;
			}
			if (this.ParentObject.IsImportant())
			{
				num += 100;
			}
			long longProperty = this.ParentObject.GetLongProperty("LastInventoryActionTurn", 0L);
			if (longProperty > 0L && The.CurrentTurn - longProperty < 10L)
			{
				num += 100;
			}
			return num;
		}

		// Token: 0x0600B4D4 RID: 46292 RVA: 0x0041FFBB File Offset: 0x0041E1BB
		public string GetSlotTypeName()
		{
			return EnergyStorage.GetSlotTypeName(this.SlotType);
		}

		// Token: 0x0600B4D5 RID: 46293 RVA: 0x0041FFC8 File Offset: 0x0041E1C8
		public string GetSlotTypeShortName()
		{
			return EnergyStorage.GetSlotTypeShortName(this.SlotType);
		}

		// Token: 0x040048D6 RID: 18646
		public static readonly string REPLACE_CELL_INTERACTION = "ReplaceSocketCell";

		// Token: 0x040048D7 RID: 18647
		public string SlotType = "EnergyCell";

		// Token: 0x040048D8 RID: 18648
		public GameObject Cell;

		/// <summary>
		///     Chance in 100 to be slotted when created.
		/// </summary>
		// Token: 0x040048D9 RID: 18649
		public int ChanceSlotted = 50;

		/// <summary>
		///     If <see cref="F:XRL.World.Parts.EnergyCellSocket.ChanceSlotted" />, the cell that will be created and inserted into the socket.
		///     Uses <see cref="M:XRL.World.GameObjectFactory.ProcessSpecification(System.String,System.Action{XRL.World.GameObject},XRL.World.InventoryObject,System.Int32,System.Int32,System.Int32,System.String,System.String,System.Action{XRL.World.GameObject},XRL.World.GameObject,XRL.World.Parts.Inventory,System.Collections.Generic.List{XRL.World.GameObject})" /> format to create object.
		/// </summary>
		// Token: 0x040048DA RID: 18650
		public string SlottedType = "#Chem Cell,Chem Cell,@DynamicObjectsTable:EnergyCells:Tier{zonetier}";

		/// <summary>
		///     Chance in 100 to fill the cell loaded via <see cref="M:XRL.World.Parts.IEnergyCell.MaximizeCharge" />
		/// </summary>
		// Token: 0x040048DB RID: 18651
		public int ChanceFullCell = 10;

		// Token: 0x040048DC RID: 18652
		public int ChanceDestroyCellOnForcedUnequip;

		// Token: 0x040048DD RID: 18653
		public string CellStartChargePercentage;

		// Token: 0x040048DE RID: 18654
		public string RemoveCellUnpoweredSound = "compartment_open";

		// Token: 0x040048DF RID: 18655
		public string RemoveCellPoweredSound = "sfx_interact_artifact_windDown";

		// Token: 0x040048E0 RID: 18656
		public string SlotCellUnpoweredSound = "compartment_close";

		// Token: 0x040048E1 RID: 18657
		public string SlotCellPoweredSound = "sfx_interact_artifact_windup";

		// Token: 0x040048E2 RID: 18658
		public bool VisibleInDisplayName = true;

		// Token: 0x040048E3 RID: 18659
		public bool VisibleInDescription;
	}
}
