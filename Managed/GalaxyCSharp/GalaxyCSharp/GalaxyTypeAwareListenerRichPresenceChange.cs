using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000049 RID: 73
	public abstract class GalaxyTypeAwareListenerRichPresenceChange : IGalaxyListener
	{
		// Token: 0x0600064F RID: 1615 RVA: 0x00006DE8 File Offset: 0x00004FE8
		internal GalaxyTypeAwareListenerRichPresenceChange(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerRichPresenceChange_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x00006E04 File Offset: 0x00005004
		public GalaxyTypeAwareListenerRichPresenceChange()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerRichPresenceChange(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00006E22 File Offset: 0x00005022
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerRichPresenceChange obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00006E40 File Offset: 0x00005040
		~GalaxyTypeAwareListenerRichPresenceChange()
		{
			this.Dispose();
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x00006E70 File Offset: 0x00005070
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerRichPresenceChange(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x00006EF8 File Offset: 0x000050F8
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerRichPresenceChange_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x0400005D RID: 93
		private HandleRef swigCPtr;
	}
}
