using System;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal.Filters
{
	// Token: 0x02000096 RID: 150
	[Serializable]
	public class MethodNameFilter : ValueMatchFilter
	{
		// Token: 0x060005C6 RID: 1478 RVA: 0x0000D425 File Offset: 0x0000B625
		public MethodNameFilter(string expectedValue)
			: base(expectedValue)
		{
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0000D519 File Offset: 0x0000B719
		public override bool Match(ITest test)
		{
			return base.Match(test.MethodName);
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x0000D527 File Offset: 0x0000B727
		protected override string ElementName
		{
			get
			{
				return "method";
			}
		}
	}
}
