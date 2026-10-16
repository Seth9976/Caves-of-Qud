using System;
using System.Collections;

namespace NUnit.Framework.Constraints
{
	// Token: 0x020000EF RID: 239
	public class CollectionContainsConstraint : CollectionItemsEqualConstraint
	{
		// Token: 0x06000759 RID: 1881 RVA: 0x00011E49 File Offset: 0x00010049
		public CollectionContainsConstraint(object expected)
			: base(expected)
		{
			this.Expected = expected;
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x0600075A RID: 1882 RVA: 0x00011E59 File Offset: 0x00010059
		public override string DisplayName
		{
			get
			{
				return "Contains";
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x0600075B RID: 1883 RVA: 0x00011E60 File Offset: 0x00010060
		public override string Description
		{
			get
			{
				return "collection containing " + MsgUtils.FormatValue(this.Expected);
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x0600075C RID: 1884 RVA: 0x00011E77 File Offset: 0x00010077
		// (set) Token: 0x0600075D RID: 1885 RVA: 0x00011E7F File Offset: 0x0001007F
		private protected object Expected { protected get; private set; }

		// Token: 0x0600075E RID: 1886 RVA: 0x00011E88 File Offset: 0x00010088
		protected override bool Matches(IEnumerable actual)
		{
			foreach (object obj in actual)
			{
				if (base.ItemsEqual(obj, this.Expected))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x00011EE8 File Offset: 0x000100E8
		public CollectionContainsConstraint Using<TCollectionType, TMemberType>(Func<TCollectionType, TMemberType, bool> comparison)
		{
			Func<TMemberType, TCollectionType, bool> func = (TMemberType actual, TCollectionType expected) => comparison(expected, actual);
			base.Using(EqualityAdapter.For<TMemberType, TCollectionType>(func));
			return this;
		}
	}
}
