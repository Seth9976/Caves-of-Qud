using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.World.ZoneBuilders.Utility;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AE4 RID: 2788
	public class WallOutcrop : ZoneBuilderSandbox
	{
		// Token: 0x06007000 RID: 28672 RVA: 0x00279724 File Offset: 0x00277924
		public bool BuildZone(Zone Z)
		{
			Z.GetCell(0, 0).RequireObject("DaylightWidget", null, null, null, null);
			List<NoiseMapNode> list = new List<NoiseMapNode>();
			for (int i = 0; i < 20; i++)
			{
				list.Add(new NoiseMapNode(10, i, -10));
				list.Add(new NoiseMapNode(i, 10, -10));
			}
			NoiseMap noiseMap = new NoiseMap(20, 20, 10, 3, 3, 2, 80, 80, 4, 3, 0, 1, list);
			NoiseMap noiseMap2 = new NoiseMap(Z.Width, Z.Height, 10, 3, 3, 2, 20, 20, 4, 3, 0, 1, list);
			int num = Stat.Random(1, 59);
			int num2 = Stat.Random(1, 3);
			bool flag = false;
			foreach (ZoneConnection zoneConnection in The.ZoneManager.GetZoneConnections(Z.ZoneID))
			{
				if (zoneConnection.Type == "StairsDown")
				{
					num = zoneConnection.X;
					num2 = zoneConnection.Y;
					flag = true;
					break;
				}
			}
			for (int j = 0; j < 20; j++)
			{
				for (int k = 0; k < 20; k++)
				{
					if (noiseMap.Noise[k, j] > 1)
					{
						Cell cell = Z.GetCell(k + num, j + num2);
						if (cell != null)
						{
							cell.ClearAndAddObject(this.Blueprint);
						}
					}
				}
			}
			for (int l = 0; l < Z.Height; l++)
			{
				for (int m = 0; m < Z.Width; m++)
				{
					if (noiseMap2.Noise[m, l] > 1)
					{
						Z.GetCell(m, l).ClearAndAddObject(this.Blueprint);
					}
				}
			}
			if (flag)
			{
				Cell cell2 = Z.GetCell(num, num2);
				cell2.Clear(null, false, false, null);
				cell2.RequireObject("StairsDown", null, null, null, null);
				base.EnsureCellReachable(Z, cell2);
				this.BuildReachableMap(Z, num, num2);
				return true;
			}
			this.BuildReachableMap(Z, num, num2);
			for (int n = 0; n < 11; n++)
			{
				for (int num3 = 10 - n; num3 <= 10 + n; num3++)
				{
					for (int num4 = 10 - n; num4 <= 10 + n; num4++)
					{
						Cell cell3 = Z.GetCell(num4 + num, num3 + num2);
						if (cell3.IsReachable() && cell3.IsEmpty())
						{
							cell3.AddObject("StairsDown", null, null, null, null);
							return true;
						}
					}
				}
			}
			MetricsManager.LogError("Failed placing stairs down, placing anywhere in zone");
			ZoneBuilderSandbox.PlaceObject("StairsDown", Z);
			return true;
		}

		// Token: 0x06007001 RID: 28673 RVA: 0x002799C4 File Offset: 0x00277BC4
		public void BuildReachableMap(Zone Z, int xp, int yp)
		{
			for (int i = 0; i < 11; i++)
			{
				for (int j = 10 - i; j <= 10 + i; j++)
				{
					for (int k = 10 - i; k <= 10 + i; k++)
					{
						Cell cell = Z.GetCell(k + xp, j + yp);
						if (cell != null && cell.IsEmpty())
						{
							Z.ClearReachableMap(true);
							if (Z.BuildReachableMap(k, j) > 400)
							{
								return;
							}
						}
					}
				}
			}
			for (int l = 0; l < Z.Height; l++)
			{
				for (int m = 0; m < Z.Width; m++)
				{
					if (Z.GetCell(m, l).IsEmpty())
					{
						Z.ClearReachableMap(true);
						if (Z.BuildReachableMap(m, l) > 400)
						{
							return;
						}
					}
				}
			}
		}

		// Token: 0x04003385 RID: 13189
		public string Blueprint;
	}
}
