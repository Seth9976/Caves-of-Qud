using System;
using System.Collections.Immutable;
using System.Threading;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200014B RID: 331
	internal class CSharpControlFlowAnalysis : ControlFlowAnalysis
	{
		// Token: 0x06001ABF RID: 6847 RVA: 0x0008BE2B File Offset: 0x0008A02B
		internal CSharpControlFlowAnalysis(RegionAnalysisContext context)
		{
			this._context = context;
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06001AC0 RID: 6848 RVA: 0x0008BE3C File Offset: 0x0008A03C
		public override ImmutableArray<SyntaxNode> EntryPoints
		{
			get
			{
				if (new ImmutableArray<SyntaxNode>?(this._entryPoints) == default(ImmutableArray<SyntaxNode>?))
				{
					this._succeeded = new bool?(!this._context.Failed);
					ImmutableArray<SyntaxNode> immutableArray = (this._context.Failed ? ImmutableArray<SyntaxNode>.Empty : EntryPointsWalker.Analyze(this._context.Compilation, this._context.Member, this._context.BoundNode, this._context.FirstInRegion, this._context.LastInRegion, out this._succeeded).ToImmutableArray<SyntaxNode>());
					ImmutableInterlocked.InterlockedInitialize<SyntaxNode>(ref this._entryPoints, immutableArray);
				}
				return this._entryPoints;
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06001AC1 RID: 6849 RVA: 0x0008BEF0 File Offset: 0x0008A0F0
		public override ImmutableArray<SyntaxNode> ExitPoints
		{
			get
			{
				if (new ImmutableArray<SyntaxNode>?(this._exitPoints) == default(ImmutableArray<SyntaxNode>?))
				{
					ImmutableArray<SyntaxNode> immutableArray = (this.Succeeded ? ExitPointsWalker.Analyze(this._context.Compilation, this._context.Member, this._context.BoundNode, this._context.FirstInRegion, this._context.LastInRegion).ToImmutableArray<SyntaxNode>() : ImmutableArray<SyntaxNode>.Empty);
					ImmutableInterlocked.InterlockedInitialize<SyntaxNode>(ref this._exitPoints, immutableArray);
				}
				return this._exitPoints;
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06001AC2 RID: 6850 RVA: 0x0008BF7C File Offset: 0x0008A17C
		public sealed override bool EndPointIsReachable
		{
			get
			{
				if (this._regionEndPointIsReachable == null)
				{
					this.ComputeReachability();
				}
				return (bool)this._regionEndPointIsReachable;
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06001AC3 RID: 6851 RVA: 0x0008BF97 File Offset: 0x0008A197
		public sealed override bool StartPointIsReachable
		{
			get
			{
				if (this._regionStartPointIsReachable == null)
				{
					this.ComputeReachability();
				}
				return (bool)this._regionStartPointIsReachable;
			}
		}

		// Token: 0x06001AC4 RID: 6852 RVA: 0x0008BFB4 File Offset: 0x0008A1B4
		private void ComputeReachability()
		{
			bool flag;
			bool flag2;
			if (this.Succeeded)
			{
				RegionReachableWalker.Analyze(this._context.Compilation, this._context.Member, this._context.BoundNode, this._context.FirstInRegion, this._context.LastInRegion, out flag, out flag2);
			}
			else
			{
				flag2 = (flag = true);
			}
			Interlocked.CompareExchange(ref this._regionEndPointIsReachable, flag2, null);
			Interlocked.CompareExchange(ref this._regionStartPointIsReachable, flag, null);
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06001AC5 RID: 6853 RVA: 0x0008C035 File Offset: 0x0008A235
		public override ImmutableArray<SyntaxNode> ReturnStatements
		{
			get
			{
				return this.ExitPoints.WhereAsArray<SyntaxNode>((SyntaxNode s) => s.IsKind(SyntaxKind.ReturnStatement) || s.IsKind(SyntaxKind.YieldBreakStatement));
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06001AC6 RID: 6854 RVA: 0x0008C061 File Offset: 0x0008A261
		public sealed override bool Succeeded
		{
			get
			{
				if (this._succeeded == null)
				{
					ImmutableArray<SyntaxNode> entryPoints = this.EntryPoints;
				}
				return this._succeeded.Value;
			}
		}

		// Token: 0x04000E05 RID: 3589
		private readonly RegionAnalysisContext _context;

		// Token: 0x04000E06 RID: 3590
		private ImmutableArray<SyntaxNode> _entryPoints;

		// Token: 0x04000E07 RID: 3591
		private ImmutableArray<SyntaxNode> _exitPoints;

		// Token: 0x04000E08 RID: 3592
		private object _regionStartPointIsReachable;

		// Token: 0x04000E09 RID: 3593
		private object _regionEndPointIsReachable;

		// Token: 0x04000E0A RID: 3594
		private bool? _succeeded;
	}
}
