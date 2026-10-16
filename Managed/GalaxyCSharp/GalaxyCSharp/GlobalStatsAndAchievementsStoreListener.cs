using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200009D RID: 157
	public abstract class GlobalStatsAndAchievementsStoreListener : IStatsAndAchievementsStoreListener
	{
		// Token: 0x06000809 RID: 2057 RVA: 0x00015696 File Offset: 0x00013896
		internal GlobalStatsAndAchievementsStoreListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalStatsAndAchievementsStoreListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x000156B2 File Offset: 0x000138B2
		public GlobalStatsAndAchievementsStoreListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerStatsAndAchievementsStore.GetListenerType(), this);
			}
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x000156D4 File Offset: 0x000138D4
		internal static HandleRef getCPtr(GlobalStatsAndAchievementsStoreListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x000156F4 File Offset: 0x000138F4
		~GlobalStatsAndAchievementsStoreListener()
		{
			this.Dispose();
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x00015724 File Offset: 0x00013924
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerStatsAndAchievementsStore.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalStatsAndAchievementsStoreListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000BA RID: 186
		private HandleRef swigCPtr;
	}
}
