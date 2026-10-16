using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000059 RID: 89
	public abstract class GameServerGlobalGogServicesConnectionStateListener : IGogServicesConnectionStateListener
	{
		// Token: 0x060006AC RID: 1708 RVA: 0x00008A5D File Offset: 0x00006C5D
		internal GameServerGlobalGogServicesConnectionStateListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GameServerGlobalGogServicesConnectionStateListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00008A79 File Offset: 0x00006C79
		public GameServerGlobalGogServicesConnectionStateListener()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Register(GalaxyTypeAwareListenerGogServicesConnectionState.GetListenerType(), this);
			}
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00008A9B File Offset: 0x00006C9B
		internal static HandleRef getCPtr(GameServerGlobalGogServicesConnectionStateListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00008ABC File Offset: 0x00006CBC
		~GameServerGlobalGogServicesConnectionStateListener()
		{
			this.Dispose();
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00008AEC File Offset: 0x00006CEC
		public override void Dispose()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Unregister(GalaxyTypeAwareListenerGogServicesConnectionState.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GameServerGlobalGogServicesConnectionStateListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0400006D RID: 109
		private HandleRef swigCPtr;
	}
}
