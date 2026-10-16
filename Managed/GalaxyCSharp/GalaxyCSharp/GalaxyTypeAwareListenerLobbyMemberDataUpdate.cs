using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200003C RID: 60
	public abstract class GalaxyTypeAwareListenerLobbyMemberDataUpdate : IGalaxyListener
	{
		// Token: 0x06000601 RID: 1537 RVA: 0x00005E44 File Offset: 0x00004044
		internal GalaxyTypeAwareListenerLobbyMemberDataUpdate(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLobbyMemberDataUpdate_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00005E60 File Offset: 0x00004060
		public GalaxyTypeAwareListenerLobbyMemberDataUpdate()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerLobbyMemberDataUpdate(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00005E7E File Offset: 0x0000407E
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerLobbyMemberDataUpdate obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00005E9C File Offset: 0x0000409C
		~GalaxyTypeAwareListenerLobbyMemberDataUpdate()
		{
			this.Dispose();
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00005ECC File Offset: 0x000040CC
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerLobbyMemberDataUpdate(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00005F54 File Offset: 0x00004154
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLobbyMemberDataUpdate_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000050 RID: 80
		private HandleRef swigCPtr;
	}
}
