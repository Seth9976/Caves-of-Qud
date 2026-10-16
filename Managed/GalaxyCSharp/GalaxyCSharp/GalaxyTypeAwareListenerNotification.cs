using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000042 RID: 66
	public abstract class GalaxyTypeAwareListenerNotification : IGalaxyListener
	{
		// Token: 0x06000625 RID: 1573 RVA: 0x0000657C File Offset: 0x0000477C
		internal GalaxyTypeAwareListenerNotification(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerNotification_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00006598 File Offset: 0x00004798
		public GalaxyTypeAwareListenerNotification()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerNotification(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x000065B6 File Offset: 0x000047B6
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerNotification obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x000065D4 File Offset: 0x000047D4
		~GalaxyTypeAwareListenerNotification()
		{
			this.Dispose();
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x00006604 File Offset: 0x00004804
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerNotification(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x0000668C File Offset: 0x0000488C
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerNotification_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000056 RID: 86
		private HandleRef swigCPtr;
	}
}
