using System;
using Genkit;
using XRL.World.AI.Pathfinding;

namespace XRL.World.AI.GoalHandlers
{
	// Token: 0x02000B89 RID: 2953
	[Serializable]
	public class MoveTo : IMovementGoal
	{
		// Token: 0x0600760D RID: 30221 RVA: 0x002C58B2 File Offset: 0x002C3AB2
		public MoveTo()
		{
		}

		// Token: 0x0600760E RID: 30222 RVA: 0x002C58D0 File Offset: 0x002C3AD0
		public MoveTo(bool careful, bool overridesCombat, int shortBy, bool wandering, bool global, bool juggernaut, int MaxTurns, int AbortIfMoreSteps, int MaxWeight)
			: this()
		{
			this.careful = careful;
			this.overridesCombat = overridesCombat;
			this.shortBy = shortBy;
			this.wandering = wandering;
			this.global = global;
			this.juggernaut = juggernaut;
			this.MaxTurns = MaxTurns;
			this.AbortIfMoreSteps = AbortIfMoreSteps;
			this.MaxWeight = MaxWeight;
		}

		// Token: 0x0600760F RID: 30223 RVA: 0x002C5928 File Offset: 0x002C3B28
		public MoveTo(Cell C, bool careful = false, bool overridesCombat = false, int shortBy = 0, bool wandering = false, bool global = false, bool juggernaut = false, int MaxTurns = -1, int AbortIfMoreSteps = -1, int MaxWeight = 95)
			: this(careful, overridesCombat, shortBy, wandering, global, juggernaut, MaxTurns, AbortIfMoreSteps, MaxWeight)
		{
			if (C != null)
			{
				Zone parentZone = C.ParentZone;
				if (parentZone != null)
				{
					this.dZone = parentZone.ZoneID;
				}
				this.dCx = C.X;
				this.dCy = C.Y;
			}
		}

		// Token: 0x06007610 RID: 30224 RVA: 0x002C597C File Offset: 0x002C3B7C
		public MoveTo(Location2D L, bool careful = false, bool overridesCombat = false, int shortBy = 0, bool wandering = false, bool global = false, bool juggernaut = false, int MaxTurns = -1, int AbortIfMoreSteps = -1, int MaxWeight = 95)
			: this(careful, overridesCombat, shortBy, wandering, global, juggernaut, MaxTurns, AbortIfMoreSteps, MaxWeight)
		{
			if (L != null)
			{
				this.dCx = L.X;
				this.dCy = L.Y;
			}
		}

		// Token: 0x06007611 RID: 30225 RVA: 0x002C59C0 File Offset: 0x002C3BC0
		public MoveTo(GameObject obj, bool careful = false, bool overridesCombat = false, int shortBy = 0, bool wandering = false, bool global = false, bool juggernaut = false, int MaxTurns = -1, int AbortIfMoreSteps = -1, int MaxWeight = 95)
			: this(obj.GetCurrentCell(), careful, overridesCombat, shortBy, wandering, global, juggernaut, MaxTurns, AbortIfMoreSteps, MaxWeight)
		{
		}

		// Token: 0x06007612 RID: 30226 RVA: 0x002C59EC File Offset: 0x002C3BEC
		public MoveTo(string ZoneID, int Cx, int Cy, bool careful = false, bool overridesCombat = false, int shortBy = 0, bool wandering = false, bool global = false, bool juggernaut = false, int MaxTurns = -1, int AbortIfMoreSteps = -1, int MaxWeight = 95)
			: this(careful, overridesCombat, shortBy, wandering, global, juggernaut, MaxTurns, AbortIfMoreSteps, MaxWeight)
		{
			this.dZone = ZoneID;
			this.dCx = Cx;
			this.dCy = Cy;
		}

		// Token: 0x06007613 RID: 30227 RVA: 0x002C5A28 File Offset: 0x002C3C28
		public MoveTo(GlobalLocation loc, bool careful = false, bool overridesCombat = false, int shortBy = 0, bool wandering = false, bool global = false, bool juggernaut = false, int MaxTurns = -1, int AbortIfMoreSteps = -1, int MaxWeight = 95)
			: this(loc.ZoneID, loc.CellX, loc.CellY, careful, overridesCombat, shortBy, wandering, global, juggernaut, MaxTurns, AbortIfMoreSteps, MaxWeight)
		{
		}

		// Token: 0x06007614 RID: 30228 RVA: 0x002C5A5D File Offset: 0x002C3C5D
		public override bool CanFight()
		{
			return !this.overridesCombat;
		}

