using System;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal.Filters
{
	// Token: 0x0200009C RID: 156
	[Serializable]
	public class NotFilter : TestFilter
	{
		// Token: 0x060005DC RID: 1500 RVA: 0x0000D7A9 File Offset: 0x0000B9A9
		public NotFilter(TestFilter baseFilter)
		{
			this.BaseFilter = baseFilter;
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060005DD RID: 1501 RVA: 0x0000D7B8 File Offset: 0x0000B9B8
		// (set) Token: 0x060005DE RID: 1502 RVA: 0x0000D7C0 File Offset: 0x0000B9C0
		public TestFilter BaseFilter { get; private set; }

		// Token: 0x060005DF RID: 1503 RVA: 0x0000D7C9 File Offset: 0x0000B9C9
		public override bool Pass(ITest test)
		{
			return !this.BaseFilter.Match(test) && !this.BaseFilter.MatchParent(test);
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0000D7EA File Offset: 0x0000B9EA
		public override bool Match(ITest test)
		{
			return !this.BaseFilter.Match(test);
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00004B9A File Offset: 0x00002D9A
		public override bool IsExplicitMatch(ITest test)
		{
			return false;
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0000D7FC File Offset: 0x0000B9FC
		public override TNode AddToXml(TNode parentNode, bool recursive)
		{
			TNode tnode = parentNode.AddElement("not");
			if (recursive)
			{
				this.BaseFilter.AddToXml(tnode, true);
			}
			return tnode;
		}
	}
}
