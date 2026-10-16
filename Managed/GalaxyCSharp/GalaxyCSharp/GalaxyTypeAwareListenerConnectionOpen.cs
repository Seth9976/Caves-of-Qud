using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000024 RID: 36
	public abstract class GalaxyTypeAwareListenerConnectionOpen : IGalaxyListener
	{
		// Token: 0x06000571 RID: 1393 RVA: 0x00004164 File Offset: 0x00002364
		internal GalaxyTypeAwareListenerConnectionOpen(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerConnectionOpen_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00004180 File Offset: 0x00002380
		public GalaxyTypeAwareListenerConnectionOpen()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerConnectionOpen(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0000419E File Offset: 0x0000239E
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerConnectionOpen obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x000041BC File Offset: 0x000023BC
		~GalaxyTypeAwareListenerConnectionOpen()
		{
			this.Dispose();
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x000041EC File Offset: 0x000023EC
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerConnectionOpen(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00004274 File Offset: 0x00002474
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerConnectionOpen_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000038 RID: 56
		private HandleRef swigCPtr;
	}
}
