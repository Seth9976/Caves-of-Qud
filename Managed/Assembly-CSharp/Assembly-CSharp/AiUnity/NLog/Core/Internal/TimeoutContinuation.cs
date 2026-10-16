using System;
using System.Threading;
using AiUnity.Common.Patterns;
using AiUnity.NLog.Core.Common;

namespace AiUnity.NLog.Core.Internal
{
	// Token: 0x02000271 RID: 625
	internal class TimeoutContinuation : IDisposable
	{
		// Token: 0x06001678 RID: 5752 RVA: 0x00078CD4 File Offset: 0x00076ED4
		public TimeoutContinuation(AsyncContinuation asyncContinuation, TimeSpan timeout)
		{
			this.asyncContinuation = asyncContinuation;
			this.timeoutTimer = new Timer(new TimerCallback(this.TimerElapsed), null, timeout, TimeSpan.FromMilliseconds(-1.0));
		}

		// Token: 0x06001679 RID: 5753 RVA: 0x00078D0C File Offset: 0x00076F0C
		public void Function(Exception exception)
		{
			try
			{
				this.StopTimer();
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
				TimeoutContinuation.ReportExceptionInHandler(ex);
			}
		}

		// Token: 0x0600167A RID: 5754 RVA: 0x00078D5C File Offset: 0x00076F5C
		public void Dispose()
		{
			this.StopTimer();
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600167B RID: 5755 RVA: 0x00078D6A File Offset: 0x00076F6A
		private static void ReportExceptionInHandler(Exception exception)
		{
			Singleton<NLogInternalLogger>.Instance.Error("Exception in asynchronous handler {0}", new object[] { exception });
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x00078D88 File Offset: 0x00076F88
		private void StopTimer()
		{
			lock (this)
			{
				if (this.timeoutTimer != null)
				{
					this.timeoutTimer.Dispose();
					this.timeoutTimer = null;
				}
			}
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x00078DD8 File Offset: 0x00076FD8
		private void TimerElapsed(object state)
		{
			this.Function(new TimeoutException("Timeout."));
		}

		// Token: 0x04000DDE RID: 3550
		private AsyncContinuation asyncContinuation;

		// Token: 0x04000DDF RID: 3551
		private Timer timeoutTimer;
	}
}
