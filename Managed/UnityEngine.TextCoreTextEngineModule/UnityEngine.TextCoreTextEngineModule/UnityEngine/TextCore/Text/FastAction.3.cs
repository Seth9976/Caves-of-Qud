using System;
using System.Collections.Generic;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200001B RID: 27
	public class FastAction<A, B>
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00004244 File Offset: 0x00002444
		public void Add(Action<A, B> rhs)
		{
			bool flag = this.lookup.ContainsKey(rhs);
			if (!flag)
			{
				this.lookup[rhs] = this.delegates.AddLast(rhs);
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00004280 File Offset: 0x00002480
		public void Remove(Action<A, B> rhs)
		{
			LinkedListNode<Action<A, B>> linkedListNode;
			bool flag = this.lookup.TryGetValue(rhs, out linkedListNode);
			if (flag)
			{
				this.lookup.Remove(rhs);
				this.delegates.Remove(linkedListNode);
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000042BC File Offset: 0x000024BC
		public void Call(A a, B b)
		{
			for (LinkedListNode<Action<A, B>> linkedListNode = this.delegates.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				linkedListNode.Value(a, b);
			}
		}

		// Token: 0x0400007A RID: 122
		private LinkedList<Action<A, B>> delegates = new LinkedList<Action<A, B>>();

		// Token: 0x0400007B RID: 123
		private Dictionary<Action<A, B>, LinkedListNode<Action<A, B>>> lookup = new Dictionary<Action<A, B>, LinkedListNode<Action<A, B>>>();
	}
}
