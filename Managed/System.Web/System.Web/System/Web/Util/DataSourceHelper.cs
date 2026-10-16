using System;
using System.Collections;

namespace System.Web.Util
{
	// Token: 0x02000137 RID: 311
	internal class DataSourceHelper
	{
		// Token: 0x06000E4F RID: 3663 RVA: 0x00002050 File Offset: 0x00000250
		private DataSourceHelper()
		{
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x00026BD4 File Offset: 0x00024DD4
		[Obsolete("Use DataSourceResolver")]
		public static IEnumerable GetResolvedDataSource(object o, string data_member)
		{
			return DataSourceResolver.ResolveDataSource(o, data_member);
		}
	}
}
