using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200005A RID: 90
	public abstract class GameServerGlobalLobbyCreatedListener : ILobbyCreatedListener
	{
		// Token: 0x060006B1 RID: 1713 RVA: 0x00008DAF File Offset: 0x00006FAF
		internal GameServerGlobalLobbyCreatedListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GameServerGlobalLobbyCreatedListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00008DCB File Offset: 0x00006FCB
		public GameServerGlobalLobbyCreatedListener()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Register(GalaxyTypeAwareListenerLobbyCreated.GetListenerType(), this);
			}
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00008DED File Offset: 0x00006FED
		internal static HandleRef getCPtr(GameServerGlobalLobbyCreatedListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x00008E0C File Offset: 0x0000700C
		~GameServerGlobalLobbyCreatedListener()
		{
			this.Dispose();
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x00008E3C File Offset: 0x0000703C
		public override void Dispose()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Unregister(GalaxyTypeAwareListenerLobbyCreated.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GameServerGlobalLobbyCreatedListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0400006E RID: 110
		private HandleRef swigCPtr;
	}
}
