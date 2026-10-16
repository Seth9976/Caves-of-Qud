using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200016C RID: 364
	public abstract class ITelemetryEventSendListener : GalaxyTypeAwareListenerTelemetryEventSend
	{
		// Token: 0x06000D7C RID: 3452 RVA: 0x0000B9D0 File Offset: 0x00009BD0
		internal ITelemetryEventSendListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ITelemetryEventSendListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			ITelemetryEventSendListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x0000B9F8 File Offset: 0x00009BF8
		public ITelemetryEventSendListener()
			: this(GalaxyInstancePINVOKE.new_ITelemetryEventSendListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x0000BA1C File Offset: 0x00009C1C
		internal static HandleRef getCPtr(ITelemetryEventSendListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x0000BA3C File Offset: 0x00009C3C
		~ITelemetryEventSendListener()
		{
			this.Dispose();
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x0000BA6C File Offset: 0x00009C6C
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ITelemetryEventSendListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (ITelemetryEventSendListener.listeners.ContainsKey(handle))
					{
						ITelemetryEventSendListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000D81 RID: 3457
		public abstract void OnTelemetryEventSendSuccess(string eventType, uint sentEventIndex);

		// Token: 0x06000D82 RID: 3458
		public abstract void OnTelemetryEventSendFailure(string eventType, uint sentEventIndex, ITelemetryEventSendListener.FailureReason failureReason);

		// Token: 0x06000D83 RID: 3459 RVA: 0x0000BB1C File Offset: 0x00009D1C
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnTelemetryEventSendSuccess", ITelemetryEventSendListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new ITelemetryEventSendListener.SwigDelegateITelemetryEventSendListener_0(ITelemetryEventSendListener.SwigDirectorOnTelemetryEventSendSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnTelemetryEventSendFailure", ITelemetryEventSendListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new ITelemetryEventSendListener.SwigDelegateITelemetryEventSendListener_1(ITelemetryEventSendListener.SwigDirectorOnTelemetryEventSendFailure);
			}
			GalaxyInstancePINVOKE.ITelemetryEventSendListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x0000BB90 File Offset: 0x00009D90
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ITelemetryEventSendListener));
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x0000BBC6 File Offset: 0x00009DC6
		[MonoPInvokeCallback(typeof(ITelemetryEventSendListener.SwigDelegateITelemetryEventSendListener_0))]
		private static void SwigDirectorOnTelemetryEventSendSuccess(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string eventType, uint sentEventIndex)
		{
			if (ITelemetryEventSendListener.listeners.ContainsKey(cPtr))
			{
				ITelemetryEventSendListener.listeners[cPtr].OnTelemetryEventSendSuccess(eventType, sentEventIndex);
			}
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x0000BBEA File Offset: 0x00009DEA
		[MonoPInvokeCallback(typeof(ITelemetryEventSendListener.SwigDelegateITelemetryEventSendListener_1))]
		private static void SwigDirectorOnTelemetryEventSendFailure(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string eventType, uint sentEventIndex, int failureReason)
		{
			if (ITelemetryEventSendListener.listeners.ContainsKey(cPtr))
			{
				ITelemetryEventSendListener.listeners[cPtr].OnTelemetryEventSendFailure(eventType, sentEventIndex, (ITelemetryEventSendListener.FailureReason)failureReason);
			}
		}

		// Token: 0x040002B4 RID: 692
		private static Dictionary<IntPtr, ITelemetryEventSendListener> listeners = new Dictionary<IntPtr, ITelemetryEventSendListener>();

		// Token: 0x040002B5 RID: 693
		private HandleRef swigCPtr;

		// Token: 0x040002B6 RID: 694
		private ITelemetryEventSendListener.SwigDelegateITelemetryEventSendListener_0 swigDelegate0;

		// Token: 0x040002B7 RID: 695
		private ITelemetryEventSendListener.SwigDelegateITelemetryEventSendListener_1 swigDelegate1;

		// Token: 0x040002B8 RID: 696
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(string),
			typeof(uint)
		};

		// Token: 0x040002B9 RID: 697
		private static Type[] swigMethodTypes1 = new Type[]
		{
			typeof(string),
			typeof(uint),
			typeof(ITelemetryEventSendListener.FailureReason)
		};

		// Token: 0x0200016D RID: 365
		// (Invoke) Token: 0x06000D89 RID: 3465
		public delegate void SwigDelegateITelemetryEventSendListener_0(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string eventType, uint sentEventIndex);

		// Token: 0x0200016E RID: 366
		// (Invoke) Token: 0x06000D8D RID: 3469
		public delegate void SwigDelegateITelemetryEventSendListener_1(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string eventType, uint sentEventIndex, int failureReason);

		// Token: 0x0200016F RID: 367
		public enum FailureReason
		{
			// Token: 0x040002BB RID: 699
			FAILURE_REASON_UNDEFINED,
			// Token: 0x040002BC RID: 700
			FAILURE_REASON_CLIENT_FORBIDDEN,
			// Token: 0x040002BD RID: 701
			FAILURE_REASON_INVALID_DATA,
			// Token: 0x040002BE RID: 702
			FAILURE_REASON_CONNECTION_FAILURE,
			// Token: 0x040002BF RID: 703
			FAILURE_REASON_NO_SAMPLING_CLASS_IN_CONFIG,
			// Token: 0x040002C0 RID: 704
			FAILURE_REASON_SAMPLING_CLASS_FIELD_MISSING,
			// Token: 0x040002C1 RID: 705
			FAILURE_REASON_EVENT_SAMPLED_OUT,
			// Token: 0x040002C2 RID: 706
			FAILURE_REASON_SAMPLING_RESULT_ALREADY_EXIST,
			// Token: 0x040002C3 RID: 707
			FAILURE_REASON_SAMPLING_INVALID_RESULT_PATH
		}
	}
}
