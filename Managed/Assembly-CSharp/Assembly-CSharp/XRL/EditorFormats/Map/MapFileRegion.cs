using System;
using System.Collections.Generic;
using UnityEngine;
using XRL.World;

namespace XRL.EditorFormats.Map
{
	// Token: 0x020005CA RID: 1482
	public struct MapFileRegion
	{
		// Token: 0x17000666 RID: 1638
		public MapFileCell this[int x, int y]
		{
			get
			{
				return this.cells[x, y];
			}
			set
			{
				this.cells[x, y] = value;
			}
		}

		// Token: 0x17000667 RID: 1639
		[Obsolete("This version is read only, please update to using [x,y]")]
		public MapFileCell[] this[int x]
		{
			get
			{
				MapFileCell[] array = new MapFileCell[this.height];
				for (int i = 0; i < this.height; i++)
				{
					array[i] = this.cells[x, i];
				}
				return array;
			}
		}

		// Token: 0x06003F0A RID: 16138 RVA: 0x00136E03 File Offset: 0x00135003
		public MapFileRegion(int width = 80, int height = 25)
		{
			this.width = width;
			this.height = height;
			this.cells = new MapFileCell[width, height];
		}

		// Token: 0x06003F0B RID: 16139 RVA: 0x00136E20 File Offset: 0x00135020
		public bool HasBlueprintInCell(int x, int y, string bpname, string paintWith = "")
		{
			return x >= 0 && x < this.width && y >= 0 && y < this.height && this.cells[x, y] != null && this.cells[x, y].Objects.FindCount<MapFileObjectBlueprint>(delegate(MapFileObjectBlueprint bp)
			{
				if (bp.Name == bpname)
				{
					return true;
				}
				if (paintWith == null)
				{
					return false;
				}
				string tag = GameObjectFactory.Factory.Blueprints[bp.Name].GetTag("PaintWith", null);
				return tag == paintWith || tag == "*" || (paintWith == "*" && tag != null);
			}) > 0;
		}

		// Token: 0x06003F0C RID: 16140 RVA: 0x00136E94 File Offset: 0x00135094
		public MapFileRegion FlippedVertical()
		{
			MapFileRegion mapFileRegion = new MapFileRegion(this.width, this.height);
			for (int i = 0; i < this.height; i++)
			{
				for (int j = 0; j < this.width; j++)
				{
					mapFileRegion.cells[j, this.height - i - 1] = this.cells[j, i];
				}
			}
			return mapFileRegion;
		}

		// Token: 0x06003F0D RID: 16141 RVA: 0x00136EFC File Offset: 0x001350FC
		public MapFileRegion Clone()
		{
			MapFileRegion mapFileRegion = new MapFileRegion(this.width, this.height);
			foreach (MapFileCellReference mapFileCellReference in this.AllCells(null))
			{
				MapFileCell orCreateCellAt = mapFileRegion.GetOrCreateCellAt(mapFileCellReference.x, mapFileCellReference.y);
				foreach (MapFileObjectBlueprint mapFileObjectBlueprint in mapFileCellReference.cell.Objects)
				{
					orCreateCellAt.Objects.Add(new MapFileObjectBlueprint(mapFileObjectBlueprint));
				}
			}
			return mapFileRegion;
		}

		// Token: 0x06003F0E RID: 16142 RVA: 0x00136FC0 File Offset: 0x001351C0
		public MapFileRegion GetRegion(Rect rect)
		{
			if (rect.xMin < 0f || rect.xMax >= (float)this.width || rect.yMin < 0f || rect.yMax >= (float)this.height)
			{
				throw new ArgumentOutOfRangeException("rect", rect, "rect is outside the bounds of this region");
			}
			MapFileRegion mapFileRegion = new MapFileRegion((int)rect.width + 1, (int)rect.height + 1);
			for (int i = (int)rect.xMin; i <= (int)rect.xMax; i++)
			{
				for (int j = (int)rect.yMin; j <= (int)rect.yMax; j++)
				{
					mapFileRegion.cells[i - (int)rect.xMin, j - (int)rect.yMin] = this.cells[i, j];
				}
			}
			return mapFileRegion;
		}

		// Token: 0x06003F0F RID: 16143 RVA: 0x0013709C File Offset: 0x0013529C
		public MapFileRegion FlippedHorizontal()
		{
			MapFileRegion mapFileRegion = new MapFileRegion(this.width, this.height);
			for (int i = 0; i < this.height; i++)
			{
				for (int j = 0; j < this.width; j++)
				{
					mapFileRegion.cells[this.width - j - 1, i] = this.cells[j, i];
				}
			}
			return mapFileRegion;
		}

		// Token: 0x06003F10 RID: 16144 RVA: 0x00137102 File Offset: 0x00135302
		public IEnumerable<MapFileCellReference> FindCellsWithObjectBlueprint(string blueprint)
		{
			Predicate<MapFileObjectBlueprint> <>9__0;
			int num;
			for (int y = 0; y < this.height; y = num + 1)
			{
				for (int x = 0; x < this.width; x = num + 1)
				{
					if (this.cells[x, y] != null)
					{
						List<MapFileObjectBlueprint> objects = this.cells[x, y].Objects;
						Predicate<MapFileObjectBlueprint> predicate;
						if ((predicate = <>9__0) == null)
						{
							predicate = (<>9__0 = (MapFileObjectBlueprint o) => o.Name == blueprint);
						}
						if (objects.Find(predicate) != null)
						{
							yield return new MapFileCellReference(ref this, x, y, this.cells[x, y]);
						}
					}
					num = x;
				}
				num = y;
			}
			yield break;
		}

