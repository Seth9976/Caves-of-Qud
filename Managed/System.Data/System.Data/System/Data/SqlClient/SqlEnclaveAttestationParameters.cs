using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Unity;

namespace System.Data.SqlClient
{
	// Token: 0x020003F6 RID: 1014
	public class SqlEnclaveAttestationParameters
	{
		// Token: 0x06002FB0 RID: 12208 RVA: 0x000108A6 File Offset: 0x0000EAA6
		public SqlEnclaveAttestationParameters(int protocol, byte[] input, ECDiffieHellmanCng clientDiffieHellmanKey)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x06002FB1 RID: 12209 RVA: 0x00060C51 File Offset: 0x0005EE51
		public ECDiffieHellmanCng ClientDiffieHellmanKey
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x06002FB2 RID: 12210 RVA: 0x000CBB90 File Offset: 0x000C9D90
		public int Protocol
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		// Token: 0x06002FB3 RID: 12211 RVA: 0x00060C51 File Offset: 0x0005EE51
		public byte[] GetInput()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
