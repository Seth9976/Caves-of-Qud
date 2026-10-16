using System;
using Genkit;
using XRL.World.AI.Pathfinding;

namespace XRL.World.Parts
{
	/// <remarks>Ideally this would be possible with a separate action queue/non-zone world map, maybe for dlc.</remarks>
	// Token: 0x02000E37 RID: 3639
	public class AIWorldMapTravel : AIBehaviorPart
	{
		// Token: 0x060093B4 RID: 37812 RVA: 0x00352E3C File Offset: 0x0035103C
		public bool SetZoneID(string Value)
		{
			string text;
			int num;
			return ZoneID.Parse(Value, out text, out this.ParasangX, out this.ParasangY, out this.ZoneX, out this.ZoneY, out num);
		}

		// Token: 0x060093B5 RID: 37813 RVA: 0x00352E6B File Offset: 0x0035106B
		public bool SetTerrain(string Blueprint)
		{
			return this.SetTerrain(this.ParentObject.CurrentZone, Blueprint);
		}

		// Token: 0x060093B6 RID: 37814 RVA: 0x00352E80 File Offset: 0x00351080
		public bool SetTerrain(Zone Zone, string Blueprint)
		{
			GameObject gameObject = ((Zone != null) ? Zone.FindObject(Blueprint) : null);
			if (gameObject != null)
			{
				this.ParasangX = gameObject.CurrentCell.X;
				this.ParasangY = gameObject.CurrentCell.Y;
				this.ZoneX = -1;
				this.ZoneY = -1;
				return true;
			}
			return false;
		}

		// Token: 0x060093B7 RID: 37815 RVA: 0x00352ED1 File Offset: 0x003510D1
		public void ResetPathing()
		{
			if (this.Pathfinder != null)
			{
				this.Pathfinder.Found = false;
			}
		}

		// Token: 0x060093B8 RID: 37816 RVA: 0x00352EE7 File Offset: 0x003510E7
		public override void Initialize()
		{
			this.CheckZone(this.ParentObject.CurrentZone);
		}

		// Token: 0x060093B9 RID: 37817 RVA: 0x00352EFA File Offset: 0x003510FA
		public override bool WantEvent(int ID, int Cascade)
		{
			return ID == EnteredCellEvent.ID || (ID == GetZoneSuspendabilityEvent.ID && this.Pinned);
		}

		// Token: 0x060093BA RID: 37818 RVA: 0x00352F18 File Offset: 0x00351118
		public void TakeAction()
		{
			Cell currentCell = this.ParentObject.CurrentCell;
			Zone parentZone = currentCell.ParentZone;
			if (!parentZone.IsWorldMap() || this.ParentObject.IsPlayer())
			{
				this.ParentObject.RemovePart(this);
				return;
			}
			if (currentCell.X == this.ParasangX && currentCell.Y == this.ParasangY)
			{
				int num = this.ZoneX;
				int num2 = this.ZoneY;
				int num3 = 10;
				if (num == -1 || num2 == -1)
				{
					Point3D landingLocation = The.ZoneManager.GetLandingLocation(parentZone.ZoneWorld, this.ParasangX, this.ParasangY);
					num = landingLocation.x;
					num2 = landingLocation.y;
					num3 = landingLocation.z;
				}
				Cell pullDownLocation = The.ZoneManager.GetZone(parentZone.ZoneWorld, this.ParasangX, this.ParasangY, num, num2, num3).GetPullDownLocation(this.ParentObject);
				this.ParentObject.SystemMoveTo(pullDownLocation, null, false, true, false, false);
				return;
			}
			if (this.Pathfinder == null)
			{
				this.Pathfinder = new FindPath();
			}
			if (!this.Pathfinder.Found)
			{
				this.Pathfinder.PerformPathfind(parentZone, currentCell.X, currentCell.Y, parentZone, this.ParasangX, this.ParasangY, false, this.ParentObject, false, false, this.Cardinal, null, 100, false, false, false, true, false, false, true);
				if (!this.Pathfinder.Found)
				{
					this.ParentObject.RemovePart(this);
					return;
				}
			}
			int num4 = this.Pathfinder.Steps.IndexOf(currentCell);
			if (num4 != -1 && num4 + 1 < this.Pathfinder.Steps.Count)
			{
				Cell cell = this.Pathfinder.Steps[num4 + 1];
				string directionFromCell = currentCell.GetDirectionFromCell(cell, false);
				if (this.ParentObject.Move(directionFromCell, true, true, true, false, true, true, null, null, false, new int?(1000), null, null, true, false, null, null, 0))
				{
					this.TravelSegments = -1;
					return;
				}
			}
			this.Pathfinder.Found = false;
		}

		// Token: 0x060093BB RID: 37819 RVA: 0x00353120 File Offset: 0x00351320
		public int GetTravelSegments()
		{
			if (this.TravelSegments == -1)
			{
				Cell currentCell = this.ParentObject.CurrentCell;
				GameObject gameObject = ((currentCell != null) ? currentCell.GetFirstObjectWithPart(typeof(TerrainTravel)) : null);
				TerrainTravel terrainTravel;
				if (gameObject != null && gameObject.TryGetPart<TerrainTravel>(out terrainTravel))
				{
					this.TravelSegments = terrainTravel.GetTravelSegments(this.ParentObject);
				}
			}
			return 1000;
		}

		// Token: 0x060093BC RID: 37820 RVA: 0x0035317C File Offset: 0x0035137C
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x060093BD RID: 37821 RVA: 0x00353180 File Offset: 0x00351380
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.Segments += 10 * Amount;
			int travelSegments = this.GetTravelSegments();
			if (this.Segments >= travelSegments)
			{
				this.TakeAction();
				this.Segments -= travelSegments;
			}
		}

		// Token: 0x060093BE RID: 37822 RVA: 0x003531C2 File Offset: 0x003513C2
		public override bool HandleEvent(EnteredCellEvent E)
		{
			this.CheckZone(E.Cell.ParentZone);
			return base.HandleEvent(E);
		}

		// Token: 0x060093BF RID: 37823 RVA: 0x003531DC File Offset: 0x003513DC
		public override bool HandleEvent(GetZoneSuspendabilityEvent E)
		{
			if (this.Pinned)
			{
				E.Suspendability = Suspendability.Pinned;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060093C0 RID: 37824 RVA: 0x003531F6 File Offset: 0x003513F6
		public void CheckZone(Zone Zone)
		{
			if (Zone == null)
			{
				return;
			}
			if (!Zone.IsWorldMap())
			{
				this.ParentObject.RemovePart(this);
				return;
			}
			if (this.Pinned && Zone.Suspended && !Zone.Stale)
			{
				The.ZoneManager.SetCachedZone(Zone);
			}
		}

		// Token: 0x04003D17 RID: 15639
		public int ParasangX;

		// Token: 0x04003D18 RID: 15640
		public int ParasangY;

		// Token: 0x04003D19 RID: 15641
		public int ZoneX = -1;

		// Token: 0x04003D1A RID: 15642
		public int ZoneY = -1;

		// Token: 0x04003D1B RID: 15643
		public int Segments;

		// Token: 0x04003D1C RID: 15644
		public bool Cardinal;

		// Token: 0x04003D1D RID: 15645
		public bool Pinned;

		// Token: 0x04003D1E RID: 15646
		[NonSerialized]
		private FindPath Pathfinder;

		// Token: 0x04003D1F RID: 15647
		[NonSerialized]
		private int TravelSegments = -1;
	}
}
