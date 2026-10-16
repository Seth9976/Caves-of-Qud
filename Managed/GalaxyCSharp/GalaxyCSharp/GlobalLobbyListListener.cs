using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000089 RID: 137
	public abstract class GlobalLobbyListListener : ILobbyListListener
	{
		// Token: 0x060007A2 RID: 1954 RVA: 0x0001207F File Offset: 0x0001027F
		internal GlobalLobbyListListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalLobbyListListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0001209B File Offset: 0x0001029B
		public GlobalLobbyListListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerLobbyList.GetListenerType(), this);
			}
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x000120BD File Offset: 0x000102BD
		internal static HandleRef getCPtr(GlobalLobbyListListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x000120DC File Offset: 0x000102DC
		~GlobalLobbyListListener()
		{
			this.Dispose();
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0001210C File Offset: 0x0001030C
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerLobbyList.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalLobbyListListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000A3 RID: 163
		private HandleRef swigCPtr;
	}
}
