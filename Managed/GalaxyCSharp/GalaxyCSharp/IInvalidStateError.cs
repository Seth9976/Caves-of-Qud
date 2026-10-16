using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000106 RID: 262
	public class IInvalidStateError : IError
	{
		// Token: 0x06000A97 RID: 2711 RVA: 0x00018FFC File Offset: 0x000171FC
		internal IInvalidStateError(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IInvalidStateError_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x00019018 File Offset: 0x00017218
		internal static HandleRef getCPtr(IInvalidStateError obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x00019038 File Offset: 0x00017238
		~IInvalidStateError()
		{
			this.Dispose();
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x00019068 File Offset: 0x00017268
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IInvalidStateError(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040001C7 RID: 455
		private HandleRef swigCPtr;
	}
}
