using System;
using ConsoleLib.Console;
using XRL.UI;
using XRL.World;

namespace Genkit
{
	// Token: 0x02000383 RID: 899
	[Serializable]
	public class Maze3D
	{
		// Token: 0x060021E7 RID: 8679 RVA: 0x000A8A91 File Offset: 0x000A6C91
		public Maze3D()
		{
		}

		// Token: 0x060021E8 RID: 8680 RVA: 0x000A8A99 File Offset: 0x000A6C99
		public Maze3D(int Width, int Height, int Depth)
		{
			this.Init(Width, Height, Depth, false);
		}

		// Token: 0x060021E9 RID: 8681 RVA: 0x000A8AAB File Offset: 0x000A6CAB
		public Maze3D(int Width, int Height, int Depth, bool InitialValue)
		{
			this.Init(Width, Height, Depth, InitialValue);
		}

		// Token: 0x060021EA RID: 8682 RVA: 0x000A8ABE File Offset: 0x000A6CBE
		public void Init(int _Width, int _Height, int Depth)
		{
			this.Init(_Width, _Height, Depth, false);
		}

		// Token: 0x060021EB RID: 8683 RVA: 0x000A8ACC File Offset: 0x000A6CCC
		public static Maze3D Load(SerializationReader Reader)
		{
			Maze3D maze3D = new Maze3D();
			maze3D.Width = Reader.ReadInt32();
			maze3D.Height = Reader.ReadInt32();
			maze3D.Depth = Reader.ReadInt32();
			maze3D.Cell = new MazeCell3D[maze3D.Width, maze3D.Height, maze3D.Depth];
			short num = 0;
			while ((int)num < maze3D.Width)
			{
				short num2 = 0;
				while ((int)num2 < maze3D.Height)
				{
					short num3 = 0;
					while ((int)num3 < maze3D.Depth)
					{
						maze3D.Cell[(int)num, (int)num2, (int)num3].x = num;
						maze3D.Cell[(int)num, (int)num2, (int)num3].y = num2;
						maze3D.Cell[(int)num, (int)num2, (int)num3].z = num3;
						maze3D.Cell[(int)num, (int)num2, (int)num3].N = Reader.ReadBoolean();
						maze3D.Cell[(int)num, (int)num2, (int)num3].S = Reader.ReadBoolean();
						maze3D.Cell[(int)num, (int)num2, (int)num3].E = Reader.ReadBoolean();
						maze3D.Cell[(int)num, (int)num2, (int)num3].W = Reader.ReadBoolean();
						maze3D.Cell[(int)num, (int)num2, (int)num3].U = Reader.ReadBoolean();
						maze3D.Cell[(int)num, (int)num2, (int)num3].D = Reader.ReadBoolean();
						num3 += 1;
					}
					num2 += 1;
				}
				num += 1;
			}
			return maze3D;
		}

		// Token: 0x060021EC RID: 8684 RVA: 0x000A8C3C File Offset: 0x000A6E3C
		public void Save(SerializationWriter Writer)
		{
			Writer.Write(this.Width);
			Writer.Write(this.Height);
			Writer.Write(this.Depth);
			for (int i = 0; i < this.Width; i++)
			{
				for (int j = 0; j < this.Height; j++)
				{
					for (int k = 0; k < this.Depth; k++)
					{
						Writer.Write(this.Cell[i, j, k].N);
						Writer.Write(this.Cell[i, j, k].S);
						Writer.Write(this.Cell[i, j, k].E);
						Writer.Write(this.Cell[i, j, k].W);
						Writer.Write(this.Cell[i, j, k].U);
						Writer.Write(this.Cell[i, j, k].D);
					}
				}
			}
		}

		// Token: 0x060021ED RID: 8685 RVA: 0x000A8D48 File Offset: 0x000A6F48
		public void Init(int _Width, int _Height, int _Depth, bool Value)
		{
			this.Width = _Width;
			this.Height = _Height;
			this.Depth = _Depth;
			this.Cell = new MazeCell3D[this.Width, this.Height, this.Depth];
			short num = 0;
			while ((int)num < this.Width)
			{
				short num2 = 0;
				while ((int)num2 < this.Height)
				{
					short num3 = 0;
					while ((int)num3 < this.Depth)
					{
						this.Cell[(int)num, (int)num2, (int)num3] = new MazeCell3D(Value);
						this.Cell[(int)num, (int)num2, (int)num3].x = num;
						this.Cell[(int)num, (int)num2, (int)num3].y = num2;
						this.Cell[(int)num, (int)num2, (int)num3].z = num3;
						num3 += 1;
					}
					num2 += 1;
				}
				num += 1;
			}
		}

