using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000C7 RID: 199
	internal abstract class BoundTreeWalkerWithStackGuardWithoutRecursionOnTheLeftOfBinaryOperator : BoundTreeWalkerWithStackGuard
	{
		// Token: 0x060010EA RID: 4330 RVA: 0x0005D6D5 File Offset: 0x0005B8D5
		protected BoundTreeWalkerWithStackGuardWithoutRecursionOnTheLeftOfBinaryOperator()
		{
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x0005D6DD File Offset: 0x0005B8DD
		protected BoundTreeWalkerWithStackGuardWithoutRecursionOnTheLeftOfBinaryOperator(int recursionDepth)
			: base(recursionDepth)
		{
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x0005D6E8 File Offset: 0x0005B8E8
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public sealed override BoundNode VisitBinaryOperator(BoundBinaryOperator node)
		{
			if (node.Left.Kind != BoundKind.BinaryOperator)
			{
				return base.VisitBinaryOperator(node);
			}
			ArrayBuilder<BoundExpression> instance = ArrayBuilder<BoundExpression>.GetInstance();
			instance.Push(node.Right);
			BoundBinaryOperator boundBinaryOperator = (BoundBinaryOperator)node.Left;
			instance.Push(boundBinaryOperator.Right);
			BoundExpression boundExpression = boundBinaryOperator.Left;
			while (boundExpression.Kind == BoundKind.BinaryOperator)
			{
				boundBinaryOperator = (BoundBinaryOperator)boundExpression;
				instance.Push(boundBinaryOperator.Right);
				boundExpression = boundBinaryOperator.Left;
			}
			this.Visit(boundExpression);
			while (instance.Count > 0)
			{
				this.Visit(instance.Pop<BoundExpression>());
			}
			instance.Free();
			return null;
		}
	}
}
