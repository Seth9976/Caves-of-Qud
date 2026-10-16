using System;

namespace LaundryBear.PlatformServices
{
	// Token: 0x02000030 RID: 48
	public interface IStatService
	{
		// Token: 0x06000099 RID: 153
		void GetStat(IUser user, string statID, OnStatGet callback);
	}
}
