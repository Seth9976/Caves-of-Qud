using System;

namespace System.Collections.Generic
{
	// Token: 0x02000AC8 RID: 2760
	[Serializable]
	internal class ObjectEqualityComparer<T> : EqualityComparer<T>
	{
		// Token: 0x060062A2 RID: 25250 RVA: 0x0014A262 File Offset: 0x00148462
		public override bool Equals(T x, T y)
		{
			if (x != null)
			{
				return y != null && x.Equals(y);
			}
			return y == null;
		}

		// Token: 0x060062A3 RID: 25251 RVA: 0x0014A00E File Offset: 0x0014820E
		public override int GetHashCode(T obj)
		{
			if (obj == null)
			{
				return 0;
			}
			return obj.GetHashCode();
		}

		// Token: 0x060062A4 RID: 25252 RVA: 0x0014A298 File Offset: 0x00148498
		internal override int IndexOf(T[] array, T value, int startIndex, int count)
		{
			int num = startIndex + count;
			if (value == null)
			{
				for (int i = startIndex; i < num; i++)
				{
					if (array[i] == null)
					{
						return i;
					}
				}
			}
			else
			{
				for (int j = startIndex; j < num; j++)
				{
					if (array[j] != null && array[j].Equals(value))
					{
						return j;
					}
				}
			}
			return -1;
		}

		// Token: 0x060062A5 RID: 25253 RVA: 0x0014A30C File Offset: 0x0014850C
		internal override int LastIndexOf(T[] array, T value, int startIndex, int count)
		{
			int num = startIndex - count + 1;
			if (value == null)
			{
				for (int i = startIndex; i >= num; i--)
				{
					if (array[i] == null)
					{
						return i;
					}
				}
			}
			else
			{
				for (int j = startIndex; j >= num; j--)
				{
					if (array[j] != null && array[j].Equals(value))
					{
						return j;
					}
				}
			}
			return -1;
		}

		// Token: 0x060062A6 RID: 25254 RVA: 0x0014A37F File Offset: 0x0014857F
		public override bool Equals(object obj)
		{
			return obj is ObjectEqualityComparer<T>;
		}

		// Token: 0x060062A7 RID: 25255 RVA: 0x00149CE6 File Offset: 0x00147EE6
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}
	}
}
