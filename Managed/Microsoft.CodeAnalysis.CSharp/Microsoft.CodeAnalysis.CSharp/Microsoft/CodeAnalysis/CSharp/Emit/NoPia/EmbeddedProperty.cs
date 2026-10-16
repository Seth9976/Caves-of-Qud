using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.Emit.NoPia;

namespace Microsoft.CodeAnalysis.CSharp.Emit.NoPia
{
	// Token: 0x0200049F RID: 1183
	internal sealed class EmbeddedProperty : EmbeddedTypesManager<PEModuleBuilder, ModuleCompilationState, EmbeddedTypesManager, SyntaxNode, CSharpAttributeData, Symbol, AssemblySymbol, NamedTypeSymbol, FieldSymbol, MethodSymbol, EventSymbol, PropertySymbol, ParameterSymbol, TypeParameterSymbol, EmbeddedType, EmbeddedField, EmbeddedMethod, EmbeddedEvent, EmbeddedProperty, EmbeddedParameter, EmbeddedTypeParameter>.CommonEmbeddedProperty
	{
		// Token: 0x06006200 RID: 25088 RVA: 0x0017893B File Offset: 0x00176B3B
		public EmbeddedProperty(PropertySymbol underlyingProperty, EmbeddedMethod getter, EmbeddedMethod setter)
			: base(underlyingProperty, getter, setter)
		{
		}

		// Token: 0x06006201 RID: 25089 RVA: 0x00178946 File Offset: 0x00176B46
		protected override IEnumerable<CSharpAttributeData> GetCustomAttributesToEmit(PEModuleBuilder moduleBuilder)
		{
			return base.UnderlyingProperty.AdaptedPropertySymbol.GetCustomAttributesToEmit(moduleBuilder);
		}

		// Token: 0x06006202 RID: 25090 RVA: 0x00178959 File Offset: 0x00176B59
		protected override ImmutableArray<EmbeddedParameter> GetParameters()
		{
			return EmbeddedTypesManager.EmbedParameters(this, base.UnderlyingProperty.AdaptedPropertySymbol.Parameters);
		}

		// Token: 0x170016FD RID: 5885
		// (get) Token: 0x06006203 RID: 25091 RVA: 0x00178971 File Offset: 0x00176B71
		protected override bool IsRuntimeSpecial
		{
			get
			{
				return base.UnderlyingProperty.AdaptedPropertySymbol.HasRuntimeSpecialName;
			}
		}

		// Token: 0x170016FE RID: 5886
		// (get) Token: 0x06006204 RID: 25092 RVA: 0x00178983 File Offset: 0x00176B83
		protected override bool IsSpecialName
		{
			get
			{
				return base.UnderlyingProperty.AdaptedPropertySymbol.HasSpecialName;
			}
		}

		// Token: 0x170016FF RID: 5887
		// (get) Token: 0x06006205 RID: 25093 RVA: 0x00178995 File Offset: 0x00176B95
		protected override ISignature UnderlyingPropertySignature
		{
			get
			{
				return base.UnderlyingProperty;
			}
		}

		// Token: 0x17001700 RID: 5888
		// (get) Token: 0x06006206 RID: 25094 RVA: 0x0017899D File Offset: 0x00176B9D
		protected override EmbeddedType ContainingType
		{
			get
			{
				return base.AnAccessor.ContainingType;
			}
		}

		// Token: 0x17001701 RID: 5889
		// (get) Token: 0x06006207 RID: 25095 RVA: 0x001789AA File Offset: 0x00176BAA
		protected override TypeMemberVisibility Visibility
		{
			get
			{
				return PEModuleBuilder.MemberVisibility(base.UnderlyingProperty.AdaptedPropertySymbol);
			}
		}

		// Token: 0x17001702 RID: 5890
		// (get) Token: 0x06006208 RID: 25096 RVA: 0x001789BC File Offset: 0x00176BBC
		protected override string Name
		{
			get
			{
				return base.UnderlyingProperty.AdaptedPropertySymbol.MetadataName;
			}
		}
	}
}
