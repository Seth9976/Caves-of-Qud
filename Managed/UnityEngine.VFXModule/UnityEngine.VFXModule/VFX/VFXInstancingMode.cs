using System;

namespace UnityEngine.VFX
{
	// Token: 0x0200000C RID: 12
	internal enum VFXInstancingMode
	{
		// Token: 0x040000F5 RID: 245
		Disabled = -1,
		// Token: 0x040000F6 RID: 246
		[InspectorName("Automatic batch capacity")]
		Auto,
		// Token: 0x040000F7 RID: 247
		[InspectorName("Custom batch capacity")]
		Custom
	}
}
