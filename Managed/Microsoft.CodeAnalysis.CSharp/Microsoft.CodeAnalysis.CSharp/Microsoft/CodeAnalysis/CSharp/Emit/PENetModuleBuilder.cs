using System;
using System.Collections.Generic;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Emit
{
	// Token: 0x02000494 RID: 1172
	internal sealed class PENetModuleBuilder : PEModuleBuilder
	{
		// Token: 0x0600618D RID: 24973 RVA: 0x0017800D File Offset: 0x0017620D
		internal PENetModuleBuilder(SourceModuleSymbol sourceModule, EmitOptions emitOptions, ModulePropertiesForSerialization serializationProperties, IEnumerable<ResourceDescription> manifestResources)
			: base(sourceModule, emitOptions, OutputKind.NetModule, serializationProperties, manifestResources)
		{
		}

		// Token: 0x0600618E RID: 24974 RVA: 0x0017801B File Offset: 0x0017621B
		internal override SynthesizedAttributeData SynthesizeEmbeddedAttribute()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x0600618F RID: 24975 RVA: 0x00178022 File Offset: 0x00176222
		protected override void AddEmbeddedResourcesFromAddedModules(ArrayBuilder<ManagedResource> builder, DiagnosticBag diagnostics)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x170016B9 RID: 5817
		// (get) Token: 0x06006190 RID: 24976 RVA: 0x00178029 File Offset: 0x00176229
		public override int CurrentGenerationOrdinal
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06006191 RID: 24977 RVA: 0x0017802C File Offset: 0x0017622C
		public override IEnumerable<IFileReference> GetFiles(EmitContext context)
		{
			return SpecializedCollections.EmptyEnumerable<IFileReference>();
		}

		// Token: 0x170016BA RID: 5818
		// (get) Token: 0x06006192 RID: 24978 RVA: 0x00178033 File Offset: 0x00176233
		public override ISourceAssemblySymbolInternal SourceAssemblyOpt
		{
			get
			{
				return null;
			}
		}
	}
}
