using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200008C RID: 140
	public abstract class GlobalLobbyMessageListener : ILobbyMessageListener
	{
		// Token: 0x060007B1 RID: 1969 RVA: 0x00012404 File Offset: 0x00010604
		internal GlobalLobbyMessageListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalLobbyMessageListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x00012420 File Offset: 0x00010620
		public GlobalLobbyMessageListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerLobbyMessage.GetListenerType(), this);
			}
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x00012442 File Offset: 0x00010642
		internal static HandleRef getCPtr(GlobalLobbyMessageListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x00012460 File Offset: 0x00010660
		~GlobalLobbyMessageListener()
		{
			this.Dispose();
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x00012490 File Offset: 0x00010690
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerLobbyMessage.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalLobbyMessageListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000A6 RID: 166
		private HandleRef swigCPtr;
	}
}
