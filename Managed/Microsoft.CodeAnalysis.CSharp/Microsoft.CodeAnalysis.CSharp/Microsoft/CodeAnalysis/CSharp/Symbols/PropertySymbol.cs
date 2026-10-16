using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CodeGen;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel;
using Microsoft.CodeAnalysis.Emit;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004B5 RID: 1205
	internal abstract class PropertySymbol : Symbol, IPropertyDefinition, ISignature, ITypeDefinitionMember, ITypeMemberReference, IReference, INamedEntity, IDefinition
	{
		// Token: 0x06006648 RID: 26184 RVA: 0x00181BD7 File Offset: 0x0017FDD7
		IEnumerable<IMethodReference> IPropertyDefinition.GetAccessors(EmitContext context)
		{
			MethodSymbol getMethod = this.AdaptedPropertySymbol.GetMethod;
			MethodSymbol methodSymbol = ((getMethod != null) ? getMethod.GetCciAdapter() : null);
			if (methodSymbol != null && methodSymbol.ShouldInclude(context))
			{
				yield return methodSymbol;
			}
			MethodSymbol setMethod = this.AdaptedPropertySymbol.SetMethod;
			MethodSymbol methodSymbol2 = ((setMethod != null) ? setMethod.GetCciAdapter() : null);
			if (methodSymbol2 != null && methodSymbol2.ShouldInclude(context))
			{
				yield return methodSymbol2;
			}
			SourcePropertySymbolBase sourcePropertySymbolBase = this.AdaptedPropertySymbol as SourcePropertySymbolBase;
			if (sourcePropertySymbolBase != null && this.ShouldInclude(context))
			{
				SynthesizedSealedPropertyAccessor synthesizedSealedAccessorOpt = sourcePropertySymbolBase.SynthesizedSealedAccessorOpt;
				if (synthesizedSealedAccessorOpt != null)
				{
					yield return synthesizedSealedAccessorOpt.GetCciAdapter();
				}
			}
			yield break;
		}

		// Token: 0x1700193D RID: 6461
		// (get) Token: 0x06006649 RID: 26185 RVA: 0x00181BEE File Offset: 0x0017FDEE
		MetadataConstant IPropertyDefinition.DefaultValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700193E RID: 6462
		// (get) Token: 0x0600664A RID: 26186 RVA: 0x00181BF4 File Offset: 0x0017FDF4
		IMethodReference IPropertyDefinition.Getter
		{
			get
			{
				MethodSymbol getMethod = this.AdaptedPropertySymbol.GetMethod;
				if (getMethod == null && this.AdaptedPropertySymbol.IsSealed)
				{
					return this.GetSynthesizedSealedAccessor(MethodKind.PropertyGet);
				}
				if (getMethod == null)
				{
					return null;
				}
				return getMethod.GetCciAdapter();
			}
		}

		// Token: 0x1700193F RID: 6463
		// (get) Token: 0x0600664B RID: 26187 RVA: 0x00181C31 File Offset: 0x0017FE31
		bool IPropertyDefinition.HasDefaultValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001940 RID: 6464
		// (get) Token: 0x0600664C RID: 26188 RVA: 0x00181C34 File Offset: 0x0017FE34
		bool IPropertyDefinition.IsRuntimeSpecial
		{
			get
			{
				return this.AdaptedPropertySymbol.HasRuntimeSpecialName;
			}
		}

		// Token: 0x17001941 RID: 6465
		// (get) Token: 0x0600664D RID: 26189 RVA: 0x00181C41 File Offset: 0x0017FE41
		bool IPropertyDefinition.IsSpecialName
		{
			get
			{
				return this.AdaptedPropertySymbol.HasSpecialName;
			}
		}

		// Token: 0x17001942 RID: 6466
		// (get) Token: 0x0600664E RID: 26190 RVA: 0x00181C4E File Offset: 0x0017FE4E
		ImmutableArray<IParameterDefinition> IPropertyDefinition.Parameters
		{
			get
			{
				return StaticCast<IParameterDefinition>.From<ParameterSymbol>(this.AdaptedPropertySymbol.Parameters);
			}
		}

		// Token: 0x17001943 RID: 6467
		// (get) Token: 0x0600664F RID: 26191 RVA: 0x00181C60 File Offset: 0x0017FE60
		IMethodReference IPropertyDefinition.Setter
		{
			get
			{
				MethodSymbol setMethod = this.AdaptedPropertySymbol.SetMethod;
				if (setMethod == null && this.AdaptedPropertySymbol.IsSealed)
				{
					return this.GetSynthesizedSealedAccessor(MethodKind.PropertySet);
				}
				if (setMethod == null)
				{
					return null;
				}
				return setMethod.GetCciAdapter();
			}
		}

		// Token: 0x06006650 RID: 26192 RVA: 0x00181C9D File Offset: 0x0017FE9D
		[Conditional("DEBUG")]
		private void CheckDefinitionInvariantAllowEmbedded()
		{
		}

		// Token: 0x17001944 RID: 6468
		// (get) Token: 0x06006651 RID: 26193 RVA: 0x00181C9F File Offset: 0x0017FE9F
		CallingConvention ISignature.CallingConvention
		{
			get
			{
				return this.AdaptedPropertySymbol.CallingConvention;
			}
		}

		// Token: 0x17001945 RID: 6469
		// (get) Token: 0x06006652 RID: 26194 RVA: 0x00181CAC File Offset: 0x0017FEAC
		ushort ISignature.ParameterCount
		{
			get
			{
				return (ushort)this.AdaptedPropertySymbol.ParameterCount;
			}
		}

		// Token: 0x06006653 RID: 26195 RVA: 0x00181CBA File Offset: 0x0017FEBA
		ImmutableArray<IParameterTypeInformation> ISignature.GetParameters(EmitContext context)
		{
			return StaticCast<IParameterTypeInformation>.From<ParameterSymbol>(this.AdaptedPropertySymbol.Parameters);
		}

		// Token: 0x17001946 RID: 6470
		// (get) Token: 0x06006654 RID: 26196 RVA: 0x00181CCC File Offset: 0x0017FECC
		ImmutableArray<ICustomModifier> ISignature.ReturnValueCustomModifiers
		{
			get
			{
				return this.AdaptedPropertySymbol.TypeWithAnnotations.CustomModifiers.As<ICustomModifier>();
			}
		}

		// Token: 0x17001947 RID: 6471
		// (get) Token: 0x06006655 RID: 26197 RVA: 0x00181CF4 File Offset: 0x0017FEF4
		ImmutableArray<ICustomModifier> ISignature.RefCustomModifiers
		{
			get
			{
				return this.AdaptedPropertySymbol.RefCustomModifiers.As<ICustomModifier>();
			}
		}

		// Token: 0x17001948 RID: 6472
		// (get) Token: 0x06006656 RID: 26198 RVA: 0x00181D14 File Offset: 0x0017FF14
		bool ISignature.ReturnValueIsByRef
		{
			get
			{
				return this.AdaptedPropertySymbol.RefKind.IsManagedReference();
			}
		}

		// Token: 0x06006657 RID: 26199 RVA: 0x00181D26 File Offset: 0x0017FF26
		ITypeReference ISignature.GetType(EmitContext context)
		{
			return ((PEModuleBuilder)context.Module).Translate(this.AdaptedPropertySymbol.Type, (CSharpSyntaxNode)context.SyntaxNode, context.Diagnostics);
		}

		// Token: 0x17001949 RID: 6473
		// (get) Token: 0x06006658 RID: 26200 RVA: 0x00181D54 File Offset: 0x0017FF54
		ITypeDefinition ITypeDefinitionMember.ContainingTypeDefinition
		{
			get
			{
				return this.AdaptedPropertySymbol.ContainingType.GetCciAdapter();
			}
		}

		// Token: 0x1700194A RID: 6474
		// (get) Token: 0x06006659 RID: 26201 RVA: 0x00181D66 File Offset: 0x0017FF66
		TypeMemberVisibility ITypeDefinitionMember.Visibility
		{
			get
			{
				return PEModuleBuilder.MemberVisibility(this.AdaptedPropertySymbol);
			}
		}

		// Token: 0x0600665A RID: 26202 RVA: 0x00181D73 File Offset: 0x0017FF73
		ITypeReference ITypeMemberReference.GetContainingType(EmitContext context)
		{
			return this.AdaptedPropertySymbol.ContainingType.GetCciAdapter();
		}

		// Token: 0x0600665B RID: 26203 RVA: 0x00181D85 File Offset: 0x0017FF85
		void IReference.Dispatch(MetadataVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x0600665C RID: 26204 RVA: 0x00181D8E File Offset: 0x0017FF8E
		IDefinition IReference.AsDefinition(EmitContext context)
		{
			return this;
		}

		// Token: 0x1700194B RID: 6475
		// (get) Token: 0x0600665D RID: 26205 RVA: 0x00181D91 File Offset: 0x0017FF91
		string INamedEntity.Name
		{
			get
			{
				return this.AdaptedPropertySymbol.MetadataName;
			}
		}

		// Token: 0x0600665E RID: 26206 RVA: 0x00181DA0 File Offset: 0x0017FFA0
		private IMethodReference GetSynthesizedSealedAccessor(MethodKind targetMethodKind)
		{
			SourcePropertySymbolBase sourcePropertySymbolBase = this.AdaptedPropertySymbol as SourcePropertySymbolBase;
			if (sourcePropertySymbolBase == null)
			{
				return null;
			}
			SynthesizedSealedPropertyAccessor synthesizedSealedAccessorOpt = sourcePropertySymbolBase.SynthesizedSealedAccessorOpt;
			if (synthesizedSealedAccessorOpt == null || synthesizedSealedAccessorOpt.MethodKind != targetMethodKind)
			{
				return null;
			}
			return synthesizedSealedAccessorOpt.GetCciAdapter();
		}

		// Token: 0x1700194C RID: 6476
		// (get) Token: 0x0600665F RID: 26207 RVA: 0x00181DD9 File Offset: 0x0017FFD9
		internal PropertySymbol AdaptedPropertySymbol
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06006660 RID: 26208 RVA: 0x00181DDC File Offset: 0x0017FFDC
		internal new PropertySymbol GetCciAdapter()
		{
			return this;
		}

		// Token: 0x1700194D RID: 6477
		// (get) Token: 0x06006661 RID: 26209 RVA: 0x00181DDF File Offset: 0x0017FFDF
		internal virtual bool HasRuntimeSpecialName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006662 RID: 26210 RVA: 0x00181DE2 File Offset: 0x0017FFE2
		internal PropertySymbol()
		{
		}

		// Token: 0x1700194E RID: 6478
		// (get) Token: 0x06006663 RID: 26211 RVA: 0x00181DEA File Offset: 0x0017FFEA
		public new virtual PropertySymbol OriginalDefinition
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700194F RID: 6479
		// (get) Token: 0x06006664 RID: 26212 RVA: 0x00181DED File Offset: 0x0017FFED
		protected sealed override Symbol OriginalSymbolDefinition
		{
			get
			{
				return this.OriginalDefinition;
			}
		}

		// Token: 0x17001950 RID: 6480
		// (get) Token: 0x06006665 RID: 26213 RVA: 0x00181DF5 File Offset: 0x0017FFF5
		internal virtual ImmutableArray<string> NotNullMembers
		{
			get
			{
				return ImmutableArray<string>.Empty;
			}
		}

		// Token: 0x17001951 RID: 6481
		// (get) Token: 0x06006666 RID: 26214 RVA: 0x00181DFC File Offset: 0x0017FFFC
		internal virtual ImmutableArray<string> NotNullWhenTrueMembers
		{
			get
			{
				return ImmutableArray<string>.Empty;
			}
		}

		// Token: 0x17001952 RID: 6482
		// (get) Token: 0x06006667 RID: 26215 RVA: 0x00181E03 File Offset: 0x00180003
		internal virtual ImmutableArray<string> NotNullWhenFalseMembers
		{
			get
			{
				return ImmutableArray<string>.Empty;
			}
		}

		// Token: 0x17001953 RID: 6483
		// (get) Token: 0x06006668 RID: 26216 RVA: 0x00181E0A File Offset: 0x0018000A
		public bool ReturnsByRef
		{
			get
			{
				return this.RefKind == RefKind.Ref;
			}
		}

		// Token: 0x17001954 RID: 6484
		// (get) Token: 0x06006669 RID: 26217 RVA: 0x00181E15 File Offset: 0x00180015
		public bool ReturnsByRefReadonly
		{
			get
			{
				return this.RefKind == RefKind.In;
			}
		}

		// Token: 0x17001955 RID: 6485
		// (get) Token: 0x0600666A RID: 26218
		public abstract RefKind RefKind { get; }

		// Token: 0x17001956 RID: 6486
		// (get) Token: 0x0600666B RID: 26219
		public abstract TypeWithAnnotations TypeWithAnnotations { get; }

		// Token: 0x17001957 RID: 6487
		// (get) Token: 0x0600666C RID: 26220 RVA: 0x00181E20 File Offset: 0x00180020
		public TypeSymbol Type
		{
			get
			{
				return this.TypeWithAnnotations.Type;
			}
		}

		// Token: 0x17001958 RID: 6488
		// (get) Token: 0x0600666D RID: 26221
		public abstract ImmutableArray<CustomModifier> RefCustomModifiers { get; }

		// Token: 0x17001959 RID: 6489
		// (get) Token: 0x0600666E RID: 26222
		public abstract ImmutableArray<ParameterSymbol> Parameters { get; }

		// Token: 0x1700195A RID: 6490
		// (get) Token: 0x0600666F RID: 26223 RVA: 0x00181E3C File Offset: 0x0018003C
		internal int ParameterCount
		{
			get
			{
				return this.Parameters.Length;
			}
		}

		// Token: 0x1700195B RID: 6491
		// (get) Token: 0x06006670 RID: 26224 RVA: 0x00181E57 File Offset: 0x00180057
		internal ImmutableArray<TypeWithAnnotations> ParameterTypesWithAnnotations
		{
			get
			{
				ParameterSignature.PopulateParameterSignature(this.Parameters, ref this._lazyParameterSignature);
				return this._lazyParameterSignature.parameterTypesWithAnnotations;
			}
		}

		// Token: 0x1700195C RID: 6492
		// (get) Token: 0x06006671 RID: 26225 RVA: 0x00181E75 File Offset: 0x00180075
		internal ImmutableArray<RefKind> ParameterRefKinds
		{
			get
			{
				ParameterSignature.PopulateParameterSignature(this.Parameters, ref this._lazyParameterSignature);
				return this._lazyParameterSignature.parameterRefKinds;
			}
		}

		// Token: 0x1700195D RID: 6493
		// (get) Token: 0x06006672 RID: 26226 RVA: 0x00181E93 File Offset: 0x00180093
		public virtual bool RequiresInstanceReceiver
		{
			get
			{
				return !this.IsStatic;
			}
		}

		// Token: 0x1700195E RID: 6494
		// (get) Token: 0x06006673 RID: 26227
		public abstract bool IsIndexer { get; }

		// Token: 0x1700195F RID: 6495
		// (get) Token: 0x06006674 RID: 26228 RVA: 0x00181E9E File Offset: 0x0018009E
		public virtual bool IsIndexedProperty
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001960 RID: 6496
		// (get) Token: 0x06006675 RID: 26229 RVA: 0x00181EA1 File Offset: 0x001800A1
		public bool IsReadOnly
		{
			get
			{
				return ((PropertySymbol)this.GetLeastOverriddenMember(this.ContainingType)).SetMethod == null;
			}
		}

		// Token: 0x17001961 RID: 6497
		// (get) Token: 0x06006676 RID: 26230 RVA: 0x00181EBC File Offset: 0x001800BC
		public bool IsWriteOnly
		{
			get
			{
				return ((PropertySymbol)this.GetLeastOverriddenMember(this.ContainingType)).GetMethod == null;
			}
		}

		// Token: 0x17001962 RID: 6498
		// (get) Token: 0x06006677 RID: 26231 RVA: 0x00181ED7 File Offset: 0x001800D7
		internal virtual bool IsDirectlyExcludedFromCodeCoverage
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001963 RID: 6499
		// (get) Token: 0x06006678 RID: 26232
		internal abstract bool HasSpecialName { get; }

		// Token: 0x17001964 RID: 6500
		// (get) Token: 0x06006679 RID: 26233
		public abstract MethodSymbol GetMethod { get; }

		// Token: 0x17001965 RID: 6501
		// (get) Token: 0x0600667A RID: 26234
		public abstract MethodSymbol SetMethod { get; }

		// Token: 0x17001966 RID: 6502
		// (get) Token: 0x0600667B RID: 26235
		internal abstract CallingConvention CallingConvention { get; }

		// Token: 0x17001967 RID: 6503
		// (get) Token: 0x0600667C RID: 26236
		internal abstract bool MustCallMethodsDirectly { get; }

		// Token: 0x17001968 RID: 6504
		// (get) Token: 0x0600667D RID: 26237 RVA: 0x00181EDA File Offset: 0x001800DA
		public PropertySymbol OverriddenProperty
		{
			get
			{
				if (!this.IsOverride)
				{
					return null;
				}
				if (base.IsDefinition)
				{
					return (PropertySymbol)this.OverriddenOrHiddenMembers.GetOverriddenMember();
				}
				return (PropertySymbol)OverriddenOrHiddenMembersResult.GetOverriddenMember(this, this.OriginalDefinition.OverriddenProperty);
			}
		}

		// Token: 0x17001969 RID: 6505
		// (get) Token: 0x0600667E RID: 26238 RVA: 0x00181F15 File Offset: 0x00180115
		internal virtual OverriddenOrHiddenMembersResult OverriddenOrHiddenMembers
		{
			get
			{
				return this.MakeOverriddenOrHiddenMembers();
			}
		}

		// Token: 0x1700196A RID: 6506
		// (get) Token: 0x0600667F RID: 26239 RVA: 0x00181F20 File Offset: 0x00180120
		internal bool HidesBasePropertiesByName
		{
			get
			{
				MethodSymbol methodSymbol = this.GetMethod ?? this.SetMethod;
				return methodSymbol != null && methodSymbol.HidesBaseMethodsByName;
			}
		}

		// Token: 0x06006680 RID: 26240 RVA: 0x00181F4C File Offset: 0x0018014C
		internal PropertySymbol GetLeastOverriddenProperty(NamedTypeSymbol accessingTypeOpt)
		{
			accessingTypeOpt = ((accessingTypeOpt != null) ? accessingTypeOpt.OriginalDefinition : null);
			PropertySymbol propertySymbol = this;
			while (propertySymbol.IsOverride && !propertySymbol.HidesBasePropertiesByName)
			{
				PropertySymbol overriddenProperty = propertySymbol.OverriddenProperty;
				CompoundUseSiteInfo<AssemblySymbol> discarded = CompoundUseSiteInfo<AssemblySymbol>.Discarded;
				if (overriddenProperty == null || (accessingTypeOpt != null && !AccessCheck.IsSymbolAccessible(overriddenProperty, accessingTypeOpt, ref discarded, null)))
				{
					break;
				}
				propertySymbol = overriddenProperty;
			}
			return propertySymbol;
		}

		// Token: 0x1700196B RID: 6507
		// (get) Token: 0x06006681 RID: 26241 RVA: 0x00181F9D File Offset: 0x0018019D
		internal virtual bool IsExplicitInterfaceImplementation
		{
			get
			{
				return this.ExplicitInterfaceImplementations.Any<PropertySymbol>();
			}
		}

		// Token: 0x1700196C RID: 6508
		// (get) Token: 0x06006682 RID: 26242
		public abstract ImmutableArray<PropertySymbol> ExplicitInterfaceImplementations { get; }

		// Token: 0x1700196D RID: 6509
		// (get) Token: 0x06006683 RID: 26243 RVA: 0x00181FAA File Offset: 0x001801AA
		public sealed override SymbolKind Kind
		{
			get
			{
				return SymbolKind.Property;
			}
		}

		// Token: 0x06006684 RID: 26244 RVA: 0x00181FAE File Offset: 0x001801AE
		internal override TResult Accept<TArgument, TResult>(CSharpSymbolVisitor<TArgument, TResult> visitor, TArgument argument)
		{
			return visitor.VisitProperty(this, argument);
		}

		// Token: 0x06006685 RID: 26245 RVA: 0x00181FB8 File Offset: 0x001801B8
		public override void Accept(CSharpSymbolVisitor visitor)
		{
			visitor.VisitProperty(this);
		}

		// Token: 0x06006686 RID: 26246 RVA: 0x00181FC1 File Offset: 0x001801C1
		public override TResult Accept<TResult>(CSharpSymbolVisitor<TResult> visitor)
		{
			return visitor.VisitProperty(this);
		}

		// Token: 0x06006687 RID: 26247 RVA: 0x00181FCA File Offset: 0x001801CA
		internal PropertySymbol AsMember(NamedTypeSymbol newOwner)
		{
			if (!newOwner.IsDefinition)
			{
				return new SubstitutedPropertySymbol(newOwner as SubstitutedNamedTypeSymbol, this);
			}
			return this;
		}

		// Token: 0x06006688 RID: 26248 RVA: 0x00181FE2 File Offset: 0x001801E2
		internal override UseSiteInfo<AssemblySymbol> GetUseSiteInfo()
		{
			if (base.IsDefinition)
			{
				return new UseSiteInfo<AssemblySymbol>(base.PrimaryDependency);
			}
			return this.OriginalDefinition.GetUseSiteInfo();
		}

		// Token: 0x06006689 RID: 26249 RVA: 0x00182004 File Offset: 0x00180204
		internal bool CalculateUseSiteDiagnostic(ref UseSiteInfo<AssemblySymbol> result)
		{
			if (base.DeriveUseSiteInfoFromType(ref result, this.TypeWithAnnotations, Symbol.AllowedRequiredModifierType.None) || base.DeriveUseSiteInfoFromCustomModifiers(ref result, this.RefCustomModifiers, Symbol.AllowedRequiredModifierType.System_Runtime_InteropServices_InAttribute) || base.DeriveUseSiteInfoFromParameters(ref result, this.Parameters))
			{
				return true;
			}
			if (this.ContainingModule.HasUnifiedReferences)
			{
				HashSet<TypeSymbol> hashSet = null;
				DiagnosticInfo diagnosticInfo = result.DiagnosticInfo;
				if (this.TypeWithAnnotations.GetUnificationUseSiteDiagnosticRecursive(ref diagnosticInfo, this, ref hashSet) || Symbol.GetUnificationUseSiteDiagnosticRecursive(ref diagnosticInfo, this.RefCustomModifiers, this, ref hashSet) || Symbol.GetUnificationUseSiteDiagnosticRecursive(ref diagnosticInfo, this.Parameters, this, ref hashSet))
				{
					result = result.AdjustDiagnosticInfo(diagnosticInfo);
					return true;
				}
				result = result.AdjustDiagnosticInfo(diagnosticInfo);
			}
			return false;
		}

		// Token: 0x1700196E RID: 6510
		// (get) Token: 0x0600668A RID: 26250 RVA: 0x001820AE File Offset: 0x001802AE
		protected override int HighestPriorityUseSiteError
		{
			get
			{
				return 570;
			}
		}

		// Token: 0x1700196F RID: 6511
		// (get) Token: 0x0600668B RID: 26251 RVA: 0x001820B8 File Offset: 0x001802B8
		public sealed override bool HasUnsupportedMetadata
		{
			get
			{
				DiagnosticInfo diagnosticInfo = this.GetUseSiteInfo().DiagnosticInfo;
				return diagnosticInfo != null && diagnosticInfo.Code == 570;
			}
		}

		// Token: 0x0600668C RID: 26252 RVA: 0x001820E3 File Offset: 0x001802E3
		protected sealed override ISymbol CreateISymbol()
		{
			return new PropertySymbol(this);
		}

		// Token: 0x0600668D RID: 26253 RVA: 0x001820EC File Offset: 0x001802EC
		public override bool Equals(Symbol symbol, TypeCompareKind compareKind)
		{
			PropertySymbol propertySymbol = symbol as PropertySymbol;
			if (propertySymbol == null)
			{
				return false;
			}
			if (this == propertySymbol)
			{
				return true;
			}
			NativeIntegerPropertySymbol nativeIntegerPropertySymbol = propertySymbol as NativeIntegerPropertySymbol;
			if (nativeIntegerPropertySymbol != null)
			{
				return nativeIntegerPropertySymbol.Equals(this, compareKind);
			}
			return TypeSymbol.Equals(this.ContainingType, propertySymbol.ContainingType, compareKind) && this.OriginalDefinition == propertySymbol.OriginalDefinition;
		}

		// Token: 0x0600668E RID: 26254 RVA: 0x00182144 File Offset: 0x00180344
		public override int GetHashCode()
		{
			int num = 1;
			num = Hash.Combine<NamedTypeSymbol>(this.ContainingType, num);
			num = Hash.Combine<string>(this.Name, num);
			return Hash.Combine(num, this.ParameterCount);
		}

		// Token: 0x040019A2 RID: 6562
		private ParameterSignature _lazyParameterSignature;
	}
}
