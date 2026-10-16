using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000228 RID: 552
	public class ExecuteCommandEvent : CommandEventBase<ExecuteCommandEvent>
	{
		// Token: 0x06001056 RID: 4182 RVA: 0x0004787F File Offset: 0x00045A7F
		static ExecuteCommandEvent()
		{
			EventBase<ExecuteCommandEvent>.SetCreateFunction(() => new ExecuteCommandEvent());
		}
	}
}
