using System;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000063 RID: 99
	public interface ISecurityDeclarationProvider : IMetadataTokenProvider
	{
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060002CD RID: 717
		bool HasSecurityDeclarations { get; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060002CE RID: 718
		Collection<SecurityDeclaration> SecurityDeclarations { get; }
	}
}
