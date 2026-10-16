using System;
using ConsoleLib.Console;
using XRL.UI;
using XRL.World;

namespace Genkit
{
	// Token: 0x02000382 RID: 898
	[Serializable]
	public class Maze : IComposite
	{
		// Token: 0x060021DD RID: 8669 RVA: 0x000A847C File Offset: 0x000A667C
		public Maze()
		{
		}

		// Token: 0x060021DE RID: 8670 RVA: 0x000A8484 File Offset: 0x000A6684
		public Maze(int Width, int Height)
		{
			this.Init(Width, Height, false);
		}

		// Token: 0x060021DF RID: 8671 RVA: 0x000A8495 File Offset: 0x000A6695
		public Maze(int Width, int Height, bool InitialValue)
		{
			this.Init(Width, Height, InitialValue);
		}

		// Token: 0x060021E0 RID: 8672 RVA: 0x000A84A6 File Offset: 0x000A66A6
		public void Init(int _Width, int _Height)
		{
			this.Init(_Width, _Height, false);
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x060021E1 RID: 8673 RVA: 0x000A84B1 File Offset: 0x000A66B1
		public bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060021E2 RID: 8674 RVA: 0x000A84B4 File Offset: 0x000A66B4
		public void Write(SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.Width);
			Writer.WriteOptimized(this.Height);
			for (int i = 0; i < this.Width; i++)
			{
				for (int j = 0; j < this.Height; j++)
				{
					Writer.Write(this.Cell[i, j].N);
					Writer.Write(this.Cell[i, j].S);
					Writer.Write(this.Cell[i, j].E);
					Writer.Write(this.Cell[i, j].W);
				}
			}
		}

		// Token: 0x060021E3 RID: 8675 RVA: 0x000A855C File Offset: 0x000A675C
		public void Read(SerializationReader Reader)
		{
			this.Width = Reader.ReadOptimizedInt32();
			this.Height = Reader.ReadOptimizedInt32();
			this.Cell = new MazeCell[this.Width, this.Height];
			for (int i = 0; i < this.Width; i++)
			{
				for (int j = 0; j < this.Height; j++)
				{
					this.Cell[i, j] = new MazeCell
					{
						x = i,
						y = j,
						N = Reader.ReadBoolean(),
						S = Reader.ReadBoolean(),
						E = Reader.ReadBoolean(),
						W = Reader.ReadBoolean()
					};
				}
			}
		}

		// Token: 0x060021E4 RID: 8676 RVA: 0x000A860C File Offset: 0x000A680C
		public void Init(int _Width, int _Height, bool Value)
		{
			this.Width = _Width;
			this.Height = _Height;
			this.Cell = new MazeCell[this.Width, this.Height];
			for (int i = 0; i < this.Width; i++)
			{
				for (int j = 0; j < this.Height; j++)
				{
					this.Cell[i, j] = new MazeCell(Value);
					this.Cell[i, j].x = i;
					this.Cell[i, j].y = j;
				}
			}
		}

		// Token: 0x060021E5 RID: 8677 RVA: 0x000A869C File Offset: 0x000A689C
		public void SetBorder(bool Value)
		{
			for (int i = 0; i < this.Width; i++)
			{
				this.Cell[i, 0].N = Value;
				this.Cell[i, this.Height - 1].S = Value;
			}
			for (int j = 0; j < this.Height; j++)
			{
				this.Cell[0, j].W = Value;
				this.Cell[this.Width - 1, j].E = Value;
			}
		}

		// Token: 0x060021E6 RID: 8678 RVA: 0x000A8728 File Offset: 0x000A6928
		public void Test(bool bWait)
		{
			ScreenBuffer scrapBuffer = TextConsole.GetScrapBuffer1(false);
			TextConsole textConsole = Popup._TextConsole;
			int num = 0;
			while (num < 80 && num < this.Width)
			{
				int num2 = 0;
				while (num2 < 25 && num2 < this.Height)
				{
					MazeCell mazeCell = this.Cell[num, num2];
					scrapBuffer.Goto(num, num2);
					if (!mazeCell.N && !mazeCell.S && !mazeCell.E && !mazeCell.W)
					{
						scrapBuffer.Write(46);
					}
					if (!mazeCell.N && !mazeCell.S && !mazeCell.E && mazeCell.W)
					{
						scrapBuffer.Write(182);
					}
					if (!mazeCell.N && !mazeCell.S && mazeCell.E && !mazeCell.W)
					{
						scrapBuffer.Write(199);
					}
					if (!mazeCell.N && !mazeCell.S && mazeCell.E && mazeCell.W)
					{
						scrapBuffer.Write(196);
					}
					if (!mazeCell.N && mazeCell.S && !mazeCell.E && !mazeCell.W)
					{
						scrapBuffer.Write(209);
					}
					if (!mazeCell.N && mazeCell.S && !mazeCell.E && mazeCell.W)
					{
						scrapBuffer.Write(191);
					}
					if (!mazeCell.N && mazeCell.S && mazeCell.E && !mazeCell.W)
					{
						scrapBuffer.Write(218);
					}
					if (!mazeCell.N && mazeCell.S && mazeCell.E && mazeCell.W)
					{
						scrapBuffer.Write(194);
					}
					if (mazeCell.N && !mazeCell.S && !mazeCell.E && !mazeCell.W)
					{
						scrapBuffer.Write(207);
					}
					if (mazeCell.N && !mazeCell.S && !mazeCell.E && mazeCell.W)
					{
						scrapBuffer.Write(217);
					}
					if (mazeCell.N && !mazeCell.S && mazeCell.E && !mazeCell.W)
					{
						scrapBuffer.Write(192);
					}
					if (mazeCell.N && !mazeCell.S && mazeCell.E && mazeCell.W)
					{
						scrapBuffer.Write(193);
					}
					if (mazeCell.N && mazeCell.S && !mazeCell.E && !mazeCell.W)
					{
						scrapBuffer.Write(179);
					}
					if (mazeCell.N && mazeCell.S && !mazeCell.E && mazeCell.W)
					{
						scrapBuffer.Write(180);
					}
					if (mazeCell.N && mazeCell.S && mazeCell.E && !mazeCell.W)
					{
						scrapBuffer.Write(195);
					}
					if (mazeCell.N && mazeCell.S && mazeCell.E && mazeCell.W)
					{
						scrapBuffer.Write(197);
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

		// Token: 0x040010F6 RID: 4342
		public int Width;

		// Token: 0x040010F7 RID: 4343
		public int Height;

		// Token: 0x040010F8 RID: 4344
		public MazeCell[,] Cell;
	}
}
