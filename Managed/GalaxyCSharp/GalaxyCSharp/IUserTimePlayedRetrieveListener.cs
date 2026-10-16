using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000180 RID: 384
	public abstract class IUserTimePlayedRetrieveListener : GalaxyTypeAwareListenerUserTimePlayedRetrieve
	{
		// Token: 0x06000E1E RID: 3614 RVA: 0x00016540 File Offset: 0x00014740
		internal IUserTimePlayedRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IUserTimePlayedRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IUserTimePlayedRetrieveListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x00016568 File Offset: 0x00014768
		public IUserTimePlayedRetrieveListener()
			: this(GalaxyInstancePINVOKE.new_IUserTimePlayedRetrieveListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x0001658C File Offset: 0x0001478C
		internal static HandleRef getCPtr(IUserTimePlayedRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x000165AC File Offset: 0x000147AC
		~IUserTimePlayedRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x000165DC File Offset: 0x000147DC
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IUserTimePlayedRetrieveListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IUserTimePlayedRetrieveListener.listeners.ContainsKey(handle))
					{
						IUserTimePlayedRetrieveListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000E23 RID: 3619
		public abstract void OnUserTimePlayedRetrieveSuccess(GalaxyID userID);

		// Token: 0x06000E24 RID: 3620
		public abstract void OnUserTimePlayedRetrieveFailure(GalaxyID userID, IUserTimePlayedRetrieveListener.FailureReason failureReason);

		// Token: 0x06000E25 RID: 3621 RVA: 0x0001668C File Offset: 0x0001488C
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnUserTimePlayedRetrieveSuccess", IUserTimePlayedRetrieveListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IUserTimePlayedRetrieveListener.SwigDelegateIUserTimePlayedRetrieveListener_0(IUserTimePlayedRetrieveListener.SwigDirectorOnUserTimePlayedRetrieveSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnUserTimePlayedRetrieveFailure", IUserTimePlayedRetrieveListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new IUserTimePlayedRetrieveListener.SwigDelegateIUserTimePlayedRetrieveListener_1(IUserTimePlayedRetrieveListener.SwigDirectorOnUserTimePlayedRetrieveFailure);
			}
			GalaxyInstancePINVOKE.IUserTimePlayedRetrieveListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x00016700 File Offset: 0x00014900
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IUserTimePlayedRetrieveListener));
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x00016736 File Offset: 0x00014936
		[MonoPInvokeCallback(typeof(IUserTimePlayedRetrieveListener.SwigDelegateIUserTimePlayedRetrieveListener_0))]
		private static void SwigDirectorOnUserTimePlayedRetrieveSuccess(IntPtr cPtr, IntPtr userID)
		{
			if (IUserTimePlayedRetrieveListener.listeners.ContainsKey(cPtr))
			{
				IUserTimePlayedRetrieveListener.listeners[cPtr].OnUserTimePlayedRetrieveSuccess(new GalaxyID(new GalaxyID(userID, false).ToUint64()));
			}
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x00016769 File Offset: 0x00014969
		[MonoPInvokeCallback(typeof(IUserTimePlayedRetrieveListener.SwigDelegateIUserTimePlayedRetrieveListener_1))]
		private static void SwigDirectorOnUserTimePlayedRetrieveFailure(IntPtr cPtr, IntPtr userID, int failureReason)
		{
			if (IUserTimePlayedRetrieveListener.listeners.ContainsKey(cPtr))
			{
				IUserTimePlayedRetrieveListener.listeners[cPtr].OnUserTimePlayedRetrieveFailure(new GalaxyID(new GalaxyID(userID, false).ToUint64()), (IUserTimePlayedRetrieveListener.FailureReason)failureReason);
			}
		}

		// Token: 0x040002E7 RID: 743
		private static Dictionary<IntPtr, IUserTimePlayedRetrieveListener> listeners = new Dictionary<IntPtr, IUserTimePlayedRetrieveListener>();

		// Token: 0x040002E8 RID: 744
		private HandleRef swigCPtr;

		// Token: 0x040002E9 RID: 745
		private IUserTimePlayedRetrieveListener.SwigDelegateIUserTimePlayedRetrieveListener_0 swigDelegate0;

		// Token: 0x040002EA RID: 746
		private IUserTimePlayedRetrieveListener.SwigDelegateIUserTimePlayedRetrieveListener_1 swigDelegate1;

		// Token: 0x040002EB RID: 747
		private static Type[] swigMethodTypes0 = new Type[] { typeof(GalaxyID) };

		// Token: 0x040002EC RID: 748
		private static Type[] swigMethodTypes1 = new Type[]
		{
			typeof(GalaxyID),
			typeof(IUserTimePlayedRetrieveListener.FailureReason)
		};

		// Token: 0x02000181 RID: 385
		// (Invoke) Token: 0x06000E2B RID: 3627
		public delegate void SwigDelegateIUserTimePlayedRetrieveListener_0(IntPtr cPtr, IntPtr userID);

		// Token: 0x02000182 RID: 386
		// (Invoke) Token: 0x06000E2F RID: 3631
		public delegate void SwigDelegateIUserTimePlayedRetrieveListener_1(IntPtr cPtr, IntPtr userID, int failureReason);

		// Token: 0x02000183 RID: 387
		public enum FailureReason
		{
			// Token: 0x040002EE RID: 750
			FAILURE_REASON_UNDEFINED,
			// Token: 0x040002EF RID: 751
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
