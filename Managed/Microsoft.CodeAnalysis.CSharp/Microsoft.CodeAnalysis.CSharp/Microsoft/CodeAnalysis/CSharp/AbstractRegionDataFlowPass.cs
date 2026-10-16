using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000149 RID: 329
	internal abstract class AbstractRegionDataFlowPass : DefiniteAssignmentPass
	{
		// Token: 0x06001AB0 RID: 6832 RVA: 0x0008BAC0 File Offset: 0x00089CC0
		internal AbstractRegionDataFlowPass(CSharpCompilation compilation, Symbol member, BoundNode node, BoundNode firstInRegion, BoundNode lastInRegion, HashSet<Symbol> initiallyAssignedVariables = null, HashSet<PrefixUnaryExpressionSyntax> unassignedVariableAddressOfSyntaxes = null, bool trackUnassignments = false)
			: base(compilation, member, node, firstInRegion, lastInRegion, initiallyAssignedVariables, unassignedVariableAddressOfSyntaxes, trackUnassignments)
		{
		}

		// Token: 0x06001AB1 RID: 6833 RVA: 0x0008BAE0 File Offset: 0x00089CE0
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1 })]
		protected override ImmutableArray<AbstractFlowPass<DefiniteAssignmentPass.LocalState, DefiniteAssignmentPass.LocalFunctionState>.PendingBranch> Scan(ref bool badRegion)
		{
			this.MakeSlots(base.MethodParameters);
			if (base.MethodThisParameter != null)
			{
				this.GetOrCreateSlot(base.MethodThisParameter, 0, false, true);
			}
			return base.Scan(ref badRegion);
		}

		// Token: 0x06001AB2 RID: 6834 RVA: 0x0008BB0D File Offset: 0x00089D0D
		public override BoundNode VisitLambda(BoundLambda node)
		{
			this.MakeSlots(node.Symbol.Parameters);
			return base.VisitLambda(node);
		}

		// Token: 0x06001AB3 RID: 6835 RVA: 0x0008BB27 File Offset: 0x00089D27
		public override BoundNode VisitLocalFunctionStatement(BoundLocalFunctionStatement node)
		{
			this.MakeSlots(node.Symbol.Parameters);
			return base.VisitLocalFunctionStatement(node);
		}

		// Token: 0x06001AB4 RID: 6836 RVA: 0x0008BB44 File Offset: 0x00089D44
		private void MakeSlots(ImmutableArray<ParameterSymbol> parameters)
		{
			foreach (ParameterSymbol parameterSymbol in parameters)
			{
				this.GetOrCreateSlot(parameterSymbol, 0, false, true);
			}
		}
	}
}
