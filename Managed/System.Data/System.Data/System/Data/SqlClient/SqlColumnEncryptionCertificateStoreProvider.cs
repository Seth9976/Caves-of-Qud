using System;
using Unity;

namespace System.Data.SqlClient
{
	// Token: 0x020003F1 RID: 1009
	public class SqlColumnEncryptionCertificateStoreProvider : SqlColumnEncryptionKeyStoreProvider
	{
		// Token: 0x06002F99 RID: 12185 RVA: 0x000108A6 File Offset: 0x0000EAA6
		public SqlColumnEncryptionCertificateStoreProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06002F9A RID: 12186 RVA: 0x00060C51 File Offset: 0x0005EE51
		public override byte[] DecryptColumnEncryptionKey(string masterKeyPath, string encryptionAlgorithm, byte[] encryptedColumnEncryptionKey)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06002F9B RID: 12187 RVA: 0x00060C51 File Offset: 0x0005EE51
		public override byte[] EncryptColumnEncryptionKey(string masterKeyPath, string encryptionAlgorithm, byte[] columnEncryptionKey)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06002F9C RID: 12188 RVA: 0x00060C51 File Offset: 0x0005EE51
		public override byte[] SignColumnMasterKeyMetadata(string masterKeyPath, bool allowEnclaveComputations)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06002F9D RID: 12189 RVA: 0x000CBB20 File Offset: 0x000C9D20
		public override bool VerifyColumnMasterKeyMetadata(string masterKeyPath, bool allowEnclaveComputations, byte[] signature)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		// Token: 0x04001CBB RID: 7355
		public const string ProviderName = "MSSQL_CERTIFICATE_STORE";
	}
}