		// Token: 0x06007615 RID: 30229 RVA: 0x002C5A68 File Offset: 0x002C3C68
		public override bool Finished()
		{
			if (!base.ParentObject.IsMobile())
			{
				return true;
			}
			Cell currentCell = base.ParentObject.CurrentCell;
			if (currentCell != null)
			{
				if (currentCell.X == this.dCx && currentCell.Y == this.dCy)
				{
					return true;
				}
				if (this.shortBy > 0 && currentCell.DistanceTo(this.dCx, this.dCy) <= this.shortBy)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06007616 RID: 30230 RVA: 0x002C5AD8 File Offset: 0x002C3CD8
		public override void TakeAction()
		{
			if (!base.ParentObject.IsMobile())
			{
				base.FailToParent();
				return;
			}
			Zone currentZone = base.ParentObject.CurrentZone;
			string text = ((currentZone != null) ? currentZone.ZoneID : null);
			if (text == null)
			{
				base.Pop();
				return;
			}
			if (this.dZone.IsNullOrEmpty())
			{
				this.dZone = text;
			}
			Cell currentCell = base.ParentObject.CurrentCell;
			if (text == this.dZone)
			{
				Cell cell = currentCell.ParentZone.GetCell(this.dCx, this.dCy);
				if (cell == null || cell == currentCell || (this.shortBy == 1 && cell.IsAdjacentTo(currentCell, true)) || (this.shortBy > 1 && cell.PathDistanceTo(currentCell) <= this.shortBy))
				{
					base.Pop();
					return;
				}
				if (AICommandList.HandleCommandList(AIGetMovementAbilityListEvent.GetFor(base.ParentObject, null, cell, -1, 0), base.ParentObject, null, cell, 0))
				{
					return;
				}
			}
			string text2 = text;
			int x = currentCell.X;
			int y = currentCell.Y;
			string text3 = this.dZone;
			int num = this.dCx;
			int num2 = this.dCy;
			bool flag = this.global;
			bool flag2 = this.careful;
			bool flag3 = this.juggernaut;
			FindPath findPath = new FindPath(text2, x, y, text3, num, num2, flag, flag2, base.ParentObject, flag3, false, false, false, this.MaxWeight);
			base.ParentObject.UseEnergy(1000, "Pathfinding", null, null, false);
			if (!findPath.Usable)
			{
				base.FailToParent();
				return;
			}
			int num3 = findPath.Directions.Count;
			if (this.MaxTurns > -1)
			{
				base.Pop();
				if (num3 > this.MaxTurns)
				{
					num3 = this.MaxTurns;
				}
			}
			if (this.AbortIfMoreSteps > -1 && num3 > this.AbortIfMoreSteps)
			{
				base.FailToParent();
				return;
			}
			findPath.Directions.Reverse();
			for (int i = this.shortBy; i < num3; i++)
			{
				this.PushGoal(new Step(findPath.Directions[i], this.careful, this.overridesCombat, this.wandering, this.juggernaut, null, this.global));
			}
		}

		// Token: 0x06007617 RID: 30231 RVA: 0x002C5CD8 File Offset: 0x002C3ED8
		public Cell GetDestinationCell()
		{
			Zone zone = The.ZoneManager.GetZone(this.dZone);
			if (zone != null)
			{
				Cell cell = zone.GetCell(this.dCx, this.dCy);
				if (cell != null)
				{
					return cell;
				}
			}
			return null;
		}

		// Token: 0x0400360A RID: 13834
		public const int DEFAULT_MAX_WEIGHT = 95;

		// Token: 0x0400360B RID: 13835
		public string dZone;

		// Token: 0x0400360C RID: 13836
		public int dCx;

		// Token: 0x0400360D RID: 13837
		public int dCy;

		// Token: 0x0400360E RID: 13838
		public int MaxTurns = -1;

		// Token: 0x0400360F RID: 13839
		public bool careful;

		// Token: 0x04003610 RID: 13840
		public bool overridesCombat;

		// Token: 0x04003611 RID: 13841
		public int shortBy;

		// Token: 0x04003612 RID: 13842
		public bool wandering;

		// Token: 0x04003613 RID: 13843
		public bool global;

		// Token: 0x04003614 RID: 13844
		public bool juggernaut;

		// Token: 0x04003615 RID: 13845
		public int AbortIfMoreSteps = -1;

		// Token: 0x04003616 RID: 13846
		public int MaxWeight = 95;
	}
}
