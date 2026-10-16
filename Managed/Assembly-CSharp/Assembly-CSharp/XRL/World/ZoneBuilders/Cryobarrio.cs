using System;
using Genkit;
using XRL.Rules;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A47 RID: 2631
	public class Cryobarrio
	{
		// Token: 0x06006DD8 RID: 28120 RVA: 0x00252A94 File Offset: 0x00250C94
		public bool BuildZone(Zone Z)
		{
			Maze maze = RecursiveBacktrackerMaze.Generate(10, 3, false, The.Game.GetWorldSeed("CryobarriorMaze" + Z.ZoneID));
			for (int i = 0; i < 15; i++)
			{
				int num = Stat.Random(1, 9);
				int num2 = Stat.Random(0, 2);
				if (25.in100())
				{
					maze.Cell[num, num2].N = true;
				}
				if (25.in100())
				{
					maze.Cell[num, num2].S = true;
				}
				if (25.in100())
				{
					maze.Cell[num, num2].E = true;
				}
				if (25.in100())
				{
					maze.Cell[num, num2].W = true;
				}
			}
			Z.Fill("Limestone");
			bool[,] array = new bool[10, 3];
			for (int j = 0; j < 10; j++)
			{
				for (int k = 0; k < 3; k++)
				{
					array[j, k] = false;
				}
			}
			int num3 = Stat.Random(1, 3);
			int l = 0;
			while (l < num3)
			{
				int num4 = Stat.Random(0, 9);
				int num5 = Stat.Random(0, 2);
				if (!array[num4, num5])
				{
					array[num4, num5] = true;
					l++;
				}
			}
			for (int m = 0; m < 10; m++)
			{
				for (int n = 0; n < 3; n++)
				{
					int num6 = m * 8;
					int num7 = n * 8;
					if (array[m, n])
					{
						Z.ClearBox(new Box(num6, num7, num6 + 7, num7 + 7), null);
						Z.GetCell(num6 + 2, num7 + 3).AddObject("VGlassWall", null, null, null, null);
						Z.GetCell(num6 + 2, num7 + 4).AddObject("VGlassWall", null, null, null, null);
						Z.GetCell(num6 + 5, num7 + 3).AddObject("VGlassWall", null, null, null, null);
						Z.GetCell(num6 + 5, num7 + 4).AddObject("VGlassWall", null, null, null, null);
						Z.GetCell(num6 + 3, num7 + 2).AddObject("HGlassWall", null, null, null, null);
						Z.GetCell(num6 + 4, num7 + 2).AddObject("HGlassWall", null, null, null, null);
						Z.GetCell(num6 + 3, num7 + 5).AddObject("HGlassWall", null, null, null, null);
						Z.GetCell(num6 + 4, num7 + 5).AddObject("HGlassWall", null, null, null, null);
						Z.GetCell(num6 + 2, num7 + 2).AddObject("CryochamberWallSE", null, null, null, null);
						Z.GetCell(num6 + 2, num7 + 5).AddObject("CryochamberWallNE", null, null, null, null);
						Z.GetCell(num6 + 5, num7 + 2).AddObject("CryochamberWallSW", null, null, null, null);
						Z.GetCell(num6 + 5, num7 + 5).AddObject("CryochamberWallNW", null, null, null, null);
						Z.GetCell(num6 + 3, num7 + 3).AddObject("CryoGas", null, null, null, null);
						Z.GetCell(num6 + 3, num7 + 4).AddObject("CryoGas", null, null, null, null);
						Z.GetCell(num6 + 4, num7 + 3).AddObject("CryoGas", null, null, null, null);
						Z.GetCell(num6 + 4, num7 + 4).AddObject("CryoGas", null, null, null, null);
						Z.GetCell(num6 + 3, num7 + 3).AddObject("Equimax", null, null, null, null);
					}
					else
					{
						if (maze.Cell[m, n].N)
						{
							Z.ClearBox(new Box(num6 + 1, num7, num6 + 6, num7 + 6), null);
							Z.GetCell(num6 + 2, num7 + 2).AddObject("Fulcrete", null, null, null, null);
							Z.GetCell(num6 + 5, num7 + 2).AddObject("Fulcrete", null, null, null, null);
						}
						if (maze.Cell[m, n].S)
						{
							Z.ClearBox(new Box(num6 + 1, num7 + 1, num6 + 6, num7 + 7), null);
							Z.GetCell(num6 + 2, num7 + 5).AddObject("Fulcrete", null, null, null, null);
							Z.GetCell(num6 + 5, num7 + 5).AddObject("Fulcrete", null, null, null, null);
						}
						if (maze.Cell[m, n].E)
						{
							Z.ClearBox(new Box(num6 + 1, num7 + 1, num6 + 7, num7 + 6), null);
							Z.GetCell(num6 + 2, num7 + 2).AddObject("Fulcrete", null, null, null, null);
							Z.GetCell(num6 + 2, num7 + 5).AddObject("Fulcrete", null, null, null, null);
						}
						if (maze.Cell[m, n].W)
						{
							Z.ClearBox(new Box(num6, num7 + 1, num6 + 6, num7 + 6), null);
							Z.GetCell(num6 + 5, num7 + 2).AddObject("Fulcrete", null, null, null, null);
							Z.GetCell(num6 + 5, num7 + 5).AddObject("Fulcrete", null, null, null, null);
						}
					}
				}
			}
			Z.ClearReachableMap();
			for (int num8 = 5; num8 < 75; num8++)
			{
				for (int num9 = 5; num9 < 15; num9++)
				{
					if (!Z.GetCell(num8, num9).IsSolid())
					{
						Z.BuildReachableMap(num8, num9);
						return true;
					}
				}
			}
			return true;
		}
	}
}
