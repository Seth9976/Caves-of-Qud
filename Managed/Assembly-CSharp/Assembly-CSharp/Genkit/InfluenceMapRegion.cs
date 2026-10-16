using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using XRL.Core;
using XRL.UI;

namespace Genkit
{
	// Token: 0x0200037B RID: 891
	[Serializable]
	public class InfluenceMapRegion : ILocationArea
	{
		// Token: 0x06002166 RID: 8550 RVA: 0x000A6724 File Offset: 0x000A4924
		public InfluenceMapRegion(int Seed, InfluenceMap parent)
		{
			this.map = parent;
			this.Seed = Seed;
			this.BoundingBox.x1 = int.MaxValue;
			this.BoundingBox.y1 = int.MaxValue;
			this.BoundingBox.x2 = int.MinValue;
			this.BoundingBox.y2 = int.MinValue;
		}

		// Token: 0x06002167 RID: 8551 RVA: 0x000A67C8 File Offset: 0x000A49C8
		public List<Location2D> reducyBy(int radius)
		{
			List<Location2D> list = new List<Location2D>(this.Cells);
			List<Location2D> list2 = new List<Location2D>();
			if (radius == 0)
			{
				return list;
			}
			for (int i = 0; i < radius; i++)
			{
				for (int j = list.Count - 1; j >= 0; j--)
				{
					if (list.Contains(list[j].FromDirection("N")) && list.Contains(list[j].FromDirection("S")) && list.Contains(list[j].FromDirection("E")) && list.Contains(list[j].FromDirection("W")))
					{
						list2.Add(list[j]);
					}
				}
				List<Location2D> list3 = list;
				list = list2;
				list2 = list3;
				list2.Clear();
			}
			return list;
		}

