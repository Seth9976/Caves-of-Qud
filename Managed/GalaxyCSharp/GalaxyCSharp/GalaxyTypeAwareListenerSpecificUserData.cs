using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200004E RID: 78
	public abstract class GalaxyTypeAwareListenerSpecificUserData : IGalaxyListener
	{
		// Token: 0x0600066D RID: 1645 RVA: 0x000073EC File Offset: 0x000055EC
		internal GalaxyTypeAwareListenerSpecificUserData(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerSpecificUserData_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x00007408 File Offset: 0x00005608
		public GalaxyTypeAwareListenerSpecificUserData()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerSpecificUserData(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00007426 File Offset: 0x00005626
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerSpecificUserData obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x00007444 File Offset: 0x00005644
		~GalaxyTypeAwareListenerSpecificUserData()
		{
			this.Dispose();
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x00007474 File Offset: 0x00005674
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerSpecificUserData(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x000074FC File Offset: 0x000056FC
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerSpecificUserData_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000062 RID: 98
		private HandleRef swigCPtr;
	}
}
