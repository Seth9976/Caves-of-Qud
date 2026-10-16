using System;

namespace AiUnity.Common.Log
{
	// Token: 0x020002D7 RID: 727
	[Flags]
	public enum LogLevels
	{
		// Token: 0x04000ED2 RID: 3794
		Trace = 64,
		// Token: 0x04000ED3 RID: 3795
		Debug = 32,
		// Token: 0x04000ED4 RID: 3796
		Info = 16,
		// Token: 0x04000ED5 RID: 3797
		Warn = 8,
		// Token: 0x04000ED6 RID: 3798
		Error = 4,
		// Token: 0x04000ED7 RID: 3799
		Fatal = 2,
		// Token: 0x04000ED8 RID: 3800
		Assert = 1,
		// Token: 0x04000ED9 RID: 3801
		Everything = -1
	}
}
