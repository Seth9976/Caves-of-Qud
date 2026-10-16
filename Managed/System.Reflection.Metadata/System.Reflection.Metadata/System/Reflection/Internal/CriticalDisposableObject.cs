using System;
using System.Runtime.ConstrainedExecution;

namespace System.Reflection.Internal
{
	// Token: 0x0200016D RID: 365
	internal abstract class CriticalDisposableObject : CriticalFinalizerObject, IDisposable
	{
		// Token: 0x06000B57 RID: 2903
		protected abstract void Release();

		// Token: 0x06000B58 RID: 2904 RVA: 0x0002295A File Offset: 0x00020B5A
		public void Dispose()
		{
			this.Release();
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x00022968 File Offset: 0x00020B68
		~CriticalDisposableObject()
		{
			this.Release();
		}
	}
}
