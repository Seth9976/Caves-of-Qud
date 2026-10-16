using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000065 RID: 101
	public abstract class GameServerGlobalRichPresenceRetrieveListener : IRichPresenceRetrieveListener
	{
		// Token: 0x060006E8 RID: 1768 RVA: 0x0000B560 File Offset: 0x00009760
		internal GameServerGlobalRichPresenceRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GameServerGlobalRichPresenceRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x0000B57C File Offset: 0x0000977C
		public GameServerGlobalRichPresenceRetrieveListener()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Register(GalaxyTypeAwareListenerRichPresenceRetrieve.GetListenerType(), this);
			}
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x0000B59E File Offset: 0x0000979E
		internal static HandleRef getCPtr(GameServerGlobalRichPresenceRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x0000B5BC File Offset: 0x000097BC
		~GameServerGlobalRichPresenceRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0000B5EC File Offset: 0x000097EC
		public override void Dispose()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Unregister(GalaxyTypeAwareListenerRichPresenceRetrieve.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GameServerGlobalRichPresenceRetrieveListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x04000079 RID: 121
		private HandleRef swigCPtr;
	}
}
