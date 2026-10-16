using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x02000AB5 RID: 2741
	internal sealed class StackDebugView<T>
	{
		// Token: 0x06006219 RID: 25113 RVA: 0x0014806B File Offset: 0x0014626B
		public StackDebugView(Stack<T> stack)
		{
			if (stack == null)
			{
				throw new ArgumentNullException("stack");
			}
			this._stack = stack;
		}

		// Token: 0x17001170 RID: 4464
		// (get) Token: 0x0600621A RID: 25114 RVA: 0x00148088 File Offset: 0x00146288
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				return this._stack.ToArray();
			}
		}

		// Token: 0x04003A25 RID: 14885
		private readonly Stack<T> _stack;
	}
}
