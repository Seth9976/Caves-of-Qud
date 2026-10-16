using System;
using System.Security.Cryptography;
using Unity;

namespace System.Data.SqlClient
{
	// Token: 0x020003F4 RID: 1012
	public abstract class SqlColumnEncryptionEnclaveProvider
	{
		// Token: 0x06002FA8 RID: 12200 RVA: 0x000108A6 File Offset: 0x0000EAA6
		protected SqlColumnEncryptionEnclaveProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06002FA9 RID: 12201
		public abstract void CreateEnclaveSession(byte[] enclaveAttestationInfo, ECDiffieHellmanCng clientDiffieHellmanKey, string attestationUrl, string servername, out SqlEnclaveSession sqlEnclaveSession, out long counter);

		// Token: 0x06002FAA RID: 12202
		public abstract SqlEnclaveAttestationParameters GetAttestationParameters();

		// Token: 0x06002FAB RID: 12203
		public abstract void GetEnclaveSession(string serverName, string attestationUrl, out SqlEnclaveSession sqlEnclaveSession, out long counter);

		// Token: 0x06002FAC RID: 12204
		public abstract void InvalidateEnclaveSession(string serverName, string enclaveAttestationUrl, SqlEnclaveSession enclaveSession);
	}
}
