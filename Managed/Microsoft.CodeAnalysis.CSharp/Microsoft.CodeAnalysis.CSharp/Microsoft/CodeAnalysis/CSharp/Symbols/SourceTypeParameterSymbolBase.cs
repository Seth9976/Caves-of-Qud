using System;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000550 RID: 1360
	internal abstract class SourceTypeParameterSymbolBase : TypeParameterSymbol, IAttributeTargetSymbol
	{
		// Token: 0x0600738F RID: 29583 RVA: 0x001B0501 File Offset: 0x001AE701
		protected SourceTypeParameterSymbolBase(string name, int ordinal, ImmutableArray<Location> locations, ImmutableArray<SyntaxReference> syntaxRefs)
		{
			this._name = name;
			this._ordinal = (short)ordinal;
			this._locations = locations;
			this._syntaxRefs = syntaxRefs;
		}

		// Token: 0x17001F8F RID: 8079
		// (get) Token: 0x06007390 RID: 29584 RVA: 0x001B0532 File Offset: 0x001AE732
		public override ImmutableArray<Location> Locations
		{
			get
			{
				return this._locations;
			}
		}

		// Token: 0x17001F90 RID: 8080
		// (get) Token: 0x06007391 RID: 29585 RVA: 0x001B053A File Offset: 0x001AE73A
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			get
			{
				return this._syntaxRefs;
			}
		}

		// Token: 0x17001F91 RID: 8081
		// (get) Token: 0x06007392 RID: 29586 RVA: 0x001B0542 File Offset: 0x001AE742
		internal ImmutableArray<SyntaxReference> SyntaxReferences
		{
			get
			{
				return this._syntaxRefs;
			}
		}

		// Token: 0x17001F92 RID: 8082
		// (get) Token: 0x06007393 RID: 29587 RVA: 0x001B054A File Offset: 0x001AE74A
		public override int Ordinal
		{
			get
			{
				return (int)this._ordinal;
			}
		}

		// Token: 0x17001F93 RID: 8083
		// (get) Token: 0x06007394 RID: 29588 RVA: 0x001B0552 File Offset: 0x001AE752
		public override VarianceKind Variance
		{
			get
			{
				return VarianceKind.None;
			}
		}

		// Token: 0x17001F94 RID: 8084
		// (get) Token: 0x06007395 RID: 29589 RVA: 0x001B0555 File Offset: 0x001AE755
		public override string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x06007396 RID: 29590 RVA: 0x001B0560 File Offset: 0x001AE760
		internal override ImmutableArray<TypeWithAnnotations> GetConstraintTypes(ConsList<TypeParameterSymbol> inProgress)
		{
			TypeParameterBounds bounds = this.GetBounds(inProgress);
			if (bounds == null)
			{
				return ImmutableArray<TypeWithAnnotations>.Empty;
			}
			return bounds.ConstraintTypes;
		}

		// Token: 0x06007397 RID: 29591 RVA: 0x001B0584 File Offset: 0x001AE784
		internal override ImmutableArray<NamedTypeSymbol> GetInterfaces(ConsList<TypeParameterSymbol> inProgress)
		{
			TypeParameterBounds bounds = this.GetBounds(inProgress);
			if (bounds == null)
			{
				return ImmutableArray<NamedTypeSymbol>.Empty;
			}
			return bounds.Interfaces;
		}

		// Token: 0x06007398 RID: 29592 RVA: 0x001B05A8 File Offset: 0x001AE7A8
		internal override NamedTypeSymbol GetEffectiveBaseClass(ConsList<TypeParameterSymbol> inProgress)
		{
			TypeParameterBounds bounds = this.GetBounds(inProgress);
			if (bounds == null)
			{
				return this.GetDefaultBaseType();
			}
			return bounds.EffectiveBaseClass;
		}

		// Token: 0x06007399 RID: 29593 RVA: 0x001B05D0 File Offset: 0x001AE7D0
		internal override TypeSymbol GetDeducedBaseType(ConsList<TypeParameterSymbol> inProgress)
		{
			TypeParameterBounds bounds = this.GetBounds(inProgress);
			if (bounds == null)
			{
				return this.GetDefaultBaseType();
			}
			return bounds.DeducedBaseType;
		}

		// Token: 0x17001F95 RID: 8085
		// (get) Token: 0x0600739A RID: 29594 RVA: 0x001B05F8 File Offset: 0x001AE7F8
		internal ImmutableArray<SyntaxList<AttributeListSyntax>> MergedAttributeDeclarationSyntaxLists
		{
			get
			{
				ArrayBuilder<SyntaxList<AttributeListSyntax>> instance = ArrayBuilder<SyntaxList<AttributeListSyntax>>.GetInstance();
				foreach (SyntaxReference syntaxReference in this._syntaxRefs)
				{
					TypeParameterSyntax typeParameterSyntax = (TypeParameterSyntax)syntaxReference.GetSyntax(default(CancellationToken));
					instance.Add(typeParameterSyntax.AttributeLists);
				}
				SourceOrdinaryMethodSymbol sourceOrdinaryMethodSymbol = this.ContainingSymbol as SourceOrdinaryMethodSymbol;
				if (sourceOrdinaryMethodSymbol != null && sourceOrdinaryMethodSymbol.IsPartial)
				{
					SourceOrdinaryMethodSymbol sourcePartialImplementation = sourceOrdinaryMethodSymbol.SourcePartialImplementation;
					if (sourcePartialImplementation != null)
					{
						SourceTypeParameterSymbolBase sourceTypeParameterSymbolBase = (SourceTypeParameterSymbolBase)sourcePartialImplementation.TypeParameters[(int)this._ordinal];
						instance.AddRange(sourceTypeParameterSymbolBase.MergedAttributeDeclarationSyntaxLists);
					}
				}
				return instance.ToImmutableAndFree();
			}
		}

		// Token: 0x17001F96 RID: 8086
		// (get) Token: 0x0600739B RID: 29595 RVA: 0x001B06A2 File Offset: 0x001AE8A2
		IAttributeTargetSymbol IAttributeTargetSymbol.AttributesOwner
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17001F97 RID: 8087
		// (get) Token: 0x0600739C RID: 29596 RVA: 0x001B06A5 File Offset: 0x001AE8A5
		AttributeLocation IAttributeTargetSymbol.DefaultAttributeLocation
		{
			get
			{
				return AttributeLocation.TypeParameter;
			}
		}

		// Token: 0x17001F98 RID: 8088
		// (get) Token: 0x0600739D RID: 29597 RVA: 0x001B06AC File Offset: 0x001AE8AC
		AttributeLocation IAttributeTargetSymbol.AllowedAttributeLocations
		{
			get
			{
				return AttributeLocation.TypeParameter;
			}
		}

		// Token: 0x0600739E RID: 29598 RVA: 0x001B06B3 File Offset: 0x001AE8B3
		public sealed override ImmutableArray<CSharpAttributeData> GetAttributes()
		{
			return this.GetAttributesBag().Attributes;
		}

		// Token: 0x0600739F RID: 29599 RVA: 0x001B06C0 File Offset: 0x001AE8C0
		internal virtual CustomAttributesBag<CSharpAttributeData> GetAttributesBag()
		{
			if (this._lazyCustomAttributesBag == null || !this._lazyCustomAttributesBag.IsSealed)
			{
				SourceOrdinaryMethodSymbol sourceOrdinaryMethodSymbol = this.ContainingSymbol as SourceOrdinaryMethodSymbol;
				bool flag2;
				if (sourceOrdinaryMethodSymbol == null || sourceOrdinaryMethodSymbol.SourcePartialDefinition == null)
				{
					OneOrMany<SyntaxList<AttributeListSyntax>> oneOrMany = OneOrMany.Create<SyntaxList<AttributeListSyntax>>(this.MergedAttributeDeclarationSyntaxLists);
					AttributeLocation attributeLocation = AttributeLocation.None;
					bool flag = false;
					LocalFunctionSymbol localFunctionSymbol = this.ContainingSymbol as LocalFunctionSymbol;
					flag2 = base.LoadAndValidateAttributes(oneOrMany, ref this._lazyCustomAttributesBag, attributeLocation, flag, (localFunctionSymbol != null) ? localFunctionSymbol.SignatureBinder : null, null);
				}
				else
				{
					CustomAttributesBag<CSharpAttributeData> attributesBag = ((SourceTypeParameterSymbolBase)sourceOrdinaryMethodSymbol.SourcePartialDefinition.TypeParameters[(int)this._ordinal]).GetAttributesBag();
					flag2 = Interlocked.CompareExchange<CustomAttributesBag<CSharpAttributeData>>(ref this._lazyCustomAttributesBag, attributesBag, null) == null;
				}
				if (flag2)
				{
					this._state.NotePartComplete(CompletionPart.Attributes);
				}
			}
			return this._lazyCustomAttributesBag;
		}

		// Token: 0x060073A0 RID: 29600 RVA: 0x001B077D File Offset: 0x001AE97D
		internal override void EnsureAllConstraintsAreResolved()
		{
			if (!this._lazyBounds.IsSet())
			{
				TypeParameterSymbol.EnsureAllConstraintsAreResolved(this.ContainerTypeParameters);
			}
		}

		// Token: 0x17001F99 RID: 8089
		// (get) Token: 0x060073A1 RID: 29601
		protected abstract ImmutableArray<TypeParameterSymbol> ContainerTypeParameters { get; }

		// Token: 0x060073A2 RID: 29602 RVA: 0x001B0798 File Offset: 0x001AE998
		private TypeParameterBounds GetBounds(ConsList<TypeParameterSymbol> inProgress)
		{
			if (!this._lazyBounds.IsSet())
			{
				BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance();
				TypeParameterBounds typeParameterBounds = this.ResolveBounds(inProgress, instance);
				if (Interlocked.CompareExchange<TypeParameterBounds>(ref this._lazyBounds, typeParameterBounds, TypeParameterBounds.Unset) == TypeParameterBounds.Unset)
				{
					this.CheckConstraintTypeConstraints(instance);
					this.CheckUnmanagedConstraint(instance);
					this.EnsureAttributesFromConstraints(instance);
					this.AddDeclarationDiagnostics(instance);
					this._state.NotePartComplete(CompletionPart.Members);
				}
				instance.Free();
			}
			return this._lazyBounds;
		}

		// Token: 0x060073A3 RID: 29603
		protected abstract TypeParameterBounds ResolveBounds(ConsList<TypeParameterSymbol> inProgress, BindingDiagnosticBag diagnostics);

		// Token: 0x060073A4 RID: 29604 RVA: 0x001B0814 File Offset: 0x001AEA14
		private void CheckConstraintTypeConstraints(BindingDiagnosticBag diagnostics)
		{
			ImmutableArray<TypeWithAnnotations> constraintTypesNoUseSiteDiagnostics = base.ConstraintTypesNoUseSiteDiagnostics;
			if (constraintTypesNoUseSiteDiagnostics.Length == 0)
			{
				return;
			}
			ConstraintsHelper.CheckConstraintsArgsBoxed checkConstraintsArgsBoxed = new ConstraintsHelper.CheckConstraintsArgsBoxed(this.DeclaringCompilation, new TypeConversions(this.ContainingAssembly.CorLibrary, false), this._locations[0], diagnostics);
			foreach (TypeWithAnnotations typeWithAnnotations in constraintTypesNoUseSiteDiagnostics)
			{
				if (!diagnostics.ReportUseSite(typeWithAnnotations.Type, checkConstraintsArgsBoxed.Args.Location))
				{
					typeWithAnnotations.Type.CheckAllConstraints(checkConstraintsArgsBoxed);
				}
			}
		}

		// Token: 0x060073A5 RID: 29605 RVA: 0x001B08A2 File Offset: 0x001AEAA2
		private void CheckUnmanagedConstraint(BindingDiagnosticBag diagnostics)
		{
			if (this.HasUnmanagedTypeConstraint)
			{
				this.DeclaringCompilation.EnsureIsUnmanagedAttributeExists(diagnostics, this.GetNonNullSyntaxNode().Location, this.ModifyCompilationForAttributeEmbedding());
			}
		}

		// Token: 0x060073A6 RID: 29606 RVA: 0x001B08CC File Offset: 0x001AEACC
		private bool ModifyCompilationForAttributeEmbedding()
		{
			Symbol containingSymbol = this.ContainingSymbol;
			bool flag;
			if (!(containingSymbol is SourceOrdinaryMethodSymbol) && !(containingSymbol is SourceMemberContainerTypeSymbol))
			{
				if (!(containingSymbol is LocalFunctionSymbol))
				{
					throw ExceptionUtilities.UnexpectedValue(this.ContainingSymbol);
				}
				flag = false;
			}
			else
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x060073A7 RID: 29607 RVA: 0x001B0910 File Offset: 0x001AEB10
		private void EnsureAttributesFromConstraints(BindingDiagnosticBag diagnostics)
		{
			if (base.ConstraintTypesNoUseSiteDiagnostics.Any<TypeWithAnnotations>((TypeWithAnnotations t) => t.ContainsNativeInteger()))
			{
				this.DeclaringCompilation.EnsureNativeIntegerAttributeExists(diagnostics, this.<EnsureAttributesFromConstraints>g__getLocation|42_1(), this.ModifyCompilationForAttributeEmbedding());
			}
			if (this.ConstraintsNeedNullableAttribute())
			{
				this.DeclaringCompilation.EnsureNullableAttributeExists(diagnostics, this.<EnsureAttributesFromConstraints>g__getLocation|42_1(), this.ModifyCompilationForAttributeEmbedding());
			}
		}

		// Token: 0x060073A8 RID: 29608 RVA: 0x001B0984 File Offset: 0x001AEB84
		internal bool ConstraintsNeedNullableAttribute()
		{
			if (!this.DeclaringCompilation.ShouldEmitNullableAttributes(this))
			{
				return false;
			}
			if (this.HasReferenceTypeConstraint && this.ReferenceTypeConstraintIsNullable != null)
			{
				return true;
			}
			if (base.ConstraintTypesNoUseSiteDiagnostics.Any<TypeWithAnnotations>((TypeWithAnnotations c) => c.NeedsNullableAttribute()))
			{
				return true;
			}
			if (this.HasNotNullConstraint)
			{
				return true;
			}
			if (!this.HasReferenceTypeConstraint && !this.HasValueTypeConstraint && base.ConstraintTypesNoUseSiteDiagnostics.IsEmpty)
			{
				bool? isNotNullable = this.IsNotNullable;
				bool flag = false;
				return (isNotNullable.GetValueOrDefault() == flag) & (isNotNullable != null);
			}
			return false;
		}

		// Token: 0x060073A9 RID: 29609 RVA: 0x001B0A30 File Offset: 0x001AEC30
		private NamedTypeSymbol GetDefaultBaseType()
		{
			return this.ContainingAssembly.GetSpecialType(SpecialType.System_Object);
		}

		// Token: 0x060073AA RID: 29610 RVA: 0x001B0A40 File Offset: 0x001AEC40
		internal override void ForceComplete(SourceLocation locationOpt, CancellationToken cancellationToken)
		{
			for (;;)
			{
				cancellationToken.ThrowIfCancellationRequested();
				CompletionPart nextIncompletePart = this._state.NextIncompletePart;
				if (nextIncompletePart == CompletionPart.None)
				{
					break;
				}
				if (nextIncompletePart != CompletionPart.Attributes)
				{
					if (nextIncompletePart != CompletionPart.Members)
					{
						this._state.NotePartComplete(CompletionPart.ReturnTypeAttributes | CompletionPart.Parameters | CompletionPart.Type | CompletionPart.StartBaseType | CompletionPart.FinishBaseType | CompletionPart.StartInterfaces | CompletionPart.FinishInterfaces | CompletionPart.EnumUnderlyingType | CompletionPart.TypeArguments | CompletionPart.TypeParameters | CompletionPart.TypeMembers | CompletionPart.SynthesizedExplicitImplementations | CompletionPart.StartMemberChecks | CompletionPart.FinishMemberChecks | CompletionPart.MembersCompleted);
					}
					else
					{
						ImmutableArray<TypeWithAnnotations> constraintTypesNoUseSiteDiagnostics = base.ConstraintTypesNoUseSiteDiagnostics;
					}
				}
				else
				{
					this.GetAttributes();
				}
				this._state.SpinWaitComplete(nextIncompletePart, cancellationToken);
			}
		}

		// Token: 0x060073AB RID: 29611 RVA: 0x001B0AA4 File Offset: 0x001AECA4
		internal override void AddSynthesizedAttributes(PEModuleBuilder moduleBuilder, ref ArrayBuilder<SynthesizedAttributeData> attributes)
		{
			base.AddSynthesizedAttributes(moduleBuilder, ref attributes);
			if (this.HasUnmanagedTypeConstraint)
			{
				Symbol.AddSynthesizedAttribute(ref attributes, moduleBuilder.SynthesizeIsUnmanagedAttribute(this));
			}
			if (this.DeclaringCompilation.ShouldEmitNullableAttributes(this))
			{
				Symbol.AddSynthesizedAttribute(ref attributes, moduleBuilder.SynthesizeNullableAttributeIfNecessary(this.GetNullableContextValue(), this.GetSynthesizedNullableAttributeValue()));
			}
		}

		// Token: 0x060073AC RID: 29612 RVA: 0x001B0AF4 File Offset: 0x001AECF4
		internal byte GetSynthesizedNullableAttributeValue()
		{
			if (this.HasReferenceTypeConstraint)
			{
				bool? flag = this.ReferenceTypeConstraintIsNullable;
				if (flag != null)
				{
					if (flag.GetValueOrDefault())
					{
						return 2;
					}
					return 1;
				}
			}
			else
			{
				if (this.HasNotNullConstraint)
				{
					return 1;
				}
				if (!this.HasValueTypeConstraint && base.ConstraintTypesNoUseSiteDiagnostics.IsEmpty)
				{
					bool? flag = this.IsNotNullable;
					bool flag2 = false;
					if ((flag.GetValueOrDefault() == flag2) & (flag != null))
					{
						return 2;
					}
				}
			}
			return 0;
		}

		// Token: 0x060073AD RID: 29613 RVA: 0x001B0B68 File Offset: 0x001AED68
		internal sealed override void DecodeWellKnownAttribute(ref DecodeWellKnownAttributeArguments<AttributeSyntax, CSharpAttributeData, AttributeLocation> arguments)
		{
			if (arguments.Attribute.IsTargetAttribute(this, AttributeDescription.NullableAttribute))
			{
				((BindingDiagnosticBag)arguments.Diagnostics).Add(ErrorCode.ERR_ExplicitNullableAttribute, arguments.AttributeSyntaxOpt.Location);
			}
			base.DecodeWellKnownAttribute(ref arguments);
		}

		// Token: 0x060073AE RID: 29614 RVA: 0x001B0BA8 File Offset: 0x001AEDA8
		protected bool? CalculateReferenceTypeConstraintIsNullable(TypeParameterConstraintKind constraints)
		{
			if ((constraints & TypeParameterConstraintKind.ReferenceType) == TypeParameterConstraintKind.None)
			{
				return new bool?(false);
			}
			TypeParameterConstraintKind typeParameterConstraintKind = constraints & TypeParameterConstraintKind.AllReferenceTypeKinds;
			if (typeParameterConstraintKind == TypeParameterConstraintKind.NullableReferenceType)
			{
				return new bool?(true);
			}
			if (typeParameterConstraintKind != TypeParameterConstraintKind.NotNullableReferenceType)
			{
				return default(bool?);
			}
			return new bool?(false);
		}

		// Token: 0x060073AF RID: 29615 RVA: 0x001B0BE9 File Offset: 0x001AEDE9
		[CompilerGenerated]
		private Location <EnsureAttributesFromConstraints>g__getLocation|42_1()
		{
			return this.GetNonNullSyntaxNode().Location;
		}

		// Token: 0x04001C11 RID: 7185
		private readonly ImmutableArray<SyntaxReference> _syntaxRefs;

		// Token: 0x04001C12 RID: 7186
		private readonly ImmutableArray<Location> _locations;

		// Token: 0x04001C13 RID: 7187
		private readonly string _name;

		// Token: 0x04001C14 RID: 7188
		private readonly short _ordinal;

		// Token: 0x04001C15 RID: 7189
		private SymbolCompletionState _state;

		// Token: 0x04001C16 RID: 7190
		private CustomAttributesBag<CSharpAttributeData> _lazyCustomAttributesBag;

		// Token: 0x04001C17 RID: 7191
		private TypeParameterBounds _lazyBounds = TypeParameterBounds.Unset;
	}
}
