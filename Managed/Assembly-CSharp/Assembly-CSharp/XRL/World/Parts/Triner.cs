using System;
using System.Collections.Generic;
using XRL.World.AI.GoalHandlers;

namespace XRL.World.Parts
{
	// Token: 0x02000F0F RID: 3855
	[Serializable]
	public class Triner : Twinner
	{
		// Token: 0x060099AB RID: 39339 RVA: 0x0037CF42 File Offset: 0x0037B142
		public override IPart DeepCopy(GameObject Parent)
		{
			return new Triner
			{
				ParentObject = Parent,
				DesiredDistance = this.DesiredDistance,
				RealityStabilizationPenetration = this.RealityStabilizationPenetration
			};
		}

		// Token: 0x060099AC RID: 39340 RVA: 0x0037CF68 File Offset: 0x0037B168
		public override void Act()
		{
			this.TrySpawn();
			this.StationKeeping();
		}

		// Token: 0x060099AD RID: 39341 RVA: 0x0037CF78 File Offset: 0x0037B178
		public void TrySpawn()
		{
			GameObject twin = this.Twin;
			if (((twin != null) ? twin.CurrentCell : null) != null)
			{
				GameObject triplet = this.Triplet;
				if (((triplet != null) ? triplet.CurrentCell : null) != null)
				{
					return;
				}
			}
			if (!base.CheckMyRealityDistortionAdvisability(null, null, null, null, null, null, 0))
			{
				return;
			}
			List<Cell> spawnCells = base.GetSpawnCells();
			if (spawnCells.Count < 1)
			{
				return;
			}
			Event @event = Event.New("InitiateRealityDistortionTransit", 0, 0, 0);
			@event.SetParameter("Object", this.ParentObject);
			@event.SetParameter("Mutation", this);
			@event.SetParameter("RealityStabilizationPenetration", this.RealityStabilizationPenetration);
			GameObject twin2 = this.Twin;
			if (((twin2 != null) ? twin2.CurrentCell : null) == null)
			{
				Cell randomElement = spawnCells.GetRandomElement<Cell>(null);
				if (randomElement != null)
				{
					@event.SetParameter("Cell", randomElement);
					if (this.ParentObject.FireEvent(@event) && randomElement.FireEvent(@event))
					{
						this.Twin = this.ParentObject.DeepCopy(false, false, null);
						if (this.Triplet != null)
						{
							this.Triplet.GetPart<Triner>().AddCopy(this.Twin);
						}
						this.Twin.RemovePart<GivesRep>();
						Triner part = this.Twin.GetPart<Triner>();
						part.Twin = this.ParentObject;
						part.Triplet = this.Triplet;
						spawnCells.Remove(randomElement);
						base.Spawn(this.Twin, randomElement);
					}
				}
			}
			GameObject triplet2 = this.Triplet;
			if (((triplet2 != null) ? triplet2.CurrentCell : null) == null)
			{
				Cell randomElement2 = spawnCells.GetRandomElement<Cell>(null);
				if (randomElement2 != null)
				{
					@event.SetParameter("Cell", randomElement2);
					if (this.ParentObject.FireEvent(@event) && randomElement2.FireEvent(@event))
					{
						this.Triplet = this.ParentObject.DeepCopy(false, false, null);
						if (this.Twin != null)
						{
							this.Twin.GetPart<Triner>().AddCopy(this.Triplet);
						}
						this.Triplet.RemovePart<GivesRep>();
						Triner part2 = this.Triplet.GetPart<Triner>();
						part2.Twin = this.Twin;
						part2.Triplet = this.ParentObject;
						spawnCells.Remove(randomElement2);
						base.Spawn(this.Triplet, randomElement2);
					}
				}
			}
		}

		// Token: 0x060099AE RID: 39342 RVA: 0x0037D19B File Offset: 0x0037B39B
		public void AddCopy(GameObject Object)
		{
			GameObject twin = this.Twin;
			if (((twin != null) ? twin.CurrentCell : null) == null)
			{
				this.Twin = Object;
				return;
			}
			GameObject triplet = this.Triplet;
			if (((triplet != null) ? triplet.CurrentCell : null) == null)
			{
				this.Triplet = Object;
			}
		}

