using System;
using System.Collections.Generic;
using XRL.Collections;
using XRL.Rules;
using XRL.World;

namespace XRL
{
	// Token: 0x020004FE RID: 1278
	public class Tools
	{
		// Token: 0x06003715 RID: 14101 RVA: 0x0010A668 File Offset: 0x00108868
		public static bool BoxOverlap(Box Box1, Box Box2)
		{
			return Box1.x1 <= Box2.x2 && Box1.x2 >= Box2.x1 && Box1.y1 <= Box2.y2 && Box1.y2 >= Box2.y1;
		}

		// Token: 0x06003716 RID: 14102 RVA: 0x0010A6B8 File Offset: 0x001088B8
		public static List<Box> GenerateBoxes(BoxGenerateOverlap Overlap, Range NumberOfBoxees, Range Width, Range Height, Range Volume, Range XRange = null, Range YRange = null)
		{
			return Tools.GenerateBoxes(new List<Box>(), Overlap, NumberOfBoxees, Width, Height, Volume, (XRange == null) ? new Range(0, 79) : XRange, (YRange == null) ? new Range(0, 24) : YRange);
		}

		// Token: 0x06003717 RID: 14103 RVA: 0x0010A6F8 File Offset: 0x001088F8
		public static List<Box> GenerateBoxes(Box OutOfBounds, BoxGenerateOverlap Overlap, Range NumberOfBoxees, Range Width, Range Height, Range Volume)
		{
			return Tools.GenerateBoxes(new List<Box> { OutOfBounds }, Overlap, NumberOfBoxees, Width, Height, Volume, new Range(0, 79), new Range(0, 24));
		}

		// Token: 0x06003718 RID: 14104 RVA: 0x0010A730 File Offset: 0x00108930
		public static List<Box> GenerateBoxes(List<Box> OutOfBounds, BoxGenerateOverlap Overlap, Range NumberOfBoxees, Range Width, Range Height, Range Volume, Range XRange, Range YRange)
		{
			List<Box> list = new List<Box>();
			for (;;)
			{
				IL_0006:
				int num = Stat.Random(NumberOfBoxees.Min, NumberOfBoxees.Max);
				list.Clear();
				for (int i = 0; i < num; i++)
				{
					int j = 5000;
					IL_0144:
					while (j > 0)
					{
						Box box;
						for (;;)
						{
							IL_0030:
							j--;
							box = new Box(Stat.Random(XRange.Min, XRange.Max), Stat.Random(YRange.Min, YRange.Max), Stat.Random(XRange.Min, XRange.Max), Stat.Random(YRange.Min, YRange.Max));
							if (box.Width >= Width.Min && box.Width <= Width.Max && box.Height >= Height.Min && box.Height <= Height.Max && (Volume == null || (box.Width * box.Height >= Volume.Min && box.Width * box.Height <= Volume.Max)))
							{
								using (List<Box>.Enumerator enumerator = OutOfBounds.GetEnumerator())
								{
									while (enumerator.MoveNext())
									{
										if (Tools.BoxOverlap(enumerator.Current, box))
										{
											goto IL_0030;
										}
									}
								}
								break;
							}
							goto IL_0144;
						}
						list.Add(box);
						break;
					}
				}
				if (Overlap == BoxGenerateOverlap.Irrelevant)
				{
					break;
				}
				foreach (Box box2 in list)
				{
					foreach (Box box3 in list)
					{
						if (box2 != box3)
						{
							if (Overlap == BoxGenerateOverlap.AlwaysOverlap && !Tools.BoxOverlap(box2, box3))
							{
								goto IL_0006;
							}
							if (Overlap == BoxGenerateOverlap.NeverOverlap && Tools.BoxOverlap(box2, box3))
							{
								goto IL_0006;
							}
						}
					}
				}
				return list;
			}
			return list;
		}

