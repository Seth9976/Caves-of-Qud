using System;
using System.Threading.Tasks;
using Unity;

namespace System.Data.SqlClient
{
	// Token: 0x020003EE RID: 1006
	public abstract class SqlAuthenticationProvider
	{
		// Token: 0x06002F8A RID: 12170 RVA: 0x000108A6 File Offset: 0x0000EAA6
		protected SqlAuthenticationProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06002F8B RID: 12171
		public abstract Task<SqlAuthenticationToken> AcquireTokenAsync(SqlAuthenticationParameters parameters);

		// Token: 0x06002F8C RID: 12172 RVA: 0x000108A6 File Offset: 0x0000EAA6
		public virtual void BeforeLoad(SqlAuthenticationMethod authenticationMethod)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06002F8D RID: 12173 RVA: 0x000108A6 File Offset: 0x0000EAA6
		public virtual void BeforeUnload(SqlAuthenticationMethod authenticationMethod)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06002F8E RID: 12174 RVA: 0x00060C51 File Offset: 0x0005EE51
		public static SqlAuthenticationProvider GetProvider(SqlAuthenticationMethod authenticationMethod)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06002F8F RID: 12175
		public abstract bool IsSupported(SqlAuthenticationMethod authenticationMethod);

		// Token: 0x06002F90 RID: 12176 RVA: 0x000CBAB0 File Offset: 0x000C9CB0
		public static bool SetProvider(SqlAuthenticationMethod authenticationMethod, SqlAuthenticationProvider provider)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}
	}
}
