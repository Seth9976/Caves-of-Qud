using System;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal.Filters
{
	// Token: 0x0200009B RID: 155
	[Serializable]
	public class IdFilter : ValueMatchFilter
	{
		// Token: 0x060005D9 RID: 1497 RVA: 0x0000D425 File Offset: 0x0000B625
		public IdFilter(string id)
			: base(id)
		{
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0000D78F File Offset: 0x0000B98F
		public override bool Match(ITest test)
		{
			return test.Id == base.ExpectedValue;
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x0000D7A2 File Offset: 0x0000B9A2
		protected override string ElementName
		{
			get
			{
				return "id";
			}
		}
	}
}
