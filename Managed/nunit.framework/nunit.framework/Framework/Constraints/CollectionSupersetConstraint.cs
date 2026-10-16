using System;
using System.Collections;

namespace NUnit.Framework.Constraints
{
	// Token: 0x020000F4 RID: 244
	public class CollectionSupersetConstraint : CollectionItemsEqualConstraint
	{
		// Token: 0x06000782 RID: 1922 RVA: 0x000125BD File Offset: 0x000107BD
		public CollectionSupersetConstraint(IEnumerable expected)
			: base(expected)
		{
			this._expected = expected;
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000783 RID: 1923 RVA: 0x000125CD File Offset: 0x000107CD
		public override string DisplayName
		{
			get
			{
				return "SupersetOf";
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x000125D4 File Offset: 0x000107D4
		public override string Description
		{
			get
			{
				return "superset of " + MsgUtils.FormatValue(this._expected);
			}
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x000125EB File Offset: 0x000107EB
		protected override bool Matches(IEnumerable actual)
		{
			return base.Tally(actual).TryRemove(this._expected);
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x00012600 File Offset: 0x00010800
		public CollectionSupersetConstraint Using<TSupersetType, TSubsetType>(Func<TSupersetType, TSubsetType, bool> comparison)
		{
			Func<TSubsetType, TSupersetType, bool> func = (TSubsetType actual, TSupersetType expected) => comparison(expected, actual);
			base.Using(EqualityAdapter.For<TSubsetType, TSupersetType>(func));
			return this;
		}

		// Token: 0x040001A1 RID: 417
		private IEnumerable _expected;
	}
}
