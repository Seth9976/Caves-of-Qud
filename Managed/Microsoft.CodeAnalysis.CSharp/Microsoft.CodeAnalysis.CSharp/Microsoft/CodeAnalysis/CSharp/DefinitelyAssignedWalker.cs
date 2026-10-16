using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000152 RID: 338
	internal sealed class DefinitelyAssignedWalker : AbstractRegionDataFlowPass
	{
		// Token: 0x06001B8B RID: 7051 RVA: 0x00090AB4 File Offset: 0x0008ECB4
		private DefinitelyAssignedWalker(CSharpCompilation compilation, Symbol member, BoundNode node, BoundNode firstInRegion, BoundNode lastInRegion)
			: base(compilation, member, node, firstInRegion, lastInRegion, null, null, false)
		{
		}

		// Token: 0x06001B8C RID: 7052 RVA: 0x00090AE8 File Offset: 0x0008ECE8
		[return: TupleElementNames(new string[] { "entry", "exit" })]
		internal static ValueTuple<HashSet<Symbol>, HashSet<Symbol>> Analyze(CSharpCompilation compilation, Symbol member, BoundNode node, BoundNode firstInRegion, BoundNode lastInRegion)
		{
			DefinitelyAssignedWalker definitelyAssignedWalker = new DefinitelyAssignedWalker(compilation, member, node, firstInRegion, lastInRegion);
			ValueTuple<HashSet<Symbol>, HashSet<Symbol>> valueTuple;
			try
			{
				bool flag = false;
				definitelyAssignedWalker.Analyze(ref flag, null);
				valueTuple = (flag ? new ValueTuple<HashSet<Symbol>, HashSet<Symbol>>(new HashSet<Symbol>(), new HashSet<Symbol>()) : new ValueTuple<HashSet<Symbol>, HashSet<Symbol>>(definitelyAssignedWalker._definitelyAssignedOnEntry, definitelyAssignedWalker._definitelyAssignedOnExit));
			}
			finally
			{
				definitelyAssignedWalker.Free();
			}
			return valueTuple;
		}

		// Token: 0x06001B8D RID: 7053 RVA: 0x00090B4C File Offset: 0x0008ED4C
		protected override void EnterRegion()
		{
			this.ProcessRegion(this._definitelyAssignedOnEntry);
			base.EnterRegion();
		}

		// Token: 0x06001B8E RID: 7054 RVA: 0x00090B60 File Offset: 0x0008ED60
		protected override void LeaveRegion()
		{
			this.ProcessRegion(this._definitelyAssignedOnExit);
			base.LeaveRegion();
		}

		// Token: 0x06001B8F RID: 7055 RVA: 0x00090B74 File Offset: 0x0008ED74
		private void ProcessRegion(HashSet<Symbol> definitelyAssigned)
		{
			definitelyAssigned.Clear();
			if (this.IsConditionalState)
			{
				this.ProcessState(definitelyAssigned, this.StateWhenTrue, new DefiniteAssignmentPass.LocalState?(this.StateWhenFalse));
				return;
			}
			this.ProcessState(definitelyAssigned, this.State, default(DefiniteAssignmentPass.LocalState?));
		}

		// Token: 0x06001B90 RID: 7056 RVA: 0x00090BC0 File Offset: 0x0008EDC0
		private void ProcessState(HashSet<Symbol> definitelyAssigned, DefiniteAssignmentPass.LocalState state1, DefiniteAssignmentPass.LocalState? state2opt)
		{
			foreach (int num in state1.Assigned.TrueBits())
			{
				if (num < this.variableBySlot.Count && (state2opt == null || state2opt.GetValueOrDefault().IsAssigned(num)))
				{
					Symbol symbol = this.variableBySlot[num].Symbol;
					if (symbol != null && symbol.Kind != SymbolKind.Field)
					{
						definitelyAssigned.Add(symbol);
					}
				}
			}
		}

		// Token: 0x04000E3B RID: 3643
		private readonly HashSet<Symbol> _definitelyAssignedOnEntry = new HashSet<Symbol>();

		// Token: 0x04000E3C RID: 3644
		private readonly HashSet<Symbol> _definitelyAssignedOnExit = new HashSet<Symbol>();
	}
}
