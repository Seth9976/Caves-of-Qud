using System;
using System.Collections.Immutable;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004D6 RID: 1238
	internal sealed class ConstructedMethodSymbol : SubstitutedMethodSymbol
	{
		// Token: 0x060068E3 RID: 26851 RVA: 0x0018A32D File Offset: 0x0018852D
		internal ConstructedMethodSymbol(MethodSymbol constructedFrom, ImmutableArray<TypeWithAnnotations> typeArgumentsWithAnnotations)
			: base(constructedFrom.ContainingType, new TypeMap(constructedFrom.ContainingType, constructedFrom.OriginalDefinition.TypeParameters, typeArgumentsWithAnnotations), constructedFrom.OriginalDefinition, constructedFrom)
		{
			this._typeArgumentsWithAnnotations = typeArgumentsWithAnnotations;
		}

		// Token: 0x17001A6A RID: 6762
		// (get) Token: 0x060068E4 RID: 26852 RVA: 0x0018A360 File Offset: 0x00188560
		public override ImmutableArray<TypeWithAnnotations> TypeArgumentsWithAnnotations
		{
			get
			{
				return this._typeArgumentsWithAnnotations;
			}
		}

		// Token: 0x04001A39 RID: 6713
		private readonly ImmutableArray<TypeWithAnnotations> _typeArgumentsWithAnnotations;
	}
}
