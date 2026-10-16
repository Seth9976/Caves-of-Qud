using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Symbols;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001CB RID: 459
	internal abstract class CommonReferenceManager
	{
		// Token: 0x06001364 RID: 4964
		[return: Nullable(new byte[] { 1, 0, 1, 1 })]
		internal abstract IEnumerable<KeyValuePair<MetadataReference, IAssemblySymbolInternal>> GetReferencedAssemblies();

		// Token: 0x06001365 RID: 4965
		[return: TupleElementNames(new string[] { "AssemblySymbol", "Aliases" })]
		[return: Nullable(new byte[] { 1, 0, 1, 0, 1 })]
		internal abstract IEnumerable<ValueTuple<IAssemblySymbolInternal, ImmutableArray<string>>> GetReferencedAssemblyAliases();

		// Token: 0x06001366 RID: 4966
		[NullableContext(2)]
		internal abstract MetadataReference GetMetadataReference(IAssemblySymbolInternal assemblySymbol);

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06001367 RID: 4967
		[Nullable(new byte[] { 0, 1 })]
		internal abstract ImmutableArray<MetadataReference> ExplicitReferences
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06001368 RID: 4968
		[Nullable(new byte[] { 1, 1, 2 })]
		internal abstract ImmutableDictionary<AssemblyIdentity, PortableExecutableReference> ImplicitReferenceResolutions
		{
			[return: Nullable(new byte[] { 1, 1, 2 })]
			get;
		}

		// Token: 0x040005C7 RID: 1479
		[Nullable(1)]
		internal static object SymbolCacheAndReferenceManagerStateGuard = new object();
	}
}
