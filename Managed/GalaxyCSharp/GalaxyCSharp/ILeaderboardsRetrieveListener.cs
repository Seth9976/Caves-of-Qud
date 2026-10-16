using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000113 RID: 275
	public abstract class ILeaderboardsRetrieveListener : GalaxyTypeAwareListenerLeaderboardsRetrieve
	{
		// Token: 0x06000AD7 RID: 2775 RVA: 0x000113CC File Offset: 0x0000F5CC
		internal ILeaderboardsRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ILeaderboardsRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			ILeaderboardsRetrieveListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x000113F4 File Offset: 0x0000F5F4
		public ILeaderboardsRetrieveListener()
			: this(GalaxyInstancePINVOKE.new_ILeaderboardsRetrieveListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x00011418 File Offset: 0x0000F618
		internal static HandleRef getCPtr(ILeaderboardsRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x00011438 File Offset: 0x0000F638
		~ILeaderboardsRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x00011468 File Offset: 0x0000F668
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ILeaderboardsRetrieveListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (ILeaderboardsRetrieveListener.listeners.ContainsKey(handle))
					{
						ILeaderboardsRetrieveListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000ADC RID: 2780
		public abstract void OnLeaderboardsRetrieveSuccess();

		// Token: 0x06000ADD RID: 2781
		public abstract void OnLeaderboardsRetrieveFailure(ILeaderboardsRetrieveListener.FailureReason failureReason);

		// Token: 0x06000ADE RID: 2782 RVA: 0x00011518 File Offset: 0x0000F718
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnLeaderboardsRetrieveSuccess", ILeaderboardsRetrieveListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new ILeaderboardsRetrieveListener.SwigDelegateILeaderboardsRetrieveListener_0(ILeaderboardsRetrieveListener.SwigDirectorOnLeaderboardsRetrieveSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnLeaderboardsRetrieveFailure", ILeaderboardsRetrieveListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new ILeaderboardsRetrieveListener.SwigDelegateILeaderboardsRetrieveListener_1(ILeaderboardsRetrieveListener.SwigDirectorOnLeaderboardsRetrieveFailure);
			}
			GalaxyInstancePINVOKE.ILeaderboardsRetrieveListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x0001158C File Offset: 0x0000F78C
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ILeaderboardsRetrieveListener));
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x000115C2 File Offset: 0x0000F7C2
		[MonoPInvokeCallback(typeof(ILeaderboardsRetrieveListener.SwigDelegateILeaderboardsRetrieveListener_0))]
		private static void SwigDirectorOnLeaderboardsRetrieveSuccess(IntPtr cPtr)
		{
			if (ILeaderboardsRetrieveListener.listeners.ContainsKey(cPtr))
			{
				ILeaderboardsRetrieveListener.listeners[cPtr].OnLeaderboardsRetrieveSuccess();
			}
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x000115E4 File Offset: 0x0000F7E4
		[MonoPInvokeCallback(typeof(ILeaderboardsRetrieveListener.SwigDelegateILeaderboardsRetrieveListener_1))]
		private static void SwigDirectorOnLeaderboardsRetrieveFailure(IntPtr cPtr, int failureReason)
		{
			if (ILeaderboardsRetrieveListener.listeners.ContainsKey(cPtr))
			{
				ILeaderboardsRetrieveListener.listeners[cPtr].OnLeaderboardsRetrieveFailure((ILeaderboardsRetrieveListener.FailureReason)failureReason);
			}
		}

		// Token: 0x040001E5 RID: 485
		private static Dictionary<IntPtr, ILeaderboardsRetrieveListener> listeners = new Dictionary<IntPtr, ILeaderboardsRetrieveListener>();

		// Token: 0x040001E6 RID: 486
		private HandleRef swigCPtr;

		// Token: 0x040001E7 RID: 487
		private ILeaderboardsRetrieveListener.SwigDelegateILeaderboardsRetrieveListener_0 swigDelegate0;

		// Token: 0x040001E8 RID: 488
		private ILeaderboardsRetrieveListener.SwigDelegateILeaderboardsRetrieveListener_1 swigDelegate1;

		// Token: 0x040001E9 RID: 489
		private static Type[] swigMethodTypes0 = new Type[0];

		// Token: 0x040001EA RID: 490
		private static Type[] swigMethodTypes1 = new Type[] { typeof(ILeaderboardsRetrieveListener.FailureReason) };

		// Token: 0x02000114 RID: 276
		// (Invoke) Token: 0x06000AE4 RID: 2788
		public delegate void SwigDelegateILeaderboardsRetrieveListener_0(IntPtr cPtr);

		// Token: 0x02000115 RID: 277
		// (Invoke) Token: 0x06000AE8 RID: 2792
		public delegate void SwigDelegateILeaderboardsRetrieveListener_1(IntPtr cPtr, int failureReason);

		// Token: 0x02000116 RID: 278
		public enum FailureReason
		{
			// Token: 0x040001EC RID: 492
			FAILURE_REASON_UNDEFINED,
			// Token: 0x040001ED RID: 493
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
