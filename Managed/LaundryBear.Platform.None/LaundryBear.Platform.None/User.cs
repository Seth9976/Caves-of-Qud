using System;

namespace LaundryBear.PlatformServices.None
{
	// Token: 0x02000007 RID: 7
	public class User : IUser, IEquatable<IUser>
	{
		// Token: 0x06000052 RID: 82 RVA: 0x00002D10 File Offset: 0x00000F10
		internal User()
		{
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002D18 File Offset: 0x00000F18
		public bool IsSignedIn()
		{
			return false;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002D1B File Offset: 0x00000F1B
		public IUserController[] GetControllers()
		{
			return null;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002D1E File Offset: 0x00000F1E
		public string GetDisplayName()
		{
			return string.Empty;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002D25 File Offset: 0x00000F25
		public ILocalID GetLocalID()
		{
			return new User.LocalID();
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002D2C File Offset: 0x00000F2C
		public void GetProfilePicture(PhotoSize size, OnProfilePictureRetrieved callback)
		{
			callback(null);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002D35 File Offset: 0x00000F35
		public IUniqueID GetUniqueID()
		{
			return new User.UniqueID();
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002D3C File Offset: 0x00000F3C
		public bool Equals(IUser otherUser)
		{
			return true;
		}

		// Token: 0x0200000C RID: 12
		private class LocalID : ILocalID, IEquatable<ILocalID>
		{
			// Token: 0x06000066 RID: 102 RVA: 0x00002E17 File Offset: 0x00001017
			public bool Equals(ILocalID other)
			{
				return true;
			}
		}

		// Token: 0x0200000D RID: 13
		private class UniqueID : IUniqueID, IEquatable<IUniqueID>
		{
			// Token: 0x06000068 RID: 104 RVA: 0x00002E22 File Offset: 0x00001022
			public bool Equals(IUniqueID other)
			{
				return true;
			}
		}
	}
}
