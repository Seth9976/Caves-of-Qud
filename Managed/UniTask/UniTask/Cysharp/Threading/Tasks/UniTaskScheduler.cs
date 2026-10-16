using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000057 RID: 87
	public static class UniTaskScheduler
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000231 RID: 561 RVA: 0x00009334 File Offset: 0x00007534
		// (remove) Token: 0x06000232 RID: 562 RVA: 0x00009368 File Offset: 0x00007568
		public static event Action<Exception> UnobservedTaskException;

		// Token: 0x06000233 RID: 563 RVA: 0x0000939B File Offset: 0x0000759B
		private static void InvokeUnobservedTaskException(object state)
		{
			UniTaskScheduler.UnobservedTaskException((Exception)state);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x000093B0 File Offset: 0x000075B0
		internal static void PublishUnobservedTaskException(Exception ex)
		{
			if (ex != null)
			{
				if (!UniTaskScheduler.PropagateOperationCanceledException && ex is OperationCanceledException)
				{
					return;
				}
				if (UniTaskScheduler.UnobservedTaskException != null)
				{
					if (!UniTaskScheduler.DispatchUnityMainThread || Thread.CurrentThread.ManagedThreadId == PlayerLoopHelper.MainThreadId)
					{
						UniTaskScheduler.UnobservedTaskException(ex);
						return;
					}
					PlayerLoopHelper.UnitySynchronizationContext.Post(UniTaskScheduler.handleExceptionInvoke, ex);
					return;
				}
				else
				{
					string text = null;
					if (UniTaskScheduler.UnobservedExceptionWriteLogType != LogType.Exception)
					{
						text = "UnobservedTaskException: " + ex.ToString();
					}
					switch (UniTaskScheduler.UnobservedExceptionWriteLogType)
					{
					case LogType.Error:
						Debug.LogError(text);
						return;
					case LogType.Assert:
						break;
					case LogType.Warning:
						Debug.LogWarning(text);
						return;
					case LogType.Log:
						Debug.Log(text);
						return;
					case LogType.Exception:
						Debug.LogException(ex);
						break;
					default:
						return;
					}
				}
			}
		}

		// Token: 0x040000C2 RID: 194
		public static bool PropagateOperationCanceledException = false;

		// Token: 0x040000C3 RID: 195
		public static LogType UnobservedExceptionWriteLogType = LogType.Exception;

		// Token: 0x040000C4 RID: 196
		public static bool DispatchUnityMainThread = true;

		// Token: 0x040000C5 RID: 197
		private static readonly SendOrPostCallback handleExceptionInvoke = new SendOrPostCallback(UniTaskScheduler.InvokeUnobservedTaskException);
	}
}
