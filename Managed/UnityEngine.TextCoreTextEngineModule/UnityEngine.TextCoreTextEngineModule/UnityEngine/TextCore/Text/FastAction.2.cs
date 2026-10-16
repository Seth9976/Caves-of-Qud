using System;
using System.Collections.Generic;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200001A RID: 26
	public class FastAction<A>
	{
		// Token: 0x06000049 RID: 73 RVA: 0x00004170 File Offset: 0x00002370
		public void Add(Action<A> rhs)
		{
			bool flag = this.lookup.ContainsKey(rhs);
			if (!flag)
			{
				this.lookup[rhs] = this.delegates.AddLast(rhs);
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000041AC File Offset: 0x000023AC
		public void Remove(Action<A> rhs)
		{
			LinkedListNode<Action<A>> linkedListNode;
			bool flag = this.lookup.TryGetValue(rhs, out linkedListNode);
			if (flag)
			{
				this.lookup.Remove(rhs);
				this.delegates.Remove(linkedListNode);
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000041E8 File Offset: 0x000023E8
		public void Call(A a)
		{
			for (LinkedListNode<Action<A>> linkedListNode = this.delegates.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				linkedListNode.Value(a);
			}
		}

		// Token: 0x04000078 RID: 120
		private LinkedList<Action<A>> delegates = new LinkedList<Action<A>>();

		// Token: 0x04000079 RID: 121
		private Dictionary<Action<A>, LinkedListNode<Action<A>>> lookup = new Dictionary<Action<A>, LinkedListNode<Action<A>>>();
	}
}