		// Token: 0x06003F11 RID: 16145 RVA: 0x0013711E File Offset: 0x0013531E
		public IEnumerable<MapFileCellReference> FindCellsWithObjectBlueprint(MapFileObjectBlueprint blueprint)
		{
			Predicate<MapFileObjectBlueprint> <>9__0;
			int num;
			for (int y = 0; y < this.height; y = num + 1)
			{
				for (int x = 0; x < this.width; x = num + 1)
				{
					if (this.cells[x, y] != null)
					{
						List<MapFileObjectBlueprint> objects = this.cells[x, y].Objects;
						Predicate<MapFileObjectBlueprint> predicate;
						if ((predicate = <>9__0) == null)
						{
							predicate = (<>9__0 = (MapFileObjectBlueprint o) => o == blueprint);
						}
						if (objects.Find(predicate) != null)
						{
							yield return new MapFileCellReference(ref this, x, y, this.cells[x, y]);
						}
					}
					num = x;
				}
				num = y;
			}
			yield break;
		}

		// Token: 0x06003F12 RID: 16146 RVA: 0x0013713A File Offset: 0x0013533A
		public IEnumerable<MapFileCellReference> AllCells(Predicate<MapFileCellReference> filter = null)
		{
			int num;
			for (int y = 0; y < this.height; y = num + 1)
			{
				for (int x = 0; x < this.width; x = num + 1)
				{
					if (this.cells[x, y] != null)
					{
						MapFileCellReference mapFileCellReference = new MapFileCellReference(ref this, x, y, this.cells[x, y]);
						if (filter == null || filter(mapFileCellReference))
						{
							yield return mapFileCellReference;
						}
					}
					num = x;
				}
				num = y;
			}
			yield break;
		}

		// Token: 0x06003F13 RID: 16147 RVA: 0x00137156 File Offset: 0x00135356
		public IEnumerable<MapFileObjectReference> AllObjects(Predicate<MapFileObjectBlueprint> filter = null)
		{
			int num;
			for (int y = 0; y < this.height; y = num + 1)
			{
				for (int x = 0; x < this.width; x = num + 1)
				{
					if (this.cells[x, y] != null)
					{
						foreach (MapFileObjectBlueprint mapFileObjectBlueprint in this.cells[x, y].Objects)
						{
							if (filter == null || filter(mapFileObjectBlueprint))
							{
								yield return new MapFileObjectReference(ref this, x, y, this.cells[x, y], mapFileObjectBlueprint);
							}
						}
						List<MapFileObjectBlueprint>.Enumerator enumerator = default(List<MapFileObjectBlueprint>.Enumerator);
					}
					num = x;
				}
				num = y;
			}
			yield break;
			yield break;
		}

		/// <summary>Copy the contents of the region to the section of this region matching the upper left x,y corner</summary>
		// Token: 0x06003F14 RID: 16148 RVA: 0x00137174 File Offset: 0x00135374
		public void SetRegion(MapFileRegion other, int x0 = 0, int y0 = 0)
		{
			int num = y0;
			while (num < this.height && num - y0 < other.height)
			{
				int num2 = x0;
				while (num2 < this.width && num2 - x0 < other.width)
				{
					this.cells[num2, num] = other.cells[num2 - x0, num - y0];
					num2++;
				}
				num++;
			}
		}

		/// <summary>The x position of the right most in use cell, -1 if no cells are in use</summary>
		// Token: 0x06003F15 RID: 16149 RVA: 0x001371D8 File Offset: 0x001353D8
		public int MaxX(bool requireObjects = false)
		{
			for (int i = 0; i < this.height; i++)
			{
				for (int j = this.width - 1; j >= 0; j--)
				{
					if (this.cells[j, i] != null && (!requireObjects || this.cells[j, i].Objects.Count > 0))
					{
						return j;
					}
				}
			}
			return -1;
		}

		/// <summary>The y position of the bottom most in use cell, -1 if no cells are in use</summary>
		/// <param>requireObjects: when true, the cell found must have at least one object in it</param>
		// Token: 0x06003F16 RID: 16150 RVA: 0x00137238 File Offset: 0x00135438
		public int MaxY(bool requireObjects = false)
		{
			for (int i = this.height - 1; i >= 0; i--)
			{
				for (int j = 0; j < this.width; j++)
				{
					if (this.cells[j, i] != null && (!requireObjects || this.cells[j, i].Objects.Count > 0))
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06003F17 RID: 16151 RVA: 0x00137298 File Offset: 0x00135498
		public MapFileCell GetOrCreateCellAt(int x, int y)
		{
			if (x < 0 || x >= this.width)
			{
				throw new ArgumentOutOfRangeException("x");
			}
			if (y < 0 || y >= this.height)
			{
				throw new ArgumentOutOfRangeException("y");
			}
			if (this.cells[x, y] == null)
			{
				this.cells[x, y] = new MapFileCell();
			}
			return this.cells[x, y];
		}

		// Token: 0x06003F18 RID: 16152 RVA: 0x00137304 File Offset: 0x00135504
		public void FillEmptyCells()
		{
			for (int i = 0; i < this.height; i++)
			{
				for (int j = 0; j < this.width; j++)
				{
					if (this.cells[j, i] == null)
					{
						this.cells[j, i] = new MapFileCell();
					}
				}
			}
		}

		// Token: 0x04001F2B RID: 7979
		public readonly int width;

		// Token: 0x04001F2C RID: 7980
		public readonly int height;

		// Token: 0x04001F2D RID: 7981
		private MapFileCell[,] cells;
	}
}
