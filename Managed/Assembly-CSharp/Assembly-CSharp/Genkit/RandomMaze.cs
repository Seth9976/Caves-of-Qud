using System;

namespace Genkit
{
	// Token: 0x02000384 RID: 900
	public class RandomMaze
	{
		// Token: 0x060021F0 RID: 8688 RVA: 0x000A925C File Offset: 0x000A745C
		public static Maze Generate(int Width, int Height, int Seed)
		{
			Maze maze = new Maze(Width, Height, false);
			Random random = new Random(Seed);
			for (int i = 0; i < Width; i++)
			{
				for (int j = 0; j < Height; j++)
				{
					maze.Cell[i, j].N = random.Next(0, 2) == 0;
					maze.Cell[i, j].S = random.Next(0, 2) == 0;
					maze.Cell[i, j].E = random.Next(0, 2) == 0;
					maze.Cell[i, j].W = random.Next(0, 2) == 0;
				}
			}
			return maze;
		}
	}
}
