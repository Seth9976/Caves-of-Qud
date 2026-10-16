using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001AD RID: 429
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundEvaluationDecisionDagNode : BoundDecisionDagNode
	{
		// Token: 0x06001F40 RID: 8000 RVA: 0x000A2219 File Offset: 0x000A0419
		public BoundEvaluationDecisionDagNode(SyntaxNode syntax, BoundDagEvaluation evaluation, BoundDecisionDagNode next, bool hasErrors = false)
			: base(BoundKind.EvaluationDecisionDagNode, syntax, hasErrors || evaluation.HasErrors() || next.HasErrors())
		{
			this.Evaluation = evaluation;
			this.Next = next;
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06001F41 RID: 8001 RVA: 0x000A2247 File Offset: 0x000A0447
		public BoundDagEvaluation Evaluation { get; }

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06001F42 RID: 8002 RVA: 0x000A224F File Offset: 0x000A044F
		public BoundDecisionDagNode Next { get; }

		// Token: 0x06001F43 RID: 8003 RVA: 0x000A2257 File Offset: 0x000A0457
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitEvaluationDecisionDagNode(this);
		}

		// Token: 0x06001F44 RID: 8004 RVA: 0x000A2260 File Offset: 0x000A0460
		public BoundEvaluationDecisionDagNode Update(BoundDagEvaluation evaluation, BoundDecisionDagNode next)
		{
			if (evaluation != this.Evaluation || next != this.Next)
			{
				BoundEvaluationDecisionDagNode boundEvaluationDecisionDagNode = new BoundEvaluationDecisionDagNode(this.Syntax, evaluation, next, base.HasErrors);
				boundEvaluationDecisionDagNode.CopyAttributes(this);
				return boundEvaluationDecisionDagNode;
			}
			return this;
		}
	}
}
