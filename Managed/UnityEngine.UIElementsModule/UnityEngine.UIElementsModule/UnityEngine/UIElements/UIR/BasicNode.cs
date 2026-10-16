using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200064A RID: 1610
	internal class BasicNode<T> : LinkedPoolItem<BasicNode<T>>
	{
		// Token: 0x06002F2B RID: 12075 RVA: 0x000AC468 File Offset: 0x000AA668
		public void InsertFirst(ref BasicNode<T> first)
		{
			bool flag = first == null;
			if (flag)
			{
				first = this;
			}
			else
			{
				this.next = first.next;
				first.next = this;
			}
		}

		// Token: 0x0400165F RID: 5727
		public BasicNode<T> next;

		// Token: 0x04001660 RID: 5728
		public T data;
	}
}
