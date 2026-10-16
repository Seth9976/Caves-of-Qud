using System;
using System.Configuration.Provider;
using System.Security.Cryptography;
using System.Web.Configuration;
using System.Web.Util;

namespace System.Web.Security
{
	// Token: 0x020004C0 RID: 1216
	internal sealed class MembershipHelper : IMembershipHelper
	{
		// Token: 0x17001141 RID: 4417
		// (get) Token: 0x060036C1 RID: 14017 RVA: 0x0008D847 File Offset: 0x0008BA47
		public int UserIsOnlineTimeWindow
		{
			get
			{
				return Membership.UserIsOnlineTimeWindow;
			}
		}

		// Token: 0x17001142 RID: 4418
		// (get) Token: 0x060036C2 RID: 14018 RVA: 0x0008D84E File Offset: 0x0008BA4E
		public MembershipProviderCollection Providers
		{
			get
			{
				return Membership.Providers;
			}
		}

		// Token: 0x060036C3 RID: 14019 RVA: 0x0008D858 File Offset: 0x0008BA58
		private static SymmetricAlgorithm GetAlgorithm()
		{
			MachineKeySection config = MachineKeySection.Config;
			if (config.DecryptionKey.StartsWith("AutoGenerate"))
			{
				throw new ProviderException("You must explicitly specify a decryption key in the <machineKey> section when using encrypted passwords.");
			}
			SymmetricAlgorithm decryptionAlgorithm = config.GetDecryptionAlgorithm();
			if (decryptionAlgorithm == null)
			{
				throw new ProviderException(string.Format("Unsupported decryption attribute '{0}' in <machineKey> configuration section", config.Decryption));
			}
			decryptionAlgorithm.Key = config.GetDecryptionKey();
			return decryptionAlgorithm;
		}

		// Token: 0x060036C4 RID: 14020 RVA: 0x0008D8B4 File Offset: 0x0008BAB4
		public byte[] DecryptPassword(byte[] encodedPassword)
		{
			byte[] array;
			using (SymmetricAlgorithm algorithm = MembershipHelper.GetAlgorithm())
			{
				array = MachineKeySectionUtils.Decrypt(algorithm, encodedPassword, 0, encodedPassword.Length);
			}
			return array;
		}

		// Token: 0x060036C5 RID: 14021 RVA: 0x0008D8F0 File Offset: 0x0008BAF0
		public byte[] EncryptPassword(byte[] password)
		{
			byte[] array;
			using (SymmetricAlgorithm algorithm = MembershipHelper.GetAlgorithm())
			{
				array = MachineKeySectionUtils.Encrypt(algorithm, password);
			}
			return array;
		}

		// Token: 0x04001DDB RID: 7643
		internal const int SALT_BYTES = 16;
	}
}
