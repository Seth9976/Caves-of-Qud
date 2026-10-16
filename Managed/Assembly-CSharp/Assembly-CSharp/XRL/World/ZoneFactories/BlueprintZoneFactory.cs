using System;
using System.Collections.Generic;
using UnityEngine;

namespace XRL.World.ZoneFactories
{
	// Token: 0x020009A0 RID: 2464
	public class BlueprintZoneFactory : IZoneFactory
	{
		// Token: 0x06006A95 RID: 27285 RVA: 0x00224637 File Offset: 0x00222837
		public override bool CanBuildZone(ZoneRequest Request)
		{
			return !Request.IsWorldZone && Request.ZoneID.LastIndexOf("-", StringComparison.Ordinal) > Request.ZoneID.LastIndexOf(".", StringComparison.Ordinal);
		}

		// Token: 0x06006A96 RID: 27286 RVA: 0x00224668 File Offset: 0x00222868
		public override void AddBlueprintsFor(ZoneRequest Request)
		{
			GameObject terrainObjectForZone = ZoneManager.GetTerrainObjectForZone(Request.WorldX, Request.WorldY, Request.WorldID);
			Dictionary<string, CellBlueprint> cellBlueprintsByApplication = Request.World.CellBlueprintsByApplication;
			int num = Mathf.Clamp(Request.Z, 0, 49);
			object obj;
			if (cellBlueprintsByApplication == null)
			{
				obj = null;
			}
			else
			{
				CellBlueprint value = cellBlueprintsByApplication.GetValue((terrainObjectForZone != null) ? terrainObjectForZone.Blueprint : null, null);
				obj = ((value != null) ? value.LevelBlueprint[Request.X, Request.Y, num] : null);
			}
			object obj2;
			if ((obj2 = obj) == null)
			{
				if (cellBlueprintsByApplication == null)
				{
					obj2 = null;
				}
				else
				{
					CellBlueprint value2 = cellBlueprintsByApplication.GetValue("*Default", null);
					obj2 = ((value2 != null) ? value2.LevelBlueprint[Request.X, Request.Y, num] : null);
				}
			}
			ZoneBlueprint zoneBlueprint = obj2;
			if (zoneBlueprint != null)
			{
				Request.Blueprints.Add(zoneBlueprint);
			}
			string text = Request.WorldX.ToString() + "." + Request.WorldY.ToString();
			ZoneBlueprint zoneBlueprint2;
			if (cellBlueprintsByApplication == null)
			{
				zoneBlueprint2 = null;
			}
			else
			{
				CellBlueprint value3 = cellBlueprintsByApplication.GetValue(text, null);
				zoneBlueprint2 = ((value3 != null) ? value3.LevelBlueprint[Request.X, Request.Y, num] : null);
			}
			zoneBlueprint = zoneBlueprint2;
			if (zoneBlueprint != null)
			{
				Request.Blueprints.Add(zoneBlueprint);
			}
		}

		// Token: 0x06006A97 RID: 27287 RVA: 0x00224784 File Offset: 0x00222984
		public override Zone BuildZone(ZoneRequest Request)
		{
			return new Zone();
		}

		// Token: 0x06006A98 RID: 27288 RVA: 0x0022478B File Offset: 0x0022298B
		public override void AfterBuildZone(Zone zone, ZoneManager zoneManager)
		{
			ZoneManager.PaintWalls(zone, 0, 0, -1, -1);
			ZoneManager.PaintWater(zone, 0, 0, -1, -1);
		}
	}
}
