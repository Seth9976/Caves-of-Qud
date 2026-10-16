using System;

namespace System.Net
{
	// Token: 0x02000589 RID: 1417
	internal enum FtpLoginState : byte
	{
		// Token: 0x04001945 RID: 6469
		NotLoggedIn,
		// Token: 0x04001946 RID: 6470
		LoggedIn,
		// Token: 0x04001947 RID: 6471
		LoggedInButNeedsRelogin,
		// Token: 0x04001948 RID: 6472
		ReloginFailed
	}
}
