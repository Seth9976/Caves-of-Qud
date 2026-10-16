using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200005F RID: 95
	public abstract class GameServerGlobalLobbyLeftListener : ILobbyLeftListener
	{
		// Token: 0x060006CA RID: 1738 RVA: 0x00009F7B File Offset: 0x0000817B
		internal GameServerGlobalLobbyLeftListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GameServerGlobalLobbyLeftListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x00009F97 File Offset: 0x00008197
		public GameServerGlobalLobbyLeftListener()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Register(GalaxyTypeAwareListenerLobbyLeft.GetListenerType(), this);
			}
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x00009FB9 File Offset: 0x000081B9
		internal static HandleRef getCPtr(GameServerGlobalLobbyLeftListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x00009FD8 File Offset: 0x000081D8
		~GameServerGlobalLobbyLeftListener()
		{
			this.Dispose();
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x0000A008 File Offset: 0x00008208
		public override void Dispose()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Unregister(GalaxyTypeAwareListenerLobbyLeft.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GameServerGlobalLobbyLeftListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x04000073 RID: 115
		private HandleRef swigCPtr;
	}
}
