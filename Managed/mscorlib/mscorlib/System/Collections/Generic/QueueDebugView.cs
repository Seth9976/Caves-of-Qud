using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x02000AB2 RID: 2738
	internal sealed class QueueDebugView<T>
	{
		// Token: 0x060061FA RID: 25082 RVA: 0x001479FE File Offset: 0x00145BFE
		public QueueDebugView(Queue<T> queue)
		{
			if (queue == null)
			{
				throw new ArgumentNullException("queue");
			}
			this._queue = queue;
		}

		// Token: 0x1700116A RID: 4458
		// (get) Token: 0x060061FB RID: 25083 RVA: 0x00147A1B File Offset: 0x00145C1B
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				return this._queue.ToArray();
			}
		}

		// Token: 0x04003A1B RID: 14875
		private readonly Queue<T> _queue;
	}
}
