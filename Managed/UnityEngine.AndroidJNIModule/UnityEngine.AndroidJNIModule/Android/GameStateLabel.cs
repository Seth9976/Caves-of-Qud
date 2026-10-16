using System;
using UnityEngine.Bindings;

namespace UnityEngine.Android
{
	// Token: 0x02000027 RID: 39
	[NativeType(Header = "Modules/AndroidJNI/Public/GameStateHelper.h")]
	internal enum GameStateLabel
	{
		// Token: 0x0400007F RID: 127
		Default = -1,
		// Token: 0x04000080 RID: 128
		InitialLoading = -2,
		// Token: 0x04000081 RID: 129
		AssetPacksLoading = -3,
		// Token: 0x04000082 RID: 130
		WebRequest = -4
	}
}
