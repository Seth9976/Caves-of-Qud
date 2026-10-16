using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000099 RID: 153
	public abstract class GlobalSendInvitationListener : ISendInvitationListener
	{
		// Token: 0x060007F3 RID: 2035 RVA: 0x000149EA File Offset: 0x00012BEA
		internal GlobalSendInvitationListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalSendInvitationListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			GlobalSendInvitationListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x00014A12 File Offset: 0x00012C12
		public GlobalSendInvitationListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerSendInvitation.GetListenerType(), this);
			}
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x00014A34 File Offset: 0x00012C34
		internal static HandleRef getCPtr(GlobalSendInvitationListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x00014A54 File Offset: 0x00012C54
		~GlobalSendInvitationListener()
		{
			this.Dispose();
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x00014A84 File Offset: 0x00012C84
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerSendInvitation.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalSendInvitationListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (GlobalSendInvitationListener.listeners.ContainsKey(handle))
					{
						GlobalSendInvitationListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000B4 RID: 180
		private static Dictionary<IntPtr, GlobalSendInvitationListener> listeners = new Dictionary<IntPtr, GlobalSendInvitationListener>();

		// Token: 0x040000B5 RID: 181
		private HandleRef swigCPtr;
	}
}
