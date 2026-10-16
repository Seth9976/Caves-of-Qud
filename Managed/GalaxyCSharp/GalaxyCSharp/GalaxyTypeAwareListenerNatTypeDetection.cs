using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000040 RID: 64
	public abstract class GalaxyTypeAwareListenerNatTypeDetection : IGalaxyListener
	{
		// Token: 0x06000619 RID: 1561 RVA: 0x00006314 File Offset: 0x00004514
		internal GalaxyTypeAwareListenerNatTypeDetection(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerNatTypeDetection_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00006330 File Offset: 0x00004530
		public GalaxyTypeAwareListenerNatTypeDetection()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerNatTypeDetection(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0000634E File Offset: 0x0000454E
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerNatTypeDetection obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0000636C File Offset: 0x0000456C
		~GalaxyTypeAwareListenerNatTypeDetection()
		{
			this.Dispose();
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0000639C File Offset: 0x0000459C
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerNatTypeDetection(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00006424 File Offset: 0x00004624
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerNatTypeDetection_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000054 RID: 84
		private HandleRef swigCPtr;
	}
}
