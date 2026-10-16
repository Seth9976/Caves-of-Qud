using System;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Data.SqlClient
{
	// Token: 0x020003F5 RID: 1013
	public class SqlEnclaveSession
	{
		// Token: 0x06002FAD RID: 12205 RVA: 0x000108A6 File Offset: 0x0000EAA6
		public SqlEnclaveSession(byte[] sessionKey, long sessionId)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x06002FAE RID: 12206 RVA: 0x000CBB74 File Offset: 0x000C9D74
		public long SessionId
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0L;
			}
		}

		// Token: 0x06002FAF RID: 12207 RVA: 0x00060C51 File Offset: 0x0005EE51
		public byte[] GetSessionKey()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
