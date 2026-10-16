using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000050 RID: 80
	public abstract class GalaxyTypeAwareListenerTelemetryEventSend : IGalaxyListener
	{
		// Token: 0x06000679 RID: 1657 RVA: 0x00007654 File Offset: 0x00005854
		internal GalaxyTypeAwareListenerTelemetryEventSend(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerTelemetryEventSend_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x00007670 File Offset: 0x00005870
		public GalaxyTypeAwareListenerTelemetryEventSend()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerTelemetryEventSend(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0000768E File Offset: 0x0000588E
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerTelemetryEventSend obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x000076AC File Offset: 0x000058AC
		~GalaxyTypeAwareListenerTelemetryEventSend()
		{
			this.Dispose();
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x000076DC File Offset: 0x000058DC
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerTelemetryEventSend(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00007764 File Offset: 0x00005964
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerTelemetryEventSend_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000064 RID: 100
		private HandleRef swigCPtr;
	}
}
