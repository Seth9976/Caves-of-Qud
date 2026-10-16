using System;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200054D RID: 1357
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class SourcePropertySymbol : SourcePropertySymbolBase
	{
		// Token: 0x060072FE RID: 29438 RVA: 0x001AE254 File Offset: 0x001AC454
		internal static SourcePropertySymbol Create(SourceMemberContainerTypeSymbol containingType, Binder bodyBinder, PropertyDeclarationSyntax syntax, BindingDiagnosticBag diagnostics)
		{
			SyntaxToken identifier = syntax.Identifier;
			Location location = identifier.GetLocation();
			return SourcePropertySymbol.Create(containingType, bodyBinder, syntax, identifier.ValueText, location, diagnostics);
		}

		// Token: 0x060072FF RID: 29439 RVA: 0x001AE284 File Offset: 0x001AC484
		internal static SourcePropertySymbol Create(SourceMemberContainerTypeSymbol containingType, Binder bodyBinder, IndexerDeclarationSyntax syntax, BindingDiagnosticBag diagnostics)
		{
			Location location = syntax.ThisKeyword.GetLocation();
			return SourcePropertySymbol.Create(containingType, bodyBinder, syntax, "Item", location, diagnostics);
		}

		// Token: 0x06007300 RID: 29440 RVA: 0x001AE2B0 File Offset: 0x001AC4B0
		private static SourcePropertySymbol Create(SourceMemberContainerTypeSymbol containingType, Binder binder, BasePropertyDeclarationSyntax syntax, string name, Location location, BindingDiagnosticBag diagnostics)
		{
			bool flag;
			bool flag2;
			bool flag3;
			bool flag4;
			CSharpSyntaxNode csharpSyntaxNode;
			CSharpSyntaxNode csharpSyntaxNode2;
			SourcePropertySymbol.GetAccessorDeclarations(syntax, diagnostics, out flag, out flag2, out flag3, out flag4, out csharpSyntaxNode, out csharpSyntaxNode2);
			ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier = SourcePropertySymbolBase.GetExplicitInterfaceSpecifier(syntax);
			SyntaxTokenList modifierTokensSyntax = SourcePropertySymbol.GetModifierTokensSyntax(syntax);
			bool flag5 = explicitInterfaceSpecifier != null;
			bool flag6;
			DeclarationModifiers declarationModifiers = SourcePropertySymbol.MakeModifiers(containingType, modifierTokensSyntax, flag5, syntax.Kind() == SyntaxKind.IndexerDeclaration, flag3, location, diagnostics, out flag6);
			bool flag7 = !flag2 && SourcePropertySymbol.GetArrowExpression(syntax) != null;
			binder = binder.WithUnsafeRegionIfNecessary(modifierTokensSyntax);
			TypeSymbol typeSymbol;
			string text;
			string memberNameAndInterfaceSymbol = ExplicitInterfaceHelpers.GetMemberNameAndInterfaceSymbol(binder, explicitInterfaceSpecifier, name, diagnostics, out typeSymbol, out text);
			return new SourcePropertySymbol(containingType, syntax, csharpSyntaxNode != null || flag7, csharpSyntaxNode2 != null, flag5, typeSymbol, text, declarationModifiers, flag, flag7, flag4, memberNameAndInterfaceSymbol, location, diagnostics);
		}

		// Token: 0x06007301 RID: 29441 RVA: 0x001AE35C File Offset: 0x001AC55C
		private SourcePropertySymbol(SourceMemberContainerTypeSymbol containingType, BasePropertyDeclarationSyntax syntax, bool hasGetAccessor, bool hasSetAccessor, bool isExplicitInterfaceImplementation, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol explicitInterfaceType, [global::System.Runtime.CompilerServices.Nullable(2)] string aliasQualifierOpt, DeclarationModifiers modifiers, bool isAutoProperty, bool isExpressionBodied, bool isInitOnly, string memberName, Location location, BindingDiagnosticBag diagnostics)
			: base(containingType, syntax, hasGetAccessor, hasSetAccessor, isExplicitInterfaceImplementation, explicitInterfaceType, aliasQualifierOpt, modifiers, SourcePropertySymbol.HasInitializer(syntax), isAutoProperty, isExpressionBodied, isInitOnly, syntax.Type.GetRefKind(), memberName, syntax.AttributeLists, location, diagnostics)
		{
			if (base.IsAutoProperty)
			{
				Binder.CheckFeatureAvailability(syntax, (hasGetAccessor && !hasSetAccessor) ? MessageID.IDS_FeatureReadonlyAutoImplementedProperties : MessageID.IDS_FeatureAutoImplementedProperties, diagnostics, location);
			}
			Symbol.CheckForBlockAndExpressionBody(syntax.AccessorList, syntax.GetExpressionBodySyntax(), syntax, diagnostics);
		}

		// Token: 0x06007302 RID: 29442 RVA: 0x001AE3D9 File Offset: 0x001AC5D9
		private TypeSyntax GetTypeSyntax(SyntaxNode syntax)
		{
			return ((BasePropertyDeclarationSyntax)syntax).Type;
		}

		// Token: 0x17001F38 RID: 7992
		// (get) Token: 0x06007303 RID: 29443 RVA: 0x001AE3E6 File Offset: 0x001AC5E6
		protected override Location TypeLocation
		{
			get
			{
				return this.GetTypeSyntax(base.CSharpSyntaxNode).Location;
			}
		}

		// Token: 0x06007304 RID: 29444 RVA: 0x001AE3F9 File Offset: 0x001AC5F9
		private static SyntaxTokenList GetModifierTokensSyntax(SyntaxNode syntax)
		{
			return ((BasePropertyDeclarationSyntax)syntax).Modifiers;
		}

		// Token: 0x06007305 RID: 29445 RVA: 0x001AE408 File Offset: 0x001AC608
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		private static ArrowExpressionClauseSyntax GetArrowExpression(SyntaxNode syntax)
		{
			PropertyDeclarationSyntax propertyDeclarationSyntax = syntax as PropertyDeclarationSyntax;
			ArrowExpressionClauseSyntax arrowExpressionClauseSyntax;
			if (propertyDeclarationSyntax == null)
			{
				IndexerDeclarationSyntax indexerDeclarationSyntax = syntax as IndexerDeclarationSyntax;
				if (indexerDeclarationSyntax == null)
				{
					throw ExceptionUtilities.UnexpectedValue(syntax.Kind());
				}
				arrowExpressionClauseSyntax = indexerDeclarationSyntax.ExpressionBody;
			}
			else
			{
				arrowExpressionClauseSyntax = propertyDeclarationSyntax.ExpressionBody;
			}
			return arrowExpressionClauseSyntax;
		}

		// Token: 0x06007306 RID: 29446 RVA: 0x001AE450 File Offset: 0x001AC650
		private static bool HasInitializer(SyntaxNode syntax)
		{
			PropertyDeclarationSyntax propertyDeclarationSyntax = syntax as PropertyDeclarationSyntax;
			return propertyDeclarationSyntax != null && propertyDeclarationSyntax.Initializer != null;
		}

		// Token: 0x17001F39 RID: 7993
		// (get) Token: 0x06007307 RID: 29447 RVA: 0x001AE472 File Offset: 0x001AC672
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeDeclarationSyntaxList
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return ((BasePropertyDeclarationSyntax)base.CSharpSyntaxNode).AttributeLists;
			}
		}

		// Token: 0x17001F3A RID: 7994
		// (get) Token: 0x06007308 RID: 29448 RVA: 0x001AE484 File Offset: 0x001AC684
		public override IAttributeTargetSymbol AttributesOwner
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06007309 RID: 29449 RVA: 0x001AE488 File Offset: 0x001AC688
		private static void GetAccessorDeclarations(CSharpSyntaxNode syntaxNode, BindingDiagnosticBag diagnostics, out bool isAutoProperty, out bool hasAccessorList, out bool accessorsHaveImplementation, out bool isInitOnly, [global::System.Runtime.CompilerServices.Nullable(2)] out CSharpSyntaxNode getSyntax, [global::System.Runtime.CompilerServices.Nullable(2)] out CSharpSyntaxNode setSyntax)
		{
			BasePropertyDeclarationSyntax basePropertyDeclarationSyntax = (BasePropertyDeclarationSyntax)syntaxNode;
			isAutoProperty = true;
			hasAccessorList = basePropertyDeclarationSyntax.AccessorList != null;
			getSyntax = null;
			setSyntax = null;
			isInitOnly = false;
			if (hasAccessorList)
			{
				accessorsHaveImplementation = false;
				foreach (AccessorDeclarationSyntax accessorDeclarationSyntax in basePropertyDeclarationSyntax.AccessorList.Accessors)
				{
					SyntaxKind syntaxKind = accessorDeclarationSyntax.Kind();
					switch (syntaxKind)
					{
					case SyntaxKind.GetAccessorDeclaration:
						if (getSyntax == null)
						{
							getSyntax = accessorDeclarationSyntax;
						}
						else
						{
							diagnostics.Add(ErrorCode.ERR_DuplicateAccessor, accessorDeclarationSyntax.Keyword.GetLocation());
						}
						break;
					case SyntaxKind.SetAccessorDeclaration:
						goto IL_00AF;
					case SyntaxKind.AddAccessorDeclaration:
					case SyntaxKind.RemoveAccessorDeclaration:
						diagnostics.Add(ErrorCode.ERR_GetOrSetExpected, accessorDeclarationSyntax.Keyword.GetLocation());
						continue;
					case SyntaxKind.UnknownAccessorDeclaration:
						continue;
					default:
						if (syntaxKind != SyntaxKind.InitAccessorDeclaration)
						{
							throw ExceptionUtilities.UnexpectedValue(accessorDeclarationSyntax.Kind());
						}
						goto IL_00AF;
					}
					IL_011B:
					if (accessorDeclarationSyntax.Body != null || accessorDeclarationSyntax.ExpressionBody != null)
					{
						isAutoProperty = false;
						accessorsHaveImplementation = true;
						continue;
					}
					continue;
					IL_00AF:
					if (setSyntax != null)
					{
						diagnostics.Add(ErrorCode.ERR_DuplicateAccessor, accessorDeclarationSyntax.Keyword.GetLocation());
						goto IL_011B;
					}
					setSyntax = accessorDeclarationSyntax;
					if (accessorDeclarationSyntax.Keyword.IsKind(SyntaxKind.InitKeyword))
					{
						isInitOnly = true;
						goto IL_011B;
					}
					goto IL_011B;
				}
				return;
			}
			isAutoProperty = false;
			accessorsHaveImplementation = SourcePropertySymbol.GetArrowExpression(basePropertyDeclarationSyntax) != null;
		}

		// Token: 0x0600730A RID: 29450 RVA: 0x001AE5E4 File Offset: 0x001AC7E4
		private static AccessorDeclarationSyntax GetGetAccessorDeclaration(BasePropertyDeclarationSyntax syntax)
		{
			foreach (AccessorDeclarationSyntax accessorDeclarationSyntax in syntax.AccessorList.Accessors)
			{
				if (accessorDeclarationSyntax.Kind() == SyntaxKind.GetAccessorDeclaration)
				{
					return accessorDeclarationSyntax;
				}
			}
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x0600730B RID: 29451 RVA: 0x001AE62C File Offset: 0x001AC82C
		private static AccessorDeclarationSyntax GetSetAccessorDeclaration(BasePropertyDeclarationSyntax syntax)
		{
			foreach (AccessorDeclarationSyntax accessorDeclarationSyntax in syntax.AccessorList.Accessors)
			{
				SyntaxKind syntaxKind = accessorDeclarationSyntax.Kind();
				if (syntaxKind == SyntaxKind.SetAccessorDeclaration || syntaxKind == SyntaxKind.InitAccessorDeclaration)
				{
					return accessorDeclarationSyntax;
				}
			}
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x0600730C RID: 29452 RVA: 0x001AE680 File Offset: 0x001AC880
		private static DeclarationModifiers MakeModifiers(NamedTypeSymbol containingType, SyntaxTokenList modifiers, bool isExplicitInterfaceImplementation, bool isIndexer, bool accessorsHaveImplementation, Location location, BindingDiagnosticBag diagnostics, out bool modifierErrors)
		{
			bool isInterface = containingType.IsInterface;
			DeclarationModifiers declarationModifiers = ((isInterface && !isExplicitInterfaceImplementation) ? DeclarationModifiers.Public : DeclarationModifiers.Private);
			DeclarationModifiers declarationModifiers2 = DeclarationModifiers.Unsafe;
			DeclarationModifiers declarationModifiers3 = DeclarationModifiers.None;
			if (!isExplicitInterfaceImplementation)
			{
				declarationModifiers2 |= DeclarationModifiers.Abstract | DeclarationModifiers.Sealed | DeclarationModifiers.New | DeclarationModifiers.Public | DeclarationModifiers.Protected | DeclarationModifiers.Internal | DeclarationModifiers.ProtectedInternal | DeclarationModifiers.Private | DeclarationModifiers.PrivateProtected | DeclarationModifiers.Virtual;
				if (!isIndexer)
				{
					declarationModifiers2 |= DeclarationModifiers.Static;
				}
				if (!isInterface)
				{
					declarationModifiers2 |= DeclarationModifiers.Override;
				}
				else
				{
					declarationModifiers = DeclarationModifiers.None;
					declarationModifiers3 |= DeclarationModifiers.Abstract | DeclarationModifiers.Sealed | (isIndexer ? DeclarationModifiers.None : DeclarationModifiers.Static) | DeclarationModifiers.Virtual | DeclarationModifiers.Extern | DeclarationModifiers.AccessibilityMask;
				}
			}
			else if (isInterface)
			{
				declarationModifiers2 |= DeclarationModifiers.Abstract;
			}
			if (containingType.IsStructType())
			{
				declarationModifiers2 |= DeclarationModifiers.ReadOnly;
			}
			declarationModifiers2 |= DeclarationModifiers.Extern;
			DeclarationModifiers declarationModifiers4 = ModifierUtils.MakeAndCheckNontypeMemberModifiers(modifiers, declarationModifiers, declarationModifiers2, location, diagnostics, out modifierErrors);
			containingType.CheckUnsafeModifier(declarationModifiers4, location, diagnostics);
			ModifierUtils.ReportDefaultInterfaceImplementationModifiers(accessorsHaveImplementation, declarationModifiers4, declarationModifiers3, location, diagnostics);
			if (isInterface)
			{
				declarationModifiers4 = ModifierUtils.AdjustModifiersForAnInterfaceMember(declarationModifiers4, accessorsHaveImplementation, isExplicitInterfaceImplementation);
			}
			if (isIndexer)
			{
				declarationModifiers4 |= DeclarationModifiers.Indexer;
			}
			return declarationModifiers4;
		}

		// Token: 0x0600730D RID: 29453 RVA: 0x001AE754 File Offset: 0x001AC954
		protected override SourcePropertyAccessorSymbol CreateGetAccessorSymbol(bool isAutoPropertyAccessor, BindingDiagnosticBag diagnostics)
		{
			BasePropertyDeclarationSyntax basePropertyDeclarationSyntax = (BasePropertyDeclarationSyntax)base.CSharpSyntaxNode;
			ArrowExpressionClauseSyntax arrowExpression = SourcePropertySymbol.GetArrowExpression(basePropertyDeclarationSyntax);
			if (basePropertyDeclarationSyntax.AccessorList == null && arrowExpression != null)
			{
				return this.CreateExpressionBodiedAccessor(arrowExpression, diagnostics);
			}
			return this.CreateAccessorSymbol(SourcePropertySymbol.GetGetAccessorDeclaration(basePropertyDeclarationSyntax), isAutoPropertyAccessor, diagnostics);
		}

		// Token: 0x0600730E RID: 29454 RVA: 0x001AE798 File Offset: 0x001AC998
		protected override SourcePropertyAccessorSymbol CreateSetAccessorSymbol(bool isAutoPropertyAccessor, BindingDiagnosticBag diagnostics)
		{
			BasePropertyDeclarationSyntax basePropertyDeclarationSyntax = (BasePropertyDeclarationSyntax)base.CSharpSyntaxNode;
			return this.CreateAccessorSymbol(SourcePropertySymbol.GetSetAccessorDeclaration(basePropertyDeclarationSyntax), isAutoPropertyAccessor, diagnostics);
		}

		// Token: 0x0600730F RID: 29455 RVA: 0x001AE7BF File Offset: 0x001AC9BF
		private SourcePropertyAccessorSymbol CreateAccessorSymbol(AccessorDeclarationSyntax syntax, bool isAutoPropertyAccessor, BindingDiagnosticBag diagnostics)
		{
			return SourcePropertyAccessorSymbol.CreateAccessorSymbol(this.ContainingType, this, this._modifiers, syntax, isAutoPropertyAccessor, diagnostics);
		}

		// Token: 0x06007310 RID: 29456 RVA: 0x001AE7D6 File Offset: 0x001AC9D6
		private SourcePropertyAccessorSymbol CreateExpressionBodiedAccessor(ArrowExpressionClauseSyntax syntax, BindingDiagnosticBag diagnostics)
		{
			return SourcePropertyAccessorSymbol.CreateAccessorSymbol(this.ContainingType, this, this._modifiers, syntax, diagnostics);
		}

		// Token: 0x06007311 RID: 29457 RVA: 0x001AE7EC File Offset: 0x001AC9EC
		private Binder CreateBinderForTypeAndParameters()
		{
			CSharpCompilation declaringCompilation = this.DeclaringCompilation;
			SyntaxTree syntaxTree = base.SyntaxTree;
			CSharpSyntaxNode csharpSyntaxNode = base.CSharpSyntaxNode;
			Binder binder = declaringCompilation.GetBinderFactory(syntaxTree, false).GetBinder(csharpSyntaxNode, csharpSyntaxNode, this);
			SyntaxTokenList modifierTokensSyntax = SourcePropertySymbol.GetModifierTokensSyntax(csharpSyntaxNode);
			return binder.WithUnsafeRegionIfNecessary(modifierTokensSyntax).WithAdditionalFlagsAndContainingMemberOrLambda(BinderFlags.SuppressConstraintChecks, this);
		}

		// Token: 0x06007312 RID: 29458 RVA: 0x001AE830 File Offset: 0x001ACA30
		[return: TupleElementNames(new string[] { "Type", "Parameters" })]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1 })]
		protected override ValueTuple<TypeWithAnnotations, ImmutableArray<ParameterSymbol>> MakeParametersAndBindType(BindingDiagnosticBag diagnostics)
		{
			Binder binder = this.CreateBinderForTypeAndParameters();
			CSharpSyntaxNode csharpSyntaxNode = base.CSharpSyntaxNode;
			return new ValueTuple<TypeWithAnnotations, ImmutableArray<ParameterSymbol>>(this.ComputeType(binder, csharpSyntaxNode, diagnostics), this.ComputeParameters(binder, csharpSyntaxNode, diagnostics));
		}

		// Token: 0x06007313 RID: 29459 RVA: 0x001AE864 File Offset: 0x001ACA64
		private TypeWithAnnotations ComputeType(Binder binder, SyntaxNode syntax, BindingDiagnosticBag diagnostics)
		{
			RefKind refKind;
			TypeSyntax typeSyntax = this.GetTypeSyntax(syntax).SkipRef(out refKind);
			TypeWithAnnotations typeWithAnnotations = binder.BindType(typeSyntax, diagnostics, null, false);
			CompoundUseSiteInfo<AssemblySymbol> newCompoundUseSiteInfo = binder.GetNewCompoundUseSiteInfo(diagnostics);
			if (base.GetExplicitInterfaceSpecifier() == null && !this.IsNoMoreVisibleThan(typeWithAnnotations, ref newCompoundUseSiteInfo))
			{
				diagnostics.Add(this.IsIndexer ? ErrorCode.ERR_BadVisIndexerReturn : ErrorCode.ERR_BadVisPropertyType, base.Location, new object[] { this, typeWithAnnotations.Type });
			}
			diagnostics.Add(base.Location, newCompoundUseSiteInfo);
			if (typeWithAnnotations.IsVoidType())
			{
				ErrorCode errorCode = (this.IsIndexer ? ErrorCode.ERR_IndexerCantHaveVoidType : ErrorCode.ERR_PropertyCantHaveVoidType);
				diagnostics.Add(errorCode, base.Location, new object[] { this });
			}
			return typeWithAnnotations;
		}

		// Token: 0x06007314 RID: 29460 RVA: 0x001AE91C File Offset: 0x001ACB1C
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private static ImmutableArray<ParameterSymbol> MakeParameters(Binder binder, SourcePropertySymbolBase owner, [global::System.Runtime.CompilerServices.Nullable(2)] BaseParameterListSyntax parameterSyntaxOpt, BindingDiagnosticBag diagnostics, bool addRefReadOnlyModifier)
		{
			if (parameterSyntaxOpt == null)
			{
				return ImmutableArray<ParameterSymbol>.Empty;
			}
			if (parameterSyntaxOpt.Parameters.Count < 1)
			{
				diagnostics.Add(ErrorCode.ERR_IndexerNeedsParam, parameterSyntaxOpt.GetLastToken(false, false, false, false).GetLocation());
			}
			SyntaxToken syntaxToken;
			ImmutableArray<ParameterSymbol> immutableArray = ParameterHelpers.MakeParameters(binder, owner, parameterSyntaxOpt, out syntaxToken, diagnostics, false, false, addRefReadOnlyModifier);
			if (syntaxToken.Kind() != SyntaxKind.None)
			{
				diagnostics.Add(ErrorCode.ERR_IllegalVarArgs, syntaxToken.GetLocation());
			}
			if (immutableArray.Length == 1 && !owner.IsExplicitInterfaceImplementation)
			{
				ParameterSyntax parameterSyntax = parameterSyntaxOpt.Parameters[0];
				if (parameterSyntax.Default != null)
				{
					SyntaxToken identifier = parameterSyntax.Identifier;
					diagnostics.Add(ErrorCode.WRN_DefaultValueForUnconsumedLocation, identifier.GetLocation(), new object[] { identifier.ValueText });
				}
			}
			return immutableArray;
		}

		// Token: 0x06007315 RID: 29461 RVA: 0x001AE9EC File Offset: 0x001ACBEC
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<ParameterSymbol> ComputeParameters(Binder binder, CSharpSyntaxNode syntax, BindingDiagnosticBag diagnostics)
		{
			BaseParameterListSyntax parameterListSyntax = SourcePropertySymbol.GetParameterListSyntax(syntax);
			return SourcePropertySymbol.MakeParameters(binder, this, parameterListSyntax, diagnostics, this.IsVirtual || this.IsAbstract);
		}

		// Token: 0x06007316 RID: 29462 RVA: 0x001AEA1C File Offset: 0x001ACC1C
		internal override void AfterAddingTypeMembersChecks(ConversionsBase conversions, BindingDiagnosticBag diagnostics)
		{
			base.AfterAddingTypeMembersChecks(conversions, diagnostics);
			CompoundUseSiteInfo<AssemblySymbol> compoundUseSiteInfo = new CompoundUseSiteInfo<AssemblySymbol>(diagnostics, this.ContainingAssembly);
			foreach (ParameterSymbol parameterSymbol in this.Parameters)
			{
				if (!this.IsExplicitInterfaceImplementation && !this.IsNoMoreVisibleThan(parameterSymbol.Type, ref compoundUseSiteInfo))
				{
					diagnostics.Add(ErrorCode.ERR_BadVisIndexerParam, base.Location, new object[] { this, parameterSymbol.Type });
				}
				else if (this.SetMethod != null && parameterSymbol.Name == "value")
				{
					diagnostics.Add(ErrorCode.ERR_DuplicateGeneratedName, parameterSymbol.Locations.FirstOrDefault<Location>() ?? base.Location, new object[] { parameterSymbol.Name });
				}
			}
			diagnostics.Add(base.Location, compoundUseSiteInfo);
		}

		// Token: 0x17001F3B RID: 7995
		// (get) Token: 0x06007317 RID: 29463 RVA: 0x001AEAFC File Offset: 0x001ACCFC
		protected override bool HasPointerTypeSyntactically
		{
			get
			{
				RefKind refKind;
				SyntaxKind syntaxKind = this.GetTypeSyntax(base.CSharpSyntaxNode).SkipRef(out refKind).Kind();
				return syntaxKind == SyntaxKind.PointerType || syntaxKind == SyntaxKind.FunctionPointerType;
			}
		}

		// Token: 0x06007318 RID: 29464 RVA: 0x001AEB3F File Offset: 0x001ACD3F
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		private static BaseParameterListSyntax GetParameterListSyntax(CSharpSyntaxNode syntax)
		{
			IndexerDeclarationSyntax indexerDeclarationSyntax = syntax as IndexerDeclarationSyntax;
			if (indexerDeclarationSyntax == null)
			{
				return null;
			}
			return indexerDeclarationSyntax.ParameterList;
		}
	}
}
