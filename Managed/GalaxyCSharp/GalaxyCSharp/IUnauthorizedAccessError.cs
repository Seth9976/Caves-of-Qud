using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000170 RID: 368
	public class IUnauthorizedAccessError : IError
	{
		// Token: 0x06000D90 RID: 3472 RVA: 0x0001B7AD File Offset: 0x000199AD
		internal IUnauthorizedAccessError(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IUnauthorizedAccessError_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x0001B7C9 File Offset: 0x000199C9
		internal static HandleRef getCPtr(IUnauthorizedAccessError obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x0001B7E8 File Offset: 0x000199E8
		~IUnauthorizedAccessError()
		{
			this.Dispose();
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x0001B818 File Offset: 0x00019A18
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IUnauthorizedAccessError(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040002C4 RID: 708
		private HandleRef swigCPtr;
	}
}
