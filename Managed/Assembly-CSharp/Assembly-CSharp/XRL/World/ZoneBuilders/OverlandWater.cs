using System;
using System.Collections.Generic;
using XRL.Core;
using XRL.Rules;
using XRL.World.ZoneBuilders.Utility;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000ABD RID: 2749
	public class OverlandWater
	{
		// Token: 0x06006F7D RID: 28541 RVA: 0x002712D0 File Offset: 0x0026F4D0
		public string ZoneIDFromXY(string World, int xp, int yp)
		{
			int num = (int)Math.Floor((double)((float)xp / 3f));
			int num2 = (int)Math.Floor((double)((float)yp / 3f));
			return ZoneID.Assemble(World, num, num2, xp % 3, yp % 3, 10);
		}

		// Token: 0x06006F7E RID: 28542 RVA: 0x00271310 File Offset: 0x0026F510
		public bool[,] GetWatermap(int Wx, int Wy)
		{
			if (Wy < 0)
			{
				bool[,] array = new bool[3, 3];
				for (int i = 0; i < 3; i++)
				{
					for (int j = 0; j < 3; j++)
					{
						array[j, i] = true;
					}
				}
				return array;
			}
			bool[,] array2 = new bool[3, 3];
			Zone zone = XRLCore.Core.Game.ZoneManager.GetZone("JoppaWorld");
			for (int k = -1; k <= 1; k++)
			{
				for (int l = -1; l <= 1; l++)
				{
					if (Wy + k >= 0)
					{
						Cell cell = zone.GetCell(Wx + l, Wy + k);
						array2[l + 1, k + 1] = cell.HasObjectWithPropertyOrTagEqualToValue("Terrain", "Water");
					}
					else
					{
						array2[l + 1, k + 1] = true;
					}
				}
			}
			bool[,] array3 = new bool[3, 3];
			array3[0, 0] = array2[0, 0] && array2[1, 0] && array2[0, 1];
			array3[1, 0] = array2[1, 0];
			array3[2, 0] = array2[1, 0] && array2[2, 0] && array2[2, 1];
			array3[0, 1] = array2[0, 1];
			array3[1, 1] = true;
			array3[2, 1] = array2[2, 1];
			array3[0, 2] = array2[0, 1] && array2[0, 2] && array2[1, 2];
			array3[1, 2] = array2[1, 2];
			array3[2, 2] = array2[2, 1] && array2[2, 2] && array2[1, 2];
			return array3;
		}

		// Token: 0x06006F7F RID: 28543 RVA: 0x002714D4 File Offset: 0x0026F6D4
		public bool BuildZone(Zone Z)
		{
			if (Z._ZoneID.Contains("JoppaWorld"))
			{
				int num = Z.wX;
				int num2 = Z.wY;
				bool[,] array = new bool[9, 9];
				for (int i = -1; i <= 1; i++)
				{
					for (int j = -1; j <= 1; j++)
					{
						bool[,] watermap = this.GetWatermap(num + i, num2 + j);
						for (int k = 0; k < 3; k++)
						{
							for (int l = 0; l < 3; l++)
							{
								array[k + (i + 1) * 3, l + (j + 1) * 3] = watermap[k, l];
							}
						}
					}
				}
				bool[,] array2 = new bool[3, 3];
				for (int m = 0; m < 3; m++)
				{
					for (int n = 0; n < 3; n++)
					{
						array2[n, m] = false;
					}
				}
				num = Z.X + 3;
				num2 = Z.Y + 3;
				if (array[num, num2])
				{
					if (array[num - 1, num2 - 1] && array[num - 1, num2] && array[num, num2 - 1])
					{
						array2[0, 0] = true;
					}
					if (array[num, num2 - 1])
					{
						array2[1, 0] = true;
					}
					if (array[num + 1, num2 - 1] && array[num + 1, num2] && array[num, num2 - 1])
					{
						array2[2, 0] = true;
					}
					if (array[num - 1, num2])
					{
						array2[0, 1] = true;
					}
					array2[1, 1] = true;
					if (array[num + 1, num2])
					{
						array2[2, 1] = true;
					}
					if (array[num - 1, num2 + 1] && array[num - 1, num2] && array[num, num2 + 1])
					{
						array2[0, 2] = true;
					}
					if (array[num, num2 + 1])
					{
						array2[1, 2] = true;
					}
					if (array[num + 1, num2 + 1] && array[num + 1, num2] && array[num, num2 + 1])
					{
						array2[2, 2] = true;
					}
					for (int num3 = 0; num3 < 3; num3++)
					{
						for (int num4 = 0; num4 < 3; num4++)
						{
							int num5 = 0;
							int num6 = 0;
							int num7 = 0;
							int num8 = 0;
							if (num3 == 0)
							{
								num5 = 0;
								num6 = 26;
							}
							if (num3 == 1)
							{
								num5 = 27;
								num6 = 54;
							}
							if (num3 == 2)
							{
								num5 = 55;
								num6 = 79;
							}
							if (num4 == 0)
							{
								num7 = 0;
								num8 = 7;
							}
							if (num4 == 1)
							{
								num7 = 8;
								num8 = 17;
							}
							if (num4 == 2)
							{
								num7 = 17;
								num8 = 24;
							}
							if (array2[num3, num4] && Z.GetCell(num3, num4).IsPassable(null, true))
							{
								for (int num9 = num5; num9 <= num6; num9++)
								{
									for (int num10 = num7; num10 <= num8; num10++)
									{
										Z.GetCell(num9, num10).AddObject("SaltyWaterExtraDeepPool", null, null, null, null);
									}
								}
							}
						}
					}
				}
			}
			List<NoiseMapNode> list = new List<NoiseMapNode>();
			NoiseMap noiseMap;
			if (!this.Underground)
			{
				noiseMap = new NoiseMap(Z.Width, Z.Height, 10, 1, 1, Stat.Random(16, 36), 40, 225, 0, 10, 0, 1, list, 5);
				for (int num11 = 0; num11 < Z.Height; num11++)
				{
					for (int num12 = 0; num12 < Z.Width; num12++)
					{
						if ((double)noiseMap.Noise[num12, num11] > 0.01)
						{
							Z.GetCell(num12, num11).Clear(null, false, false, null);
						}
					}
				}
			}
			noiseMap = new NoiseMap(Z.Width, Z.Height, 10, 1, 1, Stat.Random(6, 26), 40, 225, 0, 10, 0, 1, list, 5);
			for (int num13 = 0; num13 < Z.Height; num13++)
			{
				for (int num14 = 0; num14 < Z.Width; num14++)
				{
					if ((double)noiseMap.Noise[num14, num13] > 0.01)
					{
						Z.GetCell(num14, num13).AddObject("SaltyWaterDeepPool", null, null, null, null);
					}
				}
			}
			Z.GetCell(0, 0).AddObject("Grassy", null, null, null, null);
			return true;
		}

		// Token: 0x04003324 RID: 13092
		public bool Underground;
	}
}
