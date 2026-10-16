using System;
using System.Collections.Generic;
using Wintellect.PowerCollections;
using XRL.Rules;
using XRL.World.AI.Pathfinding;
using XRL.World.Parts;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A80 RID: 2688
	public class ConveyorBelt
	{
		// Token: 0x06006E86 RID: 28294 RVA: 0x0025D2BC File Offset: 0x0025B4BC
		public bool BuildZone(Zone Z)
		{
			FindPath findPath = new FindPath();
			if (this.StepDirection == "")
			{
				findPath = new FindPath(Z, this.x1, this.y1, Z, this.x2, this.y2, false, true, null, false, true, false, false, false, false);
			}
			if (this.StepDirection != "")
			{
				int x = this.x1;
				int y = this.y1;
				Cell cell = Z.GetCell(this.x1, this.y1);
				while ((x != this.x2 || y != this.y2) && cell != null)
				{
					findPath.Steps.Add(cell);
					Cell cell2 = null;
					for (int i = 0; i < this.StepDirection.Length; i++)
					{
						cell2 = cell.GetCellFromDirection(this.StepDirection[i].ToString(), true);
						if (cell2 != null && !cell2.HasWall() && !cell2.HasObjectWithPart("ConveyorBelt"))
						{
							break;
						}
						cell2 = null;
					}
					cell = cell2;
					if (cell != null)
					{
						x = cell.X;
						y = cell.Y;
					}
				}
				findPath.Found = true;
			}
			if (!findPath.Usable)
			{
				return false;
			}
			int num = 0;
			string text = "N";
			foreach (Cell cell3 in findPath.Steps)
			{
				cell3.Clear(null, false, false, null);
				GameObject gameObject = GameObjectFactory.Factory.CreateObject("ConveyorPad");
				ConveyorPad part = gameObject.GetPart<ConveyorPad>();
				if (num < findPath.Steps.Count - 1)
				{
					part.Direction = cell3.GetDirectionFromCell(findPath.Steps[num + 1], false);
					part.Connections = part.Direction;
					text = part.Direction;
				}
				else
				{
					part.Direction = text;
					part.Connections = text;
				}
				cell3.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				num++;
			}
			if (this.DriveDirection != "")
			{
				Z.GetCell(this.x1, this.y1).GetCellFromDirection(this.DriveDirection, true).AddObject("ConveyorDrive", null, null, null, null);
				return true;
			}
			List<Cell> cardinalAdjacentCells = Z.GetCell(this.x1, this.y1).GetCardinalAdjacentCells(false, true, false);
			Algorithms.RandomShuffleInPlace<Cell>(cardinalAdjacentCells, Stat.Rand);
			using (List<Cell>.Enumerator enumerator = cardinalAdjacentCells.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.HasObjectWithPart("ConveyorDriver"))
					{
						return true;
					}
				}
			}
			foreach (Cell cell4 in cardinalAdjacentCells)
			{
				if (cell4.IsEmpty() && !cell4.HasObjectWithPart("ConveyorPad"))
				{
					int num2 = 0;
					using (List<Cell>.Enumerator enumerator2 = cell4.GetAdjacentCells(true).GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							if (enumerator2.Current.HasObjectWithPart("ConveyorPad"))
							{
								num2++;
							}
						}
					}
					if (num2 == 1)
					{
						cell4.Clear(null, false, false, null);
						cell4.AddObject(GameObjectFactory.Factory.CreateObject("ConveyorDrive"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						return true;
					}
				}
			}
			return true;
		}

		// Token: 0x04003292 RID: 12946
		public int x1;

		// Token: 0x04003293 RID: 12947
		public int y1;

		// Token: 0x04003294 RID: 12948
		public int x2;

		// Token: 0x04003295 RID: 12949
		public int y2;

		// Token: 0x04003296 RID: 12950
		public string DriveDirection = "";

		// Token: 0x04003297 RID: 12951
		public string StepDirection = "";
	}
}
