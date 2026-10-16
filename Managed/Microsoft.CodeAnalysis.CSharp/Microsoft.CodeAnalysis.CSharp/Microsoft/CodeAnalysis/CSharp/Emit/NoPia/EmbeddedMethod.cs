using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.Emit.NoPia;

namespace Microsoft.CodeAnalysis.CSharp.Emit.NoPia
{
	// Token: 0x0200049D RID: 1181
	internal sealed class EmbeddedMethod : EmbeddedTypesManager<PEModuleBuilder, ModuleCompilationState, EmbeddedTypesManager, SyntaxNode, CSharpAttributeData, Symbol, AssemblySymbol, NamedTypeSymbol, FieldSymbol, MethodSymbol, EventSymbol, PropertySymbol, ParameterSymbol, TypeParameterSymbol, EmbeddedType, EmbeddedField, EmbeddedMethod, EmbeddedEvent, EmbeddedProperty, EmbeddedParameter, EmbeddedTypeParameter>.CommonEmbeddedMethod
	{
		// Token: 0x060061D9 RID: 25049 RVA: 0x0017866F File Offset: 0x0017686F
		public EmbeddedMethod(EmbeddedType containingType, MethodSymbol underlyingMethod)
			: base(containingType, underlyingMethod)
		{
		}

		// Token: 0x170016DE RID: 5854
		// (get) Token: 0x060061DA RID: 25050 RVA: 0x00178679 File Offset: 0x00176879
		internal override EmbeddedTypesManager TypeManager
		{
			get
			{
				return this.ContainingType.TypeManager;
			}
		}

		// Token: 0x060061DB RID: 25051 RVA: 0x00178686 File Offset: 0x00176886
		protected override IEnumerable<CSharpAttributeData> GetCustomAttributesToEmit(PEModuleBuilder moduleBuilder)
		{
			return base.UnderlyingMethod.AdaptedSymbol.GetCustomAttributesToEmit(moduleBuilder);
		}

		// Token: 0x060061DC RID: 25052 RVA: 0x00178699 File Offset: 0x00176899
		protected override ImmutableArray<EmbeddedParameter> GetParameters()
		{
			return EmbeddedTypesManager.EmbedParameters(this, base.UnderlyingMethod.AdaptedMethodSymbol.Parameters);
		}

		// Token: 0x060061DD RID: 25053 RVA: 0x001786B1 File Offset: 0x001768B1
		protected override ImmutableArray<EmbeddedTypeParameter> GetTypeParameters()
		{
			return base.UnderlyingMethod.AdaptedMethodSymbol.TypeParameters.SelectAsArray((TypeParameterSymbol t, EmbeddedMethod m) => new EmbeddedTypeParameter(m, t.GetCciAdapter()), this);
		}

		// Token: 0x170016DF RID: 5855
		// (get) Token: 0x060061DE RID: 25054 RVA: 0x001786E8 File Offset: 0x001768E8
		protected override bool IsAbstract
		{
			get
			{
				return base.UnderlyingMethod.AdaptedMethodSymbol.IsAbstract;
			}
		}

		// Token: 0x170016E0 RID: 5856
		// (get) Token: 0x060061DF RID: 25055 RVA: 0x001786FA File Offset: 0x001768FA
		protected override bool IsAccessCheckedOnOverride
		{
			get
			{
				return base.UnderlyingMethod.AdaptedMethodSymbol.IsAccessCheckedOnOverride;
			}
		}

		// Token: 0x170016E1 RID: 5857
		// (get) Token: 0x060061E0 RID: 25056 RVA: 0x0017870C File Offset: 0x0017690C
		protected override bool IsConstructor
		{
			get
			{
				return base.UnderlyingMethod.AdaptedMethodSymbol.MethodKind == MethodKind.Constructor;
			}
		}

		// Token: 0x170016E2 RID: 5858
		// (get) Token: 0x060061E1 RID: 25057 RVA: 0x00178721 File Offset: 0x00176921
		protected override bool IsExternal
		{
			get
			{
				return base.UnderlyingMethod.AdaptedMethodSymbol.IsExternal;
			}
		}

		// Token: 0x170016E3 RID: 5859
		// (get) Token: 0x060061E2 RID: 25058 RVA: 0x00178733 File Offset: 0x00176933
		protected override bool IsHiddenBySignature
		{
			get
			{
				return !base.UnderlyingMethod.AdaptedMethodSymbol.HidesBaseMethodsByName;
			}
		}

		// Token: 0x170016E4 RID: 5860
		// (get) Token: 0x060061E3 RID: 25059 RVA: 0x00178748 File Offset: 0x00176948
		protected override bool IsNewSlot
		{
			get
			{
				return base.UnderlyingMethod.AdaptedMethodSymbol.IsMetadataNewSlot(false);
			}
		}

		// Token: 0x170016E5 RID: 5861
		// (get) Token: 0x060061E4 RID: 25060 RVA: 0x0017875B File Offset: 0x0017695B
		protected override IPlatformInvokeInformation PlatformInvokeData
		{
			get
			{
				return base.UnderlyingMethod.AdaptedMethodSymbol.GetDllImportData();
			}
		}