		// Token: 0x06003719 RID: 14105 RVA: 0x0010A948 File Offset: 0x00108B48
		public static void Box(Zone Z, Box B, string Blueprint, int Chance)
		{
			for (int i = B.y1; i <= B.y2; i++)
			{
				for (int j = B.x1; j <= B.x2; j++)
				{
					if ((j == B.x1 || j == B.x2 || i == B.y1 || i == B.y2) && Stat.Random(1, 100) <= Chance)
					{
						Z.GetCell(j, i).AddObject(GameObjectFactory.Factory.CreateObject(Blueprint), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					}
				}
			}
		}

		// Token: 0x0600371A RID: 14106 RVA: 0x0010A9D8 File Offset: 0x00108BD8
		public static void FillBox(Zone Z, Box B, string Blueprint)
		{
			for (int i = B.y1; i <= B.y2; i++)
			{
				for (int j = B.x1; j <= B.x2; j++)
				{
					Z.GetCell(j, i).AddObject(GameObjectFactory.Factory.CreateObject(Blueprint), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				}
			}
		}

		// Token: 0x0600371B RID: 14107 RVA: 0x0010AA3C File Offset: 0x00108C3C
		public static void FillBox(Zone Z, Box B, string Blueprint, int Chance)
		{
			for (int i = B.y1; i <= B.y2; i++)
			{
				for (int j = B.x1; j <= B.x2; j++)
				{
					if (Z.GetCell(j, i).IsEmpty() && Stat.Random(1, 100) <= Chance)
					{
						Z.GetCell(j, i).AddObject(GameObjectFactory.Factory.CreateObject(Blueprint), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					}
				}
			}
		}

		// Token: 0x0600371C RID: 14108 RVA: 0x0010AAB8 File Offset: 0x00108CB8
		public static void ClearBox(Zone Z, Box B)
		{
			for (int i = B.x1; i <= B.x2; i++)
			{
				for (int j = B.y1; j <= B.y2; j++)
				{
					Z.GetCell(i, j).Clear(null, false, false, null);
				}
			}
		}

		// Token: 0x0600371D RID: 14109 RVA: 0x0010AB04 File Offset: 0x00108D04
		public static void ScanFill(int StartX, int StartY, int Width, int Height, Func<int, int, bool> Get, Action<int, int> Set, bool Diagonal = false)
		{
			if (!Get(StartX, StartY))
			{
				return;
			}
			bool[,] array = Tools.FillVisited;
			RingDeque<ValueTuple<int, int, int, int>> ringDeque = Tools.FillQueue;
			if (array == null)
			{
				array = (Tools.FillVisited = new bool[Width, Height]);
				ringDeque = (Tools.FillQueue = new RingDeque<ValueTuple<int, int, int, int>>(16));
			}
			else
			{
				ringDeque.Clear();
				int upperBound = array.GetUpperBound(0);
				int upperBound2 = array.GetUpperBound(1);
				if (upperBound < Width || upperBound2 < Height)
				{
					array = (Tools.FillVisited = new bool[Math.Max(upperBound, Width), Math.Max(upperBound2, Height)]);
				}
				else
				{
					Array.Clear(array, 0, array.Length);
				}
			}
			ringDeque.Enqueue(new ValueTuple<int, int, int, int>(StartX, StartX, StartY, 1));
			ringDeque.Enqueue(new ValueTuple<int, int, int, int>(StartX, StartX, StartY - 1, -1));
			int num = Width - 1;
			int num2 = Height - 1;
			int num3 = (Diagonal ? 0 : 1);
			ValueTuple<int, int, int, int> valueTuple;
			while (ringDeque.TryDequeue(out valueTuple))
			{
				ValueTuple<int, int, int, int> valueTuple2 = valueTuple;
				int i = valueTuple2.Item1;
				int num4 = valueTuple2.Item2;
				int item = valueTuple2.Item3;
				int item2 = valueTuple2.Item4;
				if (item >= 0 && item <= num2)
				{
					if (Diagonal)
					{
						if (i > 0)
						{
							i--;
						}
						if (num4 < num)
						{
							num4++;
						}
					}
					int num5 = i;
					while (num5 >= 0 && !array[num5, item])
					{
						array[num5, item] = true;
						if (!Get(num5, item))
						{
							break;
						}
						Set(num5, item);
						num5--;
					}
					num5++;
					i++;
					if (num5 < i)
					{
						int num6 = num5;
						if (num6 < i - num3)
						{
							ringDeque.Enqueue(new ValueTuple<int, int, int, int>(num6, i - 1, item - item2, -item2));
						}
						num5 = i;
						while (num5 <= num && !array[num5, item])
						{
							array[num5, item] = true;
							if (!Get(num5, item))
							{
								break;
							}
							Set(num5, item);
							num5++;
						}
						ringDeque.Enqueue(new ValueTuple<int, int, int, int>(num6, num5 - 1, item + item2, item2));
						i = num5 + 1;
						if (num5 - num3 > num4)
						{
							ringDeque.Enqueue(new ValueTuple<int, int, int, int>(num4, num5 - 1, item - item2, -item2));
						}
					}
					num5 = num4;
					while (num5 <= num && !array[num5, item])
					{
						array[num5, item] = true;
						if (!Get(num5, item))
						{
							break;
						}
						Set(num5, item);
						num5++;
					}
					num5--;
					num4--;
					if (num5 > num4)
					{
						int num6 = num5;
						if (num6 > num4 + num3)
						{
							ringDeque.Enqueue(new ValueTuple<int, int, int, int>(num4 + 1, num6, item - item2, -item2));
						}
						num5 = num4;
						while (num5 >= i && !array[num5, item])
						{
							array[num5, item] = true;
							if (!Get(num5, item))
							{
								break;
							}
							Set(num5, item);
							num5--;
						}
						if (num6 > num5)
						{
							ringDeque.Enqueue(new ValueTuple<int, int, int, int>(num5 + 1, num6, item + item2, item2));
						}
						num4 = num5 - 1;
					}
					while (i <= num4)
					{
						if (num5 > num)
						{
							break;
						}
						num5 = i;
						while (num5 <= num4 && !array[num5, item])
						{
							array[num5, item] = true;
							if (!Get(num5, item))
							{
								break;
							}
							Set(num5, item);
							num5++;
						}
						if (num5 > i)
						{
							ringDeque.Enqueue(new ValueTuple<int, int, int, int>(i, num5 - 1, item + item2, item2));
						}
						i = num5 + 1;
					}
				}
			}
		}

		// Token: 0x04001B6B RID: 7019
		[ThreadStatic]
		private static bool[,] FillVisited;

		// Token: 0x04001B6C RID: 7020
		[ThreadStatic]
		private static RingDeque<ValueTuple<int, int, int, int>> FillQueue;
	}
}
