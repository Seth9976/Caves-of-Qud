using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200034D RID: 845
	internal class CollectionDataSourceView : DataSourceView
	{
		// Token: 0x06001F33 RID: 7987 RVA: 0x0004CEEB File Offset: 0x0004B0EB
		public CollectionDataSourceView(IDataSource owner, string viewName, IEnumerable collection)
			: base(owner, viewName)
		{
			this.collection = collection;
		}

		// Token: 0x06001F34 RID: 7988 RVA: 0x0004CEFC File Offset: 0x0004B0FC
		protected internal override IEnumerable ExecuteSelect(DataSourceSelectArguments arguments)
		{
			return this.collection;
		}

		// Token: 0x04001881 RID: 6273
		private IEnumerable collection;
	}
}
