using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.Cci;

namespace Microsoft.CodeAnalysis.CodeGen
{
	// Token: 0x020004DC RID: 1244
	internal sealed class MappedField : SynthesizedStaticField
	{
		// Token: 0x06003212 RID: 12818 RVA: 0x0008E8D6 File Offset: 0x0008CAD6
		[NullableContext(1)]
		internal MappedField(string name, INamedTypeDefinition containingType, ITypeReference type, [Nullable(0)] ImmutableArray<byte> block)
			: base(name, containingType, type)
		{
			this._block = block;
		}

		// Token: 0x17000E6B RID: 3691
		// (get) Token: 0x06003213 RID: 12819 RVA: 0x0008E8E9 File Offset: 0x0008CAE9
		public override ImmutableArray<byte> MappedData
		{
			get
			{
				return this._block;
			}
		}

		// Token: 0x0400135A RID: 4954
		private readonly ImmutableArray<byte> _block;
	}
}
