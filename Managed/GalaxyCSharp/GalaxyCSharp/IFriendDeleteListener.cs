using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000E7 RID: 231
	public abstract class IFriendDeleteListener : GalaxyTypeAwareListenerFriendDelete
	{
		// Token: 0x060009B0 RID: 2480 RVA: 0x0000E908 File Offset: 0x0000CB08
		internal IFriendDeleteListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IFriendDeleteListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IFriendDeleteListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x0000E930 File Offset: 0x0000CB30
		public IFriendDeleteListener()
			: this(GalaxyInstancePINVOKE.new_IFriendDeleteListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x0000E954 File Offset: 0x0000CB54
		internal static HandleRef getCPtr(IFriendDeleteListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x0000E974 File Offset: 0x0000CB74
		~IFriendDeleteListener()
		{
			this.Dispose();
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0000E9A4 File Offset: 0x0000CBA4
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IFriendDeleteListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IFriendDeleteListener.listeners.ContainsKey(handle))
					{
						IFriendDeleteListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x060009B5 RID: 2485
		public abstract void OnFriendDeleteSuccess(GalaxyID userID);

		// Token: 0x060009B6 RID: 2486
		public abstract void OnFriendDeleteFailure(GalaxyID userID, IFriendDeleteListener.FailureReason failureReason);

		// Token: 0x060009B7 RID: 2487 RVA: 0x0000EA54 File Offset: 0x0000CC54
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnFriendDeleteSuccess", IFriendDeleteListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IFriendDeleteListener.SwigDelegateIFriendDeleteListener_0(IFriendDeleteListener.SwigDirectorOnFriendDeleteSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnFriendDeleteFailure", IFriendDeleteListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new IFriendDeleteListener.SwigDelegateIFriendDeleteListener_1(IFriendDeleteListener.SwigDirectorOnFriendDeleteFailure);
			}
			GalaxyInstancePINVOKE.IFriendDeleteListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0000EAC8 File Offset: 0x0000CCC8
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IFriendDeleteListener));
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x0000EAFE File Offset: 0x0000CCFE
		[MonoPInvokeCallback(typeof(IFriendDeleteListener.SwigDelegateIFriendDeleteListener_0))]
		private static void SwigDirectorOnFriendDeleteSuccess(IntPtr cPtr, IntPtr userID)
		{
			if (IFriendDeleteListener.listeners.ContainsKey(cPtr))
			{
				IFriendDeleteListener.listeners[cPtr].OnFriendDeleteSuccess(new GalaxyID(new GalaxyID(userID, false).ToUint64()));
			}
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x0000EB31 File Offset: 0x0000CD31
		[MonoPInvokeCallback(typeof(IFriendDeleteListener.SwigDelegateIFriendDeleteListener_1))]
		private static void SwigDirectorOnFriendDeleteFailure(IntPtr cPtr, IntPtr userID, int failureReason)
		{
			if (IFriendDeleteListener.listeners.ContainsKey(cPtr))
			{
				IFriendDeleteListener.listeners[cPtr].OnFriendDeleteFailure(new GalaxyID(new GalaxyID(userID, false).ToUint64()), (IFriendDeleteListener.FailureReason)failureReason);
			}
		}

		// Token: 0x0400017E RID: 382
		private static Dictionary<IntPtr, IFriendDeleteListener> listeners = new Dictionary<IntPtr, IFriendDeleteListener>();

		// Token: 0x0400017F RID: 383
		private HandleRef swigCPtr;

		// Token: 0x04000180 RID: 384
		private IFriendDeleteListener.SwigDelegateIFriendDeleteListener_0 swigDelegate0;

		// Token: 0x04000181 RID: 385
		private IFriendDeleteListener.SwigDelegateIFriendDeleteListener_1 swigDelegate1;

		// Token: 0x04000182 RID: 386
		private static Type[] swigMethodTypes0 = new Type[] { typeof(GalaxyID) };

		// Token: 0x04000183 RID: 387
		private static Type[] swigMethodTypes1 = new Type[]
		{
			typeof(GalaxyID),
			typeof(IFriendDeleteListener.FailureReason)
		};

		// Token: 0x020000E8 RID: 232
		// (Invoke) Token: 0x060009BD RID: 2493
		public delegate void SwigDelegateIFriendDeleteListener_0(IntPtr cPtr, IntPtr userID);

		// Token: 0x020000E9 RID: 233
		// (Invoke) Token: 0x060009C1 RID: 2497
		public delegate void SwigDelegateIFriendDeleteListener_1(IntPtr cPtr, IntPtr userID, int failureReason);

		// Token: 0x020000EA RID: 234
		public enum FailureReason
		{
			// Token: 0x04000185 RID: 389
			FAILURE_REASON_UNDEFINED,
			// Token: 0x04000186 RID: 390
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
