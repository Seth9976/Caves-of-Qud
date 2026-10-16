using System;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x02000214 RID: 532
	internal interface IGenericInstance : IMetadataTokenProvider
	{
		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000B8A RID: 2954
		bool HasGenericArguments { get; }

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000B8B RID: 2955
		Collection<TypeReference> GenericArguments { get; }
	}
}
