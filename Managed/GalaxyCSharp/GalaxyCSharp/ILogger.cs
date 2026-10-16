using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000135 RID: 309
	public class ILogger : IDisposable
	{
		// Token: 0x06000B9D RID: 2973 RVA: 0x00019224 File Offset: 0x00017424
		internal ILogger(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x00019240 File Offset: 0x00017440
		internal static HandleRef getCPtr(ILogger obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x00019260 File Offset: 0x00017460
		~ILogger()
		{
			this.Dispose();
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x00019290 File Offset: 0x00017490
		public virtual void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ILogger(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x00019310 File Offset: 0x00017510
		public virtual void Trace(string format)
		{
			GalaxyInstancePINVOKE.ILogger_Trace(this.swigCPtr, format);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x0001932E File Offset: 0x0001752E
		public virtual void Debug(string format)
		{
			GalaxyInstancePINVOKE.ILogger_Debug(this.swigCPtr, format);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x0001934C File Offset: 0x0001754C
		public virtual void Info(string format)
		{
			GalaxyInstancePINVOKE.ILogger_Info(this.swigCPtr, format);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x0001936A File Offset: 0x0001756A
		public virtual void Warning(string format)
		{
			GalaxyInstancePINVOKE.ILogger_Warning(this.swigCPtr, format);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x00019388 File Offset: 0x00017588
		public virtual void Error(string format)
		{
			GalaxyInstancePINVOKE.ILogger_Error(this.swigCPtr, format);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x000193A6 File Offset: 0x000175A6
		public virtual void Fatal(string format)
		{
			GalaxyInstancePINVOKE.ILogger_Fatal(this.swigCPtr, format);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x04000233 RID: 563
		private HandleRef swigCPtr;

		// Token: 0x04000234 RID: 564
		protected bool swigCMemOwn;
	}
}
