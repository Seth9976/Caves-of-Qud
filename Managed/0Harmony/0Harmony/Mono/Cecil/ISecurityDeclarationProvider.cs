using System;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x02000261 RID: 609
	internal interface ISecurityDeclarationProvider : IMetadataTokenProvider
	{
		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000EC4 RID: 3780
		bool HasSecurityDeclarations { get; }

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000EC5 RID: 3781
		Collection<SecurityDeclaration> SecurityDeclarations { get; }
	}
}
