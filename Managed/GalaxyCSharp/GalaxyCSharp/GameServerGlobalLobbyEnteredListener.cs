using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200005E RID: 94
	public abstract class GameServerGlobalLobbyEnteredListener : ILobbyEnteredListener
	{
		// Token: 0x060006C5 RID: 1733 RVA: 0x00009C2B File Offset: 0x00007E2B
		internal GameServerGlobalLobbyEnteredListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GameServerGlobalLobbyEnteredListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x00009C47 File Offset: 0x00007E47
		public GameServerGlobalLobbyEnteredListener()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Register(GalaxyTypeAwareListenerLobbyEntered.GetListenerType(), this);
			}
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x00009C69 File Offset: 0x00007E69
		internal static HandleRef getCPtr(GameServerGlobalLobbyEnteredListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x00009C88 File Offset: 0x00007E88
		~GameServerGlobalLobbyEnteredListener()
		{
			this.Dispose();
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x00009CB8 File Offset: 0x00007EB8
		public override void Dispose()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Unregister(GalaxyTypeAwareListenerLobbyEntered.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GameServerGlobalLobbyEnteredListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x04000072 RID: 114
		private HandleRef swigCPtr;
	}
}
