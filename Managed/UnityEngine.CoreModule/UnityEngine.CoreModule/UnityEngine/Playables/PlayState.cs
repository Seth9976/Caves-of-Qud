using System;

namespace UnityEngine.Playables
{
	// Token: 0x02000457 RID: 1111
	public enum PlayState
	{
		// Token: 0x04000C32 RID: 3122
		Paused,
		// Token: 0x04000C33 RID: 3123
		Playing,
		// Token: 0x04000C34 RID: 3124
		[Obsolete("Delayed is obsolete; use a custom ScriptPlayable to implement this feature", false)]
		Delayed
	}
}
