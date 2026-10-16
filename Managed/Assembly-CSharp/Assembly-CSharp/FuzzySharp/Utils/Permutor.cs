using System;
using System.Collections.Generic;
using System.Linq;

namespace FuzzySharp.Utils
{
	// Token: 0x020001AC RID: 428
	public class Permutor<T> where T : IComparable<T>
	{
		// Token: 0x0600103E RID: 4158 RVA: 0x00064FF7 File Offset: 0x000631F7
		public Permutor(IEnumerable<T> set)
		{
			this._set = set.ToList<T>();
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x0006500C File Offset: 0x0006320C
		public List<T> PermutationAt(long i)
		{
			List<T> list = new List<T>(this._set.OrderBy<T, T>((T e) => e).ToList<T>());
			for (long num = 0L; num < i - 1L; num += 1L)
			{
				this.NextPermutation(list);
			}
			return list;
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x00065068 File Offset: 0x00063268
		public List<T> NextPermutation()
		{
			this.NextPermutation(this._set);
			return this._set;
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x00065080 File Offset: 0x00063280
		public bool NextPermutation(List<T> set)
		{
			int i;
			for (i = set.Count - 1; i > 0; i--)
			{
				T t = set[i - 1];
				if (t.CompareTo(set[i]) < 0)
				{
					break;
				}
			}
			if (i <= 0)
			{
				return false;
			}
			int num = set.Count - 1;
			for (;;)
			{
				T t = set[num];
				if (t.CompareTo(set[i - 1]) > 0)
				{
					break;
				}
				num--;
			}
			T t2 = set[i - 1];
			set[i - 1] = set[num];
			set[num] = t2;
			num = set.Count - 1;
			while (i < num)
			{
				t2 = set[i];
				set[i] = set[num];
				set[num] = t2;
				i++;
				num--;
			}
			return true;
		}

		// Token: 0x04000B03 RID: 2819
		private readonly List<T> _set;
	}
}
