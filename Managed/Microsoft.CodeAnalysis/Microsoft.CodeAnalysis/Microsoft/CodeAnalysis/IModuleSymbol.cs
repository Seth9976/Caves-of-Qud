using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000245 RID: 581
	[NullableContext(1)]
	public interface IModuleSymbol : ISymbol, IEquatable<ISymbol>
	{
		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06001671 RID: 5745
		INamespaceSymbol GlobalNamespace { get; }

		// Token: 0x06001672 RID: 5746
		[return: Nullable(2)]
		INamespaceSymbol GetModuleNamespace(INamespaceSymbol namespaceSymbol);

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06001673 RID: 5747
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<AssemblyIdentity> ReferencedAssemblies
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06001674 RID: 5748
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<IAssemblySymbol> ReferencedAssemblySymbols
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x06001675 RID: 5749
		[NullableContext(2)]
		ModuleMetadata GetMetadata();
	}
}
