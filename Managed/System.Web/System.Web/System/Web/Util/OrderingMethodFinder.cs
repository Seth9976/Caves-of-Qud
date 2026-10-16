using System;
using System.Linq.Expressions;

namespace System.Web.Util
{
	// Token: 0x02000127 RID: 295
	internal sealed class OrderingMethodFinder : ExpressionVisitor
	{
		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06000E15 RID: 3605 RVA: 0x000259E5 File Offset: 0x00023BE5
		// (set) Token: 0x06000E16 RID: 3606 RVA: 0x000259ED File Offset: 0x00023BED
		private bool OrderingMethodFound { get; set; }

		// Token: 0x06000E17 RID: 3607 RVA: 0x000259F6 File Offset: 0x00023BF6
		protected override Expression VisitMethodCall(MethodCallExpression node)
		{
			if (this.isTopLevelMethodCall && QueryableUtility.IsOrderingMethod(node))
			{
				this.OrderingMethodFound = true;
			}
			this.isTopLevelMethodCall = false;
			Expression expression = base.VisitMethodCall(node);
			this.isTopLevelMethodCall = true;
			return expression;
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x00025A24 File Offset: 0x00023C24
		internal static bool OrderMethodExists(Expression expression)
		{
			OrderingMethodFinder orderingMethodFinder = new OrderingMethodFinder();
			orderingMethodFinder.OrderingMethodFound = false;
			orderingMethodFinder.Visit(expression);
			return orderingMethodFinder.OrderingMethodFound;
		}

		// Token: 0x040011BC RID: 4540
		private bool isTopLevelMethodCall = true;
	}
}
