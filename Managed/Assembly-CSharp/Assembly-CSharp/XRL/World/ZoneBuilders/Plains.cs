using System;
using XRL.Rules;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AC3 RID: 2755
	public class Plains
	{
		// Token: 0x06006F8B RID: 28555 RVA: 0x00271A68 File Offset: 0x0026FC68
		public bool BuildZone(Zone Z)
		{
			for (int i = 0; i < Z.Height; i++)
			{
				for (int j = 0; j < Z.Width; j++)
				{
					if (2.in100())
					{
						if (!Z.GetCell(j, i).IsOccluding())
						{
							Z.GetCell(j, i).AddObject("Dogthorn Tree", null, null, null, null);
						}
					}
					else if (2.in100())
					{
						if (!Z.GetCell(j, i).IsOccluding())
						{
							Z.GetCell(j, i).AddObject("Witchwood Tree", null, null, null, null);
						}
					}
					else if (Stat.Rnd.Next(3000) < 2 && !Z.GetCell(j, i).IsOccluding())
					{
						Z.GetCell(j, i).AddObject(GameObjectFactory.Factory.CreateObject("Starapple Tree"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					}
				}
			}
			Z.GetCell(0, 0).AddObject("DaylightWidget", null, null, null, null);
			Z.GetCell(0, 0).AddObject("Grassy", null, null, null, null);
			for (int k = 0; k < Z.Height; k++)
			{
				for (int l = 0; l < Z.Width; l++)
				{
					if (Z.GetCell(l, k).IsEmpty())
					{
						Z.ClearReachableMap(true);
						if (Z.BuildReachableMap(l, k) > 400)
						{
							return true;
						}
					}
				}
			}
			return true;
		}
	}
}
