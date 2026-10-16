using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200004D RID: 77
	public abstract class GalaxyTypeAwareListenerSharedFileDownload : IGalaxyListener
	{
		// Token: 0x06000667 RID: 1639 RVA: 0x000072B8 File Offset: 0x000054B8
		internal GalaxyTypeAwareListenerSharedFileDownload(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerSharedFileDownload_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x000072D4 File Offset: 0x000054D4
		public GalaxyTypeAwareListenerSharedFileDownload()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerSharedFileDownload(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x000072F2 File Offset: 0x000054F2
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerSharedFileDownload obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x00007310 File Offset: 0x00005510
		~GalaxyTypeAwareListenerSharedFileDownload()
		{
			this.Dispose();
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x00007340 File Offset: 0x00005540
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerSharedFileDownload(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x000073C8 File Offset: 0x000055C8
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerSharedFileDownload_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000061 RID: 97
		private HandleRef swigCPtr;
	}
}
