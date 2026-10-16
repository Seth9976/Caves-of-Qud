using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A50 RID: 2640
	[Serializable]
	public class BuildingTemplate : IComposite
	{
		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x06006DED RID: 28141 RVA: 0x00254F1C File Offset: 0x0025311C
		public bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006DEE RID: 28142 RVA: 0x00254F1F File Offset: 0x0025311F
		public BuildingTemplate()
		{
		}

		// Token: 0x06006DEF RID: 28143 RVA: 0x00254F34 File Offset: 0x00253134
		public BuildingTemplate(int _Width, int _Height, BuildingTemplateTile defaultTile = BuildingTemplateTile.Void)
		{
			this.Width = _Width;
			this.Height = _Height;
			this.Map = new BuildingTemplateTile[this.Width, this.Height];
			this.Rooms = new int[this.Width, this.Height];
			for (int i = 0; i < _Width; i++)
			{
				for (int j = 0; j < _Height; j++)
				{
					this.Map[i, j] = defaultTile;
				}
			}
		}

		// Token: 0x06006DF0 RID: 28144 RVA: 0x00254FB4 File Offset: 0x002531B4
		public void Write(SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.Width);
			Writer.WriteOptimized(this.Height);
			for (int i = 0; i < this.Width; i++)
			{
				for (int j = 0; j < this.Height; j++)
				{
					Writer.WriteOptimized((int)this.Map[i, j]);
					Writer.WriteOptimized(this.Rooms[i, j]);
				}
			}
			Writer.WriteComposite<RoomData>(this.RoomList);
		}

		// Token: 0x06006DF1 RID: 28145 RVA: 0x00255030 File Offset: 0x00253230
		public void Read(SerializationReader Reader)
		{
			this.Width = Reader.ReadOptimizedInt32();
			this.Height = Reader.ReadOptimizedInt32();
			this.Map = new BuildingTemplateTile[this.Width, this.Height];
			this.Rooms = new int[this.Width, this.Width];
			for (int i = 0; i < this.Width; i++)
			{
				for (int j = 0; j < this.Height; j++)
				{
					this.Map[i, j] = (BuildingTemplateTile)Reader.ReadOptimizedInt32();
					this.Rooms[i, j] = Reader.ReadOptimizedInt32();
				}
			}
			this.RoomList = Reader.ReadCompositeList<RoomData>();
		}

		// Token: 0x06006DF2 RID: 28146 RVA: 0x002550D8 File Offset: 0x002532D8
		public void AddMap(int StartX, int StartY, BuildingTemplate Source)
		{
			for (int i = 0; i < Source.Height; i++)
			{
				for (int j = 0; j < Source.Width; j++)
				{
					this.Map[j + StartX, i + StartY] = Source.Map[j, i];
					this.Rooms[j + StartX, i + StartY] = Source.Rooms[j, i];
				}
			}
		}

		// Token: 0x06006DF3 RID: 28147 RVA: 0x00255144 File Offset: 0x00253344
		public BuildingTemplate(int _Width, int _Height, int nSquares, bool FullSquare)
		{
			this.Width = _Width;
			this.Height = _Height;
			this.Map = new BuildingTemplateTile[this.Width, this.Height];
			this.Rooms = new int[this.Width, this.Height];
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					this.Map[j, i] = BuildingTemplateTile.Void;
					this.Rooms[j, i] = 0;
				}
			}
			if (FullSquare)
			{
				for (int k = 0; k < this.Width; k++)
				{
					for (int l = 0; l < this.Height; l++)
					{
						this.Map[k, l] = BuildingTemplateTile.Wall;
					}
				}
			}
			else
			{
				for (int m = 0; m < nSquares; m++)
				{
					int num = Stat.Random(1, _Width / 2);
					int num2 = Stat.Random(num + 3, _Width - 2);
					int num3 = Stat.Random(1, _Height / 2);
					int num4 = Stat.Random(num3 + 3, _Height - 2);
					for (int n = num; n <= num2; n++)
					{
						for (int num5 = num3; num5 <= num4; num5++)
						{
							this.Map[n, num5] = BuildingTemplateTile.Wall;
						}
					}
				}
			}
			for (int num6 = 0; num6 < this.Width; num6++)
			{
				this.FloodOutsideWall(num6, 0);
				this.FloodOutsideWall(num6, this.Height - 1);
			}
			for (int num7 = 0; num7 < this.Height; num7++)
			{
				this.FloodOutsideWall(0, num7);
				this.FloodOutsideWall(this.Width - 1, num7);
			}
			for (int num8 = 1; num8 < this.Width - 1; num8++)
			{
				for (int num9 = 1; num9 < this.Height - 1; num9++)
				{
					if (this.Map[num8, num9] == BuildingTemplateTile.Wall && this.Map[num8 - 1, num9] == BuildingTemplateTile.Wall && this.Map[num8 + 1, num9] == BuildingTemplateTile.Wall && this.Map[num8, num9 - 1] == BuildingTemplateTile.Wall && this.Map[num8, num9 + 1] == BuildingTemplateTile.Wall)
					{
						this.FloodInsideWall(num8, num9);
					}
				}
			}
			int num10 = Stat.Random(1, 2);
			if (Stat.Random(0, 100) > 90)
			{
				num10 += Stat.Random(1, 2);
			}
			int num11 = this.Width * this.Height / num10;
			int num12 = Stat.Random(0, num11);
			bool flag = false;
			for (int num13 = 0; num13 < this.Width; num13++)
			{
				for (int num14 = 0; num14 < this.Height; num14++)
				{
					num12++;
					if (num12 >= num11)
					{
						flag = true;
						num12 = 0;
					}
					if (this.Map[num13, num14] == BuildingTemplateTile.OutsideWall && flag)
					{
						this.Map[num13, num14] = BuildingTemplateTile.Door;
						flag = false;
					}
				}
			}
			for (int num15 = 0; num15 < this.Height; num15++)
			{
				for (int num16 = 0; num16 < this.Width; num16++)
				{
					if (this.Map[num16, num15] == BuildingTemplateTile.Inside)
					{
						this.FillRoom(num16, num15);
						break;
					}
				}
			}
			for (int num17 = 0; num17 < this.Width; num17++)
			{
				for (int num18 = 0; num18 < this.Height; num18++)
				{
					this.Rooms[num17, num18] = 0;
				}
			}
			int num19 = 0;
			for (int num20 = 0; num20 < this.Width; num20++)
			{
				for (int num21 = 0; num21 < this.Height; num21++)
				{
					if (this.Rooms[num20, num21] == 0 && this.Map[num20, num21] == BuildingTemplateTile.Inside)
					{
						RoomData roomData = new RoomData();
						roomData.Room = new int[this.Width, this.Height];
						this.FloodRoom(num20, num21, roomData, num19 + 1);
						this.RoomList.Add(roomData);
						num19++;
					}
				}
			}
		}

		// Token: 0x06006DF4 RID: 28148 RVA: 0x00255548 File Offset: 0x00253748
		private void FillRoom(int x, int y)
		{
			RoomData roomData = new RoomData();
			roomData.Room = new int[this.Width, this.Height];
			for (int i = 0; i < this.Width; i++)
			{
				for (int j = 0; j < this.Height; j++)
				{
					roomData.Room[i, j] = 0;
				}
			}
			this.FloodRoom(x, y, roomData, 1);
			roomData.Width = roomData.Right - roomData.Left;
			roomData.Height = roomData.Bottom - roomData.Top;
			if (roomData.Size <= 24)
			{
				return;
			}
			if (roomData.Width <= 3)
			{
				return;
			}
			if (roomData.Height <= 3)
			{
				return;
			}
			int num;
			if (roomData.Width < roomData.Height)
			{
				num = 1;
			}
			else
			{
				num = 0;
			}
			if (Stat.Random(1, 4) == 4)
			{
				num = 1 - num;
			}
			if (num == 0)
			{
				int num2 = 0;
				int num3 = Stat.Random(roomData.Left + 2, roomData.Right - 2);
				while (num2 < 10 && (this.Map[num3 - 1, roomData.Top] != BuildingTemplateTile.Inside || this.Map[num3 + 1, roomData.Top] != BuildingTemplateTile.Inside || this.Map[num3 - 1, roomData.Bottom] != BuildingTemplateTile.Inside || this.Map[num3 + 1, roomData.Bottom] != BuildingTemplateTile.Inside))
				{
					num2++;
					num3 = Stat.Random(roomData.Left + 2, roomData.Right - 2);
				}
				if (num2 == 10)
				{
					return;
				}
				for (int k = roomData.Top; k <= roomData.Bottom; k++)
				{
					if (roomData.Room[num3, k] == 1)
					{
						this.Map[num3, k] = BuildingTemplateTile.Wall;
					}
				}
				num2 = 0;
				int num4 = Stat.Random(roomData.Top + 2, roomData.Bottom - 2);
				while (num2 < 10 && this.Map[num3, num4] != BuildingTemplateTile.Wall)
				{
					num2++;
					num4 = Stat.Random(roomData.Top + 2, roomData.Bottom - 2);
				}
				if (num2 == 10)
				{
					return;
				}
				this.Map[num3, num4] = BuildingTemplateTile.Door;
				this.FillRoom(num3 - 1, num4);
				this.FillRoom(num3 + 1, num4);
			}
			if (num == 1)
			{
				int num5 = 0;
				int num3 = Stat.Random(roomData.Top + 2, roomData.Bottom - 2);
				while (num5 < 10 && (this.Map[roomData.Left, num3 - 1] != BuildingTemplateTile.Inside || this.Map[roomData.Left, num3 + 1] != BuildingTemplateTile.Inside || this.Map[roomData.Right, num3 - 1] != BuildingTemplateTile.Inside || this.Map[roomData.Right, num3 + 1] != BuildingTemplateTile.Inside))
				{
					num5++;
					num3 = Stat.Random(roomData.Top + 2, roomData.Bottom - 2);
				}
				if (num5 == 10)
				{
					return;
				}
				for (int l = roomData.Left; l <= roomData.Right; l++)
				{
					if (roomData.Room[l, num3] == 1)
					{
						this.Map[l, num3] = BuildingTemplateTile.Wall;
					}
				}
				num5 = 0;
				int num6 = Stat.Random(roomData.Left + 2, roomData.Right - 2);
				while (num5 < 10 && this.Map[num6, num3] != BuildingTemplateTile.Wall)
				{
					num5++;
					num6 = Stat.Random(roomData.Left + 2, roomData.Right - 2);
				}
				if (num5 == 10)
				{
					return;
				}
				this.Map[num6, num3] = BuildingTemplateTile.Door;
				this.FillRoom(num6, num3 - 1);
				this.FillRoom(num6, num3 + 1);
			}
		}

		// Token: 0x06006DF5 RID: 28149 RVA: 0x002558D4 File Offset: 0x00253AD4
		private void FloodRoom(int x, int y, RoomData Data, int n)
		{
			if (x < 0 || x >= this.Width)
			{
				return;
			}
			if (y < 0 || y >= this.Height)
			{
				return;
			}
			if (Data.Room[x, y] != 0)
			{
				return;
			}
			if (this.Map[x, y] != BuildingTemplateTile.Inside)
			{
				return;
			}
			if (x < Data.Left)
			{
				Data.Left = x;
			}
			if (x > Data.Right)
			{
				Data.Right = x;
			}
			if (y < Data.Top)
			{
				Data.Top = y;
			}
			if (y > Data.Bottom)
			{
				Data.Bottom = y;
			}
			this.Rooms[x, y] = n;
			Data.Room[x, y] = n;
			Data.Size++;
			this.FloodRoom(x - 1, y, Data, n);
			this.FloodRoom(x + 1, y, Data, n);
			this.FloodRoom(x, y - 1, Data, n);
			this.FloodRoom(x, y + 1, Data, n);
		}

		// Token: 0x06006DF6 RID: 28150 RVA: 0x002559C0 File Offset: 0x00253BC0
		private void FloodInsideWall(int xp, int yp)
		{
			if (xp < 0)
			{
				return;
			}
			if (xp >= this.Width)
			{
				return;
			}
			if (yp < 0)
			{
				return;
			}
			if (yp >= this.Height)
			{
				return;
			}
			if (this.Rooms[xp, yp] == 2 || this.Rooms[xp, yp] == 3)
			{
				return;
			}
			if (this.Map[xp, yp] == BuildingTemplateTile.OutsideWall)
			{
				return;
			}
			if (this.Rooms[xp, yp] > 0 && this.Map[xp, yp] <= BuildingTemplateTile.Void)
			{
				this.Rooms[xp, yp] = 2;
				return;
			}
			this.Rooms[xp, yp] = 3;
			this.Map[xp, yp] = BuildingTemplateTile.Inside;
			this.FloodInsideWall(xp - 1, yp - 1);
			this.FloodInsideWall(xp - 1, yp);
			this.FloodInsideWall(xp - 1, yp + 1);
			this.FloodInsideWall(xp, yp - 1);
			this.FloodInsideWall(xp, yp + 1);
			this.FloodInsideWall(xp + 1, yp - 1);
			this.FloodInsideWall(xp + 1, yp);
			this.FloodInsideWall(xp + 1, yp + 1);
		}

		// Token: 0x06006DF7 RID: 28151 RVA: 0x00255AC0 File Offset: 0x00253CC0
		private void FloodOutsideWall(int xp, int yp)
		{
			if (xp < 0)
			{
				return;
			}
			if (xp >= this.Width)
			{
				return;
			}
			if (yp < 0)
			{
				return;
			}
			if (yp >= this.Height)
			{
				return;
			}
			if (this.Map[xp, yp] == BuildingTemplateTile.OutsideWall || this.Map[xp, yp] == BuildingTemplateTile.Outside)
			{
				return;
			}
			if (this.Map[xp, yp] == BuildingTemplateTile.Wall)
			{
				this.Map[xp, yp] = BuildingTemplateTile.OutsideWall;
				return;
			}
			this.Map[xp, yp] = BuildingTemplateTile.Outside;
			this.FloodOutsideWall(xp - 1, yp - 1);
			this.FloodOutsideWall(xp - 1, yp);
			this.FloodOutsideWall(xp - 1, yp + 1);
			this.FloodOutsideWall(xp, yp - 1);
			this.FloodOutsideWall(xp, yp + 1);
			this.FloodOutsideWall(xp + 1, yp - 1);
			this.FloodOutsideWall(xp + 1, yp);
			this.FloodOutsideWall(xp + 1, yp + 1);
		}

		// Token: 0x06006DF8 RID: 28152 RVA: 0x00255B94 File Offset: 0x00253D94
		public void Draw(RoomData Data)
		{
			Popup._ScreenBuffer.Clear();
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					if (Data != null && Data.Room[j, i] > 0)
					{
						Popup._ScreenBuffer.Goto(j, i);
						Popup._ScreenBuffer.Write((short)(49 + Data.Room[j, i]));
					}
					if (this.Map[j, i] == BuildingTemplateTile.Inside)
					{
						Popup._ScreenBuffer.Goto(j, i);
						Popup._ScreenBuffer.Write(58);
					}
					if (this.Map[j, i] == BuildingTemplateTile.Void)
					{
						Popup._ScreenBuffer.Goto(j, i);
						Popup._ScreenBuffer.Write(88);
					}
					if (this.Map[j, i] == BuildingTemplateTile.Outside)
					{
						Popup._ScreenBuffer.Goto(j, i);
						Popup._ScreenBuffer.Write(44);
					}
					if (this.Map[j, i] == BuildingTemplateTile.OutsideWall)
					{
						Popup._ScreenBuffer.Goto(j, i);
						Popup._ScreenBuffer.Write(35);
					}
					if (this.Map[j, i] == BuildingTemplateTile.Wall)
					{
						Popup._ScreenBuffer.Goto(j, i);
						Popup._ScreenBuffer.Write(42);
					}
					if (this.Map[j, i] == BuildingTemplateTile.Door)
					{
						Popup._ScreenBuffer.Goto(j, i);
						Popup._ScreenBuffer.Write(43);
					}
					if (this.Rooms[j, i] > 0)
					{
						Popup._ScreenBuffer.Goto(j, i);
						Popup._ScreenBuffer.Write((short)(65 + this.Rooms[j, i]));
					}
				}
				Popup._TextConsole.DrawBuffer(Popup._ScreenBuffer, null, false);
			}
			Keyboard.getch();
		}

		// Token: 0x04003258 RID: 12888
		public int Width;

		// Token: 0x04003259 RID: 12889
		public int Height;

		// Token: 0x0400325A RID: 12890
		public int[,] Rooms;

		// Token: 0x0400325B RID: 12891
		public BuildingTemplateTile[,] Map;

		// Token: 0x0400325C RID: 12892
		public List<RoomData> RoomList = new List<RoomData>();
	}
}
