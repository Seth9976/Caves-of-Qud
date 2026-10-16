using System;

namespace LaundryBear.PlatformServices.None
{
	// Token: 0x02000003 RID: 3
	public class UserController : IUserController, IEquatable<IUserController>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public IUser User
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020C5 File Offset: 0x000002C5
		public bool Equals(IUserController other)
		{
			return this == other as UserController;
		}
	}
}
