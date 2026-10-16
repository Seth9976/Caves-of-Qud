using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000067 RID: 103
	public abstract class GameServerGlobalTelemetryEventSendListener : ITelemetryEventSendListener
	{
		// Token: 0x060006F2 RID: 1778 RVA: 0x0000BC7E File Offset: 0x00009E7E
		internal GameServerGlobalTelemetryEventSendListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GameServerGlobalTelemetryEventSendListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x0000BC9A File Offset: 0x00009E9A
		public GameServerGlobalTelemetryEventSendListener()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Register(GalaxyTypeAwareListenerTelemetryEventSend.GetListenerType(), this);
			}
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0000BCBC File Offset: 0x00009EBC
		internal static HandleRef getCPtr(GameServerGlobalTelemetryEventSendListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0000BCDC File Offset: 0x00009EDC
		~GameServerGlobalTelemetryEventSendListener()
		{
			this.Dispose();
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0000BD0C File Offset: 0x00009F0C
		public override void Dispose()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Unregister(GalaxyTypeAwareListenerTelemetryEventSend.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GameServerGlobalTelemetryEventSendListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0400007B RID: 123
		private HandleRef swigCPtr;
	}
}
