using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200006D RID: 109
	public abstract class GlobalChatRoomMessagesListener : IChatRoomMessagesListener
	{
		// Token: 0x06000711 RID: 1809 RVA: 0x0000CD71 File Offset: 0x0000AF71
		internal GlobalChatRoomMessagesListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalChatRoomMessagesListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0000CD8D File Offset: 0x0000AF8D
		public GlobalChatRoomMessagesListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerChatRoomMessages.GetListenerType(), this);
			}
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0000CDAF File Offset: 0x0000AFAF
		internal static HandleRef getCPtr(GlobalChatRoomMessagesListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x0000CDD0 File Offset: 0x0000AFD0
		~GlobalChatRoomMessagesListener()
		{
			this.Dispose();
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x0000CE00 File Offset: 0x0000B000
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerChatRoomMessages.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalChatRoomMessagesListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x04000082 RID: 130
		private HandleRef swigCPtr;
	}
}
