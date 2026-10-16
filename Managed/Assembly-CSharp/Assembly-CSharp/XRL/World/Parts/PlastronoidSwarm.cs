using System;
using System.Collections.Generic;

namespace XRL.World.Parts
{
	// Token: 0x02000E86 RID: 3718
	[Serializable]
	public class PlastronoidSwarm : IPart
	{
		// Token: 0x060095DC RID: 38364 RVA: 0x00363D5B File Offset: 0x00361F5B
		public override bool WantEvent(int ID, int Cascade)
		{
			return base.WantEvent(ID, Cascade) || ID == EnteredCellEvent.ID;
		}

		// Token: 0x060095DD RID: 38365 RVA: 0x00363D74 File Offset: 0x00361F74
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (!this.ParentObject.IsPlayer() && !this.ParentObject.IsNowhere() && !this.ParentObject.OnWorldMap())
			{
				List<Cell> list = new List<Cell>();
				this.ParentObject.CurrentCell.GetAdjacentCells(3, list, true, true);
				list.Add(this.ParentObject.CurrentCell);
				foreach (Cell cell in list)
				{
					if (cell.IsEmptyForPopulation() && 50.in100())
					{
						cell.AddObject("Plastronoid", null, null, null, null).MakeActive(false);
					}
				}
				this.ParentObject.Destroy(null, false, false, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04003DF9 RID: 15865
		public int SpawnTurns = 20;
	}
}
