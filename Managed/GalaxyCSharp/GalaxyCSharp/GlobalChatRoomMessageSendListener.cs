using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200006C RID: 108
	public abstract class GlobalChatRoomMessageSendListener : IChatRoomMessageSendListener
	{
		// Token: 0x0600070C RID: 1804 RVA: 0x0000CA24 File Offset: 0x0000AC24
		internal GlobalChatRoomMessageSendListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalChatRoomMessageSendListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x0000CA40 File Offset: 0x0000AC40
		public GlobalChatRoomMessageSendListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerChatRoomMessageSend.GetListenerType(), this);
			}
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0000CA62 File Offset: 0x0000AC62
		internal static HandleRef getCPtr(GlobalChatRoomMessageSendListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x0000CA80 File Offset: 0x0000AC80
		~GlobalChatRoomMessageSendListener()
		{
			this.Dispose();
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0000CAB0 File Offset: 0x0000ACB0
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerChatRoomMessageSend.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalChatRoomMessageSendListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x04000081 RID: 129
		private HandleRef swigCPtr;
	}
}
