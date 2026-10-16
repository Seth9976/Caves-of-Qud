using System;
using System.Collections.Generic;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal.Filters
{
	// Token: 0x0200009D RID: 157
	[Serializable]
	public class OrFilter : CompositeFilter
	{
		// Token: 0x060005E3 RID: 1507 RVA: 0x0000D5EF File Offset: 0x0000B7EF
		public OrFilter()
		{
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0000D5F7 File Offset: 0x0000B7F7
		public OrFilter(params ITestFilter[] filters)
			: base(filters)
		{
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0000D828 File Offset: 0x0000BA28
		public override bool Pass(ITest test)
		{
			using (IEnumerator<ITestFilter> enumerator = base.Filters.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Pass(test))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x0000D87C File Offset: 0x0000BA7C
		public override bool Match(ITest test)
		{
			using (IEnumerator<ITestFilter> enumerator = base.Filters.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((TestFilter)enumerator.Current).Match(test))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0000D8D8 File Offset: 0x0000BAD8
		public override bool IsExplicitMatch(ITest test)
		{
			using (IEnumerator<ITestFilter> enumerator = base.Filters.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((TestFilter)enumerator.Current).IsExplicitMatch(test))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x0000D934 File Offset: 0x0000BB34
		protected override string ElementName
		{
			get
			{
				return "or";
			}
		}
	}
}
