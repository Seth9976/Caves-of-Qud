using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Web.Util
{
	// Token: 0x0200012A RID: 298
	internal static class QueryableUtility
	{
		// Token: 0x06000E21 RID: 3617 RVA: 0x00025F08 File Offset: 0x00024108
		private static MethodInfo GetQueryableMethod(Expression expression)
		{
			if (expression.NodeType == ExpressionType.Call)
			{
				MethodCallExpression methodCallExpression = (MethodCallExpression)expression;
				if (methodCallExpression.Method.IsStatic && methodCallExpression.Method.DeclaringType == typeof(Queryable))
				{
					return methodCallExpression.Method.GetGenericMethodDefinition();
				}
			}
			return null;
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x00025F5C File Offset: 0x0002415C
		public static bool IsQueryableMethod(Expression expression, string method)
		{
			return QueryableUtility._methods.Where<MethodInfo>((MethodInfo m) => m.Name == method).Contains(QueryableUtility.GetQueryableMethod(expression));
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x00025F98 File Offset: 0x00024198
		public static bool IsOrderingMethod(Expression expression)
		{
			return QueryableUtility._orderMethods.Any<string>((string method) => QueryableUtility.IsQueryableMethod(expression, method));
		}

		// Token: 0x040011C3 RID: 4547
		private static readonly string[] _orderMethods = new string[] { "OrderBy", "ThenBy", "OrderByDescending", "ThenByDescending" };

		// Token: 0x040011C4 RID: 4548
		private static readonly MethodInfo[] _methods = typeof(Queryable).GetMethods(BindingFlags.Static | BindingFlags.Public);
	}
}
