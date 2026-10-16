using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000077 RID: 119
	public abstract class GlobalFriendInvitationListener : IFriendInvitationListener
	{
		// Token: 0x06000743 RID: 1859 RVA: 0x0000EF0B File Offset: 0x0000D10B
		internal GlobalFriendInvitationListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalFriendInvitationListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			GlobalFriendInvitationListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0000EF33 File Offset: 0x0000D133
		public GlobalFriendInvitationListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerFriendInvitation.GetListenerType(), this);
			}
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0000EF55 File Offset: 0x0000D155
		internal static HandleRef getCPtr(GlobalFriendInvitationListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x0000EF74 File Offset: 0x0000D174
		~GlobalFriendInvitationListener()
		{
			this.Dispose();
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x0000EFA4 File Offset: 0x0000D1A4
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerFriendInvitation.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalFriendInvitationListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (GlobalFriendInvitationListener.listeners.ContainsKey(handle))
					{
						GlobalFriendInvitationListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0400008C RID: 140
		private static Dictionary<IntPtr, GlobalFriendInvitationListener> listeners = new Dictionary<IntPtr, GlobalFriendInvitationListener>();

		// Token: 0x0400008D RID: 141
		private HandleRef swigCPtr;
	}
}
