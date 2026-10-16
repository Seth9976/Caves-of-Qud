using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200015B RID: 347
	internal class UnassignedAddressTakenVariablesWalker : DefiniteAssignmentPass
	{
		// Token: 0x06001D7F RID: 7551 RVA: 0x0009F408 File Offset: 0x0009D608
		private UnassignedAddressTakenVariablesWalker(CSharpCompilation compilation, Symbol member, BoundNode node)
			: base(compilation, member, node, true, false, null, true, false, false)
		{
		}

		// Token: 0x06001D80 RID: 7552 RVA: 0x0009F430 File Offset: 0x0009D630
		internal static HashSet<PrefixUnaryExpressionSyntax> Analyze(CSharpCompilation compilation, Symbol member, BoundNode node)
		{
			UnassignedAddressTakenVariablesWalker unassignedAddressTakenVariablesWalker = new UnassignedAddressTakenVariablesWalker(compilation, member, node);
			HashSet<PrefixUnaryExpressionSyntax> hashSet;
			try
			{
				bool flag = false;
				hashSet = unassignedAddressTakenVariablesWalker.Analyze(ref flag);
			}
			finally
			{
				unassignedAddressTakenVariablesWalker.Free();
			}
			return hashSet;
		}

		// Token: 0x06001D81 RID: 7553 RVA: 0x0009F46C File Offset: 0x0009D66C
		private HashSet<PrefixUnaryExpressionSyntax> Analyze(ref bool badRegion)
		{
			base.Analyze(ref badRegion, null);
			return this._result;
		}

		// Token: 0x06001D82 RID: 7554 RVA: 0x0009F47C File Offset: 0x0009D67C
		protected override void ReportUnassigned(Symbol symbol, SyntaxNode node, int slot, bool skipIfUseBeforeDeclaration)
		{
			if (node.Parent.Kind() == SyntaxKind.AddressOfExpression)
			{
				this._result.Add((PrefixUnaryExpressionSyntax)node.Parent);
			}
		}

		// Token: 0x06001D83 RID: 7555 RVA: 0x0009F4A7 File Offset: 0x0009D6A7
		public override BoundNode VisitAddressOfOperator(BoundAddressOfOperator node)
		{
			this.VisitRvalue(node.Operand, false);
			return null;
		}

		// Token: 0x04000E66 RID: 3686
		private readonly HashSet<PrefixUnaryExpressionSyntax> _result = new HashSet<PrefixUnaryExpressionSyntax>();
	}
}
