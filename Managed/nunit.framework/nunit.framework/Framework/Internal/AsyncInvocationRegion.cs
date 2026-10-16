using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NUnit.Framework.Internal
{
	// Token: 0x02000062 RID: 98
	internal abstract class AsyncInvocationRegion : IDisposable
	{
		// Token: 0x06000351 RID: 849 RVA: 0x00002050 File Offset: 0x00000250
		private AsyncInvocationRegion()
		{
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00007635 File Offset: 0x00005835
		public static AsyncInvocationRegion Create(Delegate @delegate)
		{
			return AsyncInvocationRegion.Create(@delegate.Method);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00007642 File Offset: 0x00005842
		public static AsyncInvocationRegion Create(MethodInfo method)
		{
			if (!AsyncInvocationRegion.IsAsyncOperation(method))
			{
				throw new ArgumentException("Either asynchronous support is not available or an attempt \r\nat wrapping a non-async method invocation in an async region was done");
			}
			if (method.ReturnType == typeof(void))
			{
				throw new ArgumentException("'async void' methods are not supported, please use 'async Task' instead");
			}
			return new AsyncInvocationRegion.AsyncTaskInvocationRegion();
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00007680 File Offset: 0x00005880
		public static bool IsAsyncOperation(MethodInfo method)
		{
			string fullName = method.ReturnType.FullName;
			if (fullName == null)
			{
				return false;
			}
			if (!fullName.StartsWith("System.Threading.Tasks.Task"))
			{
				return method.GetCustomAttributes(false).Any<object>((object attr) => "System.Runtime.CompilerServices.AsyncStateMachineAttribute" == attr.GetType().FullName);
			}
			return true;
		}

		// Token: 0x06000355 RID: 853 RVA: 0x000076D8 File Offset: 0x000058D8
		public static bool IsAsyncOperation(Delegate @delegate)
		{
			return AsyncInvocationRegion.IsAsyncOperation(@delegate.Method);
		}

		// Token: 0x06000356 RID: 854
		public abstract object WaitForPendingOperationsToComplete(object invocationResult);

		// Token: 0x06000357 RID: 855 RVA: 0x00003E5D File Offset: 0x0000205D
		public virtual void Dispose()
		{
		}

		// Token: 0x04000068 RID: 104
		private const string TaskTypeName = "System.Threading.Tasks.Task";

		// Token: 0x04000069 RID: 105
		private const string AsyncAttributeTypeName = "System.Runtime.CompilerServices.AsyncStateMachineAttribute";

		// Token: 0x02000171 RID: 369
		private class AsyncTaskInvocationRegion : AsyncInvocationRegion
		{
			// Token: 0x06000A8A RID: 2698 RVA: 0x00019680 File Offset: 0x00017880
			public override object WaitForPendingOperationsToComplete(object invocationResult)
			{
				try
				{
					invocationResult.GetType().GetMethod("Wait", new Type[0]).Invoke(invocationResult, null);
				}
				catch (TargetInvocationException ex)
				{
					ExceptionHelper.Rethrow(AsyncInvocationRegion.AsyncTaskInvocationRegion.GetAllExceptions(ex.InnerException)[0]);
				}
				Type[] genericArguments = invocationResult.GetType().GetGenericArguments();
				if (genericArguments != null && genericArguments.Length == 1 && genericArguments[0].Name == "VoidTaskResult")
				{
					return null;
				}
				PropertyInfo property = invocationResult.GetType().GetProperty("Result", BindingFlags.Instance | BindingFlags.Public);
				if (!(property != null))
				{
					return invocationResult;
				}
				return property.GetValue(invocationResult, null);
			}

			// Token: 0x06000A8B RID: 2699 RVA: 0x00019728 File Offset: 0x00017928
			private static IList<Exception> GetAllExceptions(Exception exception)
			{
				if ("System.AggregateException".Equals(exception.GetType().FullName))
				{
					return (IList<Exception>)exception.GetType().GetProperty("InnerExceptions").GetValue(exception, null);
				}
				return new Exception[] { exception };
			}

			// Token: 0x04000255 RID: 597
			private const string TaskWaitMethod = "Wait";

			// Token: 0x04000256 RID: 598
			private const string TaskResultProperty = "Result";

			// Token: 0x04000257 RID: 599
			private const string VoidTaskResultType = "VoidTaskResult";

			// Token: 0x04000258 RID: 600
			private const string SystemAggregateException = "System.AggregateException";

			// Token: 0x04000259 RID: 601
			private const string InnerExceptionsProperty = "InnerExceptions";

			// Token: 0x0400025A RID: 602
			private const BindingFlags TaskResultPropertyBindingFlags = BindingFlags.Instance | BindingFlags.Public;
		}
	}
}
