using System;

namespace NUnit.Framework.Internal.Execution
{
	// Token: 0x020000A2 RID: 162
	public interface IWorkItemDispatcher
	{
		// Token: 0x06000611 RID: 1553
		void Dispatch(WorkItem work);

		// Token: 0x06000612 RID: 1554
		void CancelRun(bool force);
	}
}
