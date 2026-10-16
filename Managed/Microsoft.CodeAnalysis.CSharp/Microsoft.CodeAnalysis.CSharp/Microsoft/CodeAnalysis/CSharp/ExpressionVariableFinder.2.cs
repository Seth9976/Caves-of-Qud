using System;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000020 RID: 32
	internal class ExpressionVariableFinder : ExpressionVariableFinder<LocalSymbol>
	{
		// Token: 0x06000599 RID: 1433 RVA: 0x00035ED4 File Offset: 0x000340D4
		internal static void FindExpressionVariables(Binder scopeBinder, ArrayBuilder<LocalSymbol> builder, CSharpSyntaxNode node, Binder enclosingBinderOpt = null)
		{
			if (node == null)
			{
				return;
			}
			ExpressionVariableFinder expressionVariableFinder = ExpressionVariableFinder.s_poolInstance.Allocate();
			expressionVariableFinder._scopeBinder = scopeBinder;
			expressionVariableFinder._enclosingBinder = enclosingBinderOpt ?? scopeBinder;
			expressionVariableFinder.FindExpressionVariables(builder, node);
			expressionVariableFinder._scopeBinder = null;
			expressionVariableFinder._enclosingBinder = null;
			ExpressionVariableFinder.s_poolInstance.Free(expressionVariableFinder);
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00035F24 File Offset: 0x00034124
		internal static void FindExpressionVariables(Binder binder, ArrayBuilder<LocalSymbol> builder, SeparatedSyntaxList<ExpressionSyntax> nodes)
		{
			if (nodes.Count == 0)
			{
				return;
			}
			ExpressionVariableFinder expressionVariableFinder = ExpressionVariableFinder.s_poolInstance.Allocate();
			expressionVariableFinder._scopeBinder = binder;
			expressionVariableFinder._enclosingBinder = binder;
			expressionVariableFinder.FindExpressionVariables(builder, nodes);
			expressionVariableFinder._scopeBinder = null;
			expressionVariableFinder._enclosingBinder = null;
			ExpressionVariableFinder.s_poolInstance.Free(expressionVariableFinder);
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00035F75 File Offset: 0x00034175
		protected override LocalSymbol MakePatternVariable(TypeSyntax type, SingleVariableDesignationSyntax designation, SyntaxNode nodeToBind)
		{
			return this.MakePatternVariable(type, designation, nodeToBind);
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00035F80 File Offset: 0x00034180
		protected override LocalSymbol MakePatternVariable(RecursivePatternSyntax node, SyntaxNode nodeToBind)
		{
			return this.MakePatternVariable(node.Type, node.Designation, nodeToBind);
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x00035F98 File Offset: 0x00034198
		private LocalSymbol MakePatternVariable(TypeSyntax type, VariableDesignationSyntax variableDesignation, SyntaxNode nodeToBind)
		{
			SingleVariableDesignationSyntax singleVariableDesignationSyntax = variableDesignation as SingleVariableDesignationSyntax;
			if (singleVariableDesignationSyntax == null)
			{
				return null;
			}
			NamedTypeSymbol containingType = this._scopeBinder.ContainingType;
			if (containingType != null && containingType.IsScriptClass && this._scopeBinder.LookupDeclaredField(singleVariableDesignationSyntax) != null)
			{
				return null;
			}
			return SourceLocalSymbol.MakeLocalSymbolWithEnclosingContext(this._scopeBinder.ContainingMemberOrLambda, this._scopeBinder, this._enclosingBinder, type, singleVariableDesignationSyntax.Identifier, LocalDeclarationKind.PatternVariable, nodeToBind, null);
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x00036000 File Offset: 0x00034200
		protected override LocalSymbol MakeDeclarationExpressionVariable(DeclarationExpressionSyntax node, SingleVariableDesignationSyntax designation, BaseArgumentListSyntax argumentListSyntaxOpt, SyntaxNode nodeToBind)
		{
			NamedTypeSymbol containingType = this._scopeBinder.ContainingType;
			if (containingType != null && containingType.IsScriptClass && this._scopeBinder.LookupDeclaredField(designation) != null)
			{
				return null;
			}
			return SourceLocalSymbol.MakeLocalSymbolWithEnclosingContext(this._scopeBinder.ContainingMemberOrLambda, this._scopeBinder, this._enclosingBinder, node.Type, designation.Identifier, node.IsOutVarDeclaration() ? LocalDeclarationKind.OutVariable : LocalDeclarationKind.DeclarationExpressionVariable, nodeToBind, argumentListSyntaxOpt);
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x00036070 File Offset: 0x00034270
		protected override LocalSymbol MakeDeconstructionVariable(TypeSyntax closestTypeSyntax, SingleVariableDesignationSyntax designation, AssignmentExpressionSyntax deconstruction)
		{
			NamedTypeSymbol containingType = this._scopeBinder.ContainingType;
			if (containingType != null && containingType.IsScriptClass && this._scopeBinder.LookupDeclaredField(designation) != null)
			{
				return null;
			}
			return SourceLocalSymbol.MakeDeconstructionLocal(this._scopeBinder.ContainingMemberOrLambda, this._scopeBinder, this._enclosingBinder, closestTypeSyntax, designation.Identifier, LocalDeclarationKind.DeconstructionVariable, deconstruction);
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x000360C9 File Offset: 0x000342C9
		public static ObjectPool<ExpressionVariableFinder> CreatePool()
		{
			return new ObjectPool<ExpressionVariableFinder>(() => new ExpressionVariableFinder(), 10);
		}

		// Token: 0x04000059 RID: 89
		private Binder _scopeBinder;

		// Token: 0x0400005A RID: 90
		private Binder _enclosingBinder;

		// Token: 0x0400005B RID: 91
		private static readonly ObjectPool<ExpressionVariableFinder> s_poolInstance = ExpressionVariableFinder.CreatePool();
	}
}
