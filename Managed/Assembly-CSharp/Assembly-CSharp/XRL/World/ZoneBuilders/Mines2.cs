using System;
using System.Collections.Generic;
using XRL.Core;
using XRL.World.ZoneBuilders.Utility;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AAA RID: 2730
	public class Mines2
	{
		// Token: 0x06006F43 RID: 28483 RVA: 0x0026B8B0 File Offset: 0x00269AB0
		public bool BuildZone(Zone Z)
		{
			List<NoiseMapNode> list = new List<NoiseMapNode>();
			foreach (ZoneConnection zoneConnection in XRLCore.Core.Game.ZoneManager.GetZoneConnections(Z.ZoneID))
			{
				list.Add(new NoiseMapNode(zoneConnection.X, zoneConnection.Y));
			}
			foreach (CachedZoneConnection cachedZoneConnection in Z.ZoneConnectionCache)
			{
				if (cachedZoneConnection.TargetDirection == "-")
				{
					list.Add(new NoiseMapNode(cachedZoneConnection.X, cachedZoneConnection.Y));
				}
			}
			NoiseMap noiseMap = new NoiseMap(80, 25, 20, 3, 3, 5, 50, 60, 6, 2, 1, 1, list);
			int num = -1;
			int num2 = -1;
			for (int i = 0; i < noiseMap.nAreas; i++)
			{
				if (noiseMap.AreaNodes[i].Count > num2)
				{
					num2 = noiseMap.AreaNodes[i].Count;
					num = i;
				}
			}
			foreach (List<NoiseMapNode> list2 in noiseMap.AreaNodes.Values)
			{
				foreach (NoiseMapNode noiseMapNode in list2)
				{
					Cell cell = Z.GetCell(noiseMapNode.x, noiseMapNode.y);
					cell.Clear(null, false, false, null);
					if (noiseMapNode.depth > 4)
					{
						cell.AddObject(GameObjectFactory.Factory.CreateObject("LavaPuddle"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					}
				}
			}
			Z.ClearReachableMap();
			Z.BuildReachableMap(noiseMap.AreaNodes[num][0].x, noiseMap.AreaNodes[num][0].y);
			return true;
		}
	}
}
