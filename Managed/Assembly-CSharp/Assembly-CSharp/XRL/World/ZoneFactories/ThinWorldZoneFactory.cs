using System;
using System.Collections.Generic;
using System.Linq;
using XRL.World.Parts;

namespace XRL.World.ZoneFactories
{
	// Token: 0x020009A3 RID: 2467
	public class ThinWorldZoneFactory : IZoneFactory
	{
		// Token: 0x06006AA1 RID: 27297 RVA: 0x00224928 File Offset: 0x00222B28
		public override Zone BuildZone(ZoneRequest Request)
		{
			if (Request.IsWorldZone)
			{
				Zone zone = new Zone(80, 25);
				zone.GetCells().ForEach(delegate(Cell c)
				{
					c.AddObject("TerrainBrightsheol", null, null, null, null);
				});
				return zone;
			}
			if (ZoneManager.instance.CachedZones.Any<KeyValuePair<string, Zone>>((KeyValuePair<string, Zone> z) => z.Key.StartsWith("ThinWorld.")))
			{
				return ZoneManager.instance.CachedZones.First<KeyValuePair<string, Zone>>((KeyValuePair<string, Zone> z) => z.Key.StartsWith("ThinWorld.")).Value;
			}
			Zone zone2 = new Zone(80, 25);
			zone2.ZoneID = Request.ZoneID;
			zone2.loadMap("Brightsheol.rpm");
			zone2.GetCell(0, 0).AddObject("ThinWorldWidget", null, null, null, null);
			zone2.DisplayName = "Garden at the Gate to Brightsheol";
			return zone2;
		}

		// Token: 0x06006AA2 RID: 27298 RVA: 0x00224A1C File Offset: 0x00222C1C
		public override void AfterBuildZone(Zone zone, ZoneManager zoneManager)
		{
			zone.GetObjectsWithPart("Physics").ForEach(delegate(GameObject o)
			{
				if (!o.HasTagOrProperty("NoThinWorldHologram"))
				{
					o.AddPart<HologramMaterial>(new HologramMaterial(), true, false);
				}
				else
				{
					o.MakeNonflammable();
				}
				if (o.HasPart<UnityPrefabImposter>())
				{
					UnityPrefabImposter part = o.GetPart<UnityPrefabImposter>();
					if (part.prefabID == "Prefabs/Imposters/StatueBase")
					{
						part.prefabID = "Prefabs/Imposters/BlueStatueBase";
					}
				}
			});
			zone.SetInside(new bool?(true));
			ZoneManager.PaintWalls(zone, 0, 0, -1, -1);
			ZoneManager.PaintWater(zone, 0, 0, -1, -1);
		}
	}
}
