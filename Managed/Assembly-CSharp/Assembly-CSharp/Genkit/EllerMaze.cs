using System;

namespace Genkit
{
	// Token: 0x02000387 RID: 903
	public class EllerMaze
	{
		// Token: 0x06002200 RID: 8704 RVA: 0x000AA19A File Offset: 0x000A839A
		public static Maze Generate(int Width, int Height)
		{
			return new Maze(Width, Height, false);
		}
	}
}
