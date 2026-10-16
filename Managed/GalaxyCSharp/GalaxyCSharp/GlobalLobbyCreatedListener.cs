using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000083 RID: 131
	public abstract class GlobalLobbyCreatedListener : ILobbyCreatedListener
	{
		// Token: 0x06000784 RID: 1924 RVA: 0x00011764 File Offset: 0x0000F964
		internal GlobalLobbyCreatedListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalLobbyCreatedListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x00011780 File Offset: 0x0000F980
		public GlobalLobbyCreatedListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerLobbyCreated.GetListenerType(), this);
			}
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x000117A2 File Offset: 0x0000F9A2
		internal static HandleRef getCPtr(GlobalLobbyCreatedListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x000117C0 File Offset: 0x0000F9C0
		~GlobalLobbyCreatedListener()
		{
			this.Dispose();
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x000117F0 File Offset: 0x0000F9F0
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerLobbyCreated.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalLobbyCreatedListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0400009D RID: 157
		private HandleRef swigCPtr;
	}
}
