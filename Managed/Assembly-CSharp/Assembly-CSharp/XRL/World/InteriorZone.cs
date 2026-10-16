using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x0200097C RID: 2428
	[HasGameBasedStaticCache]
	[Serializable]
	public class InteriorZone : Zone
	{
		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x060066C6 RID: 26310 RVA: 0x001FF79B File Offset: 0x001FD99B
		// (set) Token: 0x060066C5 RID: 26309 RVA: 0x001FF750 File Offset: 0x001FD950
		public override string ZoneID
		{
			get
			{
				return this._ZoneID;
			}
			set
			{
				this._ZoneID = value;
				XRL.World.ZoneID.Parse(value, out this.ZoneWorld, out this.Schema, out this.Instance, out this.wX, out this.wY, out this.X, out this.Y, out this.Z);
			}
		}

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x060066C7 RID: 26311 RVA: 0x001FF7A3 File Offset: 0x001FD9A3
		public Cell ParentCell
		{
			get
			{
				GameObject parentObject = this.ParentObject;
				if (parentObject == null)
				{
					return null;
				}
				return parentObject.CurrentCell;
			}
		}

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x060066C8 RID: 26312 RVA: 0x001FF7B6 File Offset: 0x001FD9B6
		public Zone ParentZone
		{
			get
			{
				GameObject parentObject = this.ParentObject;
				if (parentObject == null)
				{
					return null;
				}
				return parentObject.CurrentZone;
			}
		}

		/// <summary>The containing object of this interior zone.</summary>
		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x060066C9 RID: 26313 RVA: 0x001FF7CC File Offset: 0x001FD9CC
		// (set) Token: 0x060066CA RID: 26314 RVA: 0x001FF89D File Offset: 0x001FDA9D
		public GameObject ParentObject
		{
			get
			{
				if (GameObject.Validate(ref this._ParentObject))
				{
					this.Location.SetCell(this._ParentObject.CurrentCell);
				}
				else
				{
					if (this.Failed)
					{
						return null;
					}
					if (!this.Instance.IsNullOrEmpty())
					{
						Cell cell = this.Location.ResolveCell();
						GameObject gameObject;
						if ((gameObject = ((cell != null) ? cell.FindObjectByID(this.Instance) : null)) == null)
						{
							gameObject = ((cell != null) ? cell.ParentZone.FindObjectByID(this.Instance) : null) ?? The.ZoneManager.FindObjectByID(this.Instance);
						}
						this._ParentObject = gameObject;
						if (this._ParentObject == null)
						{
							MetricsManager.LogError("Unable to find interior zone host by ID: " + this.Instance);
						}
						this.Failed = this._ParentObject == null;
					}
				}
				return this._ParentObject;
			}
			set
			{
				this._ParentObject = value;
				this.Instance = value.ID;
				this.Location.SetCell(this._ParentObject.CurrentCell);
			}
		}

		// Token: 0x060066CB RID: 26315 RVA: 0x001FF8C8 File Offset: 0x001FDAC8
		public InteriorZone()
		{
		}

		// Token: 0x060066CC RID: 26316 RVA: 0x001FF8DB File Offset: 0x001FDADB
		public InteriorZone(int Width, int Height)
			: base(Width, Height)
		{
		}

		// Token: 0x060066CD RID: 26317 RVA: 0x001FF8F0 File Offset: 0x001FDAF0
		public override string ResolveZoneWorld()
		{
			return this.Location.World;
		}

		// Token: 0x060066CE RID: 26318 RVA: 0x001FF900 File Offset: 0x001FDB00
		public Cell GetEscapeCell(GameObject For = null)
		{
			GameObject parentObject = this.ParentObject;
			Cell cell = ((parentObject != null) ? parentObject.CurrentCell : null);
			if (cell == null || cell.ParentZone == this)
			{
				long num = -1L;
				string text = "JoppaWorld.11.22.1.1.10";
				foreach (KeyValuePair<string, long> keyValuePair in The.ZoneManager.VisitedTime)
				{
					if (keyValuePair.Value > num && !(this.ZoneID == keyValuePair.Key) && keyValuePair.Key.IndexOf('.') != -1)
					{
						num = keyValuePair.Value;
						text = keyValuePair.Key;
					}
				}
				cell = The.ZoneManager.GetZone(text).GetPullDownLocation(For);
			}
			if (For != null && !cell.IsPassable(For, true))
			{
				cell = cell.getClosestPassableCellFor(For);
			}
			return cell;
		}

		// Token: 0x060066CF RID: 26319 RVA: 0x001FF9E4 File Offset: 0x001FDBE4
		public Zone ResolveParentZone()
		{
			GameObject parentObject = this.ParentObject;
			if (parentObject == null)
			{
				return null;
			}
			Cell currentCell = parentObject.GetCurrentCell();
			if (currentCell == null)
			{
				return null;
			}
			return currentCell.ParentZone;
		}

		// Token: 0x060066D0 RID: 26320 RVA: 0x001FFA02 File Offset: 0x001FDC02
		public Cell ResolveParentCell()
		{
			GameObject parentObject = this.ParentObject;
			if (parentObject == null)
			{
				return null;
			}
			return parentObject.GetCurrentCell();
		}

		// Token: 0x060066D1 RID: 26321 RVA: 0x001FFA15 File Offset: 0x001FDC15
		public Zone ResolveBasisZone()
		{
			Cell cell = this.ResolveBasisCell();
			if (cell == null)
			{
				return null;
			}
			return cell.ParentZone;
		}

		// Token: 0x060066D2 RID: 26322 RVA: 0x001FFA28 File Offset: 0x001FDC28
		public Cell ResolveBasisCell()
		{
			Cell cell = this.ResolveParentCell();
			InteriorZone interiorZone = ((cell != null) ? cell.ParentZone : null) as InteriorZone;
			int num = 0;
			while (num < 100 && interiorZone != null && interiorZone != this)
			{
				Cell cell2 = interiorZone.ResolveParentCell();
				if (cell2 == null)
				{
					break;
				}
				cell = cell2;
				interiorZone = cell2.ParentZone as InteriorZone;
				num++;
			}
			return cell;
		}

		// Token: 0x060066D3 RID: 26323 RVA: 0x001FFA7B File Offset: 0x001FDC7B
		public override void Activated()
		{
			InteriorZone.Active = this;
			base.Activated();
		}

		// Token: 0x060066D4 RID: 26324 RVA: 0x001FFA89 File Offset: 0x001FDC89
		public override void Deactivated()
		{
			if (InteriorZone.Active == this)
			{
				InteriorZone.Active = null;
			}
			base.Deactivated();
		}

		// Token: 0x060066D5 RID: 26325 RVA: 0x001FFA9F File Offset: 0x001FDC9F
		public override bool WantEvent(int ID, int cascade)
		{
			return ID == ZoneBuiltEvent.ID || base.WantEvent(ID, cascade);
		}

		// Token: 0x060066D6 RID: 26326 RVA: 0x001FFAB3 File Offset: 0x001FDCB3
		public override bool HandleEvent(MinEvent E)
		{
			if (E.ID == ZoneBuiltEvent.ID)
			{
				InteriorZoneBuiltEvent.Send(this.ParentObject, this);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060066D7 RID: 26327 RVA: 0x001FFAD5 File Offset: 0x001FDCD5
		public override string GetCheckpointKey()
		{
			Zone zone = this.ResolveBasisZone();
			if (zone == null)
			{
				return null;
			}
			return zone.GetCheckpointKey();
		}

		// Token: 0x060066D8 RID: 26328 RVA: 0x001FFAE8 File Offset: 0x001FDCE8
		public override bool IsCheckpoint()
		{
			Zone zone = this.ResolveBasisZone();
			return zone != null && zone.IsCheckpoint();
		}

		// Token: 0x060066D9 RID: 26329 RVA: 0x001FFAFC File Offset: 0x001FDCFC
		public override int GetTransitionIntervalTo(Zone Z)
		{
			Zone zone = this.ResolveBasisZone();
			if (zone == Z)
			{
				return 0;
			}
			InteriorZone interiorZone = Z as InteriorZone;
			if (interiorZone != null && interiorZone.ResolveBasisZone() == zone)
			{
				return 0;
			}
			return base.GetTransitionIntervalTo(Z);
		}

		// Token: 0x04002F5A RID: 12122
		[GameBasedStaticCache(true, false)]
		public static InteriorZone Active;

		// Token: 0x04002F5B RID: 12123
		public string Schema;

		// Token: 0x04002F5C RID: 12124
		public string Instance;

		// Token: 0x04002F5D RID: 12125
		public GlobalLocation Location = new GlobalLocation();

		// Token: 0x04002F5E RID: 12126
		[NonSerialized]
		private bool Failed;

		// Token: 0x04002F5F RID: 12127
		[NonSerialized]
		private GameObject _ParentObject;
	}
}
