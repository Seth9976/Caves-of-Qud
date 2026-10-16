using System;
using System.Linq.Expressions;
using System.Reflection;

namespace HarmonyLib
{
	// Token: 0x02000195 RID: 405
	public static class SymbolExtensions
	{
		// Token: 0x0600069D RID: 1693 RVA: 0x00016E97 File Offset: 0x00015097
		public static MethodInfo GetMethodInfo(Expression<Action> expression)
		{
			return SymbolExtensions.GetMethodInfo(expression);
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x00016E97 File Offset: 0x00015097
		public static MethodInfo GetMethodInfo<T>(Expression<Action<T>> expression)
		{
			return SymbolExtensions.GetMethodInfo(expression);
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x00016E97 File Offset: 0x00015097
		public static MethodInfo GetMethodInfo<T, TResult>(Expression<Func<T, TResult>> expression)
		{
			return SymbolExtensions.GetMethodInfo(expression);
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x00016EA0 File Offset: 0x000150A0
		public static MethodInfo GetMethodInfo(LambdaExpression expression)
		{
			MethodCallExpression methodCallExpression = expression.Body as MethodCallExpression;
			if (methodCallExpression == null)
			{
				UnaryExpression unaryExpression = expression.Body as UnaryExpression;
				if (unaryExpression != null)
				{
					MethodCallExpression methodCallExpression2 = unaryExpression.Operand as MethodCallExpression;
					if (methodCallExpression2 != null)
					{
						ConstantExpression constantExpression = methodCallExpression2.Object as ConstantExpression;
						if (constantExpression != null)
						{
							MethodInfo methodInfo = constantExpression.Value as MethodInfo;
							if (methodInfo != null)
							{
								return methodInfo;
							}
						}
					}
				}
				throw new ArgumentException("Invalid Expression. Expression should consist of a Method call only.");
			}
			MethodInfo method = methodCallExpression.Method;
			if (method == null)
			{
				throw new Exception(string.Format("Cannot find method for expression {0}", expression));
			}
			return method;
		}
	}
}
