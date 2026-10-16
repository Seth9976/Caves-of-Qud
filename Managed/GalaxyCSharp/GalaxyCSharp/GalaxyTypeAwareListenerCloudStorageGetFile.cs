using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200001F RID: 31
	public abstract class GalaxyTypeAwareListenerCloudStorageGetFile : IGalaxyListener
	{
		// Token: 0x06000553 RID: 1363 RVA: 0x00003B60 File Offset: 0x00001D60
		internal GalaxyTypeAwareListenerCloudStorageGetFile(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerCloudStorageGetFile_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00003B7C File Offset: 0x00001D7C
		public GalaxyTypeAwareListenerCloudStorageGetFile()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerCloudStorageGetFile(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00003B9A File Offset: 0x00001D9A
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerCloudStorageGetFile obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00003BB8 File Offset: 0x00001DB8
		~GalaxyTypeAwareListenerCloudStorageGetFile()
		{
			this.Dispose();
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerCloudStorageGetFile(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00003C70 File Offset: 0x00001E70
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerCloudStorageGetFile_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000033 RID: 51
		private HandleRef swigCPtr;
	}
}
