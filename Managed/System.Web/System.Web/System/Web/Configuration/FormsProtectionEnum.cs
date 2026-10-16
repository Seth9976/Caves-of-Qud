using System;

namespace System.Web.Configuration
{
	/// <summary>Defines the type of encryption, if any, to use for cookies. </summary>
	// Token: 0x02000561 RID: 1377
	public enum FormsProtectionEnum
	{
		/// <summary>Specifies that the application use both data validation and encryption to help protect cookies. This option uses the configured data-validation algorithm (based on the machineKey element). Triple-DES (3DES) is used for encryption, if it is available and if the key is at least 48 bytes long. All is the default (and recommended) value.</summary>
		// Token: 0x04002017 RID: 8215
		All,
		/// <summary>Specifies that both encryption and validation are disabled for sites that use cookies only for personalization and thus have weaker security requirements. Using cookies in this manner is not recommended; however, it is the least resource-intensive way to enable personalization using the .NET Framework.</summary>
		// Token: 0x04002018 RID: 8216
		None,
		/// <summary>Specifies that cookies are encrypted using Triple-DES or DES, but data validation is not performed on cookies. Cookies used this way might be subject to chosen plaintext security attacks.</summary>
		// Token: 0x04002019 RID: 8217
		Encryption,
		/// <summary>Specifies that the application use a validation scheme to verify that the contents of an encrypted cookie have not been altered in transit. The cookie is created by concatenating a validation key with the cookie data, computing a Message Authentication Code (MAC), and appending the MAC to the outgoing cookie.</summary>
		// Token: 0x0400201A RID: 8218
		Validation
	}
}
