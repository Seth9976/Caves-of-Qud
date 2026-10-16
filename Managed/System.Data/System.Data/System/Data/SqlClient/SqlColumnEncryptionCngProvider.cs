using System;
using Unity;

namespace System.Data.SqlClient
{
	// Token: 0x020003F2 RID: 1010
	public class SqlColumnEncryptionCngProvider : SqlColumnEncryptionKeyStoreProvider
	{
		// Token: 0x06002F9E RID: 12190 RVA: 0x000108A6 File Offset: 0x0000EAA6
		public SqlColumnEncryptionCngProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06002F9F RID: 12191 RVA: 0x00060C51 File Offset: 0x0005EE51
		public override byte[] DecryptColumnEncryptionKey(string masterKeyPath, string encryptionAlgorithm, byte[] encryptedColumnEncryptionKey)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06002FA0 RID: 12192 RVA: 0x00060C51 File Offset: 0x0005EE51
		public override byte[] EncryptColumnEncryptionKey(string masterKeyPath, string encryptionAlgorithm, byte[] columnEncryptionKey)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06002FA1 RID: 12193 RVA: 0x00060C51 File Offset: 0x0005EE51
		public override byte[] SignColumnMasterKeyMetadata(string masterKeyPath, bool allowEnclaveComputations)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06002FA2 RID: 12194 RVA: 0x000CBB3C File Offset: 0x000C9D3C
		public override bool VerifyColumnMasterKeyMetadata(string masterKeyPath, bool allowEnclaveComputations, byte[] signature)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		// Token: 0x04001CBC RID: 7356
		public const string ProviderName = "MSSQL_CNG_STORE";
	}
}
