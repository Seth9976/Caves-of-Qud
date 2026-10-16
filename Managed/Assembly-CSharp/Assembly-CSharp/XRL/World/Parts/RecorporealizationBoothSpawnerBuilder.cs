using System;
using System.Linq;
using Genkit;
using XRL.World.ZoneBuilders;

namespace XRL.World.Parts
{
	// Token: 0x02000FB1 RID: 4017
	public class RecorporealizationBoothSpawnerBuilder : ZoneBuilderSandbox
	{
		// Token: 0x06009FBC RID: 40892 RVA: 0x003A0E4C File Offset: 0x0039F04C
		public void BuildZone(Zone zone)
		{
			InfluenceMapRegion influenceMapRegion = ZoneBuilderSandbox.GenerateInfluenceMap(zone, null, InfluenceMapSeedStrategy.LargestRegion, 200, null, false, null).Regions.Where<InfluenceMapRegion>((InfluenceMapRegion r) => r.maxRect.Width >= 12 && r.maxRect.Height >= 10).FirstOrDefault<InfluenceMapRegion>();
			Cell cell = null;
			if (influenceMapRegion != null)
			{
				cell = zone.GetCell(influenceMapRegion.maxRect.x1, influenceMapRegion.maxRect.y1);
			}
			if (cell == null)
			{
				cell = (from c in zone.GetCells()
					where c.X < 69 && c.Y < 16
					select c).GetRandomElement<Cell>(null);
			}
			for (int i = 0; i < 10; i++)
			{
				for (int j = 0; j < 10; j++)
				{
					Cell cell2 = zone.GetCell(cell.X + j, cell.Y + i);
					if (cell2 != null)
					{
						cell2.Clear(null, false, true, null);
					}
				}
			}
			cell.AddObject("RemortingNook_11X19", null, null, null, null);
		}
	}
}
