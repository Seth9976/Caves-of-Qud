using System;
using Unity;

namespace System.Data.SqlClient
{
	// Token: 0x020003EA RID: 1002
	public abstract class SqlColumnEncryptionKeyStoreProvider
	{
		// Token: 0x06002F6B RID: 12139 RVA: 0x000108A6 File Offset: 0x0000EAA6
		protected SqlColumnEncryptionKeyStoreProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06002F6C RID: 12140
		public abstract byte[] DecryptColumnEncryptionKey(string masterKeyPath, string encryptionAlgorithm, byte[] encryptedColumnEncryptionKey);

		// Token: 0x06002F6D RID: 12141
		public abstract byte[] EncryptColumnEncryptionKey(string masterKeyPath, string encryptionAlgorithm, byte[] columnEncryptionKey);

		// Token: 0x06002F6E RID: 12142 RVA: 0x00060C51 File Offset: 0x0005EE51
		public virtual byte[] SignColumnMasterKeyMetadata(string masterKeyPath, bool allowEnclaveComputations)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06002F6F RID: 12143 RVA: 0x000CB998 File Offset: 0x000C9B98
		public virtual bool VerifyColumnMasterKeyMetadata(string masterKeyPath, bool allowEnclaveComputations, byte[] signature)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}
	}
}
