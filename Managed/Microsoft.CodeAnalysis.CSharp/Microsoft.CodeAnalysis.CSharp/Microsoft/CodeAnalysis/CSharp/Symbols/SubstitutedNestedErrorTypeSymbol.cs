using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004E3 RID: 1251
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class SubstitutedNestedErrorTypeSymbol : SubstitutedErrorTypeSymbol
	{
		// Token: 0x060069DA RID: 27098 RVA: 0x0018ADA1 File Offset: 0x00188FA1
		public SubstitutedNestedErrorTypeSymbol(NamedTypeSymbol containingSymbol, ErrorTypeSymbol originalDefinition)
			: base(originalDefinition, null)
		{
			this._containingSymbol = containingSymbol;
			this._map = containingSymbol.TypeSubstitution.WithAlphaRename(originalDefinition, this, out this._typeParameters);
		}

		// Token: 0x17001B0B RID: 6923
		// (get) Token: 0x060069DB RID: 27099 RVA: 0x0018ADCB File Offset: 0x00188FCB
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<TypeParameterSymbol> TypeParameters
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._typeParameters;
			}
		}

		// Token: 0x17001B0C RID: 6924
		// (get) Token: 0x060069DC RID: 27100 RVA: 0x0018ADD3 File Offset: 0x00188FD3
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal override ImmutableArray<TypeWithAnnotations> TypeArgumentsWithAnnotationsNoUseSiteDiagnostics
		{
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			get
			{
				return base.GetTypeParametersAsTypeArguments();
			}
		}

		// Token: 0x17001B0D RID: 6925
		// (get) Token: 0x060069DD RID: 27101 RVA: 0x0018ADDB File Offset: 0x00188FDB
		public override NamedTypeSymbol ConstructedFrom
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17001B0E RID: 6926
		// (get) Token: 0x060069DE RID: 27102 RVA: 0x0018ADDE File Offset: 0x00188FDE
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._containingSymbol;
			}
		}

		// Token: 0x17001B0F RID: 6927
		// (get) Token: 0x060069DF RID: 27103 RVA: 0x0018ADE6 File Offset: 0x00188FE6
		internal override TypeMap TypeSubstitution
		{
			get
			{
				return this._map;
			}
		}

		// Token: 0x060069E0 RID: 27104 RVA: 0x0018ADEE File Offset: 0x00188FEE
		protected override NamedTypeSymbol WithTupleDataCore(NamedTypeSymbol.TupleExtraData newData)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x04001A50 RID: 6736
		private readonly NamedTypeSymbol _containingSymbol;

		// Token: 0x04001A51 RID: 6737
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private readonly ImmutableArray<TypeParameterSymbol> _typeParameters;

		// Token: 0x04001A52 RID: 6738
		private readonly TypeMap _map;
	}
}
