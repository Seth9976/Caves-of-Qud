using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000041 RID: 65
	public abstract class GalaxyTypeAwareListenerNetworking : IGalaxyListener
	{
		// Token: 0x0600061F RID: 1567 RVA: 0x00006448 File Offset: 0x00004648
		internal GalaxyTypeAwareListenerNetworking(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerNetworking_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00006464 File Offset: 0x00004664
		public GalaxyTypeAwareListenerNetworking()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerNetworking(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00006482 File Offset: 0x00004682
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerNetworking obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x000064A0 File Offset: 0x000046A0
		~GalaxyTypeAwareListenerNetworking()
		{
			this.Dispose();
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x000064D0 File Offset: 0x000046D0
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerNetworking(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00006558 File Offset: 0x00004758
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerNetworking_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000055 RID: 85
		private HandleRef swigCPtr;
	}
}
