using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200007C RID: 124
	public abstract class GlobalGameInvitationReceivedListener : IGameInvitationReceivedListener
	{
		// Token: 0x06000760 RID: 1888 RVA: 0x0001025B File Offset: 0x0000E45B
		internal GlobalGameInvitationReceivedListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalGameInvitationReceivedListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			GlobalGameInvitationReceivedListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x00010283 File Offset: 0x0000E483
		public GlobalGameInvitationReceivedListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerGameInvitationReceived.GetListenerType(), this);
			}
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x000102A5 File Offset: 0x0000E4A5
		internal static HandleRef getCPtr(GlobalGameInvitationReceivedListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x000102C4 File Offset: 0x0000E4C4
		~GlobalGameInvitationReceivedListener()
		{
			this.Dispose();
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x000102F4 File Offset: 0x0000E4F4
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerGameInvitationReceived.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalGameInvitationReceivedListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (GlobalGameInvitationReceivedListener.listeners.ContainsKey(handle))
					{
						GlobalGameInvitationReceivedListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x04000095 RID: 149
		private static Dictionary<IntPtr, GlobalGameInvitationReceivedListener> listeners = new Dictionary<IntPtr, GlobalGameInvitationReceivedListener>();

		// Token: 0x04000096 RID: 150
		private HandleRef swigCPtr;
	}
}
