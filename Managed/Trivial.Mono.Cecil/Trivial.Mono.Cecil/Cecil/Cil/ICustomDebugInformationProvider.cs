using System;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil.Cil
{
	// Token: 0x0200010C RID: 268
	public interface ICustomDebugInformationProvider : IMetadataTokenProvider
	{
		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000A59 RID: 2649
		bool HasCustomDebugInformations { get; }

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000A5A RID: 2650
		Collection<CustomDebugInformation> CustomDebugInformations { get; }
	}
}