		// Token: 0x06002168 RID: 8552 RVA: 0x000A6890 File Offset: 0x000A4A90
		public List<Location2D> getBorder(int radius)
		{
			List<Location2D> list = new List<Location2D>();
			List<Location2D> list2 = new List<Location2D>(this.Cells);
			List<Location2D> list3 = new List<Location2D>();
			if (radius == 0)
			{
				return list2;
			}
			for (int i = 0; i < radius; i++)
			{
				for (int j = list2.Count - 1; j >= 0; j--)
				{
					if (!list2.Contains(list2[j].FromDirection("N")) || !list2.Contains(list2[j].FromDirection("S")) || !list2.Contains(list2[j].FromDirection("E")) || !list2.Contains(list2[j].FromDirection("W")))
					{
						list.Add(list2[j]);
					}
					else
					{
						list3.Add(list2[j]);
					}
				}
				List<Location2D> list4 = list2;
				list2 = list3;
				list3 = list4;
				list3.Clear();
			}
			return list;
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06002169 RID: 8553 RVA: 0x000A697A File Offset: 0x000A4B7A
		public Location2D Center
		{
			get
			{
				return Location2D.Get((this.BoundingBox.x2 + this.BoundingBox.x1) / 2, (this.BoundingBox.y2 + this.BoundingBox.y1) / 2);
			}
		}

		// Token: 0x0600216A RID: 8554 RVA: 0x000A69B4 File Offset: 0x000A4BB4
		public bool AnyPointsIn(List<Location2D> P)
		{
			for (int i = 0; i < P.Count; i++)
			{
				if (this.PointIn(P[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600216B RID: 8555 RVA: 0x000A69E4 File Offset: 0x000A4BE4
		public int minRadius()
		{
			return Math.Min(this.BoundingBox.Width, this.BoundingBox.Height);
		}

		// Token: 0x0600216C RID: 8556 RVA: 0x000A6A01 File Offset: 0x000A4C01
		public bool PointIn(Location2D P)
		{
			return this.Cells.Contains(P);
		}

		// Token: 0x0600216D RID: 8557 RVA: 0x000A6A0F File Offset: 0x000A4C0F
		public bool PointIn(Point2D P)
		{
			return this.PointIn(P.location);
		}

		// Token: 0x0600216E RID: 8558 RVA: 0x000A6A20 File Offset: 0x000A4C20
		public bool IsEdgeRegion()
		{
			return this.BoundingBox.x1 <= 0 || this.BoundingBox.x2 >= 79 || this.BoundingBox.y1 <= 0 || this.BoundingBox.y2 >= 24;
		}

		// Token: 0x0600216F RID: 8559 RVA: 0x000A6A70 File Offset: 0x000A4C70
		public bool IsCornerRegion()
		{
			return (this.BoundingBox.x1 <= 0 || this.BoundingBox.x2 >= 79) && (this.BoundingBox.y1 <= 0 || this.BoundingBox.y2 >= 24);
		}

		// Token: 0x06002170 RID: 8560 RVA: 0x000A6AC0 File Offset: 0x000A4CC0
		public void draw(bool wait = true, bool drawempty = true, string color = "&B")
		{
			ScreenBuffer scrapBuffer = Popup.ScrapBuffer;
			for (int i = 0; i < 80; i++)
			{
				for (int j = 0; j < 25; j++)
				{
					scrapBuffer.Goto(i, j);
					if (this.Cells.Contains(Location2D.Get(i, j)))
					{
						scrapBuffer.Write(color + "#", true, false, false, null, -1);
					}
					else if (drawempty)
					{
						scrapBuffer.Write("&K.", true, false, false, null, -1);
					}
				}
			}
			XRLCore._Console.DrawBuffer(scrapBuffer, null, false);
			if (wait)
			{
				Keyboard.getch();
			}
		}

		// Token: 0x06002171 RID: 8561 RVA: 0x000A6B50 File Offset: 0x000A4D50
		public InfluenceMapRegion deepCopy()
		{
			return new InfluenceMapRegion(this.Seed, this.map)
			{
				map = this.map,
				BoundingBox = this.BoundingBox,
				Seed = this.Seed,
				Size = this.Size,
				AdjacentRegions = new List<InfluenceMapRegion>(this.AdjacentRegions),
				Cells = new List<Location2D>(this.Cells),
				BorderCells = new List<Location2D>(this.BorderCells),
				NonBorderCells = new List<Location2D>(this.NonBorderCells),
				Tags = new List<string>(this.Tags)
			};
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06002172 RID: 8562 RVA: 0x000A6BF4 File Offset: 0x000A4DF4
		public Rect2D maxRect
		{
			get
			{
				if (this._maxRect.Equals(Rect2D.invalid))
				{
					this._maxRect = GridTools.MaxRectByArea(this.GetGrid(), false, 0, 0).Translate(this.BoundingBox.UpperLeft);
				}
				return this._maxRect;
			}
		}

		// Token: 0x06002173 RID: 8563 RVA: 0x000A6C4B File Offset: 0x000A4E4B
		public void removeCell(Location2D c)
		{
			this.Cells.Remove(c);
			this.BorderCells.Remove(c);
			this.NonBorderCells.Remove(c);
		}

		// Token: 0x06002174 RID: 8564 RVA: 0x000A6C74 File Offset: 0x000A4E74
		public IEnumerable<Location2D> EnumerateLocations()
		{
			return this.Cells;
		}

		// Token: 0x06002175 RID: 8565 RVA: 0x000A6C7C File Offset: 0x000A4E7C
		public bool Contains(Location2D cell)
		{
			return this.Cells.Contains(cell);
		}

		// Token: 0x06002176 RID: 8566 RVA: 0x000A6C8A File Offset: 0x000A4E8A
		public bool HasTag(string tag)
		{
			return this.Tags.Contains(tag);
		}

		// Token: 0x06002177 RID: 8567 RVA: 0x000A6C98 File Offset: 0x000A4E98
		public bool ConnectsToTag(string tag, List<InfluenceMapRegion> Visited = null)
		{
			if (Visited == null)
			{
				Visited = new List<InfluenceMapRegion>();
			}
			Visited.Add(this);
			if (this.Tags.Contains(tag))
			{
				return true;
			}
			for (int i = 0; i < this.AdjacentRegions.Count; i++)
			{
				if (!Visited.Contains(this.AdjacentRegions[i]))
				{
					Visited.Add(this.AdjacentRegions[i]);
					if (this.AdjacentRegions[i].ConnectsToTag(tag, Visited))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06002178 RID: 8568 RVA: 0x000A6D1C File Offset: 0x000A4F1C
		public void AddCell(Location2D P)
		{
			this._maxRect = Rect2D.invalid;
			if (P.X < this.BoundingBox.x1)
			{
				this.BoundingBox.x1 = P.X;
			}
			if (P.Y < this.BoundingBox.y1)
			{
				this.BoundingBox.y1 = P.Y;
			}
			if (P.X > this.BoundingBox.x2)
			{
				this.BoundingBox.x2 = P.X;
			}
			if (P.Y > this.BoundingBox.y2)
			{
				this.BoundingBox.y2 = P.Y;
			}
			this.Cells.Add(P);
		}

		// Token: 0x06002179 RID: 8569 RVA: 0x000A6DD0 File Offset: 0x000A4FD0
		public int[,] GetGrid()
		{
			int[,] array = new int[this.BoundingBox.x2 - this.BoundingBox.x1 + 1, this.BoundingBox.y2 - this.BoundingBox.y1 + 1];
			for (int i = this.BoundingBox.x1; i <= this.BoundingBox.x2; i++)
			{
				for (int j = this.BoundingBox.y1; j <= this.BoundingBox.y2; j++)
				{
					int num = 1;
					if (this.Cells.Contains(Location2D.Get(i, j)))
					{
						num = 0;
					}
					array[i - this.BoundingBox.x1, j - this.BoundingBox.y1] = num;
				}
			}
			return array;
		}

		// Token: 0x0600217A RID: 8570 RVA: 0x000A6E8F File Offset: 0x000A508F
		public IEnumerable<Location2D> EnumerateBorderLocations()
		{
			return this.BorderCells;
		}

		// Token: 0x0600217B RID: 8571 RVA: 0x000A6E97 File Offset: 0x000A5097
		public IEnumerable<Location2D> EnumerateNonBorderLocations()
		{
			return this.NonBorderCells;
		}

		// Token: 0x0600217C RID: 8572 RVA: 0x000A6E9F File Offset: 0x000A509F
		public Location2D GetCenter()
		{
			return this.Center;
		}

		// Token: 0x040010D2 RID: 4306
		public InfluenceMap map;

		// Token: 0x040010D3 RID: 4307
		public Rect2D BoundingBox;

		// Token: 0x040010D4 RID: 4308
		public int Seed;

		// Token: 0x040010D5 RID: 4309
		public int Size;

		// Token: 0x040010D6 RID: 4310
		public List<InfluenceMapRegion> AdjacentRegions = new List<InfluenceMapRegion>();

		// Token: 0x040010D7 RID: 4311
		public List<Location2D> Cells = new List<Location2D>();

		// Token: 0x040010D8 RID: 4312
		public List<Location2D> BorderCells = new List<Location2D>();

		// Token: 0x040010D9 RID: 4313
		public List<Location2D> NonBorderCells = new List<Location2D>();

		// Token: 0x040010DA RID: 4314
		public List<string> Tags = new List<string>();

		// Token: 0x040010DB RID: 4315
		private Rect2D _maxRect = Rect2D.invalid;
	}
}
