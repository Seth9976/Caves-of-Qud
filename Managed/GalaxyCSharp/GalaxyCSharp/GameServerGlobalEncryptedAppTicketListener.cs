using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000058 RID: 88
	public abstract class GameServerGlobalEncryptedAppTicketListener : IEncryptedAppTicketListener
	{
		// Token: 0x060006A7 RID: 1703 RVA: 0x0000872A File Offset: 0x0000692A
		internal GameServerGlobalEncryptedAppTicketListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GameServerGlobalEncryptedAppTicketListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x00008746 File Offset: 0x00006946
		public GameServerGlobalEncryptedAppTicketListener()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Register(GalaxyTypeAwareListenerEncryptedAppTicket.GetListenerType(), this);
			}
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00008768 File Offset: 0x00006968
		internal static HandleRef getCPtr(GameServerGlobalEncryptedAppTicketListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x00008788 File Offset: 0x00006988
		~GameServerGlobalEncryptedAppTicketListener()
		{
			this.Dispose();
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x000087B8 File Offset: 0x000069B8
		public override void Dispose()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Unregister(GalaxyTypeAwareListenerEncryptedAppTicket.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GameServerGlobalEncryptedAppTicketListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0400006C RID: 108
		private HandleRef swigCPtr;
	}
}
