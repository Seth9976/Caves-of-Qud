using System;
using System.Collections.ObjectModel;

namespace System.Data.Common
{
	// Token: 0x020003A3 RID: 931
	public interface IDbColumnSchemaGenerator
	{
		// Token: 0x06002D2B RID: 11563
		ReadOnlyCollection<DbColumn> GetColumnSchema();
	}
}
