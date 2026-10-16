using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000081 RID: 129
	public abstract class GlobalLeaderboardScoreUpdateListener : ILeaderboardScoreUpdateListener
	{
		// Token: 0x0600077A RID: 1914 RVA: 0x000112A0 File Offset: 0x0000F4A0
		internal GlobalLeaderboardScoreUpdateListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalLeaderboardScoreUpdateListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x000112BC File Offset: 0x0000F4BC
		public GlobalLeaderboardScoreUpdateListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerLeaderboardScoreUpdate.GetListenerType(), this);
			}
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x000112DE File Offset: 0x0000F4DE
		internal static HandleRef getCPtr(GlobalLeaderboardScoreUpdateListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x000112FC File Offset: 0x0000F4FC
		~GlobalLeaderboardScoreUpdateListener()
		{
			this.Dispose();
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x0001132C File Offset: 0x0000F52C
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerLeaderboardScoreUpdate.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalLeaderboardScoreUpdateListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0400009B RID: 155
		private HandleRef swigCPtr;
	}
}