		// Token: 0x060021EE RID: 8686 RVA: 0x000A8E14 File Offset: 0x000A7014
		public void SetBorder(bool Value)
		{
			for (int i = 0; i < this.Width; i++)
			{
				for (int j = 0; j < this.Height; j++)
				{
					for (int k = 0; k < this.Depth; k++)
					{
						if (i == 0 || i == this.Width - 1 || j == 0 || j == this.Height - 1 || k == 0 || k == this.Depth - 1)
						{
							this.Cell[i, j, k].N = Value;
						}
					}
				}
			}
		}

		// Token: 0x060021EF RID: 8687 RVA: 0x000A8E94 File Offset: 0x000A7094
		public void Test(bool bWait)
		{
			ScreenBuffer scrapBuffer = ScreenBuffer.GetScrapBuffer1(false);
			TextConsole textConsole = Popup._TextConsole;
			for (int i = 0; i < this.Depth; i++)
			{
				int num = 0;
				while (num < 80 && num < this.Width)
				{
					int num2 = 0;
					while (num2 < 25 && num2 < this.Height)
					{
						MazeCell3D mazeCell3D = this.Cell[num, num2, i];
						scrapBuffer.Goto(num, num2);
						if (mazeCell3D.U)
						{
							scrapBuffer.Write(">", true, false, false, null, -1);
						}
						else if (mazeCell3D.D)
						{
							scrapBuffer.Write("<", true, false, false, null, -1);
						}
						else
						{
							if (!mazeCell3D.N && !mazeCell3D.S && !mazeCell3D.E && !mazeCell3D.W)
							{
								scrapBuffer.Write(46);
							}
							if (!mazeCell3D.N && !mazeCell3D.S && !mazeCell3D.E && mazeCell3D.W)
							{
								scrapBuffer.Write(182);
							}
							if (!mazeCell3D.N && !mazeCell3D.S && mazeCell3D.E && !mazeCell3D.W)
							{
								scrapBuffer.Write(199);
							}
							if (!mazeCell3D.N && !mazeCell3D.S && mazeCell3D.E && mazeCell3D.W)
							{
								scrapBuffer.Write(196);
							}
							if (!mazeCell3D.N && mazeCell3D.S && !mazeCell3D.E && !mazeCell3D.W)
							{
								scrapBuffer.Write(209);
							}
							if (!mazeCell3D.N && mazeCell3D.S && !mazeCell3D.E && mazeCell3D.W)
							{
								scrapBuffer.Write(191);
							}
							if (!mazeCell3D.N && mazeCell3D.S && mazeCell3D.E && !mazeCell3D.W)
							{
								scrapBuffer.Write(218);
							}
							if (!mazeCell3D.N && mazeCell3D.S && mazeCell3D.E && mazeCell3D.W)
							{
								scrapBuffer.Write(194);
							}
							if (mazeCell3D.N && !mazeCell3D.S && !mazeCell3D.E && !mazeCell3D.W)
							{
								scrapBuffer.Write(207);
							}
							if (mazeCell3D.N && !mazeCell3D.S && !mazeCell3D.E && mazeCell3D.W)
							{
								scrapBuffer.Write(217);
							}
							if (mazeCell3D.N && !mazeCell3D.S && mazeCell3D.E && !mazeCell3D.W)
							{
								scrapBuffer.Write(192);
							}
							if (mazeCell3D.N && !mazeCell3D.S && mazeCell3D.E && mazeCell3D.W)
							{
								scrapBuffer.Write(193);
							}
							if (mazeCell3D.N && mazeCell3D.S && !mazeCell3D.E && !mazeCell3D.W)
							{
								scrapBuffer.Write(179);
							}
							if (mazeCell3D.N && mazeCell3D.S && !mazeCell3D.E && mazeCell3D.W)
							{
								scrapBuffer.Write(180);
							}
							if (mazeCell3D.N && mazeCell3D.S && mazeCell3D.E && !mazeCell3D.W)
							{
								scrapBuffer.Write(195);
							}
							if (mazeCell3D.N && mazeCell3D.S && mazeCell3D.E && mazeCell3D.W)
							{
								scrapBuffer.Write(197);
							}
						}
						num2++;
					}
					num++;
				}
				textConsole.DrawBuffer(scrapBuffer, null, false);
				if (bWait)
				{
					Keyboard.getch();
				}
			}
		}

		// Token: 0x040010F9 RID: 4345
		public int Width;

		// Token: 0x040010FA RID: 4346
		public int Height;

		// Token: 0x040010FB RID: 4347
		public int Depth;

		// Token: 0x040010FC RID: 4348
		public MazeCell3D[,,] Cell;
	}
}
