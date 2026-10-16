using System;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal.Filters
{
	// Token: 0x02000095 RID: 149
	[Serializable]
	public class FullNameFilter : ValueMatchFilter
	{
		// Token: 0x060005C3 RID: 1475 RVA: 0x0000D425 File Offset: 0x0000B625
		public FullNameFilter(string expectedValue)
			: base(expectedValue)
		{
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x0000D504 File Offset: 0x0000B704
		public override bool Match(ITest test)
		{
			return base.Match(test.FullName);
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x0000D512 File Offset: 0x0000B712
		protected override string ElementName
		{
			get
			{
				return "test";
			}
		}
	}
}
