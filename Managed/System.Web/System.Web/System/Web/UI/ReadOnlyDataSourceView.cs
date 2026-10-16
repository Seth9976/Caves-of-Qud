using System;

namespace System.Web.UI
{
	// Token: 0x0200021A RID: 538
	internal class ReadOnlyDataSourceView : HierarchicalDataSourceView
	{
		// Token: 0x06001611 RID: 5649 RVA: 0x000390C4 File Offset: 0x000372C4
		public ReadOnlyDataSourceView(IHierarchicalEnumerable datasource)
		{
			this.datasource = datasource;
		}

		// Token: 0x06001612 RID: 5650 RVA: 0x000390D3 File Offset: 0x000372D3
		public override IHierarchicalEnumerable Select()
		{
			return this.datasource;
		}

		// Token: 0x0400155A RID: 5466
		private IHierarchicalEnumerable datasource;
	}
}
