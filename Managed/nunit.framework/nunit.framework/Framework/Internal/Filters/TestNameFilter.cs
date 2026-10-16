using System;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal.Filters
{
	// Token: 0x02000098 RID: 152
	[Serializable]
	public class TestNameFilter : ValueMatchFilter
	{
		// Token: 0x060005CD RID: 1485 RVA: 0x0000D425 File Offset: 0x0000B625
		public TestNameFilter(string expectedValue)
			: base(expectedValue)
		{
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x0000D5DA File Offset: 0x0000B7DA
		public override bool Match(ITest test)
		{
			return base.Match(test.Name);
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x0000D5E8 File Offset: 0x0000B7E8
		protected override string ElementName
		{
			get
			{
				return "name";
			}
		}
	}
}
