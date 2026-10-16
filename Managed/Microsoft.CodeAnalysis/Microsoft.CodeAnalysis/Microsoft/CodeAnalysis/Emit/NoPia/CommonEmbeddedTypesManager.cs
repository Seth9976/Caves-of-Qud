using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.Cci;

namespace Microsoft.CodeAnalysis.Emit.NoPia
{
	// Token: 0x02000498 RID: 1176
	internal abstract class CommonEmbeddedTypesManager
	{
		// Token: 0x17000D52 RID: 3410
		// (get) Token: 0x06002E50 RID: 11856
		public abstract bool IsFrozen { get; }

		// Token: 0x06002E51 RID: 11857
		public abstract ImmutableArray<INamespaceTypeDefinition> GetTypes(DiagnosticBag diagnostics, HashSet<string> namesOfTopLevelTypes);
	}
}
