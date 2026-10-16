using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000F9 RID: 249
	public abstract class IFriendListListener : GalaxyTypeAwareListenerFriendList
	{
		// Token: 0x06000A0E RID: 2574 RVA: 0x0000FCA0 File Offset: 0x0000DEA0
		internal IFriendListListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IFriendListListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IFriendListListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x0000FCC8 File Offset: 0x0000DEC8
		public IFriendListListener()
			: this(GalaxyInstancePINVOKE.new_IFriendListListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x0000FCEC File Offset: 0x0000DEEC
		internal static HandleRef getCPtr(IFriendListListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x0000FD0C File Offset: 0x0000DF0C
		~IFriendListListener()
		{
			this.Dispose();
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0000FD3C File Offset: 0x0000DF3C
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IFriendListListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IFriendListListener.listeners.ContainsKey(handle))
					{
						IFriendListListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000A13 RID: 2579
		public abstract void OnFriendListRetrieveSuccess();

		// Token: 0x06000A14 RID: 2580
		public abstract void OnFriendListRetrieveFailure(IFriendListListener.FailureReason failureReason);

		// Token: 0x06000A15 RID: 2581 RVA: 0x0000FDEC File Offset: 0x0000DFEC
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnFriendListRetrieveSuccess", IFriendListListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IFriendListListener.SwigDelegateIFriendListListener_0(IFriendListListener.SwigDirectorOnFriendListRetrieveSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnFriendListRetrieveFailure", IFriendListListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new IFriendListListener.SwigDelegateIFriendListListener_1(IFriendListListener.SwigDirectorOnFriendListRetrieveFailure);
			}
			GalaxyInstancePINVOKE.IFriendListListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x0000FE60 File Offset: 0x0000E060
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IFriendListListener));
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x0000FE96 File Offset: 0x0000E096
		[MonoPInvokeCallback(typeof(IFriendListListener.SwigDelegateIFriendListListener_0))]
		private static void SwigDirectorOnFriendListRetrieveSuccess(IntPtr cPtr)
		{
			if (IFriendListListener.listeners.ContainsKey(cPtr))
			{
				IFriendListListener.listeners[cPtr].OnFriendListRetrieveSuccess();
			}
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x0000FEB8 File Offset: 0x0000E0B8
		[MonoPInvokeCallback(typeof(IFriendListListener.SwigDelegateIFriendListListener_1))]
		private static void SwigDirectorOnFriendListRetrieveFailure(IntPtr cPtr, int failureReason)
		{
			if (IFriendListListener.listeners.ContainsKey(cPtr))
			{
				IFriendListListener.listeners[cPtr].OnFriendListRetrieveFailure((IFriendListListener.FailureReason)failureReason);
			}
		}

		// Token: 0x040001AC RID: 428
		private static Dictionary<IntPtr, IFriendListListener> listeners = new Dictionary<IntPtr, IFriendListListener>();

		// Token: 0x040001AD RID: 429
		private HandleRef swigCPtr;

		// Token: 0x040001AE RID: 430
		private IFriendListListener.SwigDelegateIFriendListListener_0 swigDelegate0;

		// Token: 0x040001AF RID: 431
		private IFriendListListener.SwigDelegateIFriendListListener_1 swigDelegate1;

		// Token: 0x040001B0 RID: 432
		private static Type[] swigMethodTypes0 = new Type[0];

		// Token: 0x040001B1 RID: 433
		private static Type[] swigMethodTypes1 = new Type[] { typeof(IFriendListListener.FailureReason) };

		// Token: 0x020000FA RID: 250
		// (Invoke) Token: 0x06000A1B RID: 2587
		public delegate void SwigDelegateIFriendListListener_0(IntPtr cPtr);

		// Token: 0x020000FB RID: 251
		// (Invoke) Token: 0x06000A1F RID: 2591
		public delegate void SwigDelegateIFriendListListener_1(IntPtr cPtr, int failureReason);

		// Token: 0x020000FC RID: 252
		public enum FailureReason
		{
			// Token: 0x040001B3 RID: 435
			FAILURE_REASON_UNDEFINED,
			// Token: 0x040001B4 RID: 436
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
