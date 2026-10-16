using System;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x0200021D RID: 541
	public interface IMethodSignature : IMetadataTokenProvider
	{
		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000BA5 RID: 2981
		// (set) Token: 0x06000BA6 RID: 2982
		bool HasThis { get; set; }

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000BA7 RID: 2983
		// (set) Token: 0x06000BA8 RID: 2984
		bool ExplicitThis { get; set; }

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000BA9 RID: 2985
		// (set) Token: 0x06000BAA RID: 2986
		MethodCallingConvention CallingConvention { get; set; }

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000BAB RID: 2987
		bool HasParameters { get; }

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000BAC RID: 2988
		Collection<ParameterDefinition> Parameters { get; }

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000BAD RID: 2989
		// (set) Token: 0x06000BAE RID: 2990
		TypeReference ReturnType { get; set; }

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000BAF RID: 2991
		MethodReturnType MethodReturnType { get; }
	}
}
