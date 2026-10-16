using System;
using System.Collections.Generic;
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
	// Token: 0x0200054C RID: 1356
	internal class SourcePropertyAccessorSymbol : SourceMemberMethodSymbol
	{
		// Token: 0x060072D1 RID: 29393 RVA: 0x001AD4D0 File Offset: 0x001AB6D0
		public static SourcePropertyAccessorSymbol CreateAccessorSymbol(NamedTypeSymbol containingType, SourcePropertySymbol property, DeclarationModifiers propertyModifiers, AccessorDeclarationSyntax syntax, bool isAutoPropertyAccessor, BindingDiagnosticBag diagnostics)
		{
			MethodKind methodKind = ((syntax.Kind() == SyntaxKind.GetAccessorDeclaration) ? MethodKind.PropertyGet : MethodKind.PropertySet);
			bool flag = syntax.Body != null;
			bool flag2 = syntax.ExpressionBody != null;
			bool flag3 = containingType.DeclaringCompilation.IsNullableAnalysisEnabledIn(syntax);
			Symbol.CheckForBlockAndExpressionBody(syntax.Body, syntax.ExpressionBody, syntax, diagnostics);
			return new SourcePropertyAccessorSymbol(containingType, property, propertyModifiers, syntax.Keyword.GetLocation(), syntax, flag, flag2, SyntaxFacts.HasYieldOperations(syntax.Body), syntax.Modifiers, methodKind, syntax.Keyword.IsKind(SyntaxKind.InitKeyword), isAutoPropertyAccessor, flag3, diagnostics);
		}

		// Token: 0x060072D2 RID: 29394 RVA: 0x001AD56C File Offset: 0x001AB76C
		public static SourcePropertyAccessorSymbol CreateAccessorSymbol(NamedTypeSymbol containingType, SourcePropertySymbol property, DeclarationModifiers propertyModifiers, ArrowExpressionClauseSyntax syntax, BindingDiagnosticBag diagnostics)
		{
			bool flag = containingType.DeclaringCompilation.IsNullableAnalysisEnabledIn(syntax);
			return new SourcePropertyAccessorSymbol(containingType, property, propertyModifiers, syntax.Expression.GetLocation(), syntax, flag, diagnostics);
		}

		// Token: 0x060072D3 RID: 29395 RVA: 0x001AD5A0 File Offset: 0x001AB7A0
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public static SourcePropertyAccessorSymbol CreateAccessorSymbol(bool isGetMethod, bool usesInit, NamedTypeSymbol containingType, SynthesizedRecordPropertySymbol property, DeclarationModifiers propertyModifiers, Location location, CSharpSyntaxNode syntax, BindingDiagnosticBag diagnostics)
		{
			MethodKind methodKind = (isGetMethod ? MethodKind.PropertyGet : MethodKind.PropertySet);
			return new SourcePropertyAccessorSymbol(containingType, property, propertyModifiers, location, syntax, false, false, false, default(SyntaxTokenList), methodKind, usesInit, true, false, diagnostics);
		}

		// Token: 0x060072D4 RID: 29396 RVA: 0x001AD5D6 File Offset: 0x001AB7D6
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public static SourcePropertyAccessorSymbol CreateAccessorSymbol(NamedTypeSymbol containingType, SynthesizedRecordEqualityContractProperty property, DeclarationModifiers propertyModifiers, Location location, CSharpSyntaxNode syntax, BindingDiagnosticBag diagnostics)
		{
			return new SynthesizedRecordEqualityContractProperty.GetAccessorSymbol(containingType, property, propertyModifiers, location, syntax, diagnostics);
		}

		// Token: 0x17001F1F RID: 7967
		// (get) Token: 0x060072D5 RID: 29397 RVA: 0x001AD5E5 File Offset: 0x001AB7E5
		internal sealed override bool IsExpressionBodied
		{
			get
			{
				return this._isExpressionBodied;
			}
		}

		// Token: 0x17001F20 RID: 7968
		// (get) Token: 0x060072D6 RID: 29398 RVA: 0x001AD5ED File Offset: 0x001AB7ED
		internal sealed override ImmutableArray<string> NotNullMembers
		{
			get
			{
				return this._property.NotNullMembers.Concat<string>(base.NotNullMembers);
			}
		}

		// Token: 0x17001F21 RID: 7969
		// (get) Token: 0x060072D7 RID: 29399 RVA: 0x001AD605 File Offset: 0x001AB805
		internal sealed override ImmutableArray<string> NotNullWhenTrueMembers
		{
			get
			{
				return this._property.NotNullWhenTrueMembers.Concat<string>(base.NotNullWhenTrueMembers);
			}
		}

		// Token: 0x17001F22 RID: 7970
		// (get) Token: 0x060072D8 RID: 29400 RVA: 0x001AD61D File Offset: 0x001AB81D
		internal sealed override ImmutableArray<string> NotNullWhenFalseMembers
		{
			get
			{
				return this._property.NotNullWhenFalseMembers.Concat<string>(base.NotNullWhenFalseMembers);
			}
		}

		// Token: 0x060072D9 RID: 29401 RVA: 0x001AD638 File Offset: 0x001AB838
		private SourcePropertyAccessorSymbol(NamedTypeSymbol containingType, SourcePropertySymbol property, DeclarationModifiers propertyModifiers, Location location, ArrowExpressionClauseSyntax syntax, bool isNullableAnalysisEnabled, BindingDiagnosticBag diagnostics)
			: base(containingType, syntax.GetReference(), location, false)
		{
			this._property = property;
			this._isAutoPropertyAccessor = false;
			this._isExpressionBodied = true;
			DeclarationModifiers accessorModifiers = SourcePropertyAccessorSymbol.GetAccessorModifiers(propertyModifiers);
			base.MakeFlags(MethodKind.PropertyGet, accessorModifiers, false, false, isNullableAnalysisEnabled, property.IsExplicitInterfaceImplementation);
			base.CheckFeatureAvailabilityAndRuntimeSupport(syntax, location, true, diagnostics);
			base.CheckModifiersForBody(location, diagnostics);
			CSDiagnosticInfo csdiagnosticInfo = ModifierUtils.CheckAccessibility(this.DeclarationModifiers, this, property.IsExplicitInterfaceImplementation);
			if (csdiagnosticInfo != null)
			{
				diagnostics.Add(csdiagnosticInfo, location);
			}
			this.CheckModifiers(location, true, true, diagnostics);
		}

		// Token: 0x060072DA RID: 29402 RVA: 0x001AD6C8 File Offset: 0x001AB8C8
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		protected SourcePropertyAccessorSymbol(NamedTypeSymbol containingType, SourcePropertySymbolBase property, DeclarationModifiers propertyModifiers, Location location, CSharpSyntaxNode syntax, bool hasBody, bool hasExpressionBody, bool isIterator, SyntaxTokenList modifiers, MethodKind methodKind, bool usesInit, bool isAutoPropertyAccessor, bool isNullableAnalysisEnabled, BindingDiagnosticBag diagnostics)
			: base(containingType, syntax.GetReference(), location, isIterator)
		{
			this._property = property;
			this._isAutoPropertyAccessor = isAutoPropertyAccessor;
			this._isExpressionBodied = !hasBody && hasExpressionBody;
			this._usesInit = usesInit;
			if (this._usesInit)
			{
				Binder.CheckFeatureAvailability(syntax, MessageID.IDS_FeatureInitOnlySetters, diagnostics, location);
			}
			bool flag;
			DeclarationModifiers declarationModifiers = this.MakeModifiers(modifiers, property.IsExplicitInterfaceImplementation, hasBody || hasExpressionBody, location, diagnostics, out flag);
			declarationModifiers |= SourcePropertyAccessorSymbol.GetAccessorModifiers(propertyModifiers) & ~(DeclarationModifiers.Public | DeclarationModifiers.Protected | DeclarationModifiers.Internal | DeclarationModifiers.ProtectedInternal | DeclarationModifiers.Private | DeclarationModifiers.PrivateProtected);
			if ((declarationModifiers & DeclarationModifiers.Private) != DeclarationModifiers.None)
			{
				declarationModifiers &= ~DeclarationModifiers.Virtual;
			}
			base.MakeFlags(methodKind, declarationModifiers, false, false, isNullableAnalysisEnabled, property.IsExplicitInterfaceImplementation);
			base.CheckFeatureAvailabilityAndRuntimeSupport(syntax, location, hasBody || hasExpressionBody || isAutoPropertyAccessor, diagnostics);
			if (hasBody || hasExpressionBody)
			{
				base.CheckModifiersForBody(location, diagnostics);
			}
			CSDiagnosticInfo csdiagnosticInfo = ModifierUtils.CheckAccessibility(this.DeclarationModifiers, this, property.IsExplicitInterfaceImplementation);
			if (csdiagnosticInfo != null)
			{
				diagnostics.Add(csdiagnosticInfo, location);
			}
			if (!flag)
			{
				this.CheckModifiers(location, hasBody || hasExpressionBody, isAutoPropertyAccessor, diagnostics);
			}
		}

		// Token: 0x060072DB RID: 29403 RVA: 0x001AD7CB File Offset: 0x001AB9CB
		private static DeclarationModifiers GetAccessorModifiers(DeclarationModifiers propertyModifiers)
		{
			return propertyModifiers & ~(DeclarationModifiers.ReadOnly | DeclarationModifiers.Indexer);
		}

		// Token: 0x060072DC RID: 29404 RVA: 0x001AD7D4 File Offset: 0x001AB9D4
		protected sealed override void MethodChecks(BindingDiagnosticBag diagnostics)
		{
			this._lazyParameters = this.ComputeParameters(diagnostics);
			this._lazyReturnType = this.ComputeReturnType(diagnostics);
			this._lazyRefCustomModifiers = ImmutableArray<CustomModifier>.Empty;
			ImmutableArray<MethodSymbol> explicitInterfaceImplementations = this.ExplicitInterfaceImplementations;
			if (explicitInterfaceImplementations.Length > 0)
			{
				CustomModifierUtils.CopyMethodCustomModifiers(explicitInterfaceImplementations[0], this, out this._lazyReturnType, out this._lazyRefCustomModifiers, out this._lazyParameters, false);
				return;
			}
			if (this.IsOverride)
			{
				MethodSymbol overriddenMethod = base.OverriddenMethod;
				if (overriddenMethod != null)
				{
					CustomModifierUtils.CopyMethodCustomModifiers(overriddenMethod, this, out this._lazyReturnType, out this._lazyRefCustomModifiers, out this._lazyParameters, true);
					return;
				}
			}
			else if (!this._lazyReturnType.IsVoidType())
			{
				PropertySymbol property = this._property;
				TypeWithAnnotations typeWithAnnotations = property.TypeWithAnnotations;
				this._lazyReturnType = this._lazyReturnType.WithTypeAndModifiers(CustomModifierUtils.CopyTypeCustomModifiers(typeWithAnnotations.Type, this._lazyReturnType.Type, this.ContainingAssembly), typeWithAnnotations.CustomModifiers);
				this._lazyRefCustomModifiers = property.RefCustomModifiers;
			}
		}

		// Token: 0x17001F23 RID: 7971
		// (get) Token: 0x060072DD RID: 29405 RVA: 0x001AD8C4 File Offset: 0x001ABAC4
		public sealed override Accessibility DeclaredAccessibility
		{
			get
			{
				Accessibility localAccessibility = this.LocalAccessibility;
				if (localAccessibility != Accessibility.NotApplicable)
				{
					return localAccessibility;
				}
				return this._property.DeclaredAccessibility;
			}
		}

		// Token: 0x17001F24 RID: 7972
		// (get) Token: 0x060072DE RID: 29406 RVA: 0x001AD8E8 File Offset: 0x001ABAE8
		public sealed override Symbol AssociatedSymbol
		{
			get
			{
				return this._property;
			}
		}

		// Token: 0x17001F25 RID: 7973
		// (get) Token: 0x060072DF RID: 29407 RVA: 0x001AD8F0 File Offset: 0x001ABAF0
		public sealed override bool IsVararg
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001F26 RID: 7974
		// (get) Token: 0x060072E0 RID: 29408 RVA: 0x001AD8F3 File Offset: 0x001ABAF3
		public sealed override bool ReturnsVoid
		{
			get
			{
				return base.ReturnType.IsVoidType();
			}
		}

		// Token: 0x17001F27 RID: 7975
		// (get) Token: 0x060072E1 RID: 29409 RVA: 0x001AD900 File Offset: 0x001ABB00
		public sealed override ImmutableArray<ParameterSymbol> Parameters
		{
			get
			{
				base.LazyMethodChecks();
				return this._lazyParameters;
			}
		}

		// Token: 0x17001F28 RID: 7976
		// (get) Token: 0x060072E2 RID: 29410 RVA: 0x001AD90E File Offset: 0x001ABB0E
		public sealed override ImmutableArray<TypeParameterSymbol> TypeParameters
		{
			get
			{
				return ImmutableArray<TypeParameterSymbol>.Empty;
			}
		}

		// Token: 0x060072E3 RID: 29411 RVA: 0x001AD915 File Offset: 0x001ABB15
		public sealed override ImmutableArray<ImmutableArray<TypeWithAnnotations>> GetTypeParameterConstraintTypes()
		{
			return ImmutableArray<ImmutableArray<TypeWithAnnotations>>.Empty;
		}

		// Token: 0x060072E4 RID: 29412 RVA: 0x001AD91C File Offset: 0x001ABB1C
		public sealed override ImmutableArray<TypeParameterConstraintKind> GetTypeParameterConstraintKinds()
		{
			return ImmutableArray<TypeParameterConstraintKind>.Empty;
		}

		// Token: 0x17001F29 RID: 7977
		// (get) Token: 0x060072E5 RID: 29413 RVA: 0x001AD923 File Offset: 0x001ABB23
		public sealed override RefKind RefKind
		{
			get
			{
				return this._property.RefKind;
			}
		}

		// Token: 0x17001F2A RID: 7978
		// (get) Token: 0x060072E6 RID: 29414 RVA: 0x001AD930 File Offset: 0x001ABB30
		public sealed override TypeWithAnnotations ReturnTypeWithAnnotations
		{
			get
			{
				base.LazyMethodChecks();
				return this._lazyReturnType;
			}
		}

		// Token: 0x17001F2B RID: 7979
		// (get) Token: 0x060072E7 RID: 29415 RVA: 0x001AD940 File Offset: 0x001ABB40
		public sealed override FlowAnalysisAnnotations ReturnTypeFlowAnalysisAnnotations
		{
			get
			{
				if (this.MethodKind == MethodKind.PropertySet)
				{
					return FlowAnalysisAnnotations.None;
				}
				FlowAnalysisAnnotations flowAnalysisAnnotations = FlowAnalysisAnnotations.None;
				if (this._property.HasMaybeNull)
				{
					flowAnalysisAnnotations |= FlowAnalysisAnnotations.MaybeNull;
				}
				if (this._property.HasNotNull)
				{
					flowAnalysisAnnotations |= FlowAnalysisAnnotations.NotNull;
				}
				return flowAnalysisAnnotations;
			}
		}

		// Token: 0x17001F2C RID: 7980
		// (get) Token: 0x060072E8 RID: 29416 RVA: 0x001AD980 File Offset: 0x001ABB80
		public sealed override ImmutableHashSet<string> ReturnNotNullIfParameterNotNull
		{
			get
			{
				return ImmutableHashSet<string>.Empty;
			}
		}

		// Token: 0x060072E9 RID: 29417 RVA: 0x001AD988 File Offset: 0x001ABB88
		private TypeWithAnnotations ComputeReturnType(BindingDiagnosticBag diagnostics)
		{
			if (this.MethodKind == MethodKind.PropertyGet)
			{
				TypeWithAnnotations typeWithAnnotations = this._property.TypeWithAnnotations;
				if (typeWithAnnotations.Type.IsStatic)
				{
					diagnostics.Add(ErrorFacts.GetStaticClassReturnCode(this.ContainingType.IsInterfaceType()), this.locations[0], new object[] { typeWithAnnotations.Type });
				}
				return typeWithAnnotations;
			}
			TypeWithAnnotations typeWithAnnotations2 = TypeWithAnnotations.Create(this.GetBinder().GetSpecialType(SpecialType.System_Void, diagnostics, this.GetSyntax()), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
			if (this.IsInitOnly)
			{
				ImmutableArray<CustomModifier> immutableArray = ImmutableArray.Create<CustomModifier>(CSharpCustomModifier.CreateRequired(Binder.GetWellKnownType(this.DeclaringCompilation, WellKnownType.System_Runtime_CompilerServices_IsExternalInit, diagnostics, this.locations[0])));
				typeWithAnnotations2 = typeWithAnnotations2.WithModifiers(immutableArray);
			}
			return typeWithAnnotations2;
		}

		// Token: 0x060072EA RID: 29418 RVA: 0x001ADA4C File Offset: 0x001ABC4C
		private Binder GetBinder()
		{
			CSharpSyntaxNode syntax = this.GetSyntax();
			return this.DeclaringCompilation.GetBinderFactory(syntax.SyntaxTree, false).GetBinder(syntax, null, null);
		}

		// Token: 0x17001F2D RID: 7981
		// (get) Token: 0x060072EB RID: 29419 RVA: 0x001ADA7A File Offset: 0x001ABC7A
		public sealed override ImmutableArray<CustomModifier> RefCustomModifiers
		{
			get
			{
				base.LazyMethodChecks();
				return this._lazyRefCustomModifiers;
			}
		}

		// Token: 0x17001F2E RID: 7982
		// (get) Token: 0x060072EC RID: 29420 RVA: 0x001ADA88 File Offset: 0x001ABC88
		internal Accessibility LocalAccessibility
		{
			get
			{
				return ModifierUtils.EffectiveAccessibility(this.DeclarationModifiers);
			}
		}

		// Token: 0x17001F2F RID: 7983
		// (get) Token: 0x060072ED RID: 29421 RVA: 0x001ADA95 File Offset: 0x001ABC95
		internal bool LocalDeclaredReadOnly
		{
			get
			{
				return (this.DeclarationModifiers & DeclarationModifiers.ReadOnly) > DeclarationModifiers.None;
			}
		}

		// Token: 0x17001F30 RID: 7984
		// (get) Token: 0x060072EE RID: 29422 RVA: 0x001ADAA8 File Offset: 0x001ABCA8
		internal sealed override bool IsDeclaredReadOnly
		{
			get
			{
				return this.LocalDeclaredReadOnly || (this._property.HasReadOnlyModifier && base.IsValidReadOnlyTarget) || (((CSharpParseOptions)base.SyntaxTree.Options).IsFeatureEnabled(MessageID.IDS_FeatureReadOnlyMembers) && (this.DeclaringCompilation.GetWellKnownTypeMember(WellKnownMember.System_Runtime_CompilerServices_IsReadOnlyAttribute__ctor) != null || (this.DeclaringCompilation.Options.OutputKind != OutputKind.NetModule && this.DeclaringCompilation.GetWellKnownType(WellKnownType.System_Runtime_CompilerServices_IsReadOnlyAttribute) is MissingMetadataTypeSymbol)) && (this.ContainingType.IsStructType() && !this._property.IsStatic && this._isAutoPropertyAccessor) && this.MethodKind == MethodKind.PropertyGet);
			}
		}

		// Token: 0x17001F31 RID: 7985
		// (get) Token: 0x060072EF RID: 29423 RVA: 0x001ADB6D File Offset: 0x001ABD6D
		internal sealed override bool IsInitOnly
		{
			get
			{
				return !this.IsStatic && this._usesInit;
			}
		}

		// Token: 0x060072F0 RID: 29424 RVA: 0x001ADB80 File Offset: 0x001ABD80
		private DeclarationModifiers MakeModifiers(SyntaxTokenList modifiers, bool isExplicitInterfaceImplementation, bool hasBody, Location location, BindingDiagnosticBag diagnostics, out bool modifierErrors)
		{
			DeclarationModifiers declarationModifiers = (isExplicitInterfaceImplementation ? DeclarationModifiers.None : DeclarationModifiers.AccessibilityMask);
			if (this.ContainingType.IsStructType())
			{
				declarationModifiers |= DeclarationModifiers.ReadOnly;
			}
			DeclarationModifiers declarationModifiers2 = DeclarationModifiers.None;
			if (this.ContainingType.IsInterface && !isExplicitInterfaceImplementation)
			{
				declarationModifiers2 = DeclarationModifiers.AccessibilityMask;
			}
			DeclarationModifiers declarationModifiers3 = ModifierUtils.MakeAndCheckNontypeMemberModifiers(modifiers, DeclarationModifiers.None, declarationModifiers, location, diagnostics, out modifierErrors);
			ModifierUtils.ReportDefaultInterfaceImplementationModifiers(hasBody, declarationModifiers3, declarationModifiers2, location, diagnostics);
			return declarationModifiers3;
		}

		// Token: 0x060072F1 RID: 29425 RVA: 0x001ADBE4 File Offset: 0x001ABDE4
		private void CheckModifiers(Location location, bool hasBody, bool isAutoPropertyOrExpressionBodied, BindingDiagnosticBag diagnostics)
		{
			Accessibility localAccessibility = this.LocalAccessibility;
			if (this.IsAbstract && !this.ContainingType.IsAbstract && (this.ContainingType.TypeKind == TypeKind.Class || this.ContainingType.TypeKind == TypeKind.Submission))
			{
				diagnostics.Add(ErrorCode.ERR_AbstractInConcreteClass, location, new object[] { this, this.ContainingType });
				return;
			}
			if (this.IsVirtual && this.ContainingType.IsSealed && this.ContainingType.TypeKind != TypeKind.Struct)
			{
				diagnostics.Add(ErrorCode.ERR_NewVirtualInSealed, location, new object[] { this, this.ContainingType });
				return;
			}
			if (!hasBody && !this.IsExtern && !this.IsAbstract && !isAutoPropertyOrExpressionBodied)
			{
				diagnostics.Add(ErrorCode.ERR_ConcreteMissingBody, location, new object[] { this });
				return;
			}
			if (this.ContainingType.IsSealed && localAccessibility.HasProtected() && !this.IsOverride)
			{
				diagnostics.Add(AccessCheck.GetProtectedMemberInSealedTypeError(this.ContainingType), location, new object[] { this });
				return;
			}
			if (this.LocalDeclaredReadOnly && this._property.HasReadOnlyModifier)
			{
				diagnostics.Add(ErrorCode.ERR_InvalidPropertyReadOnlyMods, location, new object[] { this._property });
				return;
			}
			if (this.LocalDeclaredReadOnly && this.IsStatic)
			{
				diagnostics.Add(ErrorCode.ERR_StaticMemberCantBeReadOnly, location, new object[] { this });
				return;
			}
			if (this.LocalDeclaredReadOnly && this.IsInitOnly)
			{
				diagnostics.Add(ErrorCode.ERR_InitCannotBeReadonly, location, new object[] { this._property });
				return;
			}
			if (this.LocalDeclaredReadOnly && this._isAutoPropertyAccessor && this.MethodKind == MethodKind.PropertySet)
			{
				diagnostics.Add(ErrorCode.ERR_AutoSetterCantBeReadOnly, location, new object[] { this });
				return;
			}
			if (this._usesInit && this.IsStatic)
			{
				diagnostics.Add(ErrorCode.ERR_BadInitAccessor, location);
			}
		}

		// Token: 0x060072F2 RID: 29426 RVA: 0x001ADDD8 File Offset: 0x001ABFD8
		internal static string GetAccessorName(string propertyName, bool getNotSet, bool isWinMdOutput)
		{
			return (getNotSet ? "get_" : (isWinMdOutput ? "put_" : "set_")) + propertyName;
		}

		// Token: 0x060072F3 RID: 29427 RVA: 0x001ADDFC File Offset: 0x001ABFFC
		internal CSharpSyntaxNode GetSyntax()
		{
			return (CSharpSyntaxNode)this.syntaxReferenceOpt.GetSyntax(default(CancellationToken));
		}

		// Token: 0x17001F32 RID: 7986
		// (get) Token: 0x060072F4 RID: 29428 RVA: 0x001ADE22 File Offset: 0x001AC022
		internal sealed override bool IsExplicitInterfaceImplementation
		{
			get
			{
				return this._property.IsExplicitInterfaceImplementation;
			}
		}

		// Token: 0x17001F33 RID: 7987
		// (get) Token: 0x060072F5 RID: 29429 RVA: 0x001ADE30 File Offset: 0x001AC030
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public sealed override ImmutableArray<MethodSymbol> ExplicitInterfaceImplementations
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				if (this._lazyExplicitInterfaceImplementations.IsDefault)
				{
					PropertySymbol propertySymbol = (this.IsExplicitInterfaceImplementation ? this._property.ExplicitInterfaceImplementations.FirstOrDefault<PropertySymbol>() : null);
					ImmutableArray<MethodSymbol> immutableArray;
					if (propertySymbol == null)
					{
						immutableArray = ImmutableArray<MethodSymbol>.Empty;
					}
					else
					{
						MethodSymbol methodSymbol = ((this.MethodKind == MethodKind.PropertyGet) ? propertySymbol.GetMethod : propertySymbol.SetMethod);
						immutableArray = ((methodSymbol == null) ? ImmutableArray<MethodSymbol>.Empty : ImmutableArray.Create<MethodSymbol>(methodSymbol));
					}
					ImmutableInterlocked.InterlockedInitialize<MethodSymbol>(ref this._lazyExplicitInterfaceImplementations, immutableArray);
				}
				return this._lazyExplicitInterfaceImplementations;
			}
		}

		// Token: 0x060072F6 RID: 29430 RVA: 0x001ADEB0 File Offset: 0x001AC0B0
		internal sealed override OneOrMany<SyntaxList<AttributeListSyntax>> GetAttributeDeclarations()
		{
			CSharpSyntaxNode syntax = this.GetSyntax();
			SyntaxKind syntaxKind = syntax.Kind();
			if (syntaxKind - SyntaxKind.GetAccessorDeclaration <= 1 || syntaxKind == SyntaxKind.InitAccessorDeclaration)
			{
				return OneOrMany.Create<SyntaxList<AttributeListSyntax>>(((AccessorDeclarationSyntax)syntax).AttributeLists);
			}
			return base.GetAttributeDeclarations();
		}

		// Token: 0x17001F34 RID: 7988
		// (get) Token: 0x060072F7 RID: 29431 RVA: 0x001ADEF4 File Offset: 0x001AC0F4
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public sealed override string Name
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get
			{
				if (this._lazyName == null)
				{
					bool flag = this.MethodKind == MethodKind.PropertyGet;
					string text = null;
					if (this.IsExplicitInterfaceImplementation)
					{
						PropertySymbol propertySymbol = this._property.ExplicitInterfaceImplementations.FirstOrDefault<PropertySymbol>();
						if (propertySymbol != null)
						{
							MethodSymbol methodSymbol = (flag ? propertySymbol.GetMethod : propertySymbol.SetMethod);
							string text2 = ((methodSymbol != null) ? methodSymbol.Name : SourcePropertyAccessorSymbol.GetAccessorName(propertySymbol.MetadataName, flag, this._property.IsCompilationOutputWinMdObj()));
							ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier = this._property.GetExplicitInterfaceSpecifier();
							string text3 = ((explicitInterfaceSpecifier != null) ? explicitInterfaceSpecifier.Name.GetAliasQualifierOpt() : null);
							text = ExplicitInterfaceHelpers.GetMemberName(text2, propertySymbol.ContainingType, text3);
						}
					}
					else if (this.IsOverride)
					{
						MethodSymbol overriddenMethod = base.OverriddenMethod;
						if (overriddenMethod != null)
						{
							text = overriddenMethod.Name;
						}
					}
					if (text == null)
					{
						text = SourcePropertyAccessorSymbol.GetAccessorName(this._property.SourceName, flag, this._property.IsCompilationOutputWinMdObj());
					}
					InterlockedOperations.Initialize<string>(ref this._lazyName, text);
				}
				return this._lazyName;
			}
		}

		// Token: 0x17001F35 RID: 7989
		// (get) Token: 0x060072F8 RID: 29432 RVA: 0x001ADFE8 File Offset: 0x001AC1E8
		public sealed override bool IsImplicitlyDeclared
		{
			get
			{
				SyntaxKind syntaxKind = this.GetSyntax().Kind();
				return syntaxKind - SyntaxKind.GetAccessorDeclaration > 1 && syntaxKind != SyntaxKind.ArrowExpressionClause && syntaxKind != SyntaxKind.InitAccessorDeclaration;
			}
		}

		// Token: 0x17001F36 RID: 7990
		// (get) Token: 0x060072F9 RID: 29433 RVA: 0x001AE01E File Offset: 0x001AC21E
		internal sealed override bool GenerateDebugInfo
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001F37 RID: 7991
		// (get) Token: 0x060072FA RID: 29434 RVA: 0x001AE021 File Offset: 0x001AC221
		public sealed override bool AreLocalsZeroed
		{
			get
			{
				return !this._property.HasSkipLocalsInitAttribute && base.AreLocalsZeroed;
			}
		}

		// Token: 0x060072FB RID: 29435 RVA: 0x001AE038 File Offset: 0x001AC238
		private ImmutableArray<ParameterSymbol> ComputeParameters(BindingDiagnosticBag diagnostics)
		{
			bool flag = this.MethodKind == MethodKind.PropertyGet;
			ImmutableArray<ParameterSymbol> parameters = this._property.Parameters;
			int num = parameters.Length + (flag ? 0 : 1);
			if (num == 0)
			{
				return ImmutableArray<ParameterSymbol>.Empty;
			}
			ArrayBuilder<ParameterSymbol> instance = ArrayBuilder<ParameterSymbol>.GetInstance(num);
			foreach (ParameterSymbol parameterSymbol in parameters)
			{
				SourceParameterSymbol sourceParameterSymbol = (SourceParameterSymbol)parameterSymbol;
				instance.Add(new SourceClonedParameterSymbol(sourceParameterSymbol, this, sourceParameterSymbol.Ordinal, false));
			}
			if (!flag)
			{
				TypeWithAnnotations typeWithAnnotations = this._property.TypeWithAnnotations;
				if (typeWithAnnotations.IsStatic)
				{
					diagnostics.Add(ErrorFacts.GetStaticClassParameterCode(this.ContainingType.IsInterfaceType()), this.locations[0], new object[] { typeWithAnnotations.Type });
				}
				instance.Add(new SynthesizedAccessorValueParameterSymbol(this, typeWithAnnotations, instance.Count));
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x060072FC RID: 29436 RVA: 0x001AE11C File Offset: 0x001AC31C
		internal sealed override void AddSynthesizedReturnTypeAttributes(PEModuleBuilder moduleBuilder, ref ArrayBuilder<SynthesizedAttributeData> attributes)
		{
			base.AddSynthesizedReturnTypeAttributes(moduleBuilder, ref attributes);
			FlowAnalysisAnnotations returnTypeFlowAnalysisAnnotations = this.ReturnTypeFlowAnalysisAnnotations;
			if ((returnTypeFlowAnalysisAnnotations & FlowAnalysisAnnotations.MaybeNull) != FlowAnalysisAnnotations.None)
			{
				Symbol.AddSynthesizedAttribute(ref attributes, new SynthesizedAttributeData(this._property.MaybeNullAttributeIfExists));
			}
			if ((returnTypeFlowAnalysisAnnotations & FlowAnalysisAnnotations.NotNull) != FlowAnalysisAnnotations.None)
			{
				Symbol.AddSynthesizedAttribute(ref attributes, new SynthesizedAttributeData(this._property.NotNullAttributeIfExists));
			}
		}

		// Token: 0x060072FD RID: 29437 RVA: 0x001AE170 File Offset: 0x001AC370
		internal sealed override void AddSynthesizedAttributes(PEModuleBuilder moduleBuilder, ref ArrayBuilder<SynthesizedAttributeData> attributes)
		{
			base.AddSynthesizedAttributes(moduleBuilder, ref attributes);
			if (this._isAutoPropertyAccessor)
			{
				CSharpCompilation declaringCompilation = this.DeclaringCompilation;
				Symbol.AddSynthesizedAttribute(ref attributes, declaringCompilation.TrySynthesizeAttribute(WellKnownMember.System_Runtime_CompilerServices_CompilerGeneratedAttribute__ctor, default(ImmutableArray<TypedConstant>), default(ImmutableArray<KeyValuePair<WellKnownMember, TypedConstant>>), false));
			}
			if (!this.NotNullMembers.IsEmpty)
			{
				foreach (SourceAttributeData sourceAttributeData in this._property.MemberNotNullAttributeIfExists)
				{
					Symbol.AddSynthesizedAttribute(ref attributes, new SynthesizedAttributeData(sourceAttributeData));
				}
			}
			if (!this.NotNullWhenTrueMembers.IsEmpty || !this.NotNullWhenFalseMembers.IsEmpty)
			{
				foreach (SourceAttributeData sourceAttributeData2 in this._property.MemberNotNullWhenAttributeIfExists)
				{
					Symbol.AddSynthesizedAttribute(ref attributes, new SynthesizedAttributeData(sourceAttributeData2));
				}
			}
		}

		// Token: 0x04001BF1 RID: 7153
		private readonly SourcePropertySymbolBase _property;

		// Token: 0x04001BF2 RID: 7154
		private ImmutableArray<ParameterSymbol> _lazyParameters;

		// Token: 0x04001BF3 RID: 7155
		private TypeWithAnnotations _lazyReturnType;

		// Token: 0x04001BF4 RID: 7156
		private ImmutableArray<CustomModifier> _lazyRefCustomModifiers;

		// Token: 0x04001BF5 RID: 7157
		private ImmutableArray<MethodSymbol> _lazyExplicitInterfaceImplementations;

		// Token: 0x04001BF6 RID: 7158
		private string _lazyName;

		// Token: 0x04001BF7 RID: 7159
		private readonly bool _isAutoPropertyAccessor;

		// Token: 0x04001BF8 RID: 7160
		private readonly bool _isExpressionBodied;

		// Token: 0x04001BF9 RID: 7161
		private readonly bool _usesInit;
	}
}
