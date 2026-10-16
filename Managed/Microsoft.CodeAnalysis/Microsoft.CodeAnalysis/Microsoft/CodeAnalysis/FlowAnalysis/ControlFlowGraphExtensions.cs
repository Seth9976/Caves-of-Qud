using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.CodeAnalysis.FlowAnalysis
{
	// Token: 0x020002DE RID: 734
	[NullableContext(1)]
	[Nullable(0)]
	public static class ControlFlowGraphExtensions
	{
		// Token: 0x06001F75 RID: 8053 RVA: 0x00060D04 File Offset: 0x0005EF04
		public static ControlFlowGraph GetLocalFunctionControlFlowGraphInScope(this ControlFlowGraph controlFlowGraph, IMethodSymbol localFunction, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (controlFlowGraph == null)
			{
				throw new ArgumentNullException("controlFlowGraph");
			}
			if (localFunction == null)
			{
				throw new ArgumentNullException("localFunction");
			}
			ControlFlowGraph controlFlowGraph2 = controlFlowGraph;
			ControlFlowGraph controlFlowGraph3;
			while (!controlFlowGraph2.TryGetLocalFunctionControlFlowGraph(localFunction, cancellationToken, out controlFlowGraph3))
			{
				if ((controlFlowGraph2 = controlFlowGraph2.Parent) == null)
				{
					throw new ArgumentOutOfRangeException("localFunction");
				}
			}
			return controlFlowGraph3;
		}

		// Token: 0x06001F76 RID: 8054 RVA: 0x00060D54 File Offset: 0x0005EF54
		public static ControlFlowGraph GetAnonymousFunctionControlFlowGraphInScope(this ControlFlowGraph controlFlowGraph, IFlowAnonymousFunctionOperation anonymousFunction, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (controlFlowGraph == null)
			{
				throw new ArgumentNullException("controlFlowGraph");
			}
			if (anonymousFunction == null)
			{
				throw new ArgumentNullException("anonymousFunction");
			}
			ControlFlowGraph controlFlowGraph2 = controlFlowGraph;
			ControlFlowGraph controlFlowGraph3;
			while (!controlFlowGraph2.TryGetAnonymousFunctionControlFlowGraph(anonymousFunction, cancellationToken, out controlFlowGraph3))
			{
				if ((controlFlowGraph2 = controlFlowGraph2.Parent) == null)
				{
					throw new ArgumentOutOfRangeException("anonymousFunction");
				}
			}
			return controlFlowGraph3;
		}
	}
}
