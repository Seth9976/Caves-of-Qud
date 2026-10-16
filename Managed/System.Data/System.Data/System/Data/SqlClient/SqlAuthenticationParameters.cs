using System;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Data.SqlClient
{
	// Token: 0x020003ED RID: 1005
	public class SqlAuthenticationParameters
	{
		// Token: 0x06002F81 RID: 12161 RVA: 0x000108A6 File Offset: 0x0000EAA6
		protected SqlAuthenticationParameters(SqlAuthenticationMethod authenticationMethod, string serverName, string databaseName, string resource, string authority, string userId, string password, Guid connectionId)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06002F82 RID: 12162 RVA: 0x000CBA78 File Offset: 0x000C9C78
		public SqlAuthenticationMethod AuthenticationMethod
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return SqlAuthenticationMethod.NotSpecified;
			}
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06002F83 RID: 12163 RVA: 0x00060C51 File Offset: 0x0005EE51
		public string Authority
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06002F84 RID: 12164 RVA: 0x000CBA94 File Offset: 0x000C9C94
		public Guid ConnectionId
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(Guid);
			}
		}

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06002F85 RID: 12165 RVA: 0x00060C51 File Offset: 0x0005EE51
		public string DatabaseName
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x06002F86 RID: 12166 RVA: 0x00060C51 File Offset: 0x0005EE51
		public string Password
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x06002F87 RID: 12167 RVA: 0x00060C51 File Offset: 0x0005EE51
		public string Resource
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x06002F88 RID: 12168 RVA: 0x00060C51 File Offset: 0x0005EE51
		public string ServerName
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06002F89 RID: 12169 RVA: 0x00060C51 File Offset: 0x0005EE51
		public string UserId
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}
