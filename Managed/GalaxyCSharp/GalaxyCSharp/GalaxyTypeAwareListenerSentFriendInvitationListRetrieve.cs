using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200004C RID: 76
	public abstract class GalaxyTypeAwareListenerSentFriendInvitationListRetrieve : IGalaxyListener
	{
		// Token: 0x06000661 RID: 1633 RVA: 0x00007184 File Offset: 0x00005384
		internal GalaxyTypeAwareListenerSentFriendInvitationListRetrieve(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerSentFriendInvitationListRetrieve_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x000071A0 File Offset: 0x000053A0
		public GalaxyTypeAwareListenerSentFriendInvitationListRetrieve()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerSentFriendInvitationListRetrieve(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x000071BE File Offset: 0x000053BE
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerSentFriendInvitationListRetrieve obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x000071DC File Offset: 0x000053DC
		~GalaxyTypeAwareListenerSentFriendInvitationListRetrieve()
		{
			this.Dispose();
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0000720C File Offset: 0x0000540C
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerSentFriendInvitationListRetrieve(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00007294 File Offset: 0x00005494
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerSentFriendInvitationListRetrieve_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000060 RID: 96
		private HandleRef swigCPtr;
	}
}
