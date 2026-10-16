using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000156 RID: 342
	public class IRuntimeError : IError
	{
		// Token: 0x06000CAF RID: 3247 RVA: 0x0001A466 File Offset: 0x00018666
		internal IRuntimeError(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IRuntimeError_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x0001A482 File Offset: 0x00018682
		internal static HandleRef getCPtr(IRuntimeError obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x0001A4A0 File Offset: 0x000186A0
		~IRuntimeError()
		{
			this.Dispose();
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x0001A4D0 File Offset: 0x000186D0
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IRuntimeError(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0400027E RID: 638
		private HandleRef swigCPtr;
	}
}
