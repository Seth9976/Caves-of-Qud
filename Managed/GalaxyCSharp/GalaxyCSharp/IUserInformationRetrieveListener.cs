using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000178 RID: 376
	public abstract class IUserInformationRetrieveListener : GalaxyTypeAwareListenerUserInformationRetrieve
	{
		// Token: 0x06000DF6 RID: 3574 RVA: 0x0000BDAC File Offset: 0x00009FAC
		internal IUserInformationRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IUserInformationRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IUserInformationRetrieveListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x0000BDD4 File Offset: 0x00009FD4
		public IUserInformationRetrieveListener()
			: this(GalaxyInstancePINVOKE.new_IUserInformationRetrieveListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x0000BDF8 File Offset: 0x00009FF8
		internal static HandleRef getCPtr(IUserInformationRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x0000BE18 File Offset: 0x0000A018
		~IUserInformationRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x0000BE48 File Offset: 0x0000A048
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IUserInformationRetrieveListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IUserInformationRetrieveListener.listeners.ContainsKey(handle))
					{
						IUserInformationRetrieveListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000DFB RID: 3579
		public abstract void OnUserInformationRetrieveSuccess(GalaxyID userID);

		// Token: 0x06000DFC RID: 3580
		public abstract void OnUserInformationRetrieveFailure(GalaxyID userID, IUserInformationRetrieveListener.FailureReason failureReason);

		// Token: 0x06000DFD RID: 3581 RVA: 0x0000BEF8 File Offset: 0x0000A0F8
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnUserInformationRetrieveSuccess", IUserInformationRetrieveListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IUserInformationRetrieveListener.SwigDelegateIUserInformationRetrieveListener_0(IUserInformationRetrieveListener.SwigDirectorOnUserInformationRetrieveSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnUserInformationRetrieveFailure", IUserInformationRetrieveListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new IUserInformationRetrieveListener.SwigDelegateIUserInformationRetrieveListener_1(IUserInformationRetrieveListener.SwigDirectorOnUserInformationRetrieveFailure);
			}
			GalaxyInstancePINVOKE.IUserInformationRetrieveListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x0000BF6C File Offset: 0x0000A16C
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IUserInformationRetrieveListener));
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x0000BFA2 File Offset: 0x0000A1A2
		[MonoPInvokeCallback(typeof(IUserInformationRetrieveListener.SwigDelegateIUserInformationRetrieveListener_0))]
		private static void SwigDirectorOnUserInformationRetrieveSuccess(IntPtr cPtr, IntPtr userID)
		{
			if (IUserInformationRetrieveListener.listeners.ContainsKey(cPtr))
			{
				IUserInformationRetrieveListener.listeners[cPtr].OnUserInformationRetrieveSuccess(new GalaxyID(new GalaxyID(userID, false).ToUint64()));
			}
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x0000BFD5 File Offset: 0x0000A1D5
		[MonoPInvokeCallback(typeof(IUserInformationRetrieveListener.SwigDelegateIUserInformationRetrieveListener_1))]
		private static void SwigDirectorOnUserInformationRetrieveFailure(IntPtr cPtr, IntPtr userID, int failureReason)
		{
			if (IUserInformationRetrieveListener.listeners.ContainsKey(cPtr))
			{
				IUserInformationRetrieveListener.listeners[cPtr].OnUserInformationRetrieveFailure(new GalaxyID(new GalaxyID(userID, false).ToUint64()), (IUserInformationRetrieveListener.FailureReason)failureReason);
			}
		}

		// Token: 0x040002D5 RID: 725
		private static Dictionary<IntPtr, IUserInformationRetrieveListener> listeners = new Dictionary<IntPtr, IUserInformationRetrieveListener>();

		// Token: 0x040002D6 RID: 726
		private HandleRef swigCPtr;

		// Token: 0x040002D7 RID: 727
		private IUserInformationRetrieveListener.SwigDelegateIUserInformationRetrieveListener_0 swigDelegate0;

		// Token: 0x040002D8 RID: 728
		private IUserInformationRetrieveListener.SwigDelegateIUserInformationRetrieveListener_1 swigDelegate1;

		// Token: 0x040002D9 RID: 729
		private static Type[] swigMethodTypes0 = new Type[] { typeof(GalaxyID) };

		// Token: 0x040002DA RID: 730
		private static Type[] swigMethodTypes1 = new Type[]
		{
			typeof(GalaxyID),
			typeof(IUserInformationRetrieveListener.FailureReason)
		};

		// Token: 0x02000179 RID: 377
		// (Invoke) Token: 0x06000E03 RID: 3587
		public delegate void SwigDelegateIUserInformationRetrieveListener_0(IntPtr cPtr, IntPtr userID);

		// Token: 0x0200017A RID: 378
		// (Invoke) Token: 0x06000E07 RID: 3591
		public delegate void SwigDelegateIUserInformationRetrieveListener_1(IntPtr cPtr, IntPtr userID, int failureReason);

		// Token: 0x0200017B RID: 379
		public enum FailureReason
		{
			// Token: 0x040002DC RID: 732
			FAILURE_REASON_UNDEFINED,
			// Token: 0x040002DD RID: 733
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
