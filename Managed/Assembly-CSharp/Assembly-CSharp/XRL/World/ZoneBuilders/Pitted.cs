using System;
using System.Collections.Generic;
using Genkit;
using XRL.Rules;
using XRL.World.Parts;

namespace XRL.World.ZoneBuilders
{
	/// <summary>
	/// Should typically be added as a builder to zones in a single column on Z level 10 through 13 of an area that will be pitted.
	/// </summary>
	// Token: 0x02000AD8 RID: 2776
	public class Pitted : ZoneBuilderSandbox
	{
		// Token: 0x06006FE6 RID: 28646 RVA: 0x00277E1C File Offset: 0x0027601C
		public bool BuildZone(Zone Z)
		{
			return this.BuildZone(Z, new List<Location2D>(), new List<Location2D>());
		}

		// Token: 0x06006FE7 RID: 28647 RVA: 0x00277E30 File Offset: 0x00276030
		public bool BuildZone(Zone Z, List<Location2D> pitCellsOut, List<Location2D> centerCellsOut)
		{
			return Pitted.BuildPits(Z, this.MinWells, this.MaxWells, this.MinRadius, this.MaxRadius, this.XMargin, this.YMargin, pitCellsOut, centerCellsOut, this.PitTop, this.PitDepth, this.Liquid, this.PitDetailsRandom, null, "Pit", null, this.Lazy);
		}

		// Token: 0x06006FE8 RID: 28648 RVA: 0x00277E90 File Offset: 0x00276090
		public static bool BuildPits(Zone Z, int MinWells, int MaxWells, int MinRadius, int MaxRadius, int XMargin, int YMargin, List<Location2D> pitCellsOut, List<Location2D> centerCellsOut, int PitTop = 10, int PitDepth = 2, string Liquid = "SaltyWaterExtraDeepPool", bool PitDetailsRandom = false, string CenterConnectionType = null, string PitObject = "Pit", IList<Location2D> Avoid = null, bool Lazy = false)
		{
			if (Z.Z == 10)
			{
				Z.GetCell(0, 0).RequireObject("DaylightWidget", null, null, null, null);
			}
			int num = ZoneBuilderSandbox.GetOracleIntColumn(Z, "nwells", MinWells, MaxWells);
			if (PitDetailsRandom)
			{
				num = Stat.Random(MinWells, MaxWells);
			}
			List<ValueTuple<Location2D, int>> list = new List<ValueTuple<Location2D, int>>();
			int i = 0;
			while (i < num)
			{
				int num2 = ZoneBuilderSandbox.GetOracleIntColumn(Z, "well" + i.ToString() + "_r", MinRadius, MaxRadius);
				int num3 = ZoneBuilderSandbox.GetOracleIntColumn(Z, "well" + i.ToString() + "_x", XMargin + num2 * 2, 80 - XMargin - num2 * 2);
				int num4 = ZoneBuilderSandbox.GetOracleIntColumn(Z, "well" + i.ToString() + "_y", YMargin + num2, 24 - YMargin - num2);
				if (PitDetailsRandom)
				{
					num2 = Stat.Random(MinRadius, MaxRadius);
					num3 = Stat.Random(XMargin + num2 * 2, 80 - XMargin - num2 * 2);
					num4 = Stat.Random(YMargin + num2, 24 - YMargin - num2);
				}
				if (Avoid.IsNullOrEmpty<Location2D>())
				{
					goto IL_0144;
				}
				bool flag = false;
				int j = 0;
				int count = Avoid.Count;
				while (j < count)
				{
					if (Avoid[j].Distance(num3, num4) <= MaxRadius)
					{
						flag = true;
						break;
					}
					j++;
				}
				if (!flag || num >= MaxWells * 5)
				{
					goto IL_0144;
				}
				num++;
				IL_0190:
				i++;
				continue;
				IL_0144:
				Cell cell = Z.GetCell(num3, num4);
				if (centerCellsOut != null)
				{
					centerCellsOut.Add(cell.Location);
				}
				list.Add(new ValueTuple<Location2D, int>(cell.Location, num2));
				if (!CenterConnectionType.IsNullOrEmpty())
				{
					Z.AddZoneConnection("d", num3, num4, CenterConnectionType, null);
					goto IL_0190;
				}
				goto IL_0190;
			}
			foreach (ValueTuple<Location2D, int> valueTuple in list)
			{
				int x = valueTuple.Item1.X;
				int y = valueTuple.Item1.Y;
				int item = valueTuple.Item2;
				List<Cell> list2 = new List<Cell>();
				for (int k = -(item * 2); k <= item * 2; k++)
				{
					for (int l = -item; l <= item; l++)
					{
						Cell cell2 = Z.GetCell(x + k, y + l);
						if (cell2 != null && cell2.CosmeticDistanceTo(x, y) <= item - (Z.Z - PitTop))
						{
							cell2.Clear(null, false, false, null);
							cell2.AddObject("FlyingWhitelistArea", null, null, null, null);
							list2.Add(cell2);
							if (Z.Z <= PitTop + PitDepth)
							{
								GameObject gameObject = GameObjectFactory.Factory.CreateObject(PitObject);
								gameObject.GetPart<StairsDown>().ConnectLanding = false;
								cell2.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
								pitCellsOut.Add(cell2.Location);
							}
							else
							{
								cell2.AddObject(Liquid, null, null, null, null);
							}
						}
					}
				}
				for (int m = -MaxRadius; m <= MaxRadius; m++)
				{
					for (int n = -(MaxRadius * 2); n <= MaxRadius * 2; n++)
					{
						Cell cell3 = Z.GetCell(x + n, y + m);
						if (cell3 != null && cell3.CosmeticDistanceTo(x, y) <= MaxRadius)
						{
							cell3.AddObject("StairBlocker", null, null, null, null);
							cell3.AddObject("InfluenceMapBlocker", null, null, null, null);
						}
					}
				}
				if (pitCellsOut.Count == 0)
				{
					pitCellsOut.Add(valueTuple.Item1);
				}
			}
			if (Lazy)
			{
				foreach (GameObject gameObject2 in Z.IterateObjects())
				{
					PitMaterial pitMaterial;
					if (gameObject2.TryGetPart<PitMaterial>(out pitMaterial))
					{
						pitMaterial.Lazy = true;
					}
					ChasmMaterial chasmMaterial;
					if (gameObject2.TryGetPart<ChasmMaterial>(out chasmMaterial))
					{
						pitMaterial.Lazy = true;
					}
				}
			}
			return true;
		}

		// Token: 0x04003379 RID: 13177
		public int MinWells = 3;

		// Token: 0x0400337A RID: 13178
		public int MaxWells = 6;

		// Token: 0x0400337B RID: 13179
		public int MinRadius = 3;

		// Token: 0x0400337C RID: 13180
		public int MaxRadius = 10;

		// Token: 0x0400337D RID: 13181
		public int XMargin = 4;

		// Token: 0x0400337E RID: 13182
		public int YMargin = 2;

		// Token: 0x0400337F RID: 13183
		public int PitTop = 10;

		// Token: 0x04003380 RID: 13184
		public int PitDepth = 2;

		// Token: 0x04003381 RID: 13185
		public bool PitDetailsRandom;

		// Token: 0x04003382 RID: 13186
		public string Liquid = "SaltyWaterExtraDeepPool";

		// Token: 0x04003383 RID: 13187
		public string CenterConnectionType;

		// Token: 0x04003384 RID: 13188
		public bool Lazy;
	}
}
