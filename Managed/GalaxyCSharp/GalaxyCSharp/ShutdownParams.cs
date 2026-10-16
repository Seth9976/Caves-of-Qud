using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000195 RID: 405
	public class ShutdownParams : IDisposable
	{
		// Token: 0x06000E40 RID: 3648 RVA: 0x0001C5BD File Offset: 0x0001A7BD
		internal ShutdownParams(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x0001C5D9 File Offset: 0x0001A7D9
		public ShutdownParams(bool _preserveStaticObjects)
			: this(GalaxyInstancePINVOKE.new_ShutdownParams(_preserveStaticObjects), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x0001C5F8 File Offset: 0x0001A7F8
		internal static HandleRef getCPtr(ShutdownParams obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x0001C618 File Offset: 0x0001A818
		~ShutdownParams()
		{
			this.Dispose();
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x0001C648 File Offset: 0x0001A848
		public virtual void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ShutdownParams(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000E46 RID: 3654 RVA: 0x0001C6E8 File Offset: 0x0001A8E8
		// (set) Token: 0x06000E45 RID: 3653 RVA: 0x0001C6C8 File Offset: 0x0001A8C8
		public bool preserveStaticObjects
		{
			get
			{
				bool flag = GalaxyInstancePINVOKE.ShutdownParams_preserveStaticObjects_get(this.swigCPtr);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
				return flag;
			}
			set
			{
				GalaxyInstancePINVOKE.ShutdownParams_preserveStaticObjects_set(this.swigCPtr, value);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
			}
		}

		// Token: 0x04000381 RID: 897
		private HandleRef swigCPtr;

		// Token: 0x04000382 RID: 898
		protected bool swigCMemOwn;
	}
}
