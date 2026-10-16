using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000154 RID: 340
	internal class EntryPointsWalker : AbstractRegionControlFlowPass
	{
		// Token: 0x06001BA1 RID: 7073 RVA: 0x00090F74 File Offset: 0x0008F174
		internal static IEnumerable<LabeledStatementSyntax> Analyze(CSharpCompilation compilation, Symbol member, BoundNode node, BoundNode firstInRegion, BoundNode lastInRegion, out bool? succeeded)
		{
			EntryPointsWalker entryPointsWalker = new EntryPointsWalker(compilation, member, node, firstInRegion, lastInRegion);
			bool flag = false;
			IEnumerable<LabeledStatementSyntax> enumerable;
			try
			{
				entryPointsWalker.Analyze(ref flag);
				HashSet<LabeledStatementSyntax> entryPoints = entryPointsWalker._entryPoints;
				succeeded = new bool?(!flag);
				IEnumerable<LabeledStatementSyntax> enumerable2;
				if (!flag)
				{
					enumerable = entryPoints;
					enumerable2 = enumerable;
				}
				else
				{
					enumerable2 = SpecializedCollections.EmptyEnumerable<LabeledStatementSyntax>();
				}
				enumerable = enumerable2;
			}
			finally
			{
				entryPointsWalker.Free();
			}
			return enumerable;
		}

		// Token: 0x06001BA2 RID: 7074 RVA: 0x00090FD8 File Offset: 0x0008F1D8
		private void Analyze(ref bool badRegion)
		{
			this.Scan(ref badRegion);
		}

		// Token: 0x06001BA3 RID: 7075 RVA: 0x00090FE2 File Offset: 0x0008F1E2
		private EntryPointsWalker(CSharpCompilation compilation, Symbol member, BoundNode node, BoundNode firstInRegion, BoundNode lastInRegion)
			: base(compilation, member, node, firstInRegion, lastInRegion)
		{
		}

		// Token: 0x06001BA4 RID: 7076 RVA: 0x00090FFC File Offset: 0x0008F1FC
		protected override void Free()
		{
			base.Free();
		}

		// Token: 0x06001BA5 RID: 7077 RVA: 0x00091004 File Offset: 0x0008F204
		protected override void NoteBranch(AbstractFlowPass<ControlFlowPass.LocalState, ControlFlowPass.LocalFunctionState>.PendingBranch pending, BoundNode gotoStmt, BoundStatement targetStmt)
		{
			if (!gotoStmt.WasCompilerGenerated && !targetStmt.WasCompilerGenerated && base.RegionContains(targetStmt.Syntax.Span) && !base.RegionContains(gotoStmt.Syntax.Span))
			{
				this._entryPoints.Add((LabeledStatementSyntax)targetStmt.Syntax);
			}
		}

		// Token: 0x04000E40 RID: 3648
		private readonly HashSet<LabeledStatementSyntax> _entryPoints = new HashSet<LabeledStatementSyntax>();
	}
}
