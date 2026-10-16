using System;

namespace System.Security.Cryptography
{
	/// <summary>Identifies Windows cryptographic object identifier (OID) groups.</summary>
	// Token: 0x020002BB RID: 699
	public enum OidGroup
	{
		/// <summary>All the groups.</summary>
		// Token: 0x04000C5B RID: 3163
		All,
		/// <summary>The Windows group that is represented by CRYPT_HASH_ALG_OID_GROUP_ID.</summary>
		// Token: 0x04000C5C RID: 3164
		HashAlgorithm,
		/// <summary>The Windows group that is represented by CRYPT_ENCRYPT_ALG_OID_GROUP_ID.</summary>
		// Token: 0x04000C5D RID: 3165
		EncryptionAlgorithm,
		/// <summary>The Windows group that is represented by CRYPT_PUBKEY_ALG_OID_GROUP_ID.</summary>
		// Token: 0x04000C5E RID: 3166
		PublicKeyAlgorithm,
		/// <summary>The Windows group that is represented by CRYPT_SIGN_ALG_OID_GROUP_ID.</summary>
		// Token: 0x04000C5F RID: 3167
		SignatureAlgorithm,
		/// <summary>The Windows group that is represented by CRYPT_RDN_ATTR_OID_GROUP_ID.</summary>
		// Token: 0x04000C60 RID: 3168
		Attribute,
		/// <summary>The Windows group that is represented by CRYPT_EXT_OR_ATTR_OID_GROUP_ID.</summary>
		// Token: 0x04000C61 RID: 3169
		ExtensionOrAttribute,
		/// <summary>The Windows group that is represented by CRYPT_ENHKEY_USAGE_OID_GROUP_ID.</summary>
		// Token: 0x04000C62 RID: 3170
		EnhancedKeyUsage,
		/// <summary>The Windows group that is represented by CRYPT_POLICY_OID_GROUP_ID.</summary>
		// Token: 0x04000C63 RID: 3171
		Policy,
		/// <summary>The Windows group that is represented by CRYPT_TEMPLATE_OID_GROUP_ID.</summary>
		// Token: 0x04000C64 RID: 3172
		Template,
		/// <summary>The Windows group that is represented by CRYPT_KDF_OID_GROUP_ID.</summary>
		// Token: 0x04000C65 RID: 3173
		KeyDerivationFunction
	}
}
