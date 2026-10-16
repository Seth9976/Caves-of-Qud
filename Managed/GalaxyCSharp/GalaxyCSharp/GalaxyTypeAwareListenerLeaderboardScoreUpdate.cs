using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000033 RID: 51
	public abstract class GalaxyTypeAwareListenerLeaderboardScoreUpdate : IGalaxyListener
	{
		// Token: 0x060005CB RID: 1483 RVA: 0x00005370 File Offset: 0x00003570
		internal GalaxyTypeAwareListenerLeaderboardScoreUpdate(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLeaderboardScoreUpdate_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0000538C File Offset: 0x0000358C
		public GalaxyTypeAwareListenerLeaderboardScoreUpdate()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerLeaderboardScoreUpdate(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x000053AA File Offset: 0x000035AA
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerLeaderboardScoreUpdate obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x000053C8 File Offset: 0x000035C8
		~GalaxyTypeAwareListenerLeaderboardScoreUpdate()
		{
			this.Dispose();
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x000053F8 File Offset: 0x000035F8
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerLeaderboardScoreUpdate(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00005480 File Offset: 0x00003680
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLeaderboardScoreUpdate_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000047 RID: 71
		private HandleRef swigCPtr;
	}
}
