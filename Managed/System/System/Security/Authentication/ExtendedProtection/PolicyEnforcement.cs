using System;

namespace System.Security.Authentication.ExtendedProtection
{
	/// <summary>The <see cref="T:System.Security.Authentication.ExtendedProtection.PolicyEnforcement" /> enumeration specifies when the <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> should be enforced.</summary>
	// Token: 0x020002AB RID: 683
	public enum PolicyEnforcement
	{
		/// <summary>The <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> is never enforced and extended protection is disabled.</summary>
		// Token: 0x04000C1C RID: 3100
		Never,
		/// <summary>The <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> is enforced only if the client and server supports extended protection.</summary>
		// Token: 0x04000C1D RID: 3101
		WhenSupported,
		/// <summary>The <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> is always enforced. Clients that don’t support extended protection will fail to authenticate.</summary>
		// Token: 0x04000C1E RID: 3102
		Always
	}
}
