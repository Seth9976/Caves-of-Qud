using System;

namespace System.Collections.Generic
{
	// Token: 0x02000AC1 RID: 2753
	[Serializable]
	internal class GenericComparer<T> : Comparer<T> where T : IComparable<T>
	{
		// Token: 0x0600627D RID: 25213 RVA: 0x00149CAD File Offset: 0x00147EAD
		public override int Compare(T x, T y)
		{
			if (x != null)
			{
				if (y != null)
				{
					return x.CompareTo(y);
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

		// Token: 0x0600627E RID: 25214 RVA: 0x00149CDB File Offset: 0x00147EDB
		public override bool Equals(object obj)
		{
			return obj is GenericComparer<T>;
		}

		// Token: 0x0600627F RID: 25215 RVA: 0x00149CE6 File Offset: 0x00147EE6
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}
	}
}
