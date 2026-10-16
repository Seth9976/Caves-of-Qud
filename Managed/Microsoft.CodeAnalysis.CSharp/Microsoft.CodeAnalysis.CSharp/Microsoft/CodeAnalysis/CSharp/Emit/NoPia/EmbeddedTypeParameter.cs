using System;
using System.Collections.Generic;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.Emit.NoPia;

namespace Microsoft.CodeAnalysis.CSharp.Emit.NoPia
{
	// Token: 0x020004A1 RID: 1185
	internal sealed class EmbeddedTypeParameter : EmbeddedTypesManager<PEModuleBuilder, ModuleCompilationState, EmbeddedTypesManager, SyntaxNode, CSharpAttributeData, Symbol, AssemblySymbol, NamedTypeSymbol, FieldSymbol, MethodSymbol, EventSymbol, PropertySymbol, ParameterSymbol, TypeParameterSymbol, EmbeddedType, EmbeddedField, EmbeddedMethod, EmbeddedEvent, EmbeddedProperty, EmbeddedParameter, EmbeddedTypeParameter>.CommonEmbeddedTypeParameter
	{
		// Token: 0x06006222 RID: 25122 RVA: 0x00178E49 File Offset: 0x00177049
		public EmbeddedTypeParameter(EmbeddedMethod containingMethod, TypeParameterSymbol underlyingTypeParameter)
			: base(containingMethod, underlyingTypeParameter)
		{
		}

		// Token: 0x06006223 RID: 25123 RVA: 0x00178E53 File Offset: 0x00177053
		protected override IEnumerable<TypeReferenceWithAttributes> GetConstraints(EmitContext context)
		{
			return ((IGenericParameter)this.UnderlyingTypeParameter).GetConstraints(context);
		}

		// Token: 0x1700170E RID: 5902
		// (get) Token: 0x06006224 RID: 25124 RVA: 0x00178E61 File Offset: 0x00177061
		protected override bool MustBeReferenceType
		{
			get
			{
				return this.UnderlyingTypeParameter.AdaptedTypeParameterSymbol.HasReferenceTypeConstraint;
			}
		}

		// Token: 0x1700170F RID: 5903
		// (get) Token: 0x06006225 RID: 25125 RVA: 0x00178E73 File Offset: 0x00177073
		protected override bool MustBeValueType
		{
			get
			{
				return this.UnderlyingTypeParameter.AdaptedTypeParameterSymbol.HasValueTypeConstraint;
			}
		}

		// Token: 0x17001710 RID: 5904
		// (get) Token: 0x06006226 RID: 25126 RVA: 0x00178E85 File Offset: 0x00177085
		protected override bool MustHaveDefaultConstructor
		{
			get
			{
				return this.UnderlyingTypeParameter.AdaptedTypeParameterSymbol.HasConstructorConstraint;
			}
		}

		// Token: 0x17001711 RID: 5905
		// (get) Token: 0x06006227 RID: 25127 RVA: 0x00178E97 File Offset: 0x00177097
		protected override string Name
		{
			get
			{
				return this.UnderlyingTypeParameter.AdaptedTypeParameterSymbol.MetadataName;
			}
		}

		// Token: 0x17001712 RID: 5906
		// (get) Token: 0x06006228 RID: 25128 RVA: 0x00178EA9 File Offset: 0x001770A9
		protected override ushort Index
		{
			get
			{
				return (ushort)this.UnderlyingTypeParameter.AdaptedTypeParameterSymbol.Ordinal;
			}
		}
	}
}
