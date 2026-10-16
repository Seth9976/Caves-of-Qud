using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000045 RID: 69
	public abstract class GalaxyTypeAwareListenerOverlayInitializationStateChange : IGalaxyListener
	{
		// Token: 0x06000637 RID: 1591 RVA: 0x00006918 File Offset: 0x00004B18
		internal GalaxyTypeAwareListenerOverlayInitializationStateChange(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerOverlayInitializationStateChange_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00006934 File Offset: 0x00004B34
		public GalaxyTypeAwareListenerOverlayInitializationStateChange()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerOverlayInitializationStateChange(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00006952 File Offset: 0x00004B52
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerOverlayInitializationStateChange obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00006970 File Offset: 0x00004B70
		~GalaxyTypeAwareListenerOverlayInitializationStateChange()
		{
			this.Dispose();
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x000069A0 File Offset: 0x00004BA0
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerOverlayInitializationStateChange(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00006A28 File Offset: 0x00004C28
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerOverlayInitializationStateChange_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000059 RID: 89
		private HandleRef swigCPtr;
	}
}
