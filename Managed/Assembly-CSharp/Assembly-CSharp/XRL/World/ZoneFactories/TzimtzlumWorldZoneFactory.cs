using System;
using System.Collections.Generic;

namespace XRL.World.ZoneFactories
{
	// Token: 0x020009A4 RID: 2468
	public class TzimtzlumWorldZoneFactory : IZoneFactory
	{
		// Token: 0x06006AA4 RID: 27300 RVA: 0x00224A80 File Offset: 0x00222C80
		public override Zone BuildZone(ZoneRequest Request)
		{
			if (Request.IsWorldZone)
			{
				Zone zone = new Zone(80, 25);
				zone.GetCells().ForEach(delegate(Cell c)
				{
					c.AddObject("TerrainTzimtzlum", null, null, null, null);
				});
				return zone;
			}
			ClamSystem clamSystem = The.Game.RequireSystem<ClamSystem>(() => new ClamSystem());
			if (Request.ZoneID != clamSystem.ClamWorldId)
			{
				return clamSystem.GetClamZone();
			}
			Zone zone2 = new Zone(80, 25);
			zone2.ZoneID = clamSystem.ClamWorldId;
			zone2.loadMap("Tzimtzlum.rpm");
			zone2.DisplayName = "Tzimtzlum";
			zone2.IncludeContextInZoneDisplay = false;
			zone2.IncludeStratumInZoneDisplay = false;
			zone2.SetMusic("Music/Clam Dimension");
			zone2.Built = true;
			List<GameObject> objects = zone2.GetObjects("Giant Clam");
			if (objects.Count == 0)
			{
				MetricsManager.LogError("Tzimtzlum generated without clams");
			}
			for (int i = 0; i < objects.Count; i++)
			{
				objects[i].SetIntProperty("ClamId", i, false);
			}
			The.Game.ZoneManager.SetZoneProperty(Request.ZoneID, "SpecialUpMessage", "You’re in a pocket dimension with no worldmap.");
			return zone2;
		}

		// Token: 0x06006AA5 RID: 27301 RVA: 0x00224BBC File Offset: 0x00222DBC
		public override void AfterBuildZone(Zone zone, ZoneManager zoneManager)
		{
			zone.SetInside(new bool?(true));
			ZoneManager.PaintWalls(zone, 0, 0, -1, -1);
			ZoneManager.PaintWater(zone, 0, 0, -1, -1);
		}
	}
}
