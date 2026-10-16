using System;

namespace System.Collections.Generic
{
	// Token: 0x02000AC2 RID: 2754
	[Serializable]
	internal class NullableComparer<T> : Comparer<T?> where T : struct, IComparable<T>
	{
		// Token: 0x06006281 RID: 25217 RVA: 0x00149D00 File Offset: 0x00147F00
		public override int Compare(T? x, T? y)
		{
			if (x != null)
			{
				if (y != null)
				{
					return x.value.CompareTo(y.value);
				}
				return 1;
			}
			else
			{
				if (y != null)
				{
					return -1;
				}
				return 0;
			}
		}

		// Token: 0x06006282 RID: 25218 RVA: 0x00149D3B File Offset: 0x00147F3B
		public override bool Equals(object obj)
		{
			return obj is NullableComparer<T>;
		}

		// Token: 0x06006283 RID: 25219 RVA: 0x00149CE6 File Offset: 0x00147EE6
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}
	}
}
