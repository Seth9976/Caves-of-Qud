using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.World.Parts;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AB4 RID: 2740
	public class StarappleFarm
	{
		// Token: 0x06006F65 RID: 28517 RVA: 0x0026DD64 File Offset: 0x0026BF64
		public bool InAnyBox(int x, int y, List<Box> Boxes, int Grow)
		{
			using (List<Box>.Enumerator enumerator = Boxes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Grow(Grow).contains(x, y))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06006F66 RID: 28518 RVA: 0x0026DDC4 File Offset: 0x0026BFC4
		public bool BuildZone(Zone Z, bool ClearCombatObjectsFirst, string WallObject, string ZoneTable, string Widgets)
		{
			List<Box> list = null;
			int i = 10;
			while (i > 0)
			{
				list = Tools.GenerateBoxes(BoxGenerateOverlap.Irrelevant, new Range(1, 3), new Range(40, 60), new Range(8, 16), null, new Range(1, 78), new Range(1, 23));
				if (list == null)
				{
					return false;
				}
				if (list.Count == 0)
				{
					return false;
				}
				foreach (Box box in list)
				{
					for (int j = box.y1; j <= box.y2; j++)
					{
						for (int k = box.x1; k <= box.x2; k++)
						{
							if (Z.IsReachable(k, j))
							{
								goto IL_00CB;
							}
						}
					}
				}
				i--;
				continue;
				IL_00CB:
				foreach (Box box2 in list)
				{
					Z.ClearBox(box2, null);
					for (int l = box2.x1; l <= box2.x2; l++)
					{
						for (int m = box2.y1; m < box2.y2; m++)
						{
							Grassy.PaintCell(Z.GetCell(l, m));
						}
					}
					Box box3 = box2.Grow(-2);
					for (int n = box3.x1 + Stat.Random(1, 3); n <= box3.x2; n++)
					{
						for (int num = box3.y1; num <= box3.y2; num += Stat.Random(1, 2))
						{
							if (Stat.Random(1, 100) <= 45)
							{
								Z.GetCell(n, num).ClearObjectsWithPart("Combat", false, false);
								if (Z.GetCell(n, num).IsEmpty())
								{
									Z.GetCell(n, num).AddObject("Starapple Farm Tree", null, null, null, null);
								}
							}
						}
						n += Stat.Random(0, 2);
					}
					int num2 = Stat.Random(1, 4);
					int num3 = Stat.Random(box2.x1, box2.x2);
					int num4 = num3 + Stat.Random(1, 8);
					int num5 = Stat.Random(box2.y1, box2.y2);
					int num6 = num3 + Stat.Random(1, 8);
					for (int num7 = box2.x1; num7 <= box2.x2; num7++)
					{
						if (!this.InAnyBox(num7, box2.y1, list, -1) && (num2 != 0 || num7 < num3 || num7 > num4))
						{
							this.Fence(Z.GetCell(num7, box2.y1));
						}
						if (!this.InAnyBox(num7, box2.y2, list, -1) && (num2 != 1 || num7 < num3 || num7 > num4))
						{
							this.Fence(Z.GetCell(num7, box2.y2));
						}
					}
					for (int num8 = box2.y1; num8 <= box2.y2; num8++)
					{
						if (!this.InAnyBox(box2.x1, num8, list, -1) && (num2 != 2 || num8 < num5 || num8 > num6))
						{
							this.Fence(Z.GetCell(box2.x1, num8));
						}
						if (!this.InAnyBox(box2.x2, num8, list, -1) && (num2 != 3 || num8 < num5 || num8 > num6))
						{
							this.Fence(Z.GetCell(box2.x2, num8));
						}
					}
				}
				List<Cell> list2 = new List<Cell>();
				for (int num9 = 0; num9 < 80; num9++)
				{
					for (int num10 = 0; num10 < 24; num10++)
					{
						if (Z.GetCell(num9, num10).HasObjectWithBlueprint("BrinestalkFence"))
						{
							list2.Add(Z.GetCell(num9, num10));
						}
					}
				}
				if (list2.Count > 0)
				{
					int num11 = 0;
					int num12 = Stat.Random(1, 3);
					while (num11 < num12)
					{
						int num13 = Stat.Random(0, list2.Count - 1);
						list2[num13].Clear(null, false, false, null);
						list2[num13].AddObject("Brinestalk Gate", null, null, null, null);
						num11++;
					}
				}
				new VillageMaker().BuildZone(Z, true, "PigskinWall", true, "1d2", "5-Cistern", "StarappleFarmYurt", "StarappleFarmGlobals", null, true);
				Z.GetCell(0, 0).RequireObject("Dirty", null, null, null, null);
				return true;
			}
			if (i <= 0)
			{
				return true;
			}
			goto IL_00CB;
		}

		// Token: 0x06006F67 RID: 28519 RVA: 0x0026E258 File Offset: 0x0026C458
		public void Fence(Cell C)
		{
			C.Clear(null, false, false, null);
			C.AddObject("BrinestalkFence", null, null, null, null);
		}
	}
}
