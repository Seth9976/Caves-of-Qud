using System;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x02000027 RID: 39
	[Obsolete("This class is not used anymore. See AnimatorOverrideController.GetOverrides() and AnimatorOverrideController.ApplyOverrides()")]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class AnimationClipPair
	{
		// Token: 0x0400007E RID: 126
		public AnimationClip originalClip;

		// Token: 0x0400007F RID: 127
		public AnimationClip overrideClip;
	}
}
