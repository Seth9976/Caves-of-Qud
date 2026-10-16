using System;

namespace System.Collections.Generic
{
	// Token: 0x02000AC3 RID: 2755
	[Serializable]
	internal class ObjectComparer<T> : Comparer<T>
	{
		// Token: 0x06006285 RID: 25221 RVA: 0x00149D4E File Offset: 0x00147F4E
		public override int Compare(T x, T y)
		{
			return Comparer.Default.Compare(x, y);
		}

		// Token: 0x06006286 RID: 25222 RVA: 0x00149D66 File Offset: 0x00147F66
		public override bool Equals(object obj)
		{
			return obj is ObjectComparer<T>;
		}

		// Token: 0x06006287 RID: 25223 RVA: 0x00149CE6 File Offset: 0x00147EE6
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}
	}
}
