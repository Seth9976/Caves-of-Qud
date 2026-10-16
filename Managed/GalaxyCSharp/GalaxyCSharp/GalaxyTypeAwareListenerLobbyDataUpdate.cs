using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000038 RID: 56
	public abstract class GalaxyTypeAwareListenerLobbyDataUpdate : IGalaxyListener
	{
		// Token: 0x060005E9 RID: 1513 RVA: 0x00005974 File Offset: 0x00003B74
		internal GalaxyTypeAwareListenerLobbyDataUpdate(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLobbyDataUpdate_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00005990 File Offset: 0x00003B90
		public GalaxyTypeAwareListenerLobbyDataUpdate()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerLobbyDataUpdate(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x000059AE File Offset: 0x00003BAE
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerLobbyDataUpdate obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x000059CC File Offset: 0x00003BCC
		~GalaxyTypeAwareListenerLobbyDataUpdate()
		{
			this.Dispose();
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x000059FC File Offset: 0x00003BFC
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerLobbyDataUpdate(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00005A84 File Offset: 0x00003C84
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLobbyDataUpdate_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x0400004C RID: 76
		private HandleRef swigCPtr;
	}
}
