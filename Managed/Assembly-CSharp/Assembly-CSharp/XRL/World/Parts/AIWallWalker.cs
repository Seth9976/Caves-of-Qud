using System;
using System.Collections.Generic;

namespace XRL.World.Parts
{
	// Token: 0x02000E34 RID: 3636
	[Serializable]
	public class AIWallWalker : AIBehaviorPart
	{
		// Token: 0x06009399 RID: 37785 RVA: 0x003522DC File Offset: 0x003504DC
		public bool WalkToNearbyWall()
		{
			AIWallWalker.cells.Clear();
			foreach (Cell cell in this.ParentObject.CurrentCell.GetAdjacentCells(true))
			{
				if (cell.HasWall())
				{
					bool flag = false;
					using (List<Cell>.Enumerator enumerator2 = cell.GetAdjacentCells(true).GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							if (enumerator2.Current.IsPassable(null, true))
							{
								flag = true;
								break;
							}
						}
					}
					if (flag)
					{
						AIWallWalker.cells.Add(cell);
					}
				}
			}
			if (AIWallWalker.cells.Count > 0)
			{
				Cell randomElement = AIWallWalker.cells.GetRandomElement<Cell>(null);
				this.ParentObject.DirectMoveTo(randomElement, 1000, false, false, false, null);
				AIWallWalker.cells.Clear();
				return true;
			}
			return false;
		}

		// Token: 0x0600939A RID: 37786 RVA: 0x003523DC File Offset: 0x003505DC
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<AIBoredEvent>.ID || ID == PooledEvent<CanJoinPartyLeaderEvent>.ID;
		}

		// Token: 0x0600939B RID: 37787 RVA: 0x003523FA File Offset: 0x003505FA
		public override bool HandleEvent(AIBoredEvent E)
		{
			return (!this.chanceToWander.in100() || !this.WalkToNearbyWall()) && base.HandleEvent(E);
		}

		// Token: 0x0600939C RID: 37788 RVA: 0x0035241C File Offset: 0x0035061C
		public override bool HandleEvent(CanJoinPartyLeaderEvent E)
		{
			return (E.TargetCell.HasWalkableWallFor(this.ParentObject) || E.TargetCell.HasObject("StairsDown") || E.TargetCell.HasObject("StairsUp")) && base.HandleEvent(E);
		}

		// Token: 0x0600939D RID: 37789 RVA: 0x00352469 File Offset: 0x00350669
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600939E RID: 37790 RVA: 0x0035246C File Offset: 0x0035066C
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AIFailCombatPathfind");
			Registrar.Register("BeforeTakeAction");
			Registrar.Register("KeepZoneCachedForPlayerJoin");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600939F RID: 37791 RVA: 0x00352498 File Offset: 0x00350698
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeforeTakeAction")
			{
				if (this.ParentObject.CurrentCell != null && !this.ParentObject.CurrentCell.HasWall() && this.WalkToNearbyWall())
				{
					return false;
				}
			}
			else if (E.ID == "AIFailCombatPathfind")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Target");
				if (!GameObject.Validate(ref gameObjectParameter))
				{
					return true;
				}
				if (this.ParentObject.CurrentCell == null)
				{
					return true;
				}
				int num = int.MaxValue;
				Cell cell = null;
				foreach (Cell cell2 in this.ParentObject.Physics.CurrentCell.GetAdjacentCells(true))
				{
					if (cell2.HasWall())
					{
						bool flag = false;
						using (List<Cell>.Enumerator enumerator2 = cell2.GetAdjacentCells(true).GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								if (enumerator2.Current.IsPassable(null, true))
								{
									flag = true;
									break;
								}
							}
						}
						if (flag && cell2.DistanceTo(gameObjectParameter) < num)
						{
							cell = cell2;
							num = cell2.DistanceTo(gameObjectParameter);
						}
					}
				}
				if (cell != null && cell.DistanceTo(gameObjectParameter) < this.ParentObject.Physics.CurrentCell.DistanceTo(gameObjectParameter))
				{
					this.ParentObject.DirectMoveTo(cell, 0, false, false, false, null);
					this.ParentObject.UseEnergy(1000);
					return false;
				}
			}
			else if (E.ID == "KeepZoneCachedForPlayerJoin")
			{
				return false;
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003D0C RID: 15628
		public int chanceToWander = 25;

		// Token: 0x04003D0D RID: 15629
		private static List<Cell> cells = new List<Cell>();
	}
}
