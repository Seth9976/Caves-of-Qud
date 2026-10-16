using System;

namespace System.Collections.Generic
{
	// Token: 0x02000AC4 RID: 2756
	[Serializable]
	internal class ComparisonComparer<T> : Comparer<T>
	{
		// Token: 0x06006289 RID: 25225 RVA: 0x00149D71 File Offset: 0x00147F71
		public ComparisonComparer(Comparison<T> comparison)
		{
			this._comparison = comparison;
		}

		// Token: 0x0600628A RID: 25226 RVA: 0x00149D80 File Offset: 0x00147F80
		public override int Compare(T x, T y)
		{
			return this._comparison(x, y);
		}

		// Token: 0x04003A35 RID: 14901
		private readonly Comparison<T> _comparison;
	}
}
