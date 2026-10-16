using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200004F RID: 79
	public abstract class GalaxyTypeAwareListenerStatsAndAchievementsStore : IGalaxyListener
	{
		// Token: 0x06000673 RID: 1651 RVA: 0x00007520 File Offset: 0x00005720
		internal GalaxyTypeAwareListenerStatsAndAchievementsStore(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerStatsAndAchievementsStore_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0000753C File Offset: 0x0000573C
		public GalaxyTypeAwareListenerStatsAndAchievementsStore()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerStatsAndAchievementsStore(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0000755A File Offset: 0x0000575A
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerStatsAndAchievementsStore obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x00007578 File Offset: 0x00005778
		~GalaxyTypeAwareListenerStatsAndAchievementsStore()
		{
			this.Dispose();
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x000075A8 File Offset: 0x000057A8
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerStatsAndAchievementsStore(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x00007630 File Offset: 0x00005830
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerStatsAndAchievementsStore_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000063 RID: 99
		private HandleRef swigCPtr;
	}
}
