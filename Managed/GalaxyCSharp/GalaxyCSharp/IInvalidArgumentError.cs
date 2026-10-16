using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000105 RID: 261
	public class IInvalidArgumentError : IError
	{
		// Token: 0x06000A93 RID: 2707 RVA: 0x00018F08 File Offset: 0x00017108
		internal IInvalidArgumentError(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IInvalidArgumentError_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x00018F24 File Offset: 0x00017124
		internal static HandleRef getCPtr(IInvalidArgumentError obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x00018F44 File Offset: 0x00017144
		~IInvalidArgumentError()
		{
			this.Dispose();
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x00018F74 File Offset: 0x00017174
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IInvalidArgumentError(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040001C6 RID: 454
		private HandleRef swigCPtr;
	}
}
