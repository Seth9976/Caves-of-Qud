using System;
using System.Collections.Generic;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000019 RID: 25
	public class FastAction
	{
		// Token: 0x06000045 RID: 69 RVA: 0x000040A0 File Offset: 0x000022A0
		public void Add(Action rhs)
		{
			bool flag = this.lookup.ContainsKey(rhs);
			if (!flag)
			{
				this.lookup[rhs] = this.delegates.AddLast(rhs);
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000040DC File Offset: 0x000022DC
		public void Remove(Action rhs)
		{
			LinkedListNode<Action> linkedListNode;
			bool flag = this.lookup.TryGetValue(rhs, out linkedListNode);
			if (flag)
			{
				this.lookup.Remove(rhs);
				this.delegates.Remove(linkedListNode);
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00004118 File Offset: 0x00002318
		public void Call()
		{
			for (LinkedListNode<Action> linkedListNode = this.delegates.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				linkedListNode.Value();
			}
		}

		// Token: 0x04000076 RID: 118
		private LinkedList<Action> delegates = new LinkedList<Action>();

		// Token: 0x04000077 RID: 119
		private Dictionary<Action, LinkedListNode<Action>> lookup = new Dictionary<Action, LinkedListNode<Action>>();
	}
}
