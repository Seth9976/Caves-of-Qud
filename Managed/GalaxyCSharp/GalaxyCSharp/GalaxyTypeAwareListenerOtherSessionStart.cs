using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000044 RID: 68
	public abstract class GalaxyTypeAwareListenerOtherSessionStart : IGalaxyListener
	{
		// Token: 0x06000631 RID: 1585 RVA: 0x000067E4 File Offset: 0x000049E4
		internal GalaxyTypeAwareListenerOtherSessionStart(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerOtherSessionStart_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00006800 File Offset: 0x00004A00
		public GalaxyTypeAwareListenerOtherSessionStart()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerOtherSessionStart(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0000681E File Offset: 0x00004A1E
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerOtherSessionStart obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0000683C File Offset: 0x00004A3C
		~GalaxyTypeAwareListenerOtherSessionStart()
		{
			this.Dispose();
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0000686C File Offset: 0x00004A6C
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerOtherSessionStart(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x000068F4 File Offset: 0x00004AF4
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerOtherSessionStart_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000058 RID: 88
		private HandleRef swigCPtr;
	}
}
