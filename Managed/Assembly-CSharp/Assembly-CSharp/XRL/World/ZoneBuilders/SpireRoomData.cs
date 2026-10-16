using System;
using System.Collections.Generic;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AE9 RID: 2793
	[Serializable]
	public class SpireRoomData
	{
		// Token: 0x06007008 RID: 28680 RVA: 0x00279DCC File Offset: 0x00277FCC
		public SpireRoomData(int Width, int Height)
		{
			this.LeftAt = new int[Height];
			this.RightAt = new int[Height];
			this.TopAt = new int[Width];
			this.BottomAt = new int[Width];
			this.Room = new int[Width, Height];
			for (int i = 0; i < Height; i++)
			{
				for (int j = 0; j < Width; j++)
				{
					this.LeftAt[i] = 999;
					this.RightAt[i] = 0;
					this.TopAt[j] = 999;
					this.BottomAt[j] = 0;
					this.Room[j, i] = 0;
				}
			}
		}

		// Token: 0x04003394 RID: 13204
		public int[] LeftAt;

		// Token: 0x04003395 RID: 13205
		public int[] RightAt;

		// Token: 0x04003396 RID: 13206
		public int[] TopAt;

		// Token: 0x04003397 RID: 13207
		public int[] BottomAt;

		// Token: 0x04003398 RID: 13208
		public List<Point> Doors = new List<Point>();

		// Token: 0x04003399 RID: 13209
		public int Left = 999;

		// Token: 0x0400339A RID: 13210
		public int Right;

		// Token: 0x0400339B RID: 13211
		public int Top = 999;

		// Token: 0x0400339C RID: 13212
		public int Bottom;

		// Token: 0x0400339D RID: 13213
		public int Width;

		// Token: 0x0400339E RID: 13214
		public int Height;

		// Token: 0x0400339F RID: 13215
		public int Size;

		// Token: 0x040033A0 RID: 13216
		public int[,] Room;
	}
}
