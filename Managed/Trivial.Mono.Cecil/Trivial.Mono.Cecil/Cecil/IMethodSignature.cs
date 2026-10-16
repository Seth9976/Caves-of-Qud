using System;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000058 RID: 88
	public interface IMethodSignature : IMetadataTokenProvider
	{
		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600028C RID: 652
		// (set) Token: 0x0600028D RID: 653
		bool HasThis { get; set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600028E RID: 654
		// (set) Token: 0x0600028F RID: 655
		bool ExplicitThis { get; set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000290 RID: 656
		// (set) Token: 0x06000291 RID: 657
		MethodCallingConvention CallingConvention { get; set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000292 RID: 658
		bool HasParameters { get; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000293 RID: 659
		Collection<ParameterDefinition> Parameters { get; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000294 RID: 660
		// (set) Token: 0x06000295 RID: 661
		TypeReference ReturnType { get; set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000296 RID: 662
		MethodReturnType MethodReturnType { get; }
	}
}
