using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000060 RID: 96
	public abstract class GameServerGlobalLobbyMemberDataUpdateListener : ILobbyMemberDataUpdateListener
	{
		// Token: 0x060006CF RID: 1743 RVA: 0x0000A3AE File Offset: 0x000085AE
		internal GameServerGlobalLobbyMemberDataUpdateListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GameServerGlobalLobbyMemberDataUpdateListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0000A3CA File Offset: 0x000085CA
		public GameServerGlobalLobbyMemberDataUpdateListener()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Register(GalaxyTypeAwareListenerLobbyMemberDataUpdate.GetListenerType(), this);
			}
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0000A3EC File Offset: 0x000085EC
		internal static HandleRef getCPtr(GameServerGlobalLobbyMemberDataUpdateListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0000A40C File Offset: 0x0000860C
		~GameServerGlobalLobbyMemberDataUpdateListener()
		{
			this.Dispose();
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0000A43C File Offset: 0x0000863C
		public override void Dispose()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Unregister(GalaxyTypeAwareListenerLobbyMemberDataUpdate.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GameServerGlobalLobbyMemberDataUpdateListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x04000074 RID: 116
		private HandleRef swigCPtr;
	}
}
