using System;
using System.Collections.Generic;
using System.Text;
using XRL.UI;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x02000FA6 RID: 4006
	[Serializable]
	public class ForceProjector : IPoweredPart
	{
		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x06009F29 RID: 40745 RVA: 0x0039CAA7 File Offset: 0x0039ACA7
		// (set) Token: 0x06009F28 RID: 40744 RVA: 0x0039CA99 File Offset: 0x0039AC99
		public int SecurityClearance
		{
			get
			{
				return XRL.World.Capabilities.SecurityClearance.GetSecurityClearanceByKeySpecification(this.KeyObject);
			}
			set
			{
				XRL.World.Capabilities.SecurityClearance.HandleSecurityClearanceSpecification(value, ref this.KeyObject);
			}
		}

		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x06009F2A RID: 40746 RVA: 0x0039CAB4 File Offset: 0x0039ACB4
		// (set) Token: 0x06009F2B RID: 40747 RVA: 0x0039CABC File Offset: 0x0039ACBC
		public int BaseOperatingCharge
		{
			get
			{
				return this._BaseOperatingCharge;
			}
			set
			{
				if (this._BaseOperatingCharge != value)
				{
					this._BaseOperatingCharge = value;
					this.SyncCharge();
				}
			}
		}

		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x06009F2C RID: 40748 RVA: 0x0039CAD4 File Offset: 0x0039ACD4
		// (set) Token: 0x06009F2D RID: 40749 RVA: 0x0039CADC File Offset: 0x0039ACDC
		public string AllowPassageTag
		{
			get
			{
				return this._AllowPassageTag;
			}
			set
			{
				if (this._AllowPassageTag != value)
				{
					this._AllowPassageTag = value;
					foreach (GameObject gameObject in this.CurrentField)
					{
						Forcefield part = gameObject.GetPart<Forcefield>();
						if (part != null)
						{
							part.AllowPassageTag = this._AllowPassageTag;
						}
					}
				}
			}
		}

		// Token: 0x17000AD3 RID: 2771
		// (set) Token: 0x06009F2E RID: 40750 RVA: 0x0039CB54 File Offset: 0x0039AD54
		public string AllowPassageFactions
		{
			set
			{
				this.AllowFactions.Clear();
				if (!value.IsNullOrEmpty())
				{
					this.AllowFactions.AddRange(value.Split(',', StringSplitOptions.None));
				}
				using (List<GameObject>.Enumerator enumerator = this.CurrentField.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Forcefield forcefield;
						if (enumerator.Current.TryGetPart<Forcefield>(out forcefield))
						{
							if (value.IsNullOrEmpty())
							{
								forcefield.AllowFactions = null;
							}
							else
							{
								List<string> allowFactions = forcefield.AllowFactions;
								if (allowFactions != null)
								{
									allowFactions.Clear();
								}
								Forcefield forcefield2 = forcefield;
								if (forcefield2.AllowFactions == null)
								{
									forcefield2.AllowFactions = new List<string>(this.AllowFactions.Count);
								}
								forcefield.AllowFactions.AddRange(this.AllowFactions);
							}
						}
					}
				}
			}
		}

		// Token: 0x06009F2F RID: 40751 RVA: 0x0039CC24 File Offset: 0x0039AE24
		public ForceProjector()
		{
			this.ChargeUse = this._BaseOperatingCharge;
			this.IsRealityDistortionBased = true;
			this.WorksOnSelf = true;
		}

		// Token: 0x06009F30 RID: 40752 RVA: 0x0039CCD0 File Offset: 0x0039AED0
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009F31 RID: 40753 RVA: 0x0039CCD3 File Offset: 0x0039AED3
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			Writer.WriteGameObjectList(this.CurrentField);
			Writer.WriteGameObjectList(this.AllowPassage);
			Writer.Write(this.AllowFactions);
			base.Write(Basis, Writer);
		}

		// Token: 0x06009F32 RID: 40754 RVA: 0x0039CD01 File Offset: 0x0039AF01
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			Reader.ReadGameObjectList(this.CurrentField, null);
			Reader.ReadGameObjectList(this.AllowPassage, null);
			this.AllowFactions = Reader.ReadList<string>();
			base.Read(Basis, Reader);
		}

		// Token: 0x06009F33 RID: 40755 RVA: 0x0039CD34 File Offset: 0x0039AF34
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CheckExistenceSupportEvent>.ID || ID == SingletonEvent<EndTurnEvent>.ID || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID || ID == LeftCellEvent.ID || ID == OnDestroyObjectEvent.ID || ID == ZoneBuiltEvent.ID;
		}

		// Token: 0x06009F34 RID: 40756 RVA: 0x0039CD85 File Offset: 0x0039AF85
		public override bool HandleEvent(CheckExistenceSupportEvent E)
		{
			return !this.CurrentField.Contains(E.Object) && base.HandleEvent(E);
		}

		// Token: 0x06009F35 RID: 40757 RVA: 0x0039CDA4 File Offset: 0x0039AFA4
		public override bool HandleEvent(EndTurnEvent E)
		{
			if (this.DelayMaintenance)
			{
				this.DelayMaintenance = false;
			}
			else
			{
				int num = this.MaintainProjections(false);
				if (base.IsDisabled(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) && num > 0)
				{
					this.ShutDownProjections();
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009F36 RID: 40758 RVA: 0x0039CE04 File Offset: 0x0039B004
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (this.ParentObject.Understood())
			{
				if (this.ParentObject.FireEvent("ForceProjectorActive"))
				{
					E.AddAction("Activate", "activate", "ActivateForceProjector", null, 'a', false, 100, 0, false, false, false, false, true, null, false);
				}
				else
				{
					E.AddAction("Deactivate", "deactivate", "DeactivateForceProjector", null, 'a', false, 50, 0, false, false, false, false, true, null, false);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009F37 RID: 40759 RVA: 0x0039CE84 File Offset: 0x0039B084
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "ActivateForceProjector")
			{
				if (this.ForceProjectorActivate(E.Actor, E))
				{
					E.RequestInterfaceExit();
				}
			}
			else if (E.Command == "DeactivateForceProjector" && this.ForceProjectorDeactivate(E.Actor, E))
			{
				E.RequestInterfaceExit();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009F38 RID: 40760 RVA: 0x0039CEE8 File Offset: 0x0039B0E8
		public override bool HandleEvent(LeftCellEvent E)
		{
			this.ShutDownProjections();
			return base.HandleEvent(E);
		}

		// Token: 0x06009F39 RID: 40761 RVA: 0x0039CEF7 File Offset: 0x0039B0F7
		public override bool HandleEvent(OnDestroyObjectEvent E)
		{
			this.ShutDownProjections();
			return base.HandleEvent(E);
		}

		// Token: 0x06009F3A RID: 40762 RVA: 0x0039CF06 File Offset: 0x0039B106
		public override bool HandleEvent(ZoneBuiltEvent E)
		{
			if (this.FindInitialDeployment)
			{
				this.LookForInitialDeployment();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009F3B RID: 40763 RVA: 0x0039CF1D File Offset: 0x0039B11D
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009F3C RID: 40764 RVA: 0x0039CF20 File Offset: 0x0039B120
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BootSequenceInitialized");
			Registrar.Register("CanSmartUse");
			Registrar.Register("CommandSmartUse");
			Registrar.Register("EffectApplied");
			Registrar.Register("EnteredCell");
			Registrar.Register("ForceProjectorActive");
			Registrar.Register("PowerSwitchDeactivated");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009F3D RID: 40765 RVA: 0x0039CF84 File Offset: 0x0039B184
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ForceProjectorActive")
			{
				if (this.LastCurrentFieldCount == -1)
				{
					this.MaintainProjections(false);
				}
				if (this.LastCurrentFieldCount > 0)
				{
					return false;
				}
			}
			else if (E.ID == "CanSmartUse")
			{
				if (!E.GetGameObjectParameter("User").IsPlayer() || this.ParentObject.Understood())
				{
					return false;
				}
			}
			else if (E.ID == "CommandSmartUse")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("User");
				if (!gameObjectParameter.IsPlayer() || this.ParentObject.Understood())
				{
					if (this.LastCurrentFieldCount == -1)
					{
						this.MaintainProjections(false);
					}
					if (this.LastCurrentFieldCount > 0)
					{
						this.ForceProjectorDeactivate(gameObjectParameter, E);
					}
					else
					{
						this.ForceProjectorActivate(gameObjectParameter, E);
					}
				}
			}
			else if (E.ID == "EffectApplied" || E.ID == "PowerSwitchDeactivated" || E.ID == "BootSequenceInitialized")
			{
				if (base.IsDisabled(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
				{
					this.ShutDownProjections();
				}
			}
			else if (E.ID == "EnteredCell" && this.FindInitialDeployment && this.ParentObject.CurrentCell.ParentZone.Built)
			{
				this.LookForInitialDeployment();
			}
			return base.FireEvent(E);
		}

		// Token: 0x06009F3E RID: 40766 RVA: 0x0039D108 File Offset: 0x0039B308
		public bool AddAllowPassage(GameObject Actor)
		{
			if (Actor != null && !this.AllowPassage.Contains(Actor))
			{
				this.AllowPassage.Add(Actor);
				foreach (GameObject gameObject in this.CurrentField)
				{
					Forcefield part = gameObject.GetPart<Forcefield>();
					if (part != null)
					{
						part.AddAllowPassage(Actor);
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06009F3F RID: 40767 RVA: 0x0039D184 File Offset: 0x0039B384
		public bool RemoveAllowPassage(GameObject Actor)
		{
			if (Actor != null && this.AllowPassage.Contains(Actor))
			{
				this.AllowPassage.Remove(Actor);
				foreach (GameObject gameObject in this.CurrentField)
				{
					Forcefield part = gameObject.GetPart<Forcefield>();
					if (part != null)
					{
						part.RemoveAllowPassage(Actor);
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06009F40 RID: 40768 RVA: 0x0039D200 File Offset: 0x0039B400
		public bool AddAllowPassage(string Faction)
		{
			if (!Faction.IsNullOrEmpty() && !this.AllowFactions.Contains(Faction))
			{
				this.AllowFactions.Add(Faction);
				using (List<GameObject>.Enumerator enumerator = this.CurrentField.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Forcefield forcefield;
						if (enumerator.Current.TryGetPart<Forcefield>(out forcefield))
						{
							forcefield.AddAllowPassage(Faction);
						}
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06009F41 RID: 40769 RVA: 0x0039D280 File Offset: 0x0039B480
		public bool RemoveAllowPassage(string Faction)
		{
			if (!Faction.IsNullOrEmpty() && this.AllowFactions.Remove(Faction))
			{
				using (List<GameObject>.Enumerator enumerator = this.CurrentField.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Forcefield forcefield;
						if (enumerator.Current.TryGetPart<Forcefield>(out forcefield))
						{
							forcefield.RemoveAllowPassage(Faction);
						}
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06009F42 RID: 40770 RVA: 0x0039D2F4 File Offset: 0x0039B4F4
		public void SyncCharge()
		{
			if (this.LastCurrentFieldCount == -1)
			{
				this.MaintainProjections(false);
			}
			this.ChargeUse = this.BaseOperatingCharge + this.ChargePerProjection * this.LastCurrentFieldCount;
		}

		// Token: 0x06009F43 RID: 40771 RVA: 0x0039D321 File Offset: 0x0039B521
		public void ResyncCharge()
		{
			if (this.lastResyncTurn == The.Game.Turns)
			{
				return;
			}
			this.lastResyncTurn = The.Game.Turns;
			this.LastCurrentFieldCount = -1;
			this.SyncCharge();
		}

		// Token: 0x06009F44 RID: 40772 RVA: 0x0039D354 File Offset: 0x0039B554
		public int MaintainProjections(bool FromDeploy = false)
		{
			int num = 0;
			List<GameObject> list = null;
			foreach (GameObject gameObject in this.CurrentField)
			{
				if (gameObject == null || gameObject.IsInvalid())
				{
					if (list == null)
					{
						list = Event.NewGameObjectList();
					}
					list.Add(gameObject);
				}
				else
				{
					num++;
					Phase.sync(this.ParentObject, gameObject);
				}
			}
			if (list != null)
			{
				foreach (GameObject gameObject2 in list)
				{
					this.CurrentField.Remove(gameObject2);
				}
				list.Clear();
			}
			if (this.MaintenanceRequiresContiguity && num > 0)
			{
				List<GameObject> list2 = Event.NewGameObjectList();
				List<GameObject> list3 = Event.NewGameObjectList();
				List<GameObject> list4 = Event.NewGameObjectList();
				List<Cell> localAdjacentCells = this.ParentObject.CurrentCell.GetLocalAdjacentCells();
				using (List<GameObject>.Enumerator enumerator = this.CurrentField.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						GameObject gameObject3 = enumerator.Current;
						if (localAdjacentCells.Contains(gameObject3.CurrentCell))
						{
							list4.Add(gameObject3);
						}
					}
					goto IL_0214;
				}
				IL_0128:
				list3.AddRange(list4);
				list4.Clear();
				foreach (GameObject gameObject4 in list3)
				{
					foreach (Cell cell in gameObject4.CurrentCell.GetLocalAdjacentCells())
					{
						foreach (GameObject gameObject5 in cell.Objects)
						{
							if (this.CurrentField.Contains(gameObject5) && !list2.Contains(gameObject5) && !list3.Contains(gameObject5) && !list4.Contains(gameObject5))
							{
								list4.Add(gameObject5);
							}
						}
					}
				}
				list2.AddRange(list3);
				list3.Clear();
				IL_0214:
				if (list4.Count > 0 && list2.Count + list3.Count + list4.Count < num)
				{
					goto IL_0128;
				}
				if (list4.Count > 0)
				{
					list2.AddRange(list4);
					list4.Clear();
				}
				if (list2.Count < num)
				{
					List<GameObject> list5 = Event.NewGameObjectList();
					foreach (GameObject gameObject6 in this.CurrentField)
					{
						if (!list2.Contains(gameObject6))
						{
							list5.Add(gameObject6);
							num--;
						}
					}
					foreach (GameObject gameObject7 in list5)
					{
						this.CurrentField.Remove(gameObject7);
						gameObject7.Obliterate(null, false, null);
					}
					list5.Clear();
				}
			}
			if (this.StepDownCharge)
			{
				ActivePartStatus activePartStatus = base.GetActivePartStatus(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null);
				if (this.CurrentField.Count > 0 && activePartStatus == ActivePartStatus.Unpowered)
				{
					int num2 = (this.ParentObject.QueryCharge(false, 0L, true, true) - this.BaseOperatingCharge) / this.ChargePerProjection;
					if (num2 > 0)
					{
						for (int i = this.CurrentField.Count - 1; i >= num2; i--)
						{
							Cell currentCell = this.CurrentField[i].CurrentCell;
							if (currentCell != null)
							{
								string text = currentCell.X.ToString() + "," + currentCell.Y.ToString();
								if (!this.SuspendedDeployment.IsNullOrEmpty())
								{
									this.SuspendedDeployment = text + ";" + this.SuspendedDeployment;
								}
								else
								{
									this.SuspendedDeployment = text;
								}
							}
							this.CurrentField[i].Obliterate(null, false, null);
							this.CurrentField.RemoveAt(i);
						}
						num = num2;
					}
					else
					{
						StringBuilder stringBuilder = Event.NewStringBuilder(null);
						foreach (GameObject gameObject8 in this.CurrentField)
						{
							Cell currentCell2 = gameObject8.CurrentCell;
							if (currentCell2 != null)
							{
								stringBuilder.Compound(currentCell2.X, ";").Append(',').Append(currentCell2.Y);
							}
						}
						if (!this.SuspendedDeployment.IsNullOrEmpty())
						{
							stringBuilder.Compound(this.SuspendedDeployment, ";");
						}
						this.SuspendedDeployment = ((stringBuilder.Length > 0) ? stringBuilder.ToString() : null);
						this.ShutDownProjections();
						num = 0;
					}
				}
				else if (!FromDeploy && activePartStatus == ActivePartStatus.Operational && !this.SuspendedDeployment.IsNullOrEmpty())
				{
					int num3 = (this.ParentObject.QueryCharge(false, 0L, true, true) - this.BaseOperatingCharge) / this.ChargePerProjection - this.CurrentField.Count;
					if (num3 > 0)
					{
						Zone currentZone = this.ParentObject.CurrentZone;
						if (currentZone != null)
						{
							List<Cell> list6 = Event.NewCellList();
							foreach (GameObject gameObject9 in this.CurrentField)
							{
								Cell currentCell3 = gameObject9.CurrentCell;
								if (currentCell3 != null)
								{
									list6.Add(currentCell3);
								}
							}
							List<string> list7 = new List<string>(this.SuspendedDeployment.Split(';', StringSplitOptions.None));
							int j = 0;
							int num4 = Math.Min(num3, list7.Count);
							while (j < num4)
							{
								string text2 = list7[0];
								list7.RemoveAt(0);
								string[] array = text2.Split(',', StringSplitOptions.None);
								int num5 = Convert.ToInt32(array[0]);
								int num6 = Convert.ToInt32(array[1]);
								Cell cell2 = currentZone.GetCell(num5, num6);
								if (cell2 != null)
								{
									list6.Add(cell2);
								}
								j++;
							}
							this.ForceProjectorDeploy(list6, null);
							this.SuspendedDeployment = ((list7.Count > 0) ? string.Join(";", list7.ToArray()) : null);
						}
					}
				}
			}
			this.LastCurrentFieldCount = num;
			this.SyncCharge();
			return num;
		}

		// Token: 0x06009F45 RID: 40773 RVA: 0x0039DA20 File Offset: 0x0039BC20
		public void ShutDownProjections()
		{
			int lastCurrentFieldCount = this.LastCurrentFieldCount;
			foreach (GameObject gameObject in this.CurrentField)
			{
				if (gameObject != null && gameObject.IsValid())
				{
					gameObject.Obliterate(null, false, null);
				}
			}
			this.CurrentField.Clear();
			this.LastCurrentFieldCount = 0;
			if (lastCurrentFieldCount != this.LastCurrentFieldCount)
			{
				this.ResyncCharge();
			}
		}

		// Token: 0x06009F46 RID: 40774 RVA: 0x0039DAAC File Offset: 0x0039BCAC
		private bool ForceFieldAdvisable()
		{
			GameObject activePartFirstSubject = this.GetActivePartFirstSubject();
			return activePartFirstSubject != null && IComponent<GameObject>.CheckRealityDistortionAdvisability(activePartFirstSubject, null, activePartFirstSubject, this.ParentObject, null, null, 0);
		}

		// Token: 0x06009F47 RID: 40775 RVA: 0x0039DAE0 File Offset: 0x0039BCE0
		private bool AccessCheck(GameObject Actor, bool Silent = false)
		{
			if (this.KeyObject.IsNullOrEmpty())
			{
				return true;
			}
			List<string> list = this.KeyObject.CachedCommaExpansion();
			if (Actor.FindContainedObjectByAnyBlueprint(list) != null)
			{
				return true;
			}
			if (list.Contains("*Psychometry") && base.UsePsychometry(Actor, this.ParentObject))
			{
				if (!Silent && !this.PsychometryAccessMessage.IsNullOrEmpty())
				{
					IComponent<GameObject>.EmitMessage(Actor, GameText.VariableReplace(this.PsychometryAccessMessage, this.ParentObject, null, true), ' ', true, false, false, null, null);
				}
				return true;
			}
			if (!Silent && !this.AccessFailureMessage.IsNullOrEmpty())
			{
				IComponent<GameObject>.EmitMessage(Actor, GameText.VariableReplace(this.AccessFailureMessage, this.ParentObject, null, true), ' ', true, false, false, null, null);
			}
			return false;
		}

		// Token: 0x06009F48 RID: 40776 RVA: 0x0039DB94 File Offset: 0x0039BD94
		public void ForceProjectorDeploy(List<Cell> TargetCells, GameObject Actor = null)
		{
			this.ShutDownProjections();
			foreach (Cell cell in TargetCells)
			{
				if (!this.IsRealityDistortionBased || IComponent<GameObject>.CheckRealityDistortionAccessibility(null, cell, Actor, this.ParentObject, null, 0))
				{
					GameObject gameObject = GameObject.Create(this.Blueprint);
					Forcefield part = gameObject.GetPart<Forcefield>();
					this.ProjectionSetup(gameObject, part);
					cell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					this.CurrentField.Add(gameObject);
				}
			}
			this.MaintainProjections(true);
		}

		// Token: 0x06009F49 RID: 40777 RVA: 0x0039DC44 File Offset: 0x0039BE44
		public bool ForceProjectorActivate(GameObject Actor, IEvent E = null)
		{
			Cell CC = this.ParentObject.CurrentCell;
			if (!base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) || CC == null)
			{
				Actor.ShowFailure(this.ParentObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " unresponsive.");
				return false;
			}
			if (!this.AccessCheck(Actor, false))
			{
				return false;
			}
			this.MaintainProjections(false);
			if (this.AddUserToAllowPassage)
			{
				this.AddAllowPassage(Actor);
			}
			Dictionary<GameObject, Cell> dictionary = ((this.CurrentField.Count > 0) ? new Dictionary<GameObject, Cell>(this.CurrentField.Count) : null);
			try
			{
				if (dictionary != null)
				{
					foreach (GameObject gameObject in this.CurrentField)
					{
						dictionary.Add(gameObject, gameObject.CurrentCell);
						gameObject.RemoveFromContext(null);
					}
				}
				if (!this.IsRealityDistortionBased || this.ParentObject.FireEvent(Event.New("InitiateRealityDistortionLocal", "Object", this.ParentObject, "Device", this.ParentObject, "Operator", Actor), E))
				{
					List<Cell> TargetCells = null;
					if (Actor.IsPlayer())
					{
						TargetCells = PickTarget.ShowFieldPicker(this.MaxProjections, 1, this.ParentObject.CurrentCell.X, this.ParentObject.CurrentCell.Y, this.PickerLabel, true, true, false, false, false);
					}
					else
					{
						Cell cell = CC.GetRandomLocalCardinalAdjacentCell() ?? CC;
						Cell PrevCell = null;
						TargetCells = Event.NewCellList();
						TargetCells.Add(cell);
						int i = 1;
						IL_0254:
						Predicate<Cell> <>9__0;
						while (i < this.MaxProjections)
						{
							int num = 0;
							while (++num < 20)
							{
								Cell cell2 = cell;
								Predicate<Cell> predicate;
								if ((predicate = <>9__0) == null)
								{
									predicate = (<>9__0 = (Cell c) => c != PrevCell && c != CC && !TargetCells.Contains(c));
								}
								Cell randomLocalCardinalAdjacentCell = cell2.GetRandomLocalCardinalAdjacentCell(predicate);
								if (randomLocalCardinalAdjacentCell == null)
								{
									break;
								}
								if (num <= 10 && randomLocalCardinalAdjacentCell.IsSolid())
								{
									TargetCells.Add(randomLocalCardinalAdjacentCell);
									PrevCell = cell;
									cell = randomLocalCardinalAdjacentCell;
									i++;
									goto IL_0254;
								}
							}
							break;
						}
					}
					if (TargetCells != null)
					{
						Actor.UseEnergy(1000, "Equipment", null, null, false);
						dictionary = null;
						this.ForceProjectorDeploy(TargetCells, Actor);
					}
				}
			}
			finally
			{
				if (dictionary != null)
				{
					foreach (KeyValuePair<GameObject, Cell> keyValuePair in dictionary)
					{
						GameObject key = keyValuePair.Key;
						if (key != null && key.IsValid() && this.CurrentField.Contains(key))
						{
							keyValuePair.Value.AddObject(key, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06009F4A RID: 40778 RVA: 0x0039DFA4 File Offset: 0x0039C1A4
		public bool ForceProjectorDeactivate(GameObject Actor, IEvent E = null)
		{
			Cell currentCell = this.ParentObject.CurrentCell;
			if (!base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) || currentCell == null)
			{
				Actor.ShowFailure(this.ParentObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " unresponsive.");
				return false;
			}
			if (!this.AccessCheck(Actor, false))
			{
				return false;
			}
			this.ShutDownProjections();
			return true;
		}

		// Token: 0x06009F4B RID: 40779 RVA: 0x0039E040 File Offset: 0x0039C240
		private void ProjectionSetup(GameObject obj, Forcefield pFF)
		{
			if (pFF != null)
			{
				pFF.Creator = this.ParentObject;
				pFF.AllowPassageTag = this.AllowPassageTag;
				foreach (GameObject gameObject in this.AllowPassage)
				{
					pFF.AddAllowPassage(gameObject);
				}
				foreach (string text in this.AllowFactions)
				{
					pFF.AddAllowPassage(text);
				}
			}
			Phase.carryOver(this.ParentObject, obj);
		}

		// Token: 0x06009F4C RID: 40780 RVA: 0x0039E100 File Offset: 0x0039C300
		public void LookForInitialDeployment()
		{
			this.FindInitialDeployment = false;
			this.ShutDownProjections();
			List<GameObject> list = Event.NewGameObjectList();
			List<GameObject> list2 = Event.NewGameObjectList();
			using (List<Cell>.Enumerator enumerator = this.ParentObject.CurrentCell.GetLocalAdjacentCells().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Cell cell = enumerator.Current;
					GameObject firstObject = cell.GetFirstObject(this.Blueprint);
					if (firstObject != null)
					{
						Forcefield part = firstObject.GetPart<Forcefield>();
						if (part == null || part.Creator == null || part.Creator.IsInvalid() || part.Creator == this.ParentObject)
						{
							this.ProjectionSetup(firstObject, part);
							list2.Add(firstObject);
						}
					}
				}
				goto IL_01CF;
			}
			IL_00A5:
			list.AddRange(list2);
			list2.Clear();
			foreach (GameObject gameObject in list)
			{
				foreach (Cell cell2 in gameObject.CurrentCell.GetLocalAdjacentCells())
				{
					GameObject firstObject2 = cell2.GetFirstObject(this.Blueprint);
					if (firstObject2 != null && !this.CurrentField.Contains(firstObject2) && !list.Contains(firstObject2) && !list2.Contains(firstObject2))
					{
						Forcefield part2 = firstObject2.GetPart<Forcefield>();
						if (part2 == null || part2.Creator == null || part2.Creator.IsInvalid() || part2.Creator == this.ParentObject)
						{
							this.ProjectionSetup(gameObject, part2);
							list2.Add(firstObject2);
							if (this.CurrentField.Count + list.Count + list2.Count >= this.MaxProjections)
							{
								goto IL_01DB;
							}
						}
					}
				}
			}
			this.CurrentField.AddRange(list);
			list.Clear();
			IL_01CF:
			if (list2.Count > 0)
			{
				goto IL_00A5;
			}
			IL_01DB:
			this.CurrentField.AddRange(list);
			list.Clear();
			if (list2.Count > 0)
			{
				this.CurrentField.AddRange(list2);
				list2.Clear();
			}
			if (this.ParentObject.QueryCharge(false, 0L, true, true) <= 0)
			{
				this.DelayMaintenance = true;
			}
		}

		// Token: 0x04004207 RID: 16903
		public string Blueprint = "ForceBarrier";

		// Token: 0x04004208 RID: 16904
		public string KeyObject;

		// Token: 0x04004209 RID: 16905
		public string PickerLabel;

		// Token: 0x0400420A RID: 16906
		public string PsychometryAccessMessage = "You touch =subject.t= and recall =pronouns.possessive= passcode. =pronouns.Subjective= =verb:beep:afterpronoun= warmly.";

		// Token: 0x0400420B RID: 16907
		public string AccessFailureMessage = "A loud buzz is emitted. The unauthorized glyph flashes on the display.";

		// Token: 0x0400420C RID: 16908
		public int MaxProjections = 9;

		// Token: 0x0400420D RID: 16909
		public int ChargePerProjection = 90;

		// Token: 0x0400420E RID: 16910
		public bool MaintenanceRequiresContiguity = true;

		// Token: 0x0400420F RID: 16911
		public bool StepDownCharge = true;

		// Token: 0x04004210 RID: 16912
		public bool AddUserToAllowPassage = true;

		// Token: 0x04004211 RID: 16913
		public bool FindInitialDeployment;

		// Token: 0x04004212 RID: 16914
		public string SuspendedDeployment;

		// Token: 0x04004213 RID: 16915
		[NonSerialized]
		public List<GameObject> CurrentField = new List<GameObject>(16);

		// Token: 0x04004214 RID: 16916
		[NonSerialized]
		public List<GameObject> AllowPassage = new List<GameObject>();

		// Token: 0x04004215 RID: 16917
		[NonSerialized]
		public List<string> AllowFactions = new List<string>();

		// Token: 0x04004216 RID: 16918
		[NonSerialized]
		private int LastCurrentFieldCount = -1;

		// Token: 0x04004217 RID: 16919
		[NonSerialized]
		private bool DelayMaintenance;

		// Token: 0x04004218 RID: 16920
		public int _BaseOperatingCharge = 1;

		// Token: 0x04004219 RID: 16921
		public string _AllowPassageTag;

		/// <summary>
		///     This is a workaround for a cycle discovered 8-7-2021 -BB
		///     at XRL.World.Parts.ForceProjector.SyncCharge () [0x00009] in G:\Workspace\caves-of-qud\Assets\XRL Application\World\Game Objects\Parts\Furniture\ForceProjector.cs:334
		///     at XRL.World.Parts.ForceProjector.ResyncCharge () [0x00007] in G:\Workspace\caves-of-qud\Assets\XRL Application\World\Game Objects\Parts\Furniture\ForceProjector.cs:342
		///     at XRL.World.Parts.ForceProjector.ShutDownProjections () [0x0006e] in G:\Workspace\caves-of-qud\Assets\XRL Application\World\Game Objects\Parts\Furniture\ForceProjector.cs:542
		///     at XRL.World.Parts.ForceProjector.ForceProjectorDeploy (System.Collections.Generic.List`1[T] TargetCells) [0x00000] in G:\Workspace\caves-of-qud\Assets\XRL Application\World\Game Objects\Parts\Furniture\ForceProjector.cs:584
		///     at XRL.World.Parts.ForceProjector.MaintainProjections (System.Boolean FromDeploy) [0x0061b] in G:\Workspace\caves-of-qud\Assets\XRL Application\World\Game Objects\Parts\Furniture\ForceProjector.cs:517
		///     at XRL.World.Parts.ForceProjector.SyncCharge () [0x00009] in G:\Workspace\caves-of-qud\Assets\XRL Application\World\Game Objects\Parts\Furniture\ForceProjector.cs:334
		/// </summary>
		// Token: 0x0400421A RID: 16922
		private long lastResyncTurn = -1L;
	}
}
