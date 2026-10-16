using System;
using System.Collections.Generic;
using XRL.Collections;

namespace XRL.World.Parts
{
	// Token: 0x02000F9F RID: 3999
	[Serializable]
	public class DeploymentMaintainer : IPoweredPart
	{
		// Token: 0x06009EB4 RID: 40628 RVA: 0x00397B80 File Offset: 0x00395D80
		public DeploymentMaintainer()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x06009EB5 RID: 40629 RVA: 0x00397BE4 File Offset: 0x00395DE4
		public override bool SameAs(IPart p)
		{
			DeploymentMaintainer deploymentMaintainer = p as DeploymentMaintainer;
			return deploymentMaintainer.Radius == this.Radius && deploymentMaintainer.Chance == this.Chance && deploymentMaintainer.AtLeast == this.AtLeast && deploymentMaintainer.MaintenanceInterval == this.MaintenanceInterval && !(deploymentMaintainer.Duration != this.Duration) && !(deploymentMaintainer.Blueprint != this.Blueprint) && !(deploymentMaintainer.UsabilityEvent != this.UsabilityEvent) && !(deploymentMaintainer.AccessibilityEvent != this.AccessibilityEvent) && deploymentMaintainer.ActiveMaintenance == this.ActiveMaintenance && deploymentMaintainer.RealRadius == this.RealRadius && deploymentMaintainer.BlockedBySolid == this.BlockedBySolid && deploymentMaintainer.BlockedByNonEmpty == this.BlockedByNonEmpty && deploymentMaintainer.Seeping == this.Seeping && deploymentMaintainer.DustPuffEach == this.DustPuffEach && deploymentMaintainer.NoXPValue == this.NoXPValue && deploymentMaintainer.LinkRealityStabilization == this.LinkRealityStabilization && deploymentMaintainer.RealityStabilizationPenetration == this.RealityStabilizationPenetration && base.SameAs(p);
		}

		// Token: 0x06009EB6 RID: 40630 RVA: 0x00397D24 File Offset: 0x00395F24
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CheckExistenceSupportEvent>.ID || ID == AddedToInventoryEvent.ID || ID == SingletonEvent<EndTurnEvent>.ID || ID == EnteredCellEvent.ID || ID == OnDestroyObjectEvent.ID || (ID == PowerSwitchFlippedEvent.ID && this.IsPowerSwitchSensitive);
		}

