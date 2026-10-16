using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200010B RID: 267
	internal sealed class InitializerSemanticModel : MemberSemanticModel
	{
		// Token: 0x0600158F RID: 5519 RVA: 0x00074904 File Offset: 0x00072B04
		private InitializerSemanticModel(CSharpSyntaxNode syntax, Symbol symbol, Binder rootBinder, SyntaxTreeSemanticModel containingSemanticModelOpt = null, SyntaxTreeSemanticModel parentSemanticModelOpt = null, ImmutableDictionary<Symbol, Symbol> parentRemappedSymbolsOpt = null, int speculatedPosition = 0)
			: base(syntax, symbol, rootBinder, containingSemanticModelOpt, parentSemanticModelOpt, null, parentRemappedSymbolsOpt, speculatedPosition)
		{
		}

		// Token: 0x06001590 RID: 5520 RVA: 0x00074923 File Offset: 0x00072B23
		internal static InitializerSemanticModel Create(SyntaxTreeSemanticModel containingSemanticModel, CSharpSyntaxNode syntax, FieldSymbol fieldSymbol, Binder rootBinder)
		{
			return new InitializerSemanticModel(syntax, fieldSymbol, rootBinder, containingSemanticModel, null, null, 0);
		}

		// Token: 0x06001591 RID: 5521 RVA: 0x00074931 File Offset: 0x00072B31
		internal static InitializerSemanticModel Create(SyntaxTreeSemanticModel containingSemanticModel, CSharpSyntaxNode syntax, PropertySymbol propertySymbol, Binder rootBinder)
		{
			return new InitializerSemanticModel(syntax, propertySymbol, rootBinder, containingSemanticModel, null, null, 0);
		}

		// Token: 0x06001592 RID: 5522 RVA: 0x0007493F File Offset: 0x00072B3F
		internal static InitializerSemanticModel Create(SyntaxTreeSemanticModel containingSemanticModel, ParameterSyntax syntax, ParameterSymbol parameterSymbol, Binder rootBinder, ImmutableDictionary<Symbol, Symbol> parentRemappedSymbolsOpt)
		{
			return new InitializerSemanticModel(syntax, parameterSymbol, rootBinder, containingSemanticModel, null, parentRemappedSymbolsOpt, 0);
		}

		// Token: 0x06001593 RID: 5523 RVA: 0x0007494E File Offset: 0x00072B4E
		internal static InitializerSemanticModel CreateSpeculative(SyntaxTreeSemanticModel parentSemanticModel, Symbol owner, CSharpSyntaxNode syntax, Binder rootBinder, ImmutableDictionary<Symbol, Symbol> parentRemappedSymbolsOpt, int position)
		{
			return new InitializerSemanticModel(syntax, owner, rootBinder, null, parentSemanticModel, parentRemappedSymbolsOpt, position);
		}

		// Token: 0x06001594 RID: 5524 RVA: 0x0007495E File Offset: 0x00072B5E
		protected internal override CSharpSyntaxNode GetBindableSyntaxNode(CSharpSyntaxNode node)
		{
			if (!this.IsBindableInitializer(node))
			{
				return base.GetBindableSyntaxNode(node);
			}
			return node;
		}

		// Token: 0x06001595 RID: 5525 RVA: 0x00074974 File Offset: 0x00072B74
		internal override BoundNode GetBoundRoot()
		{
			CSharpSyntaxNode csharpSyntaxNode = this.Root;
			SyntaxKind syntaxKind = csharpSyntaxNode.Kind();
			if (syntaxKind <= SyntaxKind.EqualsValueClause)
			{
				if (syntaxKind == SyntaxKind.VariableDeclarator)
				{
					csharpSyntaxNode = ((VariableDeclaratorSyntax)csharpSyntaxNode).Initializer;
					goto IL_0094;
				}
				if (syntaxKind == SyntaxKind.EqualsValueClause)
				{
					csharpSyntaxNode = (EqualsValueClauseSyntax)csharpSyntaxNode;
					goto IL_0094;
				}
			}
			else
			{
				if (syntaxKind == SyntaxKind.EnumMemberDeclaration)
				{
					csharpSyntaxNode = ((EnumMemberDeclarationSyntax)csharpSyntaxNode).EqualsValue;
					goto IL_0094;
				}
				if (syntaxKind == SyntaxKind.PropertyDeclaration)
				{
					csharpSyntaxNode = ((PropertyDeclarationSyntax)csharpSyntaxNode).Initializer;
					goto IL_0094;
				}
				if (syntaxKind == SyntaxKind.Parameter)
				{
					csharpSyntaxNode = ((ParameterSyntax)csharpSyntaxNode).Default;
					goto IL_0094;
				}
			}
			throw ExceptionUtilities.UnexpectedValue(csharpSyntaxNode.Kind());
			IL_0094:
			return base.GetUpperBoundNode(this.GetBindableSyntaxNode(csharpSyntaxNode), false);
		}

		// Token: 0x06001596 RID: 5526 RVA: 0x00074A24 File Offset: 0x00072C24
		internal override BoundNode Bind(Binder binder, CSharpSyntaxNode node, BindingDiagnosticBag diagnostics)
		{
			EqualsValueClauseSyntax equalsValueClauseSyntax = null;
			SyntaxKind syntaxKind = node.Kind();
			if (syntaxKind <= SyntaxKind.EqualsValueClause)
			{
				if (syntaxKind != SyntaxKind.VariableDeclarator)
				{
					if (syntaxKind == SyntaxKind.EqualsValueClause)
					{
						equalsValueClauseSyntax = (EqualsValueClauseSyntax)node;
					}
				}
				else
				{
					equalsValueClauseSyntax = ((VariableDeclaratorSyntax)node).Initializer;
				}
			}
			else if (syntaxKind != SyntaxKind.EnumMemberDeclaration)
			{
				if (syntaxKind != SyntaxKind.PropertyDeclaration)
				{
					if (syntaxKind == SyntaxKind.Parameter)
					{
						equalsValueClauseSyntax = ((ParameterSyntax)node).Default;
					}
				}
				else
				{
					equalsValueClauseSyntax = ((PropertyDeclarationSyntax)node).Initializer;
				}
			}
			else
			{
				equalsValueClauseSyntax = ((EnumMemberDeclarationSyntax)node).EqualsValue;
			}
			if (equalsValueClauseSyntax != null)
			{
				return this.BindEqualsValue(binder, equalsValueClauseSyntax, diagnostics);
			}
			return base.Bind(binder, node, diagnostics);
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x00074AC4 File Offset: 0x00072CC4
		private BoundEqualsValue BindEqualsValue(Binder binder, EqualsValueClauseSyntax equalsValue, BindingDiagnosticBag diagnostics)
		{
			SymbolKind kind = base.MemberSymbol.Kind;
			if (kind != SymbolKind.Field)
			{
				if (kind == SymbolKind.Parameter)
				{
					ParameterSymbol parameterSymbol = (ParameterSymbol)base.MemberSymbol;
					BoundExpression boundExpression;
					return binder.BindParameterDefaultValue(equalsValue, parameterSymbol, diagnostics, out boundExpression);
				}
				if (kind != SymbolKind.Property)
				{
					throw ExceptionUtilities.UnexpectedValue(base.MemberSymbol.Kind);
				}
				SourcePropertySymbol sourcePropertySymbol = (SourcePropertySymbol)base.MemberSymbol;
				BoundFieldEqualsValue boundFieldEqualsValue = binder.BindFieldInitializer(sourcePropertySymbol.BackingField, equalsValue, diagnostics);
				return new BoundPropertyEqualsValue(boundFieldEqualsValue.Syntax, sourcePropertySymbol, boundFieldEqualsValue.Locals, boundFieldEqualsValue.Value, false);
			}
			else
			{
				FieldSymbol fieldSymbol = (FieldSymbol)base.MemberSymbol;
				SourceEnumConstantSymbol sourceEnumConstantSymbol = fieldSymbol as SourceEnumConstantSymbol;
				if (sourceEnumConstantSymbol != null)
				{
					return binder.BindEnumConstantInitializer(sourceEnumConstantSymbol, equalsValue, diagnostics);
				}
				return binder.BindFieldInitializer(fieldSymbol, equalsValue, diagnostics);
			}
		}

		// Token: 0x06001598 RID: 5528 RVA: 0x00074B7F File Offset: 0x00072D7F
		private bool IsBindableInitializer(CSharpSyntaxNode node)
		{
			return node.Kind() == SyntaxKind.EqualsValueClause && (this.Root == node || this.Root == node.Parent);
		}

		// Token: 0x06001599 RID: 5529 RVA: 0x00074BAC File Offset: 0x00072DAC
		internal override bool TryGetSpeculativeSemanticModelCore(SyntaxTreeSemanticModel parentModel, int position, EqualsValueClauseSyntax initializer, out SemanticModel speculativeModel)
		{
			Binder binder = base.GetEnclosingBinder(position);
			if (binder == null)
			{
				speculativeModel = null;
				return false;
			}
			binder = new ExecutableCodeBinder(initializer, binder.ContainingMemberOrLambda, binder, null);
			speculativeModel = InitializerSemanticModel.CreateSpeculative(parentModel, base.MemberSymbol, initializer, binder, base.GetRemappedSymbols(), position);
			return true;
		}

		// Token: 0x0600159A RID: 5530 RVA: 0x00074BF2 File Offset: 0x00072DF2
		internal override bool TryGetSpeculativeSemanticModelCore(SyntaxTreeSemanticModel parentModel, int position, ConstructorInitializerSyntax constructorInitializer, out SemanticModel speculativeModel)
		{
			speculativeModel = null;
			return false;
		}

		// Token: 0x0600159B RID: 5531 RVA: 0x00074BF9 File Offset: 0x00072DF9
		internal override bool TryGetSpeculativeSemanticModelCore(SyntaxTreeSemanticModel parentModel, int position, PrimaryConstructorBaseTypeSyntax constructorInitializer, out SemanticModel speculativeModel)
		{
			speculativeModel = null;
			return false;
		}

		// Token: 0x0600159C RID: 5532 RVA: 0x00074C00 File Offset: 0x00072E00
		internal override bool TryGetSpeculativeSemanticModelCore(SyntaxTreeSemanticModel parentModel, int position, ArrowExpressionClauseSyntax expressionBody, out SemanticModel speculativeModel)
		{
			speculativeModel = null;
			return false;
		}

		// Token: 0x0600159D RID: 5533 RVA: 0x00074C07 File Offset: 0x00072E07
		internal override bool TryGetSpeculativeSemanticModelCore(SyntaxTreeSemanticModel parentModel, int position, StatementSyntax statement, out SemanticModel speculativeModel)
		{
			speculativeModel = null;
			return false;
		}

		// Token: 0x0600159E RID: 5534 RVA: 0x00074C0E File Offset: 0x00072E0E
		internal override bool TryGetSpeculativeSemanticModelForMethodBodyCore(SyntaxTreeSemanticModel parentModel, int position, BaseMethodDeclarationSyntax method, out SemanticModel speculativeModel)
		{
			speculativeModel = null;
			return false;
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x00074C15 File Offset: 0x00072E15
		internal override bool TryGetSpeculativeSemanticModelForMethodBodyCore(SyntaxTreeSemanticModel parentModel, int position, AccessorDeclarationSyntax accessor, out SemanticModel speculativeModel)
		{
			speculativeModel = null;
			return false;
		}

		// Token: 0x060015A0 RID: 5536 RVA: 0x00074C1C File Offset: 0x00072E1C
		protected override BoundNode RewriteNullableBoundNodesWithSnapshots(BoundNode boundRoot, Binder binder, DiagnosticBag diagnostics, bool createSnapshots, out NullableWalker.SnapshotManager snapshotManager, ref ImmutableDictionary<Symbol, Symbol> remappedSymbols)
		{
			return NullableWalker.AnalyzeAndRewrite(this.Compilation, base.MemberSymbol, boundRoot, binder, null, diagnostics, createSnapshots, out snapshotManager, ref remappedSymbols);
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x00074C44 File Offset: 0x00072E44
		protected override void AnalyzeBoundNodeNullability(BoundNode boundRoot, Binder binder, DiagnosticBag diagnostics, bool createSnapshots)
		{
			NullableWalker.AnalyzeWithoutRewrite(this.Compilation, base.MemberSymbol, boundRoot, binder, diagnostics, createSnapshots);
		}

		// Token: 0x060015A2 RID: 5538 RVA: 0x00074C5C File Offset: 0x00072E5C
		protected override bool IsNullableAnalysisEnabled()
		{
			SymbolKind kind = base.MemberSymbol.Kind;
			if (kind != SymbolKind.Field)
			{
				if (kind == SymbolKind.Parameter)
				{
					SyntaxNode defaultValueSyntaxForIsNullableAnalysisEnabled = SourceComplexParameterSymbol.GetDefaultValueSyntaxForIsNullableAnalysisEnabled(this.Root as ParameterSyntax);
					return defaultValueSyntaxForIsNullableAnalysisEnabled != null && this.Compilation.IsNullableAnalysisEnabledIn(defaultValueSyntaxForIsNullableAnalysisEnabled);
				}
				if (kind != SymbolKind.Property)
				{
					throw ExceptionUtilities.UnexpectedValue(base.MemberSymbol.Kind);
				}
			}
			SourceMemberContainerTypeSymbol sourceMemberContainerTypeSymbol = base.MemberSymbol.ContainingType as SourceMemberContainerTypeSymbol;
			return sourceMemberContainerTypeSymbol != null && sourceMemberContainerTypeSymbol.IsNullableEnabledForConstructorsAndInitializers(base.MemberSymbol.IsStatic);
		}
	}
}
