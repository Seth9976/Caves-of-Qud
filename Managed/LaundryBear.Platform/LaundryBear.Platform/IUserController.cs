using System;

namespace LaundryBear.PlatformServices
{
	// Token: 0x0200000D RID: 13
	public interface IUserController : IEquatable<IUserController>
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000025 RID: 37
		IUser User { get; }
	}
}
