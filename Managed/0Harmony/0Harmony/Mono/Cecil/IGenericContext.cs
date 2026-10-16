using System;

namespace Mono.Cecil
{
	// Token: 0x02000217 RID: 535
	internal interface IGenericContext
	{
		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000B91 RID: 2961
		bool IsDefinition { get; }

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000B92 RID: 2962
		IGenericParameterProvider Type { get; }

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000B93 RID: 2963
		IGenericParameterProvider Method { get; }
	}
}
