using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000C1 RID: 193
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class BoundTreeRewriterWithStackGuard : BoundTreeRewriter
	{
		// Token: 0x06000E6E RID: 3694 RVA: 0x0005A26D File Offset: 0x0005846D
		protected BoundTreeRewriterWithStackGuard()
		{
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x0005A275 File Offset: 0x00058475
		protected BoundTreeRewriterWithStackGuard(int recursionDepth)
		{
			this._recursionDepth = recursionDepth;
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000E70 RID: 3696 RVA: 0x0005A284 File Offset: 0x00058484
		protected int RecursionDepth
		{
			get
			{
				return this._recursionDepth;
			}
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x0005A28C File Offset: 0x0005848C
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

		// Token: 0x06000E72 RID: 3698 RVA: 0x0005A2B8 File Offset: 0x000584B8
		protected BoundExpression VisitExpressionWithStackGuard(BoundExpression node)
		{
			return base.VisitExpressionWithStackGuard(ref this._recursionDepth, node);
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x0005A2C7 File Offset: 0x000584C7
		protected sealed override BoundExpression VisitExpressionWithoutStackGuard(BoundExpression node)
		{
			return (BoundExpression)base.Visit(node);
		}

		// Token: 0x0400056A RID: 1386
		private int _recursionDepth;
	}
}
