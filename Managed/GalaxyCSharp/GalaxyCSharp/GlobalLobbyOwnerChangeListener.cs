using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200008D RID: 141
	public abstract class GlobalLobbyOwnerChangeListener : ILobbyOwnerChangeListener
	{
		// Token: 0x060007B6 RID: 1974 RVA: 0x00012770 File Offset: 0x00010970
		internal GlobalLobbyOwnerChangeListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalLobbyOwnerChangeListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x0001278C File Offset: 0x0001098C
		public GlobalLobbyOwnerChangeListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerLobbyOwnerChange.GetListenerType(), this);
			}
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x000127AE File Offset: 0x000109AE
		internal static HandleRef getCPtr(GlobalLobbyOwnerChangeListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x000127CC File Offset: 0x000109CC
		~GlobalLobbyOwnerChangeListener()
		{
			this.Dispose();
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x000127FC File Offset: 0x000109FC
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerLobbyOwnerChange.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalLobbyOwnerChangeListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000A7 RID: 167
		private HandleRef swigCPtr;
	}
}
