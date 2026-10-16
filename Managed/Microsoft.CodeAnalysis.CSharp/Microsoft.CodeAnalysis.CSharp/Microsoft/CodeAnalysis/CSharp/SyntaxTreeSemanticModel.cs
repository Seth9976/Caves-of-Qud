using System;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000113 RID: 275
	internal class SyntaxTreeSemanticModel : CSharpSemanticModel
	{
		// Token: 0x06001656 RID: 5718 RVA: 0x00078250 File Offset: 0x00076450
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal SyntaxTreeSemanticModel(CSharpCompilation compilation, SyntaxTree syntaxTree, bool ignoreAccessibility = false)
		{
			this._compilation = compilation;
			this._syntaxTree = syntaxTree;
			this._ignoresAccessibility = ignoreAccessibility;
			if (!this.Compilation.SyntaxTrees.Contains(syntaxTree))
			{
				throw new ArgumentOutOfRangeException("syntaxTree", CSharpResources.TreeNotPartOfCompilation);
			}
			this._binderFactory = compilation.GetBinderFactory(this.SyntaxTree, ignoreAccessibility);
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x000782BC File Offset: 0x000764BC
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal SyntaxTreeSemanticModel(CSharpCompilation parentCompilation, SyntaxTree parentSyntaxTree, SyntaxTree speculatedSyntaxTree)
		{
			this._compilation = parentCompilation;
			this._syntaxTree = speculatedSyntaxTree;
			this._binderFactory = this._compilation.GetBinderFactory(parentSyntaxTree, false);
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06001658 RID: 5720 RVA: 0x000782F0 File Offset: 0x000764F0
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public override CSharpCompilation Compilation
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get
			{
				return this._compilation;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06001659 RID: 5721 RVA: 0x000782F8 File Offset: 0x000764F8
		[global::System.Runtime.CompilerServices.Nullable(1)]
		internal override CSharpSyntaxNode Root
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get
			{
				return (CSharpSyntaxNode)this._syntaxTree.GetRoot(default(CancellationToken));
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x0600165A RID: 5722 RVA: 0x0007831E File Offset: 0x0007651E
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public override SyntaxTree SyntaxTree
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get
			{
				return this._syntaxTree;
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x0600165B RID: 5723 RVA: 0x00078326 File Offset: 0x00076526
		public override bool IgnoresAccessibility
		{
			get
			{
				return this._ignoresAccessibility;
			}
		}

		// Token: 0x0600165C RID: 5724 RVA: 0x00078330 File Offset: 0x00076530
		private void VerifySpanForGetDiagnostics(TextSpan? span)
		{
			if (span != null && !this.Root.FullSpan.Contains(span.Value))
			{
				throw new ArgumentException("span");
			}
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x0007836D File Offset: 0x0007656D
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<Diagnostic> GetSyntaxDiagnostics(TextSpan? span = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			this.VerifySpanForGetDiagnostics(span);
			return this.Compilation.GetDiagnosticsForSyntaxTree(CompilationStage.Parse, this.SyntaxTree, span, false, cancellationToken);
		}

		// Token: 0x0600165E RID: 5726 RVA: 0x0007838B File Offset: 0x0007658B
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<Diagnostic> GetDeclarationDiagnostics(TextSpan? span = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			this.VerifySpanForGetDiagnostics(span);
			return this.Compilation.GetDiagnosticsForSyntaxTree(CompilationStage.Declare, this.SyntaxTree, span, false, cancellationToken);
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x000783A9 File Offset: 0x000765A9
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<Diagnostic> GetMethodBodyDiagnostics(TextSpan? span = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			this.VerifySpanForGetDiagnostics(span);
			return this.Compilation.GetDiagnosticsForSyntaxTree(CompilationStage.Compile, this.SyntaxTree, span, false, cancellationToken);
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x000783C7 File Offset: 0x000765C7
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<Diagnostic> GetDiagnostics(TextSpan? span = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			this.VerifySpanForGetDiagnostics(span);
			return this.Compilation.GetDiagnosticsForSyntaxTree(CompilationStage.Compile, this.SyntaxTree, span, true, cancellationToken);
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x000783E8 File Offset: 0x000765E8
		internal override Binder GetEnclosingBinderInternal(int position)
		{
			SyntaxToken syntaxToken = this.Root.FindTokenIncludingCrefAndNameAttributes(position);
			if (position == 0 && position != syntaxToken.SpanStart)
			{
				return this._binderFactory.GetBinder(this.Root, position, null, null).WithAdditionalFlags(base.GetSemanticModelBinderFlags());
			}
			MemberSemanticModel memberModel = this.GetMemberModel(position);
			if (memberModel != null)
			{
				return memberModel.GetEnclosingBinder(position);
			}
			return this._binderFactory.GetBinder((CSharpSyntaxNode)syntaxToken.Parent, position, null, null).WithAdditionalFlags(base.GetSemanticModelBinderFlags());
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x00078468 File Offset: 0x00076668
		internal override IOperation GetOperationWorker(CSharpSyntaxNode node, CancellationToken cancellationToken)
		{
			ConstructorDeclarationSyntax constructorDeclarationSyntax = node as ConstructorDeclarationSyntax;
			MemberSemanticModel memberSemanticModel;
			if (constructorDeclarationSyntax == null)
			{
				BaseMethodDeclarationSyntax baseMethodDeclarationSyntax = node as BaseMethodDeclarationSyntax;
				if (baseMethodDeclarationSyntax == null)
				{
					AccessorDeclarationSyntax accessorDeclarationSyntax = node as AccessorDeclarationSyntax;
					if (accessorDeclarationSyntax == null)
					{
						RecordDeclarationSyntax recordDeclarationSyntax = node as RecordDeclarationSyntax;
						if (recordDeclarationSyntax != null && recordDeclarationSyntax.ParameterList != null)
						{
							if (recordDeclarationSyntax.PrimaryConstructorBaseTypeIfClass != null)
							{
								if (this.TryGetSynthesizedRecordConstructor(recordDeclarationSyntax) != null)
								{
									memberSemanticModel = this.GetOrAddModel(recordDeclarationSyntax);
									goto IL_00B1;
								}
							}
						}
						memberSemanticModel = this.GetMemberModel(node);
					}
					else
					{
						memberSemanticModel = ((accessorDeclarationSyntax.Body != null || accessorDeclarationSyntax.ExpressionBody != null) ? this.GetOrAddModel(node) : null);
					}
				}
				else
				{
					memberSemanticModel = (baseMethodDeclarationSyntax.HasAnyBody() ? this.GetOrAddModel(node) : null);
				}
			}
			else
			{
				memberSemanticModel = ((constructorDeclarationSyntax.HasAnyBody() || constructorDeclarationSyntax.Initializer != null) ? this.GetOrAddModel(node) : null);
			}
			IL_00B1:
			if (memberSemanticModel != null)
			{
				return memberSemanticModel.GetOperationWorker(node, cancellationToken);
			}
			return null;
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x00078534 File Offset: 0x00076734
		internal override SymbolInfo GetSymbolInfoWorker(CSharpSyntaxNode node, CSharpSemanticModel.SymbolInfoOptions options, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSemanticModel.ValidateSymbolInfoOptions(options);
			node = SyntaxFactory.GetStandaloneNode(node);
			MemberSemanticModel memberModel = this.GetMemberModel(node);
			SymbolInfo symbolInfo;
			XmlNameAttributeSyntax xmlNameAttributeSyntax;
			CrefSyntax crefSyntax;
			if (memberModel != null)
			{
				symbolInfo = memberModel.GetSymbolInfoWorker(node, options, cancellationToken);
				if (symbolInfo.Symbol == null && symbolInfo.CandidateReason == CandidateReason.None && node is ExpressionSyntax && SyntaxFacts.IsInNamespaceOrTypeContext((ExpressionSyntax)node))
				{
					Binder binder = base.GetEnclosingBinder(base.GetAdjustedNodePosition(node));
					if (binder != null)
					{
						binder = new LocalScopeBinder(binder);
						BoundExpression boundExpression = binder.BindExpression((ExpressionSyntax)node, BindingDiagnosticBag.Discarded);
						SymbolInfo symbolInfoForNode = base.GetSymbolInfoForNode(options, boundExpression, boundExpression, null, null);
						if (symbolInfoForNode.Symbol != null)
						{
							symbolInfo = new SymbolInfo(null, ImmutableArray.Create<ISymbol>(symbolInfoForNode.Symbol), CandidateReason.NotATypeOrNamespace);
						}
						else if (!symbolInfoForNode.CandidateSymbols.IsEmpty)
						{
							symbolInfo = new SymbolInfo(null, symbolInfoForNode.CandidateSymbols, CandidateReason.NotATypeOrNamespace);
						}
					}
				}
			}
			else if (node.Parent.Kind() == SyntaxKind.XmlNameAttribute && (xmlNameAttributeSyntax = (XmlNameAttributeSyntax)node.Parent).Identifier == node)
			{
				symbolInfo = SymbolInfo.None;
				Binder enclosingBinder = base.GetEnclosingBinder(base.GetAdjustedNodePosition(node));
				if (enclosingBinder != null)
				{
					CompoundUseSiteInfo<AssemblySymbol> discarded = CompoundUseSiteInfo<AssemblySymbol>.Discarded;
					ImmutableArray<Symbol> immutableArray = enclosingBinder.BindXmlNameAttribute(xmlNameAttributeSyntax, ref discarded);
					int length = immutableArray.Length;
					if (length != 0)
					{
						if (length != 1)
						{
							symbolInfo = SymbolInfoFactory.Create(immutableArray, LookupResultKind.Ambiguous, false);
						}
						else
						{
							symbolInfo = SymbolInfoFactory.Create(immutableArray, LookupResultKind.Viable, false);
						}
					}
					else
					{
						symbolInfo = SymbolInfo.None;
					}
				}
			}
			else if ((crefSyntax = node as CrefSyntax) != null)
			{
				int adjustedNodePosition = base.GetAdjustedNodePosition(crefSyntax);
				symbolInfo = base.GetCrefSymbolInfo(adjustedNodePosition, crefSyntax, options, CSharpSemanticModel.HasParameterList(crefSyntax));
			}
			else
			{
				Symbol semanticInfoSymbolInNonMemberContext = this.GetSemanticInfoSymbolInNonMemberContext(node, (options & CSharpSemanticModel.SymbolInfoOptions.PreserveAliases) > (CSharpSemanticModel.SymbolInfoOptions)0);
				symbolInfo = ((semanticInfoSymbolInNonMemberContext != null) ? CSharpSemanticModel.GetSymbolInfoForSymbol(semanticInfoSymbolInNonMemberContext, options) : SymbolInfo.None);
			}
			return symbolInfo;
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x00078700 File Offset: 0x00076900
		internal override SymbolInfo GetCollectionInitializerSymbolInfoWorker(InitializerExpressionSyntax collectionInitializer, ExpressionSyntax node, CancellationToken cancellationToken = default(CancellationToken))
		{
			MemberSemanticModel memberModel = this.GetMemberModel(collectionInitializer);
			if (memberModel != null)
			{
				return memberModel.GetCollectionInitializerSymbolInfoWorker(collectionInitializer, node, cancellationToken);
			}
			return SymbolInfo.None;
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x00078728 File Offset: 0x00076928
		internal override CSharpTypeInfo GetTypeInfoWorker(CSharpSyntaxNode node, CancellationToken cancellationToken = default(CancellationToken))
		{
			node = SyntaxFactory.GetStandaloneNode(node);
			MemberSemanticModel memberModel = this.GetMemberModel(node);
			if (memberModel != null)
			{
				return memberModel.GetTypeInfoWorker(node, cancellationToken);
			}
			Symbol semanticInfoSymbolInNonMemberContext = this.GetSemanticInfoSymbolInNonMemberContext(node, false);
			if (semanticInfoSymbolInNonMemberContext == null)
			{
				return CSharpTypeInfo.None;
			}
			return CSharpSemanticModel.GetTypeInfoForSymbol(semanticInfoSymbolInNonMemberContext);
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x0007876C File Offset: 0x0007696C
		private Symbol GetSemanticInfoSymbolInNonMemberContext(CSharpSyntaxNode node, bool bindVarAsAliasFirst)
		{
			Binder enclosingBinder = base.GetEnclosingBinder(base.GetAdjustedNodePosition(node));
			if (enclosingBinder != null)
			{
				TypeSyntax typeSyntax = node as TypeSyntax;
				if (typeSyntax != null)
				{
					ConsList<TypeSymbol> basesBeingResolved = this.GetBasesBeingResolved(typeSyntax);
					if (SyntaxFacts.IsNamespaceAliasQualifier(typeSyntax))
					{
						return enclosingBinder.BindNamespaceAliasSymbol(node as IdentifierNameSyntax, BindingDiagnosticBag.Discarded);
					}
					if (!SyntaxFacts.IsInTypeOnlyContext(typeSyntax))
					{
						return enclosingBinder.BindNamespaceOrTypeOrAliasSymbol(typeSyntax, BindingDiagnosticBag.Discarded, basesBeingResolved, basesBeingResolved != null).Symbol;
					}
					if (!typeSyntax.IsVar)
					{
						return enclosingBinder.BindTypeOrAlias(typeSyntax, BindingDiagnosticBag.Discarded, basesBeingResolved, false).Symbol;
					}
					Symbol symbol = (bindVarAsAliasFirst ? enclosingBinder.BindTypeOrAlias(typeSyntax, BindingDiagnosticBag.Discarded, basesBeingResolved, false).Symbol : null);
					if (symbol == null || symbol.Kind == SymbolKind.ErrorType)
					{
						VariableDeclarationSyntax variableDeclarationSyntax = typeSyntax.Parent as VariableDeclarationSyntax;
						if (variableDeclarationSyntax != null && variableDeclarationSyntax.Variables.Any())
						{
							FieldSymbol declaredFieldSymbol = this.GetDeclaredFieldSymbol(variableDeclarationSyntax.Variables.First());
							if (declaredFieldSymbol != null)
							{
								symbol = declaredFieldSymbol.Type;
							}
						}
					}
					return symbol ?? enclosingBinder.BindTypeOrAlias(typeSyntax, BindingDiagnosticBag.Discarded, basesBeingResolved, false).Symbol;
				}
			}
			return null;
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x00078894 File Offset: 0x00076A94
		internal override ImmutableArray<Symbol> GetMemberGroupWorker(CSharpSyntaxNode node, CSharpSemanticModel.SymbolInfoOptions options, CancellationToken cancellationToken = default(CancellationToken))
		{
			node = SyntaxFactory.GetStandaloneNode(node);
			MemberSemanticModel memberModel = this.GetMemberModel(node);
			if (memberModel != null)
			{
				return memberModel.GetMemberGroupWorker(node, options, cancellationToken);
			}
			return ImmutableArray<Symbol>.Empty;
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x000788C4 File Offset: 0x00076AC4
		internal override ImmutableArray<IPropertySymbol> GetIndexerGroupWorker(CSharpSyntaxNode node, CSharpSemanticModel.SymbolInfoOptions options, CancellationToken cancellationToken = default(CancellationToken))
		{
			node = SyntaxFactory.GetStandaloneNode(node);
			MemberSemanticModel memberModel = this.GetMemberModel(node);
			if (memberModel != null)
			{
				return memberModel.GetIndexerGroupWorker(node, options, cancellationToken);
			}
			return ImmutableArray<IPropertySymbol>.Empty;
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x000788F4 File Offset: 0x00076AF4
		internal override Optional<object> GetConstantValueWorker(CSharpSyntaxNode node, CancellationToken cancellationToken)
		{
			node = SyntaxFactory.GetStandaloneNode(node);
			MemberSemanticModel memberModel = this.GetMemberModel(node);
			if (memberModel != null)
			{
				return memberModel.GetConstantValueWorker(node, cancellationToken);
			}
			return default(Optional<object>);
		}

		// Token: 0x0600166A RID: 5738 RVA: 0x00078928 File Offset: 0x00076B28
		public override QueryClauseInfo GetQueryClauseInfo(QueryClauseSyntax node, CancellationToken cancellationToken = default(CancellationToken))
		{
			base.CheckSyntaxNode(node);
			MemberSemanticModel memberModel = this.GetMemberModel(node);
			if (memberModel != null)
			{
				return memberModel.GetQueryClauseInfo(node, cancellationToken);
			}
			return default(QueryClauseInfo);
		}

		// Token: 0x0600166B RID: 5739 RVA: 0x0007895C File Offset: 0x00076B5C
		public override SymbolInfo GetSymbolInfo(SelectOrGroupClauseSyntax node, CancellationToken cancellationToken = default(CancellationToken))
		{
			base.CheckSyntaxNode(node);
			MemberSemanticModel memberModel = this.GetMemberModel(node);
			if (memberModel != null)
			{
				return memberModel.GetSymbolInfo(node, cancellationToken);
			}
			return SymbolInfo.None;
		}

		// Token: 0x0600166C RID: 5740 RVA: 0x0007898C File Offset: 0x00076B8C
		public override TypeInfo GetTypeInfo(SelectOrGroupClauseSyntax node, CancellationToken cancellationToken = default(CancellationToken))
		{
			base.CheckSyntaxNode(node);
			MemberSemanticModel memberModel = this.GetMemberModel(node);
			if (memberModel != null)
			{
				return memberModel.GetTypeInfo(node, cancellationToken);
			}
			return CSharpTypeInfo.None;
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x000789C0 File Offset: 0x00076BC0
		public override IPropertySymbol GetDeclaredSymbol(AnonymousObjectMemberDeclaratorSyntax declaratorSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			base.CheckSyntaxNode(declaratorSyntax);
			MemberSemanticModel memberModel = this.GetMemberModel(declaratorSyntax);
			if (memberModel != null)
			{
				return memberModel.GetDeclaredSymbol(declaratorSyntax, cancellationToken);
			}
			return null;
		}

		// Token: 0x0600166E RID: 5742 RVA: 0x000789EC File Offset: 0x00076BEC
		public override INamedTypeSymbol GetDeclaredSymbol(AnonymousObjectCreationExpressionSyntax declaratorSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			base.CheckSyntaxNode(declaratorSyntax);
			MemberSemanticModel memberModel = this.GetMemberModel(declaratorSyntax);
			if (memberModel != null)
			{
				return memberModel.GetDeclaredSymbol(declaratorSyntax, cancellationToken);
			}
			return null;
		}

		// Token: 0x0600166F RID: 5743 RVA: 0x00078A18 File Offset: 0x00076C18
		public override INamedTypeSymbol GetDeclaredSymbol(TupleExpressionSyntax declaratorSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			base.CheckSyntaxNode(declaratorSyntax);
			MemberSemanticModel memberModel = this.GetMemberModel(declaratorSyntax);
			if (memberModel != null)
			{
				return memberModel.GetDeclaredSymbol(declaratorSyntax, cancellationToken);
			}
			return null;
		}

		// Token: 0x06001670 RID: 5744 RVA: 0x00078A44 File Offset: 0x00076C44
		public override ISymbol GetDeclaredSymbol(ArgumentSyntax declaratorSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			base.CheckSyntaxNode(declaratorSyntax);
			MemberSemanticModel memberModel = this.GetMemberModel(declaratorSyntax);
			if (memberModel != null)
			{
				return memberModel.GetDeclaredSymbol(declaratorSyntax, cancellationToken);
			}
			return null;
		}

		// Token: 0x06001671 RID: 5745 RVA: 0x00078A70 File Offset: 0x00076C70
		public override IRangeVariableSymbol GetDeclaredSymbol(QueryClauseSyntax node, CancellationToken cancellationToken = default(CancellationToken))
		{
			base.CheckSyntaxNode(node);
			MemberSemanticModel memberModel = this.GetMemberModel(node);
			if (memberModel != null)
			{
				return memberModel.GetDeclaredSymbol(node, cancellationToken);
			}
			return null;
		}

		// Token: 0x06001672 RID: 5746 RVA: 0x00078A9C File Offset: 0x00076C9C
		public override IRangeVariableSymbol GetDeclaredSymbol(JoinIntoClauseSyntax node, CancellationToken cancellationToken = default(CancellationToken))
		{
			base.CheckSyntaxNode(node);
			MemberSemanticModel memberModel = this.GetMemberModel(node);
			if (memberModel != null)
			{
				return memberModel.GetDeclaredSymbol(node, cancellationToken);
			}
			return null;
		}

		// Token: 0x06001673 RID: 5747 RVA: 0x00078AC8 File Offset: 0x00076CC8
		public override IRangeVariableSymbol GetDeclaredSymbol(QueryContinuationSyntax node, CancellationToken cancellationToken = default(CancellationToken))
		{
			base.CheckSyntaxNode(node);
			MemberSemanticModel memberModel = this.GetMemberModel(node);
			if (memberModel != null)
			{
				return memberModel.GetDeclaredSymbol(node, cancellationToken);
			}
			return null;
		}

		// Token: 0x06001674 RID: 5748 RVA: 0x00078AF4 File Offset: 0x00076CF4
		public override SymbolInfo GetSymbolInfo(OrderingSyntax node, CancellationToken cancellationToken = default(CancellationToken))
		{
			base.CheckSyntaxNode(node);
			MemberSemanticModel memberModel = this.GetMemberModel(node);
			if (memberModel != null)
			{
				return memberModel.GetSymbolInfo(node, cancellationToken);
			}
			return SymbolInfo.None;
		}

		// Token: 0x06001675 RID: 5749 RVA: 0x00078B24 File Offset: 0x00076D24
		private ConsList<TypeSymbol> GetBasesBeingResolved(TypeSyntax expression)
		{
			while (expression != null && expression.Parent != null)
			{
				CSharpSyntaxNode parent = expression.Parent;
				BaseTypeSyntax baseTypeSyntax = parent as BaseTypeSyntax;
				if (baseTypeSyntax != null && parent.Parent != null && parent.Parent.Kind() == SyntaxKind.BaseList && baseTypeSyntax.Type == expression)
				{
					BaseTypeDeclarationSyntax baseTypeDeclarationSyntax = (BaseTypeDeclarationSyntax)parent.Parent.Parent;
					INamedTypeSymbol declaredSymbol = this.GetDeclaredSymbol(baseTypeDeclarationSyntax, default(CancellationToken));
					return ConsList<TypeSymbol>.Empty.Prepend(declaredSymbol.GetSymbol().OriginalDefinition);
				}
				expression = expression.Parent as TypeSyntax;
			}
			return null;
		}

		// Token: 0x06001676 RID: 5750 RVA: 0x00078BBC File Offset: 0x00076DBC
		public override Conversion ClassifyConversion(ExpressionSyntax expression, ITypeSymbol destination, bool isExplicitInSource = false)
		{
			TypeSymbol typeSymbol = destination.EnsureCSharpSymbolOrNull("destination");
			if (expression.Kind() == SyntaxKind.DeclarationExpression)
			{
				return Conversion.NoConversion;
			}
			if (isExplicitInSource)
			{
				return this.ClassifyConversionForCast(expression, typeSymbol);
			}
			base.CheckSyntaxNode(expression);
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			MemberSemanticModel memberModel = this.GetMemberModel(expression);
			if (memberModel == null)
			{
				return Conversion.NoConversion;
			}
			return memberModel.ClassifyConversion(expression, destination, false);
		}

		// Token: 0x06001677 RID: 5751 RVA: 0x00078C24 File Offset: 0x00076E24
		internal override Conversion ClassifyConversionForCast(ExpressionSyntax expression, TypeSymbol destination)
		{
			base.CheckSyntaxNode(expression);
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			MemberSemanticModel memberModel = this.GetMemberModel(expression);
			if (memberModel == null)
			{
				return Conversion.NoConversion;
			}
			return memberModel.ClassifyConversionForCast(expression, destination);
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06001678 RID: 5752 RVA: 0x00078C5F File Offset: 0x00076E5F
		public override bool IsSpeculativeSemanticModel
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06001679 RID: 5753 RVA: 0x00078C62 File Offset: 0x00076E62
		public override int OriginalPositionForSpeculation
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x0600167A RID: 5754 RVA: 0x00078C65 File Offset: 0x00076E65
		public override CSharpSemanticModel ParentModel
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x0600167B RID: 5755 RVA: 0x00078C68 File Offset: 0x00076E68
		internal override SemanticModel ContainingModelOrSelf
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x00078C6C File Offset: 0x00076E6C
		internal sealed override bool TryGetSpeculativeSemanticModelCore(SyntaxTreeSemanticModel parentModel, int position, TypeSyntax type, SpeculativeBindingOption bindingOption, out SemanticModel speculativeModel)
		{
			position = base.CheckAndAdjustPosition(position);
			MemberSemanticModel memberModel = this.GetMemberModel(position);
			if (memberModel != null)
			{
				return memberModel.TryGetSpeculativeSemanticModelCore(parentModel, position, type, bindingOption, out speculativeModel);
			}
			Binder speculativeBinder = base.GetSpeculativeBinder(position, type, bindingOption);
			if (speculativeBinder != null)
			{
				speculativeModel = SpeculativeSyntaxTreeSemanticModel.Create(parentModel, type, speculativeBinder, position, bindingOption);
				return true;
			}
			speculativeModel = null;
			return false;
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x00078CC0 File Offset: 0x00076EC0
		internal override bool TryGetSpeculativeSemanticModelCore(SyntaxTreeSemanticModel parentModel, int position, CrefSyntax crefSyntax, out SemanticModel speculativeModel)
		{
			position = base.CheckAndAdjustPosition(position);
			Binder enclosingBinder = base.GetEnclosingBinder(position);
			if (enclosingBinder != null && enclosingBinder.InCref)
			{
				speculativeModel = SpeculativeSyntaxTreeSemanticModel.Create(parentModel, crefSyntax, enclosingBinder, position);
				return true;
			}
			speculativeModel = null;
			return false;
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x00078CFC File Offset: 0x00076EFC
		internal sealed override bool TryGetSpeculativeSemanticModelCore(SyntaxTreeSemanticModel parentModel, int position, StatementSyntax statement, out SemanticModel speculativeModel)
		{
			position = base.CheckAndAdjustPosition(position);
			MemberSemanticModel memberModel = this.GetMemberModel(position);
			if (memberModel != null)
			{
				return memberModel.TryGetSpeculativeSemanticModelCore(parentModel, position, statement, out speculativeModel);
			}
			speculativeModel = null;
			return false;
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x00078D30 File Offset: 0x00076F30
		internal sealed override bool TryGetSpeculativeSemanticModelForMethodBodyCore(SyntaxTreeSemanticModel parentModel, int position, BaseMethodDeclarationSyntax method, out SemanticModel speculativeModel)
		{
			position = base.CheckAndAdjustPosition(position);
			MemberSemanticModel memberModel = this.GetMemberModel(position);
			if (memberModel != null)
			{
				return memberModel.TryGetSpeculativeSemanticModelForMethodBodyCore(parentModel, position, method, out speculativeModel);
			}
			speculativeModel = null;
			return false;
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x00078D64 File Offset: 0x00076F64
		internal sealed override bool TryGetSpeculativeSemanticModelForMethodBodyCore(SyntaxTreeSemanticModel parentModel, int position, AccessorDeclarationSyntax accessor, out SemanticModel speculativeModel)
		{
			position = base.CheckAndAdjustPosition(position);
			MemberSemanticModel memberModel = this.GetMemberModel(position);
			if (memberModel != null)
			{
				return memberModel.TryGetSpeculativeSemanticModelForMethodBodyCore(parentModel, position, accessor, out speculativeModel);
			}
			speculativeModel = null;
			return false;
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x00078D98 File Offset: 0x00076F98
		internal sealed override bool TryGetSpeculativeSemanticModelCore(SyntaxTreeSemanticModel parentModel, int position, EqualsValueClauseSyntax initializer, out SemanticModel speculativeModel)
		{
			position = base.CheckAndAdjustPosition(position);
			MemberSemanticModel memberModel = this.GetMemberModel(position);
			if (memberModel != null)
			{
				return memberModel.TryGetSpeculativeSemanticModelCore(parentModel, position, initializer, out speculativeModel);
			}
			speculativeModel = null;
			return false;
		}

		// Token: 0x06001682 RID: 5762 RVA: 0x00078DCC File Offset: 0x00076FCC
		internal override bool TryGetSpeculativeSemanticModelCore(SyntaxTreeSemanticModel parentModel, int position, ArrowExpressionClauseSyntax expressionBody, out SemanticModel speculativeModel)
		{
			position = base.CheckAndAdjustPosition(position);
			MemberSemanticModel memberModel = this.GetMemberModel(position);
			if (memberModel != null)
			{
				return memberModel.TryGetSpeculativeSemanticModelCore(parentModel, position, expressionBody, out speculativeModel);
			}
			speculativeModel = null;
			return false;
		}

		// Token: 0x06001683 RID: 5763 RVA: 0x00078E00 File Offset: 0x00077000
		internal sealed override bool TryGetSpeculativeSemanticModelCore(SyntaxTreeSemanticModel parentModel, int position, ConstructorInitializerSyntax constructorInitializer, out SemanticModel speculativeModel)
		{
			position = base.CheckAndAdjustPosition(position);
			if (Enumerable.FirstOrDefault<ConstructorInitializerSyntax>(Enumerable.OfType<ConstructorInitializerSyntax>(this.Root.FindToken(position, false).Parent.AncestorsAndSelf(true))) != null)
			{
				MemberSemanticModel memberModel = this.GetMemberModel(position);
				if (memberModel != null)
				{
					return memberModel.TryGetSpeculativeSemanticModelCore(parentModel, position, constructorInitializer, out speculativeModel);
				}
			}
			speculativeModel = null;
			return false;
		}

		// Token: 0x06001684 RID: 5764 RVA: 0x00078E5C File Offset: 0x0007705C
		internal sealed override bool TryGetSpeculativeSemanticModelCore(SyntaxTreeSemanticModel parentModel, int position, PrimaryConstructorBaseTypeSyntax constructorInitializer, out SemanticModel speculativeModel)
		{
			position = base.CheckAndAdjustPosition(position);
			PrimaryConstructorBaseTypeSyntax primaryConstructorBaseTypeSyntax = Enumerable.FirstOrDefault<PrimaryConstructorBaseTypeSyntax>(Enumerable.OfType<PrimaryConstructorBaseTypeSyntax>(this.Root.FindToken(position, false).Parent.AncestorsAndSelf(true)));
			if (primaryConstructorBaseTypeSyntax != null)
			{
				MemberSemanticModel memberModel = this.GetMemberModel(primaryConstructorBaseTypeSyntax);
				if (memberModel != null)
				{
					return memberModel.TryGetSpeculativeSemanticModelCore(parentModel, position, constructorInitializer, out speculativeModel);
				}
			}
			speculativeModel = null;
			return false;
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x00078EB8 File Offset: 0x000770B8
		internal override BoundExpression GetSpeculativelyBoundExpression(int position, ExpressionSyntax expression, SpeculativeBindingOption bindingOption, out Binder binder, out ImmutableArray<Symbol> crefSymbols)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			if (bindingOption == SpeculativeBindingOption.BindAsExpression)
			{
				position = base.CheckAndAdjustPosition(position);
				MemberSemanticModel memberModel = this.GetMemberModel(position);
				if (memberModel != null)
				{
					return memberModel.GetSpeculativelyBoundExpression(position, expression, bindingOption, out binder, out crefSymbols);
				}
			}
			return base.GetSpeculativelyBoundExpressionWithoutNullability(position, expression, bindingOption, out binder, out crefSymbols);
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x00078F08 File Offset: 0x00077108
		internal AttributeSemanticModel CreateSpeculativeAttributeSemanticModel(int position, AttributeSyntax attribute, Binder binder, AliasSymbol aliasOpt, NamedTypeSymbol attributeType)
		{
			MemberSemanticModel memberSemanticModel = (this.IsNullableAnalysisEnabledAtSpeculativePosition(position, attribute) ? this.GetMemberModel(position) : null);
			return AttributeSemanticModel.CreateSpeculative(this, attribute, attributeType, aliasOpt, binder, (memberSemanticModel != null) ? memberSemanticModel.GetRemappedSymbols() : null, position);
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x00078F44 File Offset: 0x00077144
		internal bool IsNullableAnalysisEnabledAtSpeculativePosition(int position, SyntaxNode speculativeSyntax)
		{
			bool? flag = ((CSharpSyntaxTree)speculativeSyntax.SyntaxTree).IsNullableAnalysisEnabled(speculativeSyntax.Span);
			if (flag == null)
			{
				return this.Compilation.IsNullableAnalysisEnabledIn((CSharpSyntaxTree)this.SyntaxTree, new TextSpan(position, 0));
			}
			return flag.GetValueOrDefault();
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x00078F98 File Offset: 0x00077198
		private MemberSemanticModel GetMemberModel(int position)
		{
			CSharpSyntaxNode csharpSyntaxNode = (CSharpSyntaxNode)this.Root.FindTokenIncludingCrefAndNameAttributes(position).Parent;
			CSharpSyntaxNode memberDeclaration = SyntaxTreeSemanticModel.GetMemberDeclaration(csharpSyntaxNode);
			bool flag = false;
			if (memberDeclaration != null)
			{
				SyntaxKind syntaxKind = memberDeclaration.Kind();
				if (syntaxKind <= SyntaxKind.DestructorDeclaration)
				{
					if (syntaxKind - SyntaxKind.MethodDeclaration > 2)
					{
						if (syntaxKind == SyntaxKind.ConstructorDeclaration)
						{
							ConstructorDeclarationSyntax constructorDeclarationSyntax = (ConstructorDeclarationSyntax)memberDeclaration;
							flag = !LookupPosition.IsInConstructorParameterScope(position, constructorDeclarationSyntax) && !LookupPosition.IsInParameterList(position, constructorDeclarationSyntax);
							goto IL_0128;
						}
						if (syntaxKind != SyntaxKind.DestructorDeclaration)
						{
							goto IL_0128;
						}
					}
					BaseMethodDeclarationSyntax baseMethodDeclarationSyntax = (BaseMethodDeclarationSyntax)memberDeclaration;
					flag = !LookupPosition.IsInBody(position, baseMethodDeclarationSyntax) && !LookupPosition.IsInParameterList(position, baseMethodDeclarationSyntax);
				}
				else if (syntaxKind - SyntaxKind.GetAccessorDeclaration > 3 && syntaxKind != SyntaxKind.InitAccessorDeclaration)
				{
					if (syntaxKind == SyntaxKind.RecordDeclaration)
					{
						RecordDeclarationSyntax recordDeclarationSyntax = (RecordDeclarationSyntax)memberDeclaration;
						if (recordDeclarationSyntax.ParameterList == null)
						{
							flag = true;
						}
						else
						{
							PrimaryConstructorBaseTypeSyntax primaryConstructorBaseTypeIfClass = recordDeclarationSyntax.PrimaryConstructorBaseTypeIfClass;
							ArgumentListSyntax argumentListSyntax = ((primaryConstructorBaseTypeIfClass != null) ? primaryConstructorBaseTypeIfClass.ArgumentList : null);
							flag = argumentListSyntax == null || !LookupPosition.IsBetweenTokens(position, argumentListSyntax.OpenParenToken, argumentListSyntax.CloseParenToken);
						}
					}
				}
				else
				{
					flag = !LookupPosition.IsInBody(position, (AccessorDeclarationSyntax)memberDeclaration);
				}
			}
			IL_0128:
			if (!flag)
			{
				return this.GetMemberModel(csharpSyntaxNode);
			}
			return null;
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x000790DC File Offset: 0x000772DC
		internal override MemberSemanticModel GetMemberModel(SyntaxNode node)
		{
			if (SyntaxTreeSemanticModel.IsInDocumentationComment(node))
			{
				return null;
			}
			CSharpSyntaxNode csharpSyntaxNode = SyntaxTreeSemanticModel.GetMemberDeclaration(node) ?? (node as CompilationUnitSyntax);
			if (csharpSyntaxNode != null)
			{
				TextSpan span = node.Span;
				SyntaxKind syntaxKind = csharpSyntaxNode.Kind();
				if (syntaxKind > SyntaxKind.ConstructorDeclaration)
				{
					if (syntaxKind <= SyntaxKind.Parameter)
					{
						switch (syntaxKind)
						{
						case SyntaxKind.DestructorDeclaration:
						{
							DestructorDeclarationSyntax destructorDeclarationSyntax = (DestructorDeclarationSyntax)csharpSyntaxNode;
							ArrowExpressionClauseSyntax expressionBodySyntax = destructorDeclarationSyntax.GetExpressionBodySyntax();
							if (expressionBodySyntax == null || !expressionBodySyntax.FullSpan.Contains(span))
							{
								BlockSyntax body = destructorDeclarationSyntax.Body;
								if (body == null || !body.FullSpan.Contains(span))
								{
									return null;
								}
							}
							return this.GetOrAddModel(destructorDeclarationSyntax);
						}
						case SyntaxKind.PropertyDeclaration:
						{
							PropertyDeclarationSyntax propertyDeclarationSyntax = (PropertyDeclarationSyntax)csharpSyntaxNode;
							return this.GetOrAddModelIfContains(propertyDeclarationSyntax.Initializer, span) ?? this.GetOrAddModelIfContains(propertyDeclarationSyntax.ExpressionBody, span);
						}
						case SyntaxKind.EventDeclaration:
						case SyntaxKind.AccessorList:
							goto IL_03B6;
						case SyntaxKind.IndexerDeclaration:
						{
							IndexerDeclarationSyntax indexerDeclarationSyntax = (IndexerDeclarationSyntax)csharpSyntaxNode;
							return this.GetOrAddModelIfContains(indexerDeclarationSyntax.ExpressionBody, span);
						}
						case SyntaxKind.GetAccessorDeclaration:
						case SyntaxKind.SetAccessorDeclaration:
						case SyntaxKind.AddAccessorDeclaration:
						case SyntaxKind.RemoveAccessorDeclaration:
							break;
						default:
							if (syntaxKind != SyntaxKind.Parameter)
							{
								goto IL_03B6;
							}
							if (node != csharpSyntaxNode)
							{
								return this.GetOrAddModelForParameter((ParameterSyntax)csharpSyntaxNode, span);
							}
							return this.GetMemberModel(csharpSyntaxNode.Parent);
						}
					}
					else if (syntaxKind != SyntaxKind.InitAccessorDeclaration)
					{
						if (syntaxKind != SyntaxKind.RecordDeclaration)
						{
							goto IL_03B6;
						}
						RecordDeclarationSyntax recordDeclarationSyntax = (RecordDeclarationSyntax)csharpSyntaxNode;
						if (recordDeclarationSyntax.ParameterList != null)
						{
							PrimaryConstructorBaseTypeSyntax primaryConstructorBaseTypeIfClass = recordDeclarationSyntax.PrimaryConstructorBaseTypeIfClass;
							if (primaryConstructorBaseTypeIfClass != null && (node == primaryConstructorBaseTypeIfClass || primaryConstructorBaseTypeIfClass.ArgumentList.FullSpan.Contains(span)))
							{
								return this.GetOrAddModel(csharpSyntaxNode);
							}
						}
						return null;
					}
					AccessorDeclarationSyntax accessorDeclarationSyntax = (AccessorDeclarationSyntax)csharpSyntaxNode;
					ArrowExpressionClauseSyntax expressionBody = accessorDeclarationSyntax.ExpressionBody;
					if (expressionBody == null || !expressionBody.FullSpan.Contains(span))
					{
						BlockSyntax body2 = accessorDeclarationSyntax.Body;
						if (body2 == null || !body2.FullSpan.Contains(span))
						{
							return null;
						}
					}
					return this.GetOrAddModel(accessorDeclarationSyntax);
				}
				if (syntaxKind <= SyntaxKind.GlobalStatement)
				{
					if (syntaxKind != SyntaxKind.CompilationUnit)
					{
						if (syntaxKind == SyntaxKind.GlobalStatement)
						{
							if (SyntaxFacts.IsSimpleProgramTopLevelStatement((GlobalStatementSyntax)csharpSyntaxNode))
							{
								return this.GetOrAddModel((CompilationUnitSyntax)csharpSyntaxNode.Parent);
							}
							return this.GetOrAddModel(csharpSyntaxNode);
						}
					}
					else if (SimpleProgramNamedTypeSymbol.GetSimpleProgramEntryPoint(this.Compilation, (CompilationUnitSyntax)csharpSyntaxNode, false) != null)
					{
						return this.GetOrAddModel(csharpSyntaxNode);
					}
				}
				else
				{
					if (syntaxKind == SyntaxKind.Attribute)
					{
						return this.GetOrAddModelForAttribute((AttributeSyntax)csharpSyntaxNode);
					}
					switch (syntaxKind)
					{
					case SyntaxKind.EnumMemberDeclaration:
					{
						EnumMemberDeclarationSyntax enumMemberDeclarationSyntax = (EnumMemberDeclarationSyntax)csharpSyntaxNode;
						if (enumMemberDeclarationSyntax.EqualsValue == null)
						{
							return null;
						}
						return this.GetOrAddModelIfContains(enumMemberDeclarationSyntax.EqualsValue, span);
					}
					case SyntaxKind.FieldDeclaration:
					case SyntaxKind.EventFieldDeclaration:
						foreach (VariableDeclaratorSyntax variableDeclaratorSyntax in ((BaseFieldDeclarationSyntax)csharpSyntaxNode).Declaration.Variables)
						{
							MemberSemanticModel orAddModelIfContains = this.GetOrAddModelIfContains(variableDeclaratorSyntax.Initializer, span);
							if (orAddModelIfContains != null)
							{
								return orAddModelIfContains;
							}
						}
						break;
					case SyntaxKind.MethodDeclaration:
					case SyntaxKind.OperatorDeclaration:
					case SyntaxKind.ConversionOperatorDeclaration:
					{
						BaseMethodDeclarationSyntax baseMethodDeclarationSyntax = (BaseMethodDeclarationSyntax)csharpSyntaxNode;
						ArrowExpressionClauseSyntax expressionBodySyntax2 = baseMethodDeclarationSyntax.GetExpressionBodySyntax();
						if (expressionBodySyntax2 == null || !expressionBodySyntax2.FullSpan.Contains(span))
						{
							BlockSyntax body3 = baseMethodDeclarationSyntax.Body;
							if (body3 == null || !body3.FullSpan.Contains(span))
							{
								return null;
							}
						}
						return this.GetOrAddModel(baseMethodDeclarationSyntax);
					}
					case SyntaxKind.ConstructorDeclaration:
					{
						ConstructorDeclarationSyntax constructorDeclarationSyntax = (ConstructorDeclarationSyntax)csharpSyntaxNode;
						ArrowExpressionClauseSyntax expressionBodySyntax3 = constructorDeclarationSyntax.GetExpressionBodySyntax();
						ConstructorInitializerSyntax initializer = constructorDeclarationSyntax.Initializer;
						if ((initializer == null || !initializer.FullSpan.Contains(span)) && (expressionBodySyntax3 == null || !expressionBodySyntax3.FullSpan.Contains(span)))
						{
							BlockSyntax body4 = constructorDeclarationSyntax.Body;
							if (body4 == null || !body4.FullSpan.Contains(span))
							{
								return null;
							}
						}
						return this.GetOrAddModel(constructorDeclarationSyntax);
					}
					}
				}
			}
			IL_03B6:
			return null;
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x0600168A RID: 5770 RVA: 0x000794A0 File Offset: 0x000776A0
		internal ImmutableDictionary<CSharpSyntaxNode, MemberSemanticModel> TestOnlyMemberModels
		{
			get
			{
				return this._memberModels;
			}
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x000794A8 File Offset: 0x000776A8
		private MemberSemanticModel GetOrAddModelForAttribute(AttributeSyntax attribute)
		{
			MemberSemanticModel memberSemanticModel = ((attribute.Parent != null) ? this.GetMemberModel(attribute.Parent) : null);
			if (memberSemanticModel == null)
			{
				return this.GetOrAddModel(attribute);
			}
			return ImmutableInterlocked.GetOrAdd<CSharpSyntaxNode, MemberSemanticModel, ValueTuple<Binder, MemberSemanticModel>>(ref this._memberModels, attribute, (CSharpSyntaxNode node, [TupleElementNames(new string[] { "binder", "model" })] ValueTuple<Binder, MemberSemanticModel> binderAndModel) => this.CreateModelForAttribute(binderAndModel.Item1, (AttributeSyntax)node, binderAndModel.Item2), new ValueTuple<Binder, MemberSemanticModel>(memberSemanticModel.GetEnclosingBinder(attribute.SpanStart), memberSemanticModel));
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x00079504 File Offset: 0x00077704
		private static bool IsInDocumentationComment(SyntaxNode node)
		{
			for (SyntaxNode syntaxNode = node; syntaxNode != null; syntaxNode = syntaxNode.Parent)
			{
				if (SyntaxFacts.IsDocumentationCommentTrivia(syntaxNode.Kind()))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x00079530 File Offset: 0x00077730
		private MemberSemanticModel GetOrAddModelForParameter(ParameterSyntax paramDecl, TextSpan span)
		{
			EqualsValueClauseSyntax @default = paramDecl.Default;
			MemberSemanticModel memberSemanticModel = ((paramDecl.Parent != null) ? this.GetMemberModel(paramDecl.Parent) : null);
			if (memberSemanticModel == null)
			{
				return this.GetOrAddModelIfContains(@default, span);
			}
			if (@default != null && @default.FullSpan.Contains(span))
			{
				ParameterSymbol symbol = memberSemanticModel.GetDeclaredSymbol(paramDecl, default(CancellationToken)).GetSymbol<ParameterSymbol>();
				if (symbol != null)
				{
					return ImmutableInterlocked.GetOrAdd<CSharpSyntaxNode, MemberSemanticModel, ValueTuple<CSharpCompilation, ParameterSyntax, ParameterSymbol, MemberSemanticModel>>(ref this._memberModels, @default, (CSharpSyntaxNode equalsValue, [TupleElementNames(new string[] { "compilation", "paramDecl", "parameterSymbol", "containing" })] ValueTuple<CSharpCompilation, ParameterSyntax, ParameterSymbol, MemberSemanticModel> tuple) => InitializerSemanticModel.Create(this, tuple.Item2, tuple.Item3, tuple.Item4.GetEnclosingBinder(tuple.Item2.SpanStart).CreateBinderForParameterDefaultValue(tuple.Item3, (EqualsValueClauseSyntax)equalsValue), tuple.Item4.GetRemappedSymbols()), new ValueTuple<CSharpCompilation, ParameterSyntax, ParameterSymbol, MemberSemanticModel>(this.Compilation, paramDecl, symbol, memberSemanticModel));
				}
			}
			return memberSemanticModel;
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x000795BE File Offset: 0x000777BE
		private static CSharpSyntaxNode GetMemberDeclaration(SyntaxNode node)
		{
			return node.FirstAncestorOrSelf<CSharpSyntaxNode>(SyntaxTreeSemanticModel.s_isMemberDeclarationFunction, true);
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x000795CC File Offset: 0x000777CC
		private MemberSemanticModel GetOrAddModelIfContains(CSharpSyntaxNode node, TextSpan span)
		{
			if (node != null && node.FullSpan.Contains(span))
			{
				return this.GetOrAddModel(node);
			}
			return null;
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x000795F8 File Offset: 0x000777F8
		private MemberSemanticModel GetOrAddModel(CSharpSyntaxNode node)
		{
			Func<CSharpSyntaxNode, MemberSemanticModel> func;
			if ((func = this._createMemberModelFunction) == null)
			{
				func = (this._createMemberModelFunction = new Func<CSharpSyntaxNode, MemberSemanticModel>(this.CreateMemberModel));
			}
			Func<CSharpSyntaxNode, MemberSemanticModel> func2 = func;
			return this.GetOrAddModel(node, func2);
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x0007962D File Offset: 0x0007782D
		internal MemberSemanticModel GetOrAddModel(CSharpSyntaxNode node, Func<CSharpSyntaxNode, MemberSemanticModel> createMemberModelFunction)
		{
			return ImmutableInterlocked.GetOrAdd<CSharpSyntaxNode, MemberSemanticModel>(ref this._memberModels, node, createMemberModelFunction);
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x0007963C File Offset: 0x0007783C
		private MemberSemanticModel CreateMemberModel(CSharpSyntaxNode node)
		{
			SyntaxTreeSemanticModel.<>c__DisplayClass77_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.node = node;
			SyntaxKind syntaxKind = CS$<>8__locals1.node.Kind();
			if (syntaxKind > SyntaxKind.Attribute)
			{
				if (syntaxKind <= SyntaxKind.RemoveAccessorDeclaration)
				{
					if (syntaxKind - SyntaxKind.MethodDeclaration <= 3 || syntaxKind == SyntaxKind.DestructorDeclaration)
					{
						MemberDeclarationSyntax memberDeclarationSyntax = (MemberDeclarationSyntax)CS$<>8__locals1.node;
						SourceMemberMethodSymbol symbol = this.GetDeclaredSymbol(memberDeclarationSyntax, default(CancellationToken)).GetSymbol<SourceMemberMethodSymbol>();
						return this.<CreateMemberModel>g__createMethodBodySemanticModel|77_1(memberDeclarationSyntax, symbol, ref CS$<>8__locals1);
					}
					if (syntaxKind - SyntaxKind.GetAccessorDeclaration > 3)
					{
						goto IL_046B;
					}
				}
				else if (syntaxKind != SyntaxKind.ArrowExpressionClause)
				{
					if (syntaxKind != SyntaxKind.InitAccessorDeclaration)
					{
						if (syntaxKind != SyntaxKind.RecordDeclaration)
						{
							goto IL_046B;
						}
						SynthesizedRecordConstructor synthesizedRecordConstructor = this.TryGetSynthesizedRecordConstructor((RecordDeclarationSyntax)CS$<>8__locals1.node);
						if (synthesizedRecordConstructor == null)
						{
							return null;
						}
						return this.<CreateMemberModel>g__createMethodBodySemanticModel|77_1(CS$<>8__locals1.node, synthesizedRecordConstructor, ref CS$<>8__locals1);
					}
				}
				else
				{
					SourceMemberMethodSymbol sourceMemberMethodSymbol = null;
					ArrowExpressionClauseSyntax arrowExpressionClauseSyntax = (ArrowExpressionClauseSyntax)CS$<>8__locals1.node;
					if (CS$<>8__locals1.node.Parent is BasePropertyDeclarationSyntax)
					{
						sourceMemberMethodSymbol = this.GetDeclaredSymbol(arrowExpressionClauseSyntax, default(CancellationToken)).GetSymbol<SourceMemberMethodSymbol>();
					}
					else
					{
						ExceptionUtilities.UnexpectedValue(CS$<>8__locals1.node.Parent);
					}
					ExecutableCodeBinder executableCodeBinder = ((sourceMemberMethodSymbol != null) ? sourceMemberMethodSymbol.TryGetBodyBinder(this._binderFactory, this.IgnoresAccessibility) : null);
					if (executableCodeBinder == null)
					{
						return null;
					}
					return MethodBodySemanticModel.Create(this, sourceMemberMethodSymbol, new MethodBodySemanticModel.InitialState(arrowExpressionClauseSyntax, null, executableCodeBinder, null, null));
				}
				AccessorDeclarationSyntax accessorDeclarationSyntax = (AccessorDeclarationSyntax)CS$<>8__locals1.node;
				SourceMemberMethodSymbol symbol2 = this.GetDeclaredSymbol(accessorDeclarationSyntax, default(CancellationToken)).GetSymbol<SourceMemberMethodSymbol>();
				return this.<CreateMemberModel>g__createMethodBodySemanticModel|77_1(accessorDeclarationSyntax, symbol2, ref CS$<>8__locals1);
			}
			if (syntaxKind <= SyntaxKind.EqualsValueClause)
			{
				if (syntaxKind != SyntaxKind.Block)
				{
					if (syntaxKind == SyntaxKind.EqualsValueClause)
					{
						SyntaxKind syntaxKind2 = CS$<>8__locals1.node.Parent.Kind();
						if (syntaxKind2 <= SyntaxKind.EnumMemberDeclaration)
						{
							if (syntaxKind2 == SyntaxKind.VariableDeclarator)
							{
								VariableDeclaratorSyntax variableDeclaratorSyntax = (VariableDeclaratorSyntax)CS$<>8__locals1.node.Parent;
								FieldSymbol declaredFieldSymbol = this.GetDeclaredFieldSymbol(variableDeclaratorSyntax);
								return InitializerSemanticModel.Create(this, variableDeclaratorSyntax, declaredFieldSymbol, this.GetFieldOrPropertyInitializerBinder(declaredFieldSymbol, this.<CreateMemberModel>g__defaultOuter|77_0(ref CS$<>8__locals1), variableDeclaratorSyntax.Initializer));
							}
							if (syntaxKind2 == SyntaxKind.EnumMemberDeclaration)
							{
								EnumMemberDeclarationSyntax enumMemberDeclarationSyntax = (EnumMemberDeclarationSyntax)CS$<>8__locals1.node.Parent;
								FieldSymbol symbol3 = this.GetDeclaredSymbol(enumMemberDeclarationSyntax, default(CancellationToken)).GetSymbol<FieldSymbol>();
								if (symbol3 == null)
								{
									return null;
								}
								return InitializerSemanticModel.Create(this, enumMemberDeclarationSyntax, symbol3, this.GetFieldOrPropertyInitializerBinder(symbol3, this.<CreateMemberModel>g__defaultOuter|77_0(ref CS$<>8__locals1), enumMemberDeclarationSyntax.EqualsValue));
							}
						}
						else
						{
							if (syntaxKind2 == SyntaxKind.PropertyDeclaration)
							{
								PropertyDeclarationSyntax propertyDeclarationSyntax = (PropertyDeclarationSyntax)CS$<>8__locals1.node.Parent;
								SourcePropertySymbol symbol4 = this.GetDeclaredSymbol(propertyDeclarationSyntax, default(CancellationToken)).GetSymbol<SourcePropertySymbol>();
								return InitializerSemanticModel.Create(this, propertyDeclarationSyntax, symbol4, this.GetFieldOrPropertyInitializerBinder(symbol4.BackingField, this.<CreateMemberModel>g__defaultOuter|77_0(ref CS$<>8__locals1), propertyDeclarationSyntax.Initializer));
							}
							if (syntaxKind2 == SyntaxKind.Parameter)
							{
								ParameterSyntax parameterSyntax = (ParameterSyntax)CS$<>8__locals1.node.Parent;
								ParameterSymbol declaredNonLambdaParameterSymbol = this.GetDeclaredNonLambdaParameterSymbol(parameterSyntax, default(CancellationToken));
								if (declaredNonLambdaParameterSymbol == null)
								{
									return null;
								}
								return InitializerSemanticModel.Create(this, parameterSyntax, declaredNonLambdaParameterSymbol, this.<CreateMemberModel>g__defaultOuter|77_0(ref CS$<>8__locals1).CreateBinderForParameterDefaultValue(declaredNonLambdaParameterSymbol, (EqualsValueClauseSyntax)CS$<>8__locals1.node), null);
							}
						}
						throw ExceptionUtilities.UnexpectedValue(CS$<>8__locals1.node.Parent.Kind());
					}
				}
				else
				{
					ExceptionUtilities.UnexpectedValue(CS$<>8__locals1.node.Parent);
				}
			}
			else
			{
				if (syntaxKind == SyntaxKind.CompilationUnit)
				{
					return this.<CreateMemberModel>g__createMethodBodySemanticModel|77_1(CS$<>8__locals1.node, SimpleProgramNamedTypeSymbol.GetSimpleProgramEntryPoint(this.Compilation, (CompilationUnitSyntax)CS$<>8__locals1.node, false), ref CS$<>8__locals1);
				}
				if (syntaxKind != SyntaxKind.GlobalStatement)
				{
					if (syntaxKind == SyntaxKind.Attribute)
					{
						return this.CreateModelForAttribute(this.<CreateMemberModel>g__defaultOuter|77_0(ref CS$<>8__locals1), (AttributeSyntax)CS$<>8__locals1.node, null);
					}
				}
				else
				{
					CSharpSyntaxNode parent = CS$<>8__locals1.node.Parent;
					if (parent.Kind() == SyntaxKind.CompilationUnit && !this.IsRegularCSharp && this._compilation.ScriptClass != null)
					{
						SynthesizedInteractiveInitializerMethod scriptInitializer = this._compilation.ScriptClass.GetScriptInitializer();
						if (scriptInitializer == null)
						{
							return null;
						}
						if (this._globalStatementLabels == null)
						{
							Interlocked.CompareExchange<ScriptLocalScopeBinder.Labels>(ref this._globalStatementLabels, new ScriptLocalScopeBinder.Labels(scriptInitializer, (CompilationUnitSyntax)parent), null);
						}
						return MethodBodySemanticModel.Create(this, scriptInitializer, new MethodBodySemanticModel.InitialState(CS$<>8__locals1.node, null, new ExecutableCodeBinder(CS$<>8__locals1.node, scriptInitializer, new ScriptLocalScopeBinder(this._globalStatementLabels, this.<CreateMemberModel>g__defaultOuter|77_0(ref CS$<>8__locals1)), null), null, null));
					}
				}
			}
			IL_046B:
			return null;
		}

		// Token: 0x06001693 RID: 5779 RVA: 0x00079AB8 File Offset: 0x00077CB8
		private SynthesizedRecordConstructor TryGetSynthesizedRecordConstructor(RecordDeclarationSyntax node)
		{
			SynthesizedRecordConstructor synthesizedRecordConstructor = Enumerable.SingleOrDefault<SynthesizedRecordConstructor>(this.GetDeclaredType(node).GetMembersUnordered().OfType<SynthesizedRecordConstructor>());
			if (((synthesizedRecordConstructor != null) ? synthesizedRecordConstructor.SyntaxRef.SyntaxTree : null) != node.SyntaxTree || synthesizedRecordConstructor.GetSyntax() != node)
			{
				return null;
			}
			return synthesizedRecordConstructor;
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x00079B04 File Offset: 0x00077D04
		private AttributeSemanticModel CreateModelForAttribute(Binder enclosingBinder, AttributeSyntax attribute, MemberSemanticModel containingModel)
		{
			AliasSymbol aliasSymbol;
			NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)enclosingBinder.BindType(attribute.Name, BindingDiagnosticBag.Discarded, out aliasSymbol, null).Type;
			return AttributeSemanticModel.Create(this, attribute, namedTypeSymbol, aliasSymbol, enclosingBinder.WithAdditionalFlags(BinderFlags.AttributeArgument), (containingModel != null) ? containingModel.GetRemappedSymbols() : null);
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x00079B50 File Offset: 0x00077D50
		private FieldSymbol GetDeclaredFieldSymbol(VariableDeclaratorSyntax variableDecl)
		{
			ISymbol declaredSymbol = this.GetDeclaredSymbol(variableDecl, default(CancellationToken));
			if (declaredSymbol != null)
			{
				SyntaxKind syntaxKind = variableDecl.Parent.Parent.Kind();
				if (syntaxKind == SyntaxKind.FieldDeclaration)
				{
					return declaredSymbol.GetSymbol<FieldSymbol>();
				}
				if (syntaxKind == SyntaxKind.EventFieldDeclaration)
				{
					return declaredSymbol.GetSymbol<EventSymbol>().AssociatedField;
				}
			}
			return null;
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x00079BA8 File Offset: 0x00077DA8
		private Binder GetFieldOrPropertyInitializerBinder(FieldSymbol symbol, Binder outer, EqualsValueClauseSyntax initializer)
		{
			outer = outer.GetFieldInitializerBinder(symbol, !this.IsRegularCSharp && symbol.ContainingType.IsScriptClass);
			if (initializer != null)
			{
				outer = new ExecutableCodeBinder(initializer, symbol, outer, null);
			}
			return outer;
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x00079BD8 File Offset: 0x00077DD8
		private static bool IsMemberDeclaration(CSharpSyntaxNode node)
		{
			return node is MemberDeclarationSyntax || node is AccessorDeclarationSyntax || node.Kind() == SyntaxKind.Attribute || node.Kind() == SyntaxKind.Parameter;
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06001698 RID: 5784 RVA: 0x00079C06 File Offset: 0x00077E06
		private bool IsRegularCSharp
		{
			get
			{
				return this.SyntaxTree.Options.Kind == SourceCodeKind.Regular;
			}
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x00079C1B File Offset: 0x00077E1B
		public override INamespaceSymbol GetDeclaredSymbol(NamespaceDeclarationSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			base.CheckSyntaxNode(declarationSyntax);
			return this.GetDeclaredNamespace(declarationSyntax).GetPublicSymbol();
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x00079C30 File Offset: 0x00077E30
		private NamespaceSymbol GetDeclaredNamespace(NamespaceDeclarationSyntax declarationSyntax)
		{
			NamespaceOrTypeSymbol namespaceOrTypeSymbol;
			if (declarationSyntax.Parent.Kind() == SyntaxKind.CompilationUnit)
			{
				namespaceOrTypeSymbol = this._compilation.Assembly.GlobalNamespace;
			}
			else
			{
				namespaceOrTypeSymbol = this.GetDeclaredNamespaceOrType(declarationSyntax.Parent);
			}
			NamespaceSymbol namespaceSymbol = (NamespaceSymbol)this.GetDeclaredMember(namespaceOrTypeSymbol, declarationSyntax.Span, declarationSyntax.Name);
			return this._compilation.GetCompilationNamespace(namespaceSymbol);
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x00079C96 File Offset: 0x00077E96
		public override INamedTypeSymbol GetDeclaredSymbol(BaseTypeDeclarationSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			base.CheckSyntaxNode(declarationSyntax);
			return this.GetDeclaredType(declarationSyntax).GetPublicSymbol();
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x00079CAB File Offset: 0x00077EAB
		public override INamedTypeSymbol GetDeclaredSymbol(DelegateDeclarationSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			base.CheckSyntaxNode(declarationSyntax);
			return this.GetDeclaredType(declarationSyntax).GetPublicSymbol();
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x00079CC0 File Offset: 0x00077EC0
		private NamedTypeSymbol GetDeclaredType(BaseTypeDeclarationSyntax declarationSyntax)
		{
			string valueText = declarationSyntax.Identifier.ValueText;
			return this.GetDeclaredNamedType(declarationSyntax, valueText);
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x00079CE4 File Offset: 0x00077EE4
		private NamedTypeSymbol GetDeclaredType(DelegateDeclarationSyntax declarationSyntax)
		{
			string valueText = declarationSyntax.Identifier.ValueText;
			return this.GetDeclaredNamedType(declarationSyntax, valueText);
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x00079D08 File Offset: 0x00077F08
		private NamedTypeSymbol GetDeclaredNamedType(CSharpSyntaxNode declarationSyntax, string name)
		{
			NamespaceOrTypeSymbol declaredTypeMemberContainer = this.GetDeclaredTypeMemberContainer(declarationSyntax);
			return this.GetDeclaredMember(declaredTypeMemberContainer, declarationSyntax.Span, name) as NamedTypeSymbol;
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x00079D30 File Offset: 0x00077F30
		private NamespaceOrTypeSymbol GetDeclaredNamespaceOrType(CSharpSyntaxNode declarationSyntax)
		{
			NamespaceDeclarationSyntax namespaceDeclarationSyntax = declarationSyntax as NamespaceDeclarationSyntax;
			if (namespaceDeclarationSyntax != null)
			{
				return this.GetDeclaredNamespace(namespaceDeclarationSyntax);
			}
			BaseTypeDeclarationSyntax baseTypeDeclarationSyntax = declarationSyntax as BaseTypeDeclarationSyntax;
			if (baseTypeDeclarationSyntax != null)
			{
				return this.GetDeclaredType(baseTypeDeclarationSyntax);
			}
			DelegateDeclarationSyntax delegateDeclarationSyntax = declarationSyntax as DelegateDeclarationSyntax;
			if (delegateDeclarationSyntax != null)
			{
				return this.GetDeclaredType(delegateDeclarationSyntax);
			}
			return null;
		}

		// Token: 0x060016A1 RID: 5793 RVA: 0x00079D74 File Offset: 0x00077F74
		public override ISymbol GetDeclaredSymbol(MemberDeclarationSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			base.CheckSyntaxNode(declarationSyntax);
			SyntaxKind syntaxKind = declarationSyntax.Kind();
			if (syntaxKind == SyntaxKind.GlobalStatement)
			{
				return null;
			}
			if (syntaxKind - SyntaxKind.FieldDeclaration <= 1)
			{
				return null;
			}
			if (syntaxKind != SyntaxKind.IncompleteMember)
			{
				return (this.GetDeclaredNamespaceOrType(declarationSyntax) ?? this.GetDeclaredMemberSymbol(declarationSyntax)).GetPublicSymbol();
			}
			return null;
		}

		// Token: 0x060016A2 RID: 5794 RVA: 0x00079DC8 File Offset: 0x00077FC8
		public override IMethodSymbol GetDeclaredSymbol(CompilationUnitSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			base.CheckSyntaxNode(declarationSyntax);
			return SimpleProgramNamedTypeSymbol.GetSimpleProgramEntryPoint(this.Compilation, declarationSyntax, false).GetPublicSymbol();
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x00079DE3 File Offset: 0x00077FE3
		public override ISymbol GetDeclaredSymbol(LocalFunctionStatementSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			base.CheckSyntaxNode(declarationSyntax);
			MemberSemanticModel memberModel = this.GetMemberModel(declarationSyntax);
			if (memberModel == null)
			{
				return null;
			}
			return memberModel.GetDeclaredSymbol(declarationSyntax, cancellationToken);
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x00079E00 File Offset: 0x00078000
		public override IFieldSymbol GetDeclaredSymbol(EnumMemberDeclarationSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ((FieldSymbol)this.GetDeclaredMemberSymbol(declarationSyntax)).GetPublicSymbol();
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x00079E13 File Offset: 0x00078013
		public override IMethodSymbol GetDeclaredSymbol(BaseMethodDeclarationSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ((MethodSymbol)this.GetDeclaredMemberSymbol(declarationSyntax)).GetPublicSymbol();
		}

		// Token: 0x060016A6 RID: 5798 RVA: 0x00079E26 File Offset: 0x00078026
		public override ISymbol GetDeclaredSymbol(BasePropertyDeclarationSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.GetDeclaredMemberSymbol(declarationSyntax).GetPublicSymbol();
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x00079E34 File Offset: 0x00078034
		public override IPropertySymbol GetDeclaredSymbol(PropertyDeclarationSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ((PropertySymbol)this.GetDeclaredMemberSymbol(declarationSyntax)).GetPublicSymbol();
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x00079E47 File Offset: 0x00078047
		public override IPropertySymbol GetDeclaredSymbol(IndexerDeclarationSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ((PropertySymbol)this.GetDeclaredMemberSymbol(declarationSyntax)).GetPublicSymbol();
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x00079E5A File Offset: 0x0007805A
		public override IEventSymbol GetDeclaredSymbol(EventDeclarationSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ((EventSymbol)this.GetDeclaredMemberSymbol(declarationSyntax)).GetPublicSymbol();
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x00079E70 File Offset: 0x00078070
		public override IMethodSymbol GetDeclaredSymbol(AccessorDeclarationSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			base.CheckSyntaxNode(declarationSyntax);
			if (declarationSyntax.Kind() == SyntaxKind.UnknownAccessorDeclaration)
			{
				return null;
			}
			CSharpSyntaxNode parent = declarationSyntax.Parent.Parent;
			SyntaxKind syntaxKind = parent.Kind();
			if (syntaxKind == SyntaxKind.EventFieldDeclaration || syntaxKind - SyntaxKind.PropertyDeclaration <= 2)
			{
				NamespaceOrTypeSymbol declaredTypeMemberContainer = this.GetDeclaredTypeMemberContainer(parent);
				return (this.GetDeclaredMember(declaredTypeMemberContainer, declarationSyntax.Span, null) as MethodSymbol).GetPublicSymbol();
			}
			throw ExceptionUtilities.UnexpectedValue(parent.Kind());
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x00079EEC File Offset: 0x000780EC
		public override IMethodSymbol GetDeclaredSymbol(ArrowExpressionClauseSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			base.CheckSyntaxNode(declarationSyntax);
			CSharpSyntaxNode parent = declarationSyntax.Parent;
			SyntaxKind syntaxKind = parent.Kind();
			if (syntaxKind == SyntaxKind.PropertyDeclaration || syntaxKind == SyntaxKind.IndexerDeclaration)
			{
				NamespaceOrTypeSymbol declaredTypeMemberContainer = this.GetDeclaredTypeMemberContainer(parent);
				return (this.GetDeclaredMember(declaredTypeMemberContainer, declarationSyntax.Span, null) as MethodSymbol).GetPublicSymbol();
			}
			ExceptionUtilities.UnexpectedValue(parent.Kind());
			return null;
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x00079F54 File Offset: 0x00078154
		private string GetDeclarationName(CSharpSyntaxNode declaration)
		{
			SyntaxKind syntaxKind = declaration.Kind();
			if (syntaxKind <= SyntaxKind.DelegateDeclaration)
			{
				if (syntaxKind == SyntaxKind.VariableDeclarator)
				{
					return ((VariableDeclaratorSyntax)declaration).Identifier.ValueText;
				}
				if (syntaxKind - SyntaxKind.ClassDeclaration > 3)
				{
					if (syntaxKind != SyntaxKind.DelegateDeclaration)
					{
						goto IL_01E0;
					}
					return ((DelegateDeclarationSyntax)declaration).Identifier.ValueText;
				}
			}
			else if (syntaxKind <= SyntaxKind.IndexerDeclaration)
			{
				switch (syntaxKind)
				{
				case SyntaxKind.EnumMemberDeclaration:
					return ((EnumMemberDeclarationSyntax)declaration).Identifier.ValueText;
				case SyntaxKind.FieldDeclaration:
				case SyntaxKind.EventFieldDeclaration:
					throw new ArgumentException(CSharpResources.InvalidGetDeclarationNameMultipleDeclarators);
				case SyntaxKind.MethodDeclaration:
				{
					MethodDeclarationSyntax methodDeclarationSyntax = (MethodDeclarationSyntax)declaration;
					return this.GetDeclarationName(declaration, methodDeclarationSyntax.ExplicitInterfaceSpecifier, methodDeclarationSyntax.Identifier.ValueText);
				}
				case SyntaxKind.OperatorDeclaration:
					return OperatorFacts.OperatorNameFromDeclaration((OperatorDeclarationSyntax)declaration);
				case SyntaxKind.ConversionOperatorDeclaration:
					if (((ConversionOperatorDeclarationSyntax)declaration).ImplicitOrExplicitKeyword.Kind() == SyntaxKind.ExplicitKeyword)
					{
						return "op_Explicit";
					}
					return "op_Implicit";
				case SyntaxKind.ConstructorDeclaration:
					if (((ConstructorDeclarationSyntax)declaration).Modifiers.Any(SyntaxKind.StaticKeyword))
					{
						return ".cctor";
					}
					return ".ctor";
				default:
					switch (syntaxKind)
					{
					case SyntaxKind.DestructorDeclaration:
						return "Finalize";
					case SyntaxKind.PropertyDeclaration:
					{
						PropertyDeclarationSyntax propertyDeclarationSyntax = (PropertyDeclarationSyntax)declaration;
						return this.GetDeclarationName(declaration, propertyDeclarationSyntax.ExplicitInterfaceSpecifier, propertyDeclarationSyntax.Identifier.ValueText);
					}
					case SyntaxKind.EventDeclaration:
					{
						EventDeclarationSyntax eventDeclarationSyntax = (EventDeclarationSyntax)declaration;
						return this.GetDeclarationName(declaration, eventDeclarationSyntax.ExplicitInterfaceSpecifier, eventDeclarationSyntax.Identifier.ValueText);
					}
					case SyntaxKind.IndexerDeclaration:
					{
						IndexerDeclarationSyntax indexerDeclarationSyntax = (IndexerDeclarationSyntax)declaration;
						return this.GetDeclarationName(declaration, indexerDeclarationSyntax.ExplicitInterfaceSpecifier, "this[]");
					}
					default:
						goto IL_01E0;
					}
					break;
				}
			}
			else
			{
				if (syntaxKind == SyntaxKind.IncompleteMember)
				{
					return null;
				}
				if (syntaxKind != SyntaxKind.RecordDeclaration)
				{
					goto IL_01E0;
				}
			}
			return ((BaseTypeDeclarationSyntax)declaration).Identifier.ValueText;
			IL_01E0:
			throw ExceptionUtilities.UnexpectedValue(declaration.Kind());
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x0007A151 File Offset: 0x00078351
		private string GetDeclarationName(CSharpSyntaxNode declaration, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifierOpt, string memberName)
		{
			if (explicitInterfaceSpecifierOpt == null)
			{
				return memberName;
			}
			return ExplicitInterfaceHelpers.GetMemberName(this._binderFactory.GetBinder(declaration, null, null), explicitInterfaceSpecifierOpt, memberName);
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x0007A170 File Offset: 0x00078370
		private Symbol GetDeclaredMember(NamespaceOrTypeSymbol container, TextSpan declarationSpan, NameSyntax name)
		{
			switch (name.Kind())
			{
			case SyntaxKind.IdentifierName:
			case SyntaxKind.GenericName:
				return this.GetDeclaredMember(container, declarationSpan, ((SimpleNameSyntax)name).Identifier.ValueText);
			case SyntaxKind.QualifiedName:
			{
				QualifiedNameSyntax qualifiedNameSyntax = (QualifiedNameSyntax)name;
				NamespaceOrTypeSymbol namespaceOrTypeSymbol = this.GetDeclaredMember(container, declarationSpan, qualifiedNameSyntax.Left) as NamespaceOrTypeSymbol;
				return this.GetDeclaredMember(namespaceOrTypeSymbol, declarationSpan, qualifiedNameSyntax.Right);
			}
			case SyntaxKind.AliasQualifiedName:
			{
				AliasQualifiedNameSyntax aliasQualifiedNameSyntax = (AliasQualifiedNameSyntax)name;
				return this.GetDeclaredMember(container, declarationSpan, aliasQualifiedNameSyntax.Name);
			}
			}
			throw ExceptionUtilities.UnexpectedValue(name.Kind());
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x0007A214 File Offset: 0x00078414
		private Symbol GetDeclaredMember(NamespaceOrTypeSymbol container, TextSpan declarationSpan, string name = null)
		{
			if (container == null)
			{
				return null;
			}
			ImmutableArray<Symbol> immutableArray = ((name != null) ? container.GetMembers(name) : container.GetMembersUnordered());
			Symbol symbol = null;
			foreach (Symbol symbol2 in immutableArray)
			{
				ImplicitNamedTypeSymbol implicitNamedTypeSymbol = symbol2 as ImplicitNamedTypeSymbol;
				if (implicitNamedTypeSymbol != null && implicitNamedTypeSymbol.IsImplicitClass)
				{
					Symbol declaredMember = this.GetDeclaredMember(implicitNamedTypeSymbol, declarationSpan, name);
					if (declaredMember != null)
					{
						return declaredMember;
					}
				}
				foreach (Location location in symbol2.Locations)
				{
					if (location.IsInSource && location.SourceTree == this.SyntaxTree && declarationSpan.Contains(location.SourceSpan))
					{
						if (!location.SourceSpan.IsEmpty || location.SourceSpan.End != declarationSpan.Start)
						{
							return symbol2;
						}
						symbol = symbol2;
					}
				}
				MethodSymbol methodSymbol = ((symbol2.Kind == SymbolKind.Method) ? ((MethodSymbol)symbol2).PartialImplementationPart : null);
				if (methodSymbol != null)
				{
					Location location2 = methodSymbol.Locations[0];
					if (location2.IsInSource && location2.SourceTree == this.SyntaxTree && declarationSpan.Contains(location2.SourceSpan))
					{
						return methodSymbol;
					}
				}
			}
			Symbol symbol3;
			if ((symbol3 = symbol) == null)
			{
				if (name == null)
				{
					return null;
				}
				symbol3 = this.GetDeclaredMember(container, declarationSpan, null);
			}
			return symbol3;
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x0007A374 File Offset: 0x00078574
		public override ISymbol GetDeclaredSymbol(VariableDeclaratorSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			base.CheckSyntaxNode(declarationSyntax);
			BaseFieldDeclarationSyntax baseFieldDeclarationSyntax = ((declarationSyntax.Parent == null) ? null : (declarationSyntax.Parent.Parent as BaseFieldDeclarationSyntax));
			if (baseFieldDeclarationSyntax != null)
			{
				NamespaceOrTypeSymbol declaredTypeMemberContainer = this.GetDeclaredTypeMemberContainer(baseFieldDeclarationSyntax);
				return this.GetDeclaredMember(declaredTypeMemberContainer, declarationSyntax.Span, declarationSyntax.Identifier.ValueText).GetPublicSymbol();
			}
			MemberSemanticModel memberModel = this.GetMemberModel(declarationSyntax);
			if (memberModel == null)
			{
				return null;
			}
			return memberModel.GetDeclaredSymbol(declarationSyntax, cancellationToken);
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x0007A3E4 File Offset: 0x000785E4
		public override ISymbol GetDeclaredSymbol(SingleVariableDesignationSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			MemberSemanticModel memberModel = this.GetMemberModel(declarationSyntax);
			ISymbol symbol = ((memberModel != null) ? memberModel.GetDeclaredSymbol(declarationSyntax, cancellationToken) : null);
			if (symbol != null)
			{
				return symbol;
			}
			Binder enclosingBinder = base.GetEnclosingBinder(declarationSyntax.Position);
			if (enclosingBinder == null)
			{
				return null;
			}
			return enclosingBinder.LookupDeclaredField(declarationSyntax).GetPublicSymbol();
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x0007A42C File Offset: 0x0007862C
		internal override LocalSymbol GetAdjustedLocalSymbol(SourceLocalSymbol originalSymbol)
		{
			int spanStart = originalSymbol.IdentifierToken.SpanStart;
			MemberSemanticModel memberModel = this.GetMemberModel(spanStart);
			return ((memberModel != null) ? memberModel.GetAdjustedLocalSymbol(originalSymbol) : null) ?? originalSymbol;
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x0007A464 File Offset: 0x00078664
		public override ILabelSymbol GetDeclaredSymbol(LabeledStatementSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			base.CheckSyntaxNode(declarationSyntax);
			MemberSemanticModel memberModel = this.GetMemberModel(declarationSyntax);
			if (memberModel != null)
			{
				return memberModel.GetDeclaredSymbol(declarationSyntax, cancellationToken);
			}
			return null;
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x0007A490 File Offset: 0x00078690
		public override ILabelSymbol GetDeclaredSymbol(SwitchLabelSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			base.CheckSyntaxNode(declarationSyntax);
			MemberSemanticModel memberModel = this.GetMemberModel(declarationSyntax);
			if (memberModel != null)
			{
				return memberModel.GetDeclaredSymbol(declarationSyntax, cancellationToken);
			}
			return null;
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x0007A4BC File Offset: 0x000786BC
		public override IAliasSymbol GetDeclaredSymbol(UsingDirectiveSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			base.CheckSyntaxNode(declarationSyntax);
			if (declarationSyntax.Alias == null)
			{
				return null;
			}
			for (Binder binder = this._binderFactory.GetInNamespaceBinder(declarationSyntax.Parent); binder != null; binder = binder.Next)
			{
				ImmutableArray<AliasAndUsingDirective> usingAliases = binder.UsingAliases;
				if (!usingAliases.IsDefault)
				{
					foreach (AliasAndUsingDirective aliasAndUsingDirective in usingAliases)
					{
						if (aliasAndUsingDirective.Alias.Locations[0].SourceSpan == declarationSyntax.Alias.Name.Span)
						{
							return aliasAndUsingDirective.Alias.GetPublicSymbol();
						}
					}
					break;
				}
			}
			return null;
		}

		// Token: 0x060016B6 RID: 5814 RVA: 0x0007A564 File Offset: 0x00078764
		public override IAliasSymbol GetDeclaredSymbol(ExternAliasDirectiveSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			base.CheckSyntaxNode(declarationSyntax);
			for (Binder binder = this._binderFactory.GetInNamespaceBinder(declarationSyntax.Parent); binder != null; binder = binder.Next)
			{
				ImmutableArray<AliasAndExternAliasDirective> externAliases = binder.ExternAliases;
				if (!externAliases.IsDefault)
				{
					foreach (AliasAndExternAliasDirective aliasAndExternAliasDirective in externAliases)
					{
						if (aliasAndExternAliasDirective.Alias.Locations[0].SourceSpan == declarationSyntax.Identifier.Span)
						{
							return aliasAndExternAliasDirective.Alias.GetPublicSymbol();
						}
					}
					break;
				}
			}
			return null;
		}

		// Token: 0x060016B7 RID: 5815 RVA: 0x0007A600 File Offset: 0x00078800
		internal override ImmutableArray<ISymbol> GetDeclaredSymbols(BaseFieldDeclarationSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			base.CheckSyntaxNode(declarationSyntax);
			ArrayBuilder<ISymbol> arrayBuilder = new ArrayBuilder<ISymbol>();
			foreach (VariableDeclaratorSyntax variableDeclaratorSyntax in declarationSyntax.Declaration.Variables)
			{
				ISymbol declaredSymbol = this.GetDeclaredSymbol(variableDeclaratorSyntax, cancellationToken);
				if (declaredSymbol != null)
				{
					arrayBuilder.Add(declaredSymbol);
				}
			}
			return arrayBuilder.ToImmutableAndFree();
		}

		// Token: 0x060016B8 RID: 5816 RVA: 0x0007A660 File Offset: 0x00078860
		private ParameterSymbol GetMethodParameterSymbol(ParameterSyntax parameter, CancellationToken cancellationToken)
		{
			ParameterListSyntax parameterListSyntax = parameter.Parent as ParameterListSyntax;
			if (parameterListSyntax == null)
			{
				return null;
			}
			MemberDeclarationSyntax memberDeclarationSyntax = parameterListSyntax.Parent as MemberDeclarationSyntax;
			if (memberDeclarationSyntax == null)
			{
				return null;
			}
			RecordDeclarationSyntax recordDeclarationSyntax = memberDeclarationSyntax as RecordDeclarationSyntax;
			MethodSymbol methodSymbol;
			if (recordDeclarationSyntax != null && recordDeclarationSyntax.ParameterList == parameterListSyntax)
			{
				methodSymbol = this.TryGetSynthesizedRecordConstructor(recordDeclarationSyntax);
			}
			else
			{
				methodSymbol = (this.GetDeclaredSymbol(memberDeclarationSyntax, cancellationToken) as IMethodSymbol).GetSymbol();
			}
			if (methodSymbol == null)
			{
				return null;
			}
			ParameterSymbol parameterSymbol;
			if ((parameterSymbol = base.GetParameterSymbol(methodSymbol.Parameters, parameter, cancellationToken)) == null)
			{
				if (methodSymbol.PartialDefinitionPart != null)
				{
					return base.GetParameterSymbol(methodSymbol.PartialDefinitionPart.Parameters, parameter, cancellationToken);
				}
				parameterSymbol = null;
			}
			return parameterSymbol;
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x0007A6F4 File Offset: 0x000788F4
		private ParameterSymbol GetIndexerParameterSymbol(ParameterSyntax parameter, CancellationToken cancellationToken)
		{
			BracketedParameterListSyntax bracketedParameterListSyntax = parameter.Parent as BracketedParameterListSyntax;
			if (bracketedParameterListSyntax == null)
			{
				return null;
			}
			MemberDeclarationSyntax memberDeclarationSyntax = bracketedParameterListSyntax.Parent as MemberDeclarationSyntax;
			if (memberDeclarationSyntax == null)
			{
				return null;
			}
			PropertySymbol symbol = (this.GetDeclaredSymbol(memberDeclarationSyntax, cancellationToken) as IPropertySymbol).GetSymbol();
			if (symbol == null)
			{
				return null;
			}
			return base.GetParameterSymbol(symbol.Parameters, parameter, cancellationToken);
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x0007A74C File Offset: 0x0007894C
		private ParameterSymbol GetDelegateParameterSymbol(ParameterSyntax parameter, CancellationToken cancellationToken)
		{
			ParameterListSyntax parameterListSyntax = parameter.Parent as ParameterListSyntax;
			if (parameterListSyntax == null)
			{
				return null;
			}
			DelegateDeclarationSyntax delegateDeclarationSyntax = parameterListSyntax.Parent as DelegateDeclarationSyntax;
			if (delegateDeclarationSyntax == null)
			{
				return null;
			}
			NamedTypeSymbol symbol = this.GetDeclaredSymbol(delegateDeclarationSyntax, cancellationToken).GetSymbol();
			if (symbol == null)
			{
				return null;
			}
			MethodSymbol delegateInvokeMethod = symbol.DelegateInvokeMethod;
			if (delegateInvokeMethod == null || delegateInvokeMethod.HasUseSiteError)
			{
				return null;
			}
			return base.GetParameterSymbol(delegateInvokeMethod.Parameters, parameter, cancellationToken);
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x0007A7B0 File Offset: 0x000789B0
		public override IParameterSymbol GetDeclaredSymbol(ParameterSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			base.CheckSyntaxNode(declarationSyntax);
			MemberSemanticModel memberModel = this.GetMemberModel(declarationSyntax);
			if (memberModel != null)
			{
				return memberModel.GetDeclaredSymbol(declarationSyntax, cancellationToken);
			}
			return this.GetDeclaredNonLambdaParameterSymbol(declarationSyntax, cancellationToken).GetPublicSymbol();
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x0007A7E5 File Offset: 0x000789E5
		private ParameterSymbol GetDeclaredNonLambdaParameterSymbol(ParameterSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			ParameterSymbol parameterSymbol;
			if ((parameterSymbol = this.GetMethodParameterSymbol(declarationSyntax, cancellationToken)) == null)
			{
				parameterSymbol = this.GetIndexerParameterSymbol(declarationSyntax, cancellationToken) ?? this.GetDelegateParameterSymbol(declarationSyntax, cancellationToken);
			}
			return parameterSymbol;
		}

		// Token: 0x060016BD RID: 5821 RVA: 0x0007A808 File Offset: 0x00078A08
		public override ITypeParameterSymbol GetDeclaredSymbol(TypeParameterSyntax typeParameter, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (typeParameter == null)
			{
				throw new ArgumentNullException("typeParameter");
			}
			if (!base.IsInTree(typeParameter))
			{
				throw new ArgumentException("typeParameter not within tree");
			}
			TypeParameterListSyntax typeParameterListSyntax = typeParameter.Parent as TypeParameterListSyntax;
			if (typeParameterListSyntax != null)
			{
				CSharpSyntaxNode parent = typeParameterListSyntax.Parent;
				MemberDeclarationSyntax memberDeclarationSyntax = parent as MemberDeclarationSyntax;
				ISymbol symbol;
				if (memberDeclarationSyntax == null)
				{
					LocalFunctionStatementSyntax localFunctionStatementSyntax = parent as LocalFunctionStatementSyntax;
					if (localFunctionStatementSyntax == null)
					{
						throw ExceptionUtilities.UnexpectedValue(typeParameter.Parent.Kind());
					}
					symbol = this.GetDeclaredSymbol(localFunctionStatementSyntax, cancellationToken);
				}
				else
				{
					symbol = this.GetDeclaredSymbol(memberDeclarationSyntax, cancellationToken);
				}
				Symbol symbol2 = symbol.GetSymbol();
				NamedTypeSymbol namedTypeSymbol = symbol2 as NamedTypeSymbol;
				if (namedTypeSymbol != null)
				{
					return this.GetTypeParameterSymbol(namedTypeSymbol.TypeParameters, typeParameter).GetPublicSymbol();
				}
				MethodSymbol methodSymbol = symbol2 as MethodSymbol;
				if (methodSymbol != null)
				{
					return (this.GetTypeParameterSymbol(methodSymbol.TypeParameters, typeParameter) ?? ((methodSymbol.PartialDefinitionPart == null) ? null : this.GetTypeParameterSymbol(methodSymbol.PartialDefinitionPart.TypeParameters, typeParameter))).GetPublicSymbol();
				}
			}
			return null;
		}

		// Token: 0x060016BE RID: 5822 RVA: 0x0007A908 File Offset: 0x00078B08
		private TypeParameterSymbol GetTypeParameterSymbol(ImmutableArray<TypeParameterSymbol> parameters, TypeParameterSyntax parameter)
		{
			foreach (TypeParameterSymbol typeParameterSymbol in parameters)
			{
				foreach (Location location in typeParameterSymbol.Locations)
				{
					if (location.SourceTree == this.SyntaxTree && parameter.Span.Contains(location.SourceSpan))
					{
						return typeParameterSymbol;
					}
				}
			}
			return null;
		}

		// Token: 0x060016BF RID: 5823 RVA: 0x0007A97D File Offset: 0x00078B7D
		public override ControlFlowAnalysis AnalyzeControlFlow(StatementSyntax firstStatement, StatementSyntax lastStatement)
		{
			this.ValidateStatementRange(firstStatement, lastStatement);
			return new CSharpControlFlowAnalysis(this.RegionAnalysisContext(firstStatement, lastStatement));
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x0007A994 File Offset: 0x00078B94
		private void ValidateStatementRange(StatementSyntax firstStatement, StatementSyntax lastStatement)
		{
			if (firstStatement == null)
			{
				throw new ArgumentNullException("firstStatement");
			}
			if (lastStatement == null)
			{
				throw new ArgumentNullException("lastStatement");
			}
			if (!base.IsInTree(firstStatement))
			{
				throw new ArgumentException("statements not within tree");
			}
			if (firstStatement.Parent == null || firstStatement.Parent != lastStatement.Parent)
			{
				throw new ArgumentException("statements not within the same statement list");
			}
			if (firstStatement.SpanStart > lastStatement.SpanStart)
			{
				throw new ArgumentException("first statement does not precede last statement");
			}
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x0007AA0B File Offset: 0x00078C0B
		public override DataFlowAnalysis AnalyzeDataFlow(ExpressionSyntax expression)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			if (!base.IsInTree(expression))
			{
				throw new ArgumentException("expression not within tree");
			}
			return new CSharpDataFlowAnalysis(this.RegionAnalysisContext(expression));
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x0007AA3B File Offset: 0x00078C3B
		public override DataFlowAnalysis AnalyzeDataFlow(StatementSyntax firstStatement, StatementSyntax lastStatement)
		{
			this.ValidateStatementRange(firstStatement, lastStatement);
			return new CSharpDataFlowAnalysis(this.RegionAnalysisContext(firstStatement, lastStatement));
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x0007AA52 File Offset: 0x00078C52
		private static BoundNode GetBoundRoot(MemberSemanticModel memberModel, out Symbol member)
		{
			member = memberModel.MemberSymbol;
			return memberModel.GetBoundRoot();
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x0007AA64 File Offset: 0x00078C64
		private NamespaceOrTypeSymbol GetDeclaredTypeMemberContainer(CSharpSyntaxNode memberDeclaration)
		{
			if (memberDeclaration.Parent.Kind() == SyntaxKind.CompilationUnit)
			{
				if (memberDeclaration.Kind() == SyntaxKind.NamespaceDeclaration)
				{
					return this._compilation.Assembly.GlobalNamespace;
				}
				if (this.SyntaxTree.Options.Kind != SourceCodeKind.Regular)
				{
					return this.Compilation.ScriptClass;
				}
				if (SyntaxFacts.IsTypeDeclaration(memberDeclaration.Kind()))
				{
					return this._compilation.Assembly.GlobalNamespace;
				}
				return this._compilation.Assembly.GlobalNamespace.ImplicitType;
			}
			else
			{
				NamespaceOrTypeSymbol declaredNamespaceOrType = this.GetDeclaredNamespaceOrType(memberDeclaration.Parent);
				if (!declaredNamespaceOrType.IsNamespace)
				{
					return declaredNamespaceOrType;
				}
				if (memberDeclaration.Kind() == SyntaxKind.NamespaceDeclaration || SyntaxFacts.IsTypeDeclaration(memberDeclaration.Kind()))
				{
					return declaredNamespaceOrType;
				}
				return ((NamespaceSymbol)declaredNamespaceOrType).ImplicitType;
			}
		}

		// Token: 0x060016C5 RID: 5829 RVA: 0x0007AB34 File Offset: 0x00078D34
		private Symbol GetDeclaredMemberSymbol(CSharpSyntaxNode declarationSyntax)
		{
			base.CheckSyntaxNode(declarationSyntax);
			NamespaceOrTypeSymbol declaredTypeMemberContainer = this.GetDeclaredTypeMemberContainer(declarationSyntax);
			string declarationName = this.GetDeclarationName(declarationSyntax);
			return this.GetDeclaredMember(declaredTypeMemberContainer, declarationSyntax.Span, declarationName);
		}

		// Token: 0x060016C6 RID: 5830 RVA: 0x0007AB68 File Offset: 0x00078D68
		public override AwaitExpressionInfo GetAwaitExpressionInfo(AwaitExpressionSyntax node)
		{
			MemberSemanticModel memberModel = this.GetMemberModel(node);
			if (memberModel != null)
			{
				return memberModel.GetAwaitExpressionInfo(node);
			}
			return default(AwaitExpressionInfo);
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x0007AB94 File Offset: 0x00078D94
		public override ForEachStatementInfo GetForEachStatementInfo(ForEachStatementSyntax node)
		{
			MemberSemanticModel memberModel = this.GetMemberModel(node);
			if (memberModel != null)
			{
				return memberModel.GetForEachStatementInfo(node);
			}
			return default(ForEachStatementInfo);
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x0007ABC0 File Offset: 0x00078DC0
		public override ForEachStatementInfo GetForEachStatementInfo(CommonForEachStatementSyntax node)
		{
			MemberSemanticModel memberModel = this.GetMemberModel(node);
			if (memberModel != null)
			{
				return memberModel.GetForEachStatementInfo(node);
			}
			return default(ForEachStatementInfo);
		}

		// Token: 0x060016C9 RID: 5833 RVA: 0x0007ABEC File Offset: 0x00078DEC
		public override DeconstructionInfo GetDeconstructionInfo(AssignmentExpressionSyntax node)
		{
			MemberSemanticModel memberModel = this.GetMemberModel(node);
			if (memberModel == null)
			{
				return default(DeconstructionInfo);
			}
			return memberModel.GetDeconstructionInfo(node);
		}

		// Token: 0x060016CA RID: 5834 RVA: 0x0007AC14 File Offset: 0x00078E14
		public override DeconstructionInfo GetDeconstructionInfo(ForEachVariableStatementSyntax node)
		{
			MemberSemanticModel memberModel = this.GetMemberModel(node);
			if (memberModel == null)
			{
				return default(DeconstructionInfo);
			}
			return memberModel.GetDeconstructionInfo(node);
		}

		// Token: 0x060016CB RID: 5835 RVA: 0x0007AC3C File Offset: 0x00078E3C
		internal override Symbol RemapSymbolIfNecessaryCore(Symbol symbol)
		{
			if (symbol.Locations.IsDefaultOrEmpty)
			{
				return symbol;
			}
			Location location = symbol.Locations[0];
			if (location.SourceTree != this.SyntaxTree)
			{
				return symbol;
			}
			int num = base.CheckAndAdjustPosition(location.SourceSpan.Start);
			MemberSemanticModel memberModel = this.GetMemberModel(num);
			return ((memberModel != null) ? memberModel.RemapSymbolIfNecessaryCore(symbol) : null) ?? symbol;
		}

		// Token: 0x060016CC RID: 5836 RVA: 0x0007ACAC File Offset: 0x00078EAC
		internal override Func<SyntaxNode, bool> GetSyntaxNodesToAnalyzeFilter(SyntaxNode declaredNode, ISymbol declaredSymbol)
		{
			CompilationUnitSyntax <unit>5__2 = declaredNode as CompilationUnitSyntax;
			if (<unit>5__2 == null)
			{
				RecordDeclarationSyntax <recordDeclaration>5__3 = declaredNode as RecordDeclarationSyntax;
				if (<recordDeclaration>5__3 == null)
				{
					PrimaryConstructorBaseTypeSyntax <baseType>5__4 = declaredNode as PrimaryConstructorBaseTypeSyntax;
					if (<baseType>5__4 != null)
					{
						CSharpSyntaxNode parent = <baseType>5__4.Parent;
						if (parent is BaseListSyntax)
						{
							RecordDeclarationSyntax recordDeclarationSyntax = parent.Parent as RecordDeclarationSyntax;
							if (recordDeclarationSyntax != null)
							{
								if (recordDeclarationSyntax.PrimaryConstructorBaseTypeIfClass == declaredNode)
								{
									SynthesizedRecordConstructor synthesizedRecordConstructor = this.TryGetSynthesizedRecordConstructor(recordDeclarationSyntax);
									if (synthesizedRecordConstructor != null && declaredSymbol.GetSymbol() == synthesizedRecordConstructor)
									{
										return (SyntaxNode node) => node != <baseType>5__4.Type;
									}
								}
							}
						}
					}
					else
					{
						ParameterSyntax parameterSyntax = declaredNode as ParameterSyntax;
						if (parameterSyntax != null)
						{
							if (declaredSymbol.Kind == SymbolKind.Property)
							{
								CSharpSyntaxNode parent2 = parameterSyntax.Parent;
								RecordDeclarationSyntax recordDeclarationSyntax2 = ((parent2 != null) ? parent2.Parent : null) as RecordDeclarationSyntax;
								if (recordDeclarationSyntax2 != null && recordDeclarationSyntax2.ParameterList == parameterSyntax.Parent)
								{
									return (SyntaxNode node) => false;
								}
							}
						}
					}
				}
				else if (this.TryGetSynthesizedRecordConstructor(<recordDeclaration>5__3) != null)
				{
					if (<recordDeclaration>5__3.IsKind(SyntaxKind.RecordDeclaration))
					{
						SymbolKind symbolKind = declaredSymbol.Kind;
						if (symbolKind == SymbolKind.Method)
						{
							return delegate(SyntaxNode node)
							{
								if (node.Parent == <recordDeclaration>5__3)
								{
									return node == <recordDeclaration>5__3.ParameterList || node == <recordDeclaration>5__3.BaseList;
								}
								if (node.Parent is BaseListSyntax)
								{
									return node == <recordDeclaration>5__3.PrimaryConstructorBaseTypeIfClass;
								}
								PrimaryConstructorBaseTypeSyntax primaryConstructorBaseTypeSyntax = node.Parent as PrimaryConstructorBaseTypeSyntax;
								return primaryConstructorBaseTypeSyntax == null || primaryConstructorBaseTypeSyntax != <recordDeclaration>5__3.PrimaryConstructorBaseTypeIfClass || node == primaryConstructorBaseTypeSyntax.ArgumentList;
							};
						}
						if (symbolKind == SymbolKind.NamedType)
						{
							return delegate(SyntaxNode node)
							{
								if (node == <recordDeclaration>5__3.ParameterList)
								{
									return false;
								}
								if (node.Kind() == SyntaxKind.ArgumentList)
								{
									PrimaryConstructorBaseTypeSyntax primaryConstructorBaseTypeIfClass = <recordDeclaration>5__3.PrimaryConstructorBaseTypeIfClass;
									return node != ((primaryConstructorBaseTypeIfClass != null) ? primaryConstructorBaseTypeIfClass.ArgumentList : null);
								}
								return true;
							};
						}
						ExceptionUtilities.UnexpectedValue(declaredSymbol.Kind);
					}
					else
					{
						SymbolKind symbolKind = declaredSymbol.Kind;
						if (symbolKind == SymbolKind.Method)
						{
							return (SyntaxNode node) => node.Parent != <recordDeclaration>5__3 || node == <recordDeclaration>5__3.ParameterList;
						}
						if (symbolKind == SymbolKind.NamedType)
						{
							return (SyntaxNode node) => node != <recordDeclaration>5__3.ParameterList;
						}
						ExceptionUtilities.UnexpectedValue(declaredSymbol.Kind);
					}
				}
			}
			else if (SimpleProgramNamedTypeSymbol.GetSimpleProgramEntryPoint(this.Compilation, <unit>5__2, false) != null)
			{
				switch (declaredSymbol.Kind)
				{
				case SymbolKind.Method:
					return (SyntaxNode node) => node.Parent != <unit>5__2 || node.Kind() == SyntaxKind.GlobalStatement;
				case SymbolKind.NamedType:
					return (SyntaxNode node) => false;
				case SymbolKind.Namespace:
					return (SyntaxNode node) => node.Kind() != SyntaxKind.GlobalStatement || node.Parent != <unit>5__2;
				}
				ExceptionUtilities.UnexpectedValue(declaredSymbol.Kind);
			}
			return null;
		}

		// Token: 0x060016CD RID: 5837 RVA: 0x0007AF04 File Offset: 0x00079104
		private RegionAnalysisContext RegionAnalysisContext(ExpressionSyntax expression)
		{
			while (expression.Kind() == SyntaxKind.ParenthesizedExpression)
			{
				expression = ((ParenthesizedExpressionSyntax)expression).Expression;
			}
			MemberSemanticModel memberModel = this.GetMemberModel(expression);
			if (memberModel == null)
			{
				BoundBadStatement boundBadStatement = new BoundBadStatement(expression, ImmutableArray<BoundNode>.Empty, true);
				return new RegionAnalysisContext(this.Compilation, null, boundBadStatement, boundBadStatement, boundBadStatement);
			}
			Symbol symbol;
			BoundNode boundRoot = SyntaxTreeSemanticModel.GetBoundRoot(memberModel, out symbol);
			BoundNode upperBoundNode = memberModel.GetUpperBoundNode(expression, true);
			BoundNode boundNode = upperBoundNode;
			return new RegionAnalysisContext(this.Compilation, symbol, boundRoot, upperBoundNode, boundNode);
		}

		// Token: 0x060016CE RID: 5838 RVA: 0x0007AF80 File Offset: 0x00079180
		private RegionAnalysisContext RegionAnalysisContext(StatementSyntax firstStatement, StatementSyntax lastStatement)
		{
			MemberSemanticModel memberModel = this.GetMemberModel(firstStatement);
			if (memberModel == null)
			{
				BoundBadStatement boundBadStatement = new BoundBadStatement(firstStatement, ImmutableArray<BoundNode>.Empty, true);
				return new RegionAnalysisContext(this.Compilation, null, boundBadStatement, boundBadStatement, boundBadStatement);
			}
			Symbol symbol;
			BoundNode boundRoot = SyntaxTreeSemanticModel.GetBoundRoot(memberModel, out symbol);
			BoundNode upperBoundNode = memberModel.GetUpperBoundNode(firstStatement, true);
			BoundNode upperBoundNode2 = memberModel.GetUpperBoundNode(lastStatement, true);
			return new RegionAnalysisContext(this.Compilation, symbol, boundRoot, upperBoundNode, upperBoundNode2);
		}

		// Token: 0x060016D2 RID: 5842 RVA: 0x0007B065 File Offset: 0x00079265
		[CompilerGenerated]
		private Binder <CreateMemberModel>g__defaultOuter|77_0(ref SyntaxTreeSemanticModel.<>c__DisplayClass77_0 A_1)
		{
			return this._binderFactory.GetBinder(A_1.node, null, null).WithAdditionalFlags(this.IgnoresAccessibility ? BinderFlags.IgnoreAccessibility : BinderFlags.None);
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x0007B090 File Offset: 0x00079290
		[CompilerGenerated]
		private MemberSemanticModel <CreateMemberModel>g__createMethodBodySemanticModel|77_1(CSharpSyntaxNode memberDecl, SourceMemberMethodSymbol symbol, ref SyntaxTreeSemanticModel.<>c__DisplayClass77_0 A_3)
		{
			ExecutableCodeBinder executableCodeBinder = ((symbol != null) ? symbol.TryGetBodyBinder(this._binderFactory, this.IgnoresAccessibility) : null);
			if (executableCodeBinder == null)
			{
				return null;
			}
			return MethodBodySemanticModel.Create(this, symbol, new MethodBodySemanticModel.InitialState(memberDecl, null, executableCodeBinder, null, null));
		}

		// Token: 0x04000649 RID: 1609
		private readonly CSharpCompilation _compilation;

		// Token: 0x0400064A RID: 1610
		private readonly SyntaxTree _syntaxTree;

		// Token: 0x0400064B RID: 1611
		private ImmutableDictionary<CSharpSyntaxNode, MemberSemanticModel> _memberModels = ImmutableDictionary<CSharpSyntaxNode, MemberSemanticModel>.Empty;

		// Token: 0x0400064C RID: 1612
		private readonly BinderFactory _binderFactory;

		// Token: 0x0400064D RID: 1613
		private Func<CSharpSyntaxNode, MemberSemanticModel> _createMemberModelFunction;

		// Token: 0x0400064E RID: 1614
		private readonly bool _ignoresAccessibility;

		// Token: 0x0400064F RID: 1615
		private ScriptLocalScopeBinder.Labels _globalStatementLabels;

		// Token: 0x04000650 RID: 1616
		private static readonly Func<CSharpSyntaxNode, bool> s_isMemberDeclarationFunction = new Func<CSharpSyntaxNode, bool>(SyntaxTreeSemanticModel.IsMemberDeclaration);
	}
}
