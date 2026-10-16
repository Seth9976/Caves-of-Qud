using System;
using XRL.World.AI.Pathfinding;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A7D RID: 2685
	public class StairConnector
	{
		// Token: 0x06006E7B RID: 28283 RVA: 0x0025C840 File Offset: 0x0025AA40
		public bool BuildZone(Zone Z)
		{
			GameObject gameObject = Z.FindObject("StairsUp");
			GameObject gameObject2 = Z.FindObject("StairsDown");
			if (gameObject == null || gameObject2 == null || !gameObject.InACell() || !gameObject2.InACell())
			{
				return true;
			}
			GameObject gameObject3 = GameObject.Create("Drillbot");
			FindPath findPath = new FindPath(Z, gameObject.CurrentCell.X, gameObject.CurrentCell.Y, Z, gameObject2.CurrentCell.X, gameObject2.CurrentCell.Y, false, true, gameObject3, true);
			if (!findPath.Usable)
			{
				return false;
			}
			foreach (Cell cell in findPath.Steps)
			{
				cell.ClearWalls();
				cell.ClearImpassableObjects(The.Player, false);
			}
			return true;
		}
	}
}
