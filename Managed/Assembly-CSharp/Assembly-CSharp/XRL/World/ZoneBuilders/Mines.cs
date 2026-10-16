using System;
using System.Collections.Generic;
using XRL.Core;
using XRL.World.ZoneBuilders.Utility;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AA9 RID: 2729
	public class Mines
	{
		// Token: 0x06006F41 RID: 28481 RVA: 0x0026B61C File Offset: 0x0026981C
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
					if (noiseMapNode.depth >= this.MinimumDepth)
					{
						cell.AddObject(GameObjectFactory.Factory.CreateObject("AsphaltPuddle"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					}
				}
			}
			Z.ClearReachableMap();
			if (noiseMap.AreaNodes.ContainsKey(num) && noiseMap.AreaNodes[0].Count > 0)
			{
				Z.BuildReachableMap(noiseMap.AreaNodes[num][0].x, noiseMap.AreaNodes[num][0].y);
			}
			else
			{
				Z.ClearReachableMap(true);
			}
			return true;
		}

		// Token: 0x04003319 RID: 13081
		public int MinimumDepth = 5;
	}
}
