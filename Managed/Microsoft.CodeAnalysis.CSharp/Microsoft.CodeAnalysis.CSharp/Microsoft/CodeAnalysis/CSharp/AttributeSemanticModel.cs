using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000100 RID: 256
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class AttributeSemanticModel : MemberSemanticModel
	{
		// Token: 0x0600135B RID: 4955 RVA: 0x000691E0 File Offset: 0x000673E0
		private AttributeSemanticModel(AttributeSyntax syntax, NamedTypeSymbol attributeType, AliasSymbol aliasOpt, Binder rootBinder, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxTreeSemanticModel containingSemanticModelOpt = null, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxTreeSemanticModel parentSemanticModelOpt = null, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1, 1 })] ImmutableDictionary<Symbol, Symbol> parentRemappedSymbolsOpt = null, int speculatedPosition = 0)
			: base(syntax, attributeType, new ExecutableCodeBinder(syntax, rootBinder.ContainingMember(), rootBinder, null), containingSemanticModelOpt, parentSemanticModelOpt, null, parentRemappedSymbolsOpt, speculatedPosition)
		{
			this._aliasOpt = aliasOpt;
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x00069215 File Offset: 0x00067415
		public static AttributeSemanticModel Create(SyntaxTreeSemanticModel containingSemanticModel, AttributeSyntax syntax, NamedTypeSymbol attributeType, AliasSymbol aliasOpt, Binder rootBinder, ImmutableDictionary<Symbol, Symbol> parentRemappedSymbolsOpt)
		{
			return new AttributeSemanticModel(syntax, attributeType, aliasOpt, rootBinder, containingSemanticModel, null, parentRemappedSymbolsOpt, 0);
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x00069226 File Offset: 0x00067426
		public static AttributeSemanticModel CreateSpeculative(SyntaxTreeSemanticModel parentSemanticModel, AttributeSyntax syntax, NamedTypeSymbol attributeType, AliasSymbol aliasOpt, Binder rootBinder, ImmutableDictionary<Symbol, Symbol> parentRemappedSymbolsOpt, int position)
		{
			return new AttributeSemanticModel(syntax, attributeType, aliasOpt, rootBinder, null, parentSemanticModel, parentRemappedSymbolsOpt, position);
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x0600135E RID: 4958 RVA: 0x00069238 File Offset: 0x00067438
		private NamedTypeSymbol AttributeType
		{
			get
			{
				return (NamedTypeSymbol)base.MemberSymbol;
			}
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x00069248 File Offset: 0x00067448
		protected internal override CSharpSyntaxNode GetBindableSyntaxNode(CSharpSyntaxNode node)
		{
			SyntaxKind syntaxKind = node.Kind();
			if (syntaxKind != SyntaxKind.Attribute)
			{
				if (syntaxKind == SyntaxKind.AttributeArgument)
				{
					CSharpSyntaxNode csharpSyntaxNode = node.Parent;
					if (csharpSyntaxNode != null)
					{
						csharpSyntaxNode = csharpSyntaxNode.Parent;
						if (csharpSyntaxNode != null)
						{
							return csharpSyntaxNode;
						}
					}
				}
				return base.GetBindableSyntaxNode(node);
			}
			return node;
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x00069290 File Offset: 0x00067490
		internal override BoundNode Bind(Binder binder, CSharpSyntaxNode node, BindingDiagnosticBag diagnostics)
		{
			if (node.Kind() == SyntaxKind.Attribute)
			{
				AttributeSyntax attributeSyntax = (AttributeSyntax)node;
				return binder.BindAttribute(attributeSyntax, this.AttributeType, diagnostics);
			}
			if (SyntaxFacts.IsAttributeName(node))
			{
				return new BoundTypeExpression((NameSyntax)node, this._aliasOpt, this.AttributeType, false);
			}
			return base.Bind(binder, node, diagnostics);
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x000692EC File Offset: 0x000674EC
		protected override BoundNode RewriteNullableBoundNodesWithSnapshots(BoundNode boundRoot, Binder binder, DiagnosticBag diagnostics, bool createSnapshots, [global::System.Runtime.CompilerServices.Nullable(2)] out NullableWalker.SnapshotManager snapshotManager, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1, 1 })] ref ImmutableDictionary<Symbol, Symbol> remappedSymbols)
		{
			return NullableWalker.AnalyzeAndRewrite(this.Compilation, null, boundRoot, binder, null, diagnostics, createSnapshots, out snapshotManager, ref remappedSymbols);
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x0006930F File Offset: 0x0006750F
		protected override void AnalyzeBoundNodeNullability(BoundNode boundRoot, Binder binder, DiagnosticBag diagnostics, bool createSnapshots)
		{
			NullableWalker.AnalyzeWithoutRewrite(this.Compilation, null, boundRoot, binder, diagnostics, createSnapshots);
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x00069322 File Offset: 0x00067522
		protected override bool IsNullableAnalysisEnabled()
		{
			return AttributeSemanticModel.IsNullableAnalysisEnabledIn(this.Compilation, (AttributeSyntax)this.Root);
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x0006933A File Offset: 0x0006753A
		internal static bool IsNullableAnalysisEnabledIn(CSharpCompilation compilation, AttributeSyntax syntax)
		{
			return compilation.IsNullableAnalysisEnabledIn(syntax);
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x00069343 File Offset: 0x00067543
		internal override bool TryGetSpeculativeSemanticModelCore(SyntaxTreeSemanticModel parentModel, int position, ConstructorInitializerSyntax constructorInitializer, [global::System.Runtime.CompilerServices.Nullable(2)] out SemanticModel speculativeModel)
		{
			speculativeModel = null;
			return false;
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x0006934A File Offset: 0x0006754A
		internal override bool TryGetSpeculativeSemanticModelCore(SyntaxTreeSemanticModel parentModel, int position, PrimaryConstructorBaseTypeSyntax constructorInitializer, [global::System.Runtime.CompilerServices.Nullable(2)] out SemanticModel speculativeModel)
		{
			speculativeModel = null;
			return false;
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x00069351 File Offset: 0x00067551
		internal override bool TryGetSpeculativeSemanticModelCore(SyntaxTreeSemanticModel parentModel, int position, EqualsValueClauseSyntax initializer, [global::System.Runtime.CompilerServices.Nullable(2)] out SemanticModel speculativeModel)
		{
			speculativeModel = null;
			return false;
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x00069358 File Offset: 0x00067558
		internal override bool TryGetSpeculativeSemanticModelCore(SyntaxTreeSemanticModel parentModel, int position, ArrowExpressionClauseSyntax expressionBody, [global::System.Runtime.CompilerServices.Nullable(2)] out SemanticModel speculativeModel)
		{
			speculativeModel = null;
			return false;
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x0006935F File Offset: 0x0006755F
		internal override bool TryGetSpeculativeSemanticModelCore(SyntaxTreeSemanticModel parentModel, int position, StatementSyntax statement, [global::System.Runtime.CompilerServices.Nullable(2)] out SemanticModel speculativeModel)
		{
			speculativeModel = null;
			return false;
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x00069366 File Offset: 0x00067566
		internal override bool TryGetSpeculativeSemanticModelForMethodBodyCore(SyntaxTreeSemanticModel parentModel, int position, BaseMethodDeclarationSyntax method, [global::System.Runtime.CompilerServices.Nullable(2)] out SemanticModel speculativeModel)
		{
			speculativeModel = null;
			return false;
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x0006936D File Offset: 0x0006756D
		internal override bool TryGetSpeculativeSemanticModelForMethodBodyCore(SyntaxTreeSemanticModel parentModel, int position, AccessorDeclarationSyntax accessor, [global::System.Runtime.CompilerServices.Nullable(2)] out SemanticModel speculativeModel)
		{
			speculativeModel = null;
			return false;
		}

		// Token: 0x040005F4 RID: 1524
		private readonly AliasSymbol _aliasOpt;
	}
}
