using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000264 RID: 612
	[UsedByNativeCode]
	[AttributeUsage(AttributeTargets.Class)]
	public class DefaultExecutionOrder : Attribute
	{
		// Token: 0x06001F6C RID: 8044 RVA: 0x0003C515 File Offset: 0x0003A715
		public DefaultExecutionOrder(int order)
		{
			this.m_Order = order;
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06001F6D RID: 8045 RVA: 0x0003C528 File Offset: 0x0003A728
		public int order
		{
			get
			{
				return this.m_Order;
			}
		}

		// Token: 0x04000945 RID: 2373
		private int m_Order;
	}
}
