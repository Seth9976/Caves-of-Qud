using System;
using System.Collections.Generic;
using System.Linq;

namespace Sheeter
{
	// Token: 0x02000317 RID: 791
	public static class SheeterStatExtensions
	{
		// Token: 0x06001D0B RID: 7435 RVA: 0x0008FEE0 File Offset: 0x0008E0E0
		public static double StandardDeviation(this IList<int> sequence)
		{
			List<int> list = sequence.ToList<int>();
			double avg = list.Average<int>((int v) => v);
			return Math.Sqrt(list.Average<int>((int v) => Math.Pow((double)v - avg, 2.0)));
		}

		// Token: 0x06001D0C RID: 7436 RVA: 0x0008FF3C File Offset: 0x0008E13C
		public static double StandardDeviation(this IList<float> sequence)
		{
			List<float> list = sequence.ToList<float>();
			float avg = list.Average<float>((float v) => v);
			return Math.Sqrt(list.Average<float>((float v) => Math.Pow((double)(v - avg), 2.0)));
		}

		// Token: 0x06001D0D RID: 7437 RVA: 0x0008FF98 File Offset: 0x0008E198
		public static double StandardDeviation(this IList<double> sequence)
		{
			List<double> list = sequence.ToList<double>();
			double avg = list.Average<double>((double v) => v);
			return Math.Sqrt(list.Average<double>((double v) => Math.Pow(v - avg, 2.0)));
		}

		// Token: 0x06001D0E RID: 7438 RVA: 0x0008FFF4 File Offset: 0x0008E1F4
		private static int Partition<T>(this IList<T> list, int start, int end, Random rnd = null) where T : IComparable<T>
		{
			if (rnd != null)
			{
				list.Swap<T>(end, rnd.Next(start, end + 1));
			}
			T t = list[end];
			int num = start - 1;
			for (int i = start; i < end; i++)
			{
				T t2 = list[i];
				if (t2.CompareTo(t) <= 0)
				{
					list.Swap<T>(i, ++num);
				}
			}
			list.Swap<T>(end, ++num);
			return num;
		}

		// Token: 0x06001D0F RID: 7439 RVA: 0x0009005F File Offset: 0x0008E25F
		public static T NthOrderStatistic<T>(this IList<T> list, int n, Random rnd = null) where T : IComparable<T>
		{
			return list.NthOrderStatistic<T>(n, 0, list.Count - 1, rnd);
		}

		// Token: 0x06001D10 RID: 7440 RVA: 0x00090074 File Offset: 0x0008E274
		private static T NthOrderStatistic<T>(this IList<T> list, int n, int start, int end, Random rnd) where T : IComparable<T>
		{
			int num;
			for (;;)
			{
				num = list.Partition<T>(start, end, rnd);
				if (num == n)
				{
					break;
				}
				if (n < num)
				{
					end = num - 1;
				}
				else
				{
					start = num + 1;
				}
			}
			return list[num];
		}

		// Token: 0x06001D11 RID: 7441 RVA: 0x000900AC File Offset: 0x0008E2AC
		public static void Swap<T>(this IList<T> list, int i, int j)
		{
			if (i == j)
			{
				return;
			}
			T t = list[i];
			list[i] = list[j];
			list[j] = t;
		}

		// Token: 0x06001D12 RID: 7442 RVA: 0x000900DC File Offset: 0x0008E2DC
		public static T Median<T>(this IList<T> list) where T : IComparable<T>
		{
			return list.NthOrderStatistic<T>((list.Count - 1) / 2, null);
		}

		// Token: 0x06001D13 RID: 7443 RVA: 0x000900F0 File Offset: 0x0008E2F0
		public static double Median<T>(this IEnumerable<T> sequence, Func<T, double> getValue)
		{
			List<double> list = sequence.Select<T, double>(getValue).ToList<double>();
			int num = (list.Count - 1) / 2;
			return list.NthOrderStatistic<double>(num, null);
		}

		// Token: 0x06001D14 RID: 7444 RVA: 0x0009011C File Offset: 0x0008E31C
		public static List<U> Map<T, U>(this List<T> sequence, Func<T, U> mapper)
		{
			List<U> list = new List<U>(sequence.Count);
			foreach (T t in sequence)
			{
				list.Add(mapper(t));
			}
			return list;
		}
	}
}
