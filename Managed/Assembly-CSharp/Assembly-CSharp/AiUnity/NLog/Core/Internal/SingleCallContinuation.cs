using System;
using System.Threading;
using AiUnity.Common.Patterns;
using AiUnity.NLog.Core.Common;

namespace AiUnity.NLog.Core.Internal
{
	// Token: 0x0200026E RID: 622
	internal class SingleCallContinuation
	{
		// Token: 0x0600166B RID: 5739 RVA: 0x00078B2F File Offset: 0x00076D2F
		public SingleCallContinuation(AsyncContinuation asyncContinuation)
		{
			this.asyncContinuation = asyncContinuation;
		}

		// Token: 0x0600166C RID: 5740 RVA: 0x00078B40 File Offset: 0x00076D40
		public void Function(Exception exception)
		{
			try
			{
				AsyncContinuation asyncContinuation = Interlocked.Exchange<AsyncContinuation>(ref this.asyncContinuation, null);
				if (asyncContinuation != null)
				{
					asyncContinuation(exception);
				}
			}
			catch (Exception ex)
			{
				if (ex.MustBeRethrown())
				{
					throw;
				}
				if (Singleton<NLogManager>.Instance.ThrowExceptions)
				{
					throw;
				}
				SingleCallContinuation.ReportExceptionInHandler(ex);
			}
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x00078B98 File Offset: 0x00076D98
		private static void ReportExceptionInHandler(Exception exception)
		{
			Singleton<NLogInternalLogger>.Instance.Error("Exception in asynchronous handler {0}", new object[] { exception });
		}

		// Token: 0x04000DD9 RID: 3545
		private AsyncContinuation asyncContinuation;
	}
}
