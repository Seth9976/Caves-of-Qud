using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000DE RID: 222
	public class IError : IDisposable
	{
		// Token: 0x06000986 RID: 2438 RVA: 0x00018176 File Offset: 0x00016376
		internal IError(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x00018192 File Offset: 0x00016392
		internal static HandleRef getCPtr(IError obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x000181B0 File Offset: 0x000163B0
		~IError()
		{
			this.Dispose();
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x000181E0 File Offset: 0x000163E0
		public virtual void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IError(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x00018260 File Offset: 0x00016460
		public override string ToString()
		{
			return string.Format("{0}: {1} ({2})", this.GetName(), this.GetMsg(), this.GetErrorType().ToString());
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x00018298 File Offset: 0x00016498
		public virtual string GetName()
		{
			string text = GalaxyInstancePINVOKE.IError_GetName(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x000182C4 File Offset: 0x000164C4
		public virtual string GetMsg()
		{
			string text = GalaxyInstancePINVOKE.IError_GetMsg(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x000182F0 File Offset: 0x000164F0
		public virtual IError.Type GetErrorType()
		{
			IError.Type type = (IError.Type)GalaxyInstancePINVOKE.IError_GetErrorType(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return type;
		}

		// Token: 0x04000167 RID: 359
		private HandleRef swigCPtr;

		// Token: 0x04000168 RID: 360
		protected bool swigCMemOwn;

		// Token: 0x020000DF RID: 223
		public enum Type
		{
			// Token: 0x0400016A RID: 362
			UNAUTHORIZED_ACCESS,
			// Token: 0x0400016B RID: 363
			INVALID_ARGUMENT,
			// Token: 0x0400016C RID: 364
			INVALID_STATE,
			// Token: 0x0400016D RID: 365
			RUNTIME_ERROR
		}
	}
}
