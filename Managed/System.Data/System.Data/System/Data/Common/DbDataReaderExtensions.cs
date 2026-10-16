using System;
using System.Collections.ObjectModel;

namespace System.Data.Common
{
	// Token: 0x02000393 RID: 915
	public static class DbDataReaderExtensions
	{
		// Token: 0x06002C77 RID: 11383 RVA: 0x000BEA54 File Offset: 0x000BCC54
		public static ReadOnlyCollection<DbColumn> GetColumnSchema(this DbDataReader reader)
		{
			if (reader.CanGetColumnSchema())
			{
				return ((IDbColumnSchemaGenerator)reader).GetColumnSchema();
			}
			throw new NotSupportedException();
		}

		// Token: 0x06002C78 RID: 11384 RVA: 0x000BEA6F File Offset: 0x000BCC6F
		public static bool CanGetColumnSchema(this DbDataReader reader)
		{
			return reader is IDbColumnSchemaGenerator;
		}
	}
}
