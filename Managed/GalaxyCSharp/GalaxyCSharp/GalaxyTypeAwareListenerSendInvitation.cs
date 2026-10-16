using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200004B RID: 75
	public abstract class GalaxyTypeAwareListenerSendInvitation : IGalaxyListener
	{
		// Token: 0x0600065B RID: 1627 RVA: 0x00007050 File Offset: 0x00005250
		internal GalaxyTypeAwareListenerSendInvitation(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerSendInvitation_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x0000706C File Offset: 0x0000526C
		public GalaxyTypeAwareListenerSendInvitation()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerSendInvitation(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x0000708A File Offset: 0x0000528A
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerSendInvitation obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x000070A8 File Offset: 0x000052A8
		~GalaxyTypeAwareListenerSendInvitation()
		{
			this.Dispose();
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x000070D8 File Offset: 0x000052D8
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerSendInvitation(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00007160 File Offset: 0x00005360
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerSendInvitation_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x0400005F RID: 95
		private HandleRef swigCPtr;
	}
}
