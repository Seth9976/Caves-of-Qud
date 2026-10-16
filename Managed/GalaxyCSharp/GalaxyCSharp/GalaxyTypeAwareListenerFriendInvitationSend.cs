using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200002C RID: 44
	public abstract class GalaxyTypeAwareListenerFriendInvitationSend : IGalaxyListener
	{
		// Token: 0x060005A1 RID: 1441 RVA: 0x00004B04 File Offset: 0x00002D04
		internal GalaxyTypeAwareListenerFriendInvitationSend(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerFriendInvitationSend_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00004B20 File Offset: 0x00002D20
		public GalaxyTypeAwareListenerFriendInvitationSend()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerFriendInvitationSend(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x00004B3E File Offset: 0x00002D3E
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerFriendInvitationSend obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00004B5C File Offset: 0x00002D5C
		~GalaxyTypeAwareListenerFriendInvitationSend()
		{
			this.Dispose();
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x00004B8C File Offset: 0x00002D8C
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerFriendInvitationSend(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00004C14 File Offset: 0x00002E14
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerFriendInvitationSend_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000040 RID: 64
		private HandleRef swigCPtr;
	}
}