		// Token: 0x06009EB7 RID: 40631 RVA: 0x00397D78 File Offset: 0x00395F78
		public override bool HandleEvent(CheckExistenceSupportEvent E)
		{
			return (!this.Deployed.ContainsValue(E.Object) || !base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null)) && base.HandleEvent(E);
		}

		// Token: 0x06009EB8 RID: 40632 RVA: 0x00397DC9 File Offset: 0x00395FC9
		public override bool HandleEvent(EndTurnEvent E)
		{
			this.MaintainDeployment();
			return base.HandleEvent(E);
		}

		// Token: 0x06009EB9 RID: 40633 RVA: 0x00397DD9 File Offset: 0x00395FD9
		public override bool HandleEvent(EnteredCellEvent E)
		{
			this.MaintainDeployment();
			return base.HandleEvent(E);
		}

		// Token: 0x06009EBA RID: 40634 RVA: 0x00397DE9 File Offset: 0x00395FE9
		public override bool HandleEvent(AddedToInventoryEvent E)
		{
			this.MaintainDeployment();
			return base.HandleEvent(E);
		}

		// Token: 0x06009EBB RID: 40635 RVA: 0x00397DF9 File Offset: 0x00395FF9
		public override bool HandleEvent(OnDestroyObjectEvent E)
		{
			if (this.ActiveMaintenance)
			{
				this.TeardownDeployment();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009EBC RID: 40636 RVA: 0x00397E10 File Offset: 0x00396010
		public override bool HandleEvent(PowerSwitchFlippedEvent E)
		{
			if (this.IsPowerSwitchSensitive)
			{
				this.MaintenanceCountdown = 0;
				this.MaintainDeployment();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009EBD RID: 40637 RVA: 0x00397E2F File Offset: 0x0039602F
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009EBE RID: 40638 RVA: 0x00397E34 File Offset: 0x00396034
		private bool CanDeploy(Cell C, Cell CC, Event Check, Dictionary<int, bool> Track = null)
		{
			if (C == null || CC == null)
			{
				return false;
			}
			if (this.RealRadius && C != CC && C.RealDistanceTo(CC, true) > (double)this.Radius)
			{
				return false;
			}
			int localCoordKey = C.LocalCoordKey;
			if (Track != null)
			{
				Track[localCoordKey] = true;
			}
			if (this.Deployed.ContainsKey(localCoordKey))
			{
				GameObject gameObject = this.Deployed[localCoordKey];
				if (GameObject.Validate(ref gameObject))
				{
					return false;
				}
			}
			using (Container<GameObject>.Enumerator enumerator = C.Objects.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Blueprint == this.Blueprint)
					{
						return false;
					}
				}
			}
			return this.Chance.in100() && (!this.BlockedBySolid || !C.IsSolid(this.Seeping)) && (!this.BlockedByNonEmpty || C.IsEmpty()) && (Check == null || C.FireEvent(Check)) && (!this.IsRealityDistortionBased || IComponent<GameObject>.CheckRealityDistortionAccessibility(this.ParentObject, C, this.ParentObject.Equipped ?? this.ParentObject.Implantee, this.ParentObject, null, this.RealityStabilizationPenetration));
		}

		// Token: 0x06009EBF RID: 40639 RVA: 0x00397F84 File Offset: 0x00396184
		private void Deploy(Cell C)
		{
			GameObject gameObject = GameObject.Create(this.Blueprint);
			if (this.LinkRealityStabilization)
			{
				RealityStabilization part = gameObject.GetPart<RealityStabilization>();
				if (part != null)
				{
					part.Strength = 0;
					part.FromSource = this.ParentObject;
				}
			}
			if (!this.Duration.IsNullOrEmpty())
			{
				gameObject.RequirePart<Temporary>(false).Duration = this.Duration.RollCached();
			}
			if (this.ActiveMaintenance)
			{
				gameObject.RequirePart<ExistenceSupport>(false).SupportedBy = this.ParentObject;
			}
			C.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			gameObject.MakeActive(false);
			if (this.NoXPValue && gameObject.HasStat("XPValue"))
			{
				gameObject.Statistics["XPValue"].BaseValue = 0;
			}
			if (this.DustPuffEach)
			{
				gameObject.DustPuff("&y", 15);
			}
			this.Deployed[C.LocalCoordKey] = gameObject;
		}

		// Token: 0x06009EC0 RID: 40640 RVA: 0x00398070 File Offset: 0x00396270
		private void CleanDeployment()
		{
			int num = 0;
			foreach (KeyValuePair<int, GameObject> keyValuePair in this.Deployed)
			{
				GameObject value = keyValuePair.Value;
				if (!GameObject.Validate(ref value))
				{
					num++;
				}
			}
			if (num > 0)
			{
				if (num >= this.Deployed.Count)
				{
					this.TeardownDeployment();
					return;
				}
				List<int> list = new List<int>(num);
				foreach (KeyValuePair<int, GameObject> keyValuePair2 in this.Deployed)
				{
					int key = keyValuePair2.Key;
					GameObject value2 = keyValuePair2.Value;
					if (!GameObject.Validate(ref value2))
					{
						list.Add(key);
					}
				}
				foreach (int num2 in list)
				{
					this.Deployed.Remove(num2);
				}
			}
		}

		// Token: 0x06009EC1 RID: 40641 RVA: 0x003981A0 File Offset: 0x003963A0
		private void ValidateDeployment(Dictionary<int, bool> Track)
		{
			int num = 0;
			foreach (KeyValuePair<int, GameObject> keyValuePair in this.Deployed)
			{
				if (!Track.ContainsKey(keyValuePair.Key))
				{
					num++;
				}
			}
			if (num > 0)
			{
				if (num >= this.Deployed.Count)
				{
					this.TeardownDeployment();
					return;
				}
				List<KeyValuePair<int, GameObject>> list = new List<KeyValuePair<int, GameObject>>(num);
				foreach (KeyValuePair<int, GameObject> keyValuePair2 in this.Deployed)
				{
					list.Add(keyValuePair2);
				}
				foreach (KeyValuePair<int, GameObject> keyValuePair3 in list)
				{
					int key = keyValuePair3.Key;
					GameObject value = keyValuePair3.Value;
					if (GameObject.Validate(ref value))
					{
						value.Obliterate(null, false, null);
					}
					this.Deployed.Remove(key);
				}
			}
		}

		// Token: 0x06009EC2 RID: 40642 RVA: 0x003982D4 File Offset: 0x003964D4
		private void TeardownDeployment()
		{
			foreach (KeyValuePair<int, GameObject> keyValuePair in this.Deployed)
			{
				GameObject value = keyValuePair.Value;
				if (GameObject.Validate(ref value))
				{
					value.Obliterate(null, false, null);
				}
			}
			if (this.Deployed.Count > 0)
			{
				this.Deployed.Clear();
			}
		}

		// Token: 0x06009EC3 RID: 40643 RVA: 0x00398354 File Offset: 0x00396554
		public override bool GetActivePartLocallyDefinedFailure()
		{
			GameObject gameObject = this.GetActivePartFirstSubject() ?? this.ParentObject;
			Cell cell = ((gameObject != null) ? gameObject.CurrentCell : null);
			return cell == null || (this.UsabilityEvent != null && !cell.FireEvent(this.UsabilityEvent));
		}

		// Token: 0x06009EC4 RID: 40644 RVA: 0x0039839C File Offset: 0x0039659C
		public bool MaintainDeployment()
		{
			Cell currentCell = (this.GetActivePartFirstSubject() ?? this.ParentObject).CurrentCell;
			if (base.IsDisabled(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				if (this.ActiveMaintenance)
				{
					this.TeardownDeployment();
				}
				this.LastCell = currentCell;
				return false;
			}
			if (this.MaintenanceCountdown > 0)
			{
				this.MaintenanceCountdown--;
				return false;
			}
			if (this.MaintenanceInterval > 0)
			{
				this.MaintenanceCountdown = this.MaintenanceInterval;
			}
			this.CleanDeployment();
			Event @event = ((this.AccessibilityEvent != null) ? Event.New(this.AccessibilityEvent, 0, 0, 0) : null);
			if (this.AdjacentCells == null || this.LastCell != currentCell)
			{
				this.AdjacentCells = ((this.Radius == -1) ? currentCell.ParentZone.GetCells() : currentCell.GetLocalAdjacentCells(this.Radius, false));
				this.AdjacentCellsShuffled = false;
			}
			Dictionary<int, bool> dictionary = ((this.ActiveMaintenance && this.LastCell != currentCell) ? new Dictionary<int, bool>(this.AdjacentCells.Count) : null);
			int num = 0;
			do
			{
				if (num == 1 && !this.AdjacentCellsShuffled)
				{
					if (this.Radius == -1)
					{
						this.AdjacentCells = new List<Cell>(this.AdjacentCells);
					}
					this.AdjacentCells.ShuffleInPlace<Cell>(null);
					this.AdjacentCellsShuffled = true;
				}
				if (this.CanDeploy(currentCell, currentCell, @event, dictionary))
				{
					this.Deploy(currentCell);
				}
				foreach (Cell cell in this.AdjacentCells)
				{
					if (num > 0 && this.Deployed.Count >= this.AtLeast)
					{
						break;
					}
					if (this.CanDeploy(cell, currentCell, @event, dictionary))
					{
						this.Deploy(cell);
					}
				}
			}
			while (this.AtLeast > 0 && this.Deployed.Count < this.AtLeast && ++num < 10);
			if (this.ActiveMaintenance && dictionary != null)
			{
				this.ValidateDeployment(dictionary);
			}
			this.LastCell = currentCell;
			return true;
		}

		// Token: 0x0400419E RID: 16798
		public int Radius = 1;

		// Token: 0x0400419F RID: 16799
		public int Chance = 100;

		// Token: 0x040041A0 RID: 16800
		public int AtLeast;

		// Token: 0x040041A1 RID: 16801
		public int MaintenanceInterval = 10;

		// Token: 0x040041A2 RID: 16802
		public string Duration;

		// Token: 0x040041A3 RID: 16803
		public string Blueprint = "Forcefield";

		// Token: 0x040041A4 RID: 16804
		public string UsabilityEvent;

		// Token: 0x040041A5 RID: 16805
		public string AccessibilityEvent;

		// Token: 0x040041A6 RID: 16806
		public bool ActiveMaintenance = true;

		// Token: 0x040041A7 RID: 16807
		public bool RealRadius;

		// Token: 0x040041A8 RID: 16808
		public bool BlockedBySolid = true;

		// Token: 0x040041A9 RID: 16809
		public bool BlockedByNonEmpty = true;

		// Token: 0x040041AA RID: 16810
		public bool Seeping;

		// Token: 0x040041AB RID: 16811
		public bool DustPuffEach;

		// Token: 0x040041AC RID: 16812
		public bool NoXPValue = true;

		// Token: 0x040041AD RID: 16813
		public bool LinkRealityStabilization;

		// Token: 0x040041AE RID: 16814
		public int RealityStabilizationPenetration;

		// Token: 0x040041AF RID: 16815
		[NonSerialized]
		private Dictionary<int, GameObject> Deployed = new Dictionary<int, GameObject>();

		// Token: 0x040041B0 RID: 16816
		[NonSerialized]
		private Cell LastCell;

		// Token: 0x040041B1 RID: 16817
		[NonSerialized]
		private int MaintenanceCountdown;

		// Token: 0x040041B2 RID: 16818
		[NonSerialized]
		private List<Cell> AdjacentCells;

		// Token: 0x040041B3 RID: 16819
		[NonSerialized]
		private bool AdjacentCellsShuffled;
	}
}
