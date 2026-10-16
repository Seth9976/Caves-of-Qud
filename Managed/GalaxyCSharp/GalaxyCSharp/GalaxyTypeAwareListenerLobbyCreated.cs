using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000035 RID: 53
	public abstract class GalaxyTypeAwareListenerLobbyCreated : IGalaxyListener
	{
		// Token: 0x060005D7 RID: 1495 RVA: 0x000055D8 File Offset: 0x000037D8
		internal GalaxyTypeAwareListenerLobbyCreated(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLobbyCreated_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x000055F4 File Offset: 0x000037F4
		public GalaxyTypeAwareListenerLobbyCreated()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerLobbyCreated(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x00005612 File Offset: 0x00003812
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerLobbyCreated obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x00005630 File Offset: 0x00003830
		~GalaxyTypeAwareListenerLobbyCreated()
		{
			this.Dispose();
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x00005660 File Offset: 0x00003860
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerLobbyCreated(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x000056E8 File Offset: 0x000038E8
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLobbyCreated_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000049 RID: 73
		private HandleRef swigCPtr;
	}
}
