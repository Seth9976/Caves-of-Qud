using System;

namespace UnityEngine
{
	// Token: 0x02000051 RID: 81
	[Flags]
	public enum ParticleSystemSubEmitterProperties
	{
		// Token: 0x04000155 RID: 341
		InheritNothing = 0,
		// Token: 0x04000156 RID: 342
		InheritEverything = 31,
		// Token: 0x04000157 RID: 343
		InheritColor = 1,
		// Token: 0x04000158 RID: 344
		InheritSize = 2,
		// Token: 0x04000159 RID: 345
		InheritRotation = 4,
		// Token: 0x0400015A RID: 346
		InheritLifetime = 8,
		// Token: 0x0400015B RID: 347
		InheritDuration = 16
	}
}
