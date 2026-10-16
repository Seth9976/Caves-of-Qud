using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000078 RID: 120
	public abstract class GlobalFriendInvitationListRetrieveListener : IFriendInvitationListRetrieveListener
	{
		// Token: 0x06000749 RID: 1865 RVA: 0x0000F2E2 File Offset: 0x0000D4E2
		internal GlobalFriendInvitationListRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalFriendInvitationListRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			GlobalFriendInvitationListRetrieveListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x0000F30A File Offset: 0x0000D50A
		public GlobalFriendInvitationListRetrieveListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerFriendInvitationListRetrieve.GetListenerType(), this);
			}
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0000F32C File Offset: 0x0000D52C
		internal static HandleRef getCPtr(GlobalFriendInvitationListRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0000F34C File Offset: 0x0000D54C
		~GlobalFriendInvitationListRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x0000F37C File Offset: 0x0000D57C
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerFriendInvitationListRetrieve.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalFriendInvitationListRetrieveListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (GlobalFriendInvitationListRetrieveListener.listeners.ContainsKey(handle))
					{
						GlobalFriendInvitationListRetrieveListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0400008E RID: 142
		private static Dictionary<IntPtr, GlobalFriendInvitationListRetrieveListener> listeners = new Dictionary<IntPtr, GlobalFriendInvitationListRetrieveListener>();

		// Token: 0x0400008F RID: 143
		private HandleRef swigCPtr;
	}
}
