using System;
using XRL.Rules;
using XRL.World.ZoneBuilders.Utility;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A44 RID: 2628
	public class BathPools
	{
		// Token: 0x06006DD2 RID: 28114 RVA: 0x0025206C File Offset: 0x0025026C
		public bool BuildZone(Zone Z)
		{
			int num = 10;
			int num2 = 3;
			int num3 = 60;
			int num4 = 18;
			Z.FillBox(new Box(0, 0, Z.Width - 1, Z.Height - 1), "Marble", false);
			Z.ClearBox(new Box(num, num2, num + num3, num2 + num4), null);
			NoiseMap noiseMap = new NoiseMap(num3, num4, 10, 1, 1, Stat.Random(40, 50), Stat.Random(50, 70), Stat.Random(125, 135), 0, 10, 0, 1, null, 5);
			for (int i = 0; i < num3; i++)
			{
				for (int j = 0; j < num4; j++)
				{
					int num5 = i;
					int num6 = j;
					if (i >= num3 / 2)
					{
						num5 = num3 - i;
					}
					if (j >= num4 / 2)
					{
						num6 = num4 - j;
					}
					if ((float)noiseMap.Noise[num5, num6] > 0.5f)
					{
						bool flag = true;
						for (int k = -1; k <= 1; k++)
						{
							for (int l = -1; l <= 1; l++)
							{
								if (num5 + k > 0 && num5 + k < 80 && num6 + l > 0 && num6 + l < 24 && (l != 0 || k != 0) && (float)noiseMap.Noise[num5 + k, num6 + l] <= 0.5f)
								{
									flag = false;
									goto IL_0131;
								}
							}
						}
						IL_0131:
						if (flag)
						{
							Z.GetCell(i + num, j + num2).Clear(null, false, false, null);
							Z.GetCell(i + num, j + num2).AddObject("ConvalessenceDeepPool", null, null, null, null);
						}
						else
						{
							Z.GetCell(i + num, j + num2).Clear(null, false, false, null);
							Z.GetCell(i + num, j + num2).AddObject("CyanMarbleWalkway", null, null, null, null);
						}
					}
				}
			}
			int num7 = 0;
			int num8 = 0;
			while (num7 < 1000 && num8 < 5)
			{
				int num9 = Stat.Random(4, num3 / 2 - 1);
				int num10 = Stat.Random(3, num4 / 2 - 1);
				bool flag2 = false;
				for (int m = -1; m <= 1; m++)
				{
					for (int n = -1; n <= 1; n++)
					{
						if (num9 + m > 0 && num9 + m < num3 && num10 + n > 0 && num10 + n < num4 && (float)noiseMap.Noise[num9 + m, num10 + n] > 0.5f)
						{
							flag2 = true;
							goto IL_0248;
						}
					}
				}
				IL_0248:
				if (!flag2)
				{
					num8++;
					Z.GetCell(num9 + num, num10 + num2).AddObject("Marble", null, null, null, null);
					Z.GetCell(num9 + num, num4 - num10 + num2).AddObject("Marble", null, null, null, null);
					Z.GetCell(num3 - num9 + num, num10 + num2).AddObject("Marble", null, null, null, null);
					Z.GetCell(num3 - num9 + num, num4 - num10 + num2).AddObject("Marble", null, null, null, null);
				}
				num7++;
			}
			Box box = new Box(num - 1, num2 + num4 - 3, num + 3, num2 + num4 + 1);
			Box box2 = new Box(num + num3 - 3, num2 + num4 - 3, num + num3 + 1, num2 + num4 + 1);
			if (Z.Z % 2 == 0)
			{
				Z.ClearBox(box, null);
			}
			else
			{
				Z.ClearBox(box2, null);
			}
			Point point = new Point(num + 3, num2 + num4 - 1);
			Point point2 = new Point(num + num3 - 3, num2 + num4 - 1);
			Point point3 = new Point(num + 1, num2 + num4 - 1);
			Point point4 = new Point(num + num3 - 1, num2 + num4 - 1);
			Z.GetCell(point).Clear(null, false, false, null);
			Z.GetCell(point2).Clear(null, false, false, null);
			for (int num11 = box.y1; num11 <= box.y2; num11++)
			{
				for (int num12 = box.x1; num12 <= box.x2; num12++)
				{
					if ((num12 + num11) % 2 == 0)
					{
						Z.GetCell(num12, num11).AddObject("WhiteTile", null, null, null, null);
					}
					else
					{
						Z.GetCell(num12, num11).AddObject("GreenTile", null, null, null, null);
					}
				}
			}
			for (int num13 = box2.y1; num13 <= box2.y2; num13++)
			{
				for (int num14 = box2.x1; num14 <= box2.x2; num14++)
				{
					if ((num14 + num13) % 2 == 0)
					{
						Z.GetCell(num14, num13).AddObject("WhiteTile", null, null, null, null);
					}
					else
					{
						Z.GetCell(num14, num13).AddObject("GreenTile", null, null, null, null);
					}
				}
			}
			if (Z.Z % 2 == 0)
			{
				Z.FillHollowBox(box, "Marble");
			}
			if (Z.Z % 2 != 0)
			{
				Z.FillHollowBox(box2, "Marble");
			}
			string text = "Door";
			string text2 = "Door";
			if (Z.Z == 12)
			{
				text2 = "Troll Door 1";
			}
			if (Z.Z == 13)
			{
				text = "Troll Door 2";
			}
			if (Z.Z == 14)
			{
				text2 = "Troll Door 3";
			}
			if (text2 != "Door")
			{
				Z.GetCell(point).Clear(null, false, false, null);
				Z.GetCell(point).AddObject(text2, null, null, null, null);
			}
			if (text != "Door")
			{
				Z.GetCell(point2).Clear(null, false, false, null);
				Z.GetCell(point2).AddObject(text, null, null, null, null);
			}
			if (Z.Z % 2 == 0)
			{
				Z.GetCell(point3).AddObject("StairsDown", null, null, null, null);
				Z.GetCell(point4).AddObject("StairsUp", null, null, null, null);
			}
			else
			{
				Z.GetCell(point3).AddObject("StairsUp", null, null, null, null);
				Z.GetCell(point4).AddObject("StairsDown", null, null, null, null);
			}
			Z.ClearReachableMap();
			for (int num15 = 10; num15 < 20; num15++)
			{
				for (int num16 = 10; num16 < 20; num16++)
				{
					if (Z.GetCell(num15, num16).IsEmpty())
					{
						Z.BuildReachableMap(num15, num16);
						return true;
					}
				}
			}
			return true;
		}
	}
}
