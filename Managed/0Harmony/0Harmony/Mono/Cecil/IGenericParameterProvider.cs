using System;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x02000215 RID: 533
	public interface IGenericParameterProvider : IMetadataTokenProvider
	{
		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000B8C RID: 2956
		bool HasGenericParameters { get; }

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000B8D RID: 2957
		bool IsDefinition { get; }

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000B8E RID: 2958
		ModuleDefinition Module { get; }

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000B8F RID: 2959
		Collection<GenericParameter> GenericParameters { get; }

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000B90 RID: 2960
		GenericParameterType GenericParameterType { get; }
	}
}
