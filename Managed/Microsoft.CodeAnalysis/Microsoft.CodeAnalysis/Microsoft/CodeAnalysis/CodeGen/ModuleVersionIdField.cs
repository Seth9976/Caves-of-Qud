using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.Cci;

namespace Microsoft.CodeAnalysis.CodeGen
{
	// Token: 0x020004DA RID: 1242
	internal sealed class ModuleVersionIdField : SynthesizedStaticField
	{
		// Token: 0x0600320E RID: 12814 RVA: 0x0008E87C File Offset: 0x0008CA7C
		[NullableContext(1)]
		internal ModuleVersionIdField(INamedTypeDefinition containingType, ITypeReference type)
			: base("MVID", containingType, type)
		{
		}

		// Token: 0x17000E69 RID: 3689
		// (get) Token: 0x0600320F RID: 12815 RVA: 0x0008E88C File Offset: 0x0008CA8C
		public override ImmutableArray<byte> MappedData
		{
			get
			{
				return default(ImmutableArray<byte>);
			}
		}
	}
}
