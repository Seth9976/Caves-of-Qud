using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000471 RID: 1137
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 1, 2 })]
	public abstract class OperationWalker<[Nullable(2)] TArgument> : OperationVisitor<TArgument, object>
	{
		// Token: 0x06002C79 RID: 11385 RVA: 0x0007D974 File Offset: 0x0007BB74
		private void VisitChildrenOperations(IOperation operation, TArgument argument)
		{
			foreach (IOperation operation2 in ((Operation)operation).ChildOperations)
			{
				this.Visit(operation2, argument);
			}
		}

		// Token: 0x06002C7A RID: 11386 RVA: 0x0007D9D4 File Offset: 0x0007BBD4
		[NullableContext(2)]
		public override object Visit(IOperation operation, [Nullable(1)] TArgument argument)
		{
			if (operation != null)
			{
				this._recursionDepth++;
				try
				{
					StackGuard.EnsureSufficientExecutionStack(this._recursionDepth);
					operation.Accept<TArgument, object>(this, argument);
				}
				finally
				{
					this._recursionDepth--;
				}
			}
			return null;
		}

		// Token: 0x06002C7B RID: 11387 RVA: 0x0007DA28 File Offset: 0x0007BC28
		[return: Nullable(2)]
		public override object DefaultVisit(IOperation operation, TArgument argument)
		{
			this.VisitChildrenOperations(operation, argument);
			return null;
		}

		// Token: 0x06002C7C RID: 11388 RVA: 0x0007DA33 File Offset: 0x0007BC33
		[return: Nullable(2)]
		internal override object VisitNoneOperation(IOperation operation, TArgument argument)
		{
			this.VisitChildrenOperations(operation, argument);
			return null;
		}

		// Token: 0x0400119E RID: 4510
		private int _recursionDepth;
	}
}
