using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000043 RID: 67
	public abstract class GalaxyTypeAwareListenerOperationalStateChange : IGalaxyListener
	{
		// Token: 0x0600062B RID: 1579 RVA: 0x000066B0 File Offset: 0x000048B0
		internal GalaxyTypeAwareListenerOperationalStateChange(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerOperationalStateChange_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x000066CC File Offset: 0x000048CC
		public GalaxyTypeAwareListenerOperationalStateChange()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerOperationalStateChange(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x000066EA File Offset: 0x000048EA
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerOperationalStateChange obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x00006708 File Offset: 0x00004908
		~GalaxyTypeAwareListenerOperationalStateChange()
		{
			this.Dispose();
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00006738 File Offset: 0x00004938
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerOperationalStateChange(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x000067C0 File Offset: 0x000049C0
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerOperationalStateChange_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000057 RID: 87
		private HandleRef swigCPtr;
	}
}
