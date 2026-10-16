using System;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x02000213 RID: 531
	public interface ICustomAttributeProvider : IMetadataTokenProvider
	{
		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000B88 RID: 2952
		Collection<CustomAttribute> CustomAttributes { get; }

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000B89 RID: 2953
		bool HasCustomAttributes { get; }
	}
}
