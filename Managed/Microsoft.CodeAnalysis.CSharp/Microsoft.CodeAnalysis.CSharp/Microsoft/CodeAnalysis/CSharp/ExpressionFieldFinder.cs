using System;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000021 RID: 33
	internal class ExpressionFieldFinder : ExpressionVariableFinder<Symbol>
	{
		// Token: 0x060005A3 RID: 1443 RVA: 0x00036108 File Offset: 0x00034308
		internal static void FindExpressionVariables(ArrayBuilder<Symbol> builder, CSharpSyntaxNode node, SourceMemberContainerTypeSymbol containingType, DeclarationModifiers modifiers, FieldSymbol containingFieldOpt)
		{
			if (node == null)
			{
				return;
			}
			ExpressionFieldFinder expressionFieldFinder = ExpressionFieldFinder.s_poolInstance.Allocate();
			expressionFieldFinder._containingType = containingType;
			expressionFieldFinder._modifiers = modifiers;
			expressionFieldFinder._containingFieldOpt = containingFieldOpt;
			expressionFieldFinder.FindExpressionVariables(builder, node);
			expressionFieldFinder._containingType = null;
			expressionFieldFinder._modifiers = DeclarationModifiers.None;
			expressionFieldFinder._containingFieldOpt = null;
			ExpressionFieldFinder.s_poolInstance.Free(expressionFieldFinder);
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00036164 File Offset: 0x00034364
		protected override Symbol MakePatternVariable(TypeSyntax type, SingleVariableDesignationSyntax designation, SyntaxNode nodeToBind)
		{
			if (designation != null)
			{
				return GlobalExpressionVariable.Create(this._containingType, this._modifiers, type, designation.Identifier.ValueText, designation, designation.GetLocation(), this._containingFieldOpt, nodeToBind);
			}
			return null;
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x000361A4 File Offset: 0x000343A4
		protected override Symbol MakePatternVariable(RecursivePatternSyntax node, SyntaxNode nodeToBind)
		{
			return this.MakePatternVariable(node.Type, node.Designation as SingleVariableDesignationSyntax, nodeToBind);
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x000361C0 File Offset: 0x000343C0
		protected override Symbol MakeDeclarationExpressionVariable(DeclarationExpressionSyntax node, SingleVariableDesignationSyntax designation, BaseArgumentListSyntax argumentListSyntaxOpt, SyntaxNode nodeToBind)
		{
			return GlobalExpressionVariable.Create(this._containingType, this._modifiers, node.Type, designation.Identifier.ValueText, designation, designation.Identifier.GetLocation(), this._containingFieldOpt, nodeToBind);
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0003620C File Offset: 0x0003440C
		protected override Symbol MakeDeconstructionVariable(TypeSyntax closestTypeSyntax, SingleVariableDesignationSyntax designation, AssignmentExpressionSyntax deconstruction)
		{
			return GlobalExpressionVariable.Create(this._containingType, DeclarationModifiers.Private, closestTypeSyntax, designation.Identifier.ValueText, designation, designation.Location, null, deconstruction);
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00036241 File Offset: 0x00034441
		public static ObjectPool<ExpressionFieldFinder> CreatePool()
		{
			return new ObjectPool<ExpressionFieldFinder>(() => new ExpressionFieldFinder(), 10);
		}

		// Token: 0x0400005C RID: 92
		private SourceMemberContainerTypeSymbol _containingType;

		// Token: 0x0400005D RID: 93
		private DeclarationModifiers _modifiers;

		// Token: 0x0400005E RID: 94
		private FieldSymbol _containingFieldOpt;

		// Token: 0x0400005F RID: 95
		private static readonly ObjectPool<ExpressionFieldFinder> s_poolInstance = ExpressionFieldFinder.CreatePool();
	}
}
