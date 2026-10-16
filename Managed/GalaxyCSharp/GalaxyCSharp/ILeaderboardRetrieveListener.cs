using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200010B RID: 267
	public abstract class ILeaderboardRetrieveListener : GalaxyTypeAwareListenerLeaderboardRetrieve
	{
		// Token: 0x06000AAF RID: 2735 RVA: 0x00010C14 File Offset: 0x0000EE14
		internal ILeaderboardRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ILeaderboardRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			ILeaderboardRetrieveListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x00010C3C File Offset: 0x0000EE3C
		public ILeaderboardRetrieveListener()
			: this(GalaxyInstancePINVOKE.new_ILeaderboardRetrieveListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x00010C60 File Offset: 0x0000EE60
		internal static HandleRef getCPtr(ILeaderboardRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x00010C80 File Offset: 0x0000EE80
		~ILeaderboardRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x00010CB0 File Offset: 0x0000EEB0
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ILeaderboardRetrieveListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (ILeaderboardRetrieveListener.listeners.ContainsKey(handle))
					{
						ILeaderboardRetrieveListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000AB4 RID: 2740
		public abstract void OnLeaderboardRetrieveSuccess(string name);

		// Token: 0x06000AB5 RID: 2741
		public abstract void OnLeaderboardRetrieveFailure(string name, ILeaderboardRetrieveListener.FailureReason failureReason);

		// Token: 0x06000AB6 RID: 2742 RVA: 0x00010D60 File Offset: 0x0000EF60
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnLeaderboardRetrieveSuccess", ILeaderboardRetrieveListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new ILeaderboardRetrieveListener.SwigDelegateILeaderboardRetrieveListener_0(ILeaderboardRetrieveListener.SwigDirectorOnLeaderboardRetrieveSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnLeaderboardRetrieveFailure", ILeaderboardRetrieveListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new ILeaderboardRetrieveListener.SwigDelegateILeaderboardRetrieveListener_1(ILeaderboardRetrieveListener.SwigDirectorOnLeaderboardRetrieveFailure);
			}
			GalaxyInstancePINVOKE.ILeaderboardRetrieveListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x00010DD4 File Offset: 0x0000EFD4
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ILeaderboardRetrieveListener));
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x00010E0A File Offset: 0x0000F00A
		[MonoPInvokeCallback(typeof(ILeaderboardRetrieveListener.SwigDelegateILeaderboardRetrieveListener_0))]
		private static void SwigDirectorOnLeaderboardRetrieveSuccess(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string name)
		{
			if (ILeaderboardRetrieveListener.listeners.ContainsKey(cPtr))
			{
				ILeaderboardRetrieveListener.listeners[cPtr].OnLeaderboardRetrieveSuccess(name);
			}
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x00010E2D File Offset: 0x0000F02D
		[MonoPInvokeCallback(typeof(ILeaderboardRetrieveListener.SwigDelegateILeaderboardRetrieveListener_1))]
		private static void SwigDirectorOnLeaderboardRetrieveFailure(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string name, int failureReason)
		{
			if (ILeaderboardRetrieveListener.listeners.ContainsKey(cPtr))
			{
				ILeaderboardRetrieveListener.listeners[cPtr].OnLeaderboardRetrieveFailure(name, (ILeaderboardRetrieveListener.FailureReason)failureReason);
			}
		}

		// Token: 0x040001D2 RID: 466
		private static Dictionary<IntPtr, ILeaderboardRetrieveListener> listeners = new Dictionary<IntPtr, ILeaderboardRetrieveListener>();

		// Token: 0x040001D3 RID: 467
		private HandleRef swigCPtr;

		// Token: 0x040001D4 RID: 468
		private ILeaderboardRetrieveListener.SwigDelegateILeaderboardRetrieveListener_0 swigDelegate0;

		// Token: 0x040001D5 RID: 469
		private ILeaderboardRetrieveListener.SwigDelegateILeaderboardRetrieveListener_1 swigDelegate1;

		// Token: 0x040001D6 RID: 470
		private static Type[] swigMethodTypes0 = new Type[] { typeof(string) };

		// Token: 0x040001D7 RID: 471
		private static Type[] swigMethodTypes1 = new Type[]
		{
			typeof(string),
			typeof(ILeaderboardRetrieveListener.FailureReason)
		};

		// Token: 0x0200010C RID: 268
		// (Invoke) Token: 0x06000ABC RID: 2748
		public delegate void SwigDelegateILeaderboardRetrieveListener_0(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string name);

		// Token: 0x0200010D RID: 269
		// (Invoke) Token: 0x06000AC0 RID: 2752
		public delegate void SwigDelegateILeaderboardRetrieveListener_1(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string name, int failureReason);

		// Token: 0x0200010E RID: 270
		public enum FailureReason
		{
			// Token: 0x040001D9 RID: 473
			FAILURE_REASON_UNDEFINED,
			// Token: 0x040001DA RID: 474
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
