using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004E2 RID: 1250
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class ConstructedErrorTypeSymbol : SubstitutedErrorTypeSymbol
	{
		// Token: 0x060069D3 RID: 27091 RVA: 0x0018AD1C File Offset: 0x00188F1C
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public ConstructedErrorTypeSymbol([global::System.Runtime.CompilerServices.Nullable(1)] ErrorTypeSymbol constructedFrom, ImmutableArray<TypeWithAnnotations> typeArgumentsWithAnnotations, [global::System.Runtime.CompilerServices.Nullable(2)] NamedTypeSymbol.TupleExtraData tupleData = null)
			: base((ErrorTypeSymbol)constructedFrom.OriginalDefinition, tupleData)
		{
			this._constructedFrom = constructedFrom;
			this._typeArgumentsWithAnnotations = typeArgumentsWithAnnotations;
			this._map = new TypeMap(constructedFrom.ContainingType, constructedFrom.OriginalDefinition.TypeParameters, typeArgumentsWithAnnotations);
		}

		// Token: 0x060069D4 RID: 27092 RVA: 0x0018AD5B File Offset: 0x00188F5B
		protected override NamedTypeSymbol WithTupleDataCore(NamedTypeSymbol.TupleExtraData newData)
		{
			return new ConstructedErrorTypeSymbol(this._constructedFrom, this._typeArgumentsWithAnnotations, newData);
		}

		// Token: 0x17001B06 RID: 6918
		// (get) Token: 0x060069D5 RID: 27093 RVA: 0x0018AD6F File Offset: 0x00188F6F
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<TypeParameterSymbol> TypeParameters
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._constructedFrom.TypeParameters;
			}
		}

		// Token: 0x17001B07 RID: 6919
		// (get) Token: 0x060069D6 RID: 27094 RVA: 0x0018AD7C File Offset: 0x00188F7C
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal override ImmutableArray<TypeWithAnnotations> TypeArgumentsWithAnnotationsNoUseSiteDiagnostics
		{
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			get
			{
				return this._typeArgumentsWithAnnotations;
			}
		}

		// Token: 0x17001B08 RID: 6920
		// (get) Token: 0x060069D7 RID: 27095 RVA: 0x0018AD84 File Offset: 0x00188F84
		public override NamedTypeSymbol ConstructedFrom
		{
			get
			{
				return this._constructedFrom;
			}
		}

		// Token: 0x17001B09 RID: 6921
		// (get) Token: 0x060069D8 RID: 27096 RVA: 0x0018AD8C File Offset: 0x00188F8C
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override Symbol ContainingSymbol
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this._constructedFrom.ContainingSymbol;
			}
		}

		// Token: 0x17001B0A RID: 6922
		// (get) Token: 0x060069D9 RID: 27097 RVA: 0x0018AD99 File Offset: 0x00188F99
		internal override TypeMap TypeSubstitution
		{
			get
			{
				return this._map;
			}
		}

		// Token: 0x04001A4D RID: 6733
		private readonly ErrorTypeSymbol _constructedFrom;

		// Token: 0x04001A4E RID: 6734
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private readonly ImmutableArray<TypeWithAnnotations> _typeArgumentsWithAnnotations;

		// Token: 0x04001A4F RID: 6735
		private readonly TypeMap _map;
	}
}
