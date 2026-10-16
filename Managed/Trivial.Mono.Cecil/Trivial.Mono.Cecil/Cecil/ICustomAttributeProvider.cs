using System;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil
{
	// Token: 0x0200004D RID: 77
	public interface ICustomAttributeProvider : IMetadataTokenProvider
	{
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000268 RID: 616
		Collection<CustomAttribute> CustomAttributes { get; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000269 RID: 617
		bool HasCustomAttributes { get; }
	}
}
