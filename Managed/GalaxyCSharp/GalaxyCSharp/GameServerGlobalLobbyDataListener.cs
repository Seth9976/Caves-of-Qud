using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200005B RID: 91
	public abstract class GameServerGlobalLobbyDataListener : ILobbyDataListener
	{
		// Token: 0x060006B6 RID: 1718 RVA: 0x0000911C File Offset: 0x0000731C
		internal GameServerGlobalLobbyDataListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GameServerGlobalLobbyDataListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x00009138 File Offset: 0x00007338
		public GameServerGlobalLobbyDataListener()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Register(GalaxyTypeAwareListenerLobbyData.GetListenerType(), this);
			}
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0000915A File Offset: 0x0000735A
		internal static HandleRef getCPtr(GameServerGlobalLobbyDataListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x00009178 File Offset: 0x00007378
		~GameServerGlobalLobbyDataListener()
		{
			this.Dispose();
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x000091A8 File Offset: 0x000073A8
		public override void Dispose()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Unregister(GalaxyTypeAwareListenerLobbyData.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GameServerGlobalLobbyDataListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0400006F RID: 111
		private HandleRef swigCPtr;
	}
}
