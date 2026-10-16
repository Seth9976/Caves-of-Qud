using System;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x0200058F RID: 1423
	public abstract class ScriptableRuntimeReflectionSystem : IScriptableRuntimeReflectionSystem, IDisposable
	{
		// Token: 0x060036F8 RID: 14072 RVA: 0x0006C7BC File Offset: 0x0006A9BC
		public virtual bool TickRealtimeProbes()
		{
			return false;
		}

		// Token: 0x060036F9 RID: 14073 RVA: 0x00002925 File Offset: 0x00000B25
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x060036FA RID: 14074 RVA: 0x0006C7CF File Offset: 0x0006A9CF
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
