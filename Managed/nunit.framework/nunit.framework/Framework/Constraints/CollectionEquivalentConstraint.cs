using System;
using System.Collections;

namespace NUnit.Framework.Constraints
{
	// Token: 0x020000F0 RID: 240
	public class CollectionEquivalentConstraint : CollectionItemsEqualConstraint
	{
		// Token: 0x06000760 RID: 1888 RVA: 0x00011F1B File Offset: 0x0001011B
		public CollectionEquivalentConstraint(IEnumerable expected)
			: base(expected)
		{
			this._expected = expected;
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x00011F2B File Offset: 0x0001012B
		public override string DisplayName
		{
			get
			{
				return "Equivalent";
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000762 RID: 1890 RVA: 0x00011F32 File Offset: 0x00010132
		public override string Description
		{
			get
			{
				return "equivalent to " + MsgUtils.FormatValue(this._expected);
			}
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x00011F4C File Offset: 0x0001014C
		protected override bool Matches(IEnumerable actual)
		{
			if (this._expected is ICollection && actual is ICollection && ((ICollection)actual).Count != ((ICollection)this._expected).Count)
			{
				return false;
			}
			CollectionTally collectionTally = base.Tally(this._expected);
			return collectionTally.TryRemove(actual) && collectionTally.Count == 0;
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x00011FAE File Offset: 0x000101AE
		public CollectionEquivalentConstraint Using<TActual, TExpected>(Func<TActual, TExpected, bool> comparison)
		{
			base.Using(EqualityAdapter.For<TActual, TExpected>(comparison));
			return this;
		}

		// Token: 0x0400019C RID: 412
		private readonly IEnumerable _expected;
	}
}
