using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000061 RID: 97
	public abstract class GameServerGlobalLobbyMemberStateListener : ILobbyMemberStateListener
	{
		// Token: 0x060006D4 RID: 1748 RVA: 0x0000A72A File Offset: 0x0000892A
		internal GameServerGlobalLobbyMemberStateListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GameServerGlobalLobbyMemberStateListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x0000A746 File Offset: 0x00008946
		public GameServerGlobalLobbyMemberStateListener()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Register(GalaxyTypeAwareListenerLobbyMemberState.GetListenerType(), this);
			}
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0000A768 File Offset: 0x00008968
		internal static HandleRef getCPtr(GameServerGlobalLobbyMemberStateListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x0000A788 File Offset: 0x00008988
		~GameServerGlobalLobbyMemberStateListener()
		{
			this.Dispose();
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x0000A7B8 File Offset: 0x000089B8
		public override void Dispose()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Unregister(GalaxyTypeAwareListenerLobbyMemberState.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GameServerGlobalLobbyMemberStateListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x04000075 RID: 117
		private HandleRef swigCPtr;
	}
}
