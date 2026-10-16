using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000C6 RID: 198
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class BoundTreeWalkerWithStackGuard : BoundTreeWalker
	{
		// Token: 0x060010E4 RID: 4324 RVA: 0x0005D66C File Offset: 0x0005B86C
		protected BoundTreeWalkerWithStackGuard()
		{
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x0005D674 File Offset: 0x0005B874
		protected BoundTreeWalkerWithStackGuard(int recursionDepth)
		{
			this._recursionDepth = recursionDepth;
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x060010E6 RID: 4326 RVA: 0x0005D683 File Offset: 0x0005B883
		protected int RecursionDepth
		{
			get
			{
				return this._recursionDepth;
			}
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x0005D68C File Offset: 0x0005B88C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override BoundNode Visit(BoundNode node)
		{
			BoundExpression boundExpression = node as BoundExpression;
			if (boundExpression != null)
			{
				return base.VisitExpressionWithStackGuard(ref this._recursionDepth, boundExpression);
			}
			return base.Visit(node);
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x0005D6B8 File Offset: 0x0005B8B8
		protected BoundExpression VisitExpressionWithStackGuard(BoundExpression node)
		{
			return base.VisitExpressionWithStackGuard(ref this._recursionDepth, node);
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x0005D6C7 File Offset: 0x0005B8C7
		protected sealed override BoundExpression VisitExpressionWithoutStackGuard(BoundExpression node)
		{
			return (BoundExpression)base.Visit(node);
		}

		// Token: 0x0400056B RID: 1387
		private int _recursionDepth;
	}
}