		// Token: 0x060099AF RID: 39343 RVA: 0x0037D1D4 File Offset: 0x0037B3D4
		public void StationKeeping()
		{
			if (this.ParentObject.Brain == null)
			{
				return;
			}
			int num = ((this.Twin != null) ? this.ParentObject.DistanceTo(this.Twin) : this.DesiredDistance);
			int num2 = ((this.Triplet != null) ? this.ParentObject.DistanceTo(this.Triplet) : this.DesiredDistance);
			if (num == this.DesiredDistance && num2 == this.DesiredDistance)
			{
				return;
			}
			GameObject target = this.ParentObject.Target;
			if (target != null && this.ParentObject.InAdjacentCellTo(target))
			{
				GameObject twin = this.Twin;
				GameObject gameObject = ((twin != null) ? twin.Target : null);
				GameObject triplet = this.Triplet;
				GameObject gameObject2 = ((triplet != null) ? triplet.Target : null);
				if (gameObject == null || gameObject2 == null || !this.Twin.InAdjacentCellTo(gameObject) || !this.Triplet.InAdjacentCellTo(gameObject2))
				{
					return;
				}
			}
			List<Cell> passableConnectedAdjacentCellsFor = this.ParentObject.CurrentCell.GetPassableConnectedAdjacentCellsFor(this.ParentObject, 1, null, true, true);
			if (passableConnectedAdjacentCellsFor.Count < 1)
			{
				return;
			}
			if (passableConnectedAdjacentCellsFor.Count > 1)
			{
				passableConnectedAdjacentCellsFor.Sort(new Comparison<Cell>(this.CellSort));
			}
			if (this.ParentObject.Brain.Goals.Peek() is Step)
			{
				this.ParentObject.Brain.Goals.Pop();
			}
			this.ParentObject.Brain.Think("I'm going to try keeping station with my trins.");
			this.ParentObject.Brain.PushGoal(new Step(this.ParentObject.CurrentCell.GetDirectionFromCell(passableConnectedAdjacentCellsFor[0], false), false, false, false, false, null, false));
		}

		// Token: 0x060099B0 RID: 39344 RVA: 0x0037D368 File Offset: 0x0037B568
		private int CellSort(Cell a, Cell b)
		{
			int num = this.CellAvoidance(a).CompareTo(this.CellAvoidance(b));
			if (num != 0)
			{
				return num;
			}
			GameObject target = this.ParentObject.Target;
			if (target != null)
			{
				return a.DistanceTo(target).CompareTo(b.DistanceTo(target));
			}
			return 0;
		}

		// Token: 0x060099B1 RID: 39345 RVA: 0x0037D3B8 File Offset: 0x0037B5B8
		private int CellAvoidance(Cell cell)
		{
			int num = ((this.Twin != null) ? Math.Abs(this.DesiredDistance - cell.DistanceTo(this.Twin)) : 0);
			int num2 = ((this.Triplet != null) ? Math.Abs(this.DesiredDistance - cell.DistanceTo(this.Triplet)) : 0);
			return num + num2;
		}

		// Token: 0x060099B2 RID: 39346 RVA: 0x0037D410 File Offset: 0x0037B610
		public override void EnteredCell()
		{
			GameObject twin = this.Twin;
			if (((twin != null) ? twin.CurrentCell : null) != null && this.ParentObject.CurrentCell.ParentZone != this.Twin.CurrentCell.ParentZone)
			{
				GameObject triplet = this.Triplet;
				if (((triplet != null) ? triplet.CurrentCell : null) != null && this.ParentObject.CurrentCell.ParentZone != this.Triplet.CurrentCell.ParentZone)
				{
					List<Cell> followCells = base.GetFollowCells();
					Cell cell = this.ParentObject.CurrentCell;
					if (this.Twin != null)
					{
						cell = followCells.GetRandomElement<Cell>(null);
						if (cell != null)
						{
							followCells.Remove(cell);
							this.Twin.Brain.Goals.Clear();
							this.Twin.SystemLongDistanceMoveTo(cell, null, false, true);
							this.Twin.UseEnergy(1000, "Move Join Leader", null, null, false);
						}
					}
					if (this.Triplet != null)
					{
						cell = followCells.GetRandomElement<Cell>(null);
						if (cell != null)
						{
							followCells.Remove(cell);
							this.Triplet.Brain.Goals.Clear();
							this.Triplet.SystemLongDistanceMoveTo(cell, null, false, true);
							this.Triplet.UseEnergy(1000, "Move Join Leader", null, null, false);
						}
					}
				}
			}
		}

		// Token: 0x060099B3 RID: 39347 RVA: 0x0037D57C File Offset: 0x0037B77C
		public override void OnDestroyObject()
		{
			if (this.Twin != null && this.Twin.CurrentCell == null)
			{
				Triner part = this.Twin.GetPart<Triner>();
				if (part != null)
				{
					part.Twin = null;
					part.Triplet = null;
				}
				this.Twin.Obliterate(null, false, null);
			}
			if (this.Triplet != null && this.Triplet.CurrentCell == null)
			{
				Triner part2 = this.Triplet.GetPart<Triner>();
				if (part2 != null)
				{
					part2.Twin = null;
					part2.Triplet = null;
				}
				this.Triplet.Obliterate(null, false, null);
			}
		}

		// Token: 0x0400400F RID: 16399
		public GameObject Triplet;

		// Token: 0x04004010 RID: 16400
		public int DesiredDistance = 3;

		// Token: 0x04004011 RID: 16401
		public int RealityStabilizationPenetration = 80;
	}
}
