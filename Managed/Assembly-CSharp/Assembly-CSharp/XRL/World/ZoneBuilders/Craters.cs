using System;
using XRL.Rules;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A81 RID: 2689
	public class Craters : ZoneBuilderSandbox
	{
		// Token: 0x06006E88 RID: 28296 RVA: 0x0025D69C File Offset: 0x0025B89C
		public bool BuildZone(Zone Z)
		{
			Z.GetCell(0, 0).AddObject(GameObjectFactory.Factory.CreateObject("DaylightWidget"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			Z.GetCell(0, 0).AddObject(GameObjectFactory.Factory.CreateObject("Dirty"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			for (int i = 0; i < Z.Width; i++)
			{
				for (int j = 0; j < Z.Height; j++)
				{
					if (Stat.Random(1, 100) <= 10)
					{
						Z.GetCell(i, j).AddObject("TerrainCraters", null, null, null, null);
					}
				}
			}
			return true;
		}
	}
}
