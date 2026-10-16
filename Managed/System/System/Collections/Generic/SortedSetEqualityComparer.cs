using System;

namespace System.Collections.Generic
{
	// Token: 0x020004F1 RID: 1265
	internal sealed class SortedSetEqualityComparer<T> : IEqualityComparer<SortedSet<T>>
	{
		// Token: 0x06002959 RID: 10585 RVA: 0x0008E74B File Offset: 0x0008C94B
		public SortedSetEqualityComparer(IEqualityComparer<T> memberEqualityComparer)
			: this(null, memberEqualityComparer)
		{
		}

		// Token: 0x0600295A RID: 10586 RVA: 0x0008E755 File Offset: 0x0008C955
		private SortedSetEqualityComparer(IComparer<T> comparer, IEqualityComparer<T> memberEqualityComparer)
		{
			this._comparer = comparer ?? Comparer<T>.Default;
			this._memberEqualityComparer = memberEqualityComparer ?? EqualityComparer<T>.Default;
		}

		// Token: 0x0600295B RID: 10587 RVA: 0x0008E77D File Offset: 0x0008C97D
		public bool Equals(SortedSet<T> x, SortedSet<T> y)
		{
			return SortedSet<T>.SortedSetEquals(x, y, this._comparer);
		}

		// Token: 0x0600295C RID: 10588 RVA: 0x0008E78C File Offset: 0x0008C98C
		public int GetHashCode(SortedSet<T> obj)
		{
			int num = 0;
			if (obj != null)
			{
				foreach (T t in obj)
				{
					num ^= this._memberEqualityComparer.GetHashCode(t) & int.MaxValue;
				}
			}
			return num;
		}

		// Token: 0x0600295D RID: 10589 RVA: 0x0008E7F0 File Offset: 0x0008C9F0
		public override bool Equals(object obj)
		{
			SortedSetEqualityComparer<T> sortedSetEqualityComparer = obj as SortedSetEqualityComparer<T>;
			return sortedSetEqualityComparer != null && this._comparer == sortedSetEqualityComparer._comparer;
		}

		// Token: 0x0600295E RID: 10590 RVA: 0x0008E817 File Offset: 0x0008CA17
		public override int GetHashCode()
		{
			return this._comparer.GetHashCode() ^ this._memberEqualityComparer.GetHashCode();
		}

		// Token: 0x04001601 RID: 5633
		private readonly IComparer<T> _comparer;

		// Token: 0x04001602 RID: 5634
		private readonly IEqualityComparer<T> _memberEqualityComparer;
	}
}
