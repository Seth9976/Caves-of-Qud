using System;
using System.Collections.Immutable;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000110 RID: 272
	internal class SpeculativeSyntaxTreeSemanticModel : SyntaxTreeSemanticModel
	{
		// Token: 0x06001637 RID: 5687 RVA: 0x00077596 File Offset: 0x00075796
		public static SpeculativeSyntaxTreeSemanticModel Create(SyntaxTreeSemanticModel parentSemanticModel, TypeSyntax root, Binder rootBinder, int position, SpeculativeBindingOption bindingOption)
		{
			return SpeculativeSyntaxTreeSemanticModel.CreateCore(parentSemanticModel, root, rootBinder, position, bindingOption);
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x000775A3 File Offset: 0x000757A3
		public static SpeculativeSyntaxTreeSemanticModel Create(SyntaxTreeSemanticModel parentSemanticModel, CrefSyntax root, Binder rootBinder, int position)
		{
			return SpeculativeSyntaxTreeSemanticModel.CreateCore(parentSemanticModel, root, rootBinder, position, SpeculativeBindingOption.BindAsTypeOrNamespace);
		}

		// Token: 0x06001639 RID: 5689 RVA: 0x000775AF File Offset: 0x000757AF
		private static SpeculativeSyntaxTreeSemanticModel CreateCore(SyntaxTreeSemanticModel parentSemanticModel, CSharpSyntaxNode root, Binder rootBinder, int position, SpeculativeBindingOption bindingOption)
		{
			return new SpeculativeSyntaxTreeSemanticModel(parentSemanticModel, root, rootBinder, position, bindingOption);
		}

		// Token: 0x0600163A RID: 5690 RVA: 0x000775BC File Offset: 0x000757BC
		private SpeculativeSyntaxTreeSemanticModel(SyntaxTreeSemanticModel parentSemanticModel, CSharpSyntaxNode root, Binder rootBinder, int position, SpeculativeBindingOption bindingOption)
			: base(parentSemanticModel.Compilation, parentSemanticModel.SyntaxTree, root.SyntaxTree)
		{
			this._parentSemanticModel = parentSemanticModel;
			this._root = root;
			this._rootBinder = rootBinder;
			this._position = position;
			this._bindingOption = bindingOption;
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x0600163B RID: 5691 RVA: 0x000775FB File Offset: 0x000757FB
		public override bool IsSpeculativeSemanticModel
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x0600163C RID: 5692 RVA: 0x000775FE File Offset: 0x000757FE
		public override int OriginalPositionForSpeculation
		{
			get
			{
				return this._position;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x0600163D RID: 5693 RVA: 0x00077606 File Offset: 0x00075806
		public override CSharpSemanticModel ParentModel
		{
			get
			{
				return this._parentSemanticModel;
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x0600163E RID: 5694 RVA: 0x0007760E File Offset: 0x0007580E
		internal override CSharpSyntaxNode Root
		{
			get
			{
				return this._root;
			}
		}

		// Token: 0x0600163F RID: 5695 RVA: 0x00077616 File Offset: 0x00075816
		internal override BoundNode Bind(Binder binder, CSharpSyntaxNode node, BindingDiagnosticBag diagnostics)
		{
			return this._parentSemanticModel.Bind(binder, node, diagnostics);
		}

		// Token: 0x06001640 RID: 5696 RVA: 0x00077626 File Offset: 0x00075826
		internal override Binder GetEnclosingBinderInternal(int position)
		{
			return this._rootBinder;
		}

		// Token: 0x06001641 RID: 5697 RVA: 0x0007762E File Offset: 0x0007582E
		private SpeculativeBindingOption GetSpeculativeBindingOption(ExpressionSyntax node)
		{
			if (SyntaxFacts.IsInNamespaceOrTypeContext(node))
			{
				return SpeculativeBindingOption.BindAsTypeOrNamespace;
			}
			return this._bindingOption;
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x00077640 File Offset: 0x00075840
		internal override SymbolInfo GetSymbolInfoWorker(CSharpSyntaxNode node, CSharpSemanticModel.SymbolInfoOptions options, CancellationToken cancellationToken = default(CancellationToken))
		{
			CrefSyntax crefSyntax = node as CrefSyntax;
			if (crefSyntax != null)
			{
				return this._parentSemanticModel.GetSpeculativeSymbolInfo(this._position, crefSyntax, options);
			}
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)node;
			if ((options & CSharpSemanticModel.SymbolInfoOptions.PreserveAliases) != (CSharpSemanticModel.SymbolInfoOptions)0)
			{
				return new SymbolInfo(this._parentSemanticModel.GetSpeculativeAliasInfo(this._position, expressionSyntax, this.GetSpeculativeBindingOption(expressionSyntax)), ImmutableArray<ISymbol>.Empty, CandidateReason.None);
			}
			return this._parentSemanticModel.GetSpeculativeSymbolInfo(this._position, expressionSyntax, this.GetSpeculativeBindingOption(expressionSyntax));
		}

		// Token: 0x06001643 RID: 5699 RVA: 0x000776B8 File Offset: 0x000758B8
		internal override CSharpTypeInfo GetTypeInfoWorker(CSharpSyntaxNode node, CancellationToken cancellationToken = default(CancellationToken))
		{
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)node;
			return this._parentSemanticModel.GetSpeculativeTypeInfoWorker(this._position, expressionSyntax, this.GetSpeculativeBindingOption(expressionSyntax));
		}

		// Token: 0x04000643 RID: 1603
		private readonly SyntaxTreeSemanticModel _parentSemanticModel;

		// Token: 0x04000644 RID: 1604
		private readonly CSharpSyntaxNode _root;

		// Token: 0x04000645 RID: 1605
		private readonly Binder _rootBinder;

		// Token: 0x04000646 RID: 1606
		private readonly int _position;

		// Token: 0x04000647 RID: 1607
		private readonly SpeculativeBindingOption _bindingOption;
	}
}
