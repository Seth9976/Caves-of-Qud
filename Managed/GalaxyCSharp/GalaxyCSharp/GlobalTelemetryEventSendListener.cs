using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200009E RID: 158
	public abstract class GlobalTelemetryEventSendListener : ITelemetryEventSendListener
	{
		// Token: 0x0600080E RID: 2062 RVA: 0x000157C4 File Offset: 0x000139C4
		internal GlobalTelemetryEventSendListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalTelemetryEventSendListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x000157E0 File Offset: 0x000139E0
		public GlobalTelemetryEventSendListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerTelemetryEventSend.GetListenerType(), this);
			}
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x00015802 File Offset: 0x00013A02
		internal static HandleRef getCPtr(GlobalTelemetryEventSendListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x00015820 File Offset: 0x00013A20
		~GlobalTelemetryEventSendListener()
		{
			this.Dispose();
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x00015850 File Offset: 0x00013A50
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerTelemetryEventSend.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalTelemetryEventSendListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000BB RID: 187
		private HandleRef swigCPtr;
	}
}
