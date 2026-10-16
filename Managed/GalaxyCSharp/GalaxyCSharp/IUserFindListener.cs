using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000174 RID: 372
	public abstract class IUserFindListener : GalaxyTypeAwareListenerUserFind
	{
		// Token: 0x06000DE2 RID: 3554 RVA: 0x00015C14 File Offset: 0x00013E14
		internal IUserFindListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IUserFindListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IUserFindListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x00015C3C File Offset: 0x00013E3C
		public IUserFindListener()
			: this(GalaxyInstancePINVOKE.new_IUserFindListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x00015C60 File Offset: 0x00013E60
		internal static HandleRef getCPtr(IUserFindListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x00015C80 File Offset: 0x00013E80
		~IUserFindListener()
		{
			this.Dispose();
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x00015CB0 File Offset: 0x00013EB0
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IUserFindListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IUserFindListener.listeners.ContainsKey(handle))
					{
						IUserFindListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000DE7 RID: 3559
		public abstract void OnUserFindSuccess(string userSpecifier, GalaxyID userID);

		// Token: 0x06000DE8 RID: 3560
		public abstract void OnUserFindFailure(string userSpecifier, IUserFindListener.FailureReason failureReason);

		// Token: 0x06000DE9 RID: 3561 RVA: 0x00015D60 File Offset: 0x00013F60
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnUserFindSuccess", IUserFindListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IUserFindListener.SwigDelegateIUserFindListener_0(IUserFindListener.SwigDirectorOnUserFindSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnUserFindFailure", IUserFindListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new IUserFindListener.SwigDelegateIUserFindListener_1(IUserFindListener.SwigDirectorOnUserFindFailure);
			}
			GalaxyInstancePINVOKE.IUserFindListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x00015DD4 File Offset: 0x00013FD4
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IUserFindListener));
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x00015E0A File Offset: 0x0001400A
		[MonoPInvokeCallback(typeof(IUserFindListener.SwigDelegateIUserFindListener_0))]
		private static void SwigDirectorOnUserFindSuccess(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string userSpecifier, IntPtr userID)
		{
			if (IUserFindListener.listeners.ContainsKey(cPtr))
			{
				IUserFindListener.listeners[cPtr].OnUserFindSuccess(userSpecifier, new GalaxyID(new GalaxyID(userID, false).ToUint64()));
			}
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x00015E3E File Offset: 0x0001403E
		[MonoPInvokeCallback(typeof(IUserFindListener.SwigDelegateIUserFindListener_1))]
		private static void SwigDirectorOnUserFindFailure(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string userSpecifier, int failureReason)
		{
			if (IUserFindListener.listeners.ContainsKey(cPtr))
			{
				IUserFindListener.listeners[cPtr].OnUserFindFailure(userSpecifier, (IUserFindListener.FailureReason)failureReason);
			}
		}

		// Token: 0x040002CB RID: 715
		private static Dictionary<IntPtr, IUserFindListener> listeners = new Dictionary<IntPtr, IUserFindListener>();

		// Token: 0x040002CC RID: 716
		private HandleRef swigCPtr;

		// Token: 0x040002CD RID: 717
		private IUserFindListener.SwigDelegateIUserFindListener_0 swigDelegate0;

		// Token: 0x040002CE RID: 718
		private IUserFindListener.SwigDelegateIUserFindListener_1 swigDelegate1;

		// Token: 0x040002CF RID: 719
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(string),
			typeof(GalaxyID)
		};

		// Token: 0x040002D0 RID: 720
		private static Type[] swigMethodTypes1 = new Type[]
		{
			typeof(string),
			typeof(IUserFindListener.FailureReason)
		};

		// Token: 0x02000175 RID: 373
		// (Invoke) Token: 0x06000DEF RID: 3567
		public delegate void SwigDelegateIUserFindListener_0(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string userSpecifier, IntPtr userID);

		// Token: 0x02000176 RID: 374
		// (Invoke) Token: 0x06000DF3 RID: 3571
		public delegate void SwigDelegateIUserFindListener_1(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string userSpecifier, int failureReason);

		// Token: 0x02000177 RID: 375
		public enum FailureReason
		{
			// Token: 0x040002D2 RID: 722
			FAILURE_REASON_UNDEFINED,
			// Token: 0x040002D3 RID: 723
			FAILURE_REASON_USER_NOT_FOUND,
			// Token: 0x040002D4 RID: 724
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