		// Token: 0x170016E6 RID: 5862
		// (get) Token: 0x060061E5 RID: 25061 RVA: 0x0017876D File Offset: 0x0017696D
		protected override bool IsRuntimeSpecial
		{
			get
			{
				return base.UnderlyingMethod.AdaptedMethodSymbol.HasRuntimeSpecialName;
			}
		}

		// Token: 0x170016E7 RID: 5863
		// (get) Token: 0x060061E6 RID: 25062 RVA: 0x0017877F File Offset: 0x0017697F
		protected override bool IsSpecialName
		{
			get
			{
				return base.UnderlyingMethod.AdaptedMethodSymbol.HasSpecialName;
			}
		}

		// Token: 0x170016E8 RID: 5864
		// (get) Token: 0x060061E7 RID: 25063 RVA: 0x00178791 File Offset: 0x00176991
		protected override bool IsSealed
		{
			get
			{
				return base.UnderlyingMethod.AdaptedMethodSymbol.IsMetadataFinal;
			}
		}

		// Token: 0x170016E9 RID: 5865
		// (get) Token: 0x060061E8 RID: 25064 RVA: 0x001787A3 File Offset: 0x001769A3
		protected override bool IsStatic
		{
			get
			{
				return base.UnderlyingMethod.AdaptedMethodSymbol.IsStatic;
			}
		}

		// Token: 0x170016EA RID: 5866
		// (get) Token: 0x060061E9 RID: 25065 RVA: 0x001787B5 File Offset: 0x001769B5
		protected override bool IsVirtual
		{
			get
			{
				return base.UnderlyingMethod.AdaptedMethodSymbol.IsMetadataVirtual(false);
			}
		}

		// Token: 0x060061EA RID: 25066 RVA: 0x001787C8 File Offset: 0x001769C8
		protected override MethodImplAttributes GetImplementationAttributes(EmitContext context)
		{
			return base.UnderlyingMethod.AdaptedMethodSymbol.ImplementationAttributes;
		}

		// Token: 0x170016EB RID: 5867
		// (get) Token: 0x060061EB RID: 25067 RVA: 0x001787DA File Offset: 0x001769DA
		protected override bool ReturnValueIsMarshalledExplicitly
		{
			get
			{
				return base.UnderlyingMethod.AdaptedMethodSymbol.ReturnValueIsMarshalledExplicitly;
			}
		}

		// Token: 0x170016EC RID: 5868
		// (get) Token: 0x060061EC RID: 25068 RVA: 0x001787EC File Offset: 0x001769EC
		protected override IMarshallingInformation ReturnValueMarshallingInformation
		{
			get
			{
				return base.UnderlyingMethod.AdaptedMethodSymbol.ReturnValueMarshallingInformation;
			}
		}

		// Token: 0x170016ED RID: 5869
		// (get) Token: 0x060061ED RID: 25069 RVA: 0x001787FE File Offset: 0x001769FE
		protected override ImmutableArray<byte> ReturnValueMarshallingDescriptor
		{
			get
			{
				return base.UnderlyingMethod.AdaptedMethodSymbol.ReturnValueMarshallingDescriptor;
			}
		}

		// Token: 0x170016EE RID: 5870
		// (get) Token: 0x060061EE RID: 25070 RVA: 0x00178810 File Offset: 0x00176A10
		protected override TypeMemberVisibility Visibility
		{
			get
			{
				return PEModuleBuilder.MemberVisibility(base.UnderlyingMethod.AdaptedMethodSymbol);
			}
		}

		// Token: 0x170016EF RID: 5871
		// (get) Token: 0x060061EF RID: 25071 RVA: 0x00178822 File Offset: 0x00176A22
		protected override string Name
		{
			get
			{
				return base.UnderlyingMethod.AdaptedMethodSymbol.MetadataName;
			}
		}

		// Token: 0x170016F0 RID: 5872
		// (get) Token: 0x060061F0 RID: 25072 RVA: 0x00178834 File Offset: 0x00176A34
		protected override bool AcceptsExtraArguments
		{
			get
			{
				return base.UnderlyingMethod.AdaptedMethodSymbol.IsVararg;
			}
		}

		// Token: 0x170016F1 RID: 5873
		// (get) Token: 0x060061F1 RID: 25073 RVA: 0x00178846 File Offset: 0x00176A46
		protected override ISignature UnderlyingMethodSignature
		{
			get
			{
				return base.UnderlyingMethod;
			}
		}

		// Token: 0x170016F2 RID: 5874
		// (get) Token: 0x060061F2 RID: 25074 RVA: 0x0017884E File Offset: 0x00176A4E
		protected override INamespace ContainingNamespace
		{
			get
			{
				return base.UnderlyingMethod.AdaptedMethodSymbol.ContainingNamespace.GetCciAdapter();
			}
		}
	}
}
