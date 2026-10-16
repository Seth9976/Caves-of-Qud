using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000079 RID: 121
	public abstract class GlobalFriendInvitationRespondToListener : IFriendInvitationRespondToListener
	{
		// Token: 0x0600074F RID: 1871 RVA: 0x0000F711 File Offset: 0x0000D911
		internal GlobalFriendInvitationRespondToListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalFriendInvitationRespondToListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			GlobalFriendInvitationRespondToListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x0000F739 File Offset: 0x0000D939
		public GlobalFriendInvitationRespondToListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerFriendInvitationRespondTo.GetListenerType(), this);
			}
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x0000F75B File Offset: 0x0000D95B
		internal static HandleRef getCPtr(GlobalFriendInvitationRespondToListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x0000F77C File Offset: 0x0000D97C
		~GlobalFriendInvitationRespondToListener()
		{
			this.Dispose();
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x0000F7AC File Offset: 0x0000D9AC
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerFriendInvitationRespondTo.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalFriendInvitationRespondToListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (GlobalFriendInvitationRespondToListener.listeners.ContainsKey(handle))
					{
						GlobalFriendInvitationRespondToListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x04000090 RID: 144
		private static Dictionary<IntPtr, GlobalFriendInvitationRespondToListener> listeners = new Dictionary<IntPtr, GlobalFriendInvitationRespondToListener>();

		// Token: 0x04000091 RID: 145
		private HandleRef swigCPtr;
	}
}
