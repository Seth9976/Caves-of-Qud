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
	// Token: 0x0200049E RID: 1182
	internal sealed class EmbeddedParameter : EmbeddedTypesManager<PEModuleBuilder, ModuleCompilationState, EmbeddedTypesManager, SyntaxNode, CSharpAttributeData, Symbol, AssemblySymbol, NamedTypeSymbol, FieldSymbol, MethodSymbol, EventSymbol, PropertySymbol, ParameterSymbol, TypeParameterSymbol, EmbeddedType, EmbeddedField, EmbeddedMethod, EmbeddedEvent, EmbeddedProperty, EmbeddedParameter, EmbeddedTypeParameter>.CommonEmbeddedParameter
	{
		// Token: 0x060061F3 RID: 25075 RVA: 0x00178865 File Offset: 0x00176A65
		public EmbeddedParameter(EmbeddedTypesManager<PEModuleBuilder, ModuleCompilationState, EmbeddedTypesManager, SyntaxNode, CSharpAttributeData, Symbol, AssemblySymbol, NamedTypeSymbol, FieldSymbol, MethodSymbol, EventSymbol, PropertySymbol, ParameterSymbol, TypeParameterSymbol, EmbeddedType, EmbeddedField, EmbeddedMethod, EmbeddedEvent, EmbeddedProperty, EmbeddedParameter, EmbeddedTypeParameter>.CommonEmbeddedMember containingPropertyOrMethod, ParameterSymbol underlyingParameter)
			: base(containingPropertyOrMethod, underlyingParameter)
		{
		}

		// Token: 0x170016F3 RID: 5875
		// (get) Token: 0x060061F4 RID: 25076 RVA: 0x0017886F File Offset: 0x00176A6F
		protected override bool HasDefaultValue
		{
			get
			{
				return this.UnderlyingParameter.AdaptedParameterSymbol.HasMetadataConstantValue;
			}
		}

		// Token: 0x060061F5 RID: 25077 RVA: 0x00178881 File Offset: 0x00176A81
		protected override IEnumerable<CSharpAttributeData> GetCustomAttributesToEmit(PEModuleBuilder moduleBuilder)
		{
			return this.UnderlyingParameter.AdaptedParameterSymbol.GetCustomAttributesToEmit(moduleBuilder);
		}

		// Token: 0x060061F6 RID: 25078 RVA: 0x00178894 File Offset: 0x00176A94
		protected override MetadataConstant GetDefaultValue(EmitContext context)
		{
			return this.UnderlyingParameter.GetMetadataConstantValue(context);
		}

		// Token: 0x170016F4 RID: 5876
		// (get) Token: 0x060061F7 RID: 25079 RVA: 0x001788A2 File Offset: 0x00176AA2
		protected override bool IsIn
		{
			get
			{
				return this.UnderlyingParameter.AdaptedParameterSymbol.IsMetadataIn;
			}
		}

		// Token: 0x170016F5 RID: 5877
		// (get) Token: 0x060061F8 RID: 25080 RVA: 0x001788B4 File Offset: 0x00176AB4
		protected override bool IsOut
		{
			get
			{
				return this.UnderlyingParameter.AdaptedParameterSymbol.IsMetadataOut;
			}
		}

		// Token: 0x170016F6 RID: 5878
		// (get) Token: 0x060061F9 RID: 25081 RVA: 0x001788C6 File Offset: 0x00176AC6
		protected override bool IsOptional
		{
			get
			{
				return this.UnderlyingParameter.AdaptedParameterSymbol.IsMetadataOptional;
			}
		}

		// Token: 0x170016F7 RID: 5879
		// (get) Token: 0x060061FA RID: 25082 RVA: 0x001788D8 File Offset: 0x00176AD8
		protected override bool IsMarshalledExplicitly
		{
			get
			{
				return this.UnderlyingParameter.AdaptedParameterSymbol.IsMarshalledExplicitly;
			}
		}

		// Token: 0x170016F8 RID: 5880
		// (get) Token: 0x060061FB RID: 25083 RVA: 0x001788EA File Offset: 0x00176AEA
		protected override IMarshallingInformation MarshallingInformation
		{
			get
			{
				return this.UnderlyingParameter.AdaptedParameterSymbol.MarshallingInformation;
			}
		}

		// Token: 0x170016F9 RID: 5881
		// (get) Token: 0x060061FC RID: 25084 RVA: 0x001788FC File Offset: 0x00176AFC
		protected override ImmutableArray<byte> MarshallingDescriptor
		{
			get
			{
				return this.UnderlyingParameter.AdaptedParameterSymbol.MarshallingDescriptor;
			}
		}

		// Token: 0x170016FA RID: 5882
		// (get) Token: 0x060061FD RID: 25085 RVA: 0x0017890E File Offset: 0x00176B0E
		protected override string Name
		{
			get
			{
				return this.UnderlyingParameter.AdaptedParameterSymbol.MetadataName;
			}
		}

		// Token: 0x170016FB RID: 5883
		// (get) Token: 0x060061FE RID: 25086 RVA: 0x00178920 File Offset: 0x00176B20
		protected override IParameterTypeInformation UnderlyingParameterTypeInformation
		{
			get
			{
				return this.UnderlyingParameter;
			}
		}

		// Token: 0x170016FC RID: 5884
		// (get) Token: 0x060061FF RID: 25087 RVA: 0x00178928 File Offset: 0x00176B28
		protected override ushort Index
		{
			get
			{
				return (ushort)this.UnderlyingParameter.AdaptedParameterSymbol.Ordinal;
			}
		}
	}
}
