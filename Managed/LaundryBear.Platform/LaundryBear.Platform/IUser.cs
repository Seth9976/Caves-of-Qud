using System;

namespace LaundryBear.PlatformServices
{
	// Token: 0x02000059 RID: 89
	public interface IUser : IEquatable<IUser>
	{
		// Token: 0x06000125 RID: 293
		bool IsSignedIn();

		// Token: 0x06000126 RID: 294
		ILocalID GetLocalID();

		// Token: 0x06000127 RID: 295
		IUniqueID GetUniqueID();

		// Token: 0x06000128 RID: 296
		string GetDisplayName();

		// Token: 0x06000129 RID: 297
		void GetProfilePicture(PhotoSize size, OnProfilePictureRetrieved callback);

		// Token: 0x0600012A RID: 298
		IUserController[] GetControllers();
	}
}
