using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.Emit;

namespace Microsoft.CodeAnalysis.CSharp.Emit
{
	// Token: 0x02000492 RID: 1170
	internal sealed class PEAssemblyBuilder : PEAssemblyBuilderBase
	{
		// Token: 0x0600612A RID: 24874 RVA: 0x001765AC File Offset: 0x001747AC
		public PEAssemblyBuilder(SourceAssemblySymbol sourceAssembly, EmitOptions emitOptions, OutputKind outputKind, ModulePropertiesForSerialization serializationProperties, IEnumerable<ResourceDescription> manifestResources)
			: base(sourceAssembly, emitOptions, outputKind, serializationProperties, manifestResources, ImmutableArray<NamedTypeSymbol>.Empty)
		{
		}

		// Token: 0x170016AF RID: 5807
		// (get) Token: 0x0600612B RID: 24875 RVA: 0x001765C0 File Offset: 0x001747C0
		public override int CurrentGenerationOrdinal
		{
			get
			{
				return 0;
			}
		}
	}
}
