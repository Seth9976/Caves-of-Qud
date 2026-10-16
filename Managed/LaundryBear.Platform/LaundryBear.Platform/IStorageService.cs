using System;
using System.Collections;

namespace LaundryBear.PlatformServices
{
	// Token: 0x02000045 RID: 69
	public interface IStorageService
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000DC RID: 220
		bool SupportsPlayerPrefs { get; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000DD RID: 221
		bool RequiresAssociatedUser { get; }

		// Token: 0x060000DE RID: 222
		void InitializePlayerPrefs();

		// Token: 0x060000DF RID: 223
		IEnumerator InitializePlayerPrefsAsync();

		// Token: 0x060000E0 RID: 224
		string Combine(params string[] paths);

		// Token: 0x060000E1 RID: 225
		void OpenOrCreate(string drive, OnCreateStorage callback);

		// Token: 0x060000E2 RID: 226
		void OpenOrCreate(IUser user, string name, OnCreateStorage callback);
	}
}
