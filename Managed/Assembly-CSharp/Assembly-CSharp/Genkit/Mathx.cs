using System;

namespace Genkit
{
	// Token: 0x02000388 RID: 904
	public static class Mathx
	{
		// Token: 0x06002202 RID: 8706 RVA: 0x000AA1AC File Offset: 0x000A83AC
		public static T Min<T>(T[] d, T max) where T : IComparable<T>
		{
			T t = max;
			for (int i = 0; i < d.Length; i++)
			{
				if (d[i].CompareTo(t) < 0)
				{
					t = d[i];
				}
			}
			return t;
		}

		// Token: 0x06002203 RID: 8707 RVA: 0x000AA1EC File Offset: 0x000A83EC
		public static T Min<T>(T[,] d, T max) where T : IComparable<T>
		{
			T t = max;
			for (int i = 0; i <= d.GetUpperBound(0); i++)
			{
				for (int j = 0; j <= d.GetUpperBound(1); j++)
				{
					if (d[i, j].CompareTo(t) < 0)
					{
						t = d[i, j];
					}
				}
			}
			return t;
		}

		// Token: 0x06002204 RID: 8708 RVA: 0x000AA244 File Offset: 0x000A8444
		public static T Min<T>(T[,,] d, T max) where T : IComparable<T>
		{
			T t = max;
			for (int i = 0; i <= d.GetUpperBound(0); i++)
			{
				for (int j = 0; j <= d.GetUpperBound(1); j++)
				{
					for (int k = 0; k <= d.GetUpperBound(2); k++)
					{
						if (d[i, j, k].CompareTo(t) < 0)
						{
							t = d[i, j, k];
						}
					}
				}
			}
			return t;
		}

		// Token: 0x06002205 RID: 8709 RVA: 0x000AA2B0 File Offset: 0x000A84B0
		public static T Max<T>(T[] d, T min) where T : IComparable<T>
		{
			T t = min;
			for (int i = 0; i < d.Length; i++)
			{
				if (d[i].CompareTo(t) > 0)
				{
					t = d[i];
				}
			}
			return t;
		}

		// Token: 0x06002206 RID: 8710 RVA: 0x000AA2F0 File Offset: 0x000A84F0
		public static T Max<T>(T[,] d, T min) where T : IComparable<T>
		{
			T t = min;
			for (int i = 0; i < d.GetUpperBound(0); i++)
			{
				for (int j = 0; j < d.GetUpperBound(1); j++)
				{
					if (d[i, j].CompareTo(t) > 0)
					{
						t = d[i, j];
					}
				}
			}
			return t;
		}

		// Token: 0x06002207 RID: 8711 RVA: 0x000AA348 File Offset: 0x000A8548
		public static T Max<T>(T[,,] d, T min) where T : IComparable<T>
		{
			T t = min;
			for (int i = 0; i < d.GetUpperBound(0); i++)
			{
				for (int j = 0; j < d.GetUpperBound(1); j++)
				{
					for (int k = 0; k < d.GetUpperBound(2); k++)
					{
						if (d[i, j, k].CompareTo(t) > 0)
						{
							t = d[i, j, k];
						}
					}
				}
			}
			return t;
		}
	}
}
