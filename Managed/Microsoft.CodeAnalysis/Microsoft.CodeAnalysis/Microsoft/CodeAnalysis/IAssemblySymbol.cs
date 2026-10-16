using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200023B RID: 571
	[NullableContext(1)]
	public interface IAssemblySymbol : ISymbol, IEquatable<ISymbol>
	{
		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06001614 RID: 5652
		bool IsInteractive { get; }

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06001615 RID: 5653
		AssemblyIdentity Identity { get; }

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06001616 RID: 5654
		INamespaceSymbol GlobalNamespace { get; }

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06001617 RID: 5655
		IEnumerable<IModuleSymbol> Modules { get; }

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06001618 RID: 5656
		ICollection<string> TypeNames { get; }

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06001619 RID: 5657
		ICollection<string> NamespaceNames { get; }

		// Token: 0x0600161A RID: 5658
		bool GivesAccessTo(IAssemblySymbol toAssembly);

		// Token: 0x0600161B RID: 5659
		[return: Nullable(2)]
		INamedTypeSymbol GetTypeByMetadataName(string fullyQualifiedMetadataName);

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x0600161C RID: 5660
		bool MightContainExtensionMethods { get; }

		// Token: 0x0600161D RID: 5661
		[return: Nullable(2)]
		INamedTypeSymbol ResolveForwardedType(string fullyQualifiedMetadataName);

		// Token: 0x0600161E RID: 5662
		[return: Nullable(new byte[] { 0, 1 })]
		ImmutableArray<INamedTypeSymbol> GetForwardedTypes();

		// Token: 0x0600161F RID: 5663
		[NullableContext(2)]
		AssemblyMetadata GetMetadata();
	}
}
