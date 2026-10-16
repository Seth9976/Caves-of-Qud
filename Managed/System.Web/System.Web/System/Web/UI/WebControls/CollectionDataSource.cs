using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200034C RID: 844
	internal class CollectionDataSource : IDataSource
	{
		// Token: 0x06001F2D RID: 7981 RVA: 0x0004CEB9 File Offset: 0x0004B0B9
		public CollectionDataSource(IEnumerable collection)
		{
			this.collection = collection;
		}

		// Token: 0x14000056 RID: 86
		// (add) Token: 0x06001F2E RID: 7982 RVA: 0x00003946 File Offset: 0x00001B46
		// (remove) Token: 0x06001F2F RID: 7983 RVA: 0x00003946 File Offset: 0x00001B46
		public event EventHandler DataSourceChanged
		{
			add
			{
			}
			remove
			{
			}
		}

		// Token: 0x06001F30 RID: 7984 RVA: 0x0004CEC8 File Offset: 0x0004B0C8
		public DataSourceView GetView(string viewName)
		{
			return new CollectionDataSourceView(this, viewName, this.collection);
		}

		// Token: 0x06001F31 RID: 7985 RVA: 0x0004CED7 File Offset: 0x0004B0D7
		public ICollection GetViewNames()
		{
			return CollectionDataSource.names;
		}

		// Token: 0x0400187F RID: 6271
		private static readonly string[] names = new string[0];

		// Token: 0x04001880 RID: 6272
		private IEnumerable collection;
	}
}
