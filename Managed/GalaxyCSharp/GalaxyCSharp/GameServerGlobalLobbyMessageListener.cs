using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000062 RID: 98
	public abstract class GameServerGlobalLobbyMessageListener : ILobbyMessageListener
	{
		// Token: 0x060006D9 RID: 1753 RVA: 0x0000AAC2 File Offset: 0x00008CC2
		internal GameServerGlobalLobbyMessageListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GameServerGlobalLobbyMessageListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x0000AADE File Offset: 0x00008CDE
		public GameServerGlobalLobbyMessageListener()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Register(GalaxyTypeAwareListenerLobbyMessage.GetListenerType(), this);
			}
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x0000AB00 File Offset: 0x00008D00
		internal static HandleRef getCPtr(GameServerGlobalLobbyMessageListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x0000AB20 File Offset: 0x00008D20
		~GameServerGlobalLobbyMessageListener()
		{
			this.Dispose();
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x0000AB50 File Offset: 0x00008D50
		public override void Dispose()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Unregister(GalaxyTypeAwareListenerLobbyMessage.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GameServerGlobalLobbyMessageListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x04000076 RID: 118
		private HandleRef swigCPtr;
	}
}
