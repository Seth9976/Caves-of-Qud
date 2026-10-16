using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000030 RID: 48
	public abstract class GalaxyTypeAwareListenerGogServicesConnectionState : IGalaxyListener
	{
		// Token: 0x060005B9 RID: 1465 RVA: 0x00004FD4 File Offset: 0x000031D4
		internal GalaxyTypeAwareListenerGogServicesConnectionState(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerGogServicesConnectionState_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00004FF0 File Offset: 0x000031F0
		public GalaxyTypeAwareListenerGogServicesConnectionState()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerGogServicesConnectionState(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x0000500E File Offset: 0x0000320E
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerGogServicesConnectionState obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0000502C File Offset: 0x0000322C
		~GalaxyTypeAwareListenerGogServicesConnectionState()
		{
			this.Dispose();
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x0000505C File Offset: 0x0000325C
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerGogServicesConnectionState(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x000050E4 File Offset: 0x000032E4
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerGogServicesConnectionState_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000044 RID: 68
		private HandleRef swigCPtr;
	}
}
