using System;

namespace LaundryBear.PlatformServices
{
	// Token: 0x02000046 RID: 70
	public interface IRamStorageService
	{
		// Token: 0x060000E3 RID: 227
		void CreateRamStorage(string name, int bytes, OnCreateStorage callback);
	}
}
