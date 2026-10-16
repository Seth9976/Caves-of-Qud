using System;
using System.Collections.Generic;
using System.Linq;
using Genkit;

namespace XRL.World
{
	// Token: 0x02000985 RID: 2437
	public class MutabilityMap
	{
		// Token: 0x060066F8 RID: 26360 RVA: 0x0020029C File Offset: 0x001FE49C
		public Location2D popMutableLocationInArea(int x1, int y1, int x2, int y2)
		{
			foreach (Location2D location2D in this.mutableLocations.Keys.ToList<Location2D>().ShuffleInPlace<Location2D>(null))
			{
				if (location2D.X >= x1 && location2D.X <= x2 && location2D.Y >= y1 && location2D.Y <= y2)
				{
					this.SetMutable(location2D, 0);
					return location2D;
				}
			}
			return null;
		}

		// Token: 0x060066F9 RID: 26361 RVA: 0x0020032C File Offset: 0x001FE52C
		public Location2D popMutableLocation()
		{
			Location2D randomElement = this.mutableLocations.Keys.GetRandomElement<Location2D>(null);
			this.SetMutable(randomElement, 0);
			return randomElement;
		}

		// Token: 0x060066FA RID: 26362 RVA: 0x00200354 File Offset: 0x001FE554
		public Location2D GetMutableLocationWithTerrain(string Terrain)
		{
			List<Location2D> list;
			if (!this.mutableLocationsByTerrainType.TryGetValue(Terrain, out list))
			{
				return null;
			}
			if (list.Count == 0)
			{
				return null;
			}
			return list.GetRandomElement<Location2D>(null);
		}

		// Token: 0x060066FB RID: 26363 RVA: 0x00200384 File Offset: 0x001FE584
		public void Init(int width, int height)
		{
			this.mutable = new int[width, height];
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					this.mutable[j, i] = 0;
				}
			}
			this.mutableLocations.Clear();
			this.mutableLocationsByTerrainType.Clear();
		}

		// Token: 0x060066FC RID: 26364 RVA: 0x002003DC File Offset: 0x001FE5DC
		public void SetMutable(Location2D location, int value)
		{
			if (value == 0)
			{
				if (this.mutableLocations.ContainsKey(location))
				{
					this.mutableLocations.Remove(location);
				}
				using (Dictionary<string, List<Location2D>>.Enumerator enumerator = this.mutableLocationsByTerrainType.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<string, List<Location2D>> keyValuePair = enumerator.Current;
						keyValuePair.Value.Remove(location);
					}
					goto IL_0076;
				}
			}
			if (!this.mutableLocations.ContainsKey(location))
			{
				this.mutableLocations.Add(location, true);
			}
			IL_0076:
			this.mutable[location.X, location.Y] = value;
		}

		// Token: 0x060066FD RID: 26365 RVA: 0x00200488 File Offset: 0x001FE688
		public bool GetWorldMutable(Location2D Location)
		{
			return this.GetMutable(Location.X * 3 + 1, Location.Y * 3 + 1) > 0;
		}

		// Token: 0x060066FE RID: 26366 RVA: 0x002004A7 File Offset: 0x001FE6A7
		public void SetWorldMutable(Location2D Location, int Mutable)
		{
			this.SetMutable(Location2D.Get(Location.X * 3 + 1, Location.Y * 3 + 1), Mutable);
		}

		// Token: 0x060066FF RID: 26367 RVA: 0x002004CC File Offset: 0x001FE6CC
		public bool GetWorldBlockMutable(Location2D Location)
		{
			for (int i = 0; i <= 2; i++)
			{
				for (int j = 0; j <= 2; j++)
				{
					if (this.GetMutable(Location.X * 3 + j, Location.Y * 3 + i) <= 0)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06006700 RID: 26368 RVA: 0x00200514 File Offset: 0x001FE714
		public void SetWorldBlockMutable(Location2D Location, int Mutable)
		{
			for (int i = 0; i <= 2; i++)
			{
				for (int j = 0; j <= 2; j++)
				{
					this.SetMutable(Location2D.Get(Location.X * 3 + j, Location.Y * 3 + i), Mutable);
				}
			}
		}

		// Token: 0x06006701 RID: 26369 RVA: 0x00200559 File Offset: 0x001FE759
		public int GetMutable(Location2D L)
		{
			return this.GetMutable(L.X, L.Y);
		}

		// Token: 0x06006702 RID: 26370 RVA: 0x0020056D File Offset: 0x001FE76D
		public int GetMutable(int x, int y)
		{
			if (x < 0)
			{
				return 0;
			}
			if (y < 0)
			{
				return 0;
			}
			if (x >= 240)
			{
				return 0;
			}
			if (y >= 75)
			{
				return 0;
			}
			return this.mutable[x, y];
		}

		// Token: 0x06006703 RID: 26371 RVA: 0x0020059C File Offset: 0x001FE79C
		public void RemoveMutableLocation(string zoneID)
		{
			int num;
			int num2;
			int num3;
			int num4;
			if (ZoneID.Parse(zoneID, out num, out num2, out num3, out num4))
			{
				this.RemoveMutableLocation(Location2D.Get(num * 3 + num3, num2 * 3 + num4));
			}
		}

		// Token: 0x06006704 RID: 26372 RVA: 0x002005CE File Offset: 0x001FE7CE
		public void RemoveMutableLocation(Location2D location)
		{
			this.SetMutable(location, 0);
		}

		// Token: 0x06006705 RID: 26373 RVA: 0x002005D8 File Offset: 0x001FE7D8
		public void AddMutableLocation(Location2D location, string terrainType = null, int value = 1)
		{
			this.mutable[location.X, location.Y] = value;
			this.mutableLocations.Add(location, true);
			if (!terrainType.IsNullOrEmpty())
			{
				List<Location2D> list;
				if (!this.mutableLocationsByTerrainType.TryGetValue(terrainType, out list))
				{
					list = new List<Location2D>();
					this.mutableLocationsByTerrainType.Add(terrainType, list);
				}
				list.Add(location);
			}
		}

		// Token: 0x04002F99 RID: 12185
		private int[,] mutable;

		// Token: 0x04002F9A RID: 12186
		private Dictionary<Location2D, bool> mutableLocations = new Dictionary<Location2D, bool>();

		// Token: 0x04002F9B RID: 12187
		private Dictionary<string, List<Location2D>> mutableLocationsByTerrainType = new Dictionary<string, List<Location2D>>();
	}
}
