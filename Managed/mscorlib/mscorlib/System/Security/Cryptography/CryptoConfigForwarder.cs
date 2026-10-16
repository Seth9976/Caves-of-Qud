using System;

namespace System.Security.Cryptography
{
	// Token: 0x020004D4 RID: 1236
	internal static class CryptoConfigForwarder
	{
		// Token: 0x06003166 RID: 12646 RVA: 0x000B6C7B File Offset: 0x000B4E7B
		internal static object CreateFromName(string name)
		{
			return CryptoConfig.CreateFromName(name);
		}

		// Token: 0x06003167 RID: 12647 RVA: 0x000B6C83 File Offset: 0x000B4E83
		internal static HashAlgorithm CreateDefaultHashAlgorithm()
		{
			return (HashAlgorithm)CryptoConfigForwarder.CreateFromName("System.Security.Cryptography.HashAlgorithm");
		}
	}
}
