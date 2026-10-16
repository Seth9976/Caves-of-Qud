using System;
using System.Collections.Generic;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal.Filters
{
	// Token: 0x02000094 RID: 148
	public abstract class CompositeFilter : TestFilter
	{
		// Token: 0x060005B9 RID: 1465 RVA: 0x0000D45D File Offset: 0x0000B65D
		public CompositeFilter()
		{
			this.Filters = new List<ITestFilter>();
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0000D470 File Offset: 0x0000B670
		public CompositeFilter(params ITestFilter[] filters)
		{
			this.Filters = new List<ITestFilter>(filters);
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x0000D484 File Offset: 0x0000B684
		public void Add(ITestFilter filter)
		{
			this.Filters.Add(filter);
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x0000D492 File Offset: 0x0000B692
		// (set) Token: 0x060005BD RID: 1469 RVA: 0x0000D49A File Offset: 0x0000B69A
		public IList<ITestFilter> Filters { get; private set; }

		// Token: 0x060005BE RID: 1470
		public abstract override bool Pass(ITest test);

		// Token: 0x060005BF RID: 1471
		public abstract override bool Match(ITest test);

		// Token: 0x060005C0 RID: 1472
		public abstract override bool IsExplicitMatch(ITest test);

		// Token: 0x060005C1 RID: 1473 RVA: 0x0000D4A4 File Offset: 0x0000B6A4
		public override TNode AddToXml(TNode parentNode, bool recursive)
		{
			TNode tnode = parentNode.AddElement(this.ElementName);
			if (recursive)
			{
				foreach (ITestFilter testFilter in this.Filters)
				{
					testFilter.AddToXml(tnode, true);
				}
			}
			return tnode;
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060005C2 RID: 1474
		protected abstract string ElementName { get; }
	}
}
