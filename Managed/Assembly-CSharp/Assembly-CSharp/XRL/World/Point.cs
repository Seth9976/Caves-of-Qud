using System;
using Genkit;

namespace XRL.World
{
	// Token: 0x0200097D RID: 2429
	[Serializable]
	public class Point
	{
		// Token: 0x060066DA RID: 26330 RVA: 0x001FFB32 File Offset: 0x001FDD32
		public Point(int x, int y)
		{
			this.X = x;
			this.Y = y;
			this.Direction = 0;
			this.DisplayChar = ' ';
		}

		// Token: 0x060066DB RID: 26331 RVA: 0x001FFB57 File Offset: 0x001FDD57
		public Point(int x, int y, int direction, char displaychar)
		{
			this.X = x;
			this.Y = y;
			this.Direction = direction;
			this.DisplayChar = displaychar;
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x060066DC RID: 26332 RVA: 0x001FFB7C File Offset: 0x001FDD7C
		public Location2D location
		{
			get
			{
				return Location2D.Get(this.X, this.Y);
			}
		}

		// Token: 0x04002F60 RID: 12128
		public int X;

		// Token: 0x04002F61 RID: 12129
		public int Y;

		// Token: 0x04002F62 RID: 12130
		public int Direction;

		// Token: 0x04002F63 RID: 12131
		public char DisplayChar;
	}
}
