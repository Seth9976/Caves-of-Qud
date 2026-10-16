using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000052 RID: 82
	public abstract class GalaxyTypeAwareListenerUserFind : IGalaxyListener
	{
		// Token: 0x06000685 RID: 1669 RVA: 0x000078BC File Offset: 0x00005ABC
		internal GalaxyTypeAwareListenerUserFind(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerUserFind_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x000078D8 File Offset: 0x00005AD8
		public GalaxyTypeAwareListenerUserFind()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerUserFind(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x000078F6 File Offset: 0x00005AF6
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerUserFind obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x00007914 File Offset: 0x00005B14
		~GalaxyTypeAwareListenerUserFind()
		{
			this.Dispose();
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x00007944 File Offset: 0x00005B44
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerUserFind(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x000079CC File Offset: 0x00005BCC
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerUserFind_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000066 RID: 102
		private HandleRef swigCPtr;
	}
}
