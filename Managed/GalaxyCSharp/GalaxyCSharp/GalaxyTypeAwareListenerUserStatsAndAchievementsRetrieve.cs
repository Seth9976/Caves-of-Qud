using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000054 RID: 84
	public abstract class GalaxyTypeAwareListenerUserStatsAndAchievementsRetrieve : IGalaxyListener
	{
		// Token: 0x06000691 RID: 1681 RVA: 0x00007B24 File Offset: 0x00005D24
		internal GalaxyTypeAwareListenerUserStatsAndAchievementsRetrieve(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerUserStatsAndAchievementsRetrieve_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x00007B40 File Offset: 0x00005D40
		public GalaxyTypeAwareListenerUserStatsAndAchievementsRetrieve()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerUserStatsAndAchievementsRetrieve(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x00007B5E File Offset: 0x00005D5E
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerUserStatsAndAchievementsRetrieve obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x00007B7C File Offset: 0x00005D7C
		~GalaxyTypeAwareListenerUserStatsAndAchievementsRetrieve()
		{
			this.Dispose();
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x00007BAC File Offset: 0x00005DAC
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerUserStatsAndAchievementsRetrieve(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x00007C34 File Offset: 0x00005E34
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerUserStatsAndAchievementsRetrieve_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000068 RID: 104
		private HandleRef swigCPtr;
	}
}
