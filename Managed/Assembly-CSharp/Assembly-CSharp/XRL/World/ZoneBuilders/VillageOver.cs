using System;
using System.Collections.Generic;
using Genkit;
using XRL.Core;
using XRL.Rules;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000B0B RID: 2827
	public class VillageOver : ZoneBuilderSandbox
	{
		// Token: 0x06007199 RID: 29081 RVA: 0x002A60E4 File Offset: 0x002A42E4
		public bool BuildZone(Zone Z)
		{
			Z.Clear(null);
			Z.Fill("Air");
			Z.GetCell(0, 0).AddObject("DaylightWidget", null, null, null, null);
			Z.SetZoneProperty("relaxedbiomes", "true");
			List<Location2D> list = new List<Location2D>();
			foreach (CachedZoneConnection cachedZoneConnection in Z.ZoneConnectionCache)
			{
				if (cachedZoneConnection.TargetDirection == "-" && cachedZoneConnection.Type == "aerie")
				{
					list.Add(Location2D.Get(cachedZoneConnection.X, cachedZoneConnection.Y));
				}
			}
			foreach (ZoneConnection zoneConnection in XRLCore.Core.Game.ZoneManager.GetZoneConnections(Z.ZoneID))
			{
				if (zoneConnection.Type == "aerie")
				{
					list.Add(Location2D.Get(zoneConnection.X, zoneConnection.Y));
				}
			}
			List<Box> list2 = new List<Box>();
			string blueprint = PopulationManager.RollOneFrom("AerieTemplates", null, null).Blueprint;
			foreach (Location2D location2D in list)
			{
				ColorOutputMap randomElement = base.getWfcBuildingTemplate(blueprint).GetRandomElement<ColorOutputMap>(null);
				int num = randomElement.width / 2;
				int num2 = randomElement.height / 2;
				int num3 = Stat.Random(1, 3);
				Box box = new Box(location2D.X - num3, location2D.Y - num3, location2D.X + num3, location2D.Y + num3).clamp(1, 1, 78, 23);
				for (int i = 0; i < randomElement.width; i++)
				{
					for (int j = 0; j < randomElement.height; j++)
					{
						Cell cell = Z.GetCell(location2D.X - num + i, location2D.Y - num2 + j);
						if (cell != null && (randomElement.getPixel(i, j).Equals(ColorOutputMap.BLACK) || location2D.X - num + i == location2D.X || location2D.X - num + i == location2D.Y))
						{
							cell.Clear(null, true, false, null);
							cell.AddObject("WoodFloor", null, null, null, null);
						}
					}
				}
				Z.GetCell(location2D).Clear(null, true, false, null);
				Z.GetCell(location2D).AddObject("StairsDown", null, null, null, null).SetIntProperty("IdleStairs", 1, false);
				list2.Add(box);
			}
			Z.GetCell(0, 0).AddObject("DaylightWidget", null, null, null, null);
			return true;
		}
	}
}
