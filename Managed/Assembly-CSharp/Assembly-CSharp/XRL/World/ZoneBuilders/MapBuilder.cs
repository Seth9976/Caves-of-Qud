using System;
using XRL.EditorFormats.Map;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AA7 RID: 2727
	public class MapBuilder
	{
		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x06006F2A RID: 28458 RVA: 0x0026A11C File Offset: 0x0026831C
		// (set) Token: 0x06006F2B RID: 28459 RVA: 0x0026A124 File Offset: 0x00268324
		public string FileName
		{
			get
			{
				return this.ID;
			}
			set
			{
				this.ID = value;
			}
		}

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x06006F2C RID: 28460 RVA: 0x0026A130 File Offset: 0x00268330
		public MapFile Map
		{
			get
			{
				MapFile mapFile;
				if ((mapFile = this._Map) == null)
				{
					mapFile = (this._Map = MapFile.Resolve(this.ID, false));
				}
				return mapFile;
			}
		}

		// Token: 0x06006F2D RID: 28461 RVA: 0x0026A15C File Offset: 0x0026835C
		public MapBuilder()
		{
		}

		// Token: 0x06006F2E RID: 28462 RVA: 0x0026A190 File Offset: 0x00268390
		public MapBuilder(string ID)
			: this()
		{
			this.ID = ID;
		}

		// Token: 0x06006F2F RID: 28463 RVA: 0x0026A19F File Offset: 0x0026839F
		public void Clear(Cell C)
		{
			if (this.ClearBeforePlace)
			{
				C.Clear(null, false, true, null);
			}
			if (this.ClearChasms)
			{
				C.ClearObjectsWithPart("ChasmMaterial", true, true);
			}
		}

		// Token: 0x06006F30 RID: 28464 RVA: 0x0026A1C9 File Offset: 0x002683C9
		public static bool BuildFromFile(Zone Z, string FileName, bool Clear = false)
		{
			return new MapBuilder
			{
				ID = FileName,
				ClearBeforePlace = Clear
			}.BuildZone(Z);
		}

		// Token: 0x06006F31 RID: 28465 RVA: 0x0026A1E4 File Offset: 0x002683E4
		public bool BuildZone(Zone Z)
		{
			MapFile map = this.Map;
			if (map.width == 0)
			{
				MetricsManager.LogError("Couldn't find the map: " + this.ID);
				return false;
			}
			if (this.X == -1)
			{
				this.X = Z.Width / 2 - this.Width / 2;
			}
			if (this.Y == -1)
			{
				this.Y = Z.Height / 2 - this.Height / 2;
			}
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					Cell cell = Z.GetCell(this.X + j, this.Y + i);
					if (cell != null)
					{
						map.Cells[j, i].ApplyTo(cell, this.CheckEmpty, new Action<Cell>(this.Clear), null, null, null, null, null, null);
					}
				}
			}
			if (this.BuildReachability)
			{
				Cell cell2 = Z.GetCell(this.X + this.Width / 2, this.Y + this.Height / 2);
				if (cell2.IsEmpty())
				{
					Z.BuildReachableMap(cell2.X, cell2.Y);
				}
				else
				{
					foreach (Cell cell3 in cell2.IterateAdjacent(5, true, true, true))
					{
						if (cell3.IsEmpty())
						{
							Z.BuildReachableMap(cell3.X, cell3.Y);
							break;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x040032F4 RID: 13044
		public string ID;

		// Token: 0x040032F5 RID: 13045
		public int Width = 80;

		// Token: 0x040032F6 RID: 13046
		public int Height = 25;

		// Token: 0x040032F7 RID: 13047
		public int X = -1;

		// Token: 0x040032F8 RID: 13048
		public int Y = -1;

		// Token: 0x040032F9 RID: 13049
		public bool ClearBeforePlace;

		// Token: 0x040032FA RID: 13050
		public bool ClearChasms;

		// Token: 0x040032FB RID: 13051
		public bool CheckEmpty = true;

		// Token: 0x040032FC RID: 13052
		public bool BuildReachability = true;

		// Token: 0x040032FD RID: 13053
		private MapFile _Map;
	}
}
