using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using AiUnity.Common.InternalLog;
using AiUnity.Common.Patterns;
using AiUnity.NLog.Core.Internal;

namespace AiUnity.NLog.Core.Common
{
	// Token: 0x020002B3 RID: 691
	public static class AsyncHelpers
	{
		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06001840 RID: 6208 RVA: 0x0007E7C4 File Offset: 0x0007C9C4
		private static IInternalLogger Logger
		{
			get
			{
				return Singleton<NLogInternalLogger>.Instance;
			}
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x0007E7CC File Offset: 0x0007C9CC
		public static void ForEachItemSequentially<T>(IEnumerable<T> items, AsyncContinuation asyncContinuation, AsynchronousAction<T> action)
		{
			action = AsyncHelpers.ExceptionGuard<T>(action);
			AsyncContinuation invokeNext = null;
			IEnumerator<T> enumerator = items.GetEnumerator();
			invokeNext = delegate(Exception ex)
			{
				if (ex != null)
				{
					asyncContinuation(ex);
					return;
				}
				if (!enumerator.MoveNext())
				{
					asyncContinuation(null);
					return;
				}
				action(enumerator.Current, AsyncHelpers.PreventMultipleCalls(invokeNext));
			};
			invokeNext(null);
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x0007E830 File Offset: 0x0007CA30
		public static void Repeat(int repeatCount, AsyncContinuation asyncContinuation, AsynchronousAction action)
		{
			action = AsyncHelpers.ExceptionGuard(action);
			AsyncContinuation invokeNext = null;
			int remaining = repeatCount;
			invokeNext = delegate(Exception ex)
			{
				if (ex != null)
				{
					asyncContinuation(ex);
					return;
				}
				int remaining2 = remaining;
				remaining = remaining2 - 1;
				if (remaining2 <= 0)
				{
					asyncContinuation(null);
					return;
				}
				action(AsyncHelpers.PreventMultipleCalls(invokeNext));
			};
			invokeNext(null);
		}

		// Token: 0x06001843 RID: 6211 RVA: 0x0007E88C File Offset: 0x0007CA8C
		public static AsyncContinuation PrecededBy(AsyncContinuation asyncContinuation, AsynchronousAction action)
		{
			action = AsyncHelpers.ExceptionGuard(action);
			return delegate(Exception ex)
			{
				if (ex != null)
				{
					asyncContinuation(ex);
					return;
				}
				action(AsyncHelpers.PreventMultipleCalls(asyncContinuation));
			};
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x0007E8BD File Offset: 0x0007CABD
		public static AsyncContinuation WithTimeout(AsyncContinuation asyncContinuation, TimeSpan timeout)
		{
			return new AsyncContinuation(new TimeoutContinuation(asyncContinuation, timeout).Function);
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x0007E8D4 File Offset: 0x0007CAD4
		public static void ForEachItemInParallel<T>(IEnumerable<T> values, AsyncContinuation asyncContinuation, AsynchronousAction<T> action)
		{
			action = AsyncHelpers.ExceptionGuard<T>(action);
			List<T> list = new List<T>(values);
			int remaining = list.Count;
			List<Exception> exceptions = new List<Exception>();
			AsyncHelpers.Logger.Trace("ForEachItemInParallel() {0} items", new object[] { list.Count });
			if (remaining == 0)
			{
				asyncContinuation(null);
				return;
			}
			AsyncContinuation continuation = delegate(Exception ex)
			{
				AsyncHelpers.Logger.Trace("Continuation invoked: {0}", new object[] { ex });
				if (ex != null)
				{
					List<Exception> exceptions2 = exceptions;
					lock (exceptions2)
					{
						exceptions.Add(ex);
					}
				}
				int num = Interlocked.Decrement(ref remaining);
				AsyncHelpers.Logger.Trace("Parallel task completed. {0} items remaining", new object[] { num });
				if (num == 0)
				{
					asyncContinuation(AsyncHelpers.GetCombinedException(exceptions));
				}
			};
			foreach (T t in list)
			{
				T itemCopy = t;
				ThreadPool.QueueUserWorkItem(delegate(object s)
				{
					action(itemCopy, AsyncHelpers.PreventMultipleCalls(continuation));
				});
			}
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x0007E9CC File Offset: 0x0007CBCC
		public static void RunSynchronously(AsynchronousAction action)
		{
			ManualResetEvent ev = new ManualResetEvent(false);
			Exception lastException = null;
			action(AsyncHelpers.PreventMultipleCalls(delegate(Exception ex)
			{
				lastException = ex;
				ev.Set();
			}));
			ev.WaitOne();
			if (lastException != null)
			{
				throw new NLogRuntimeException("Asynchronous exception has occurred.", lastException);
			}
		}

		// Token: 0x06001847 RID: 6215 RVA: 0x0007EA2E File Offset: 0x0007CC2E
		public static AsyncContinuation PreventMultipleCalls(AsyncContinuation asyncContinuation)
		{
			if (asyncContinuation.Target is SingleCallContinuation)
			{
				return asyncContinuation;
			}
			return new AsyncContinuation(new SingleCallContinuation(asyncContinuation).Function);
		}

		// Token: 0x06001848 RID: 6216 RVA: 0x0007EA50 File Offset: 0x0007CC50
		public static Exception GetCombinedException(IList<Exception> exceptions)
		{
			if (exceptions.Count == 0)
			{
				return null;
			}
			if (exceptions.Count == 1)
			{
				return exceptions[0];
			}
			StringBuilder stringBuilder = new StringBuilder();
			string text = string.Empty;
			string newLine = Environment.NewLine;
			foreach (Exception ex in exceptions)
			{
				stringBuilder.Append(text);
				stringBuilder.Append(ex.ToString());
				stringBuilder.Append(newLine);
				text = newLine;
			}
			string text2 = "Got multiple exceptions:";
			string newLine2 = Environment.NewLine;
			StringBuilder stringBuilder2 = stringBuilder;
			return new NLogRuntimeException(text2 + newLine2 + ((stringBuilder2 != null) ? stringBuilder2.ToString() : null));
		}

		// Token: 0x06001849 RID: 6217 RVA: 0x0007EB00 File Offset: 0x0007CD00
		private static AsynchronousAction ExceptionGuard(AsynchronousAction action)
		{
			return delegate(AsyncContinuation cont)
			{
				try
				{
					action(cont);
				}
				catch (Exception ex)
				{
					if (ex.MustBeRethrown())
					{
						throw;
					}
					cont(ex);
				}
			};
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x0007EB19 File Offset: 0x0007CD19
		private static AsynchronousAction<T> ExceptionGuard<T>(AsynchronousAction<T> action)
		{
			return delegate(T argument, AsyncContinuation cont)
			{
				try
				{
					action(argument, cont);
				}
				catch (Exception ex)
				{
					if (ex.MustBeRethrown())
					{
						throw;
					}
					cont(ex);
				}
			};
		}
	}
}
