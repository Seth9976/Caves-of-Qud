using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000084 RID: 132
	public abstract class GlobalLobbyDataListener : ILobbyDataListener
	{
		// Token: 0x06000789 RID: 1929 RVA: 0x00011890 File Offset: 0x0000FA90
		internal GlobalLobbyDataListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalLobbyDataListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x000118AC File Offset: 0x0000FAAC
		public GlobalLobbyDataListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerLobbyData.GetListenerType(), this);
			}
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x000118CE File Offset: 0x0000FACE
		internal static HandleRef getCPtr(GlobalLobbyDataListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x000118EC File Offset: 0x0000FAEC
		~GlobalLobbyDataListener()
		{
			this.Dispose();
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0001191C File Offset: 0x0000FB1C
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerLobbyData.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalLobbyDataListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0400009E RID: 158
		private HandleRef swigCPtr;
	}
}
