using System;
using UnityEngine;

namespace AiUnity.Common.Attributes
{
	// Token: 0x020002EB RID: 747
	public class OrderAttribute : PropertyAttribute
	{
		// Token: 0x06001986 RID: 6534 RVA: 0x00081807 File Offset: 0x0007FA07
		public OrderAttribute(int order)
		{
			base.order = order;
		}
	}
}
