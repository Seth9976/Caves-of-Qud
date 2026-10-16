using System;

namespace System.Web.Security
{
	/// <summary>Specifies values that indicates whether data should be encrypted or decrypted, whether a hash-based message authentication code (HMAC) should be appended or validated, or both.</summary>
	// Token: 0x020004BE RID: 1214
	public enum MachineKeyProtection
	{
		/// <summary>Specifies that a hash-based message authentication code (HMAC) should be appended to the data and that the data should be encrypted (for the <see cref="M:System.Web.Security.Encode" /> method) or that the data should be both decrypted and validated (for the <see cref="M:System.Web.Security.Decode" /> method).</summary>
		// Token: 0x04001DD4 RID: 7636
		All,
		/// <summary>Specifies that the data should be encrypted (for the <see cref="M:System.Web.Security.Encode" /> method) or decrypted (for the <see cref="M:System.Web.Security.Decode" /> method).</summary>
		// Token: 0x04001DD5 RID: 7637
		Encryption,
		/// <summary>Specifies that a hash-based message authentication code (HMAC) should be appended to the data (for the <see cref="M:System.Web.Security.Encode" /> method) or validated (for the <see cref="M:System.Web.Security.Decode" /> method).</summary>
		// Token: 0x04001DD6 RID: 7638
		Validation
	}
}
