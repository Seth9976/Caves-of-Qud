using System;
using XRL.World.ZoneBuilders.Utility;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AF4 RID: 2804
	public class SurfaceCave
	{
		// Token: 0x06007069 RID: 28777 RVA: 0x00280394 File Offset: 0x0027E594
		public bool BuildZone(Zone Z)
		{
			Z.GetCell(0, 0).AddObject(GameObjectFactory.Factory.CreateObject("DaylightWidget"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			NoiseMap noiseMap = new NoiseMap(80, 25, 10, 3, 2, 5, 50, 50, 4, 1, 2, 1, null);
			int num = -1;
			for (int i = 0; i < noiseMap.nAreas; i++)
			{
				if (noiseMap.AreaNodes[i].Count > num)
				{
					num = noiseMap.AreaNodes[i].Count;
				}
			}
			for (int j = 0; j < Z.Width; j++)
			{
				for (int k = 0; k < Z.Height; k++)
				{
					if (noiseMap.Noise[j, k] > 1)
					{
						Z.GetCell(j, k).AddObject(GameObjectFactory.Factory.CreateObject("Sandstone"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					}
				}
			}
			Z.ClearReachableMap();
			return Z.BuildReachableMap(0, 0, false) >= 400;
		}
	}
}
