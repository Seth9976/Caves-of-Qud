using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000017 RID: 23
	public abstract class GalaxyTypeAwareListenerAccessToken : IGalaxyListener
	{
		// Token: 0x06000523 RID: 1315 RVA: 0x000031C0 File Offset: 0x000013C0
		internal GalaxyTypeAwareListenerAccessToken(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerAccessToken_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x000031DC File Offset: 0x000013DC
		public GalaxyTypeAwareListenerAccessToken()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerAccessToken(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x000031FA File Offset: 0x000013FA
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerAccessToken obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00003218 File Offset: 0x00001418
		~GalaxyTypeAwareListenerAccessToken()
		{
			this.Dispose();
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00003248 File Offset: 0x00001448
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerAccessToken(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x000032D0 File Offset: 0x000014D0
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerAccessToken_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x0400002B RID: 43
		private HandleRef swigCPtr;
	}
}
