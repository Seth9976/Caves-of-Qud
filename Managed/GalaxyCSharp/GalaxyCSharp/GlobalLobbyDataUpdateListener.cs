using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000086 RID: 134
	public abstract class GlobalLobbyDataUpdateListener : ILobbyDataUpdateListener
	{
		// Token: 0x06000793 RID: 1939 RVA: 0x00011AE8 File Offset: 0x0000FCE8
		internal GlobalLobbyDataUpdateListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalLobbyDataUpdateListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x00011B04 File Offset: 0x0000FD04
		public GlobalLobbyDataUpdateListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerLobbyDataUpdate.GetListenerType(), this);
			}
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x00011B26 File Offset: 0x0000FD26
		internal static HandleRef getCPtr(GlobalLobbyDataUpdateListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x00011B44 File Offset: 0x0000FD44
		~GlobalLobbyDataUpdateListener()
		{
			this.Dispose();
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x00011B74 File Offset: 0x0000FD74
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerLobbyDataUpdate.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalLobbyDataUpdateListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000A0 RID: 160
		private HandleRef swigCPtr;
	}
}
