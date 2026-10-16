using System;
using System.Threading;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200013A RID: 314
	internal class CommonModuleCompilationState
	{
		// Token: 0x06000C69 RID: 3177 RVA: 0x0002625D File Offset: 0x0002445D
		internal void Freeze()
		{
			Interlocked.MemoryBarrier();
			this._frozen = true;
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000C6A RID: 3178 RVA: 0x0002626B File Offset: 0x0002446B
		internal bool Frozen
		{
			get
			{
				return this._frozen;
			}
		}

		// Token: 0x04000367 RID: 871
		private bool _frozen;
	}
}
