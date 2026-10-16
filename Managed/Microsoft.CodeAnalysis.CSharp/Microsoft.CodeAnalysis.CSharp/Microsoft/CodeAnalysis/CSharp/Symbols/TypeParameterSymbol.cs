using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection.Metadata;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004B8 RID: 1208
	internal abstract class TypeParameterSymbol : TypeSymbol, IGenericParameterReference, ITypeReference, IReference, INamedEntity, IParameterListEntry, IGenericMethodParameterReference, IGenericTypeParameterReference, IGenericParameter, IGenericMethodParameter, IGenericTypeParameter, ITypeParameterSymbolInternal, ITypeSymbolInternal, INamespaceOrTypeSymbolInternal, ISymbolInternal
	{
		// Token: 0x17001999 RID: 6553
		// (get) Token: 0x060066FF RID: 26367 RVA: 0x001857CC File Offset: 0x001839CC
		bool ITypeReference.IsEnum
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700199A RID: 6554
		// (get) Token: 0x06006700 RID: 26368 RVA: 0x001857CF File Offset: 0x001839CF
		bool ITypeReference.IsValueType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006701 RID: 26369 RVA: 0x001857D2 File Offset: 0x001839D2
		ITypeDefinition ITypeReference.GetResolvedType(EmitContext context)
		{
			return null;
		}

		// Token: 0x1700199B RID: 6555
		// (get) Token: 0x06006702 RID: 26370 RVA: 0x001857D5 File Offset: 0x001839D5
		Microsoft.Cci.PrimitiveTypeCode ITypeReference.TypeCode
		{
			get
			{
				return Microsoft.Cci.PrimitiveTypeCode.NotPrimitive;
			}
		}

		// Token: 0x1700199C RID: 6556
		// (get) Token: 0x06006703 RID: 26371 RVA: 0x001857DC File Offset: 0x001839DC
		TypeDefinitionHandle ITypeReference.TypeDef
		{
			get
			{
				return default(TypeDefinitionHandle);
			}
		}

		// Token: 0x1700199D RID: 6557
		// (get) Token: 0x06006704 RID: 26372 RVA: 0x001857F2 File Offset: 0x001839F2
		IGenericMethodParameter IGenericParameter.AsGenericMethodParameter
		{
			get
			{
				if (this.AdaptedTypeParameterSymbol.ContainingSymbol.Kind == SymbolKind.Method)
				{
					return this;
				}
				return null;
			}
		}

		// Token: 0x1700199E RID: 6558
		// (get) Token: 0x06006705 RID: 26373 RVA: 0x0018580B File Offset: 0x00183A0B
		IGenericMethodParameterReference ITypeReference.AsGenericMethodParameterReference
		{
			get
			{
				if (this.AdaptedTypeParameterSymbol.ContainingSymbol.Kind == SymbolKind.Method)
				{
					return this;
				}
				return null;
			}
		}

		// Token: 0x1700199F RID: 6559
		// (get) Token: 0x06006706 RID: 26374 RVA: 0x00185824 File Offset: 0x00183A24
		IGenericTypeInstanceReference ITypeReference.AsGenericTypeInstanceReference
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170019A0 RID: 6560
		// (get) Token: 0x06006707 RID: 26375 RVA: 0x00185827 File Offset: 0x00183A27
		IGenericTypeParameter IGenericParameter.AsGenericTypeParameter
		{
			get
			{
				if (this.AdaptedTypeParameterSymbol.ContainingSymbol.Kind == SymbolKind.NamedType)
				{
					return this;
				}
				return null;
			}
		}

		// Token: 0x170019A1 RID: 6561
		// (get) Token: 0x06006708 RID: 26376 RVA: 0x00185840 File Offset: 0x00183A40
		IGenericTypeParameterReference ITypeReference.AsGenericTypeParameterReference
		{
			get
			{
				if (this.AdaptedTypeParameterSymbol.ContainingSymbol.Kind == SymbolKind.NamedType)
				{
					return this;
				}
				return null;
			}
		}

		// Token: 0x06006709 RID: 26377 RVA: 0x00185859 File Offset: 0x00183A59
		INamespaceTypeDefinition ITypeReference.AsNamespaceTypeDefinition(EmitContext context)
		{
			return null;
		}

		// Token: 0x170019A2 RID: 6562
		// (get) Token: 0x0600670A RID: 26378 RVA: 0x0018585C File Offset: 0x00183A5C
		INamespaceTypeReference ITypeReference.AsNamespaceTypeReference
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600670B RID: 26379 RVA: 0x0018585F File Offset: 0x00183A5F
		INestedTypeDefinition ITypeReference.AsNestedTypeDefinition(EmitContext context)
		{
			return null;
		}

		// Token: 0x170019A3 RID: 6563
		// (get) Token: 0x0600670C RID: 26380 RVA: 0x00185862 File Offset: 0x00183A62
		INestedTypeReference ITypeReference.AsNestedTypeReference
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170019A4 RID: 6564
		// (get) Token: 0x0600670D RID: 26381 RVA: 0x00185865 File Offset: 0x00183A65
		ISpecializedNestedTypeReference ITypeReference.AsSpecializedNestedTypeReference
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600670E RID: 26382 RVA: 0x00185868 File Offset: 0x00183A68
		ITypeDefinition ITypeReference.AsTypeDefinition(EmitContext context)
		{
			return null;
		}

		// Token: 0x0600670F RID: 26383 RVA: 0x0018586B File Offset: 0x00183A6B
		void IReference.Dispatch(MetadataVisitor visitor)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06006710 RID: 26384 RVA: 0x00185872 File Offset: 0x00183A72
		IDefinition IReference.AsDefinition(EmitContext context)
		{
			return null;
		}

		// Token: 0x170019A5 RID: 6565
		// (get) Token: 0x06006711 RID: 26385 RVA: 0x00185875 File Offset: 0x00183A75
		string INamedEntity.Name
		{
			get
			{
				return this.AdaptedTypeParameterSymbol.MetadataName;
			}
		}

		// Token: 0x170019A6 RID: 6566
		// (get) Token: 0x06006712 RID: 26386 RVA: 0x00185882 File Offset: 0x00183A82
		ushort IParameterListEntry.Index
		{
			get
			{
				return (ushort)this.AdaptedTypeParameterSymbol.Ordinal;
			}
		}

		// Token: 0x170019A7 RID: 6567
		// (get) Token: 0x06006713 RID: 26387 RVA: 0x00185890 File Offset: 0x00183A90
		IMethodReference IGenericMethodParameterReference.DefiningMethod
		{
			get
			{
				return ((MethodSymbol)this.AdaptedTypeParameterSymbol.ContainingSymbol).GetCciAdapter();
			}
		}

		// Token: 0x170019A8 RID: 6568
		// (get) Token: 0x06006714 RID: 26388 RVA: 0x001858A7 File Offset: 0x00183AA7
		ITypeReference IGenericTypeParameterReference.DefiningType
		{
			get
			{
				return ((NamedTypeSymbol)this.AdaptedTypeParameterSymbol.ContainingSymbol).GetCciAdapter();
			}
		}

		// Token: 0x06006715 RID: 26389 RVA: 0x001858BE File Offset: 0x00183ABE
		IEnumerable<TypeReferenceWithAttributes> IGenericParameter.GetConstraints(EmitContext context)
		{
			PEModuleBuilder moduleBeingBuilt = (PEModuleBuilder)context.Module;
			bool seenValueType = false;
			if (this.AdaptedTypeParameterSymbol.HasUnmanagedTypeConstraint)
			{
				INamedTypeReference specialType = moduleBeingBuilt.GetSpecialType(SpecialType.System_ValueType, (CSharpSyntaxNode)context.SyntaxNode, context.Diagnostics);
				CustomModifier customModifier = CSharpCustomModifier.CreateRequired(moduleBeingBuilt.Compilation.GetWellKnownType(WellKnownType.System_Runtime_InteropServices_UnmanagedType));
				yield return new TypeReferenceWithAttributes(new ModifiedTypeReference(specialType, ImmutableArray.Create<ICustomModifier>(customModifier)), default(ImmutableArray<ICustomAttribute>));
				seenValueType = true;
			}
			foreach (TypeWithAnnotations typeWithAnnotations in this.AdaptedTypeParameterSymbol.ConstraintTypesNoUseSiteDiagnostics)
			{
				SpecialType specialType2 = typeWithAnnotations.SpecialType;
				if (specialType2 != SpecialType.System_Object && specialType2 == SpecialType.System_ValueType)
				{
					seenValueType = true;
				}
				ITypeReference typeReference = moduleBeingBuilt.Translate(typeWithAnnotations.Type, (CSharpSyntaxNode)context.SyntaxNode, context.Diagnostics);
				yield return typeWithAnnotations.GetTypeRefWithAttributes(moduleBeingBuilt, this.AdaptedTypeParameterSymbol, typeReference);
			}
			ImmutableArray<TypeWithAnnotations>.Enumerator enumerator = default(ImmutableArray<TypeWithAnnotations>.Enumerator);
			if (this.AdaptedTypeParameterSymbol.HasValueTypeConstraint && !seenValueType)
			{
				INamedTypeReference specialType3 = moduleBeingBuilt.GetSpecialType(SpecialType.System_ValueType, (CSharpSyntaxNode)context.SyntaxNode, context.Diagnostics);
				yield return new TypeReferenceWithAttributes(specialType3, default(ImmutableArray<ICustomAttribute>));
			}
			yield break;
		}

		// Token: 0x170019A9 RID: 6569
		// (get) Token: 0x06006716 RID: 26390 RVA: 0x001858D5 File Offset: 0x00183AD5
		bool IGenericParameter.MustBeReferenceType
		{
			get
			{
				return this.AdaptedTypeParameterSymbol.HasReferenceTypeConstraint;
			}
		}

		// Token: 0x170019AA RID: 6570
		// (get) Token: 0x06006717 RID: 26391 RVA: 0x001858E2 File Offset: 0x00183AE2
		bool IGenericParameter.MustBeValueType
		{
			get
			{
				return this.AdaptedTypeParameterSymbol.HasValueTypeConstraint || this.AdaptedTypeParameterSymbol.HasUnmanagedTypeConstraint;
			}
		}

		// Token: 0x170019AB RID: 6571
		// (get) Token: 0x06006718 RID: 26392 RVA: 0x001858FE File Offset: 0x00183AFE
		bool IGenericParameter.MustHaveDefaultConstructor
		{
			get
			{
				return this.AdaptedTypeParameterSymbol.HasConstructorConstraint || this.AdaptedTypeParameterSymbol.HasValueTypeConstraint || this.AdaptedTypeParameterSymbol.HasUnmanagedTypeConstraint;
			}
		}

		// Token: 0x170019AC RID: 6572
		// (get) Token: 0x06006719 RID: 26393 RVA: 0x00185928 File Offset: 0x00183B28
		TypeParameterVariance IGenericParameter.Variance
		{
			get
			{
				switch (this.AdaptedTypeParameterSymbol.Variance)
				{
				case VarianceKind.None:
					return TypeParameterVariance.NonVariant;
				case VarianceKind.Out:
					return TypeParameterVariance.Covariant;
				case VarianceKind.In:
					return TypeParameterVariance.Contravariant;
				default:
					throw ExceptionUtilities.UnexpectedValue(this.AdaptedTypeParameterSymbol.Variance);
				}
			}
		}

		// Token: 0x170019AD RID: 6573
		// (get) Token: 0x0600671A RID: 26394 RVA: 0x00185970 File Offset: 0x00183B70
		IMethodDefinition IGenericMethodParameter.DefiningMethod
		{
			get
			{
				return ((MethodSymbol)this.AdaptedTypeParameterSymbol.ContainingSymbol).GetCciAdapter();
			}
		}

		// Token: 0x170019AE RID: 6574
		// (get) Token: 0x0600671B RID: 26395 RVA: 0x00185987 File Offset: 0x00183B87
		ITypeDefinition IGenericTypeParameter.DefiningType
		{
			get
			{
				return ((NamedTypeSymbol)this.AdaptedTypeParameterSymbol.ContainingSymbol).GetCciAdapter();
			}
		}

		// Token: 0x170019AF RID: 6575
		// (get) Token: 0x0600671C RID: 26396 RVA: 0x0018599E File Offset: 0x00183B9E
		internal TypeParameterSymbol AdaptedTypeParameterSymbol
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600671D RID: 26397 RVA: 0x001859A1 File Offset: 0x00183BA1
		internal new TypeParameterSymbol GetCciAdapter()
		{
			return this;
		}

		// Token: 0x170019B0 RID: 6576
		// (get) Token: 0x0600671E RID: 26398 RVA: 0x001859A4 File Offset: 0x00183BA4
		public new virtual TypeParameterSymbol OriginalDefinition
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170019B1 RID: 6577
		// (get) Token: 0x0600671F RID: 26399 RVA: 0x001859A7 File Offset: 0x00183BA7
		protected sealed override TypeSymbol OriginalTypeSymbolDefinition
		{
			get
			{
				return this.OriginalDefinition;
			}
		}

		// Token: 0x170019B2 RID: 6578
		// (get) Token: 0x06006720 RID: 26400 RVA: 0x001859AF File Offset: 0x00183BAF
		public virtual TypeParameterSymbol ReducedFrom
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170019B3 RID: 6579
		// (get) Token: 0x06006721 RID: 26401
		public abstract int Ordinal { get; }

		// Token: 0x06006722 RID: 26402 RVA: 0x001859B4 File Offset: 0x00183BB4
		internal virtual UseSiteInfo<AssemblySymbol> GetConstraintsUseSiteErrorInfo()
		{
			return default(UseSiteInfo<AssemblySymbol>);
		}

		// Token: 0x170019B4 RID: 6580
		// (get) Token: 0x06006723 RID: 26403 RVA: 0x001859CA File Offset: 0x00183BCA
		internal ImmutableArray<TypeWithAnnotations> ConstraintTypesNoUseSiteDiagnostics
		{
			get
			{
				this.EnsureAllConstraintsAreResolved();
				return this.GetConstraintTypes(ConsList<TypeParameterSymbol>.Empty);
			}
		}

		// Token: 0x06006724 RID: 26404 RVA: 0x001859E0 File Offset: 0x00183BE0
		internal ImmutableArray<TypeWithAnnotations> ConstraintTypesWithDefinitionUseSiteDiagnostics(ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			ImmutableArray<TypeWithAnnotations> constraintTypesNoUseSiteDiagnostics = this.ConstraintTypesNoUseSiteDiagnostics;
			this.AppendConstraintsUseSiteErrorInfo(ref useSiteInfo);
			foreach (TypeWithAnnotations typeWithAnnotations in constraintTypesNoUseSiteDiagnostics)
			{
				typeWithAnnotations.Type.OriginalDefinition.AddUseSiteInfo(ref useSiteInfo, true);
			}
			return constraintTypesNoUseSiteDiagnostics;
		}

		// Token: 0x06006725 RID: 26405 RVA: 0x00185A2A File Offset: 0x00183C2A
		private void AppendConstraintsUseSiteErrorInfo(ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			useSiteInfo.Add(this.GetConstraintsUseSiteErrorInfo());
		}

		// Token: 0x170019B5 RID: 6581
		// (get) Token: 0x06006726 RID: 26406
		public abstract bool HasConstructorConstraint { get; }

		// Token: 0x170019B6 RID: 6582
		// (get) Token: 0x06006727 RID: 26407
		public abstract TypeParameterKind TypeParameterKind { get; }

		// Token: 0x170019B7 RID: 6583
		// (get) Token: 0x06006728 RID: 26408 RVA: 0x00185A38 File Offset: 0x00183C38
		public MethodSymbol DeclaringMethod
		{
			get
			{
				return this.ContainingSymbol as MethodSymbol;
			}
		}

		// Token: 0x170019B8 RID: 6584
		// (get) Token: 0x06006729 RID: 26409 RVA: 0x00185A45 File Offset: 0x00183C45
		public NamedTypeSymbol DeclaringType
		{
			get
			{
				return this.ContainingSymbol as NamedTypeSymbol;
			}
		}

		// Token: 0x0600672A RID: 26410 RVA: 0x00185A52 File Offset: 0x00183C52
		public sealed override ImmutableArray<Symbol> GetMembers()
		{
			return ImmutableArray<Symbol>.Empty;
		}

		// Token: 0x0600672B RID: 26411 RVA: 0x00185A59 File Offset: 0x00183C59
		public sealed override ImmutableArray<Symbol> GetMembers(string name)
		{
			return ImmutableArray<Symbol>.Empty;
		}

		// Token: 0x0600672C RID: 26412 RVA: 0x00185A60 File Offset: 0x00183C60
		public sealed override ImmutableArray<NamedTypeSymbol> GetTypeMembers()
		{
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x0600672D RID: 26413 RVA: 0x00185A67 File Offset: 0x00183C67
		public sealed override ImmutableArray<NamedTypeSymbol> GetTypeMembers(string name)
		{
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x0600672E RID: 26414 RVA: 0x00185A6E File Offset: 0x00183C6E
		public sealed override ImmutableArray<NamedTypeSymbol> GetTypeMembers(string name, int arity)
		{
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x0600672F RID: 26415 RVA: 0x00185A75 File Offset: 0x00183C75
		internal override TResult Accept<TArgument, TResult>(CSharpSymbolVisitor<TArgument, TResult> visitor, TArgument argument)
		{
			return visitor.VisitTypeParameter(this, argument);
		}

		// Token: 0x06006730 RID: 26416 RVA: 0x00185A7F File Offset: 0x00183C7F
		public override void Accept(CSharpSymbolVisitor visitor)
		{
			visitor.VisitTypeParameter(this);
		}

		// Token: 0x06006731 RID: 26417 RVA: 0x00185A88 File Offset: 0x00183C88
		public override TResult Accept<TResult>(CSharpSymbolVisitor<TResult> visitor)
		{
			return visitor.VisitTypeParameter(this);
		}

		// Token: 0x170019B9 RID: 6585
		// (get) Token: 0x06006732 RID: 26418 RVA: 0x00185A91 File Offset: 0x00183C91
		public sealed override SymbolKind Kind
		{
			get
			{
				return SymbolKind.TypeParameter;
			}
		}

		// Token: 0x170019BA RID: 6586
		// (get) Token: 0x06006733 RID: 26419 RVA: 0x00185A95 File Offset: 0x00183C95
		public sealed override TypeKind TypeKind
		{
			get
			{
				return TypeKind.TypeParameter;
			}
		}

		// Token: 0x06006734 RID: 26420 RVA: 0x00185A99 File Offset: 0x00183C99
		internal TypeParameterSymbol()
		{
		}

		// Token: 0x170019BB RID: 6587
		// (get) Token: 0x06006735 RID: 26421 RVA: 0x00185AA1 File Offset: 0x00183CA1
		public sealed override Accessibility DeclaredAccessibility
		{
			get
			{
				return Accessibility.NotApplicable;
			}
		}

		// Token: 0x170019BC RID: 6588
		// (get) Token: 0x06006736 RID: 26422 RVA: 0x00185AA4 File Offset: 0x00183CA4
		public sealed override bool IsStatic
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170019BD RID: 6589
		// (get) Token: 0x06006737 RID: 26423 RVA: 0x00185AA7 File Offset: 0x00183CA7
		public sealed override bool IsAbstract
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170019BE RID: 6590
		// (get) Token: 0x06006738 RID: 26424 RVA: 0x00185AAA File Offset: 0x00183CAA
		public sealed override bool IsSealed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170019BF RID: 6591
		// (get) Token: 0x06006739 RID: 26425 RVA: 0x00185AAD File Offset: 0x00183CAD
		internal sealed override NamedTypeSymbol BaseTypeNoUseSiteDiagnostics
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600673A RID: 26426 RVA: 0x00185AB0 File Offset: 0x00183CB0
		internal sealed override ImmutableArray<NamedTypeSymbol> InterfacesNoUseSiteDiagnostics(ConsList<TypeSymbol> basesBeingResolved = null)
		{
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x0600673B RID: 26427 RVA: 0x00185AB7 File Offset: 0x00183CB7
		protected override ImmutableArray<NamedTypeSymbol> GetAllInterfaces()
		{
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x170019C0 RID: 6592
		// (get) Token: 0x0600673C RID: 26428 RVA: 0x00185ABE File Offset: 0x00183CBE
		internal NamedTypeSymbol EffectiveBaseClassNoUseSiteDiagnostics
		{
			get
			{
				this.EnsureAllConstraintsAreResolved();
				return this.GetEffectiveBaseClass(ConsList<TypeParameterSymbol>.Empty);
			}
		}

		// Token: 0x0600673D RID: 26429 RVA: 0x00185AD4 File Offset: 0x00183CD4
		internal NamedTypeSymbol EffectiveBaseClass(ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			this.AppendConstraintsUseSiteErrorInfo(ref useSiteInfo);
			NamedTypeSymbol effectiveBaseClassNoUseSiteDiagnostics = this.EffectiveBaseClassNoUseSiteDiagnostics;
			if (effectiveBaseClassNoUseSiteDiagnostics != null)
			{
				effectiveBaseClassNoUseSiteDiagnostics.OriginalDefinition.AddUseSiteInfo(ref useSiteInfo, true);
			}
			return effectiveBaseClassNoUseSiteDiagnostics;
		}

		// Token: 0x170019C1 RID: 6593
		// (get) Token: 0x0600673E RID: 26430 RVA: 0x00185B00 File Offset: 0x00183D00
		internal ImmutableArray<NamedTypeSymbol> EffectiveInterfacesNoUseSiteDiagnostics
		{
			get
			{
				this.EnsureAllConstraintsAreResolved();
				return this.GetInterfaces(ConsList<TypeParameterSymbol>.Empty);
			}
		}

		// Token: 0x170019C2 RID: 6594
		// (get) Token: 0x0600673F RID: 26431 RVA: 0x00185B13 File Offset: 0x00183D13
		internal TypeSymbol DeducedBaseTypeNoUseSiteDiagnostics
		{
			get
			{
				this.EnsureAllConstraintsAreResolved();
				return this.GetDeducedBaseType(ConsList<TypeParameterSymbol>.Empty);
			}
		}

		// Token: 0x06006740 RID: 26432 RVA: 0x00185B28 File Offset: 0x00183D28
		internal TypeSymbol DeducedBaseType(ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			this.AppendConstraintsUseSiteErrorInfo(ref useSiteInfo);
			TypeSymbol deducedBaseTypeNoUseSiteDiagnostics = this.DeducedBaseTypeNoUseSiteDiagnostics;
			if (deducedBaseTypeNoUseSiteDiagnostics != null)
			{
				deducedBaseTypeNoUseSiteDiagnostics.OriginalDefinition.AddUseSiteInfo(ref useSiteInfo, true);
			}
			return deducedBaseTypeNoUseSiteDiagnostics;
		}

		// Token: 0x170019C3 RID: 6595
		// (get) Token: 0x06006741 RID: 26433 RVA: 0x00185B54 File Offset: 0x00183D54
		internal ImmutableArray<NamedTypeSymbol> AllEffectiveInterfacesNoUseSiteDiagnostics
		{
			get
			{
				return base.GetAllInterfaces();
			}
		}

		// Token: 0x06006742 RID: 26434 RVA: 0x00185B5C File Offset: 0x00183D5C
		internal ImmutableArray<NamedTypeSymbol> AllEffectiveInterfacesWithDefinitionUseSiteDiagnostics(ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			ImmutableArray<NamedTypeSymbol> allEffectiveInterfacesNoUseSiteDiagnostics = this.AllEffectiveInterfacesNoUseSiteDiagnostics;
			for (TypeSymbol typeSymbol = this.DeducedBaseType(ref useSiteInfo); typeSymbol != null; typeSymbol = typeSymbol.BaseTypeWithDefinitionUseSiteDiagnostics(ref useSiteInfo))
			{
			}
			foreach (NamedTypeSymbol namedTypeSymbol in allEffectiveInterfacesNoUseSiteDiagnostics)
			{
				namedTypeSymbol.OriginalDefinition.AddUseSiteInfo(ref useSiteInfo, true);
			}
			return allEffectiveInterfacesNoUseSiteDiagnostics;
		}

		// Token: 0x06006743 RID: 26435
		internal abstract void EnsureAllConstraintsAreResolved();

		// Token: 0x06006744 RID: 26436 RVA: 0x00185BAC File Offset: 0x00183DAC
		protected static void EnsureAllConstraintsAreResolved(ImmutableArray<TypeParameterSymbol> typeParameters)
		{
			foreach (TypeParameterSymbol typeParameterSymbol in typeParameters)
			{
				typeParameterSymbol.GetConstraintTypes(ConsList<TypeParameterSymbol>.Empty);
			}
		}

		// Token: 0x06006745 RID: 26437
		internal abstract ImmutableArray<TypeWithAnnotations> GetConstraintTypes(ConsList<TypeParameterSymbol> inProgress);

		// Token: 0x06006746 RID: 26438
		internal abstract ImmutableArray<NamedTypeSymbol> GetInterfaces(ConsList<TypeParameterSymbol> inProgress);

		// Token: 0x06006747 RID: 26439
		internal abstract NamedTypeSymbol GetEffectiveBaseClass(ConsList<TypeParameterSymbol> inProgress);

		// Token: 0x06006748 RID: 26440
		internal abstract TypeSymbol GetDeducedBaseType(ConsList<TypeParameterSymbol> inProgress);

		// Token: 0x06006749 RID: 26441 RVA: 0x00185BDE File Offset: 0x00183DDE
		private static bool ConstraintImpliesReferenceType(TypeSymbol constraint)
		{
			if (constraint.TypeKind == TypeKind.TypeParameter)
			{
				return ((TypeParameterSymbol)constraint).IsReferenceTypeFromConstraintTypes;
			}
			return TypeParameterSymbol.NonTypeParameterConstraintImpliesReferenceType(constraint);
		}

		// Token: 0x0600674A RID: 26442 RVA: 0x00185BFC File Offset: 0x00183DFC
		internal static bool NonTypeParameterConstraintImpliesReferenceType(TypeSymbol constraint)
		{
			if (!constraint.IsReferenceType)
			{
				return false;
			}
			TypeKind typeKind = constraint.TypeKind;
			if (typeKind == TypeKind.Error)
			{
				return false;
			}
			if (typeKind == TypeKind.Interface)
			{
				return false;
			}
			SpecialType specialType = constraint.SpecialType;
			return specialType - SpecialType.System_Object > 1 && specialType != SpecialType.System_ValueType;
		}

		// Token: 0x0600674B RID: 26443 RVA: 0x00185C3C File Offset: 0x00183E3C
		internal static bool CalculateIsReferenceTypeFromConstraintTypes(ImmutableArray<TypeWithAnnotations> constraintTypes)
		{
			foreach (TypeWithAnnotations typeWithAnnotations in constraintTypes)
			{
				if (TypeParameterSymbol.ConstraintImpliesReferenceType(typeWithAnnotations.Type))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600674C RID: 26444 RVA: 0x00185C78 File Offset: 0x00183E78
		internal static bool? IsNotNullableFromConstraintTypes(ImmutableArray<TypeWithAnnotations> constraintTypes)
		{
			bool? flag = new bool?(false);
			foreach (TypeWithAnnotations typeWithAnnotations in constraintTypes)
			{
				bool flag3;
				bool? flag2 = TypeParameterSymbol.IsNotNullableFromConstraintType(typeWithAnnotations, out flag3);
				bool? flag4 = flag2;
				flag3 = true;
				if ((flag4.GetValueOrDefault() == flag3) & (flag4 != null))
				{
					return new bool?(true);
				}
				if (flag2 == null)
				{
					flag = default(bool?);
				}
			}
			return flag;
		}

		// Token: 0x0600674D RID: 26445 RVA: 0x00185CE4 File Offset: 0x00183EE4
		internal static bool? IsNotNullableFromConstraintType(TypeWithAnnotations constraintType, out bool isNonNullableValueType)
		{
			if (constraintType.Type.IsNonNullableValueType())
			{
				isNonNullableValueType = true;
				return new bool?(true);
			}
			isNonNullableValueType = false;
			if (constraintType.NullableAnnotation.IsAnnotated())
			{
				return new bool?(false);
			}
			if (constraintType.TypeKind == TypeKind.TypeParameter)
			{
				bool? isNotNullable = ((TypeParameterSymbol)constraintType.Type).IsNotNullable;
				bool? flag = isNotNullable;
				bool flag2 = false;
				if ((flag.GetValueOrDefault() == flag2) & (flag != null))
				{
					return new bool?(false);
				}
				if (isNotNullable == null)
				{
					return default(bool?);
				}
			}
			if (constraintType.NullableAnnotation.IsOblivious())
			{
				return default(bool?);
			}
			return new bool?(true);
		}

		// Token: 0x0600674E RID: 26446 RVA: 0x00185D8C File Offset: 0x00183F8C
		internal static bool CalculateIsValueTypeFromConstraintTypes(ImmutableArray<TypeWithAnnotations> constraintTypes)
		{
			foreach (TypeWithAnnotations typeWithAnnotations in constraintTypes)
			{
				if (typeWithAnnotations.Type.IsValueType)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x170019C4 RID: 6596
		// (get) Token: 0x0600674F RID: 26447 RVA: 0x00185DC5 File Offset: 0x00183FC5
		public sealed override bool IsReferenceType
		{
			get
			{
				return this.HasReferenceTypeConstraint || this.IsReferenceTypeFromConstraintTypes;
			}
		}

		// Token: 0x06006750 RID: 26448 RVA: 0x00185DD8 File Offset: 0x00183FD8
		protected bool? CalculateIsNotNullableFromNonTypeConstraints()
		{
			if (this.HasNotNullConstraint || this.HasValueTypeConstraint)
			{
				return new bool?(true);
			}
			if (!this.HasReferenceTypeConstraint)
			{
				return new bool?(false);
			}
			bool? referenceTypeConstraintIsNullable = this.ReferenceTypeConstraintIsNullable;
			if (referenceTypeConstraintIsNullable == null)
			{
				return default(bool?);
			}
			return new bool?(!referenceTypeConstraintIsNullable.GetValueOrDefault());
		}

		// Token: 0x06006751 RID: 26449 RVA: 0x00185E34 File Offset: 0x00184034
		protected bool? CalculateIsNotNullable()
		{
			bool? flag = this.CalculateIsNotNullableFromNonTypeConstraints();
			bool? flag2 = flag;
			bool flag3 = true;
			if ((flag2.GetValueOrDefault() == flag3) & (flag2 != null))
			{
				return flag;
			}
			ImmutableArray<TypeWithAnnotations> constraintTypesNoUseSiteDiagnostics = this.ConstraintTypesNoUseSiteDiagnostics;
			if (constraintTypesNoUseSiteDiagnostics.IsEmpty)
			{
				return flag;
			}
			bool? flag4 = TypeParameterSymbol.IsNotNullableFromConstraintTypes(constraintTypesNoUseSiteDiagnostics);
			flag2 = flag4;
			flag3 = true;
			if (!((flag2.GetValueOrDefault() == flag3) & (flag2 != null)))
			{
				flag2 = flag;
				flag3 = false;
				if (!((flag2.GetValueOrDefault() == flag3) & (flag2 != null)))
				{
					return default(bool?);
				}
			}
			return flag4;
		}

		// Token: 0x170019C5 RID: 6597
		// (get) Token: 0x06006752 RID: 26450
		internal abstract bool? IsNotNullable { get; }

		// Token: 0x170019C6 RID: 6598
		// (get) Token: 0x06006753 RID: 26451 RVA: 0x00185EBC File Offset: 0x001840BC
		public sealed override bool IsValueType
		{
			get
			{
				return this.HasValueTypeConstraint || this.IsValueTypeFromConstraintTypes;
			}
		}

		// Token: 0x06006754 RID: 26452 RVA: 0x00185ECE File Offset: 0x001840CE
		internal sealed override ManagedKind GetManagedKind(ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (!this.HasUnmanagedTypeConstraint)
			{
				return ManagedKind.Managed;
			}
			return ManagedKind.Unmanaged;
		}

		// Token: 0x170019C7 RID: 6599
		// (get) Token: 0x06006755 RID: 26453 RVA: 0x00185EDB File Offset: 0x001840DB
		public sealed override bool IsRefLikeType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170019C8 RID: 6600
		// (get) Token: 0x06006756 RID: 26454 RVA: 0x00185EDE File Offset: 0x001840DE
		public sealed override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170019C9 RID: 6601
		// (get) Token: 0x06006757 RID: 26455 RVA: 0x00185EE1 File Offset: 0x001840E1
		internal sealed override ObsoleteAttributeData ObsoleteAttributeData
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170019CA RID: 6602
		// (get) Token: 0x06006758 RID: 26456
		public abstract bool HasReferenceTypeConstraint { get; }

		// Token: 0x170019CB RID: 6603
		// (get) Token: 0x06006759 RID: 26457
		public abstract bool IsReferenceTypeFromConstraintTypes { get; }

		// Token: 0x170019CC RID: 6604
		// (get) Token: 0x0600675A RID: 26458
		internal abstract bool? ReferenceTypeConstraintIsNullable { get; }

		// Token: 0x170019CD RID: 6605
		// (get) Token: 0x0600675B RID: 26459
		public abstract bool HasNotNullConstraint { get; }

		// Token: 0x170019CE RID: 6606
		// (get) Token: 0x0600675C RID: 26460
		public abstract bool HasValueTypeConstraint { get; }

		// Token: 0x170019CF RID: 6607
		// (get) Token: 0x0600675D RID: 26461
		public abstract bool IsValueTypeFromConstraintTypes { get; }

		// Token: 0x170019D0 RID: 6608
		// (get) Token: 0x0600675E RID: 26462
		public abstract bool HasUnmanagedTypeConstraint { get; }

		// Token: 0x170019D1 RID: 6609
		// (get) Token: 0x0600675F RID: 26463
		public abstract VarianceKind Variance { get; }

		// Token: 0x06006760 RID: 26464 RVA: 0x00185EE4 File Offset: 0x001840E4
		internal sealed override bool GetUnificationUseSiteDiagnosticRecursive(ref DiagnosticInfo result, Symbol owner, ref HashSet<TypeSymbol> checkedTypes)
		{
			return false;
		}

		// Token: 0x06006761 RID: 26465 RVA: 0x00185EE7 File Offset: 0x001840E7
		internal override bool Equals(TypeSymbol t2, TypeCompareKind comparison)
		{
			return this.Equals(t2 as TypeParameterSymbol, comparison);
		}

		// Token: 0x06006762 RID: 26466 RVA: 0x00185EF6 File Offset: 0x001840F6
		internal bool Equals(TypeParameterSymbol other)
		{
			return this.Equals(other, TypeCompareKind.ConsiderEverything);
		}

		// Token: 0x06006763 RID: 26467 RVA: 0x00185F00 File Offset: 0x00184100
		private bool Equals(TypeParameterSymbol other, TypeCompareKind comparison)
		{
			return this == other || (other != null && other.OriginalDefinition == this.OriginalDefinition && other.ContainingSymbol.ContainingType.Equals(this.ContainingSymbol.ContainingType, comparison));
		}

		// Token: 0x06006764 RID: 26468 RVA: 0x00185F37 File Offset: 0x00184137
		public override int GetHashCode()
		{
			return Hash.Combine<Symbol>(this.ContainingSymbol, this.Ordinal);
		}

		// Token: 0x06006765 RID: 26469 RVA: 0x00185F4A File Offset: 0x0018414A
		internal override void AddNullableTransforms(ArrayBuilder<byte> transforms)
		{
		}

		// Token: 0x06006766 RID: 26470 RVA: 0x00185F4C File Offset: 0x0018414C
		internal override bool ApplyNullableTransforms(byte defaultTransformFlag, ImmutableArray<byte> transforms, ref int position, out TypeSymbol result)
		{
			result = this;
			return true;
		}

		// Token: 0x06006767 RID: 26471 RVA: 0x00185F53 File Offset: 0x00184153
		internal override TypeSymbol SetNullabilityForReferenceTypes(Func<TypeWithAnnotations, TypeWithAnnotations> transform)
		{
			return this;
		}

		// Token: 0x06006768 RID: 26472 RVA: 0x00185F56 File Offset: 0x00184156
		internal override TypeSymbol MergeEquivalentTypes(TypeSymbol other, VarianceKind variance)
		{
			return this;
		}

		// Token: 0x06006769 RID: 26473 RVA: 0x00185F59 File Offset: 0x00184159
		protected sealed override ISymbol CreateISymbol()
		{
			return new TypeParameterSymbol(this, base.DefaultNullableAnnotation);
		}

		// Token: 0x0600676A RID: 26474 RVA: 0x00185F67 File Offset: 0x00184167
		protected sealed override ITypeSymbol CreateITypeSymbol(NullableAnnotation nullableAnnotation)
		{
			return new TypeParameterSymbol(this, nullableAnnotation);
		}

		// Token: 0x170019D2 RID: 6610
		// (get) Token: 0x0600676B RID: 26475 RVA: 0x00185F70 File Offset: 0x00184170
		internal override bool IsRecord
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170019D3 RID: 6611
		// (get) Token: 0x0600676C RID: 26476 RVA: 0x00185F73 File Offset: 0x00184173
		internal override bool IsRecordStruct
		{
			get
			{
				return false;
			}
		}
	}
}
