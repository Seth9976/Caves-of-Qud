using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200009A RID: 154
	public abstract class GlobalSentFriendInvitationListRetrieveListener : ISentFriendInvitationListRetrieveListener
	{
		// Token: 0x060007F9 RID: 2041 RVA: 0x00014DC2 File Offset: 0x00012FC2
		internal GlobalSentFriendInvitationListRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalSentFriendInvitationListRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			GlobalSentFriendInvitationListRetrieveListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x00014DEA File Offset: 0x00012FEA
		public GlobalSentFriendInvitationListRetrieveListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerSentFriendInvitationListRetrieve.GetListenerType(), this);
			}
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x00014E0C File Offset: 0x0001300C
		internal static HandleRef getCPtr(GlobalSentFriendInvitationListRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x00014E2C File Offset: 0x0001302C
		~GlobalSentFriendInvitationListRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x00014E5C File Offset: 0x0001305C
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerSentFriendInvitationListRetrieve.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalSentFriendInvitationListRetrieveListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (GlobalSentFriendInvitationListRetrieveListener.listeners.ContainsKey(handle))
					{
						GlobalSentFriendInvitationListRetrieveListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000B6 RID: 182
		private static Dictionary<IntPtr, GlobalSentFriendInvitationListRetrieveListener> listeners = new Dictionary<IntPtr, GlobalSentFriendInvitationListRetrieveListener>();

		// Token: 0x040000B7 RID: 183
		private HandleRef swigCPtr;
	}
}
