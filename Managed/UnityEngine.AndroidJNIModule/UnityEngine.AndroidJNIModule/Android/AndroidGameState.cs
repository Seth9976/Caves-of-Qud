using System;
using UnityEngine.Bindings;

namespace UnityEngine.Android
{
	// Token: 0x02000026 RID: 38
	[NativeType(Header = "Modules/AndroidJNI/Public/GameStateHelper.h")]
	public enum AndroidGameState
	{
		// Token: 0x04000079 RID: 121
		Unknown,
		// Token: 0x0400007A RID: 122
		None,
		// Token: 0x0400007B RID: 123
		GamePlayInterruptible,
		// Token: 0x0400007C RID: 124
		GamePlayUninterruptible,
		// Token: 0x0400007D RID: 125
		Content
	}
}
