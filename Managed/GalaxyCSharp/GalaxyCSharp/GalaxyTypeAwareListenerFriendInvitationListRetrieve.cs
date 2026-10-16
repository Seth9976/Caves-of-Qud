using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200002A RID: 42
	public abstract class GalaxyTypeAwareListenerFriendInvitationListRetrieve : IGalaxyListener
	{
		// Token: 0x06000595 RID: 1429 RVA: 0x0000489C File Offset: 0x00002A9C
		internal GalaxyTypeAwareListenerFriendInvitationListRetrieve(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerFriendInvitationListRetrieve_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x000048B8 File Offset: 0x00002AB8
		public GalaxyTypeAwareListenerFriendInvitationListRetrieve()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerFriendInvitationListRetrieve(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x000048D6 File Offset: 0x00002AD6
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerFriendInvitationListRetrieve obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x000048F4 File Offset: 0x00002AF4
		~GalaxyTypeAwareListenerFriendInvitationListRetrieve()
		{
			this.Dispose();
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00004924 File Offset: 0x00002B24
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerFriendInvitationListRetrieve(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x000049AC File Offset: 0x00002BAC
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerFriendInvitationListRetrieve_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x0400003E RID: 62
		private HandleRef swigCPtr;
	}
}
