using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CodeGen;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.Emit.NoPia;

namespace Microsoft.CodeAnalysis.CSharp.Emit.NoPia
{
	// Token: 0x0200049C RID: 1180
	internal sealed class EmbeddedField : EmbeddedTypesManager<PEModuleBuilder, ModuleCompilationState, EmbeddedTypesManager, SyntaxNode, CSharpAttributeData, Symbol, AssemblySymbol, NamedTypeSymbol, FieldSymbol, MethodSymbol, EventSymbol, PropertySymbol, ParameterSymbol, TypeParameterSymbol, EmbeddedType, EmbeddedField, EmbeddedMethod, EmbeddedEvent, EmbeddedProperty, EmbeddedParameter, EmbeddedTypeParameter>.CommonEmbeddedField
	{
		// Token: 0x060061C9 RID: 25033 RVA: 0x0017855F File Offset: 0x0017675F
		public EmbeddedField(EmbeddedType containingType, FieldSymbol underlyingField)
			: base(containingType, underlyingField)
		{
		}

		// Token: 0x170016D1 RID: 5841
		// (get) Token: 0x060061CA RID: 25034 RVA: 0x00178569 File Offset: 0x00176769
		internal override EmbeddedTypesManager TypeManager
		{
			get
			{
				return this.ContainingType.TypeManager;
			}
		}

		// Token: 0x060061CB RID: 25035 RVA: 0x00178576 File Offset: 0x00176776
		protected override IEnumerable<CSharpAttributeData> GetCustomAttributesToEmit(PEModuleBuilder moduleBuilder)
		{
			return base.UnderlyingField.AdaptedFieldSymbol.GetCustomAttributesToEmit(moduleBuilder);
		}

		// Token: 0x060061CC RID: 25036 RVA: 0x00178589 File Offset: 0x00176789
		protected override MetadataConstant GetCompileTimeValue(EmitContext context)
		{
			return base.UnderlyingField.GetMetadataConstantValue(context);
		}

		// Token: 0x170016D2 RID: 5842
		// (get) Token: 0x060061CD RID: 25037 RVA: 0x00178597 File Offset: 0x00176797
		protected override bool IsCompileTimeConstant
		{
			get
			{
				return base.UnderlyingField.AdaptedFieldSymbol.IsMetadataConstant;
			}
		}

		// Token: 0x170016D3 RID: 5843
		// (get) Token: 0x060061CE RID: 25038 RVA: 0x001785A9 File Offset: 0x001767A9
		protected override bool IsNotSerialized
		{
			get
			{
				return base.UnderlyingField.AdaptedFieldSymbol.IsNotSerialized;
			}
		}

		// Token: 0x170016D4 RID: 5844
		// (get) Token: 0x060061CF RID: 25039 RVA: 0x001785BB File Offset: 0x001767BB
		protected override bool IsReadOnly
		{
			get
			{
				return base.UnderlyingField.AdaptedFieldSymbol.IsReadOnly;
			}
		}

		// Token: 0x170016D5 RID: 5845
		// (get) Token: 0x060061D0 RID: 25040 RVA: 0x001785CD File Offset: 0x001767CD
		protected override bool IsRuntimeSpecial
		{
			get
			{
				return base.UnderlyingField.AdaptedFieldSymbol.HasRuntimeSpecialName;
			}
		}

		// Token: 0x170016D6 RID: 5846
		// (get) Token: 0x060061D1 RID: 25041 RVA: 0x001785DF File Offset: 0x001767DF
		protected override bool IsSpecialName
		{
			get
			{
				return base.UnderlyingField.AdaptedFieldSymbol.HasSpecialName;
			}
		}

		// Token: 0x170016D7 RID: 5847
		// (get) Token: 0x060061D2 RID: 25042 RVA: 0x001785F1 File Offset: 0x001767F1
		protected override bool IsStatic
		{
			get
			{
				return base.UnderlyingField.AdaptedFieldSymbol.IsStatic;
			}
		}

		// Token: 0x170016D8 RID: 5848
		// (get) Token: 0x060061D3 RID: 25043 RVA: 0x00178603 File Offset: 0x00176803
		protected override bool IsMarshalledExplicitly
		{
			get
			{
				return base.UnderlyingField.AdaptedFieldSymbol.IsMarshalledExplicitly;
			}
		}

		// Token: 0x170016D9 RID: 5849
		// (get) Token: 0x060061D4 RID: 25044 RVA: 0x00178615 File Offset: 0x00176815
		protected override IMarshallingInformation MarshallingInformation
		{
			get
			{
				return base.UnderlyingField.AdaptedFieldSymbol.MarshallingInformation;
			}
		}

		// Token: 0x170016DA RID: 5850
		// (get) Token: 0x060061D5 RID: 25045 RVA: 0x00178627 File Offset: 0x00176827
		protected override ImmutableArray<byte> MarshallingDescriptor
		{
			get
			{
				return base.UnderlyingField.AdaptedFieldSymbol.MarshallingDescriptor;
			}
		}

		// Token: 0x170016DB RID: 5851
		// (get) Token: 0x060061D6 RID: 25046 RVA: 0x00178639 File Offset: 0x00176839
		protected override int? TypeLayoutOffset
		{
			get
			{
				return base.UnderlyingField.AdaptedFieldSymbol.TypeLayoutOffset;
			}
		}

		// Token: 0x170016DC RID: 5852
		// (get) Token: 0x060061D7 RID: 25047 RVA: 0x0017864B File Offset: 0x0017684B
		protected override TypeMemberVisibility Visibility
		{
			get
			{
				return PEModuleBuilder.MemberVisibility(base.UnderlyingField.AdaptedFieldSymbol);
			}
		}

		// Token: 0x170016DD RID: 5853
		// (get) Token: 0x060061D8 RID: 25048 RVA: 0x0017865D File Offset: 0x0017685D
		protected override string Name
		{
			get
			{
				return base.UnderlyingField.AdaptedFieldSymbol.MetadataName;
			}
		}
	}
}
