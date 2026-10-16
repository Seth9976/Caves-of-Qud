using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000D5 RID: 213
	public abstract class IConnectionOpenListener : GalaxyTypeAwareListenerConnectionOpen
	{
		// Token: 0x06000951 RID: 2385 RVA: 0x0000DCEC File Offset: 0x0000BEEC
		internal IConnectionOpenListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IConnectionOpenListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IConnectionOpenListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x0000DD14 File Offset: 0x0000BF14
		public IConnectionOpenListener()
			: this(GalaxyInstancePINVOKE.new_IConnectionOpenListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x0000DD38 File Offset: 0x0000BF38
		internal static HandleRef getCPtr(IConnectionOpenListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x0000DD58 File Offset: 0x0000BF58
		~IConnectionOpenListener()
		{
			this.Dispose();
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x0000DD88 File Offset: 0x0000BF88
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IConnectionOpenListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IConnectionOpenListener.listeners.ContainsKey(handle))
					{
						IConnectionOpenListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000956 RID: 2390
		public abstract void OnConnectionOpenSuccess(string connectionString, ulong connectionID);

		// Token: 0x06000957 RID: 2391
		public abstract void OnConnectionOpenFailure(string connectionString, IConnectionOpenListener.FailureReason failureReason);

		// Token: 0x06000958 RID: 2392 RVA: 0x0000DE38 File Offset: 0x0000C038
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnConnectionOpenSuccess", IConnectionOpenListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IConnectionOpenListener.SwigDelegateIConnectionOpenListener_0(IConnectionOpenListener.SwigDirectorOnConnectionOpenSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnConnectionOpenFailure", IConnectionOpenListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new IConnectionOpenListener.SwigDelegateIConnectionOpenListener_1(IConnectionOpenListener.SwigDirectorOnConnectionOpenFailure);
			}
			GalaxyInstancePINVOKE.IConnectionOpenListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x0000DEAC File Offset: 0x0000C0AC
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IConnectionOpenListener));
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x0000DEE2 File Offset: 0x0000C0E2
		[MonoPInvokeCallback(typeof(IConnectionOpenListener.SwigDelegateIConnectionOpenListener_0))]
		private static void SwigDirectorOnConnectionOpenSuccess(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string connectionString, ulong connectionID)
		{
			if (IConnectionOpenListener.listeners.ContainsKey(cPtr))
			{
				IConnectionOpenListener.listeners[cPtr].OnConnectionOpenSuccess(connectionString, connectionID);
			}
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x0000DF06 File Offset: 0x0000C106
		[MonoPInvokeCallback(typeof(IConnectionOpenListener.SwigDelegateIConnectionOpenListener_1))]
		private static void SwigDirectorOnConnectionOpenFailure(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string connectionString, int failureReason)
		{
			if (IConnectionOpenListener.listeners.ContainsKey(cPtr))
			{
				IConnectionOpenListener.listeners[cPtr].OnConnectionOpenFailure(connectionString, (IConnectionOpenListener.FailureReason)failureReason);
			}
		}

		// Token: 0x04000152 RID: 338
		private static Dictionary<IntPtr, IConnectionOpenListener> listeners = new Dictionary<IntPtr, IConnectionOpenListener>();

		// Token: 0x04000153 RID: 339
		private HandleRef swigCPtr;

		// Token: 0x04000154 RID: 340
		private IConnectionOpenListener.SwigDelegateIConnectionOpenListener_0 swigDelegate0;

		// Token: 0x04000155 RID: 341
		private IConnectionOpenListener.SwigDelegateIConnectionOpenListener_1 swigDelegate1;

		// Token: 0x04000156 RID: 342
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(string),
			typeof(ulong)
		};

		// Token: 0x04000157 RID: 343
		private static Type[] swigMethodTypes1 = new Type[]
		{
			typeof(string),
			typeof(IConnectionOpenListener.FailureReason)
		};

		// Token: 0x020000D6 RID: 214
		// (Invoke) Token: 0x0600095E RID: 2398
		public delegate void SwigDelegateIConnectionOpenListener_0(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string connectionString, ulong connectionID);

		// Token: 0x020000D7 RID: 215
		// (Invoke) Token: 0x06000962 RID: 2402
		public delegate void SwigDelegateIConnectionOpenListener_1(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string connectionString, int failureReason);

		// Token: 0x020000D8 RID: 216
		public enum FailureReason
		{
			// Token: 0x04000159 RID: 345
			FAILURE_REASON_UNDEFINED,
			// Token: 0x0400015A RID: 346
			FAILURE_REASON_CONNECTION_FAILURE,
			// Token: 0x0400015B RID: 347
			FAILURE_REASON_UNAUTHORIZED
		}
	}
}
