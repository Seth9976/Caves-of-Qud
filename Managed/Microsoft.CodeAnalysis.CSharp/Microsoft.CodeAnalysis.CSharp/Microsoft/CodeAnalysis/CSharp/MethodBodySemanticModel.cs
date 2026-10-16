using System;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200010E RID: 270
	internal sealed class MethodBodySemanticModel : MemberSemanticModel
	{
		// Token: 0x0600161F RID: 5663 RVA: 0x00077050 File Offset: 0x00075250
		private MethodBodySemanticModel(MethodSymbol owner, Binder rootBinder, CSharpSyntaxNode syntax, SyntaxTreeSemanticModel containingSemanticModelOpt = null, SyntaxTreeSemanticModel parentSemanticModelOpt = null, NullableWalker.SnapshotManager snapshotManagerOpt = null, ImmutableDictionary<Symbol, Symbol> parentRemappedSymbolsOpt = null, int speculatedPosition = 0)
			: base(syntax, owner, rootBinder, containingSemanticModelOpt, parentSemanticModelOpt, snapshotManagerOpt, parentRemappedSymbolsOpt, speculatedPosition)
		{
		}

		// Token: 0x06001620 RID: 5664 RVA: 0x00077070 File Offset: 0x00075270
		internal static MethodBodySemanticModel Create(SyntaxTreeSemanticModel containingSemanticModel, MethodSymbol owner, MethodBodySemanticModel.InitialState initialState)
		{
			MethodBodySemanticModel methodBodySemanticModel = new MethodBodySemanticModel(owner, initialState.Binder, initialState.Syntax, containingSemanticModel, null, null, null, 0);
			if (initialState.Body != null)
			{
				methodBodySemanticModel.UnguardedAddBoundTreeForStandaloneSyntax(initialState.Syntax, initialState.Body, initialState.SnapshotManager, initialState.RemappedSymbols);
			}
			return methodBodySemanticModel;
		}

		// Token: 0x06001621 RID: 5665 RVA: 0x000770BC File Offset: 0x000752BC
		internal override BoundNode Bind(Binder binder, CSharpSyntaxNode node, BindingDiagnosticBag diagnostics)
		{
			SyntaxKind syntaxKind = node.Kind();
			if (syntaxKind <= SyntaxKind.RemoveAccessorDeclaration)
			{
				if (syntaxKind != SyntaxKind.CompilationUnit && syntaxKind - SyntaxKind.MethodDeclaration > 3)
				{
					switch (syntaxKind)
					{
					case SyntaxKind.BaseConstructorInitializer:
					case SyntaxKind.ThisConstructorInitializer:
						return binder.BindConstructorInitializer((ConstructorInitializerSyntax)node, diagnostics);
					case SyntaxKind.DestructorDeclaration:
					case SyntaxKind.GetAccessorDeclaration:
					case SyntaxKind.SetAccessorDeclaration:
					case SyntaxKind.AddAccessorDeclaration:
					case SyntaxKind.RemoveAccessorDeclaration:
						break;
					case SyntaxKind.PropertyDeclaration:
					case SyntaxKind.EventDeclaration:
					case SyntaxKind.IndexerDeclaration:
					case SyntaxKind.AccessorList:
						goto IL_00C0;
					default:
						goto IL_00C0;
					}
				}
			}
			else if (syntaxKind <= SyntaxKind.InitAccessorDeclaration)
			{
				if (syntaxKind == SyntaxKind.ArrowExpressionClause)
				{
					return binder.BindExpressionBodyAsBlock((ArrowExpressionClauseSyntax)node, diagnostics);
				}
				if (syntaxKind != SyntaxKind.InitAccessorDeclaration)
				{
					goto IL_00C0;
				}
			}
			else if (syntaxKind != SyntaxKind.RecordDeclaration)
			{
				if (syntaxKind != SyntaxKind.PrimaryConstructorBaseType)
				{
					goto IL_00C0;
				}
				return binder.BindConstructorInitializer((PrimaryConstructorBaseTypeSyntax)node, diagnostics);
			}
			return binder.BindMethodBody(node, diagnostics);
			IL_00C0:
			return base.Bind(binder, node, diagnostics);
		}

		// Token: 0x06001622 RID: 5666 RVA: 0x00077192 File Offset: 0x00075392
		internal static MethodBodySemanticModel CreateSpeculative(SyntaxTreeSemanticModel parentSemanticModel, MethodSymbol owner, StatementSyntax syntax, Binder rootBinder, NullableWalker.SnapshotManager snapshotManagerOpt, ImmutableDictionary<Symbol, Symbol> parentRemappedSymbolsOpt, int position)
		{
			return new MethodBodySemanticModel(owner, rootBinder, syntax, null, parentSemanticModel, snapshotManagerOpt, parentRemappedSymbolsOpt, position);
		}

		// Token: 0x06001623 RID: 5667 RVA: 0x000771A4 File Offset: 0x000753A4
		internal static MethodBodySemanticModel CreateSpeculative(SyntaxTreeSemanticModel parentSemanticModel, MethodSymbol owner, ArrowExpressionClauseSyntax syntax, Binder rootBinder, int position)
		{
			return new MethodBodySemanticModel(owner, rootBinder, syntax, null, parentSemanticModel, null, null, position);
		}

		// Token: 0x06001624 RID: 5668 RVA: 0x000771B4 File Offset: 0x000753B4
		internal static MethodBodySemanticModel CreateSpeculative(SyntaxTreeSemanticModel parentSemanticModel, MethodSymbol owner, ConstructorInitializerSyntax syntax, Binder rootBinder, int position)
		{
			return new MethodBodySemanticModel(owner, rootBinder, syntax, null, parentSemanticModel, null, null, position);
		}

		// Token: 0x06001625 RID: 5669 RVA: 0x000771C4 File Offset: 0x000753C4
		internal static MethodBodySemanticModel CreateSpeculative(SyntaxTreeSemanticModel parentSemanticModel, MethodSymbol owner, PrimaryConstructorBaseTypeSyntax syntax, Binder rootBinder, int position)
		{
			return new MethodBodySemanticModel(owner, rootBinder, syntax, null, parentSemanticModel, null, null, position);
		}

		// Token: 0x06001626 RID: 5670 RVA: 0x000771D4 File Offset: 0x000753D4
		internal override bool TryGetSpeculativeSemanticModelForMethodBodyCore(SyntaxTreeSemanticModel parentModel, int position, BaseMethodDeclarationSyntax method, out SemanticModel speculativeModel)
		{
			return this.GetSpeculativeSemanticModelForMethodBody(parentModel, position, method.Body, out speculativeModel);
		}

		// Token: 0x06001627 RID: 5671 RVA: 0x000771E8 File Offset: 0x000753E8
		private bool GetSpeculativeSemanticModelForMethodBody(SyntaxTreeSemanticModel parentModel, int position, BlockSyntax body, out SemanticModel speculativeModel)
		{
			position = base.CheckAndAdjustPosition(position);
			MethodSymbol methodSymbol = (MethodSymbol)base.MemberSymbol;
			Binder binder = this.RootBinder;
			while (!(binder is ExecutableCodeBinder))
			{
				binder = binder.Next;
				if (binder == null)
				{
					IL_0037:
					Binder binder2 = new WithNullableContextBinder(this.SyntaxTree, position, binder ?? this.RootBinder);
					binder2 = new ExecutableCodeBinder(body, methodSymbol, binder2, null);
					Binder binder3 = binder2.GetBinder(body).WithAdditionalFlags(base.GetSemanticModelBinderFlags());
					speculativeModel = MethodBodySemanticModel.CreateSpeculative(parentModel, methodSymbol, body, binder3, null, null, position);
					return true;
				}
			}
			binder = binder.Next;
			goto IL_0037;
		}

		// Token: 0x06001628 RID: 5672 RVA: 0x00077271 File Offset: 0x00075471
		internal override bool TryGetSpeculativeSemanticModelForMethodBodyCore(SyntaxTreeSemanticModel parentModel, int position, AccessorDeclarationSyntax accessor, out SemanticModel speculativeModel)
		{
			return this.GetSpeculativeSemanticModelForMethodBody(parentModel, position, accessor.Body, out speculativeModel);
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x00077284 File Offset: 0x00075484
		internal override bool TryGetSpeculativeSemanticModelCore(SyntaxTreeSemanticModel parentModel, int position, StatementSyntax statement, out SemanticModel speculativeModel)
		{
			position = base.CheckAndAdjustPosition(position);
			Binder binder = base.GetEnclosingBinder(position);
			if (binder == null)
			{
				speculativeModel = null;
				return false;
			}
			MethodSymbol methodSymbol = (MethodSymbol)base.MemberSymbol;
			binder = new WithNullableContextBinder(this.SyntaxTree, position, binder);
			binder = new ExecutableCodeBinder(statement, methodSymbol, binder, null);
			speculativeModel = MethodBodySemanticModel.CreateSpeculative(parentModel, methodSymbol, statement, binder, this.GetSnapshotManager(), base.GetRemappedSymbols(), position);
			return true;
		}

		// Token: 0x0600162A RID: 5674 RVA: 0x000772EC File Offset: 0x000754EC
		internal override bool TryGetSpeculativeSemanticModelCore(SyntaxTreeSemanticModel parentModel, int position, ArrowExpressionClauseSyntax expressionBody, out SemanticModel speculativeModel)
		{
			position = base.CheckAndAdjustPosition(position);
			Binder binder = base.GetEnclosingBinder(position);
			if (binder == null)
			{
				speculativeModel = null;
				return false;
			}
			MethodSymbol methodSymbol = (MethodSymbol)base.MemberSymbol;
			binder = new WithNullableContextBinder(this.SyntaxTree, position, binder);
			binder = new ExecutableCodeBinder(expressionBody, methodSymbol, binder, null);
			speculativeModel = MethodBodySemanticModel.CreateSpeculative(parentModel, methodSymbol, expressionBody, binder, position);
			return true;
		}

		// Token: 0x0600162B RID: 5675 RVA: 0x00077348 File Offset: 0x00075548
		internal override bool TryGetSpeculativeSemanticModelCore(SyntaxTreeSemanticModel parentModel, int position, ConstructorInitializerSyntax constructorInitializer, out SemanticModel speculativeModel)
		{
			MethodSymbol methodSymbol = base.MemberSymbol as MethodSymbol;
			if (methodSymbol != null && methodSymbol.MethodKind == MethodKind.Constructor)
			{
				SyntaxNode parent = this.Root.FindToken(position, false).Parent;
				CSharpSyntaxNode csharpSyntaxNode;
				if (parent == null)
				{
					csharpSyntaxNode = null;
				}
				else
				{
					ConstructorInitializerSyntax constructorInitializerSyntax = Enumerable.FirstOrDefault<ConstructorInitializerSyntax>(Enumerable.OfType<ConstructorInitializerSyntax>(parent.AncestorsAndSelf(true)));
					csharpSyntaxNode = ((constructorInitializerSyntax != null) ? constructorInitializerSyntax.Parent : null);
				}
				if (csharpSyntaxNode == this.Root)
				{
					Binder binder = base.GetEnclosingBinder(position);
					if (binder != null)
					{
						binder = new WithNullableContextBinder(this.SyntaxTree, position, binder);
						binder = new ExecutableCodeBinder(constructorInitializer, methodSymbol, binder, null);
						speculativeModel = MethodBodySemanticModel.CreateSpeculative(parentModel, methodSymbol, constructorInitializer, binder, position);
						return true;
					}
				}
			}
			speculativeModel = null;
			return false;
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x000773E4 File Offset: 0x000755E4
		internal override bool TryGetSpeculativeSemanticModelCore(SyntaxTreeSemanticModel parentModel, int position, PrimaryConstructorBaseTypeSyntax constructorInitializer, out SemanticModel speculativeModel)
		{
			SynthesizedRecordConstructor synthesizedRecordConstructor = base.MemberSymbol as SynthesizedRecordConstructor;
			if (synthesizedRecordConstructor != null)
			{
				RecordDeclarationSyntax syntax = synthesizedRecordConstructor.GetSyntax();
				if (syntax != null)
				{
					SyntaxNode parent = this.Root.FindToken(position, false).Parent;
					if (((parent != null) ? Enumerable.FirstOrDefault<PrimaryConstructorBaseTypeSyntax>(Enumerable.OfType<PrimaryConstructorBaseTypeSyntax>(parent.AncestorsAndSelf(true))) : null) == syntax.PrimaryConstructorBaseTypeIfClass)
					{
						Binder binder = base.GetEnclosingBinder(position);
						if (binder != null)
						{
							binder = new WithNullableContextBinder(this.SyntaxTree, position, binder);
							binder = new ExecutableCodeBinder(constructorInitializer, synthesizedRecordConstructor, binder, null);
							speculativeModel = MethodBodySemanticModel.CreateSpeculative(parentModel, synthesizedRecordConstructor, constructorInitializer, binder, position);
							return true;
						}
					}
				}
			}
			speculativeModel = null;
			return false;
		}

		// Token: 0x0600162D RID: 5677 RVA: 0x00077475 File Offset: 0x00075675
		internal override bool TryGetSpeculativeSemanticModelCore(SyntaxTreeSemanticModel parentModel, int position, EqualsValueClauseSyntax initializer, out SemanticModel speculativeModel)
		{
			speculativeModel = null;
			return false;
		}

		// Token: 0x0600162E RID: 5678 RVA: 0x0007747C File Offset: 0x0007567C
		protected override BoundNode RewriteNullableBoundNodesWithSnapshots(BoundNode boundRoot, Binder binder, DiagnosticBag diagnostics, bool createSnapshots, out NullableWalker.SnapshotManager snapshotManager, ref ImmutableDictionary<Symbol, Symbol> remappedSymbols)
		{
			NullableWalker.VariableState afterInitializersState = NullableWalker.GetAfterInitializersState(this.Compilation, base.MemberSymbol);
			return NullableWalker.AnalyzeAndRewrite(this.Compilation, base.MemberSymbol, boundRoot, binder, afterInitializersState, diagnostics, createSnapshots, out snapshotManager, ref remappedSymbols);
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x000774B6 File Offset: 0x000756B6
		protected override void AnalyzeBoundNodeNullability(BoundNode boundRoot, Binder binder, DiagnosticBag diagnostics, bool createSnapshots)
		{
			NullableWalker.AnalyzeWithoutRewrite(this.Compilation, base.MemberSymbol, boundRoot, binder, diagnostics, createSnapshots);
		}

		// Token: 0x06001630 RID: 5680 RVA: 0x000774CE File Offset: 0x000756CE
		protected override bool IsNullableAnalysisEnabled()
		{
			return this.Compilation.IsNullableAnalysisEnabledIn((MethodSymbol)base.MemberSymbol);
		}

		// Token: 0x020006BA RID: 1722
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal readonly struct InitialState
		{
			// Token: 0x06008437 RID: 33847 RVA: 0x001DF39B File Offset: 0x001DD59B
			internal InitialState([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxNode syntax, BoundNode bodyOpt = null, ExecutableCodeBinder binder = null, NullableWalker.SnapshotManager snapshotManager = null, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1, 1 })] ImmutableDictionary<Symbol, Symbol> remappedSymbols = null)
			{
				this.Syntax = syntax;
				this.Body = bodyOpt;
				this.Binder = binder;
				this.SnapshotManager = snapshotManager;
				this.RemappedSymbols = remappedSymbols;
			}

			// Token: 0x04002161 RID: 8545
			[global::System.Runtime.CompilerServices.Nullable(1)]
			internal readonly CSharpSyntaxNode Syntax;

			// Token: 0x04002162 RID: 8546
			internal readonly BoundNode Body;

			// Token: 0x04002163 RID: 8547
			internal readonly ExecutableCodeBinder Binder;

			// Token: 0x04002164 RID: 8548
			internal readonly NullableWalker.SnapshotManager SnapshotManager;

			// Token: 0x04002165 RID: 8549
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1, 1 })]
			internal readonly ImmutableDictionary<Symbol, Symbol> RemappedSymbols;
		}
	}
}
