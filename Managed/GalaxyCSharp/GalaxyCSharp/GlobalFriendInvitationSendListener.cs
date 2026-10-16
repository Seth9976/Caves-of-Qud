using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200007A RID: 122
	public abstract class GlobalFriendInvitationSendListener : IFriendInvitationSendListener
	{
		// Token: 0x06000755 RID: 1877 RVA: 0x0000FB34 File Offset: 0x0000DD34
		internal GlobalFriendInvitationSendListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalFriendInvitationSendListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			GlobalFriendInvitationSendListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0000FB5C File Offset: 0x0000DD5C
		public GlobalFriendInvitationSendListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerFriendInvitationSend.GetListenerType(), this);
			}
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0000FB7E File Offset: 0x0000DD7E
		internal static HandleRef getCPtr(GlobalFriendInvitationSendListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x0000FB9C File Offset: 0x0000DD9C
		~GlobalFriendInvitationSendListener()
		{
			this.Dispose();
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x0000FBCC File Offset: 0x0000DDCC
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerFriendInvitationSend.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalFriendInvitationSendListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (GlobalFriendInvitationSendListener.listeners.ContainsKey(handle))
					{
						GlobalFriendInvitationSendListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x04000092 RID: 146
		private static Dictionary<IntPtr, GlobalFriendInvitationSendListener> listeners = new Dictionary<IntPtr, GlobalFriendInvitationSendListener>();

		// Token: 0x04000093 RID: 147
		private HandleRef swigCPtr;
	}
}
