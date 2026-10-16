using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000085 RID: 133
	public abstract class GlobalLobbyDataRetrieveListener : ILobbyDataRetrieveListener
	{
		// Token: 0x0600078E RID: 1934 RVA: 0x000119BC File Offset: 0x0000FBBC
		internal GlobalLobbyDataRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalLobbyDataRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x000119D8 File Offset: 0x0000FBD8
		public GlobalLobbyDataRetrieveListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerLobbyDataRetrieve.GetListenerType(), this);
			}
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x000119FA File Offset: 0x0000FBFA
		internal static HandleRef getCPtr(GlobalLobbyDataRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x00011A18 File Offset: 0x0000FC18
		~GlobalLobbyDataRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x00011A48 File Offset: 0x0000FC48
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerLobbyDataRetrieve.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalLobbyDataRetrieveListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0400009F RID: 159
		private HandleRef swigCPtr;
	}
}
