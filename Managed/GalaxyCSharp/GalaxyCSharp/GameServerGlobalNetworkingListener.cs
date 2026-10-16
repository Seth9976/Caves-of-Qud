using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000063 RID: 99
	public abstract class GameServerGlobalNetworkingListener : INetworkingListener
	{
		// Token: 0x060006DE RID: 1758 RVA: 0x0000AE03 File Offset: 0x00009003
		internal GameServerGlobalNetworkingListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GameServerGlobalNetworkingListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x0000AE1F File Offset: 0x0000901F
		public GameServerGlobalNetworkingListener()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Register(GalaxyTypeAwareListenerNetworking.GetListenerType(), this);
			}
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x0000AE41 File Offset: 0x00009041
		internal static HandleRef getCPtr(GameServerGlobalNetworkingListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x0000AE60 File Offset: 0x00009060
		~GameServerGlobalNetworkingListener()
		{
			this.Dispose();
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x0000AE90 File Offset: 0x00009090
		public override void Dispose()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Unregister(GalaxyTypeAwareListenerNetworking.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GameServerGlobalNetworkingListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x04000077 RID: 119
		private HandleRef swigCPtr;
	}
}
