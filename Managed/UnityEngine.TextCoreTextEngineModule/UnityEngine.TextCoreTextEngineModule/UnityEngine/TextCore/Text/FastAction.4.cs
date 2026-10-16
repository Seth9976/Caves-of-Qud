using System;
using System.Collections.Generic;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200001C RID: 28
	public class FastAction<A, B, C>
	{
		// Token: 0x06000051 RID: 81 RVA: 0x00004318 File Offset: 0x00002518
		public void Add(Action<A, B, C> rhs)
		{
			bool flag = this.lookup.ContainsKey(rhs);
			if (!flag)
			{
				this.lookup[rhs] = this.delegates.AddLast(rhs);
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00004354 File Offset: 0x00002554
		public void Remove(Action<A, B, C> rhs)
		{
			LinkedListNode<Action<A, B, C>> linkedListNode;
			bool flag = this.lookup.TryGetValue(rhs, out linkedListNode);
			if (flag)
			{
				this.lookup.Remove(rhs);
				this.delegates.Remove(linkedListNode);
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00004390 File Offset: 0x00002590
		public void Call(A a, B b, C c)
		{
			for (LinkedListNode<Action<A, B, C>> linkedListNode = this.delegates.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				linkedListNode.Value(a, b, c);
			}
		}

		// Token: 0x0400007C RID: 124
		private LinkedList<Action<A, B, C>> delegates = new LinkedList<Action<A, B, C>>();

		// Token: 0x0400007D RID: 125
		private Dictionary<Action<A, B, C>, LinkedListNode<Action<A, B, C>>> lookup = new Dictionary<Action<A, B, C>, LinkedListNode<Action<A, B, C>>>();
	}
}
