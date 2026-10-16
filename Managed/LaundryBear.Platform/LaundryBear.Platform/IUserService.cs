using System;

namespace LaundryBear.PlatformServices
{
	// Token: 0x02000057 RID: 87
	public interface IUserService
	{
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000116 RID: 278
		// (remove) Token: 0x06000117 RID: 279
		event OnUserSignIn UserSignInEvent;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000118 RID: 280
		// (remove) Token: 0x06000119 RID: 281
		event OnUserSignOut UserSignOutEvent;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600011A RID: 282
		// (remove) Token: 0x0600011B RID: 283
		event ControllerPairingChangedEventHandler ControllerPairingChangedEvent;

		// Token: 0x0600011C RID: 284
		bool SupportsMultipleUsers();

		// Token: 0x0600011D RID: 285
		IUser[] GetActiveUsers();

		// Token: 0x0600011E RID: 286
		IUser GetUser(ILocalID localID);

		// Token: 0x0600011F RID: 287
		void GetLaunchUser(OnGetLaunchUser callback);

		// Token: 0x06000120 RID: 288
		void ShowSignInModal(bool allowGuests, OnSignInModalComplete callback);
	}
}
