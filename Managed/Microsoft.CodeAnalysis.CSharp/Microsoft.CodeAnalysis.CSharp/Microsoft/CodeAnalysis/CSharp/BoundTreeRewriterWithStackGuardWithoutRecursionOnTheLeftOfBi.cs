using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000C2 RID: 194
	internal abstract class BoundTreeRewriterWithStackGuardWithoutRecursionOnTheLeftOfBinaryOperator : BoundTreeRewriterWithStackGuard
	{
		// Token: 0x06000E74 RID: 3700 RVA: 0x0005A2D5 File Offset: 0x000584D5
		protected BoundTreeRewriterWithStackGuardWithoutRecursionOnTheLeftOfBinaryOperator()
		{
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x0005A2DD File Offset: 0x000584DD
		protected BoundTreeRewriterWithStackGuardWithoutRecursionOnTheLeftOfBinaryOperator(int recursionDepth)
			: base(recursionDepth)
		{
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x0005A2E8 File Offset: 0x000584E8
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public sealed override BoundNode VisitBinaryOperator(BoundBinaryOperator node)
		{
			BoundExpression boundExpression = node.Left;
			if (boundExpression.Kind != BoundKind.BinaryOperator)
			{
				return base.VisitBinaryOperator(node);
			}
			ArrayBuilder<BoundBinaryOperator> instance = ArrayBuilder<BoundBinaryOperator>.GetInstance();
			instance.Push(node);
			BoundBinaryOperator boundBinaryOperator = (BoundBinaryOperator)boundExpression;
			for (;;)
			{
				instance.Push(boundBinaryOperator);
				boundExpression = boundBinaryOperator.Left;
				if (boundExpression.Kind != BoundKind.BinaryOperator)
				{
					break;
				}
				boundBinaryOperator = (BoundBinaryOperator)boundExpression;
			}
			BoundExpression boundExpression2 = (BoundExpression)this.Visit(boundExpression);
			do
			{
				boundBinaryOperator = instance.Pop<BoundBinaryOperator>();
				BoundExpression boundExpression3 = (BoundExpression)this.Visit(boundBinaryOperator.Right);
				TypeSymbol typeSymbol = this.VisitType(boundBinaryOperator.Type);
				boundExpression2 = boundBinaryOperator.Update(boundBinaryOperator.OperatorKind, boundBinaryOperator.ConstantValueOpt, boundBinaryOperator.MethodOpt, boundBinaryOperator.ResultKind, boundBinaryOperator.OriginalUserDefinedOperatorsOpt, boundExpression2, boundExpression3, typeSymbol);
			}
			while (instance.Count > 0);
			instance.Free();
			return boundExpression2;
		}
	}
}
