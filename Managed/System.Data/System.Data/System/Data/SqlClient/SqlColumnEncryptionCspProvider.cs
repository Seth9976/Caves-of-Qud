using System;
using Unity;

namespace System.Data.SqlClient
{
	// Token: 0x020003F3 RID: 1011
	public class SqlColumnEncryptionCspProvider : SqlColumnEncryptionKeyStoreProvider
	{
		// Token: 0x06002FA3 RID: 12195 RVA: 0x000108A6 File Offset: 0x0000EAA6
		public SqlColumnEncryptionCspProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06002FA4 RID: 12196 RVA: 0x00060C51 File Offset: 0x0005EE51
		public override byte[] DecryptColumnEncryptionKey(string masterKeyPath, string encryptionAlgorithm, byte[] encryptedColumnEncryptionKey)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06002FA5 RID: 12197 RVA: 0x00060C51 File Offset: 0x0005EE51
		public override byte[] EncryptColumnEncryptionKey(string masterKeyPath, string encryptionAlgorithm, byte[] columnEncryptionKey)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06002FA6 RID: 12198 RVA: 0x00060C51 File Offset: 0x0005EE51
		public override byte[] SignColumnMasterKeyMetadata(string masterKeyPath, bool allowEnclaveComputations)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06002FA7 RID: 12199 RVA: 0x000CBB58 File Offset: 0x000C9D58
		public override bool VerifyColumnMasterKeyMetadata(string masterKeyPath, bool allowEnclaveComputations, byte[] signature)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		// Token: 0x04001CBD RID: 7357
		public const string ProviderName = "MSSQL_CSP_PROVIDER";
	}
}
