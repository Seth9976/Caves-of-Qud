using System;
using Genkit;
using XRL.Rules;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AF5 RID: 2805
	public class Tarland
	{
		// Token: 0x0600706B RID: 28779 RVA: 0x002804A4 File Offset: 0x0027E6A4
		public bool BuildZone(Zone Z)
		{
			CellularGrid cellularGrid = new CellularGrid();
			cellularGrid.Passes = 3;
			cellularGrid.SeedChance = 55;
			cellularGrid.SeedBorders = true;
			cellularGrid.Generate(Stat.Rand, Z.Width, Z.Height);
			CellularGrid cellularGrid2 = new CellularGrid();
			cellularGrid2.Passes = 3;
			cellularGrid2.SeedChance = 45;
			cellularGrid2.SeedBorders = true;
			cellularGrid2.Generate(Stat.Rand, Z.Width, Z.Height);
			for (int i = 0; i < Z.Height; i++)
			{
				for (int j = 0; j < Z.Width; j++)
				{
					if (cellularGrid.cells[j, i] == 0)
					{
						Z.GetCell(j, i).Clear(null, false, false, null).AddObject("AsphaltPuddle", null, null, null, null);
					}
					else if (cellularGrid2.cells[j, i] == 0)
					{
						Z.GetCell(j, i).Clear(null, false, false, null).AddObject("Shale", null, null, null, null);
					}
				}
			}
			Z.GetCell(0, 0).AddObject("Dirty", null, null, null, null);
			return true;
		}
	}
}
