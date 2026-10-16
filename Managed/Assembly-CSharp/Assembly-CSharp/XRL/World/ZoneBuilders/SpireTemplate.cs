using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AEA RID: 2794
	[Serializable]
	public class SpireTemplate
	{
		// Token: 0x06007009 RID: 28681 RVA: 0x00279E90 File Offset: 0x00278090
		public SpireTemplate(SpireTemplate Source)
		{
			this.Width = Source.Width;
			this.Height = Source.Height;
			this.Map = new SpireTemplateTile[this.Width, this.Height];
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					this.Map[j, i] = Source.Map[j, i];
				}
			}
			this.Rooms = new int[this.Width, this.Height];
		}

		// Token: 0x0600700A RID: 28682 RVA: 0x00279F30 File Offset: 0x00278130
		public SpireTemplate(int Width, int Height)
		{
			this.Width = Width;
			this.Height = Height;
			this.Map = new SpireTemplateTile[Width, Height];
			this.Rooms = new int[Width, Height];
		}

		// Token: 0x0600700B RID: 28683 RVA: 0x00279F6C File Offset: 0x0027816C
		public SpireTemplate(int _Width, int _Height, int nSquares)
		{
			this.Width = _Width;
			this.Height = _Height;
			this.Map = new SpireTemplateTile[this.Width, this.Height];
			this.Rooms = new int[this.Width, this.Height];
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					this.Map[j, i] = SpireTemplateTile.Void;
					this.Rooms[j, i] = 0;
				}
			}
			for (int k = 0; k < nSquares; k++)
			{
				int num = Stat.Random(9, _Width / 2);
				int num2 = Stat.Random(num + 3, _Width - 10);
				int num3 = Stat.Random(4, _Height / 2);
				int num4 = Stat.Random(num3 + 3, _Height - 5);
				for (int l = num; l <= num2; l++)
				{
					for (int m = num3; m <= num4; m++)
					{
						this.Map[l, m] = SpireTemplateTile.Wall;
					}
				}
			}
			for (int n = 0; n < this.Width; n++)
			{
				this.FloodOutsideWall(n, 0);
				this.FloodOutsideWall(n, this.Height - 1);
			}
			for (int num5 = 0; num5 < this.Height; num5++)
			{
				this.FloodOutsideWall(0, num5);
				this.FloodOutsideWall(this.Width - 1, num5);
			}
			for (int num6 = 1; num6 < this.Width - 1; num6++)
			{
				for (int num7 = 1; num7 < this.Height - 1; num7++)
				{
					if (this.Map[num6, num7] == SpireTemplateTile.Wall && this.Map[num6 - 1, num7] == SpireTemplateTile.Wall && this.Map[num6 + 1, num7] == SpireTemplateTile.Wall && this.Map[num6, num7 - 1] == SpireTemplateTile.Wall && this.Map[num6, num7 + 1] == SpireTemplateTile.Wall)
					{
						this.FloodInsideWall(num6, num7);
					}
				}
			}
			int num8 = Stat.Random(1, 2);
			if (10.in100())
			{
				num8 += Stat.Random(1, 2);
			}
			int num9 = this.Width * this.Height / num8;
			int num10 = Stat.Random(0, num9);
			bool flag = false;
			for (int num11 = 0; num11 < this.Width; num11++)
			{
				for (int num12 = 0; num12 < this.Height; num12++)
				{
					num10++;
					if (num10 >= num9)
					{
						flag = true;
						num10 = 0;
					}
					if (this.Map[num11, num12] == SpireTemplateTile.OutsideWall && flag)
					{
						this.Map[num11, num12] = SpireTemplateTile.Door;
						flag = false;
					}
				}
			}
		}

		// Token: 0x0600700C RID: 28684 RVA: 0x0027A214 File Offset: 0x00278414
		public void AddMap(int StartX, int StartY, SpireTemplate Source)
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

		// Token: 0x0600700D RID: 28685 RVA: 0x0027A280 File Offset: 0x00278480
		public void GenerateRooms(int Depth)
		{
			this.MaxDepth = "2d4".RollCached();
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					if (this.Map[j, i] == SpireTemplateTile.OutsideWall)
					{
						this.Map[j, i] = SpireTemplateTile.Wall;
					}
				}
			}
			for (int k = 0; k < this.Height; k++)
			{
				for (int l = 0; l < this.Width; l++)
				{
					if (this.Map[l, k] == SpireTemplateTile.Inside)
					{
						this.FillRoom(l, k, 0);
						break;
					}
				}
			}
			for (int m = 0; m < this.Width; m++)
			{
				for (int n = 0; n < this.Height; n++)
				{
					this.Rooms[m, n] = 0;
				}
			}
			int num = 0;
			short num2 = 0;
			while ((int)num2 < this.Width)
			{
				short num3 = 0;
				while ((int)num3 < this.Height)
				{
					if (this.Rooms[(int)num2, (int)num3] == 0 && this.Map[(int)num2, (int)num3] == SpireTemplateTile.Inside)
					{
						SpireRoomData spireRoomData = new SpireRoomData(this.Width, this.Height);
						this.FloodRoom(num2, num3, spireRoomData, (short)(num + 1));
						this.RoomList.Add(spireRoomData);
						num++;
					}
					num3 += 1;
				}
				num2 += 1;
			}
			int num4 = 20;
			int num5 = 20 + 3 * Depth;
			for (int num6 = 0; num6 < num4; num6++)
			{
				int num7 = 100;
				while (num7 > 0)
				{
					int num8 = Stat.Random(0, this.Width - 1);
					int num9 = Stat.Random(0, this.Height - 1);
					if (this.DoorPositionOk(num8, num9))
					{
						if (num5.in100())
						{
							this.Map[num8, num9] = SpireTemplateTile.SecurityDoor;
							break;
						}
						this.Map[num8, num9] = SpireTemplateTile.Door;
						break;
					}
					else
					{
						num7--;
					}
				}
			}
			foreach (SpireRoomData spireRoomData2 in this.RoomList)
			{
				if (spireRoomData2.Doors.Count == 1)
				{
					for (int num10 = 0; num10 < this.Width; num10++)
					{
						for (int num11 = 0; num11 < this.Height; num11++)
						{
							if (spireRoomData2.Room[num10, num11] != 0)
							{
								this.Map[num10, num11] = SpireTemplateTile.InsideSealed;
							}
						}
					}
					this.Map[spireRoomData2.Doors[0].X, spireRoomData2.Doors[0].Y] = SpireTemplateTile.SecurityDoor;
				}
			}
		}

		// Token: 0x0600700E RID: 28686 RVA: 0x0027A544 File Offset: 0x00278744
		private bool DoorPositionOk(int x, int y)
		{
			if (x < 1)
			{
				return false;
			}
			if (x > this.Width - 2)
			{
				return false;
			}
			if (y < 1)
			{
				return false;
			}
			if (y > this.Height - 2)
			{
				return false;
			}
			if (this.Map[x, y] != SpireTemplateTile.Wall)
			{
				return false;
			}
			if (this.Map[x + 1, y] == SpireTemplateTile.Wall)
			{
				if (this.Map[x, y + 1] == SpireTemplateTile.Wall)
				{
					return false;
				}
				if (this.Map[x, y - 1] == SpireTemplateTile.Wall)
				{
					return false;
				}
			}
			if (this.Map[x - 1, y] == SpireTemplateTile.Wall)
			{
				if (this.Map[x, y + 1] == SpireTemplateTile.Wall)
				{
					return false;
				}
				if (this.Map[x, y - 1] == SpireTemplateTile.Wall)
				{
					return false;
				}
			}
			if (this.Map[x, y - 1] == SpireTemplateTile.Wall)
			{
				if (this.Map[x - 1, y] == SpireTemplateTile.Wall)
				{
					return false;
				}
				if (this.Map[x + 1, y] == SpireTemplateTile.Wall)
				{
					return false;
				}
			}
			if (this.Map[x, y + 1] == SpireTemplateTile.Wall)
			{
				if (this.Map[x - 1, y] == SpireTemplateTile.Wall)
				{
					return false;
				}
				if (this.Map[x + 1, y] == SpireTemplateTile.Wall)
				{
					return false;
				}
			}
			return this.Map[x + 1, y] != SpireTemplateTile.Door && this.Map[x - 1, y] != SpireTemplateTile.Door && this.Map[x, y + 1] != SpireTemplateTile.Door && this.Map[x, y - 1] != SpireTemplateTile.Door && this.Map[x + 1, y] != SpireTemplateTile.SecurityDoor && this.Map[x - 1, y] != SpireTemplateTile.SecurityDoor && this.Map[x, y + 1] != SpireTemplateTile.SecurityDoor && this.Map[x, y - 1] != SpireTemplateTile.SecurityDoor;
		}

		// Token: 0x0600700F RID: 28687 RVA: 0x0027A714 File Offset: 0x00278914
		private void FillRoom(int x, int y, int Depth)
		{
			if (Depth >= this.MaxDepth)
			{
				return;
			}
			SpireRoomData spireRoomData = new SpireRoomData(this.Width, this.Height);
			for (int i = 0; i < this.Width; i++)
			{
				for (int j = 0; j < this.Height; j++)
				{
					spireRoomData.Room[i, j] = 0;
				}
			}
			this.FloodRoom1((short)x, (short)y, spireRoomData);
			spireRoomData.Width = spireRoomData.Right - spireRoomData.Left + 1;
			spireRoomData.Height = spireRoomData.Bottom - spireRoomData.Top + 1;
			if (spireRoomData.Size <= 24)
			{
				return;
			}
			if (spireRoomData.Width <= 4)
			{
				return;
			}
			if (spireRoomData.Height <= 4)
			{
				return;
			}
			if (spireRoomData.RightAt[y] - spireRoomData.LeftAt[y] <= 2)
			{
				return;
			}
			if (spireRoomData.BottomAt[x] - spireRoomData.TopAt[x] <= 2)
			{
				return;
			}
			int num;
			if (spireRoomData.Width > spireRoomData.Height)
			{
				num = 0;
			}
			else
			{
				num = 1;
			}
			if (num == 0)
			{
				int num2 = 0;
				int num3 = Stat.Random(spireRoomData.LeftAt[y] + 1, spireRoomData.RightAt[y] - 1);
				while (num2 < 10 && (this.Map[num3, spireRoomData.TopAt[num3] - 1] != SpireTemplateTile.Wall || this.Map[num3, spireRoomData.BottomAt[num3] + 1] != SpireTemplateTile.Wall))
				{
					num2++;
					num3 = Stat.Random(spireRoomData.LeftAt[y] + 1, spireRoomData.RightAt[y] - 1);
				}
				if (num2 == 10)
				{
					return;
				}
				if (Math.Abs(spireRoomData.TopAt[num3] - spireRoomData.BottomAt[num3]) < 3)
				{
					return;
				}
				for (int k = spireRoomData.TopAt[num3]; k <= spireRoomData.BottomAt[num3]; k++)
				{
					if (spireRoomData.Room[num3, k] == 1)
					{
						this.Map[num3, k] = SpireTemplateTile.Wall;
						this.Rooms[num3, k] = 0;
					}
				}
				num2 = 0;
				int num4 = Stat.Random(spireRoomData.TopAt[num3], spireRoomData.BottomAt[num3]);
				while (num2 < 10 && !this.DoorPositionOk(num3, num4))
				{
					num2++;
					num4 = Stat.Random(spireRoomData.TopAt[num3], spireRoomData.BottomAt[num3]);
				}
				this.Map[num3, num4] = SpireTemplateTile.Door;
				this.Rooms[num3, num4] = 0;
				this.FillRoom(num3 - 1, num4, Depth + 1);
				this.FillRoom(num3 + 1, num4, Depth + 1);
			}
			if (num == 1)
			{
				int num5 = 0;
				int num3 = Stat.Random(spireRoomData.TopAt[x] + 1, spireRoomData.BottomAt[x] - 1);
				while (num5 < 10 && (this.Map[spireRoomData.LeftAt[num3] - 1, num3] != SpireTemplateTile.Wall || this.Map[spireRoomData.RightAt[num3] + 1, num3] != SpireTemplateTile.Wall))
				{
					num5++;
					num3 = Stat.Random(spireRoomData.TopAt[x] + 1, spireRoomData.BottomAt[x] - 1);
				}
				if (num5 == 10)
				{
					return;
				}
				if (Math.Abs(spireRoomData.LeftAt[num3] - spireRoomData.RightAt[num3]) < 3)
				{
					return;
				}
				for (int l = spireRoomData.LeftAt[num3]; l <= spireRoomData.RightAt[num3]; l++)
				{
					if (spireRoomData.Room[l, num3] == 1)
					{
						this.Map[l, num3] = SpireTemplateTile.Wall;
						this.Rooms[l, num3] = 0;
					}
				}
				num5 = 0;
				int num6 = Stat.Random(spireRoomData.LeftAt[num3], spireRoomData.RightAt[num3]);
				while (num5 < 10 && !this.DoorPositionOk(num6, num3))
				{
					num5++;
					num6 = Stat.Random(spireRoomData.LeftAt[num3], spireRoomData.RightAt[num3]);
				}
				this.Map[num6, num3] = SpireTemplateTile.Door;
				this.Rooms[num6, num3] = 0;
				this.FillRoom(num6, num3 - 1, Depth + 1);
				this.FillRoom(num6, num3 + 1, Depth + 1);
			}
		}

		// Token: 0x06007010 RID: 28688 RVA: 0x0027AAE4 File Offset: 0x00278CE4
		private void FloodRoom1(short x, short y, SpireRoomData Data)
		{
			if (x < 0 || (int)x >= this.Width)
			{
				return;
			}
			if (y < 0 || (int)y >= this.Height)
			{
				return;
			}
			if (Data.Room[(int)x, (int)y] != 0)
			{
				return;
			}
			if (this.Map[(int)x, (int)y] != SpireTemplateTile.Inside)
			{
				return;
			}
			if ((int)x < Data.Left)
			{
				Data.Left = (int)x;
			}
			if ((int)x > Data.Right)
			{
				Data.Right = (int)x;
			}
			if ((int)y < Data.Top)
			{
				Data.Top = (int)y;
			}
			if ((int)y > Data.Bottom)
			{
				Data.Bottom = (int)y;
			}
			if ((int)x < Data.LeftAt[(int)y])
			{
				Data.LeftAt[(int)y] = (int)x;
			}
			if ((int)x > Data.RightAt[(int)y])
			{
				Data.RightAt[(int)y] = (int)x;
			}
			if ((int)y < Data.TopAt[(int)x])
			{
				Data.TopAt[(int)x] = (int)y;
			}
			if ((int)y > Data.BottomAt[(int)x])
			{
				Data.BottomAt[(int)x] = (int)y;
			}
			this.Rooms[(int)x, (int)y] = 1;
			Data.Room[(int)x, (int)y] = 1;
			Data.Size++;
			this.FloodRoom1(x - 1, y, Data);
			this.FloodRoom1(x + 1, y, Data);
			this.FloodRoom1(x, y - 1, Data);
			this.FloodRoom1(x, y + 1, Data);
		}

		// Token: 0x06007011 RID: 28689 RVA: 0x0027AC18 File Offset: 0x00278E18
		private void FloodRoom(short x, short y, SpireRoomData Data, short n)
		{
			if (x < 0 || (int)x >= this.Width)
			{
				return;
			}
			if (y < 0 || (int)y >= this.Height)
			{
				return;
			}
			if (Data.Room[(int)x, (int)y] != 0)
			{
				return;
			}
			if (this.Map[(int)x, (int)y] == SpireTemplateTile.Door || this.Map[(int)x, (int)y] == SpireTemplateTile.SecurityDoor)
			{
				SpireTemplate.P = new Point((int)x, (int)y);
				SpireTemplate.bOK = true;
				foreach (Point point in Data.Doors)
				{
					if (point.X == (int)x && point.Y == (int)y)
					{
						SpireTemplate.bOK = false;
						break;
					}
				}
				if (SpireTemplate.bOK)
				{
					Data.Doors.Add(SpireTemplate.P);
				}
			}
			if (this.Map[(int)x, (int)y] != SpireTemplateTile.Inside)
			{
				return;
			}
			if ((int)x < Data.Left)
			{
				Data.Left = (int)x;
			}
			if ((int)x > Data.Right)
			{
				Data.Right = (int)x;
			}
			if ((int)y < Data.Top)
			{
				Data.Top = (int)y;
			}
			if ((int)y > Data.Bottom)
			{
				Data.Bottom = (int)y;
			}
			if ((int)x < Data.LeftAt[(int)y])
			{
				Data.LeftAt[(int)y] = (int)x;
			}
			if ((int)x > Data.RightAt[(int)y])
			{
				Data.RightAt[(int)y] = (int)x;
			}
			if ((int)y < Data.TopAt[(int)x])
			{
				Data.TopAt[(int)x] = (int)y;
			}
			if ((int)y > Data.BottomAt[(int)x])
			{
				Data.BottomAt[(int)x] = (int)y;
			}
			this.Rooms[(int)x, (int)y] = (int)n;
			Data.Room[(int)x, (int)y] = (int)n;
			Data.Size++;
			this.FloodRoom(x - 1, y, Data, n);
			this.FloodRoom(x + 1, y, Data, n);
			this.FloodRoom(x, y - 1, Data, n);
			this.FloodRoom(x, y + 1, Data, n);
		}

		// Token: 0x06007012 RID: 28690 RVA: 0x0027ADF8 File Offset: 0x00278FF8
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
			if (this.Map[xp, yp] == SpireTemplateTile.OutsideWall)
			{
				return;
			}
			if (this.Rooms[xp, yp] > 0 && this.Map[xp, yp] <= SpireTemplateTile.Void)
			{
				this.Rooms[xp, yp] = 2;
				return;
			}
			this.Rooms[xp, yp] = 3;
			this.Map[xp, yp] = SpireTemplateTile.Inside;
			this.FloodInsideWall(xp - 1, yp - 1);
			this.FloodInsideWall(xp - 1, yp);
			this.FloodInsideWall(xp - 1, yp + 1);
			this.FloodInsideWall(xp, yp - 1);
			this.FloodInsideWall(xp, yp + 1);
			this.FloodInsideWall(xp + 1, yp - 1);
			this.FloodInsideWall(xp + 1, yp);
			this.FloodInsideWall(xp + 1, yp + 1);
		}

		// Token: 0x06007013 RID: 28691 RVA: 0x0027AEF8 File Offset: 0x002790F8
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
			if (this.Map[xp, yp] == SpireTemplateTile.OutsideWall || this.Map[xp, yp] == SpireTemplateTile.Outside)
			{
				return;
			}
			if (this.Map[xp, yp] == SpireTemplateTile.Wall)
			{
				this.Map[xp, yp] = SpireTemplateTile.OutsideWall;
				return;
			}
			this.Map[xp, yp] = SpireTemplateTile.Outside;
			this.FloodOutsideWall(xp - 1, yp - 1);
			this.FloodOutsideWall(xp - 1, yp);
			this.FloodOutsideWall(xp - 1, yp + 1);
			this.FloodOutsideWall(xp, yp - 1);
			this.FloodOutsideWall(xp, yp + 1);
			this.FloodOutsideWall(xp + 1, yp - 1);
			this.FloodOutsideWall(xp + 1, yp);
			this.FloodOutsideWall(xp + 1, yp + 1);
		}

		// Token: 0x06007014 RID: 28692 RVA: 0x0027AFCC File Offset: 0x002791CC
		public void Draw(SpireRoomData Data)
		{
			Popup._ScreenBuffer.Clear();
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					if (this.Map[j, i] == SpireTemplateTile.Inside)
					{
						Popup._ScreenBuffer.Goto(j, i);
						Popup._ScreenBuffer.Write(58);
					}
					if (this.Map[j, i] == SpireTemplateTile.Void)
					{
						Popup._ScreenBuffer.Goto(j, i);
						Popup._ScreenBuffer.Write(88);
					}
					if (this.Map[j, i] == SpireTemplateTile.Outside)
					{
						Popup._ScreenBuffer.Goto(j, i);
						Popup._ScreenBuffer.Write(44);
					}
					if (this.Map[j, i] == SpireTemplateTile.OutsideWall)
					{
						Popup._ScreenBuffer.Goto(j, i);
						Popup._ScreenBuffer.Write(36);
					}
					if (Data != null && Data.Room[j, i] > 0)
					{
						Popup._ScreenBuffer.Goto(j, i);
						Popup._ScreenBuffer.Write((short)(65 + Data.Room[j, i]));
					}
					if (this.Map[j, i] == SpireTemplateTile.Wall)
					{
						Popup._ScreenBuffer.Goto(j, i);
						Popup._ScreenBuffer.Write(42);
					}
					if (this.Map[j, i] == SpireTemplateTile.Door)
					{
						Popup._ScreenBuffer.Goto(j, i);
						Popup._ScreenBuffer.Write(43);
					}
				}
			}
			Popup._TextConsole.DrawBuffer(Popup._ScreenBuffer, null, false);
			Popup._ScreenBuffer.Goto(0, 0);
			Popup._ScreenBuffer.Write(Data.Width.ToString() + "," + Data.Height.ToString(), true, false, false, null, -1);
			Keyboard.getch();
		}

		// Token: 0x040033A1 RID: 13217
		public int MaxDepth;

		// Token: 0x040033A2 RID: 13218
		[NonSerialized]
		private static Point P;

		// Token: 0x040033A3 RID: 13219
		[NonSerialized]
		private static bool bOK;

		// Token: 0x040033A4 RID: 13220
		public int Width;

		// Token: 0x040033A5 RID: 13221
		public int Height;

		// Token: 0x040033A6 RID: 13222
		public int[,] Rooms;

		// Token: 0x040033A7 RID: 13223
		public SpireTemplateTile[,] Map;

		// Token: 0x040033A8 RID: 13224
		public List<SpireRoomData> RoomList = new List<SpireRoomData>();
	}
}
