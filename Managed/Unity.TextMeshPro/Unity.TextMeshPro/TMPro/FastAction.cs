using System;
using System.Collections.Generic;

namespace TMPro
{
	// Token: 0x02000004 RID: 4
	public class FastAction
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C3 File Offset: 0x000002C3
		public void Add(Action rhs)
		{
			if (this.lookup.ContainsKey(rhs))
			{
				return;
			}
			this.lookup[rhs] = this.delegates.AddLast(rhs);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020EC File Offset: 0x000002EC
		public void Remove(Action rhs)
		{
			LinkedListNode<Action> linkedListNode;
			if (this.lookup.TryGetValue(rhs, out linkedListNode))
			{
				this.lookup.Remove(rhs);
				this.delegates.Remove(linkedListNode);
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002124 File Offset: 0x00000324
		public void Call()
		{
			for (LinkedListNode<Action> linkedListNode = this.delegates.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				linkedListNode.Value();
			}
		}

		// Token: 0x04000006 RID: 6
		private LinkedList<Action> delegates = new LinkedList<Action>();

		// Token: 0x04000007 RID: 7
		private Dictionary<Action, LinkedListNode<Action>> lookup = new Dictionary<Action, LinkedListNode<Action>>();
	}
}
