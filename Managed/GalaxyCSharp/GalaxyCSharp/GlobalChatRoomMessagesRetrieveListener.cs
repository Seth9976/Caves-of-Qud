using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200006E RID: 110
	public abstract class GlobalChatRoomMessagesRetrieveListener : IChatRoomMessagesRetrieveListener
	{
		// Token: 0x06000716 RID: 1814 RVA: 0x0000D14E File Offset: 0x0000B34E
		internal GlobalChatRoomMessagesRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalChatRoomMessagesRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x0000D16A File Offset: 0x0000B36A
		public GlobalChatRoomMessagesRetrieveListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerChatRoomMessagesRetrieve.GetListenerType(), this);
			}
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0000D18C File Offset: 0x0000B38C
		internal static HandleRef getCPtr(GlobalChatRoomMessagesRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0000D1AC File Offset: 0x0000B3AC
		~GlobalChatRoomMessagesRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0000D1DC File Offset: 0x0000B3DC
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerChatRoomMessagesRetrieve.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalChatRoomMessagesRetrieveListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x04000083 RID: 131
		private HandleRef swigCPtr;
	}
}
