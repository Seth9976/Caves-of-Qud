using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.Collections;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200010D RID: 269
	internal abstract class MemberSemanticModel : CSharpSemanticModel
	{
		// Token: 0x060015A6 RID: 5542 RVA: 0x00074D5C File Offset: 0x00072F5C
		protected MemberSemanticModel(CSharpSyntaxNode root, Symbol memberSymbol, Binder rootBinder, SyntaxTreeSemanticModel containingSemanticModelOpt, SyntaxTreeSemanticModel parentSemanticModelOpt, NullableWalker.SnapshotManager snapshotManagerOpt, ImmutableDictionary<Symbol, Symbol> parentRemappedSymbolsOpt, int speculatedPosition)
		{
			this._root = root;
			this._memberSymbol = memberSymbol;
			this.RootBinder = rootBinder.WithAdditionalFlags(base.GetSemanticModelBinderFlags());
			this._containingSemanticModelOpt = containingSemanticModelOpt;
			this._parentSemanticModelOpt = parentSemanticModelOpt;
			this._parentSnapshotManagerOpt = snapshotManagerOpt;
			this._parentRemappedSymbolsOpt = parentRemappedSymbolsOpt;
			this._speculatedPosition = speculatedPosition;
			this._operationFactory = new Lazy<CSharpOperationFactory>(() => new CSharpOperationFactory(this));
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x060015A7 RID: 5543 RVA: 0x00074DF0 File Offset: 0x00072FF0
		public override CSharpCompilation Compilation
		{
			get
			{
				return (this._containingSemanticModelOpt ?? this._parentSemanticModelOpt).Compilation;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x060015A8 RID: 5544 RVA: 0x00074E07 File Offset: 0x00073007
		internal override CSharpSyntaxNode Root
		{
			get
			{
				return this._root;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x060015A9 RID: 5545 RVA: 0x00074E0F File Offset: 0x0007300F
		internal Symbol MemberSymbol
		{
			get
			{
				return this._memberSymbol;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x060015AA RID: 5546 RVA: 0x00074E17 File Offset: 0x00073017
		public sealed override bool IsSpeculativeSemanticModel
		{
			get
			{
				return this._parentSemanticModelOpt != null;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x060015AB RID: 5547 RVA: 0x00074E22 File Offset: 0x00073022
		public sealed override int OriginalPositionForSpeculation
		{
			get
			{
				return this._speculatedPosition;
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x060015AC RID: 5548 RVA: 0x00074E2A File Offset: 0x0007302A
		public sealed override CSharpSemanticModel ParentModel
		{
			get
			{
				return this._parentSemanticModelOpt;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x060015AD RID: 5549 RVA: 0x00074E32 File Offset: 0x00073032
		internal sealed override SemanticModel ContainingModelOrSelf
		{
			get
			{
				return this._containingSemanticModelOpt ?? this;
			}
		}

		// Token: 0x060015AE RID: 5550 RVA: 0x00074E3F File Offset: 0x0007303F
		internal override MemberSemanticModel GetMemberModel(SyntaxNode node)
		{
			if (!base.IsInTree(node))
			{
				return null;
			}
			return this;
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x00074E4D File Offset: 0x0007304D
		protected virtual NullableWalker.SnapshotManager GetSnapshotManager()
		{
			this.EnsureNullabilityAnalysisPerformedIfNecessary();
			return this._lazySnapshotManager;
		}

		// Token: 0x060015B0 RID: 5552 RVA: 0x00074E5B File Offset: 0x0007305B
		internal ImmutableDictionary<Symbol, Symbol> GetRemappedSymbols()
		{
			this.EnsureNullabilityAnalysisPerformedIfNecessary();
			return this._lazyRemappedSymbols;
		}

		// Token: 0x060015B1 RID: 5553 RVA: 0x00074E6C File Offset: 0x0007306C
		internal sealed override bool TryGetSpeculativeSemanticModelCore(SyntaxTreeSemanticModel parentModel, int position, TypeSyntax type, SpeculativeBindingOption bindingOption, out SemanticModel speculativeModel)
		{
			ExpressionSyntax standaloneExpression = SyntaxFactory.GetStandaloneExpression(type);
			Binder speculativeBinder = base.GetSpeculativeBinder(position, standaloneExpression, bindingOption);
			if (speculativeBinder != null)
			{
				speculativeModel = new MemberSemanticModel.SpeculativeMemberSemanticModel(parentModel, this._memberSymbol, type, speculativeBinder, this.GetSnapshotManager(), this.GetRemappedSymbols(), position);
				return true;
			}
			speculativeModel = null;
			return false;
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x00074EB3 File Offset: 0x000730B3
		internal sealed override bool TryGetSpeculativeSemanticModelCore(SyntaxTreeSemanticModel parentModel, int position, CrefSyntax crefSyntax, out SemanticModel speculativeModel)
		{
			speculativeModel = null;
			return false;
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x00074EBC File Offset: 0x000730BC
		internal override BoundExpression GetSpeculativelyBoundExpression(int position, ExpressionSyntax expression, SpeculativeBindingOption bindingOption, out Binder binder, out ImmutableArray<Symbol> crefSymbols)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			if (bindingOption == SpeculativeBindingOption.BindAsExpression)
			{
				NullableWalker.SnapshotManager snapshotManager = this.GetSnapshotManager();
				if (snapshotManager != null)
				{
					crefSymbols = default(ImmutableArray<Symbol>);
					position = base.CheckAndAdjustPosition(position);
					expression = SyntaxFactory.GetStandaloneExpression(expression);
					binder = base.GetSpeculativeBinder(position, expression, bindingOption);
					BoundExpression boundExpression = binder.BindExpression(expression, BindingDiagnosticBag.Discarded);
					ImmutableDictionary<Symbol, Symbol> immutableDictionary = null;
					NullableWalker.SnapshotManager snapshotManager2;
					return (BoundExpression)NullableWalker.AnalyzeAndRewriteSpeculation(position, boundExpression, binder, snapshotManager, out snapshotManager2, ref immutableDictionary);
				}
			}
			return base.GetSpeculativelyBoundExpressionWithoutNullability(position, expression, bindingOption, out binder, out crefSymbols);
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x00074F3C File Offset: 0x0007313C
		private Binder GetEnclosingBinderInternalWithinRoot(SyntaxNode node, int position)
		{
			return MemberSemanticModel.GetEnclosingBinderInternalWithinRoot(node, position, this.RootBinder, this._root).WithAdditionalFlags(base.GetSemanticModelBinderFlags());
		}

		// Token: 0x060015B5 RID: 5557 RVA: 0x00074F5C File Offset: 0x0007315C
		private static Binder GetEnclosingBinderInternalWithinRoot(SyntaxNode node, int position, Binder rootBinder, SyntaxNode root)
		{
			if (node == root)
			{
				return rootBinder.GetBinder(node) ?? rootBinder;
			}
			ExpressionSyntax expressionSyntax = null;
			LocalFunctionStatementSyntax localFunctionStatementSyntax = null;
			Binder binder = null;
			SyntaxNode syntaxNode = node;
			while (binder == null)
			{
				StatementSyntax statementSyntax = syntaxNode as StatementSyntax;
				SyntaxKind syntaxKind = syntaxNode.Kind();
				TypeOfExpressionSyntax typeOfExpressionSyntax;
				if (statementSyntax != null)
				{
					if (LookupPosition.IsInStatementScope(position, statementSyntax))
					{
						binder = rootBinder.GetBinder(syntaxNode);
						if (binder != null)
						{
							binder = MemberSemanticModel.AdjustBinderForPositionWithinStatement(position, binder, statementSyntax);
						}
						else if (syntaxKind == SyntaxKind.LocalFunctionStatement)
						{
							LocalFunctionStatementSyntax localFunctionStatementSyntax2 = (LocalFunctionStatementSyntax)statementSyntax;
							if (localFunctionStatementSyntax2.TypeParameterList != null && !LookupPosition.IsBetweenTokens(position, localFunctionStatementSyntax2.Identifier, localFunctionStatementSyntax2.TypeParameterList.LessThanToken))
							{
								localFunctionStatementSyntax = localFunctionStatementSyntax2;
							}
						}
					}
				}
				else if (syntaxKind == SyntaxKind.CatchClause)
				{
					if (LookupPosition.IsInCatchBlockScope(position, (CatchClauseSyntax)syntaxNode))
					{
						binder = rootBinder.GetBinder(syntaxNode);
					}
				}
				else if (syntaxKind == SyntaxKind.CatchFilterClause)
				{
					if (LookupPosition.IsInCatchFilterScope(position, (CatchFilterClauseSyntax)syntaxNode))
					{
						binder = rootBinder.GetBinder(syntaxNode);
					}
				}
				else if (syntaxNode.IsAnonymousFunction())
				{
					if (LookupPosition.IsInAnonymousFunctionOrQuery(position, syntaxNode))
					{
						binder = rootBinder.GetBinder(syntaxNode.AnonymousFunctionBody());
					}
				}
				else if (syntaxKind == SyntaxKind.TypeOfExpression && expressionSyntax == null && LookupPosition.IsBetweenTokens(position, (typeOfExpressionSyntax = (TypeOfExpressionSyntax)syntaxNode).OpenParenToken, typeOfExpressionSyntax.CloseParenToken))
				{
					expressionSyntax = typeOfExpressionSyntax.Type;
				}
				else if (syntaxKind == SyntaxKind.SwitchSection)
				{
					if (LookupPosition.IsInSwitchSectionScope(position, (SwitchSectionSyntax)syntaxNode))
					{
						binder = rootBinder.GetBinder(syntaxNode);
					}
				}
				else if (syntaxKind == SyntaxKind.ArgumentList)
				{
					ArgumentListSyntax argumentListSyntax = (ArgumentListSyntax)syntaxNode;
					if (LookupPosition.IsBetweenTokens(position, argumentListSyntax.OpenParenToken, argumentListSyntax.CloseParenToken))
					{
						binder = rootBinder.GetBinder(syntaxNode);
					}
				}
				else if (syntaxKind == SyntaxKind.EqualsValueClause)
				{
					binder = rootBinder.GetBinder(syntaxNode);
				}
				else if (syntaxKind == SyntaxKind.Attribute)
				{
					binder = rootBinder.GetBinder(syntaxNode);
				}
				else if (syntaxKind == SyntaxKind.ArrowExpressionClause)
				{
					binder = rootBinder.GetBinder(syntaxNode);
				}
				else if (syntaxKind == SyntaxKind.ThisConstructorInitializer || syntaxKind == SyntaxKind.BaseConstructorInitializer || syntaxKind == SyntaxKind.PrimaryConstructorBaseType)
				{
					binder = rootBinder.GetBinder(syntaxNode);
				}
				else if (syntaxKind == SyntaxKind.ConstructorDeclaration)
				{
					binder = rootBinder.GetBinder(syntaxNode);
				}
				else if (syntaxKind == SyntaxKind.SwitchExpression)
				{
					binder = rootBinder.GetBinder(syntaxNode);
				}
				else if (syntaxKind == SyntaxKind.SwitchExpressionArm)
				{
					binder = rootBinder.GetBinder(syntaxNode);
				}
				else if ((syntaxNode as ExpressionSyntax).IsValidScopeDesignator())
				{
					binder = rootBinder.GetBinder(syntaxNode);
				}
				if (syntaxNode == root)
				{
					break;
				}
				syntaxNode = syntaxNode.ParentOrStructuredTriviaParent;
			}
			Binder binder2;
			if ((binder2 = binder) == null)
			{
				binder2 = rootBinder.GetBinder(root) ?? rootBinder;
			}
			binder = binder2;
			if (localFunctionStatementSyntax != null)
			{
				LocalFunctionSymbol declaredLocalFunction = MemberSemanticModel.GetDeclaredLocalFunction(binder, localFunctionStatementSyntax.Identifier);
				if (declaredLocalFunction != null)
				{
					binder = declaredLocalFunction.SignatureBinder;
				}
			}
			if (expressionSyntax != null)
			{
				binder = new TypeofBinder(expressionSyntax, binder);
			}
			return binder;
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x00075210 File Offset: 0x00073410
		private static Binder AdjustBinderForPositionWithinStatement(int position, Binder binder, StatementSyntax stmt)
		{
			SyntaxKind syntaxKind = stmt.Kind();
			if (syntaxKind <= SyntaxKind.ForEachStatement)
			{
				if (syntaxKind != SyntaxKind.ForStatement)
				{
					if (syntaxKind != SyntaxKind.ForEachStatement)
					{
						return binder;
					}
				}
				else
				{
					ForStatementSyntax forStatementSyntax = (ForStatementSyntax)stmt;
					if (LookupPosition.IsBetweenTokens(position, forStatementSyntax.SecondSemicolonToken, forStatementSyntax.CloseParenToken) && forStatementSyntax.Incrementors.Count > 0)
					{
						return binder.GetBinder(forStatementSyntax.Incrementors.First());
					}
					if (LookupPosition.IsBetweenTokens(position, forStatementSyntax.FirstSemicolonToken, LookupPosition.GetFirstExcludedToken(forStatementSyntax)) && forStatementSyntax.Condition != null)
					{
						return binder.GetBinder(forStatementSyntax.Condition);
					}
					return binder;
				}
			}
			else if (syntaxKind != SyntaxKind.SwitchStatement)
			{
				if (syntaxKind != SyntaxKind.ForEachVariableStatement)
				{
					return binder;
				}
			}
			else
			{
				SwitchStatementSyntax switchStatementSyntax = (SwitchStatementSyntax)stmt;
				if (LookupPosition.IsBetweenTokens(position, switchStatementSyntax.SwitchKeyword, switchStatementSyntax.OpenBraceToken))
				{
					return binder.GetBinder(switchStatementSyntax.Expression);
				}
				return binder;
			}
			CommonForEachStatementSyntax commonForEachStatementSyntax = (CommonForEachStatementSyntax)stmt;
			SyntaxToken syntaxToken = ((stmt.Kind() == SyntaxKind.ForEachVariableStatement) ? commonForEachStatementSyntax.InKeyword : commonForEachStatementSyntax.OpenParenToken);
			if (LookupPosition.IsBetweenTokens(position, syntaxToken, commonForEachStatementSyntax.Statement.GetFirstToken(false, false, false, false)))
			{
				binder = binder.GetBinder(commonForEachStatementSyntax.Expression);
			}
			return binder;
		}

		// Token: 0x060015B7 RID: 5559 RVA: 0x00075350 File Offset: 0x00073550
		public override Conversion ClassifyConversion(ExpressionSyntax expression, ITypeSymbol destination, bool isExplicitInSource = false)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			TypeSymbol typeSymbol = destination.EnsureCSharpSymbolOrNull("destination");
			if (expression.Kind() == SyntaxKind.DeclarationExpression)
			{
				return Conversion.NoConversion;
			}
			if (expression.IsAnonymousFunction())
			{
				base.CheckSyntaxNode(expression);
				return base.ClassifyConversion(expression.SpanStart, expression, destination, isExplicitInSource);
			}
			if (isExplicitInSource)
			{
				return this.ClassifyConversionForCast(expression, typeSymbol);
			}
			base.CheckSyntaxNode(expression);
			Binder enclosingBinderInternal = this.GetEnclosingBinderInternal(expression, base.GetAdjustedNodePosition(expression));
			CSharpSyntaxNode bindableSyntaxNode = this.GetBindableSyntaxNode(expression);
			BoundExpression boundExpression = this.GetLowerBoundNode(bindableSyntaxNode) as BoundExpression;
			if (enclosingBinderInternal == null || boundExpression == null)
			{
				return Conversion.NoConversion;
			}
			CompoundUseSiteInfo<AssemblySymbol> discarded = CompoundUseSiteInfo<AssemblySymbol>.Discarded;
			return enclosingBinderInternal.Conversions.ClassifyConversionFromExpression(boundExpression, typeSymbol, ref discarded, false);
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x00075404 File Offset: 0x00073604
		internal override Conversion ClassifyConversionForCast(ExpressionSyntax expression, TypeSymbol destination)
		{
			base.CheckSyntaxNode(expression);
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			Binder enclosingBinderInternal = this.GetEnclosingBinderInternal(expression, base.GetAdjustedNodePosition(expression));
			CSharpSyntaxNode bindableSyntaxNode = this.GetBindableSyntaxNode(expression);
			BoundExpression boundExpression = this.GetLowerBoundNode(bindableSyntaxNode) as BoundExpression;
			if (enclosingBinderInternal == null || boundExpression == null)
			{
				return Conversion.NoConversion;
			}
			CompoundUseSiteInfo<AssemblySymbol> discarded = CompoundUseSiteInfo<AssemblySymbol>.Discarded;
			return enclosingBinderInternal.Conversions.ClassifyConversionFromExpression(boundExpression, destination, ref discarded, true);
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x0007546C File Offset: 0x0007366C
		internal virtual BoundNode GetBoundRoot()
		{
			return this.GetUpperBoundNode(this.GetBindableSyntaxNode(this.Root), false);
		}

		// Token: 0x060015BA RID: 5562 RVA: 0x00075484 File Offset: 0x00073684
		internal BoundNode GetUpperBoundNode(CSharpSyntaxNode node, bool promoteToBindable = false)
		{
			if (promoteToBindable)
			{
				node = this.GetBindableSyntaxNode(node);
			}
			ImmutableArray<BoundNode> boundNodes = this.GetBoundNodes(node);
			if (boundNodes.Length == 0)
			{
				return null;
			}
			return boundNodes[0];
		}

		// Token: 0x060015BB RID: 5563 RVA: 0x000754B8 File Offset: 0x000736B8
		internal BoundNode GetLowerBoundNode(CSharpSyntaxNode node)
		{
			ImmutableArray<BoundNode> boundNodes = this.GetBoundNodes(node);
			if (boundNodes.Length == 0)
			{
				return null;
			}
			return MemberSemanticModel.GetLowerBoundNode(boundNodes);
		}

		// Token: 0x060015BC RID: 5564 RVA: 0x000754DE File Offset: 0x000736DE
		private static BoundNode GetLowerBoundNode(ImmutableArray<BoundNode> boundNodes)
		{
			return boundNodes[boundNodes.Length - 1];
		}

		// Token: 0x060015BD RID: 5565 RVA: 0x000754F0 File Offset: 0x000736F0
		public override ImmutableArray<Diagnostic> GetSyntaxDiagnostics(TextSpan? span = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			throw new NotSupportedException();
		}

		// Token: 0x060015BE RID: 5566 RVA: 0x000754F7 File Offset: 0x000736F7
		public override ImmutableArray<Diagnostic> GetDeclarationDiagnostics(TextSpan? span = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			throw new NotSupportedException();
		}

		// Token: 0x060015BF RID: 5567 RVA: 0x000754FE File Offset: 0x000736FE
		public override ImmutableArray<Diagnostic> GetMethodBodyDiagnostics(TextSpan? span = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			throw new NotSupportedException();
		}

		// Token: 0x060015C0 RID: 5568 RVA: 0x00075505 File Offset: 0x00073705
		public override ImmutableArray<Diagnostic> GetDiagnostics(TextSpan? span = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			throw new NotSupportedException();
		}

		// Token: 0x060015C1 RID: 5569 RVA: 0x0007550C File Offset: 0x0007370C
		public override INamespaceSymbol GetDeclaredSymbol(NamespaceDeclarationSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			return null;
		}

		// Token: 0x060015C2 RID: 5570 RVA: 0x0007550F File Offset: 0x0007370F
		public override INamedTypeSymbol GetDeclaredSymbol(BaseTypeDeclarationSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			return null;
		}

		// Token: 0x060015C3 RID: 5571 RVA: 0x00075512 File Offset: 0x00073712
		public override INamedTypeSymbol GetDeclaredSymbol(DelegateDeclarationSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			return null;
		}

		// Token: 0x060015C4 RID: 5572 RVA: 0x00075515 File Offset: 0x00073715
		public override IFieldSymbol GetDeclaredSymbol(EnumMemberDeclarationSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			return null;
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x00075518 File Offset: 0x00073718
		public override ISymbol GetDeclaredSymbol(LocalFunctionStatementSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			base.CheckSyntaxNode(declarationSyntax);
			return this.GetDeclaredLocalFunction(declarationSyntax).GetPublicSymbol();
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x0007552D File Offset: 0x0007372D
		public override ISymbol GetDeclaredSymbol(MemberDeclarationSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			return null;
		}

		// Token: 0x060015C7 RID: 5575 RVA: 0x00075530 File Offset: 0x00073730
		public override IMethodSymbol GetDeclaredSymbol(CompilationUnitSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			return null;
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x00075533 File Offset: 0x00073733
		public override IMethodSymbol GetDeclaredSymbol(BaseMethodDeclarationSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			return null;
		}

		// Token: 0x060015C9 RID: 5577 RVA: 0x00075536 File Offset: 0x00073736
		public override ISymbol GetDeclaredSymbol(BasePropertyDeclarationSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			return null;
		}

		// Token: 0x060015CA RID: 5578 RVA: 0x00075539 File Offset: 0x00073739
		public override IPropertySymbol GetDeclaredSymbol(PropertyDeclarationSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			return null;
		}

		// Token: 0x060015CB RID: 5579 RVA: 0x0007553C File Offset: 0x0007373C
		public override IPropertySymbol GetDeclaredSymbol(IndexerDeclarationSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			return null;
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x0007553F File Offset: 0x0007373F
		public override IEventSymbol GetDeclaredSymbol(EventDeclarationSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			return null;
		}

		// Token: 0x060015CD RID: 5581 RVA: 0x00075542 File Offset: 0x00073742
		public override IMethodSymbol GetDeclaredSymbol(AccessorDeclarationSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			return null;
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x00075545 File Offset: 0x00073745
		public override IMethodSymbol GetDeclaredSymbol(ArrowExpressionClauseSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			return null;
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x00075548 File Offset: 0x00073748
		public override ISymbol GetDeclaredSymbol(VariableDeclaratorSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			base.CheckSyntaxNode(declarationSyntax);
			return this.GetDeclaredLocal(declarationSyntax, declarationSyntax.Identifier).GetPublicSymbol();
		}

		// Token: 0x060015D0 RID: 5584 RVA: 0x00075563 File Offset: 0x00073763
		public override ISymbol GetDeclaredSymbol(SingleVariableDesignationSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			base.CheckSyntaxNode(declarationSyntax);
			return this.GetDeclaredLocal(declarationSyntax, declarationSyntax.Identifier).GetPublicSymbol();
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x00075580 File Offset: 0x00073780
		private LocalSymbol GetDeclaredLocal(CSharpSyntaxNode declarationSyntax, SyntaxToken declaredIdentifier)
		{
			for (Binder binder = base.GetEnclosingBinder(base.GetAdjustedNodePosition(declarationSyntax)); binder != null; binder = binder.Next)
			{
				foreach (LocalSymbol localSymbol in binder.Locals)
				{
					if (localSymbol.IdentifierToken == declaredIdentifier)
					{
						return this.GetAdjustedLocalSymbol((SourceLocalSymbol)localSymbol);
					}
				}
			}
			return null;
		}

		// Token: 0x060015D2 RID: 5586 RVA: 0x000755E5 File Offset: 0x000737E5
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal override LocalSymbol GetAdjustedLocalSymbol(SourceLocalSymbol local)
		{
			return this.GetRemappedSymbol<LocalSymbol>(local);
		}

		// Token: 0x060015D3 RID: 5587 RVA: 0x000755F0 File Offset: 0x000737F0
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		private LocalFunctionSymbol GetDeclaredLocalFunction(LocalFunctionStatementSyntax declarationSyntax)
		{
			LocalFunctionSymbol declaredLocalFunction = MemberSemanticModel.GetDeclaredLocalFunction(base.GetEnclosingBinder(base.GetAdjustedNodePosition(declarationSyntax)), declarationSyntax.Identifier);
			return this.GetRemappedSymbol<LocalFunctionSymbol>(declaredLocalFunction);
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x00075620 File Offset: 0x00073820
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		private T GetRemappedSymbol<[global::System.Runtime.CompilerServices.Nullable(0)] T>(T originalSymbol) where T : Symbol
		{
			this.EnsureNullabilityAnalysisPerformedIfNecessary();
			if (this._lazyRemappedSymbols == null)
			{
				return originalSymbol;
			}
			Symbol symbol;
			if (this._lazyRemappedSymbols.TryGetValue(originalSymbol, out symbol))
			{
				return (T)((object)symbol);
			}
			return originalSymbol;
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x0007565C File Offset: 0x0007385C
		private static LocalFunctionSymbol GetDeclaredLocalFunction(Binder enclosingBinder, SyntaxToken declaredIdentifier)
		{
			for (Binder binder = enclosingBinder; binder != null; binder = binder.Next)
			{
				foreach (LocalFunctionSymbol localFunctionSymbol in binder.LocalFunctions)
				{
					if (localFunctionSymbol.NameToken == declaredIdentifier)
					{
						return localFunctionSymbol;
					}
				}
			}
			return null;
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x000756AC File Offset: 0x000738AC
		public override ILabelSymbol GetDeclaredSymbol(LabeledStatementSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			base.CheckSyntaxNode(declarationSyntax);
			Binder binder = base.GetEnclosingBinder(base.GetAdjustedNodePosition(declarationSyntax));
			while (binder != null && !binder.IsLabelsScopeBinder)
			{
				binder = binder.Next;
			}
			if (binder != null)
			{
				foreach (LabelSymbol labelSymbol in binder.Labels)
				{
					if (labelSymbol.IdentifierNodeOrToken.IsToken && labelSymbol.IdentifierNodeOrToken.AsToken() == declarationSyntax.Identifier)
					{
						return labelSymbol.GetPublicSymbol();
					}
				}
			}
			return null;
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x0007573C File Offset: 0x0007393C
		public override ILabelSymbol GetDeclaredSymbol(SwitchLabelSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			base.CheckSyntaxNode(declarationSyntax);
			Binder binder = base.GetEnclosingBinder(base.GetAdjustedNodePosition(declarationSyntax));
			while (binder != null && !(binder is SwitchBinder))
			{
				binder = binder.Next;
			}
			if (binder != null)
			{
				foreach (LabelSymbol labelSymbol in binder.Labels)
				{
					if (labelSymbol.IdentifierNodeOrToken.IsNode && labelSymbol.IdentifierNodeOrToken.AsNode() == declarationSyntax)
					{
						return labelSymbol.GetPublicSymbol();
					}
				}
			}
			return null;
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x000757C2 File Offset: 0x000739C2
		public override IAliasSymbol GetDeclaredSymbol(UsingDirectiveSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			return null;
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x000757C5 File Offset: 0x000739C5
		public override IAliasSymbol GetDeclaredSymbol(ExternAliasDirectiveSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			return null;
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x000757C8 File Offset: 0x000739C8
		public override IParameterSymbol GetDeclaredSymbol(ParameterSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			base.CheckSyntaxNode(declarationSyntax);
			return this.GetLambdaOrLocalFunctionParameterSymbol(declarationSyntax, cancellationToken).GetPublicSymbol();
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x000757DE File Offset: 0x000739DE
		internal override ImmutableArray<ISymbol> GetDeclaredSymbols(BaseFieldDeclarationSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ImmutableArray.Create<ISymbol>();
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x000757E8 File Offset: 0x000739E8
		private ParameterSymbol GetLambdaOrLocalFunctionParameterSymbol(ParameterSyntax parameter, CancellationToken cancellationToken)
		{
			SimpleLambdaExpressionSyntax simpleLambdaExpressionSyntax = parameter.Parent as SimpleLambdaExpressionSyntax;
			if (simpleLambdaExpressionSyntax != null)
			{
				return this.GetLambdaParameterSymbol(parameter, simpleLambdaExpressionSyntax, cancellationToken);
			}
			ParameterListSyntax parameterListSyntax = parameter.Parent as ParameterListSyntax;
			if (parameterListSyntax == null || parameterListSyntax.Parent == null)
			{
				return null;
			}
			if (parameterListSyntax.Parent.IsAnonymousFunction())
			{
				return this.GetLambdaParameterSymbol(parameter, (ExpressionSyntax)parameterListSyntax.Parent, cancellationToken);
			}
			if (parameterListSyntax.Parent.Kind() == SyntaxKind.LocalFunctionStatement)
			{
				MethodSymbol symbol = this.GetDeclaredSymbol((LocalFunctionStatementSyntax)parameterListSyntax.Parent, cancellationToken).GetSymbol<MethodSymbol>();
				if (symbol != null)
				{
					return base.GetParameterSymbol(symbol.Parameters, parameter, cancellationToken);
				}
			}
			return null;
		}

		// Token: 0x060015DD RID: 5597 RVA: 0x00075888 File Offset: 0x00073A88
		private ParameterSymbol GetLambdaParameterSymbol(ParameterSyntax parameter, ExpressionSyntax lambda, CancellationToken cancellationToken)
		{
			SymbolInfo symbolInfo = base.GetSymbolInfo(lambda, cancellationToken);
			LambdaSymbol lambdaSymbol;
			if (symbolInfo.Symbol != null)
			{
				lambdaSymbol = symbolInfo.Symbol.GetSymbol<LambdaSymbol>();
			}
			else
			{
				if (symbolInfo.CandidateSymbols.Length != 1)
				{
					return null;
				}
				lambdaSymbol = symbolInfo.CandidateSymbols.Single<ISymbol>().GetSymbol<LambdaSymbol>();
			}
			return base.GetParameterSymbol(lambdaSymbol.Parameters, parameter, cancellationToken);
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x000758EC File Offset: 0x00073AEC
		public override ITypeParameterSymbol GetDeclaredSymbol(TypeParameterSyntax typeParameter, CancellationToken cancellationToken = default(CancellationToken))
		{
			return null;
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x000758F0 File Offset: 0x00073AF0
		public override IRangeVariableSymbol GetDeclaredSymbol(JoinIntoClauseSyntax node, CancellationToken cancellationToken = default(CancellationToken))
		{
			BoundQueryClause boundQueryClause = this.GetBoundQueryClause(node);
			if (boundQueryClause != null)
			{
				return boundQueryClause.DefinedSymbol.GetPublicSymbol();
			}
			return null;
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x00075918 File Offset: 0x00073B18
		public override IRangeVariableSymbol GetDeclaredSymbol(QueryClauseSyntax queryClause, CancellationToken cancellationToken = default(CancellationToken))
		{
			BoundQueryClause boundQueryClause = this.GetBoundQueryClause(queryClause);
			if (boundQueryClause != null)
			{
				return boundQueryClause.DefinedSymbol.GetPublicSymbol();
			}
			return null;
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x00075940 File Offset: 0x00073B40
		public override IRangeVariableSymbol GetDeclaredSymbol(QueryContinuationSyntax node, CancellationToken cancellationToken = default(CancellationToken))
		{
			BoundQueryClause boundQueryClause = this.GetBoundQueryClause(node);
			if (boundQueryClause != null)
			{
				return boundQueryClause.DefinedSymbol.GetPublicSymbol();
			}
			return null;
		}

		// Token: 0x060015E2 RID: 5602 RVA: 0x00075968 File Offset: 0x00073B68
		public override AwaitExpressionInfo GetAwaitExpressionInfo(AwaitExpressionSyntax node)
		{
			if (node.Kind() != SyntaxKind.AwaitExpression)
			{
				throw new ArgumentException("node.Kind==" + node.Kind().ToString());
			}
			BoundNode upperBoundNode = this.GetUpperBoundNode(node, false);
			BoundExpressionStatement boundExpressionStatement = upperBoundNode as BoundExpressionStatement;
			BoundAwaitExpression boundAwaitExpression = (((boundExpressionStatement != null) ? boundExpressionStatement.Expression : null) ?? upperBoundNode) as BoundAwaitExpression;
			BoundAwaitableInfo boundAwaitableInfo = ((boundAwaitExpression != null) ? boundAwaitExpression.AwaitableInfo : null);
			if (boundAwaitableInfo == null)
			{
				return default(AwaitExpressionInfo);
			}
			BoundExpression getAwaiter = boundAwaitableInfo.GetAwaiter;
			return new AwaitExpressionInfo((IMethodSymbol)((getAwaiter != null) ? getAwaiter.ExpressionSymbol.GetPublicSymbol() : null), boundAwaitableInfo.IsCompleted.GetPublicSymbol(), boundAwaitableInfo.GetResult.GetPublicSymbol(), boundAwaitableInfo.IsDynamic);
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x00075A22 File Offset: 0x00073C22
		public override ForEachStatementInfo GetForEachStatementInfo(ForEachStatementSyntax node)
		{
			return this.GetForEachStatementInfo(node);
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x00075A2C File Offset: 0x00073C2C
		public override ForEachStatementInfo GetForEachStatementInfo(CommonForEachStatementSyntax node)
		{
			BoundForEachStatement boundForEachStatement = (BoundForEachStatement)this.GetUpperBoundNode(node, false);
			if (boundForEachStatement == null)
			{
				return default(ForEachStatementInfo);
			}
			ForEachEnumeratorInfo enumeratorInfoOpt = boundForEachStatement.EnumeratorInfoOpt;
			if (enumeratorInfoOpt == null)
			{
				return default(ForEachStatementInfo);
			}
			if (enumeratorInfoOpt.ElementType.IsPointerType())
			{
				return default(ForEachStatementInfo);
			}
			MethodSymbol methodSymbol = null;
			if (enumeratorInfoOpt.NeedsDisposal)
			{
				MethodArgumentInfo patternDisposeInfo = enumeratorInfoOpt.PatternDisposeInfo;
				if (patternDisposeInfo != null)
				{
					MethodSymbol method = patternDisposeInfo.Method;
					methodSymbol = method;
				}
				else
				{
					methodSymbol = (enumeratorInfoOpt.IsAsync ? ((MethodSymbol)this.Compilation.GetWellKnownTypeMember(WellKnownMember.System_IAsyncDisposable__DisposeAsync)) : ((MethodSymbol)this.Compilation.GetSpecialTypeMember(SpecialMember.System_IDisposable__Dispose)));
				}
			}
			bool isAsync = enumeratorInfoOpt.IsAsync;
			IMethodSymbol publicSymbol = enumeratorInfoOpt.GetEnumeratorInfo.Method.GetPublicSymbol();
			IMethodSymbol publicSymbol2 = enumeratorInfoOpt.MoveNextInfo.Method.GetPublicSymbol();
			MethodSymbol currentPropertyGetter = enumeratorInfoOpt.CurrentPropertyGetter;
			return new ForEachStatementInfo(isAsync, publicSymbol, publicSymbol2, ((PropertySymbol)((currentPropertyGetter != null) ? currentPropertyGetter.AssociatedSymbol : null)).GetPublicSymbol(), methodSymbol.GetPublicSymbol(), enumeratorInfoOpt.ElementType.GetPublicSymbol(), boundForEachStatement.ElementConversion, enumeratorInfoOpt.CurrentConversion);
		}

		// Token: 0x060015E5 RID: 5605 RVA: 0x00075B3C File Offset: 0x00073D3C
		public override DeconstructionInfo GetDeconstructionInfo(AssignmentExpressionSyntax node)
		{
			BoundDeconstructionAssignmentOperator boundDeconstructionAssignmentOperator = this.GetUpperBoundNode(node, false) as BoundDeconstructionAssignmentOperator;
			if (boundDeconstructionAssignmentOperator == null)
			{
				return default(DeconstructionInfo);
			}
			BoundConversion right = boundDeconstructionAssignmentOperator.Right;
			if (right == null)
			{
				return default(DeconstructionInfo);
			}
			return new DeconstructionInfo(right.Conversion);
		}

		// Token: 0x060015E6 RID: 5606 RVA: 0x00075B84 File Offset: 0x00073D84
		public override DeconstructionInfo GetDeconstructionInfo(ForEachVariableStatementSyntax node)
		{
			BoundForEachStatement boundForEachStatement = (BoundForEachStatement)this.GetUpperBoundNode(node, false);
			if (boundForEachStatement == null)
			{
				return default(DeconstructionInfo);
			}
			BoundForEachDeconstructStep deconstructionOpt = boundForEachStatement.DeconstructionOpt;
			if (deconstructionOpt == null)
			{
				return default(DeconstructionInfo);
			}
			return new DeconstructionInfo(deconstructionOpt.DeconstructionAssignment.Right.Conversion);
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x00075BD5 File Offset: 0x00073DD5
		private BoundQueryClause GetBoundQueryClause(CSharpSyntaxNode node)
		{
			base.CheckSyntaxNode(node);
			return this.GetLowerBoundNode(node) as BoundQueryClause;
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x00075BEC File Offset: 0x00073DEC
		private QueryClauseInfo GetQueryClauseInfo(BoundQueryClause bound)
		{
			if (bound == null)
			{
				return default(QueryClauseInfo);
			}
			SymbolInfo symbolInfo = ((bound.Cast == null) ? SymbolInfo.None : base.GetSymbolInfoForNode(CSharpSemanticModel.SymbolInfoOptions.DefaultOptions, bound.Cast, bound.Cast, null, null));
			SymbolInfo symbolInfoForQuery = this.GetSymbolInfoForQuery(bound);
			return new QueryClauseInfo(symbolInfo, symbolInfoForQuery);
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x00075C38 File Offset: 0x00073E38
		private SymbolInfo GetSymbolInfoForQuery(BoundQueryClause bound)
		{
			BoundCall boundCall = ((bound != null) ? bound.Operation : null) as BoundCall;
			if (boundCall == null)
			{
				return SymbolInfo.None;
			}
			BoundExpression boundExpression = (boundCall.IsDelegateCall ? boundCall.ReceiverOpt : boundCall);
			return base.GetSymbolInfoForNode(CSharpSemanticModel.SymbolInfoOptions.DefaultOptions, boundExpression, boundExpression, null, null);
		}

		// Token: 0x060015EA RID: 5610 RVA: 0x00075C7D File Offset: 0x00073E7D
		private CSharpTypeInfo GetTypeInfoForQuery(BoundQueryClause bound)
		{
			if (bound != null)
			{
				return base.GetTypeInfoForNode(bound, bound, bound);
			}
			return CSharpTypeInfo.None;
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x00075C94 File Offset: 0x00073E94
		public override QueryClauseInfo GetQueryClauseInfo(QueryClauseSyntax node, CancellationToken cancellationToken = default(CancellationToken))
		{
			BoundQueryClause boundQueryClause = this.GetBoundQueryClause(node);
			return this.GetQueryClauseInfo(boundQueryClause);
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x00075CB0 File Offset: 0x00073EB0
		public override IPropertySymbol GetDeclaredSymbol(AnonymousObjectMemberDeclaratorSyntax declaratorSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			base.CheckSyntaxNode(declaratorSyntax);
			AnonymousObjectCreationExpressionSyntax anonymousObjectCreationExpressionSyntax = (AnonymousObjectCreationExpressionSyntax)declaratorSyntax.Parent;
			if (anonymousObjectCreationExpressionSyntax == null)
			{
				return null;
			}
			BoundAnonymousObjectCreationExpression boundAnonymousObjectCreationExpression = this.GetLowerBoundNode(anonymousObjectCreationExpressionSyntax) as BoundAnonymousObjectCreationExpression;
			if (boundAnonymousObjectCreationExpression == null)
			{
				return null;
			}
			NamedTypeSymbol namedTypeSymbol = boundAnonymousObjectCreationExpression.Type as NamedTypeSymbol;
			if (namedTypeSymbol == null)
			{
				return null;
			}
			int num = anonymousObjectCreationExpressionSyntax.Initializers.IndexOf(declaratorSyntax);
			return AnonymousTypeManager.GetAnonymousTypeProperty(namedTypeSymbol, num).GetPublicSymbol();
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x00075D18 File Offset: 0x00073F18
		public override INamedTypeSymbol GetDeclaredSymbol(AnonymousObjectCreationExpressionSyntax declaratorSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			base.CheckSyntaxNode(declaratorSyntax);
			BoundAnonymousObjectCreationExpression boundAnonymousObjectCreationExpression = this.GetLowerBoundNode(declaratorSyntax) as BoundAnonymousObjectCreationExpression;
			if (boundAnonymousObjectCreationExpression != null)
			{
				return (boundAnonymousObjectCreationExpression.Type as NamedTypeSymbol).GetPublicSymbol();
			}
			return null;
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x00075D4E File Offset: 0x00073F4E
		public override INamedTypeSymbol GetDeclaredSymbol(TupleExpressionSyntax declaratorSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			base.CheckSyntaxNode(declaratorSyntax);
			return this.GetTypeOfTupleLiteral(declaratorSyntax).GetPublicSymbol();
		}

		// Token: 0x060015EF RID: 5615 RVA: 0x00075D64 File Offset: 0x00073F64
		public override ISymbol GetDeclaredSymbol(ArgumentSyntax declaratorSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			base.CheckSyntaxNode(declaratorSyntax);
			TupleExpressionSyntax tupleExpressionSyntax = ((declaratorSyntax != null) ? declaratorSyntax.Parent : null) as TupleExpressionSyntax;
			if (tupleExpressionSyntax == null)
			{
				return null;
			}
			NamedTypeSymbol typeOfTupleLiteral = this.GetTypeOfTupleLiteral(tupleExpressionSyntax);
			if (typeOfTupleLiteral != null)
			{
				ImmutableArray<FieldSymbol> tupleElements = typeOfTupleLiteral.TupleElements;
				if (!tupleElements.IsDefault)
				{
					int num = tupleExpressionSyntax.Arguments.IndexOf(declaratorSyntax);
					return tupleElements[num].GetPublicSymbol();
				}
			}
			return null;
		}

		// Token: 0x060015F0 RID: 5616 RVA: 0x00075DCA File Offset: 0x00073FCA
		private NamedTypeSymbol GetTypeOfTupleLiteral(TupleExpressionSyntax declaratorSyntax)
		{
			BoundTupleExpression boundTupleExpression = this.GetLowerBoundNode(declaratorSyntax) as BoundTupleExpression;
			return ((boundTupleExpression != null) ? boundTupleExpression.Type : null) as NamedTypeSymbol;
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x060015F1 RID: 5617 RVA: 0x00075DE9 File Offset: 0x00073FE9
		public override SyntaxTree SyntaxTree
		{
			get
			{
				return this._root.SyntaxTree;
			}
		}

		// Token: 0x060015F2 RID: 5618 RVA: 0x00075DF8 File Offset: 0x00073FF8
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal override IOperation GetOperationWorker(CSharpSyntaxNode node, CancellationToken cancellationToken)
		{
			MemberSemanticModel.<>c__DisplayClass99_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.node = node;
			using (this._nodeMapLock.DisposableRead())
			{
				if (this._guardedIOperationNodeMap.Count != 0)
				{
					return this.<GetOperationWorker>g__guardedGetIOperation|99_0(ref CS$<>8__locals1);
				}
			}
			IOperation rootOperation = this.GetRootOperation();
			IOperation operation;
			using (this._nodeMapLock.DisposableWrite())
			{
				if (this._guardedIOperationNodeMap.Count != 0)
				{
					operation = this.<GetOperationWorker>g__guardedGetIOperation|99_0(ref CS$<>8__locals1);
				}
				else
				{
					OperationMapBuilder.AddToMap(rootOperation, this._guardedIOperationNodeMap);
					operation = this.<GetOperationWorker>g__guardedGetIOperation|99_0(ref CS$<>8__locals1);
				}
			}
			return operation;
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x00075EBC File Offset: 0x000740BC
		private CSharpSyntaxNode GetBindingRootOrInitializer(CSharpSyntaxNode node)
		{
			CSharpSyntaxNode bindingRoot = this.GetBindingRoot(node);
			ParameterSyntax parameterSyntax = bindingRoot as ParameterSyntax;
			if (parameterSyntax != null)
			{
				EqualsValueClauseSyntax @default = parameterSyntax.Default;
				if (@default != null && @default.FullSpan.Contains(node.Span))
				{
					return parameterSyntax.Default;
				}
			}
			VariableDeclaratorSyntax variableDeclaratorSyntax = bindingRoot as VariableDeclaratorSyntax;
			if (variableDeclaratorSyntax != null)
			{
				EqualsValueClauseSyntax initializer = variableDeclaratorSyntax.Initializer;
				if (initializer != null && initializer.FullSpan.Contains(node.Span))
				{
					CSharpSyntaxNode parent = variableDeclaratorSyntax.Parent;
					if (parent == null || !parent.Parent.IsKind(SyntaxKind.FieldDeclaration))
					{
						CSharpSyntaxNode parent2 = variableDeclaratorSyntax.Parent;
						if (parent2 == null || !parent2.Parent.IsKind(SyntaxKind.EventFieldDeclaration))
						{
							goto IL_00AC;
						}
					}
					return variableDeclaratorSyntax.Initializer;
				}
			}
			IL_00AC:
			EnumMemberDeclarationSyntax enumMemberDeclarationSyntax = bindingRoot as EnumMemberDeclarationSyntax;
			if (enumMemberDeclarationSyntax != null)
			{
				EqualsValueClauseSyntax equalsValue = enumMemberDeclarationSyntax.EqualsValue;
				if (equalsValue != null && equalsValue.FullSpan.Contains(node.Span))
				{
					return enumMemberDeclarationSyntax.EqualsValue;
				}
			}
			PropertyDeclarationSyntax propertyDeclarationSyntax = bindingRoot as PropertyDeclarationSyntax;
			if (propertyDeclarationSyntax != null)
			{
				EqualsValueClauseSyntax initializer2 = propertyDeclarationSyntax.Initializer;
				if (initializer2 != null && initializer2.FullSpan.Contains(node.Span))
				{
					return propertyDeclarationSyntax.Initializer;
				}
			}
			return bindingRoot;
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x00075FE4 File Offset: 0x000741E4
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		private IOperation GetRootOperation()
		{
			BoundNode boundNode = this.GetBoundRoot();
			BoundGlobalStatementInitializer boundGlobalStatementInitializer = boundNode as BoundGlobalStatementInitializer;
			if (boundGlobalStatementInitializer != null)
			{
				BoundStatement statement = boundGlobalStatementInitializer.Statement;
				boundNode = statement;
			}
			IOperation operation = this._operationFactory.Value.Create(boundNode);
			Operation.SetParentOperation<IOperation>(operation, null);
			return operation;
		}

		// Token: 0x060015F5 RID: 5621 RVA: 0x00076024 File Offset: 0x00074224
		internal override SymbolInfo GetSymbolInfoWorker(CSharpSyntaxNode node, CSharpSemanticModel.SymbolInfoOptions options, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSemanticModel.ValidateSymbolInfoOptions(options);
			CSharpSyntaxNode csharpSyntaxNode;
			BoundNode boundNode;
			BoundNode boundNode2;
			BoundNode boundNode3;
			this.GetBoundNodes(node, out csharpSyntaxNode, out boundNode, out boundNode2, out boundNode3);
			return base.GetSymbolInfoForNode(options, boundNode, boundNode2, boundNode3, null);
		}

		// Token: 0x060015F6 RID: 5622 RVA: 0x00076054 File Offset: 0x00074254
		internal override CSharpTypeInfo GetTypeInfoWorker(CSharpSyntaxNode node, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSyntaxNode csharpSyntaxNode;
			BoundNode boundNode;
			BoundNode boundNode2;
			BoundNode boundNode3;
			this.GetBoundNodes(node, out csharpSyntaxNode, out boundNode, out boundNode2, out boundNode3);
			return base.GetTypeInfoForNode(boundNode, boundNode2, boundNode3);
		}

		// Token: 0x060015F7 RID: 5623 RVA: 0x0007607C File Offset: 0x0007427C
		internal override ImmutableArray<Symbol> GetMemberGroupWorker(CSharpSyntaxNode node, CSharpSemanticModel.SymbolInfoOptions options, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSyntaxNode csharpSyntaxNode;
			BoundNode boundNode;
			BoundNode boundNode2;
			BoundNode boundNode3;
			this.GetBoundNodes(node, out csharpSyntaxNode, out boundNode, out boundNode2, out boundNode3);
			return base.GetMemberGroupForNode(options, boundNode, boundNode3, null);
		}

		// Token: 0x060015F8 RID: 5624 RVA: 0x000760A4 File Offset: 0x000742A4
		internal override ImmutableArray<IPropertySymbol> GetIndexerGroupWorker(CSharpSyntaxNode node, CSharpSemanticModel.SymbolInfoOptions options, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSyntaxNode csharpSyntaxNode;
			BoundNode boundNode;
			BoundNode boundNode2;
			BoundNode boundNode3;
			this.GetBoundNodes(node, out csharpSyntaxNode, out boundNode, out boundNode2, out boundNode3);
			return base.GetIndexerGroupForNode(boundNode, null);
		}

		// Token: 0x060015F9 RID: 5625 RVA: 0x000760C8 File Offset: 0x000742C8
		internal override Optional<object> GetConstantValueWorker(CSharpSyntaxNode node, CancellationToken cancellationToken)
		{
			CSharpSyntaxNode bindableSyntaxNode = this.GetBindableSyntaxNode(node);
			BoundExpression boundExpression = this.GetLowerBoundNode(bindableSyntaxNode) as BoundExpression;
			if (boundExpression == null)
			{
				return default(Optional<object>);
			}
			ConstantValue constantValue = boundExpression.ConstantValue;
			if (!(constantValue == null) && !constantValue.IsBad)
			{
				return new Optional<object>(constantValue.Value);
			}
			return default(Optional<object>);
		}

		// Token: 0x060015FA RID: 5626 RVA: 0x00076124 File Offset: 0x00074324
		internal override SymbolInfo GetCollectionInitializerSymbolInfoWorker(InitializerExpressionSyntax collectionInitializer, ExpressionSyntax node, CancellationToken cancellationToken = default(CancellationToken))
		{
			BoundCollectionInitializerExpression boundCollectionInitializerExpression = this.GetLowerBoundNode(collectionInitializer) as BoundCollectionInitializerExpression;
			if (boundCollectionInitializerExpression != null)
			{
				BoundExpression boundExpression = boundCollectionInitializerExpression.Initializers[collectionInitializer.Expressions.IndexOf(node)];
				return base.GetSymbolInfoForNode(CSharpSemanticModel.SymbolInfoOptions.DefaultOptions, boundExpression, boundExpression, null, null);
			}
			return SymbolInfo.None;
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x00076170 File Offset: 0x00074370
		public override SymbolInfo GetSymbolInfo(OrderingSyntax node, CancellationToken cancellationToken = default(CancellationToken))
		{
			BoundQueryClause boundQueryClause = this.GetBoundQueryClause(node);
			return this.GetSymbolInfoForQuery(boundQueryClause);
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x0007618C File Offset: 0x0007438C
		public override SymbolInfo GetSymbolInfo(SelectOrGroupClauseSyntax node, CancellationToken cancellationToken = default(CancellationToken))
		{
			BoundQueryClause boundQueryClause = this.GetBoundQueryClause(node);
			return this.GetSymbolInfoForQuery(boundQueryClause);
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x000761A8 File Offset: 0x000743A8
		public override TypeInfo GetTypeInfo(SelectOrGroupClauseSyntax node, CancellationToken cancellationToken = default(CancellationToken))
		{
			BoundQueryClause boundQueryClause = this.GetBoundQueryClause(node);
			return this.GetTypeInfoForQuery(boundQueryClause);
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x000761CC File Offset: 0x000743CC
		private void GetBoundNodes(CSharpSyntaxNode node, out CSharpSyntaxNode bindableNode, out BoundNode lowestBoundNode, out BoundNode highestBoundNode, out BoundNode boundParent)
		{
			bindableNode = this.GetBindableSyntaxNode(node);
			CSharpSyntaxNode csharpSyntaxNode = this.GetBindableParentNode(bindableNode);
			if (csharpSyntaxNode != null && csharpSyntaxNode.Kind() == SyntaxKind.SimpleMemberAccessExpression && ((MemberAccessExpressionSyntax)csharpSyntaxNode).Expression == bindableNode)
			{
				csharpSyntaxNode = this.GetBindableParentNode(csharpSyntaxNode);
			}
			boundParent = ((csharpSyntaxNode == null) ? null : this.GetLowerBoundNode(csharpSyntaxNode));
			lowestBoundNode = this.GetLowerBoundNode(bindableNode);
			highestBoundNode = this.GetUpperBoundNode(bindableNode, false);
		}

		// Token: 0x060015FF RID: 5631 RVA: 0x00076238 File Offset: 0x00074438
		private CSharpSyntaxNode GetInnermostLambdaOrQuery(CSharpSyntaxNode node, int position, bool allowStarting = false)
		{
			for (CSharpSyntaxNode csharpSyntaxNode = node; csharpSyntaxNode != this.Root; csharpSyntaxNode = csharpSyntaxNode.ParentOrStructuredTriviaParent)
			{
				if ((csharpSyntaxNode.IsAnonymousFunction() || csharpSyntaxNode.IsQuery()) && LookupPosition.IsInAnonymousFunctionOrQuery(position, csharpSyntaxNode) && (allowStarting || csharpSyntaxNode != node))
				{
					return csharpSyntaxNode;
				}
			}
			return null;
		}

		// Token: 0x06001600 RID: 5632 RVA: 0x0007627C File Offset: 0x0007447C
		private void GuardedAddSynthesizedStatementToMap(StatementSyntax node, BoundStatement statement)
		{
			if (this._lazyGuardedSynthesizedStatementsMap == null)
			{
				this._lazyGuardedSynthesizedStatementsMap = new Dictionary<SyntaxNode, BoundStatement>();
			}
			this._lazyGuardedSynthesizedStatementsMap.Add(node, statement);
		}

		// Token: 0x06001601 RID: 5633 RVA: 0x000762A0 File Offset: 0x000744A0
		private BoundStatement GuardedGetSynthesizedStatementFromMap(StatementSyntax node)
		{
			BoundStatement boundStatement;
			if (this._lazyGuardedSynthesizedStatementsMap != null && this._lazyGuardedSynthesizedStatementsMap.TryGetValue(node, ref boundStatement))
			{
				return boundStatement;
			}
			return null;
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x000762C8 File Offset: 0x000744C8
		private ImmutableArray<BoundNode> GuardedGetBoundNodesFromMap(CSharpSyntaxNode node)
		{
			ImmutableArray<BoundNode> immutableArray;
			if (!this._guardedBoundNodeMap.TryGetValue(node, ref immutableArray))
			{
				return default(ImmutableArray<BoundNode>);
			}
			return immutableArray;
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x000762F0 File Offset: 0x000744F0
		internal ImmutableArray<BoundNode> TestOnlyTryGetBoundNodesFromMap(CSharpSyntaxNode node)
		{
			ImmutableArray<BoundNode> immutableArray;
			if (!this._guardedBoundNodeMap.TryGetValue(node, ref immutableArray))
			{
				return default(ImmutableArray<BoundNode>);
			}
			return immutableArray;
		}

		// Token: 0x06001604 RID: 5636 RVA: 0x00076318 File Offset: 0x00074518
		private ImmutableArray<BoundNode> GuardedAddBoundTreeAndGetBoundNodeFromMap(CSharpSyntaxNode syntax, BoundNode bound)
		{
			bool flag = false;
			if (bound != null)
			{
				flag = this._guardedBoundNodeMap.ContainsKey(bound.Syntax);
			}
			if (!flag)
			{
				MemberSemanticModel.NodeMapBuilder.AddToMap(bound, this._guardedBoundNodeMap, this.SyntaxTree, null);
			}
			ImmutableArray<BoundNode> immutableArray;
			if (!this._guardedBoundNodeMap.TryGetValue(syntax, ref immutableArray))
			{
				return default(ImmutableArray<BoundNode>);
			}
			return immutableArray;
		}

		// Token: 0x06001605 RID: 5637 RVA: 0x00076370 File Offset: 0x00074570
		protected void UnguardedAddBoundTreeForStandaloneSyntax(SyntaxNode syntax, BoundNode bound, NullableWalker.SnapshotManager manager = null, ImmutableDictionary<Symbol, Symbol> remappedSymbols = null)
		{
			using (this._nodeMapLock.DisposableWrite())
			{
				this.GuardedAddBoundTreeForStandaloneSyntax(syntax, bound, manager, remappedSymbols);
			}
		}

		// Token: 0x06001606 RID: 5638 RVA: 0x000763B4 File Offset: 0x000745B4
		protected void GuardedAddBoundTreeForStandaloneSyntax(SyntaxNode syntax, BoundNode bound, NullableWalker.SnapshotManager manager = null, ImmutableDictionary<Symbol, Symbol> remappedSymbols = null)
		{
			bool flag = false;
			if (bound != null)
			{
				flag = this._guardedBoundNodeMap.ContainsKey(bound.Syntax);
			}
			if (!flag)
			{
				if (syntax == this._root || syntax is StatementSyntax)
				{
					MemberSemanticModel.NodeMapBuilder.AddToMap(bound, this._guardedBoundNodeMap, this.SyntaxTree, null);
				}
				else
				{
					MemberSemanticModel.NodeMapBuilder.AddToMap(bound, this._guardedBoundNodeMap, this.SyntaxTree, syntax);
				}
				if (manager != null)
				{
					this._lazySnapshotManager = manager;
					this._lazyRemappedSymbols = remappedSymbols;
				}
			}
		}

		// Token: 0x06001607 RID: 5639 RVA: 0x00076428 File Offset: 0x00074628
		private CSharpSyntaxNode GetBindingRoot(CSharpSyntaxNode node)
		{
			CSharpSyntaxNode csharpSyntaxNode = node;
			while (csharpSyntaxNode != this.Root)
			{
				if (csharpSyntaxNode is StatementSyntax)
				{
					return csharpSyntaxNode;
				}
				SyntaxKind syntaxKind = csharpSyntaxNode.Kind();
				if (syntaxKind - SyntaxKind.BaseConstructorInitializer > 1)
				{
					if (syntaxKind != SyntaxKind.ArrowExpressionClause)
					{
						if (syntaxKind == SyntaxKind.PrimaryConstructorBaseType)
						{
							return csharpSyntaxNode;
						}
					}
					else if (csharpSyntaxNode.Parent == null || csharpSyntaxNode.Parent.Kind() != SyntaxKind.LocalFunctionStatement)
					{
						return csharpSyntaxNode;
					}
					csharpSyntaxNode = csharpSyntaxNode.ParentOrStructuredTriviaParent;
					continue;
				}
				return csharpSyntaxNode;
			}
			return this.Root;
		}

		// Token: 0x06001608 RID: 5640 RVA: 0x00076498 File Offset: 0x00074698
		internal override Binder GetEnclosingBinderInternal(int position)
		{
			if (!this.Root.FullSpan.Contains(position))
			{
				return this.RootBinder;
			}
			CSharpSyntaxNode csharpSyntaxNode = (CSharpSyntaxNode)this.Root.FindToken(position, false).Parent;
			return this.GetEnclosingBinderInternal(csharpSyntaxNode, position);
		}

		// Token: 0x06001609 RID: 5641 RVA: 0x000764E8 File Offset: 0x000746E8
		private Binder GetEnclosingBinderInternal(CSharpSyntaxNode node, int position)
		{
			CSharpSyntaxNode innermostLambdaOrQuery = this.GetInnermostLambdaOrQuery(node, position, true);
			if (innermostLambdaOrQuery == null)
			{
				return this.GetEnclosingBinderInternalWithinRoot(node, position);
			}
			BoundNode boundLambdaOrQuery = this.GetBoundLambdaOrQuery(innermostLambdaOrQuery);
			return this.GetEnclosingBinderInLambdaOrQuery(position, node, innermostLambdaOrQuery, ref boundLambdaOrQuery);
		}

		// Token: 0x0600160A RID: 5642 RVA: 0x00076520 File Offset: 0x00074720
		private BoundNode GetBoundLambdaOrQuery(CSharpSyntaxNode lambdaOrQuery)
		{
			this.EnsureNullabilityAnalysisPerformedIfNecessary();
			ImmutableArray<BoundNode> immutableArray;
			using (this._nodeMapLock.DisposableRead())
			{
				immutableArray = this.GuardedGetBoundNodesFromMap(lambdaOrQuery);
			}
			if (!immutableArray.IsDefaultOrEmpty)
			{
				return MemberSemanticModel.GetLowerBoundNode(immutableArray);
			}
			CSharpSyntaxNode bindingRoot = this.GetBindingRoot(lambdaOrQuery);
			CSharpSyntaxNode innermostLambdaOrQuery = this.GetInnermostLambdaOrQuery(lambdaOrQuery, lambdaOrQuery.SpanStart, false);
			BoundNode boundNode = null;
			CSharpSyntaxNode csharpSyntaxNode;
			Binder binder;
			if (innermostLambdaOrQuery == null)
			{
				csharpSyntaxNode = bindingRoot;
				binder = this.GetEnclosingBinderInternalWithinRoot(csharpSyntaxNode, base.GetAdjustedNodePosition(csharpSyntaxNode));
			}
			else
			{
				if (innermostLambdaOrQuery == bindingRoot || !innermostLambdaOrQuery.Contains(bindingRoot))
				{
					csharpSyntaxNode = lambdaOrQuery;
				}
				else
				{
					csharpSyntaxNode = bindingRoot;
				}
				boundNode = this.GetBoundLambdaOrQuery(innermostLambdaOrQuery);
				using (this._nodeMapLock.DisposableRead())
				{
					immutableArray = this.GuardedGetBoundNodesFromMap(lambdaOrQuery);
				}
				if (!immutableArray.IsDefaultOrEmpty)
				{
					return MemberSemanticModel.GetLowerBoundNode(immutableArray);
				}
				binder = this.GetEnclosingBinderInLambdaOrQuery(base.GetAdjustedNodePosition(csharpSyntaxNode), csharpSyntaxNode, innermostLambdaOrQuery, ref boundNode);
			}
			Binder binder2 = new MemberSemanticModel.IncrementalBinder(this, binder);
			using (this._nodeMapLock.DisposableWrite())
			{
				BoundNode boundNode2 = this.Bind(binder2, csharpSyntaxNode, BindingDiagnosticBag.Discarded);
				immutableArray = this.GuardedAddBoundTreeAndGetBoundNodeFromMap(lambdaOrQuery, boundNode2);
			}
			if (!immutableArray.IsDefaultOrEmpty)
			{
				return MemberSemanticModel.GetLowerBoundNode(immutableArray);
			}
			if (innermostLambdaOrQuery == null)
			{
				binder = this.GetEnclosingBinderInternalWithinRoot(lambdaOrQuery, base.GetAdjustedNodePosition(lambdaOrQuery));
			}
			else
			{
				binder = this.GetEnclosingBinderInLambdaOrQuery(base.GetAdjustedNodePosition(lambdaOrQuery), lambdaOrQuery, innermostLambdaOrQuery, ref boundNode);
			}
			binder2 = new MemberSemanticModel.IncrementalBinder(this, binder);
			using (this._nodeMapLock.DisposableWrite())
			{
				BoundNode boundNode3 = this.Bind(binder2, lambdaOrQuery, BindingDiagnosticBag.Discarded);
				if (!this.IsNullableAnalysisEnabled() && this.Compilation.IsNullableAnalysisEnabledAlways)
				{
					this.AnalyzeBoundNodeNullability(boundNode3, binder2, new DiagnosticBag(), false);
				}
				immutableArray = this.GuardedAddBoundTreeAndGetBoundNodeFromMap(lambdaOrQuery, boundNode3);
			}
			return MemberSemanticModel.GetLowerBoundNode(immutableArray);
		}

		// Token: 0x0600160B RID: 5643 RVA: 0x00076718 File Offset: 0x00074918
		private Binder GetEnclosingBinderInLambdaOrQuery(int position, CSharpSyntaxNode node, CSharpSyntaxNode innerLambdaOrQuery, ref BoundNode boundInnerLambdaOrQuery)
		{
			Binder binder;
			switch (boundInnerLambdaOrQuery.Kind)
			{
			case BoundKind.Lambda:
				break;
			case BoundKind.UnboundLambda:
				boundInnerLambdaOrQuery = ((UnboundLambda)boundInnerLambdaOrQuery).BindForErrorRecovery();
				break;
			case BoundKind.QueryClause:
				binder = MemberSemanticModel.GetQueryEnclosingBinder(position, node, (BoundQueryClause)boundInnerLambdaOrQuery);
				goto IL_0066;
			default:
				return this.GetEnclosingBinderInternalWithinRoot(node, position);
			}
			binder = MemberSemanticModel.GetLambdaEnclosingBinder(position, node, innerLambdaOrQuery, ((BoundLambda)boundInnerLambdaOrQuery).Binder);
			IL_0066:
			return binder.WithAdditionalFlags(base.GetSemanticModelBinderFlags());
		}

		// Token: 0x0600160C RID: 5644 RVA: 0x00076798 File Offset: 0x00074998
		private static Binder GetQueryEnclosingBinder(int position, CSharpSyntaxNode startingNode, BoundQueryClause queryClause)
		{
			BoundExpression boundExpression = queryClause;
			do
			{
				BoundKind kind = boundExpression.Kind;
				if (kind != BoundKind.Conversion)
				{
					if (kind != BoundKind.Call)
					{
						switch (kind)
						{
						case BoundKind.Lambda:
							goto IL_00E2;
						case BoundKind.UnboundLambda:
							goto IL_00B7;
						case BoundKind.QueryClause:
							queryClause = (BoundQueryClause)boundExpression;
							boundExpression = MemberSemanticModel.GetQueryClauseValue(queryClause);
							goto IL_0116;
						}
						break;
					}
					BoundCall boundCall = (BoundCall)boundExpression;
					boundExpression = MemberSemanticModel.GetContainingArgument(boundCall.Arguments, position);
					if (boundExpression == null)
					{
						BoundExpression boundExpression2 = boundCall.ReceiverOpt;
						while (boundExpression2 != null && boundExpression2.Kind == BoundKind.MethodGroup)
						{
							boundExpression2 = ((BoundMethodGroup)boundExpression2).ReceiverOpt;
						}
						if (boundExpression2 != null)
						{
							boundExpression = MemberSemanticModel.GetContainingExprOrQueryClause(boundExpression2, position);
							if (boundExpression != null)
							{
								goto IL_0116;
							}
						}
						boundExpression = boundCall.Arguments.LastOrDefault<BoundExpression>();
					}
				}
				else
				{
					boundExpression = ((BoundConversion)boundExpression).Operand;
				}
				IL_0116:;
			}
			while (boundExpression != null);
			goto IL_011C;
			IL_00B7:
			UnboundLambda unboundLambda = (UnboundLambda)boundExpression;
			return MemberSemanticModel.GetEnclosingBinderInternalWithinRoot(MemberSemanticModel.AdjustStartingNodeAccordingToNewRoot(startingNode, unboundLambda.Syntax), position, unboundLambda.BindForErrorRecovery().Binder, unboundLambda.Syntax);
			IL_00E2:
			BoundLambda boundLambda = (BoundLambda)boundExpression;
			return MemberSemanticModel.GetEnclosingBinderInternalWithinRoot(MemberSemanticModel.AdjustStartingNodeAccordingToNewRoot(startingNode, boundLambda.Body.Syntax), position, boundLambda.Binder, boundLambda.Body.Syntax);
			IL_011C:
			return MemberSemanticModel.GetEnclosingBinderInternalWithinRoot(MemberSemanticModel.AdjustStartingNodeAccordingToNewRoot(startingNode, queryClause.Syntax), position, queryClause.Binder, queryClause.Syntax);
		}

		// Token: 0x0600160D RID: 5645 RVA: 0x000768E0 File Offset: 0x00074AE0
		private static BoundExpression GetContainingArgument(ImmutableArray<BoundExpression> arguments, int position)
		{
			BoundExpression boundExpression = null;
			TextSpan textSpan = default(TextSpan);
			foreach (BoundExpression boundExpression2 in arguments)
			{
				BoundExpression containingExprOrQueryClause = MemberSemanticModel.GetContainingExprOrQueryClause(boundExpression2, position);
				if (containingExprOrQueryClause != null)
				{
					TextSpan fullSpan = containingExprOrQueryClause.Syntax.FullSpan;
					if (boundExpression == null || textSpan.Contains(fullSpan))
					{
						boundExpression = containingExprOrQueryClause;
						textSpan = fullSpan;
					}
				}
			}
			return boundExpression;
		}

		// Token: 0x0600160E RID: 5646 RVA: 0x0007693C File Offset: 0x00074B3C
		private static BoundExpression GetContainingExprOrQueryClause(BoundExpression expr, int position)
		{
			if (expr.Kind == BoundKind.QueryClause)
			{
				BoundExpression queryClauseValue = MemberSemanticModel.GetQueryClauseValue((BoundQueryClause)expr);
				if (queryClauseValue.Syntax.FullSpan.Contains(position))
				{
					return queryClauseValue;
				}
			}
			if (expr.Syntax.FullSpan.Contains(position))
			{
				return expr;
			}
			return null;
		}

		// Token: 0x0600160F RID: 5647 RVA: 0x00076993 File Offset: 0x00074B93
		private static BoundExpression GetQueryClauseValue(BoundQueryClause queryClause)
		{
			return queryClause.UnoptimizedForm ?? queryClause.Value;
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x000769A8 File Offset: 0x00074BA8
		private static SyntaxNode AdjustStartingNodeAccordingToNewRoot(SyntaxNode startingNode, SyntaxNode root)
		{
			SyntaxNode syntaxNode = (startingNode.Contains(root) ? root : startingNode);
			if (syntaxNode != root && !root.Contains(syntaxNode))
			{
				syntaxNode = root;
			}
			return syntaxNode;
		}

		// Token: 0x06001611 RID: 5649 RVA: 0x000769D3 File Offset: 0x00074BD3
		private static Binder GetLambdaEnclosingBinder(int position, CSharpSyntaxNode startingNode, CSharpSyntaxNode containingLambda, Binder lambdaBinder)
		{
			return MemberSemanticModel.GetEnclosingBinderInternalWithinRoot(startingNode, position, lambdaBinder, containingLambda);
		}

		// Token: 0x06001612 RID: 5650 RVA: 0x000769E0 File Offset: 0x00074BE0
		protected void EnsureNullabilityAnalysisPerformedIfNecessary()
		{
			MemberSemanticModel.<>c__DisplayClass131_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			bool flag = this.IsNullableAnalysisEnabled();
			if (!flag && !this.Compilation.IsNullableAnalysisEnabledAlways)
			{
				return;
			}
			if (this._lazySnapshotManager != null)
			{
				return;
			}
			CS$<>8__locals1.bindableRoot = this.GetBindableSyntaxNode(this.Root);
			using (ReaderWriterLockSlimExtensions.UpgradeableReadLockExiter upgradeableReadLockExiter = this._nodeMapLock.DisposableUpgradeableRead())
			{
				if (this._guardedBoundNodeMap.Count <= 0)
				{
					upgradeableReadLockExiter.EnterWrite();
					CS$<>8__locals1.remappedSymbols = this._parentRemappedSymbolsOpt;
					CS$<>8__locals1.boundRoot = this.<EnsureNullabilityAnalysisPerformedIfNecessary>g__bind|131_0(CS$<>8__locals1.bindableRoot, out CS$<>8__locals1.binder, ref CS$<>8__locals1);
					if (this.IsSpeculativeSemanticModel)
					{
						if (this._parentSnapshotManagerOpt == null || !flag)
						{
							this.<EnsureNullabilityAnalysisPerformedIfNecessary>g__rewriteAndCache|131_1(ref CS$<>8__locals1);
						}
						else
						{
							NullableWalker.SnapshotManager snapshotManager;
							CS$<>8__locals1.boundRoot = NullableWalker.AnalyzeAndRewriteSpeculation(this._speculatedPosition, CS$<>8__locals1.boundRoot, CS$<>8__locals1.binder, this._parentSnapshotManagerOpt, out snapshotManager, ref CS$<>8__locals1.remappedSymbols);
							this.GuardedAddBoundTreeForStandaloneSyntax(CS$<>8__locals1.bindableRoot, CS$<>8__locals1.boundRoot, snapshotManager, CS$<>8__locals1.remappedSymbols);
						}
					}
					else
					{
						this.<EnsureNullabilityAnalysisPerformedIfNecessary>g__rewriteAndCache|131_1(ref CS$<>8__locals1);
					}
				}
			}
		}

		// Token: 0x06001613 RID: 5651
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		protected abstract BoundNode RewriteNullableBoundNodesWithSnapshots(BoundNode boundRoot, Binder binder, DiagnosticBag diagnostics, bool createSnapshots, [global::System.Runtime.CompilerServices.Nullable(2)] out NullableWalker.SnapshotManager snapshotManager, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1, 1 })] ref ImmutableDictionary<Symbol, Symbol> remappedSymbols);

		// Token: 0x06001614 RID: 5652
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		protected abstract void AnalyzeBoundNodeNullability(BoundNode boundRoot, Binder binder, DiagnosticBag diagnostics, bool createSnapshots);

		// Token: 0x06001615 RID: 5653
		protected abstract bool IsNullableAnalysisEnabled();

		// Token: 0x06001616 RID: 5654 RVA: 0x00076B08 File Offset: 0x00074D08
		internal ImmutableArray<BoundNode> GetBoundNodes(CSharpSyntaxNode node)
		{
			if (node == null)
			{
				node = this.GetBindableSyntaxNode(this.Root);
			}
			this.EnsureNullabilityAnalysisPerformedIfNecessary();
			ImmutableArray<BoundNode> immutableArray;
			using (this._nodeMapLock.DisposableRead())
			{
				immutableArray = this.GuardedGetBoundNodesFromMap(node);
			}
			if (!immutableArray.IsDefaultOrEmpty)
			{
				return immutableArray;
			}
			CSharpSyntaxNode bindingRoot = this.GetBindingRoot(node);
			Binder enclosingBinder = base.GetEnclosingBinder(base.GetAdjustedNodePosition(bindingRoot));
			Binder binder = new MemberSemanticModel.IncrementalBinder(this, enclosingBinder);
			using (this._nodeMapLock.DisposableWrite())
			{
				BoundNode boundNode = this.Bind(binder, bindingRoot, BindingDiagnosticBag.Discarded);
				immutableArray = this.GuardedAddBoundTreeAndGetBoundNodeFromMap(node, boundNode);
			}
			if (!immutableArray.IsDefaultOrEmpty)
			{
				return immutableArray;
			}
			Binder enclosingBinder2 = base.GetEnclosingBinder(base.GetAdjustedNodePosition(node));
			binder = new MemberSemanticModel.IncrementalBinder(this, enclosingBinder2);
			using (this._nodeMapLock.DisposableRead())
			{
				immutableArray = this.GuardedGetBoundNodesFromMap(node);
			}
			if (!immutableArray.IsDefaultOrEmpty)
			{
				return immutableArray;
			}
			using (this._nodeMapLock.DisposableWrite())
			{
				BoundNode boundNode2 = this.Bind(binder, node, BindingDiagnosticBag.Discarded);
				this.GuardedAddBoundTreeForStandaloneSyntax(node, boundNode2, null, null);
				immutableArray = this.GuardedGetBoundNodesFromMap(node);
			}
			if (!immutableArray.IsDefaultOrEmpty)
			{
				return immutableArray;
			}
			return ImmutableArray<BoundNode>.Empty;
		}

		// Token: 0x06001617 RID: 5655 RVA: 0x00076C88 File Offset: 0x00074E88
		protected internal virtual CSharpSyntaxNode GetBindableSyntaxNode(CSharpSyntaxNode node)
		{
			SyntaxKind syntaxKind = node.Kind();
			if (syntaxKind <= SyntaxKind.DestructorDeclaration)
			{
				if (syntaxKind == SyntaxKind.GlobalStatement || syntaxKind - SyntaxKind.MethodDeclaration <= 3 || syntaxKind == SyntaxKind.DestructorDeclaration)
				{
					return node;
				}
			}
			else if (syntaxKind <= SyntaxKind.Subpattern)
			{
				if (syntaxKind - SyntaxKind.GetAccessorDeclaration <= 3 || syntaxKind == SyntaxKind.Subpattern)
				{
					return node;
				}
			}
			else
			{
				if (syntaxKind == SyntaxKind.PositionalPatternClause)
				{
					return node.Parent;
				}
				if (syntaxKind == SyntaxKind.InitAccessorDeclaration)
				{
					return node;
				}
			}
			for (;;)
			{
				ParenthesizedExpressionSyntax parenthesizedExpressionSyntax = node as ParenthesizedExpressionSyntax;
				if (parenthesizedExpressionSyntax == null)
				{
					CheckedExpressionSyntax checkedExpressionSyntax = node as CheckedExpressionSyntax;
					if (checkedExpressionSyntax == null)
					{
						PostfixUnaryExpressionSyntax postfixUnaryExpressionSyntax = node as PostfixUnaryExpressionSyntax;
						if (postfixUnaryExpressionSyntax != null)
						{
							if (node.RawKind != 9054)
							{
								break;
							}
							node = postfixUnaryExpressionSyntax.Operand;
						}
						else
						{
							UnsafeStatementSyntax unsafeStatementSyntax = node as UnsafeStatementSyntax;
							if (unsafeStatementSyntax == null)
							{
								CheckedStatementSyntax checkedStatementSyntax = node as CheckedStatementSyntax;
								if (checkedStatementSyntax == null)
								{
									break;
								}
								node = checkedStatementSyntax.Block;
							}
							else
							{
								node = unsafeStatementSyntax.Block;
							}
						}
					}
					else
					{
						node = checkedExpressionSyntax.Expression;
					}
				}
				else
				{
					node = parenthesizedExpressionSyntax.Expression;
				}
			}
			CSharpSyntaxNode parent = node.Parent;
			if (parent != null && node != this.Root)
			{
				syntaxKind = node.Kind();
				if (syntaxKind <= SyntaxKind.GenericName)
				{
					if (syntaxKind == SyntaxKind.IdentifierName || syntaxKind == SyntaxKind.GenericName)
					{
						CSharpSyntaxNode standaloneNode = SyntaxFactory.GetStandaloneNode(node);
						if (standaloneNode != node)
						{
							return this.GetBindableSyntaxNode(standaloneNode);
						}
						return node;
					}
				}
				else
				{
					if (syntaxKind == SyntaxKind.AnonymousObjectMemberDeclarator)
					{
						return this.GetBindableSyntaxNode(parent);
					}
					if (syntaxKind == SyntaxKind.VariableDeclarator)
					{
						CSharpSyntaxNode parent2 = parent.Parent;
						if (parent2 != null && parent2.Kind() == SyntaxKind.LocalDeclarationStatement && ((VariableDeclarationSyntax)parent).Variables.Count == 1)
						{
							return this.GetBindableSyntaxNode(parent);
						}
						return node;
					}
				}
				if ((node is QueryExpressionSyntax && parent is QueryContinuationSyntax) || (!(node is ExpressionSyntax) && !(node is StatementSyntax) && !(node is SelectOrGroupClauseSyntax) && !(node is QueryClauseSyntax) && !(node is OrderingSyntax) && !(node is JoinIntoClauseSyntax) && !(node is QueryContinuationSyntax) && !(node is ConstructorInitializerSyntax) && !(node is PrimaryConstructorBaseTypeSyntax) && !(node is ArrowExpressionClauseSyntax) && !(node is PatternSyntax)))
				{
					return this.GetBindableSyntaxNode(parent);
				}
			}
			return node;
		}

		// Token: 0x06001618 RID: 5656 RVA: 0x00076E90 File Offset: 0x00075090
		protected CSharpSyntaxNode GetBindableParentNode(CSharpSyntaxNode node)
		{
			if (!(node is ExpressionSyntax))
			{
				return null;
			}
			CSharpSyntaxNode csharpSyntaxNode = node.Parent;
			if (csharpSyntaxNode != null)
			{
				for (;;)
				{
					SyntaxKind syntaxKind = csharpSyntaxNode.Kind();
					if (syntaxKind != SyntaxKind.ParenthesizedExpression && syntaxKind - SyntaxKind.RefExpression > 1)
					{
						break;
					}
					CSharpSyntaxNode parent = csharpSyntaxNode.Parent;
					if (parent != null)
					{
						csharpSyntaxNode = parent;
					}
				}
				CSharpSyntaxNode csharpSyntaxNode2 = this.GetBindableSyntaxNode(csharpSyntaxNode);
				if (csharpSyntaxNode2.Kind() == SyntaxKind.SimpleMemberAccessExpression && csharpSyntaxNode2.Parent.Kind() == SyntaxKind.InvocationExpression)
				{
					csharpSyntaxNode2 = csharpSyntaxNode2.Parent;
				}
				else if (csharpSyntaxNode2.Kind() == SyntaxKind.ArrayType)
				{
					csharpSyntaxNode2 = SyntaxFactory.GetStandaloneExpression((ArrayTypeSyntax)csharpSyntaxNode2);
				}
				return csharpSyntaxNode2;
			}
			if (this.IsSpeculativeSemanticModel && this.Root == node)
			{
				return null;
			}
			throw new ArgumentException("The parent of node must not be null unless this is a speculative semantic model.", "node");
		}

		// Token: 0x06001619 RID: 5657 RVA: 0x00076F48 File Offset: 0x00075148
		internal override Symbol RemapSymbolIfNecessaryCore(Symbol symbol)
		{
			this.EnsureNullabilityAnalysisPerformedIfNecessary();
			if (this._lazyRemappedSymbols == null)
			{
				return symbol;
			}
			Symbol symbol2;
			if (this._lazyRemappedSymbols.TryGetValue(symbol, out symbol2))
			{
				return symbol2;
			}
			return symbol;
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x00076F78 File Offset: 0x00075178
		internal sealed override Func<SyntaxNode, bool> GetSyntaxNodesToAnalyzeFilter(SyntaxNode declaredNode, ISymbol declaredSymbol)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x00076F88 File Offset: 0x00075188
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[CompilerGenerated]
		private IOperation <GetOperationWorker>g__guardedGetIOperation|99_0(ref MemberSemanticModel.<>c__DisplayClass99_0 A_1)
		{
			this._nodeMapLock.AssertCanRead();
			IOperation operation;
			if (!this._guardedIOperationNodeMap.TryGetValue(A_1.node, ref operation))
			{
				return null;
			}
			return operation;
		}

		// Token: 0x0600161D RID: 5661 RVA: 0x00076FB8 File Offset: 0x000751B8
		[CompilerGenerated]
		private BoundNode <EnsureNullabilityAnalysisPerformedIfNecessary>g__bind|131_0(CSharpSyntaxNode root, out Binder binder, ref MemberSemanticModel.<>c__DisplayClass131_0 A_3)
		{
			if (root is CompilationUnitSyntax)
			{
				binder = this.RootBinder.GetBinder(root);
			}
			else
			{
				binder = base.GetEnclosingBinder(base.GetAdjustedNodePosition(root));
			}
			return this.Bind(binder, root, BindingDiagnosticBag.Discarded);
		}

		// Token: 0x0600161E RID: 5662 RVA: 0x00076FF0 File Offset: 0x000751F0
		[CompilerGenerated]
		private void <EnsureNullabilityAnalysisPerformedIfNecessary>g__rewriteAndCache|131_1(ref MemberSemanticModel.<>c__DisplayClass131_0 A_1)
		{
			DiagnosticBag instance = DiagnosticBag.GetInstance();
			A_1.boundRoot = this.RewriteNullableBoundNodesWithSnapshots(A_1.boundRoot, A_1.binder, instance, true, out A_1.snapshotManager, ref A_1.remappedSymbols);
			instance.Free();
			this.GuardedAddBoundTreeForStandaloneSyntax(A_1.bindableRoot, A_1.boundRoot, A_1.snapshotManager, A_1.remappedSymbols);
		}

		// Token: 0x04000632 RID: 1586
		private readonly Symbol _memberSymbol;

		// Token: 0x04000633 RID: 1587
		private readonly CSharpSyntaxNode _root;

		// Token: 0x04000634 RID: 1588
		private readonly ReaderWriterLockSlim _nodeMapLock = new ReaderWriterLockSlim(0);

		// Token: 0x04000635 RID: 1589
		private readonly Dictionary<SyntaxNode, ImmutableArray<BoundNode>> _guardedBoundNodeMap = new Dictionary<SyntaxNode, ImmutableArray<BoundNode>>();

		// Token: 0x04000636 RID: 1590
		private readonly Dictionary<SyntaxNode, IOperation> _guardedIOperationNodeMap = new Dictionary<SyntaxNode, IOperation>();

		// Token: 0x04000637 RID: 1591
		private Dictionary<SyntaxNode, BoundStatement> _lazyGuardedSynthesizedStatementsMap;

		// Token: 0x04000638 RID: 1592
		private NullableWalker.SnapshotManager _lazySnapshotManager;

		// Token: 0x04000639 RID: 1593
		private ImmutableDictionary<Symbol, Symbol> _lazyRemappedSymbols;

		// Token: 0x0400063A RID: 1594
		private readonly ImmutableDictionary<Symbol, Symbol> _parentRemappedSymbolsOpt;

		// Token: 0x0400063B RID: 1595
		private readonly NullableWalker.SnapshotManager _parentSnapshotManagerOpt;

		// Token: 0x0400063C RID: 1596
		internal readonly Binder RootBinder;

		// Token: 0x0400063D RID: 1597
		private readonly SyntaxTreeSemanticModel _containingSemanticModelOpt;

		// Token: 0x0400063E RID: 1598
		private readonly SyntaxTreeSemanticModel _parentSemanticModelOpt;

		// Token: 0x0400063F RID: 1599
		private readonly int _speculatedPosition;

		// Token: 0x04000640 RID: 1600
		private readonly Lazy<CSharpOperationFactory> _operationFactory;

		// Token: 0x020006B5 RID: 1717
		internal class IncrementalBinder : Binder
		{
			// Token: 0x06008419 RID: 33817 RVA: 0x001DEF3D File Offset: 0x001DD13D
			internal IncrementalBinder(MemberSemanticModel semanticModel, Binder next)
				: base(next, null)
			{
				this._semanticModel = semanticModel;
			}

			// Token: 0x0600841A RID: 33818 RVA: 0x001DEF50 File Offset: 0x001DD150
			internal override Binder GetBinder(SyntaxNode node)
			{
				Binder binder = base.Next.GetBinder(node);
				if (binder != null)
				{
					return new MemberSemanticModel.IncrementalBinder(this._semanticModel, binder.WithAdditionalFlags(BinderFlags.SemanticModel));
				}
				return null;
			}

			// Token: 0x0600841B RID: 33819 RVA: 0x001DEF88 File Offset: 0x001DD188
			public override BoundStatement BindStatement(StatementSyntax node, BindingDiagnosticBag diagnostics)
			{
				if (node.SyntaxTree == this._semanticModel.SyntaxTree)
				{
					BoundStatement boundStatement = this._semanticModel.GuardedGetSynthesizedStatementFromMap(node);
					if (boundStatement != null)
					{
						return boundStatement;
					}
					BoundNode boundNode = this.TryGetBoundNodeFromMap(node);
					if (boundNode != null)
					{
						return (BoundStatement)boundNode;
					}
				}
				BoundStatement boundStatement2 = base.BindStatement(node, diagnostics);
				if (boundStatement2.WasCompilerGenerated && node.SyntaxTree == this._semanticModel.SyntaxTree)
				{
					this._semanticModel.GuardedAddSynthesizedStatementToMap(node, boundStatement2);
				}
				return boundStatement2;
			}

			// Token: 0x0600841C RID: 33820 RVA: 0x001DF000 File Offset: 0x001DD200
			internal override BoundBlock BindEmbeddedBlock(BlockSyntax node, BindingDiagnosticBag diagnostics)
			{
				BoundBlock boundBlock = (BoundBlock)this.TryGetBoundNodeFromMap(node);
				if (boundBlock != null)
				{
					return boundBlock;
				}
				return base.BindEmbeddedBlock(node, diagnostics);
			}

			// Token: 0x0600841D RID: 33821 RVA: 0x001DF02C File Offset: 0x001DD22C
			private BoundNode TryGetBoundNodeFromMap(CSharpSyntaxNode node)
			{
				if (node.SyntaxTree == this._semanticModel.SyntaxTree)
				{
					ImmutableArray<BoundNode> immutableArray = this._semanticModel.GuardedGetBoundNodesFromMap(node);
					if (!immutableArray.IsDefaultOrEmpty)
					{
						return immutableArray[0];
					}
				}
				return null;
			}

			// Token: 0x0600841E RID: 33822 RVA: 0x001DF06C File Offset: 0x001DD26C
			public override BoundNode BindMethodBody(CSharpSyntaxNode node, BindingDiagnosticBag diagnostics)
			{
				BoundNode boundNode = this.TryGetBoundNodeFromMap(node);
				if (boundNode != null)
				{
					return boundNode;
				}
				return base.BindMethodBody(node, diagnostics);
			}

			// Token: 0x0600841F RID: 33823 RVA: 0x001DF090 File Offset: 0x001DD290
			internal override BoundExpressionStatement BindConstructorInitializer(ConstructorInitializerSyntax node, BindingDiagnosticBag diagnostics)
			{
				return ((BoundExpressionStatement)this.TryGetBoundNodeFromMap(node)) ?? base.BindConstructorInitializer(node, diagnostics);
			}

			// Token: 0x06008420 RID: 33824 RVA: 0x001DF0AA File Offset: 0x001DD2AA
			internal override BoundExpressionStatement BindConstructorInitializer(PrimaryConstructorBaseTypeSyntax node, BindingDiagnosticBag diagnostics)
			{
				return ((BoundExpressionStatement)this.TryGetBoundNodeFromMap(node)) ?? base.BindConstructorInitializer(node, diagnostics);
			}

			// Token: 0x06008421 RID: 33825 RVA: 0x001DF0C4 File Offset: 0x001DD2C4
			internal override BoundBlock BindExpressionBodyAsBlock(ArrowExpressionClauseSyntax node, BindingDiagnosticBag diagnostics)
			{
				BoundBlock boundBlock = (BoundBlock)this.TryGetBoundNodeFromMap(node);
				if (boundBlock != null)
				{
					return boundBlock;
				}
				return base.BindExpressionBodyAsBlock(node, diagnostics);
			}

			// Token: 0x04002155 RID: 8533
			private readonly MemberSemanticModel _semanticModel;
		}

		// Token: 0x020006B6 RID: 1718
		protected sealed class NodeMapBuilder : BoundTreeWalkerWithStackGuard
		{
			// Token: 0x06008422 RID: 33826 RVA: 0x001DF0ED File Offset: 0x001DD2ED
			private NodeMapBuilder(OrderPreservingMultiDictionary<SyntaxNode, BoundNode> map, SyntaxTree tree, SyntaxNode thisSyntaxNodeOnly)
			{
				this._map = map;
				this._tree = tree;
				this._thisSyntaxNodeOnly = thisSyntaxNodeOnly;
			}

			// Token: 0x06008423 RID: 33827 RVA: 0x001DF10C File Offset: 0x001DD30C
			public static void AddToMap(BoundNode root, Dictionary<SyntaxNode, ImmutableArray<BoundNode>> map, SyntaxTree tree, SyntaxNode node = null)
			{
				if (root == null || map.ContainsKey(root.Syntax))
				{
					return;
				}
				OrderPreservingMultiDictionary<SyntaxNode, BoundNode> instance = OrderPreservingMultiDictionary<SyntaxNode, BoundNode>.GetInstance();
				new MemberSemanticModel.NodeMapBuilder(instance, tree, node).Visit(root);
				foreach (SyntaxNode syntaxNode in instance.Keys)
				{
					CSharpSyntaxNode csharpSyntaxNode = (CSharpSyntaxNode)syntaxNode;
					if (!map.ContainsKey(csharpSyntaxNode))
					{
						map[csharpSyntaxNode] = instance[csharpSyntaxNode];
					}
				}
				instance.Free();
			}

			// Token: 0x06008424 RID: 33828 RVA: 0x001DF1A4 File Offset: 0x001DD3A4
			public override BoundNode Visit(BoundNode node)
			{
				if (node == null || node.SyntaxTree != this._tree)
				{
					return null;
				}
				BoundNode boundNode = node;
				if (node.Kind == BoundKind.UnboundLambda)
				{
					boundNode = ((UnboundLambda)node).BindForErrorRecovery();
				}
				if (this.ShouldAddNode(boundNode))
				{
					this._map.Add(boundNode.Syntax, boundNode);
				}
				BoundBinaryOperator boundBinaryOperator = boundNode as BoundBinaryOperator;
				if (boundBinaryOperator != null)
				{
					ArrayBuilder<BoundExpression> instance = ArrayBuilder<BoundExpression>.GetInstance();
					instance.Push(boundBinaryOperator.Right);
					boundNode = boundBinaryOperator.Left;
					for (boundBinaryOperator = boundNode as BoundBinaryOperator; boundBinaryOperator != null; boundBinaryOperator = boundNode as BoundBinaryOperator)
					{
						if (this.ShouldAddNode(boundBinaryOperator))
						{
							this._map.Add(boundBinaryOperator.Syntax, boundBinaryOperator);
						}
						instance.Push(boundBinaryOperator.Right);
						boundNode = boundBinaryOperator.Left;
					}
					this.Visit(boundNode);
					while (instance.Count > 0)
					{
						this.Visit(instance.Pop<BoundExpression>());
					}
					instance.Free();
				}
				else
				{
					base.Visit(boundNode);
				}
				return null;
			}

			// Token: 0x06008425 RID: 33829 RVA: 0x001DF292 File Offset: 0x001DD492
			private bool ShouldAddNode(BoundNode currentBoundNode)
			{
				return !currentBoundNode.WasCompilerGenerated && (this._thisSyntaxNodeOnly == null || currentBoundNode.Syntax == this._thisSyntaxNodeOnly);
			}

			// Token: 0x06008426 RID: 33830 RVA: 0x001DF2B7 File Offset: 0x001DD4B7
			public override BoundNode VisitQueryClause(BoundQueryClause node)
			{
				this.Visit(node.Value);
				base.VisitUnoptimizedForm(node);
				return null;
			}

			// Token: 0x06008427 RID: 33831 RVA: 0x001DF2CE File Offset: 0x001DD4CE
			public override BoundNode VisitRangeVariable(BoundRangeVariable node)
			{
				return null;
			}

			// Token: 0x06008428 RID: 33832 RVA: 0x001DF2D1 File Offset: 0x001DD4D1
			public override BoundNode VisitAwaitableInfo(BoundAwaitableInfo node)
			{
				return null;
			}

			// Token: 0x06008429 RID: 33833 RVA: 0x001DF2D4 File Offset: 0x001DD4D4
			public override BoundNode VisitBinaryOperator(BoundBinaryOperator node)
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x0600842A RID: 33834 RVA: 0x001DF2DB File Offset: 0x001DD4DB
			protected override bool ConvertInsufficientExecutionStackExceptionToCancelledByStackGuardException()
			{
				return false;
			}

			// Token: 0x04002156 RID: 8534
			private readonly OrderPreservingMultiDictionary<SyntaxNode, BoundNode> _map;

			// Token: 0x04002157 RID: 8535
			private readonly SyntaxTree _tree;

			// Token: 0x04002158 RID: 8536
			private readonly SyntaxNode _thisSyntaxNodeOnly;
		}

		// Token: 0x020006B7 RID: 1719
		private sealed class SpeculativeMemberSemanticModel : MemberSemanticModel
		{
			// Token: 0x0600842B RID: 33835 RVA: 0x001DF2E0 File Offset: 0x001DD4E0
			public SpeculativeMemberSemanticModel(SyntaxTreeSemanticModel parentSemanticModel, Symbol owner, TypeSyntax root, Binder rootBinder, NullableWalker.SnapshotManager snapshotManagerOpt, ImmutableDictionary<Symbol, Symbol> parentRemappedSymbolsOpt, int position)
				: base(root, owner, rootBinder, null, parentSemanticModel, snapshotManagerOpt, parentRemappedSymbolsOpt, position)
			{
			}

			// Token: 0x0600842C RID: 33836 RVA: 0x001DF2FF File Offset: 0x001DD4FF
			protected override NullableWalker.SnapshotManager GetSnapshotManager()
			{
				return this._parentSnapshotManagerOpt;
			}

			// Token: 0x0600842D RID: 33837 RVA: 0x001DF308 File Offset: 0x001DD508
			protected override BoundNode RewriteNullableBoundNodesWithSnapshots(BoundNode boundRoot, Binder binder, DiagnosticBag diagnostics, bool createSnapshots, out NullableWalker.SnapshotManager snapshotManager, ref ImmutableDictionary<Symbol, Symbol> remappedSymbols)
			{
				return NullableWalker.AnalyzeAndRewrite(this.Compilation, base.MemberSymbol as MethodSymbol, boundRoot, binder, null, diagnostics, false, out snapshotManager, ref remappedSymbols);
			}

			// Token: 0x0600842E RID: 33838 RVA: 0x001DF334 File Offset: 0x001DD534
			protected override void AnalyzeBoundNodeNullability(BoundNode boundRoot, Binder binder, DiagnosticBag diagnostics, bool createSnapshots)
			{
				NullableWalker.AnalyzeWithoutRewrite(this.Compilation, base.MemberSymbol as MethodSymbol, boundRoot, binder, diagnostics, createSnapshots);
			}

			// Token: 0x0600842F RID: 33839 RVA: 0x001DF351 File Offset: 0x001DD551
			protected override bool IsNullableAnalysisEnabled()
			{
				return this._parentSemanticModelOpt.IsNullableAnalysisEnabledAtSpeculativePosition(this.OriginalPositionForSpeculation, this.Root);
			}

			// Token: 0x06008430 RID: 33840 RVA: 0x001DF36A File Offset: 0x001DD56A
			internal override bool TryGetSpeculativeSemanticModelCore(SyntaxTreeSemanticModel parentModel, int position, ConstructorInitializerSyntax constructorInitializer, out SemanticModel speculativeModel)
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x06008431 RID: 33841 RVA: 0x001DF371 File Offset: 0x001DD571
			internal override bool TryGetSpeculativeSemanticModelCore(SyntaxTreeSemanticModel parentModel, int position, PrimaryConstructorBaseTypeSyntax constructorInitializer, out SemanticModel speculativeModel)
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x06008432 RID: 33842 RVA: 0x001DF378 File Offset: 0x001DD578
			internal override bool TryGetSpeculativeSemanticModelCore(SyntaxTreeSemanticModel parentModel, int position, EqualsValueClauseSyntax initializer, out SemanticModel speculativeModel)
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x06008433 RID: 33843 RVA: 0x001DF37F File Offset: 0x001DD57F
			internal override bool TryGetSpeculativeSemanticModelCore(SyntaxTreeSemanticModel parentModel, int position, ArrowExpressionClauseSyntax expressionBody, out SemanticModel speculativeModel)
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x06008434 RID: 33844 RVA: 0x001DF386 File Offset: 0x001DD586
			internal override bool TryGetSpeculativeSemanticModelCore(SyntaxTreeSemanticModel parentModel, int position, StatementSyntax statement, out SemanticModel speculativeModel)
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x06008435 RID: 33845 RVA: 0x001DF38D File Offset: 0x001DD58D
			internal override bool TryGetSpeculativeSemanticModelForMethodBodyCore(SyntaxTreeSemanticModel parentModel, int position, BaseMethodDeclarationSyntax method, out SemanticModel speculativeModel)
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x06008436 RID: 33846 RVA: 0x001DF394 File Offset: 0x001DD594
			internal override bool TryGetSpeculativeSemanticModelForMethodBodyCore(SyntaxTreeSemanticModel parentModel, int position, AccessorDeclarationSyntax accessor, out SemanticModel speculativeModel)
			{
				throw ExceptionUtilities.Unreachable;
			}
		}
	}
}
