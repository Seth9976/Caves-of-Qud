using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000051 RID: 81
	public abstract class GalaxyTypeAwareListenerUserData : IGalaxyListener
	{
		// Token: 0x0600067F RID: 1663 RVA: 0x00007788 File Offset: 0x00005988
		internal GalaxyTypeAwareListenerUserData(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerUserData_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x000077A4 File Offset: 0x000059A4
		public GalaxyTypeAwareListenerUserData()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerUserData(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x000077C2 File Offset: 0x000059C2
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerUserData obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x000077E0 File Offset: 0x000059E0
		~GalaxyTypeAwareListenerUserData()
		{
			this.Dispose();
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x00007810 File Offset: 0x00005A10
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerUserData(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x00007898 File Offset: 0x00005A98
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerUserData_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000065 RID: 101
		private HandleRef swigCPtr;
	}
}
