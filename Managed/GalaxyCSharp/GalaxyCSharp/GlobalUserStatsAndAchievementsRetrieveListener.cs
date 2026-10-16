using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000A2 RID: 162
	public abstract class GlobalUserStatsAndAchievementsRetrieveListener : IUserStatsAndAchievementsRetrieveListener
	{
		// Token: 0x06000823 RID: 2083 RVA: 0x00016414 File Offset: 0x00014614
		internal GlobalUserStatsAndAchievementsRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalUserStatsAndAchievementsRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x00016430 File Offset: 0x00014630
		public GlobalUserStatsAndAchievementsRetrieveListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerUserStatsAndAchievementsRetrieve.GetListenerType(), this);
			}
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x00016452 File Offset: 0x00014652
		internal static HandleRef getCPtr(GlobalUserStatsAndAchievementsRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x00016470 File Offset: 0x00014670
		~GlobalUserStatsAndAchievementsRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x000164A0 File Offset: 0x000146A0
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerUserStatsAndAchievementsRetrieve.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalUserStatsAndAchievementsRetrieveListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000C0 RID: 192
		private HandleRef swigCPtr;
	}
}
