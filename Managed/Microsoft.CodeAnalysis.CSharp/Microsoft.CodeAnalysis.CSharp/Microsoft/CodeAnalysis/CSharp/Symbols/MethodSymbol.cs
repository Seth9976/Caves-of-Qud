using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004B0 RID: 1200
	internal abstract class MethodSymbol : Symbol, ITypeMemberReference, IReference, INamedEntity, IMethodReference, ISignature, IGenericMethodInstanceReference, ISpecializedMethodReference, ITypeDefinitionMember, IDefinition, IMethodDefinition, IMethodSymbolInternal, ISymbolInternal
	{
		// Token: 0x170017FC RID: 6140
		// (get) Token: 0x06006417 RID: 25623 RVA: 0x0017DCAA File Offset: 0x0017BEAA
		IGenericMethodInstanceReference IMethodReference.AsGenericMethodInstanceReference
		{
			get
			{
				if (!this.AdaptedMethodSymbol.IsDefinition && this.AdaptedMethodSymbol.IsGenericMethod)
				{
					return this;
				}
				return null;
			}
		}

		// Token: 0x170017FD RID: 6141
		// (get) Token: 0x06006418 RID: 25624 RVA: 0x0017DCC9 File Offset: 0x0017BEC9
		ISpecializedMethodReference IMethodReference.AsSpecializedMethodReference
		{
			get
			{
				if (!this.AdaptedMethodSymbol.IsDefinition && (!this.AdaptedMethodSymbol.IsGenericMethod || PEModuleBuilder.IsGenericType(this.AdaptedMethodSymbol.ContainingType)))
				{
					return this;
				}
				return null;
			}
		}

		// Token: 0x06006419 RID: 25625 RVA: 0x0017DCFA File Offset: 0x0017BEFA
		IDefinition IReference.AsDefinition(EmitContext context)
		{
			return this.ResolvedMethodImpl(context);
		}

		// Token: 0x0600641A RID: 25626 RVA: 0x0017DD04 File Offset: 0x0017BF04
		ITypeReference ITypeMemberReference.GetContainingType(EmitContext context)
		{
			SynthesizedGlobalMethodSymbol synthesizedGlobalMethodSymbol = this.AdaptedMethodSymbol as SynthesizedGlobalMethodSymbol;
			if (synthesizedGlobalMethodSymbol != null)
			{
				return synthesizedGlobalMethodSymbol.ContainingPrivateImplementationDetailsType;
			}
			NamedTypeSymbol containingType = this.AdaptedMethodSymbol.ContainingType;
			return ((PEModuleBuilder)context.Module).Translate(containingType, (CSharpSyntaxNode)context.SyntaxNode, context.Diagnostics, false, this.AdaptedMethodSymbol.IsDefinition);
		}

		// Token: 0x0600641B RID: 25627 RVA: 0x0017DD64 File Offset: 0x0017BF64
		void IReference.Dispatch(MetadataVisitor visitor)
		{
			if (!this.AdaptedMethodSymbol.IsDefinition)
			{
				if (this.AdaptedMethodSymbol.IsGenericMethod)
				{
					visitor.Visit(this);
					return;
				}
				visitor.Visit(this);
				return;
			}
			else
			{
				PEModuleBuilder pemoduleBuilder = (PEModuleBuilder)visitor.Context.Module;
				if (this.AdaptedMethodSymbol.ContainingModule == pemoduleBuilder.SourceModule)
				{
					visitor.Visit(this);
					return;
				}
				visitor.Visit(this);
				return;
			}
		}

		// Token: 0x170017FE RID: 6142
		// (get) Token: 0x0600641C RID: 25628 RVA: 0x0017DDD3 File Offset: 0x0017BFD3
		string INamedEntity.Name
		{
			get
			{
				return this.AdaptedMethodSymbol.MetadataName;
			}
		}

		// Token: 0x170017FF RID: 6143
		// (get) Token: 0x0600641D RID: 25629 RVA: 0x0017DDE0 File Offset: 0x0017BFE0
		bool IMethodReference.AcceptsExtraArguments
		{
			get
			{
				return this.AdaptedMethodSymbol.IsVararg;
			}
		}

		// Token: 0x17001800 RID: 6144
		// (get) Token: 0x0600641E RID: 25630 RVA: 0x0017DDED File Offset: 0x0017BFED
		ushort IMethodReference.GenericParameterCount
		{
			get
			{
				return (ushort)this.AdaptedMethodSymbol.Arity;
			}
		}

		// Token: 0x17001801 RID: 6145
		// (get) Token: 0x0600641F RID: 25631 RVA: 0x0017DDFB File Offset: 0x0017BFFB
		bool IMethodReference.IsGeneric
		{
			get
			{
				return this.AdaptedMethodSymbol.IsGenericMethod;
			}
		}

		// Token: 0x17001802 RID: 6146
		// (get) Token: 0x06006420 RID: 25632 RVA: 0x0017DE08 File Offset: 0x0017C008
		ushort ISignature.ParameterCount
		{
			get
			{
				return (ushort)this.AdaptedMethodSymbol.ParameterCount;
			}
		}

		// Token: 0x06006421 RID: 25633 RVA: 0x0017DE16 File Offset: 0x0017C016
		IMethodDefinition IMethodReference.GetResolvedMethod(EmitContext context)
		{
			return this.ResolvedMethodImpl(context);
		}

		// Token: 0x06006422 RID: 25634 RVA: 0x0017DE20 File Offset: 0x0017C020
		private IMethodDefinition ResolvedMethodImpl(EmitContext context)
		{
			PEModuleBuilder pemoduleBuilder = (PEModuleBuilder)context.Module;
			if (this.AdaptedMethodSymbol.IsDefinition && this.AdaptedMethodSymbol.ContainingModule == pemoduleBuilder.SourceModule)
			{
				return this;
			}
			return null;
		}

		// Token: 0x17001803 RID: 6147
		// (get) Token: 0x06006423 RID: 25635 RVA: 0x0017DE61 File Offset: 0x0017C061
		ImmutableArray<IParameterTypeInformation> IMethodReference.ExtraParameters
		{
			get
			{
				return ImmutableArray<IParameterTypeInformation>.Empty;
			}
		}

		// Token: 0x17001804 RID: 6148
		// (get) Token: 0x06006424 RID: 25636 RVA: 0x0017DE68 File Offset: 0x0017C068
		CallingConvention ISignature.CallingConvention
		{
			get
			{
				return this.AdaptedMethodSymbol.CallingConvention;
			}
		}

		// Token: 0x06006425 RID: 25637 RVA: 0x0017DE78 File Offset: 0x0017C078
		ImmutableArray<IParameterTypeInformation> ISignature.GetParameters(EmitContext context)
		{
			PEModuleBuilder pemoduleBuilder = (PEModuleBuilder)context.Module;
			if (this.AdaptedMethodSymbol.IsDefinition && this.AdaptedMethodSymbol.ContainingModule == pemoduleBuilder.SourceModule)
			{
				return StaticCast<IParameterTypeInformation>.From<IParameterDefinition>(this.EnumerateDefinitionParameters());
			}
			return pemoduleBuilder.Translate(this.AdaptedMethodSymbol.Parameters);
		}

		// Token: 0x06006426 RID: 25638 RVA: 0x0017DED3 File Offset: 0x0017C0D3
		private ImmutableArray<IParameterDefinition> EnumerateDefinitionParameters()
		{
			return StaticCast<IParameterDefinition>.From<ParameterSymbol>(this.AdaptedMethodSymbol.Parameters);
		}

		// Token: 0x17001805 RID: 6149
		// (get) Token: 0x06006427 RID: 25639 RVA: 0x0017DEE8 File Offset: 0x0017C0E8
		ImmutableArray<ICustomModifier> ISignature.ReturnValueCustomModifiers
		{
			get
			{
				return ImmutableArray<ICustomModifier>.CastUp<CustomModifier>(this.AdaptedMethodSymbol.ReturnTypeWithAnnotations.CustomModifiers);
			}
		}

		// Token: 0x17001806 RID: 6150
		// (get) Token: 0x06006428 RID: 25640 RVA: 0x0017DF0D File Offset: 0x0017C10D
		ImmutableArray<ICustomModifier> ISignature.RefCustomModifiers
		{
			get
			{
				return ImmutableArray<ICustomModifier>.CastUp<CustomModifier>(this.AdaptedMethodSymbol.RefCustomModifiers);
			}
		}

		// Token: 0x17001807 RID: 6151
		// (get) Token: 0x06006429 RID: 25641 RVA: 0x0017DF1F File Offset: 0x0017C11F
		bool ISignature.ReturnValueIsByRef
		{
			get
			{
				return this.AdaptedMethodSymbol.RefKind.IsManagedReference();
			}
		}

		// Token: 0x0600642A RID: 25642 RVA: 0x0017DF31 File Offset: 0x0017C131
		ITypeReference ISignature.GetType(EmitContext context)
		{
			return ((PEModuleBuilder)context.Module).Translate(this.AdaptedMethodSymbol.ReturnType, (CSharpSyntaxNode)context.SyntaxNode, context.Diagnostics);
		}

		// Token: 0x0600642B RID: 25643 RVA: 0x0017DF5F File Offset: 0x0017C15F
		IEnumerable<ITypeReference> IGenericMethodInstanceReference.GetGenericArguments(EmitContext context)
		{
			PEModuleBuilder moduleBeingBuilt = (PEModuleBuilder)context.Module;
			foreach (TypeWithAnnotations typeWithAnnotations in this.AdaptedMethodSymbol.TypeArgumentsWithAnnotations)
			{
				yield return moduleBeingBuilt.Translate(typeWithAnnotations.Type, (CSharpSyntaxNode)context.SyntaxNode, context.Diagnostics);
			}
			ImmutableArray<TypeWithAnnotations>.Enumerator enumerator = default(ImmutableArray<TypeWithAnnotations>.Enumerator);
			yield break;
		}

		// Token: 0x0600642C RID: 25644 RVA: 0x0017DF78 File Offset: 0x0017C178
		IMethodReference IGenericMethodInstanceReference.GetGenericMethod(EmitContext context)
		{
			if (!PEModuleBuilder.IsGenericType(this.AdaptedMethodSymbol.ContainingType))
			{
				return ((PEModuleBuilder)context.Module).Translate(this.AdaptedMethodSymbol.OriginalDefinition, (CSharpSyntaxNode)context.SyntaxNode, context.Diagnostics, null, true);
			}
			return new SpecializedMethodReference(this.AdaptedMethodSymbol.ConstructedFrom);
		}

		// Token: 0x17001808 RID: 6152
		// (get) Token: 0x0600642D RID: 25645 RVA: 0x0017DFD6 File Offset: 0x0017C1D6
		IMethodReference ISpecializedMethodReference.UnspecializedVersion
		{
			get
			{
				return this.AdaptedMethodSymbol.OriginalDefinition.GetCciAdapter();
			}
		}

		// Token: 0x17001809 RID: 6153
		// (get) Token: 0x0600642E RID: 25646 RVA: 0x0017DFE8 File Offset: 0x0017C1E8
		ITypeDefinition ITypeDefinitionMember.ContainingTypeDefinition
		{
			get
			{
				SynthesizedGlobalMethodSymbol synthesizedGlobalMethodSymbol = this.AdaptedMethodSymbol as SynthesizedGlobalMethodSymbol;
				if (synthesizedGlobalMethodSymbol != null)
				{
					return synthesizedGlobalMethodSymbol.ContainingPrivateImplementationDetailsType;
				}
				return this.AdaptedMethodSymbol.ContainingType.GetCciAdapter();
			}
		}

		// Token: 0x1700180A RID: 6154
		// (get) Token: 0x0600642F RID: 25647 RVA: 0x0017E01B File Offset: 0x0017C21B
		TypeMemberVisibility ITypeDefinitionMember.Visibility
		{
			get
			{
				return PEModuleBuilder.MemberVisibility(this.AdaptedMethodSymbol);
			}
		}

		// Token: 0x06006430 RID: 25648 RVA: 0x0017E028 File Offset: 0x0017C228
		IMethodBody IMethodDefinition.GetBody(EmitContext context)
		{
			return ((PEModuleBuilder)context.Module).GetMethodBody(this.AdaptedMethodSymbol);
		}

		// Token: 0x1700180B RID: 6155
		// (get) Token: 0x06006431 RID: 25649 RVA: 0x0017E040 File Offset: 0x0017C240
		IEnumerable<IGenericMethodParameter> IMethodDefinition.GenericParameters
		{
			get
			{
				foreach (TypeParameterSymbol typeParameterSymbol in this.AdaptedMethodSymbol.TypeParameters)
				{
					yield return typeParameterSymbol.GetCciAdapter();
				}
				ImmutableArray<TypeParameterSymbol>.Enumerator enumerator = default(ImmutableArray<TypeParameterSymbol>.Enumerator);
				yield break;
			}
		}

		// Token: 0x1700180C RID: 6156
		// (get) Token: 0x06006432 RID: 25650 RVA: 0x0017E050 File Offset: 0x0017C250
		bool IMethodDefinition.HasDeclarativeSecurity
		{
			get
			{
				return this.AdaptedMethodSymbol.HasDeclarativeSecurity;
			}
		}

		// Token: 0x1700180D RID: 6157
		// (get) Token: 0x06006433 RID: 25651 RVA: 0x0017E05D File Offset: 0x0017C25D
		IEnumerable<SecurityAttribute> IMethodDefinition.SecurityAttributes
		{
			get
			{
				return this.AdaptedMethodSymbol.GetSecurityInformation();
			}
		}

		// Token: 0x1700180E RID: 6158
		// (get) Token: 0x06006434 RID: 25652 RVA: 0x0017E06A File Offset: 0x0017C26A
		bool IMethodDefinition.IsAbstract
		{
			get
			{
				return this.AdaptedMethodSymbol.IsAbstract;
			}
		}

		// Token: 0x1700180F RID: 6159
		// (get) Token: 0x06006435 RID: 25653 RVA: 0x0017E077 File Offset: 0x0017C277
		bool IMethodDefinition.IsAccessCheckedOnOverride
		{
			get
			{
				return this.AdaptedMethodSymbol.IsAccessCheckedOnOverride;
			}
		}

		// Token: 0x17001810 RID: 6160
		// (get) Token: 0x06006436 RID: 25654 RVA: 0x0017E084 File Offset: 0x0017C284
		bool IMethodDefinition.IsConstructor
		{
			get
			{
				return this.AdaptedMethodSymbol.MethodKind == MethodKind.Constructor;
			}
		}

		// Token: 0x17001811 RID: 6161
		// (get) Token: 0x06006437 RID: 25655 RVA: 0x0017E094 File Offset: 0x0017C294
		bool IMethodDefinition.IsExternal
		{
			get
			{
				return this.AdaptedMethodSymbol.IsExternal;
			}
		}

		// Token: 0x17001812 RID: 6162
		// (get) Token: 0x06006438 RID: 25656 RVA: 0x0017E0A1 File Offset: 0x0017C2A1
		bool IMethodDefinition.IsHiddenBySignature
		{
			get
			{
				return !this.AdaptedMethodSymbol.HidesBaseMethodsByName;
			}
		}

		// Token: 0x17001813 RID: 6163
		// (get) Token: 0x06006439 RID: 25657 RVA: 0x0017E0B1 File Offset: 0x0017C2B1
		bool IMethodDefinition.IsNewSlot
		{
			get
			{
				return this.AdaptedMethodSymbol.IsMetadataNewSlot(false);
			}
		}

		// Token: 0x17001814 RID: 6164
		// (get) Token: 0x0600643A RID: 25658 RVA: 0x0017E0BF File Offset: 0x0017C2BF
		bool IMethodDefinition.IsPlatformInvoke
		{
			get
			{
				return this.AdaptedMethodSymbol.GetDllImportData() != null;
			}
		}

		// Token: 0x17001815 RID: 6165
		// (get) Token: 0x0600643B RID: 25659 RVA: 0x0017E0CF File Offset: 0x0017C2CF
		IPlatformInvokeInformation IMethodDefinition.PlatformInvokeData
		{
			get
			{
				return this.AdaptedMethodSymbol.GetDllImportData();
			}
		}

		// Token: 0x0600643C RID: 25660 RVA: 0x0017E0DC File Offset: 0x0017C2DC
		MethodImplAttributes IMethodDefinition.GetImplementationAttributes(EmitContext context)
		{
			return this.AdaptedMethodSymbol.ImplementationAttributes;
		}

		// Token: 0x17001816 RID: 6166
		// (get) Token: 0x0600643D RID: 25661 RVA: 0x0017E0E9 File Offset: 0x0017C2E9
		bool IMethodDefinition.IsRuntimeSpecial
		{
			get
			{
				return this.AdaptedMethodSymbol.HasRuntimeSpecialName;
			}
		}

		// Token: 0x17001817 RID: 6167
		// (get) Token: 0x0600643E RID: 25662 RVA: 0x0017E0F6 File Offset: 0x0017C2F6
		bool IMethodDefinition.IsSealed
		{
			get
			{
				return this.AdaptedMethodSymbol.IsMetadataFinal;
			}
		}

		// Token: 0x17001818 RID: 6168
		// (get) Token: 0x0600643F RID: 25663 RVA: 0x0017E103 File Offset: 0x0017C303
		bool IMethodDefinition.IsSpecialName
		{
			get
			{
				return this.AdaptedMethodSymbol.HasSpecialName;
			}
		}

		// Token: 0x17001819 RID: 6169
		// (get) Token: 0x06006440 RID: 25664 RVA: 0x0017E110 File Offset: 0x0017C310
		bool IMethodDefinition.IsStatic
		{
			get
			{
				return this.AdaptedMethodSymbol.IsStatic;
			}
		}

		// Token: 0x1700181A RID: 6170
		// (get) Token: 0x06006441 RID: 25665 RVA: 0x0017E11D File Offset: 0x0017C31D
		bool IMethodDefinition.IsVirtual
		{
			get
			{
				return this.AdaptedMethodSymbol.IsMetadataVirtual(false);
			}
		}

		// Token: 0x1700181B RID: 6171
		// (get) Token: 0x06006442 RID: 25666 RVA: 0x0017E12B File Offset: 0x0017C32B
		ImmutableArray<IParameterDefinition> IMethodDefinition.Parameters
		{
			get
			{
				return this.EnumerateDefinitionParameters();
			}
		}

		// Token: 0x1700181C RID: 6172
		// (get) Token: 0x06006443 RID: 25667 RVA: 0x0017E133 File Offset: 0x0017C333
		bool IMethodDefinition.RequiresSecurityObject
		{
			get
			{
				return this.AdaptedMethodSymbol.RequiresSecurityObject;
			}
		}

		// Token: 0x06006444 RID: 25668 RVA: 0x0017E140 File Offset: 0x0017C340
		IEnumerable<ICustomAttribute> IMethodDefinition.GetReturnValueAttributes(EmitContext context)
		{
			ImmutableArray<CSharpAttributeData> returnTypeAttributes = this.AdaptedMethodSymbol.GetReturnTypeAttributes();
			ArrayBuilder<SynthesizedAttributeData> arrayBuilder = null;
			this.AdaptedMethodSymbol.AddSynthesizedReturnTypeAttributes((PEModuleBuilder)context.Module, ref arrayBuilder);
			return this.AdaptedMethodSymbol.GetCustomAttributesToEmit(returnTypeAttributes, arrayBuilder, true, false);
		}

		// Token: 0x1700181D RID: 6173
		// (get) Token: 0x06006445 RID: 25669 RVA: 0x0017E182 File Offset: 0x0017C382
		bool IMethodDefinition.ReturnValueIsMarshalledExplicitly
		{
			get
			{
				return this.AdaptedMethodSymbol.ReturnValueIsMarshalledExplicitly;
			}
		}

		// Token: 0x1700181E RID: 6174
		// (get) Token: 0x06006446 RID: 25670 RVA: 0x0017E18F File Offset: 0x0017C38F
		IMarshallingInformation IMethodDefinition.ReturnValueMarshallingInformation
		{
			get
			{
				return this.AdaptedMethodSymbol.ReturnValueMarshallingInformation;
			}
		}

		// Token: 0x1700181F RID: 6175
		// (get) Token: 0x06006447 RID: 25671 RVA: 0x0017E19C File Offset: 0x0017C39C
		ImmutableArray<byte> IMethodDefinition.ReturnValueMarshallingDescriptor
		{
			get
			{
				return this.AdaptedMethodSymbol.ReturnValueMarshallingDescriptor;
			}
		}

		// Token: 0x17001820 RID: 6176
		// (get) Token: 0x06006448 RID: 25672 RVA: 0x0017E1A9 File Offset: 0x0017C3A9
		INamespace IMethodDefinition.ContainingNamespace
		{
			get
			{
				return this.AdaptedMethodSymbol.ContainingNamespace.GetCciAdapter();
			}
		}

		// Token: 0x17001821 RID: 6177
		// (get) Token: 0x06006449 RID: 25673 RVA: 0x0017E1BB File Offset: 0x0017C3BB
		internal MethodSymbol AdaptedMethodSymbol
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600644A RID: 25674 RVA: 0x0017E1BE File Offset: 0x0017C3BE
		internal new MethodSymbol GetCciAdapter()
		{
			return this;
		}

		// Token: 0x17001822 RID: 6178
		// (get) Token: 0x0600644B RID: 25675 RVA: 0x0017E1C4 File Offset: 0x0017C3C4
		internal virtual bool IsAccessCheckedOnOverride
		{
			get
			{
				Accessibility declaredAccessibility = this.DeclaredAccessibility;
				return (declaredAccessibility == Accessibility.Private || declaredAccessibility == Accessibility.ProtectedAndInternal || declaredAccessibility == Accessibility.Internal) && this.IsMetadataVirtual(false) && !this.IsMetadataFinal;
			}
		}

		// Token: 0x17001823 RID: 6179
		// (get) Token: 0x0600644C RID: 25676 RVA: 0x0017E1F8 File Offset: 0x0017C3F8
		internal virtual bool IsExternal
		{
			get
			{
				return this.IsExtern || (this.ContainingType != null && this.ContainingType.TypeKind == TypeKind.Delegate);
			}
		}

		// Token: 0x0600644D RID: 25677
		internal abstract bool IsMetadataNewSlot(bool ignoreInterfaceImplementationChanges = false);

		// Token: 0x17001824 RID: 6180
		// (get) Token: 0x0600644E RID: 25678 RVA: 0x0017E21C File Offset: 0x0017C41C
		internal virtual bool HasRuntimeSpecialName
		{
			get
			{
				return this.MethodKind == MethodKind.Constructor || this.MethodKind == MethodKind.StaticConstructor;
			}
		}

		// Token: 0x17001825 RID: 6181
		// (get) Token: 0x0600644F RID: 25679 RVA: 0x0017E233 File Offset: 0x0017C433
		internal virtual bool IsMetadataFinal
		{
			get
			{
				return this.IsSealed || (this.IsMetadataVirtual(false) && (!this.IsVirtual && !this.IsOverride && !this.IsAbstract) && this.MethodKind != MethodKind.Destructor);
			}
		}

		// Token: 0x06006450 RID: 25680
		internal abstract bool IsMetadataVirtual(bool ignoreInterfaceImplementationChanges = false);

		// Token: 0x17001826 RID: 6182
		// (get) Token: 0x06006451 RID: 25681 RVA: 0x0017E270 File Offset: 0x0017C470
		internal virtual bool ReturnValueIsMarshalledExplicitly
		{
			get
			{
				return this.ReturnValueMarshallingInformation != null;
			}
		}

		// Token: 0x17001827 RID: 6183
		// (get) Token: 0x06006452 RID: 25682 RVA: 0x0017E27C File Offset: 0x0017C47C
		internal virtual ImmutableArray<byte> ReturnValueMarshallingDescriptor
		{
			get
			{
				return default(ImmutableArray<byte>);
			}
		}

		// Token: 0x17001828 RID: 6184
		// (get) Token: 0x06006454 RID: 25684 RVA: 0x0017E29A File Offset: 0x0017C49A
		public new virtual MethodSymbol OriginalDefinition
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17001829 RID: 6185
		// (get) Token: 0x06006455 RID: 25685 RVA: 0x0017E29D File Offset: 0x0017C49D
		protected sealed override Symbol OriginalSymbolDefinition
		{
			get
			{
				return this.OriginalDefinition;
			}
		}

		// Token: 0x1700182A RID: 6186
		// (get) Token: 0x06006456 RID: 25686
		public abstract MethodKind MethodKind { get; }

		// Token: 0x1700182B RID: 6187
		// (get) Token: 0x06006457 RID: 25687
		public abstract int Arity { get; }

		// Token: 0x1700182C RID: 6188
		// (get) Token: 0x06006458 RID: 25688 RVA: 0x0017E2A5 File Offset: 0x0017C4A5
		public virtual bool IsGenericMethod
		{
			get
			{
				return this.Arity != 0;
			}
		}

		// Token: 0x1700182D RID: 6189
		// (get) Token: 0x06006459 RID: 25689 RVA: 0x0017E2B0 File Offset: 0x0017C4B0
		public virtual bool RequiresInstanceReceiver
		{
			get
			{
				return !this.IsStatic;
			}
		}

		// Token: 0x1700182E RID: 6190
		// (get) Token: 0x0600645A RID: 25690 RVA: 0x0017E2BB File Offset: 0x0017C4BB
		internal virtual bool IsDirectlyExcludedFromCodeCoverage
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700182F RID: 6191
		// (get) Token: 0x0600645B RID: 25691 RVA: 0x0017E2BE File Offset: 0x0017C4BE
		internal virtual ImmutableArray<string> NotNullMembers
		{
			get
			{
				return ImmutableArray<string>.Empty;
			}
		}

		// Token: 0x17001830 RID: 6192
		// (get) Token: 0x0600645C RID: 25692 RVA: 0x0017E2C5 File Offset: 0x0017C4C5
		internal virtual ImmutableArray<string> NotNullWhenTrueMembers
		{
			get
			{
				return ImmutableArray<string>.Empty;
			}
		}

		// Token: 0x17001831 RID: 6193
		// (get) Token: 0x0600645D RID: 25693 RVA: 0x0017E2CC File Offset: 0x0017C4CC
		internal virtual ImmutableArray<string> NotNullWhenFalseMembers
		{
			get
			{
				return ImmutableArray<string>.Empty;
			}
		}

		// Token: 0x0600645E RID: 25694
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal abstract UnmanagedCallersOnlyAttributeData GetUnmanagedCallersOnlyAttributeData(bool forceComplete);

		// Token: 0x17001832 RID: 6194
		// (get) Token: 0x0600645F RID: 25695
		public abstract bool IsExtensionMethod { get; }

		// Token: 0x17001833 RID: 6195
		// (get) Token: 0x06006460 RID: 25696
		internal abstract bool HasSpecialName { get; }

		// Token: 0x17001834 RID: 6196
		// (get) Token: 0x06006461 RID: 25697
		internal abstract MethodImplAttributes ImplementationAttributes { get; }

		// Token: 0x17001835 RID: 6197
		// (get) Token: 0x06006462 RID: 25698
		internal abstract bool HasDeclarativeSecurity { get; }

		// Token: 0x06006463 RID: 25699
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public abstract DllImportData GetDllImportData();

		// Token: 0x06006464 RID: 25700
		internal abstract IEnumerable<SecurityAttribute> GetSecurityInformation();

		// Token: 0x17001836 RID: 6198
		// (get) Token: 0x06006465 RID: 25701
		internal abstract MarshalPseudoCustomAttributeData ReturnValueMarshallingInformation { get; }

		// Token: 0x17001837 RID: 6199
		// (get) Token: 0x06006466 RID: 25702
		internal abstract bool RequiresSecurityObject { get; }

		// Token: 0x17001838 RID: 6200
		// (get) Token: 0x06006467 RID: 25703
		public abstract bool HidesBaseMethodsByName { get; }

		// Token: 0x17001839 RID: 6201
		// (get) Token: 0x06006468 RID: 25704
		public abstract bool IsVararg { get; }

		// Token: 0x1700183A RID: 6202
		// (get) Token: 0x06006469 RID: 25705 RVA: 0x0017E2D3 File Offset: 0x0017C4D3
		public virtual bool IsCheckedBuiltin
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700183B RID: 6203
		// (get) Token: 0x0600646A RID: 25706
		public abstract bool ReturnsVoid { get; }

		// Token: 0x1700183C RID: 6204
		// (get) Token: 0x0600646B RID: 25707
		public abstract bool IsAsync { get; }

		// Token: 0x1700183D RID: 6205
		// (get) Token: 0x0600646C RID: 25708 RVA: 0x0017E2D6 File Offset: 0x0017C4D6
		public bool ReturnsByRef
		{
			get
			{
				return this.RefKind == RefKind.Ref;
			}
		}

		// Token: 0x1700183E RID: 6206
		// (get) Token: 0x0600646D RID: 25709 RVA: 0x0017E2E1 File Offset: 0x0017C4E1
		public bool ReturnsByRefReadonly
		{
			get
			{
				return this.RefKind == RefKind.In;
			}
		}

		// Token: 0x1700183F RID: 6207
		// (get) Token: 0x0600646E RID: 25710
		public abstract RefKind RefKind { get; }

		// Token: 0x17001840 RID: 6208
		// (get) Token: 0x0600646F RID: 25711
		public abstract TypeWithAnnotations ReturnTypeWithAnnotations { get; }

		// Token: 0x17001841 RID: 6209
		// (get) Token: 0x06006470 RID: 25712 RVA: 0x0017E2EC File Offset: 0x0017C4EC
		public TypeSymbol ReturnType
		{
			get
			{
				return this.ReturnTypeWithAnnotations.Type;
			}
		}

		// Token: 0x17001842 RID: 6210
		// (get) Token: 0x06006471 RID: 25713
		public abstract FlowAnalysisAnnotations ReturnTypeFlowAnalysisAnnotations { get; }

		// Token: 0x17001843 RID: 6211
		// (get) Token: 0x06006472 RID: 25714
		public abstract ImmutableHashSet<string> ReturnNotNullIfParameterNotNull { get; }

		// Token: 0x17001844 RID: 6212
		// (get) Token: 0x06006473 RID: 25715
		public abstract FlowAnalysisAnnotations FlowAnalysisAnnotations { get; }

		// Token: 0x17001845 RID: 6213
		// (get) Token: 0x06006474 RID: 25716
		public abstract ImmutableArray<TypeWithAnnotations> TypeArgumentsWithAnnotations { get; }

		// Token: 0x17001846 RID: 6214
		// (get) Token: 0x06006475 RID: 25717
		public abstract ImmutableArray<TypeParameterSymbol> TypeParameters { get; }

		// Token: 0x06006476 RID: 25718 RVA: 0x0017E307 File Offset: 0x0017C507
		internal ImmutableArray<TypeWithAnnotations> GetTypeParametersAsTypeArguments()
		{
			return TypeMap.TypeParametersAsTypeSymbolsWithAnnotations(this.TypeParameters);
		}

		// Token: 0x17001847 RID: 6215
		// (get) Token: 0x06006477 RID: 25719 RVA: 0x0017E314 File Offset: 0x0017C514
		internal ParameterSymbol ThisParameter
		{
			get
			{
				ParameterSymbol parameterSymbol;
				if (!this.TryGetThisParameter(out parameterSymbol))
				{
					throw ExceptionUtilities.Unreachable;
				}
				return parameterSymbol;
			}
		}

		// Token: 0x06006478 RID: 25720 RVA: 0x0017E332 File Offset: 0x0017C532
		internal virtual bool TryGetThisParameter(out ParameterSymbol thisParameter)
		{
			thisParameter = null;
			return false;
		}

		// Token: 0x17001848 RID: 6216
		// (get) Token: 0x06006479 RID: 25721 RVA: 0x0017E338 File Offset: 0x0017C538
		internal virtual int ParameterCount
		{
			get
			{
				return this.Parameters.Length;
			}
		}

		// Token: 0x17001849 RID: 6217
		// (get) Token: 0x0600647A RID: 25722
		public abstract ImmutableArray<ParameterSymbol> Parameters { get; }

		// Token: 0x1700184A RID: 6218
		// (get) Token: 0x0600647B RID: 25723 RVA: 0x0017E353 File Offset: 0x0017C553
		public virtual MethodSymbol ConstructedFrom
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700184B RID: 6219
		// (get) Token: 0x0600647C RID: 25724 RVA: 0x0017E356 File Offset: 0x0017C556
		internal virtual bool IsExplicitInterfaceImplementation
		{
			get
			{
				return this.ExplicitInterfaceImplementations.Any<MethodSymbol>();
			}
		}

		// Token: 0x1700184C RID: 6220
		// (get) Token: 0x0600647D RID: 25725
		internal abstract bool IsDeclaredReadOnly { get; }

		// Token: 0x1700184D RID: 6221
		// (get) Token: 0x0600647E RID: 25726
		internal abstract bool IsInitOnly { get; }

		// Token: 0x1700184E RID: 6222
		// (get) Token: 0x0600647F RID: 25727 RVA: 0x0017E363 File Offset: 0x0017C563
		internal virtual bool IsEffectivelyReadOnly
		{
			get
			{
				if (!this.IsDeclaredReadOnly)
				{
					NamedTypeSymbol containingType = this.ContainingType;
					if (containingType == null || !containingType.IsReadOnly)
					{
						return false;
					}
				}
				return this.IsValidReadOnlyTarget;
			}
		}

		// Token: 0x1700184F RID: 6223
		// (get) Token: 0x06006480 RID: 25728 RVA: 0x0017E389 File Offset: 0x0017C589
		protected bool IsValidReadOnlyTarget
		{
			get
			{
				return !this.IsStatic && this.ContainingType.IsStructType() && this.MethodKind != MethodKind.Constructor && !this.IsInitOnly;
			}
		}

		// Token: 0x17001850 RID: 6224
		// (get) Token: 0x06006481 RID: 25729
		public abstract ImmutableArray<MethodSymbol> ExplicitInterfaceImplementations { get; }

		// Token: 0x17001851 RID: 6225
		// (get) Token: 0x06006482 RID: 25730
		public abstract ImmutableArray<CustomModifier> RefCustomModifiers { get; }

		// Token: 0x06006483 RID: 25731 RVA: 0x0017E3B4 File Offset: 0x0017C5B4
		public virtual ImmutableArray<CSharpAttributeData> GetReturnTypeAttributes()
		{
			return ImmutableArray<CSharpAttributeData>.Empty;
		}

		// Token: 0x17001852 RID: 6226
		// (get) Token: 0x06006484 RID: 25732
		public abstract Symbol AssociatedSymbol { get; }

		// Token: 0x06006485 RID: 25733 RVA: 0x0017E3BB File Offset: 0x0017C5BB
		internal MethodSymbol GetLeastOverriddenMethod(NamedTypeSymbol accessingTypeOpt)
		{
			return this.GetLeastOverriddenMethodCore(accessingTypeOpt, false);
		}

		// Token: 0x06006486 RID: 25734 RVA: 0x0017E3C8 File Offset: 0x0017C5C8
		private MethodSymbol GetLeastOverriddenMethodCore(NamedTypeSymbol accessingTypeOpt, bool requireSameReturnType)
		{
			accessingTypeOpt = ((accessingTypeOpt != null) ? accessingTypeOpt.OriginalDefinition : null);
			MethodSymbol methodSymbol = this;
			while (methodSymbol.IsOverride && !methodSymbol.HidesBaseMethodsByName)
			{
				MethodSymbol overriddenMethod = methodSymbol.OverriddenMethod;
				CompoundUseSiteInfo<AssemblySymbol> discarded = CompoundUseSiteInfo<AssemblySymbol>.Discarded;
				if (overriddenMethod == null || (accessingTypeOpt != null && !AccessCheck.IsSymbolAccessible(overriddenMethod, accessingTypeOpt, ref discarded, null)) || (requireSameReturnType && !this.ReturnType.Equals(overriddenMethod.ReturnType, TypeCompareKind.AllIgnoreOptions)))
				{
					break;
				}
				methodSymbol = overriddenMethod;
			}
			return methodSymbol;
		}

		// Token: 0x06006487 RID: 25735 RVA: 0x0017E434 File Offset: 0x0017C634
		internal MethodSymbol GetConstructedLeastOverriddenMethod(NamedTypeSymbol accessingTypeOpt, bool requireSameReturnType)
		{
			MethodSymbol leastOverriddenMethodCore = this.ConstructedFrom.GetLeastOverriddenMethodCore(accessingTypeOpt, requireSameReturnType);
			if (!leastOverriddenMethodCore.IsGenericMethod)
			{
				return leastOverriddenMethodCore;
			}
			return leastOverriddenMethodCore.Construct(this.TypeArgumentsWithAnnotations);
		}

		// Token: 0x17001853 RID: 6227
		// (get) Token: 0x06006488 RID: 25736 RVA: 0x0017E468 File Offset: 0x0017C668
		public MethodSymbol OverriddenMethod
		{
			get
			{
				if (!this.IsOverride || this.ConstructedFrom != this)
				{
					return null;
				}
				if (base.IsDefinition)
				{
					return (MethodSymbol)this.OverriddenOrHiddenMembers.GetOverriddenMember();
				}
				return (MethodSymbol)OverriddenOrHiddenMembersResult.GetOverriddenMember(this, this.OriginalDefinition.OverriddenMethod);
			}
		}

		// Token: 0x06006489 RID: 25737 RVA: 0x0017E4B7 File Offset: 0x0017C6B7
		internal virtual bool CallsAreOmitted(SyntaxTree syntaxTree)
		{
			return syntaxTree != null && this.CallsAreConditionallyOmitted(syntaxTree);
		}

		// Token: 0x0600648A RID: 25738 RVA: 0x0017E4C8 File Offset: 0x0017C6C8
		private bool CallsAreConditionallyOmitted(SyntaxTree syntaxTree)
		{
			if (!this.IsConditional)
			{
				return false;
			}
			ImmutableArray<string> appliedConditionalSymbols = this.GetAppliedConditionalSymbols();
			if (syntaxTree.IsAnyPreprocessorSymbolDefined(appliedConditionalSymbols))
			{
				return false;
			}
			if (this.IsOverride)
			{
				MethodSymbol overriddenMethod = this.OverriddenMethod;
				if (overriddenMethod != null && overriddenMethod.IsConditional)
				{
					return overriddenMethod.CallsAreConditionallyOmitted(syntaxTree);
				}
			}
			return true;
		}

		// Token: 0x0600648B RID: 25739
		internal abstract ImmutableArray<string> GetAppliedConditionalSymbols();

		// Token: 0x17001854 RID: 6228
		// (get) Token: 0x0600648C RID: 25740 RVA: 0x0017E514 File Offset: 0x0017C714
		public bool IsConditional
		{
			get
			{
				if (this.GetAppliedConditionalSymbols().Any<string>())
				{
					return true;
				}
				if (this.IsOverride)
				{
					MethodSymbol overriddenMethod = this.OverriddenMethod;
					if (overriddenMethod != null)
					{
						return overriddenMethod.IsConditional;
					}
				}
				return false;
			}
		}

		// Token: 0x0600648D RID: 25741 RVA: 0x0017E54C File Offset: 0x0017C74C
		internal static bool CanOverrideOrHide(MethodKind kind)
		{
			switch (kind)
			{
			case MethodKind.AnonymousFunction:
			case MethodKind.Constructor:
			case MethodKind.Destructor:
			case MethodKind.ExplicitInterfaceImplementation:
			case MethodKind.ReducedExtension:
			case MethodKind.StaticConstructor:
				return false;
			case MethodKind.Conversion:
			case MethodKind.DelegateInvoke:
			case MethodKind.EventAdd:
			case MethodKind.EventRemove:
			case MethodKind.UserDefinedOperator:
			case MethodKind.Ordinary:
			case MethodKind.PropertyGet:
			case MethodKind.PropertySet:
			case MethodKind.LocalFunction:
				return true;
			}
			throw ExceptionUtilities.UnexpectedValue(kind);
		}

		// Token: 0x17001855 RID: 6229
		// (get) Token: 0x0600648E RID: 25742 RVA: 0x0017E5B8 File Offset: 0x0017C7B8
		internal virtual OverriddenOrHiddenMembersResult OverriddenOrHiddenMembers
		{
			get
			{
				return this.MakeOverriddenOrHiddenMembers();
			}
		}

		// Token: 0x17001856 RID: 6230
		// (get) Token: 0x0600648F RID: 25743 RVA: 0x0017E5C0 File Offset: 0x0017C7C0
		public sealed override SymbolKind Kind
		{
			get
			{
				return SymbolKind.Method;
			}
		}

		// Token: 0x17001857 RID: 6231
		// (get) Token: 0x06006490 RID: 25744 RVA: 0x0017E5C4 File Offset: 0x0017C7C4
		internal bool IsScriptConstructor
		{
			get
			{
				return this.MethodKind == MethodKind.Constructor && this.ContainingType.IsScriptClass;
			}
		}

		// Token: 0x17001858 RID: 6232
		// (get) Token: 0x06006491 RID: 25745 RVA: 0x0017E5DC File Offset: 0x0017C7DC
		internal virtual bool IsScriptInitializer
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001859 RID: 6233
		// (get) Token: 0x06006492 RID: 25746 RVA: 0x0017E5DF File Offset: 0x0017C7DF
		internal bool IsImplicitConstructor
		{
			get
			{
				return (this.MethodKind == MethodKind.Constructor || this.MethodKind == MethodKind.StaticConstructor) && this.IsImplicitlyDeclared;
			}
		}

		// Token: 0x1700185A RID: 6234
		// (get) Token: 0x06006493 RID: 25747 RVA: 0x0017E5FC File Offset: 0x0017C7FC
		internal bool IsImplicitInstanceConstructor
		{
			get
			{
				return this.MethodKind == MethodKind.Constructor && this.IsImplicitlyDeclared;
			}
		}

		// Token: 0x1700185B RID: 6235
		// (get) Token: 0x06006494 RID: 25748 RVA: 0x0017E60F File Offset: 0x0017C80F
		internal bool IsSubmissionConstructor
		{
			get
			{
				return this.IsScriptConstructor && this.ContainingAssembly.IsInteractive;
			}
		}

		// Token: 0x1700185C RID: 6236
		// (get) Token: 0x06006495 RID: 25749 RVA: 0x0017E626 File Offset: 0x0017C826
		internal bool IsSubmissionInitializer
		{
			get
			{
				return this.IsScriptInitializer && this.ContainingAssembly.IsInteractive;
			}
		}

		// Token: 0x1700185D RID: 6237
		// (get) Token: 0x06006496 RID: 25750 RVA: 0x0017E63D File Offset: 0x0017C83D
		internal bool IsEntryPointCandidate
		{
			get
			{
				return (!this.IsPartialDefinition() || this.PartialImplementationPart != null) && this.IsStatic && this.Name == "Main";
			}
		}

		// Token: 0x06006497 RID: 25751 RVA: 0x0017E66B File Offset: 0x0017C86B
		internal override TResult Accept<TArgument, TResult>(CSharpSymbolVisitor<TArgument, TResult> visitor, TArgument argument)
		{
			return visitor.VisitMethod(this, argument);
		}

		// Token: 0x06006498 RID: 25752 RVA: 0x0017E675 File Offset: 0x0017C875
		public override void Accept(CSharpSymbolVisitor visitor)
		{
			visitor.VisitMethod(this);
		}

		// Token: 0x06006499 RID: 25753 RVA: 0x0017E67E File Offset: 0x0017C87E
		public override TResult Accept<TResult>(CSharpSymbolVisitor<TResult> visitor)
		{
			return visitor.VisitMethod(this);
		}

		// Token: 0x0600649A RID: 25754 RVA: 0x0017E687 File Offset: 0x0017C887
		public MethodSymbol ReduceExtensionMethod(TypeSymbol receiverType, CSharpCompilation compilation)
		{
			if (receiverType == null)
			{
				throw new ArgumentNullException("receiverType");
			}
			if (!this.IsExtensionMethod || this.MethodKind == MethodKind.ReducedExtension)
			{
				return null;
			}
			return ReducedExtensionMethodSymbol.Create(this, receiverType, compilation);
		}

		// Token: 0x0600649B RID: 25755 RVA: 0x0017E6B3 File Offset: 0x0017C8B3
		public MethodSymbol ReduceExtensionMethod()
		{
			if (!this.IsExtensionMethod || this.MethodKind == MethodKind.ReducedExtension)
			{
				return null;
			}
			return ReducedExtensionMethodSymbol.Create(this);
		}

		// Token: 0x1700185E RID: 6238
		// (get) Token: 0x0600649C RID: 25756 RVA: 0x0017E6CF File Offset: 0x0017C8CF
		internal virtual MethodSymbol CallsiteReducedFromMethod
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700185F RID: 6239
		// (get) Token: 0x0600649D RID: 25757 RVA: 0x0017E6D2 File Offset: 0x0017C8D2
		public virtual MethodSymbol PartialImplementationPart
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001860 RID: 6240
		// (get) Token: 0x0600649E RID: 25758 RVA: 0x0017E6D5 File Offset: 0x0017C8D5
		public virtual MethodSymbol PartialDefinitionPart
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001861 RID: 6241
		// (get) Token: 0x0600649F RID: 25759 RVA: 0x0017E6D8 File Offset: 0x0017C8D8
		public virtual MethodSymbol ReducedFrom
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001862 RID: 6242
		// (get) Token: 0x060064A0 RID: 25760 RVA: 0x0017E6DB File Offset: 0x0017C8DB
		public virtual TypeSymbol ReceiverType
		{
			get
			{
				return this.ContainingType;
			}
		}

		// Token: 0x060064A1 RID: 25761 RVA: 0x0017E6E3 File Offset: 0x0017C8E3
		public virtual TypeSymbol GetTypeInferredDuringReduction(TypeParameterSymbol reducedFromTypeParameter)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060064A2 RID: 25762 RVA: 0x0017E6EA File Offset: 0x0017C8EA
		public MethodSymbol Construct(params TypeSymbol[] typeArguments)
		{
			return this.Construct(ImmutableArray.Create<TypeSymbol>(typeArguments));
		}

		// Token: 0x060064A3 RID: 25763 RVA: 0x0017E6F8 File Offset: 0x0017C8F8
		public MethodSymbol Construct(ImmutableArray<TypeSymbol> typeArguments)
		{
			return this.Construct(typeArguments.SelectAsArray<TypeSymbol, TypeWithAnnotations>((TypeSymbol a) => TypeWithAnnotations.Create(a, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>))));
		}

		// Token: 0x060064A4 RID: 25764 RVA: 0x0017E728 File Offset: 0x0017C928
		internal MethodSymbol Construct(ImmutableArray<TypeWithAnnotations> typeArguments)
		{
			if (this != this.ConstructedFrom || this.Arity == 0)
			{
				throw new InvalidOperationException();
			}
			if (typeArguments.IsDefault)
			{
				throw new ArgumentNullException("typeArguments");
			}
			if (typeArguments.Any<TypeWithAnnotations>(NamedTypeSymbol.TypeWithAnnotationsIsNullFunction))
			{
				throw new ArgumentException(CSharpResources.TypeArgumentCannotBeNull, "typeArguments");
			}
			if (typeArguments.Length != this.Arity)
			{
				throw new ArgumentException(CSharpResources.WrongNumberOfTypeArguments, "typeArguments");
			}
			if (ConstructedNamedTypeSymbol.TypeParametersMatchTypeArguments(this.TypeParameters, typeArguments))
			{
				return this;
			}
			return new ConstructedMethodSymbol(this, typeArguments);
		}

		// Token: 0x060064A5 RID: 25765 RVA: 0x0017E7B3 File Offset: 0x0017C9B3
		internal MethodSymbol AsMember(NamedTypeSymbol newOwner)
		{
			if (!newOwner.IsDefinition)
			{
				return new SubstitutedMethodSymbol(newOwner, this);
			}
			return this;
		}

		// Token: 0x17001863 RID: 6243
		// (get) Token: 0x060064A6 RID: 25766 RVA: 0x0017E7C6 File Offset: 0x0017C9C6
		internal ImmutableArray<TypeWithAnnotations> ParameterTypesWithAnnotations
		{
			get
			{
				ParameterSignature.PopulateParameterSignature(this.Parameters, ref this._lazyParameterSignature);
				return this._lazyParameterSignature.parameterTypesWithAnnotations;
			}
		}

		// Token: 0x060064A7 RID: 25767 RVA: 0x0017E7E4 File Offset: 0x0017C9E4
		internal TypeSymbol GetParameterType(int index)
		{
			return this.ParameterTypesWithAnnotations[index].Type;
		}

		// Token: 0x17001864 RID: 6244
		// (get) Token: 0x060064A8 RID: 25768 RVA: 0x0017E808 File Offset: 0x0017CA08
		internal ImmutableArray<RefKind> ParameterRefKinds
		{
			get
			{
				ParameterSignature.PopulateParameterSignature(this.Parameters, ref this._lazyParameterSignature);
				return this._lazyParameterSignature.parameterRefKinds;
			}
		}

		// Token: 0x17001865 RID: 6245
		// (get) Token: 0x060064A9 RID: 25769
		internal abstract CallingConvention CallingConvention { get; }

		// Token: 0x17001866 RID: 6246
		// (get) Token: 0x060064AA RID: 25770 RVA: 0x0017E826 File Offset: 0x0017CA26
		internal virtual ImmutableArray<NamedTypeSymbol> UnmanagedCallingConventionTypes
		{
			get
			{
				return ImmutableArray<NamedTypeSymbol>.Empty;
			}
		}

		// Token: 0x17001867 RID: 6247
		// (get) Token: 0x060064AB RID: 25771 RVA: 0x0017E82D File Offset: 0x0017CA2D
		internal virtual TypeMap TypeSubstitution
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060064AC RID: 25772 RVA: 0x0017E830 File Offset: 0x0017CA30
		internal override UseSiteInfo<AssemblySymbol> GetUseSiteInfo()
		{
			if (base.IsDefinition)
			{
				return new UseSiteInfo<AssemblySymbol>(base.PrimaryDependency);
			}
			return this.OriginalDefinition.GetUseSiteInfo();
		}

		// Token: 0x060064AD RID: 25773 RVA: 0x0017E854 File Offset: 0x0017CA54
		internal bool CalculateUseSiteDiagnostic(ref UseSiteInfo<AssemblySymbol> result)
		{
			if (base.DeriveUseSiteInfoFromType(ref result, this.ReturnTypeWithAnnotations, this.IsInitOnly ? Symbol.AllowedRequiredModifierType.System_Runtime_CompilerServices_IsExternalInit : Symbol.AllowedRequiredModifierType.None) || base.DeriveUseSiteInfoFromCustomModifiers(ref result, this.RefCustomModifiers, Symbol.AllowedRequiredModifierType.System_Runtime_InteropServices_InAttribute) || base.DeriveUseSiteInfoFromParameters(ref result, this.Parameters))
			{
				return true;
			}
			ModuleSymbol containingModule = this.ContainingModule;
			if (containingModule != null && containingModule.HasUnifiedReferences)
			{
				HashSet<TypeSymbol> hashSet = null;
				DiagnosticInfo diagnosticInfo = result.DiagnosticInfo;
				if (this.ReturnTypeWithAnnotations.GetUnificationUseSiteDiagnosticRecursive(ref diagnosticInfo, this, ref hashSet) || Symbol.GetUnificationUseSiteDiagnosticRecursive(ref diagnosticInfo, this.RefCustomModifiers, this, ref hashSet) || Symbol.GetUnificationUseSiteDiagnosticRecursive(ref diagnosticInfo, this.Parameters, this, ref hashSet) || Symbol.GetUnificationUseSiteDiagnosticRecursive(ref diagnosticInfo, this.TypeParameters, this, ref hashSet))
				{
					result = result.AdjustDiagnosticInfo(diagnosticInfo);
					return true;
				}
				result = result.AdjustDiagnosticInfo(diagnosticInfo);
			}
			return false;
		}

		// Token: 0x060064AE RID: 25774 RVA: 0x0017E924 File Offset: 0x0017CB24
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: TupleElementNames(new string[] { "IsCallConvs", "CallConvs" })]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2, 1 })]
		internal static ValueTuple<bool, ImmutableHashSet<INamedTypeSymbolInternal>> TryDecodeUnmanagedCallersOnlyCallConvsField([global::System.Runtime.CompilerServices.Nullable(1)] string key, TypedConstant value, bool isField, Location location, BindingDiagnosticBag diagnostics)
		{
			ImmutableHashSet<INamedTypeSymbolInternal> immutableHashSet = null;
			if (!UnmanagedCallersOnlyAttributeData.IsCallConvsTypedConstant(key, isField, in value))
			{
				return new ValueTuple<bool, ImmutableHashSet<INamedTypeSymbolInternal>>(false, immutableHashSet);
			}
			if (value.Values.IsDefaultOrEmpty)
			{
				immutableHashSet = ImmutableHashSet<INamedTypeSymbolInternal>.Empty;
				return new ValueTuple<bool, ImmutableHashSet<INamedTypeSymbolInternal>>(true, immutableHashSet);
			}
			PooledHashSet<INamedTypeSymbolInternal> instance = PooledHashSet<INamedTypeSymbolInternal>.GetInstance();
			foreach (TypedConstant typedConstant in value.Values)
			{
				NamedTypeSymbol namedTypeSymbol = typedConstant.ValueInternal as NamedTypeSymbol;
				if (namedTypeSymbol == null || !FunctionPointerTypeSymbol.IsCallingConventionModifier(namedTypeSymbol))
				{
					if (diagnostics != null)
					{
						diagnostics.Add(ErrorCode.ERR_InvalidUnmanagedCallersOnlyCallConv, location, new object[] { typedConstant.ValueInternal ?? "null" });
					}
				}
				else
				{
					instance.Add(namedTypeSymbol);
				}
			}
			immutableHashSet = instance.ToImmutableHashSet<INamedTypeSymbolInternal>();
			instance.Free();
			return new ValueTuple<bool, ImmutableHashSet<INamedTypeSymbolInternal>>(true, immutableHashSet);
		}

		// Token: 0x060064AF RID: 25775 RVA: 0x0017E9F4 File Offset: 0x0017CBF4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal bool CheckAndReportValidUnmanagedCallersOnlyTarget(Location location, BindingDiagnosticBag diagnostics)
		{
			if (this.IsStatic)
			{
				MethodKind methodKind = this.MethodKind;
				if (methodKind == MethodKind.Ordinary || methodKind == MethodKind.LocalFunction)
				{
					if (MethodSymbol.<CheckAndReportValidUnmanagedCallersOnlyTarget>g__isGenericMethod|262_0(this) || this.ContainingType.IsGenericType)
					{
						if (diagnostics != null)
						{
							diagnostics.Add(ErrorCode.ERR_UnmanagedCallersOnlyMethodOrTypeCannotBeGeneric, location);
						}
						return true;
					}
					return false;
				}
			}
			if (diagnostics != null)
			{
				diagnostics.Add(ErrorCode.ERR_UnmanagedCallersOnlyRequiresStatic, location);
			}
			return true;
		}

		// Token: 0x17001868 RID: 6248
		// (get) Token: 0x060064B0 RID: 25776 RVA: 0x0017EA54 File Offset: 0x0017CC54
		protected override int HighestPriorityUseSiteError
		{
			get
			{
				return 570;
			}
		}

		// Token: 0x17001869 RID: 6249
		// (get) Token: 0x060064B1 RID: 25777 RVA: 0x0017EA5C File Offset: 0x0017CC5C
		public sealed override bool HasUnsupportedMetadata
		{
			get
			{
				DiagnosticInfo diagnosticInfo = this.GetUseSiteInfo().DiagnosticInfo;
				return diagnosticInfo != null && diagnosticInfo.Code == 570;
			}
		}

		// Token: 0x1700186A RID: 6250
		// (get) Token: 0x060064B2 RID: 25778 RVA: 0x0017EA87 File Offset: 0x0017CC87
		internal virtual bool IsIterator
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700186B RID: 6251
		// (get) Token: 0x060064B3 RID: 25779 RVA: 0x0017EA8C File Offset: 0x0017CC8C
		// (set) Token: 0x060064B4 RID: 25780 RVA: 0x0017EAA2 File Offset: 0x0017CCA2
		internal virtual TypeWithAnnotations IteratorElementTypeWithAnnotations
		{
			get
			{
				return default(TypeWithAnnotations);
			}
			set
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x060064B5 RID: 25781 RVA: 0x0017EAA9 File Offset: 0x0017CCA9
		internal virtual void GenerateMethodBody(TypeCompilationState compilationState, BindingDiagnosticBag diagnostics)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x1700186C RID: 6252
		// (get) Token: 0x060064B6 RID: 25782 RVA: 0x0017EAB0 File Offset: 0x0017CCB0
		internal virtual bool SynthesizesLoweredBoundBody
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700186D RID: 6253
		// (get) Token: 0x060064B7 RID: 25783
		internal abstract bool GenerateDebugInfo { get; }

		// Token: 0x060064B8 RID: 25784
		internal abstract int CalculateLocalSyntaxOffset(int localPosition, SyntaxTree localTree);

		// Token: 0x1700186E RID: 6254
		// (get) Token: 0x060064B9 RID: 25785 RVA: 0x0017EAB3 File Offset: 0x0017CCB3
		internal virtual NullableAnnotation ReceiverNullableAnnotation
		{
			get
			{
				if (!this.RequiresInstanceReceiver)
				{
					return NullableAnnotation.None;
				}
				return NullableAnnotation.NotAnnotated;
			}
		}

		// Token: 0x060064BA RID: 25786 RVA: 0x0017EAC0 File Offset: 0x0017CCC0
		internal virtual void AddSynthesizedReturnTypeAttributes(PEModuleBuilder moduleBuilder, ref ArrayBuilder<SynthesizedAttributeData> attributes)
		{
			if (this.ReturnsByRefReadonly)
			{
				Symbol.AddSynthesizedAttribute(ref attributes, moduleBuilder.SynthesizeIsReadOnlyAttribute(this));
			}
			CSharpCompilation declaringCompilation = this.DeclaringCompilation;
			TypeWithAnnotations returnTypeWithAnnotations = this.ReturnTypeWithAnnotations;
			if (returnTypeWithAnnotations.Type.ContainsDynamic() && declaringCompilation.HasDynamicEmitAttributes(BindingDiagnosticBag.Discarded, Location.None))
			{
				Symbol.AddSynthesizedAttribute(ref attributes, declaringCompilation.SynthesizeDynamicAttribute(returnTypeWithAnnotations.Type, returnTypeWithAnnotations.CustomModifiers.Length + this.RefCustomModifiers.Length, this.RefKind));
			}
			if (returnTypeWithAnnotations.Type.ContainsNativeInteger())
			{
				Symbol.AddSynthesizedAttribute(ref attributes, moduleBuilder.SynthesizeNativeIntegerAttribute(this, returnTypeWithAnnotations.Type));
			}
			if (returnTypeWithAnnotations.Type.ContainsTupleNames() && declaringCompilation.HasTupleNamesAttributes(BindingDiagnosticBag.Discarded, Location.None))
			{
				Symbol.AddSynthesizedAttribute(ref attributes, declaringCompilation.SynthesizeTupleNamesAttribute(returnTypeWithAnnotations.Type));
			}
			if (declaringCompilation.ShouldEmitNullableAttributes(this))
			{
				Symbol.AddSynthesizedAttribute(ref attributes, moduleBuilder.SynthesizeNullableAttributeIfNecessary(this, this.GetNullableContextValue(), returnTypeWithAnnotations));
			}
		}

		// Token: 0x1700186F RID: 6255
		// (get) Token: 0x060064BB RID: 25787
		public abstract bool AreLocalsZeroed { get; }

		// Token: 0x060064BC RID: 25788
		internal abstract bool IsNullableAnalysisEnabled();

		// Token: 0x17001870 RID: 6256
		// (get) Token: 0x060064BD RID: 25789 RVA: 0x0017EBB9 File Offset: 0x0017CDB9
		bool IMethodSymbolInternal.IsIterator
		{
			get
			{
				return this.IsIterator;
			}
		}

		// Token: 0x060064BE RID: 25790 RVA: 0x0017EBC1 File Offset: 0x0017CDC1
		int IMethodSymbolInternal.CalculateLocalSyntaxOffset(int localPosition, SyntaxTree localTree)
		{
			return this.CalculateLocalSyntaxOffset(localPosition, localTree);
		}

		// Token: 0x060064BF RID: 25791 RVA: 0x0017EBCB File Offset: 0x0017CDCB
		IMethodSymbolInternal IMethodSymbolInternal.Construct(params ITypeSymbolInternal[] typeArguments)
		{
			return this.Construct((TypeSymbol[])typeArguments);
		}

		// Token: 0x060064C0 RID: 25792 RVA: 0x0017EBD9 File Offset: 0x0017CDD9
		protected sealed override ISymbol CreateISymbol()
		{
			return new MethodSymbol(this);
		}

		// Token: 0x060064C1 RID: 25793 RVA: 0x0017EBE4 File Offset: 0x0017CDE4
		public override bool Equals(Symbol other, TypeCompareKind compareKind)
		{
			SubstitutedMethodSymbol substitutedMethodSymbol = other as SubstitutedMethodSymbol;
			if (substitutedMethodSymbol != null)
			{
				return substitutedMethodSymbol.Equals(this, compareKind);
			}
			NativeIntegerMethodSymbol nativeIntegerMethodSymbol = other as NativeIntegerMethodSymbol;
			if (nativeIntegerMethodSymbol != null)
			{
				return nativeIntegerMethodSymbol.Equals(this, compareKind);
			}
			return base.Equals(other, compareKind);
		}

		// Token: 0x060064C2 RID: 25794 RVA: 0x0017EC1F File Offset: 0x0017CE1F
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060064C3 RID: 25795 RVA: 0x0017EC27 File Offset: 0x0017CE27
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[CompilerGenerated]
		internal static bool <CheckAndReportValidUnmanagedCallersOnlyTarget>g__isGenericMethod|262_0([DisallowNull] MethodSymbol method)
		{
			while (!method.IsGenericMethod)
			{
				method = method.ContainingSymbol as MethodSymbol;
				if (method == null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04001990 RID: 6544
		internal const MethodSymbol None = null;

		// Token: 0x04001991 RID: 6545
		private ParameterSignature _lazyParameterSignature;
	}
}
