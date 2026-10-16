using System;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000050 RID: 80
	public interface IGenericParameterProvider : IMetadataTokenProvider
	{
		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600026F RID: 623
		bool HasGenericParameters { get; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000270 RID: 624
		bool IsDefinition { get; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000271 RID: 625
		ModuleDefinition Module { get; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000272 RID: 626
		Collection<GenericParameter> GenericParameters { get; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000273 RID: 627
		GenericParameterType GenericParameterType { get; }
	}
}
