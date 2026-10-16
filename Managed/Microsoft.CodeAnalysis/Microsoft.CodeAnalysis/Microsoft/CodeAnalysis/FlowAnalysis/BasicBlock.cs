using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.FlowAnalysis
{
	// Token: 0x020002D6 RID: 726
	[NullableContext(2)]
	[Nullable(0)]
	public sealed class BasicBlock
	{
		// Token: 0x06001E32 RID: 7730 RVA: 0x00057174 File Offset: 0x00055374
		[NullableContext(1)]
		internal BasicBlock(BasicBlockKind kind, [Nullable(new byte[] { 0, 1 })] ImmutableArray<IOperation> operations, [Nullable(2)] IOperation branchValue, ControlFlowConditionKind conditionKind, int ordinal, bool isReachable, ControlFlowRegion region)
		{
			this.Kind = kind;
			this.Operations = operations;
			this.BranchValue = branchValue;
			this.ConditionKind = conditionKind;
			this.Ordinal = ordinal;
			this.IsReachable = isReachable;
			this.EnclosingRegion = region;
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x06001E33 RID: 7731 RVA: 0x000571B1 File Offset: 0x000553B1
		public BasicBlockKind Kind { get; }

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x06001E34 RID: 7732 RVA: 0x000571B9 File Offset: 0x000553B9
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<IOperation> Operations
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x06001E35 RID: 7733 RVA: 0x000571C1 File Offset: 0x000553C1
		public IOperation BranchValue { get; }

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x06001E36 RID: 7734 RVA: 0x000571C9 File Offset: 0x000553C9
		public ControlFlowConditionKind ConditionKind { get; }

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x06001E37 RID: 7735 RVA: 0x000571D1 File Offset: 0x000553D1
		public ControlFlowBranch FallThroughSuccessor
		{
			get
			{
				return this._lazySuccessor;
			}
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x06001E38 RID: 7736 RVA: 0x000571D9 File Offset: 0x000553D9
		public ControlFlowBranch ConditionalSuccessor
		{
			get
			{
				return this._lazyConditionalSuccessor;
			}
		}

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x06001E39 RID: 7737 RVA: 0x000571E1 File Offset: 0x000553E1
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<ControlFlowBranch> Predecessors
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._lazyPredecessors;
			}
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x06001E3A RID: 7738 RVA: 0x000571E9 File Offset: 0x000553E9
		public int Ordinal { get; }

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x06001E3B RID: 7739 RVA: 0x000571F1 File Offset: 0x000553F1
		public bool IsReachable { get; }

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x06001E3C RID: 7740 RVA: 0x000571F9 File Offset: 0x000553F9
		[Nullable(1)]
		public ControlFlowRegion EnclosingRegion
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x06001E3D RID: 7741 RVA: 0x00057201 File Offset: 0x00055401
		internal void SetSuccessors(ControlFlowBranch successor, ControlFlowBranch conditionalSuccessor)
		{
			this._lazySuccessor = successor;
			this._lazyConditionalSuccessor = conditionalSuccessor;
		}

		// Token: 0x06001E3E RID: 7742 RVA: 0x00057211 File Offset: 0x00055411
		internal void SetPredecessors([Nullable(new byte[] { 0, 1 })] ImmutableArray<ControlFlowBranch> predecessors)
		{
			this._lazyPredecessors = predecessors;
		}

		// Token: 0x04000D8A RID: 3466
		private ControlFlowBranch _lazySuccessor;

		// Token: 0x04000D8B RID: 3467
		private ControlFlowBranch _lazyConditionalSuccessor;

		// Token: 0x04000D8C RID: 3468
		[Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<ControlFlowBranch> _lazyPredecessors;
	}
}
