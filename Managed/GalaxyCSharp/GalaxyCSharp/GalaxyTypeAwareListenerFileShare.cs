using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000026 RID: 38
	public abstract class GalaxyTypeAwareListenerFileShare : IGalaxyListener
	{
		// Token: 0x0600057D RID: 1405 RVA: 0x000043CC File Offset: 0x000025CC
		internal GalaxyTypeAwareListenerFileShare(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerFileShare_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x000043E8 File Offset: 0x000025E8
		public GalaxyTypeAwareListenerFileShare()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerFileShare(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00004406 File Offset: 0x00002606
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerFileShare obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00004424 File Offset: 0x00002624
		~GalaxyTypeAwareListenerFileShare()
		{
			this.Dispose();
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00004454 File Offset: 0x00002654
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerFileShare(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x000044DC File Offset: 0x000026DC
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerFileShare_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x0400003A RID: 58
		private HandleRef swigCPtr;
	}
}
