using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x020002D4 RID: 724
	internal class ThreadHelper
	{
		// Token: 0x06001F5D RID: 8029 RVA: 0x00073B9D File Offset: 0x00071D9D
		internal ThreadHelper(Delegate start)
		{
			this._start = start;
		}

		// Token: 0x06001F5E RID: 8030 RVA: 0x00073BAC File Offset: 0x00071DAC
		internal void SetExecutionContextHelper(ExecutionContext ec)
		{
			this._executionContext = ec;
		}

		// Token: 0x06001F5F RID: 8031 RVA: 0x00073BB8 File Offset: 0x00071DB8
		[SecurityCritical]
		private static void ThreadStart_Context(object state)
		{
			ThreadHelper threadHelper = (ThreadHelper)state;
			if (threadHelper._start is ThreadStart)
			{
				((ThreadStart)threadHelper._start)();
				return;
			}
			((ParameterizedThreadStart)threadHelper._start)(threadHelper._startArg);
		}

		// Token: 0x06001F60 RID: 8032 RVA: 0x00073C00 File Offset: 0x00071E00
		[SecurityCritical]
		internal void ThreadStart(object obj)
		{
			this._startArg = obj;
			if (this._executionContext != null)
			{
				ExecutionContext.Run(this._executionContext, ThreadHelper._ccb, this);
				return;
			}
			((ParameterizedThreadStart)this._start)(obj);
		}

		// Token: 0x06001F61 RID: 8033 RVA: 0x00073C34 File Offset: 0x00071E34
		[SecurityCritical]
		internal void ThreadStart()
		{
			if (this._executionContext != null)
			{
				ExecutionContext.Run(this._executionContext, ThreadHelper._ccb, this);
				return;
			}
			((ThreadStart)this._start)();
		}

		// Token: 0x04001B11 RID: 6929
		private Delegate _start;

		// Token: 0x04001B12 RID: 6930
		private object _startArg;

		// Token: 0x04001B13 RID: 6931
		private ExecutionContext _executionContext;

		// Token: 0x04001B14 RID: 6932
		[SecurityCritical]
		internal static ContextCallback _ccb = new ContextCallback(ThreadHelper.ThreadStart_Context);
	}
}
