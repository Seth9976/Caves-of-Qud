using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000046 RID: 70
	public abstract class GalaxyTypeAwareListenerOverlayVisibilityChange : IGalaxyListener
	{
		// Token: 0x0600063D RID: 1597 RVA: 0x00006A4C File Offset: 0x00004C4C
		internal GalaxyTypeAwareListenerOverlayVisibilityChange(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerOverlayVisibilityChange_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x00006A68 File Offset: 0x00004C68
		public GalaxyTypeAwareListenerOverlayVisibilityChange()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerOverlayVisibilityChange(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x00006A86 File Offset: 0x00004C86
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerOverlayVisibilityChange obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x00006AA4 File Offset: 0x00004CA4
		~GalaxyTypeAwareListenerOverlayVisibilityChange()
		{
			this.Dispose();
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x00006AD4 File Offset: 0x00004CD4
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerOverlayVisibilityChange(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x00006B5C File Offset: 0x00004D5C
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerOverlayVisibilityChange_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x0400005A RID: 90
		private HandleRef swigCPtr;
	}
}
