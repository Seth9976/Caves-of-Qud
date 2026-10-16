using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000237 RID: 567
	internal class EventCallbackListPool
	{
		// Token: 0x060010D0 RID: 4304 RVA: 0x000489C0 File Offset: 0x00046BC0
		public EventCallbackList Get(EventCallbackList initializer)
		{
			bool flag = this.m_Stack.Count == 0;
			EventCallbackList eventCallbackList;
			if (flag)
			{
				bool flag2 = initializer != null;
				if (flag2)
				{
					eventCallbackList = new EventCallbackList(initializer);
				}
				else
				{
					eventCallbackList = new EventCallbackList();
				}
			}
			else
			{
				eventCallbackList = this.m_Stack.Pop();
				bool flag3 = initializer != null;
				if (flag3)
				{
					eventCallbackList.AddRange(initializer);
				}
			}
			return eventCallbackList;
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x00048A20 File Offset: 0x00046C20
		public void Release(EventCallbackList element)
		{
			element.Clear();
			this.m_Stack.Push(element);
		}

		// Token: 0x04000A0C RID: 2572
		private readonly Stack<EventCallbackList> m_Stack = new Stack<EventCallbackList>();
	}
}
