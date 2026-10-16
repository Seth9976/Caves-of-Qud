using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x0200007E RID: 126
	[Flags]
	internal enum MethodDefinitionTreatment
	{
		// Token: 0x04000170 RID: 368
		None = 0,
		// Token: 0x04000171 RID: 369
		Abstract = 2,
		// Token: 0x04000172 RID: 370
		Private = 4,
		// Token: 0x04000173 RID: 371
		Public = 8,
		// Token: 0x04000174 RID: 372
		Runtime = 16,
		// Token: 0x04000175 RID: 373
		InternalCall = 32
	}
}
