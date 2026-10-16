using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200006F RID: 111
	public abstract class GlobalChatRoomWithUserRetrieveListener : IChatRoomWithUserRetrieveListener
	{
		// Token: 0x0600071B RID: 1819 RVA: 0x0000D53D File Offset: 0x0000B73D
		internal GlobalChatRoomWithUserRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalChatRoomWithUserRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0000D559 File Offset: 0x0000B759
		public GlobalChatRoomWithUserRetrieveListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerChatRoomWithUserRetrieve.GetListenerType(), this);
			}
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0000D57B File Offset: 0x0000B77B
		internal static HandleRef getCPtr(GlobalChatRoomWithUserRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x0000D59C File Offset: 0x0000B79C
		~GlobalChatRoomWithUserRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x0000D5CC File Offset: 0x0000B7CC
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerChatRoomWithUserRetrieve.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalChatRoomWithUserRetrieveListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x04000084 RID: 132
		private HandleRef swigCPtr;
	}
}
