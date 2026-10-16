using System;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil
{
	// Token: 0x0200004F RID: 79
	public interface IGenericInstance : IMetadataTokenProvider
	{
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600026D RID: 621
		bool HasGenericArguments { get; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600026E RID: 622
		Collection<TypeReference> GenericArguments { get; }
	}
}
