using System;
using System.Collections.Immutable;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000548 RID: 1352
	internal sealed class SourceOrdinaryMethodSymbol : SourceOrdinaryMethodSymbolBase
	{
		// Token: 0x06007263 RID: 29283 RVA: 0x001AB5B4 File Offset: 0x001A97B4
		public static SourceOrdinaryMethodSymbol CreateMethodSymbol(NamedTypeSymbol containingType, Binder bodyBinder, MethodDeclarationSyntax syntax, bool isNullableAnalysisEnabled, BindingDiagnosticBag diagnostics)
		{
			ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier = syntax.ExplicitInterfaceSpecifier;
			SyntaxToken identifier = syntax.Identifier;
			TypeSymbol typeSymbol;
			string text;
			string memberNameAndInterfaceSymbol = ExplicitInterfaceHelpers.GetMemberNameAndInterfaceSymbol(bodyBinder, explicitInterfaceSpecifier, identifier.ValueText, diagnostics, out typeSymbol, out text);
			SourceLocation sourceLocation = new SourceLocation(in identifier);
			MethodKind methodKind = ((explicitInterfaceSpecifier == null) ? MethodKind.Ordinary : MethodKind.ExplicitInterfaceImplementation);
			return new SourceOrdinaryMethodSymbol(containingType, typeSymbol, memberNameAndInterfaceSymbol, sourceLocation, syntax, methodKind, isNullableAnalysisEnabled, diagnostics);
		}

		// Token: 0x06007264 RID: 29284 RVA: 0x001AB60C File Offset: 0x001A980C
		private SourceOrdinaryMethodSymbol(NamedTypeSymbol containingType, TypeSymbol explicitInterfaceType, string name, Location location, MethodDeclarationSyntax syntax, MethodKind methodKind, bool isNullableAnalysisEnabled, BindingDiagnosticBag diagnostics)
		{
			bool flag = SyntaxFacts.HasYieldOperations(syntax.Body);
			ParameterSyntax parameterSyntax = syntax.ParameterList.Parameters.FirstOrDefault();
			base..ctor(containingType, name, location, syntax, methodKind, flag, parameterSyntax != null && !parameterSyntax.IsArgList && parameterSyntax.Modifiers.Any(SyntaxKind.ThisKeyword), syntax.Modifiers.IndexOf(SyntaxKind.PartialKeyword) < 0, syntax.Body != null || syntax.ExpressionBody != null, isNullableAnalysisEnabled, diagnostics);
			this._explicitInterfaceType = explicitInterfaceType;
			bool flag2 = syntax.Body != null;
			this._isExpressionBodied = !flag2 && syntax.ExpressionBody != null;
			bool flag3 = flag2 || this._isExpressionBodied;
			this._hasAnyBody = flag3;
			this._refKind = syntax.ReturnType.GetRefKind();
			Symbol.CheckForBlockAndExpressionBody(syntax.Body, syntax.ExpressionBody, syntax, diagnostics);
		}

		// Token: 0x06007265 RID: 29285 RVA: 0x001AB6FC File Offset: 0x001A98FC
		protected override ImmutableArray<TypeParameterSymbol> MakeTypeParameters(CSharpSyntaxNode node, BindingDiagnosticBag diagnostics)
		{
			MethodDeclarationSyntax methodDeclarationSyntax = (MethodDeclarationSyntax)node;
			if (methodDeclarationSyntax.Arity == 0)
			{
				Symbol.ReportErrorIfHasConstraints(methodDeclarationSyntax.ConstraintClauses, diagnostics.DiagnosticBag);
				return ImmutableArray<TypeParameterSymbol>.Empty;
			}
			return this.MakeTypeParameters(methodDeclarationSyntax, diagnostics);
		}

		// Token: 0x06007266 RID: 29286 RVA: 0x001AB738 File Offset: 0x001A9938
		[return: TupleElementNames(new string[] { "ReturnType", "Parameters", "IsVararg", "DeclaredConstraintsForOverrideOrImplementation" })]
		protected override ValueTuple<TypeWithAnnotations, ImmutableArray<ParameterSymbol>, bool, ImmutableArray<TypeParameterConstraintClause>> MakeParametersAndBindReturnType(BindingDiagnosticBag diagnostics)
		{
			MethodDeclarationSyntax syntax = this.GetSyntax();
			Binder binder = this.DeclaringCompilation.GetBinderFactory(syntax.SyntaxTree, false).GetBinder(syntax.ReturnType, syntax, this).WithAdditionalFlagsAndContainingMemberOrLambda(BinderFlags.SuppressConstraintChecks, this);
			Binder binder2 = binder;
			BaseParameterListSyntax parameterList = syntax.ParameterList;
			bool flag = this.IsVirtual || this.IsAbstract;
			SyntaxToken syntaxToken;
			ImmutableArray<ParameterSymbol> immutableArray = ParameterHelpers.MakeParameters(binder2, this, parameterList, out syntaxToken, diagnostics, true, true, flag);
			this._lazyIsVararg = syntaxToken.Kind() == SyntaxKind.ArgListKeyword;
			RefKind refKind;
			TypeSyntax typeSyntax = syntax.ReturnType.SkipRef(out refKind);
			TypeWithAnnotations typeWithAnnotations = binder.BindType(typeSyntax, diagnostics, null, false);
			if (typeWithAnnotations.IsRestrictedType(true) && (typeWithAnnotations.SpecialType != SpecialType.System_TypedReference || (this.ContainingType.SpecialType != SpecialType.System_TypedReference && this.ContainingType.SpecialType != SpecialType.System_ArgIterator)))
			{
				diagnostics.Add(ErrorCode.ERR_MethodReturnCantBeRefAny, syntax.ReturnType.Location, new object[] { typeWithAnnotations.Type });
			}
			ImmutableArray<TypeParameterConstraintClause> immutableArray2 = default(ImmutableArray<TypeParameterConstraintClause>);
			if (this.Arity != 0 && (syntax.ExplicitInterfaceSpecifier != null || this.IsOverride))
			{
				if (syntax.ConstraintClauses.Count > 0)
				{
					Binder.CheckFeatureAvailability(syntax.SyntaxTree, MessageID.IDS_OverrideWithConstraints, diagnostics, syntax.ConstraintClauses[0].WhereKeyword.GetLocation());
					immutableArray2 = binder.WithAdditionalFlags(BinderFlags.SuppressConstraintChecks | BinderFlags.GenericConstraintsClause).BindTypeParameterConstraintClauses(this, this.TypeParameters, syntax.TypeParameterList, syntax.ConstraintClauses, diagnostics, false, true);
				}
				foreach (ParameterSymbol parameterSymbol in immutableArray)
				{
					SourceOrdinaryMethodSymbol.<MakeParametersAndBindReturnType>g__forceMethodTypeParameters|11_0(parameterSymbol.TypeWithAnnotations, this, immutableArray2);
				}
				SourceOrdinaryMethodSymbol.<MakeParametersAndBindReturnType>g__forceMethodTypeParameters|11_0(typeWithAnnotations, this, immutableArray2);
			}
			return new ValueTuple<TypeWithAnnotations, ImmutableArray<ParameterSymbol>, bool, ImmutableArray<TypeParameterConstraintClause>>(typeWithAnnotations, immutableArray, this._lazyIsVararg, immutableArray2);
		}

		// Token: 0x06007267 RID: 29287 RVA: 0x001AB8F8 File Offset: 0x001A9AF8
		protected override void ExtensionMethodChecks(BindingDiagnosticBag diagnostics)
		{
			if (this.IsExtensionMethod)
			{
				MethodDeclarationSyntax syntax = this.GetSyntax();
				Location location = this.locations[0];
				TypeWithAnnotations typeWithAnnotations = this.Parameters[0].TypeWithAnnotations;
				RefKind refKind = this.Parameters[0].RefKind;
				if (!typeWithAnnotations.Type.IsValidExtensionParameterType())
				{
					Location location2 = syntax.ParameterList.Parameters[0].Type.Location;
					diagnostics.Add(ErrorCode.ERR_BadTypeforThis, location2, new object[] { typeWithAnnotations.Type });
					return;
				}
				if (refKind == RefKind.Ref && !typeWithAnnotations.Type.IsValueType)
				{
					diagnostics.Add(ErrorCode.ERR_RefExtensionMustBeValueTypeOrConstrainedToOne, location, new object[] { this.Name });
					return;
				}
				if (refKind == RefKind.In && typeWithAnnotations.TypeKind != TypeKind.Struct)
				{
					diagnostics.Add(ErrorCode.ERR_InExtensionMustBeValueType, location, new object[] { this.Name });
					return;
				}
				if (this.ContainingType.ContainingType != null)
				{
					diagnostics.Add(ErrorCode.ERR_ExtensionMethodsDecl, location, new object[] { this.ContainingType.Name });
					return;
				}
				if (!this.ContainingType.IsScriptClass && (!this.ContainingType.IsStatic || this.ContainingType.Arity != 0))
				{
					TypeDeclarationSyntax typeDeclarationSyntax = syntax.Parent as TypeDeclarationSyntax;
					Location location3 = ((typeDeclarationSyntax != null) ? typeDeclarationSyntax.Identifier : syntax.Identifier).GetLocation();
					diagnostics.Add(ErrorCode.ERR_BadExtensionAgg, location3);
					return;
				}
				if (!this.IsStatic)
				{
					diagnostics.Add(ErrorCode.ERR_BadExtensionMeth, location);
					return;
				}
				UseSiteInfo<AssemblySymbol> useSiteInfo;
				bool wellKnownTypeMember = Binder.GetWellKnownTypeMember(this.DeclaringCompilation, WellKnownMember.System_Runtime_CompilerServices_ExtensionAttribute__ctor, out useSiteInfo, false) != null;
				Location location4 = syntax.ParameterList.Parameters[0].Modifiers.FirstOrDefault(SyntaxKind.ThisKeyword).GetLocation();
				if (!wellKnownTypeMember)
				{
					diagnostics.Add(ErrorCode.ERR_ExtensionAttrNotFound, location4, new object[] { WellKnownMembers.GetDescriptor(WellKnownMember.System_Runtime_CompilerServices_ExtensionAttribute__ctor).DeclaringTypeMetadataName });
					return;
				}
				diagnostics.Add(useSiteInfo, location4);
			}
		}

		// Token: 0x06007268 RID: 29288 RVA: 0x001ABB14 File Offset: 0x001A9D14
		protected override MethodSymbol FindExplicitlyImplementedMethod(BindingDiagnosticBag diagnostics)
		{
			MethodDeclarationSyntax syntax = this.GetSyntax();
			return this.FindExplicitlyImplementedMethod(this._explicitInterfaceType, syntax.Identifier.ValueText, syntax.ExplicitInterfaceSpecifier, diagnostics);
		}

		// Token: 0x17001EEB RID: 7915
		// (get) Token: 0x06007269 RID: 29289 RVA: 0x001ABB49 File Offset: 0x001A9D49
		protected override Location ReturnTypeLocation
		{
			get
			{
				return this.GetSyntax().ReturnType.Location;
			}
		}

		// Token: 0x17001EEC RID: 7916
		// (get) Token: 0x0600726A RID: 29290 RVA: 0x001ABB5B File Offset: 0x001A9D5B
		protected override TypeSymbol ExplicitInterfaceType
		{
			get
			{
				return this._explicitInterfaceType;
			}
		}

		// Token: 0x17001EED RID: 7917
		// (get) Token: 0x0600726B RID: 29291 RVA: 0x001ABB63 File Offset: 0x001A9D63
		protected override bool HasAnyBody
		{
			get
			{
				return this._hasAnyBody;
			}
		}

		// Token: 0x0600726C RID: 29292 RVA: 0x001ABB6C File Offset: 0x001A9D6C
		internal MethodDeclarationSyntax GetSyntax()
		{
			return (MethodDeclarationSyntax)this.syntaxReferenceOpt.GetSyntax(default(CancellationToken));
		}

		// Token: 0x0600726D RID: 29293 RVA: 0x001ABB92 File Offset: 0x001A9D92
		protected override void CompleteAsyncMethodChecksBetweenStartAndFinish()
		{
			if (this.IsPartialDefinition)
			{
				this.DeclaringCompilation.SymbolDeclaredEvent(this);
			}
		}

		// Token: 0x0600726E RID: 29294 RVA: 0x001ABBA8 File Offset: 0x001A9DA8
		public override ImmutableArray<ImmutableArray<TypeWithAnnotations>> GetTypeParameterConstraintTypes()
		{
			if (this._lazyTypeParameterConstraintTypes.IsDefault)
			{
				this.GetTypeParameterConstraintKinds();
				BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance();
				MethodDeclarationSyntax syntax = this.GetSyntax();
				Binder binder = this.DeclaringCompilation.GetBinderFactory(syntax.SyntaxTree, false).GetBinder(syntax.ReturnType, syntax, this);
				ImmutableArray<ImmutableArray<TypeWithAnnotations>> immutableArray = this.MakeTypeParameterConstraintTypes(binder, this.TypeParameters, syntax.TypeParameterList, syntax.ConstraintClauses, instance);
				if (ImmutableInterlocked.InterlockedInitialize<ImmutableArray<TypeWithAnnotations>>(ref this._lazyTypeParameterConstraintTypes, immutableArray))
				{
					this.AddDeclarationDiagnostics(instance);
				}
				instance.Free();
			}
			return this._lazyTypeParameterConstraintTypes;
		}

		// Token: 0x0600726F RID: 29295 RVA: 0x001ABC34 File Offset: 0x001A9E34
		public override ImmutableArray<TypeParameterConstraintKind> GetTypeParameterConstraintKinds()
		{
			if (this._lazyTypeParameterConstraintKinds.IsDefault)
			{
				MethodDeclarationSyntax syntax = this.GetSyntax();
				Binder binder = this.DeclaringCompilation.GetBinderFactory(syntax.SyntaxTree, false).GetBinder(syntax.ReturnType, syntax, this);
				ImmutableArray<TypeParameterConstraintKind> immutableArray = this.MakeTypeParameterConstraintKinds(binder, this.TypeParameters, syntax.TypeParameterList, syntax.ConstraintClauses);
				ImmutableInterlocked.InterlockedInitialize<TypeParameterConstraintKind>(ref this._lazyTypeParameterConstraintKinds, immutableArray);
			}
			return this._lazyTypeParameterConstraintKinds;
		}

		// Token: 0x17001EEE RID: 7918
		// (get) Token: 0x06007270 RID: 29296 RVA: 0x001ABCA2 File Offset: 0x001A9EA2
		public override bool IsVararg
		{
			get
			{
				base.LazyMethodChecks();
				return this._lazyIsVararg;
			}
		}

		// Token: 0x06007271 RID: 29297 RVA: 0x001ABCB0 File Offset: 0x001A9EB0
		protected override int GetParameterCountFromSyntax()
		{
			return this.GetSyntax().ParameterList.ParameterCount;
		}

		// Token: 0x17001EEF RID: 7919
		// (get) Token: 0x06007272 RID: 29298 RVA: 0x001ABCC2 File Offset: 0x001A9EC2
		public override RefKind RefKind
		{
			get
			{
				return this._refKind;
			}
		}

		// Token: 0x06007273 RID: 29299 RVA: 0x001ABCCA File Offset: 0x001A9ECA
		internal static void InitializePartialMethodParts(SourceOrdinaryMethodSymbol definition, SourceOrdinaryMethodSymbol implementation)
		{
			definition._otherPartOfPartial = implementation;
			implementation._otherPartOfPartial = definition;
		}

		// Token: 0x17001EF0 RID: 7920
		// (get) Token: 0x06007274 RID: 29300 RVA: 0x001ABCDA File Offset: 0x001A9EDA
		internal SourceOrdinaryMethodSymbol OtherPartOfPartial
		{
			get
			{
				return this._otherPartOfPartial;
			}
		}

		// Token: 0x17001EF1 RID: 7921
		// (get) Token: 0x06007275 RID: 29301 RVA: 0x001ABCE2 File Offset: 0x001A9EE2
		internal bool IsPartialDefinition
		{
			get
			{
				return base.IsPartial && !this._hasAnyBody && !base.HasExternModifier;
			}
		}

		// Token: 0x17001EF2 RID: 7922
		// (get) Token: 0x06007276 RID: 29302 RVA: 0x001ABCFF File Offset: 0x001A9EFF
		internal bool IsPartialImplementation
		{
			get
			{
				return base.IsPartial && (this._hasAnyBody || base.HasExternModifier);
			}
		}

		// Token: 0x17001EF3 RID: 7923
		// (get) Token: 0x06007277 RID: 29303 RVA: 0x001ABD1B File Offset: 0x001A9F1B
		internal bool IsPartialWithoutImplementation
		{
			get
			{
				return this.IsPartialDefinition && this._otherPartOfPartial == null;
			}
		}

		// Token: 0x17001EF4 RID: 7924
		// (get) Token: 0x06007278 RID: 29304 RVA: 0x001ABD30 File Offset: 0x001A9F30
		internal SourceOrdinaryMethodSymbol SourcePartialDefinition
		{
			get
			{
				if (!this.IsPartialImplementation)
				{
					return null;
				}
				return this._otherPartOfPartial;
			}
		}

		// Token: 0x17001EF5 RID: 7925
		// (get) Token: 0x06007279 RID: 29305 RVA: 0x001ABD42 File Offset: 0x001A9F42
		internal SourceOrdinaryMethodSymbol SourcePartialImplementation
		{
			get
			{
				if (!this.IsPartialDefinition)
				{
					return null;
				}
				return this._otherPartOfPartial;
			}
		}

		// Token: 0x17001EF6 RID: 7926
		// (get) Token: 0x0600727A RID: 29306 RVA: 0x001ABD54 File Offset: 0x001A9F54
		public override MethodSymbol PartialDefinitionPart
		{
			get
			{
				return this.SourcePartialDefinition;
			}
		}

		// Token: 0x17001EF7 RID: 7927
		// (get) Token: 0x0600727B RID: 29307 RVA: 0x001ABD5C File Offset: 0x001A9F5C
		public override MethodSymbol PartialImplementationPart
		{
			get
			{
				return this.SourcePartialImplementation;
			}
		}

		// Token: 0x17001EF8 RID: 7928
		// (get) Token: 0x0600727C RID: 29308 RVA: 0x001ABD64 File Offset: 0x001A9F64
		public sealed override bool IsExtern
		{
			get
			{
				if (!this.IsPartialDefinition)
				{
					return base.HasExternModifier;
				}
				SourceOrdinaryMethodSymbol otherPartOfPartial = this._otherPartOfPartial;
				return otherPartOfPartial != null && otherPartOfPartial.IsExtern;
			}
		}

		// Token: 0x0600727D RID: 29309 RVA: 0x001ABD88 File Offset: 0x001A9F88
		public override string GetDocumentationCommentXml(CultureInfo preferredCulture = null, bool expandIncludes = false, CancellationToken cancellationToken = default(CancellationToken))
		{
			ref string ptr = (ref expandIncludes ? ref this.lazyExpandedDocComment : ref this.lazyDocComment);
			return SourceDocumentationCommentUtils.GetAndCacheDocumentationComment(this.SourcePartialImplementation ?? this, expandIncludes, ref ptr);
		}

		// Token: 0x17001EF9 RID: 7929
		// (get) Token: 0x0600727E RID: 29310 RVA: 0x001ABDB9 File Offset: 0x001A9FB9
		protected override SourceMemberMethodSymbol BoundAttributesSource
		{
			get
			{
				return this.SourcePartialDefinition;
			}
		}

		// Token: 0x0600727F RID: 29311 RVA: 0x001ABDC1 File Offset: 0x001A9FC1
		internal override OneOrMany<SyntaxList<AttributeListSyntax>> GetAttributeDeclarations()
		{
			if (this.SourcePartialImplementation != null)
			{
				return OneOrMany.Create<SyntaxList<AttributeListSyntax>>(ImmutableArray.Create<SyntaxList<AttributeListSyntax>>(this.AttributeDeclarationSyntaxList, this.SourcePartialImplementation.AttributeDeclarationSyntaxList));
			}
			return OneOrMany.Create<SyntaxList<AttributeListSyntax>>(this.AttributeDeclarationSyntaxList);
		}

		// Token: 0x17001EFA RID: 7930
		// (get) Token: 0x06007280 RID: 29312 RVA: 0x001ABDF4 File Offset: 0x001A9FF4
		private SyntaxList<AttributeListSyntax> AttributeDeclarationSyntaxList
		{
			get
			{
				SourceMemberContainerTypeSymbol sourceMemberContainerTypeSymbol = this.ContainingType as SourceMemberContainerTypeSymbol;
				if (sourceMemberContainerTypeSymbol != null && sourceMemberContainerTypeSymbol.AnyMemberHasAttributes)
				{
					return this.GetSyntax().AttributeLists;
				}
				return default(SyntaxList<AttributeListSyntax>);
			}
		}

		// Token: 0x17001EFB RID: 7931
		// (get) Token: 0x06007281 RID: 29313 RVA: 0x001ABE2D File Offset: 0x001AA02D
		internal override bool IsExpressionBodied
		{
			get
			{
				return this._isExpressionBodied;
			}
		}

		// Token: 0x06007282 RID: 29314 RVA: 0x001ABE38 File Offset: 0x001AA038
		protected override DeclarationModifiers MakeDeclarationModifiers(DeclarationModifiers allowedModifiers, BindingDiagnosticBag diagnostics)
		{
			bool flag;
			return ModifierUtils.MakeAndCheckNontypeMemberModifiers(this.GetSyntax().Modifiers, DeclarationModifiers.None, allowedModifiers, this.Locations[0], diagnostics, out flag);
		}

		// Token: 0x06007283 RID: 29315 RVA: 0x001ABE6C File Offset: 0x001AA06C
		private ImmutableArray<TypeParameterSymbol> MakeTypeParameters(MethodDeclarationSyntax syntax, BindingDiagnosticBag diagnostics)
		{
			OverriddenMethodTypeParameterMapBase overriddenMethodTypeParameterMapBase = null;
			if (this.IsOverride)
			{
				overriddenMethodTypeParameterMapBase = new OverriddenMethodTypeParameterMap(this);
			}
			else if (this.IsExplicitInterfaceImplementation)
			{
				overriddenMethodTypeParameterMapBase = new ExplicitInterfaceMethodTypeParameterMap(this);
			}
			SeparatedSyntaxList<TypeParameterSyntax> parameters = syntax.TypeParameterList.Parameters;
			ArrayBuilder<TypeParameterSymbol> instance = ArrayBuilder<TypeParameterSymbol>.GetInstance();
			for (int i = 0; i < parameters.Count; i++)
			{
				TypeParameterSyntax typeParameterSyntax = parameters[i];
				if (typeParameterSyntax.VarianceKeyword.Kind() != SyntaxKind.None)
				{
					diagnostics.Add(ErrorCode.ERR_IllegalVarianceSyntax, typeParameterSyntax.VarianceKeyword.GetLocation());
				}
				SyntaxToken identifier = typeParameterSyntax.Identifier;
				Location location = identifier.GetLocation();
				string valueText = identifier.ValueText;
				for (int j = 0; j < instance.Count; j++)
				{
					if (valueText == instance[j].Name)
					{
						diagnostics.Add(ErrorCode.ERR_DuplicateTypeParameter, location, new object[] { valueText });
						break;
					}
				}
				SourceMemberContainerTypeSymbol.ReportTypeNamedRecord(identifier.Text, this.DeclaringCompilation, diagnostics.DiagnosticBag, location);
				TypeParameterSymbol typeParameterSymbol = this.ContainingType.FindEnclosingTypeParameter(valueText);
				if (typeParameterSymbol != null)
				{
					diagnostics.Add(ErrorCode.WRN_TypeParameterSameAsOuterTypeParameter, location, new object[] { valueText, typeParameterSymbol.ContainingType });
				}
				ImmutableArray<SyntaxReference> immutableArray = ImmutableArray.Create<SyntaxReference>(typeParameterSyntax.GetReference());
				ImmutableArray<Location> immutableArray2 = ImmutableArray.Create<Location>(location);
				TypeParameterSymbol typeParameterSymbol2 = ((overriddenMethodTypeParameterMapBase != null) ? new SourceOverridingMethodTypeParameterSymbol(overriddenMethodTypeParameterMapBase, valueText, i, immutableArray2, immutableArray) : new SourceMethodTypeParameterSymbol(this, valueText, i, immutableArray2, immutableArray));
				instance.Add(typeParameterSymbol2);
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06007284 RID: 29316 RVA: 0x001ABFF0 File Offset: 0x001AA1F0
		internal override void ForceComplete(SourceLocation locationOpt, CancellationToken cancellationToken)
		{
			SourceOrdinaryMethodSymbol sourcePartialImplementation = this.SourcePartialImplementation;
			if (sourcePartialImplementation != null)
			{
				sourcePartialImplementation.ForceComplete(locationOpt, cancellationToken);
			}
			base.ForceComplete(locationOpt, cancellationToken);
		}

		// Token: 0x06007285 RID: 29317 RVA: 0x001AC017 File Offset: 0x001AA217
		internal override bool IsDefinedInSourceTree(SyntaxTree tree, TextSpan? definedWithinSpan, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!base.IsDefinedInSourceTree(tree, definedWithinSpan, cancellationToken))
			{
				SourceOrdinaryMethodSymbol sourcePartialImplementation = this.SourcePartialImplementation;
				return sourcePartialImplementation != null && sourcePartialImplementation.IsDefinedInSourceTree(tree, definedWithinSpan, cancellationToken);
			}
			return true;
		}

		// Token: 0x06007286 RID: 29318 RVA: 0x001AC03C File Offset: 0x001AA23C
		protected override void CheckConstraintsForExplicitInterfaceType(ConversionsBase conversions, BindingDiagnosticBag diagnostics)
		{
			if (this._explicitInterfaceType != null)
			{
				MethodDeclarationSyntax syntax = this.GetSyntax();
				this._explicitInterfaceType.CheckAllConstraints(this.DeclaringCompilation, conversions, new SourceLocation(syntax.ExplicitInterfaceSpecifier.Name), diagnostics);
			}
		}

		// Token: 0x06007287 RID: 29319 RVA: 0x001AC07C File Offset: 0x001AA27C
		protected override void PartialMethodChecks(BindingDiagnosticBag diagnostics)
		{
			SourceOrdinaryMethodSymbol sourcePartialImplementation = this.SourcePartialImplementation;
			if (sourcePartialImplementation != null)
			{
				SourceOrdinaryMethodSymbol.PartialMethodChecks(this, sourcePartialImplementation, diagnostics);
			}
		}

		// Token: 0x06007288 RID: 29320 RVA: 0x001AC09C File Offset: 0x001AA29C
		private static void PartialMethodChecks(SourceOrdinaryMethodSymbol definition, SourceOrdinaryMethodSymbol implementation, BindingDiagnosticBag diagnostics)
		{
			MethodSymbol methodSymbol = definition.ConstructIfGeneric(TypeMap.TypeParametersAsTypeSymbolsWithIgnoredAnnotations(implementation.TypeParameters));
			bool flag = methodSymbol.ReturnTypeWithAnnotations.Equals(implementation.ReturnTypeWithAnnotations, TypeCompareKind.AllIgnoreOptions);
			if (!flag && !SourceMemberContainerTypeSymbol.IsOrContainsErrorType(implementation.ReturnType) && !SourceMemberContainerTypeSymbol.IsOrContainsErrorType(definition.ReturnType))
			{
				diagnostics.Add(ErrorCode.ERR_PartialMethodReturnTypeDifference, implementation.Locations[0]);
			}
			if (definition.RefKind != implementation.RefKind)
			{
				diagnostics.Add(ErrorCode.ERR_PartialMethodRefReturnDifference, implementation.Locations[0]);
			}
			if (definition.IsStatic != implementation.IsStatic)
			{
				diagnostics.Add(ErrorCode.ERR_PartialMethodStaticDifference, implementation.Locations[0]);
			}
			if (definition.IsDeclaredReadOnly != implementation.IsDeclaredReadOnly)
			{
				diagnostics.Add(ErrorCode.ERR_PartialMethodReadOnlyDifference, implementation.Locations[0]);
			}
			if (definition.IsExtensionMethod != implementation.IsExtensionMethod)
			{
				diagnostics.Add(ErrorCode.ERR_PartialMethodExtensionDifference, implementation.Locations[0]);
			}
			if (definition.IsUnsafe != implementation.IsUnsafe && definition.CompilationAllowsUnsafe())
			{
				diagnostics.Add(ErrorCode.ERR_PartialMethodUnsafeDifference, implementation.Locations[0]);
			}
			if (definition.IsParams() != implementation.IsParams())
			{
				diagnostics.Add(ErrorCode.ERR_PartialMethodParamsDifference, implementation.Locations[0]);
			}
			if (definition.HasExplicitAccessModifier != implementation.HasExplicitAccessModifier || definition.DeclaredAccessibility != implementation.DeclaredAccessibility)
			{
				diagnostics.Add(ErrorCode.ERR_PartialMethodAccessibilityDifference, implementation.Locations[0]);
			}
			if (definition.IsVirtual != implementation.IsVirtual || definition.IsOverride != implementation.IsOverride || definition.IsSealed != implementation.IsSealed || definition.IsNew != implementation.IsNew)
			{
				diagnostics.Add(ErrorCode.ERR_PartialMethodExtendedModDifference, implementation.Locations[0]);
			}
			SourceOrdinaryMethodSymbol.PartialMethodConstraintsChecks(definition, implementation, diagnostics);
			SourceMemberContainerTypeSymbol.CheckValidNullableMethodOverride<bool>(implementation.DeclaringCompilation, methodSymbol, implementation, diagnostics, delegate(BindingDiagnosticBag diagnostics, MethodSymbol implementedMethod, MethodSymbol implementingMethod, bool topLevel, bool returnTypesEqual)
			{
				if (returnTypesEqual)
				{
					diagnostics.Add(ErrorCode.WRN_NullabilityMismatchInReturnTypeOnPartial, implementingMethod.Locations[0]);
				}
			}, delegate(BindingDiagnosticBag diagnostics, MethodSymbol implementedMethod, MethodSymbol implementingMethod, ParameterSymbol implementingParameter, bool blameAttributes, bool arg)
			{
				diagnostics.Add(ErrorCode.WRN_NullabilityMismatchInParameterTypeOnPartial, implementingMethod.Locations[0], new object[]
				{
					new FormattedSymbol(implementingParameter, SymbolDisplayFormat.ShortFormat)
				});
			}, flag, false);
		}

		// Token: 0x06007289 RID: 29321 RVA: 0x001AC2E8 File Offset: 0x001AA4E8
		private static void PartialMethodConstraintsChecks(SourceOrdinaryMethodSymbol definition, SourceOrdinaryMethodSymbol implementation, BindingDiagnosticBag diagnostics)
		{
			ImmutableArray<TypeParameterSymbol> typeParameters = definition.TypeParameters;
			int length = typeParameters.Length;
			if (length == 0)
			{
				return;
			}
			ImmutableArray<TypeParameterSymbol> typeParameters2 = implementation.TypeParameters;
			ImmutableArray<TypeWithAnnotations> immutableArray = IndexedTypeParameterSymbol.Take(length);
			TypeMap typeMap = new TypeMap(typeParameters, immutableArray, true);
			TypeMap typeMap2 = new TypeMap(typeParameters2, immutableArray, true);
			for (int i = 0; i < length; i++)
			{
				TypeParameterSymbol typeParameterSymbol = typeParameters[i];
				TypeParameterSymbol typeParameterSymbol2 = typeParameters2[i];
				if (!MemberSignatureComparer.HaveSameConstraints(typeParameterSymbol, typeMap, typeParameterSymbol2, typeMap2))
				{
					diagnostics.Add(ErrorCode.ERR_PartialMethodInconsistentConstraints, implementation.Locations[0], new object[] { implementation, typeParameterSymbol2.Name });
				}
				else if (!MemberSignatureComparer.HaveSameNullabilityInConstraints(typeParameterSymbol, typeMap, typeParameterSymbol2, typeMap2))
				{
					diagnostics.Add(ErrorCode.WRN_NullabilityMismatchInConstraintsOnPartialImplementation, implementation.Locations[0], new object[] { implementation, typeParameterSymbol2.Name });
				}
			}
		}

		// Token: 0x0600728A RID: 29322 RVA: 0x001AC3D6 File Offset: 0x001AA5D6
		internal override bool CallsAreOmitted(SyntaxTree syntaxTree)
		{
			return this.IsPartialWithoutImplementation || base.CallsAreOmitted(syntaxTree);
		}

		// Token: 0x17001EFC RID: 7932
		// (get) Token: 0x0600728B RID: 29323 RVA: 0x001AC3E9 File Offset: 0x001AA5E9
		internal override bool GenerateDebugInfo
		{
			get
			{
				return !this.IsAsync && !this.IsIterator;
			}
		}

		// Token: 0x0600728C RID: 29324 RVA: 0x001AC3FE File Offset: 0x001AA5FE
		[CompilerGenerated]
		internal static void <MakeParametersAndBindReturnType>g__forceMethodTypeParameters|11_0(TypeWithAnnotations type, SourceOrdinaryMethodSymbol method, ImmutableArray<TypeParameterConstraintClause> declaredConstraints)
		{
			type.VisitType(null, delegate(TypeWithAnnotations type, [TupleElementNames(new string[] { "method", "declaredConstraints" })] ValueTuple<SourceOrdinaryMethodSymbol, ImmutableArray<TypeParameterConstraintClause>> args, bool unused2)
			{
				TypeParameterSymbol typeParameterSymbol = type.DefaultType as TypeParameterSymbol;
				if (typeParameterSymbol != null && typeParameterSymbol.DeclaringMethod == args.Item1)
				{
					bool flag = args.Item2.IsDefault || (args.Item2[typeParameterSymbol.Ordinal].Constraints & (TypeParameterConstraintKind.ReferenceType | TypeParameterConstraintKind.Default)) == TypeParameterConstraintKind.None;
					type.TryForceResolve(flag);
				}
				return false;
			}, null, new ValueTuple<SourceOrdinaryMethodSymbol, ImmutableArray<TypeParameterConstraintClause>>(method, declaredConstraints), false, true);
		}

		// Token: 0x04001BDA RID: 7130
		private readonly TypeSymbol _explicitInterfaceType;

		// Token: 0x04001BDB RID: 7131
		private readonly bool _isExpressionBodied;

		// Token: 0x04001BDC RID: 7132
		private readonly bool _hasAnyBody;

		// Token: 0x04001BDD RID: 7133
		private readonly RefKind _refKind;

		// Token: 0x04001BDE RID: 7134
		private bool _lazyIsVararg;

		// Token: 0x04001BDF RID: 7135
		private ImmutableArray<ImmutableArray<TypeWithAnnotations>> _lazyTypeParameterConstraintTypes;

		// Token: 0x04001BE0 RID: 7136
		private ImmutableArray<TypeParameterConstraintKind> _lazyTypeParameterConstraintKinds;

		// Token: 0x04001BE1 RID: 7137
		private SourceOrdinaryMethodSymbol _otherPartOfPartial;
	}
}
