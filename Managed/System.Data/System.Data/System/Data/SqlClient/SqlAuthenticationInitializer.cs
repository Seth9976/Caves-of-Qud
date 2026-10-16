using System;
using Unity;

namespace System.Data.SqlClient
{
	// Token: 0x020003EC RID: 1004
	public abstract class SqlAuthenticationInitializer
	{
		// Token: 0x06002F7F RID: 12159 RVA: 0x000108A6 File Offset: 0x0000EAA6
		protected SqlAuthenticationInitializer()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06002F80 RID: 12160
		public abstract void Initialize();
	}
}
