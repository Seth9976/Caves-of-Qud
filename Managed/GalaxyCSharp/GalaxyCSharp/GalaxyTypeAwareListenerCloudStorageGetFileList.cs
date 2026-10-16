using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000020 RID: 32
	public abstract class GalaxyTypeAwareListenerCloudStorageGetFileList : IGalaxyListener
	{
		// Token: 0x06000559 RID: 1369 RVA: 0x00003C94 File Offset: 0x00001E94
		internal GalaxyTypeAwareListenerCloudStorageGetFileList(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerCloudStorageGetFileList_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00003CB0 File Offset: 0x00001EB0
		public GalaxyTypeAwareListenerCloudStorageGetFileList()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerCloudStorageGetFileList(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00003CCE File Offset: 0x00001ECE
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerCloudStorageGetFileList obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00003CEC File Offset: 0x00001EEC
		~GalaxyTypeAwareListenerCloudStorageGetFileList()
		{
			this.Dispose();
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00003D1C File Offset: 0x00001F1C
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerCloudStorageGetFileList(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00003DA4 File Offset: 0x00001FA4
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerCloudStorageGetFileList_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000034 RID: 52
		private HandleRef swigCPtr;
	}
}
