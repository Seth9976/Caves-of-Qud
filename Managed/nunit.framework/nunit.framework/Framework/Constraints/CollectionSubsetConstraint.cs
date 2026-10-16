using System;
using System.Collections;

namespace NUnit.Framework.Constraints
{
	// Token: 0x020000F3 RID: 243
	public class CollectionSubsetConstraint : CollectionItemsEqualConstraint
	{
		// Token: 0x0600077D RID: 1917 RVA: 0x0001256B File Offset: 0x0001076B
		public CollectionSubsetConstraint(IEnumerable expected)
			: base(expected)
		{
			this._expected = expected;
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x0600077E RID: 1918 RVA: 0x0001257B File Offset: 0x0001077B
		public override string DisplayName
		{
			get
			{
				return "SubsetOf";
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x0600077F RID: 1919 RVA: 0x00012582 File Offset: 0x00010782
		public override string Description
		{
			get
			{
				return "subset of " + MsgUtils.FormatValue(this._expected);
			}
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x00012599 File Offset: 0x00010799
		protected override bool Matches(IEnumerable actual)
		{
			return base.Tally(this._expected).TryRemove(actual);
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x000125AD File Offset: 0x000107AD
		public CollectionSubsetConstraint Using<TSubsetType, TSupersetType>(Func<TSubsetType, TSupersetType, bool> comparison)
		{
			base.Using(EqualityAdapter.For<TSubsetType, TSupersetType>(comparison));
			return this;
		}

		// Token: 0x040001A0 RID: 416
		private IEnumerable _expected;
	}
}
