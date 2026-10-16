using System;
using System.Collections.Generic;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal.Filters
{
	// Token: 0x02000099 RID: 153
	[Serializable]
	public class AndFilter : CompositeFilter
	{
		// Token: 0x060005D0 RID: 1488 RVA: 0x0000D5EF File Offset: 0x0000B7EF
		public AndFilter()
		{
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x0000D5F7 File Offset: 0x0000B7F7
		public AndFilter(params ITestFilter[] filters)
			: base(filters)
		{
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x0000D600 File Offset: 0x0000B800
		public override bool Pass(ITest test)
		{
			using (IEnumerator<ITestFilter> enumerator = base.Filters.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.Pass(test))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x0000D654 File Offset: 0x0000B854
		public override bool Match(ITest test)
		{
			using (IEnumerator<ITestFilter> enumerator = base.Filters.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!((TestFilter)enumerator.Current).Match(test))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0000D6B0 File Offset: 0x0000B8B0
		public override bool IsExplicitMatch(ITest test)
		{
			using (IEnumerator<ITestFilter> enumerator = base.Filters.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!((TestFilter)enumerator.Current).IsExplicitMatch(test))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x0000D70C File Offset: 0x0000B90C
		protected override string ElementName
		{
			get
			{
				return "and";
			}
		}
	}
}
