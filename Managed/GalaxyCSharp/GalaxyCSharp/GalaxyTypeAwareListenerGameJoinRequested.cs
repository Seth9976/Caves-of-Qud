using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200002F RID: 47
	public abstract class GalaxyTypeAwareListenerGameJoinRequested : IGalaxyListener
	{
		// Token: 0x060005B3 RID: 1459 RVA: 0x00004EA0 File Offset: 0x000030A0
		internal GalaxyTypeAwareListenerGameJoinRequested(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerGameJoinRequested_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00004EBC File Offset: 0x000030BC
		public GalaxyTypeAwareListenerGameJoinRequested()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerGameJoinRequested(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00004EDA File Offset: 0x000030DA
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerGameJoinRequested obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00004EF8 File Offset: 0x000030F8
		~GalaxyTypeAwareListenerGameJoinRequested()
		{
			this.Dispose();
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00004F28 File Offset: 0x00003128
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerGameJoinRequested(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00004FB0 File Offset: 0x000031B0
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerGameJoinRequested_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000043 RID: 67
		private HandleRef swigCPtr;
	}
}
