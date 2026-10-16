using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000470 RID: 1136
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class OperationWalker : OperationVisitor
	{
		// Token: 0x06002C74 RID: 11380 RVA: 0x0007D8A8 File Offset: 0x0007BAA8
		private void VisitChildOperations(IOperation operation)
		{
			foreach (IOperation operation2 in ((Operation)operation).ChildOperations)
			{
				this.Visit(operation2);
			}
		}

		// Token: 0x06002C75 RID: 11381 RVA: 0x0007D904 File Offset: 0x0007BB04
		[NullableContext(2)]
		public override void Visit(IOperation operation)
		{
			if (operation != null)
			{
				this._recursionDepth++;
				try
				{
					StackGuard.EnsureSufficientExecutionStack(this._recursionDepth);
					operation.Accept(this);
				}
				finally
				{
					this._recursionDepth--;
				}
			}
		}

		// Token: 0x06002C76 RID: 11382 RVA: 0x0007D958 File Offset: 0x0007BB58
		public override void DefaultVisit(IOperation operation)
		{
			this.VisitChildOperations(operation);
		}

		// Token: 0x06002C77 RID: 11383 RVA: 0x0007D961 File Offset: 0x0007BB61
		internal override void VisitNoneOperation(IOperation operation)
		{
			this.VisitChildOperations(operation);
		}

		// Token: 0x0400119D RID: 4509
		private int _recursionDepth;
	}
}
