using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x0200007A RID: 122
	public enum MethodCallingConvention : byte
	{
		// Token: 0x04000158 RID: 344
		Default,
		// Token: 0x04000159 RID: 345
		C,
		// Token: 0x0400015A RID: 346
		StdCall,
		// Token: 0x0400015B RID: 347
		ThisCall,
		// Token: 0x0400015C RID: 348
		FastCall,
		// Token: 0x0400015D RID: 349
		VarArg,
		// Token: 0x0400015E RID: 350
		Generic = 16
	}
}
