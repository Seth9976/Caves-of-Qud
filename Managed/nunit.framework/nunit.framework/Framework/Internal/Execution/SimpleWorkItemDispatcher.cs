using System;
using System.Threading;

namespace NUnit.Framework.Internal.Execution
{
	// Token: 0x020000A4 RID: 164
	public class SimpleWorkItemDispatcher : IWorkItemDispatcher
	{
		// Token: 0x06000615 RID: 1557 RVA: 0x0000E5DC File Offset: 0x0000C7DC
		public void Dispatch(WorkItem work)
		{
			if (this._topLevelWorkItem != null)
			{
				work.Execute();
				return;
			}
			this._topLevelWorkItem = work;
			this._runnerThread = new Thread(new ThreadStart(this.RunnerThreadProc));
			if (work.TargetApartment == ApartmentState.STA)
			{
				this._runnerThread.SetApartmentState(ApartmentState.STA);
			}
			this._runnerThread.Start();
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x0000E635 File Offset: 0x0000C835
		private void RunnerThreadProc()
		{
			this._topLevelWorkItem.Execute();
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0000E644 File Offset: 0x0000C844
		public void CancelRun(bool force)
		{
			object obj = this.cancelLock;
			lock (obj)
			{
				if (this._topLevelWorkItem != null)
				{
					this._topLevelWorkItem.Cancel(force);
					if (force)
					{
						this._topLevelWorkItem = null;
					}
				}
			}
		}

		// Token: 0x04000134 RID: 308
		private WorkItem _topLevelWorkItem;

		// Token: 0x04000135 RID: 309
		private Thread _runnerThread;

		// Token: 0x04000136 RID: 310
		private object cancelLock = new object();
	}
}
