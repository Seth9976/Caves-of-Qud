using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x0200004E RID: 78
	internal interface IGenericContext
	{
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600026A RID: 618
		bool IsDefinition { get; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600026B RID: 619
		IGenericParameterProvider Type { get; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600026C RID: 620
		IGenericParameterProvider Method { get; }
	}
}
