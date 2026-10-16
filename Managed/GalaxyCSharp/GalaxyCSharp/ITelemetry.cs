using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Galaxy.Api
{
	// Token: 0x0200016B RID: 363
	public class ITelemetry : IDisposable
	{
		// Token: 0x06000D68 RID: 3432 RVA: 0x0001B44F File Offset: 0x0001964F
		internal ITelemetry(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x0001B46B File Offset: 0x0001966B
		internal static HandleRef getCPtr(ITelemetry obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x0001B48C File Offset: 0x0001968C
		~ITelemetry()
		{
			this.Dispose();
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x0001B4BC File Offset: 0x000196BC
		public virtual void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ITelemetry(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x0001B53C File Offset: 0x0001973C
		public virtual void AddStringParam(string name, string value)
		{
			GalaxyInstancePINVOKE.ITelemetry_AddStringParam(this.swigCPtr, name, value);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x0001B55B File Offset: 0x0001975B
		public virtual void AddIntParam(string name, int value)
		{
			GalaxyInstancePINVOKE.ITelemetry_AddIntParam(this.swigCPtr, name, value);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x0001B57A File Offset: 0x0001977A
		public virtual void AddFloatParam(string name, double value)
		{
			GalaxyInstancePINVOKE.ITelemetry_AddFloatParam(this.swigCPtr, name, value);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x0001B599 File Offset: 0x00019799
		public virtual void AddBoolParam(string name, bool value)
		{
			GalaxyInstancePINVOKE.ITelemetry_AddBoolParam(this.swigCPtr, name, value);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x0001B5B8 File Offset: 0x000197B8
		public virtual void AddObjectParam(string name)
		{
			GalaxyInstancePINVOKE.ITelemetry_AddObjectParam(this.swigCPtr, name);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x0001B5D6 File Offset: 0x000197D6
		public virtual void AddArrayParam(string name)
		{
			GalaxyInstancePINVOKE.ITelemetry_AddArrayParam(this.swigCPtr, name);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x0001B5F4 File Offset: 0x000197F4
		public virtual void CloseParam()
		{
			GalaxyInstancePINVOKE.ITelemetry_CloseParam(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x0001B611 File Offset: 0x00019811
		public virtual void ClearParams()
		{
			GalaxyInstancePINVOKE.ITelemetry_ClearParams(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x0001B62E File Offset: 0x0001982E
		public virtual void SetSamplingClass(string name)
		{
			GalaxyInstancePINVOKE.ITelemetry_SetSamplingClass(this.swigCPtr, name);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x0001B64C File Offset: 0x0001984C
		public virtual uint SendTelemetryEvent(string eventType, ITelemetryEventSendListener listener)
		{
			uint num = GalaxyInstancePINVOKE.ITelemetry_SendTelemetryEvent__SWIG_0(this.swigCPtr, eventType, ITelemetryEventSendListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x0001B680 File Offset: 0x00019880
		public virtual uint SendTelemetryEvent(string eventType)
		{
			uint num = GalaxyInstancePINVOKE.ITelemetry_SendTelemetryEvent__SWIG_1(this.swigCPtr, eventType);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x0001B6AC File Offset: 0x000198AC
		public virtual uint SendAnonymousTelemetryEvent(string eventType, ITelemetryEventSendListener listener)
		{
			uint num = GalaxyInstancePINVOKE.ITelemetry_SendAnonymousTelemetryEvent__SWIG_0(this.swigCPtr, eventType, ITelemetryEventSendListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x0001B6E0 File Offset: 0x000198E0
		public virtual uint SendAnonymousTelemetryEvent(string eventType)
		{
			uint num = GalaxyInstancePINVOKE.ITelemetry_SendAnonymousTelemetryEvent__SWIG_1(this.swigCPtr, eventType);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x0001B70C File Offset: 0x0001990C
		public virtual string GetVisitID()
		{
			string text = GalaxyInstancePINVOKE.ITelemetry_GetVisitID(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x0001B738 File Offset: 0x00019938
		public virtual void GetVisitIDCopy(out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.ITelemetry_GetVisitIDCopy(this.swigCPtr, array, bufferLength);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
			}
			finally
			{
				buffer = Encoding.UTF8.GetString(array);
			}
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x0001B790 File Offset: 0x00019990
		public virtual void ResetVisitID()
		{
			GalaxyInstancePINVOKE.ITelemetry_ResetVisitID(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x040002B2 RID: 690
		private HandleRef swigCPtr;

		// Token: 0x040002B3 RID: 691
		protected bool swigCMemOwn;
	}
}
