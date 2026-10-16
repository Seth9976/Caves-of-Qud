using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000107 RID: 263
	public abstract class ILeaderboardEntriesRetrieveListener : GalaxyTypeAwareListenerLeaderboardEntriesRetrieve
	{
		// Token: 0x06000A9B RID: 2715 RVA: 0x00010844 File Offset: 0x0000EA44
		internal ILeaderboardEntriesRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ILeaderboardEntriesRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			ILeaderboardEntriesRetrieveListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x0001086C File Offset: 0x0000EA6C
		public ILeaderboardEntriesRetrieveListener()
			: this(GalaxyInstancePINVOKE.new_ILeaderboardEntriesRetrieveListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x00010890 File Offset: 0x0000EA90
		internal static HandleRef getCPtr(ILeaderboardEntriesRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x000108B0 File Offset: 0x0000EAB0
		~ILeaderboardEntriesRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x000108E0 File Offset: 0x0000EAE0
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ILeaderboardEntriesRetrieveListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (ILeaderboardEntriesRetrieveListener.listeners.ContainsKey(handle))
					{
						ILeaderboardEntriesRetrieveListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000AA0 RID: 2720
		public abstract void OnLeaderboardEntriesRetrieveSuccess(string name, uint entryCount);

		// Token: 0x06000AA1 RID: 2721
		public abstract void OnLeaderboardEntriesRetrieveFailure(string name, ILeaderboardEntriesRetrieveListener.FailureReason failureReason);

		// Token: 0x06000AA2 RID: 2722 RVA: 0x00010990 File Offset: 0x0000EB90
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnLeaderboardEntriesRetrieveSuccess", ILeaderboardEntriesRetrieveListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new ILeaderboardEntriesRetrieveListener.SwigDelegateILeaderboardEntriesRetrieveListener_0(ILeaderboardEntriesRetrieveListener.SwigDirectorOnLeaderboardEntriesRetrieveSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnLeaderboardEntriesRetrieveFailure", ILeaderboardEntriesRetrieveListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new ILeaderboardEntriesRetrieveListener.SwigDelegateILeaderboardEntriesRetrieveListener_1(ILeaderboardEntriesRetrieveListener.SwigDirectorOnLeaderboardEntriesRetrieveFailure);
			}
			GalaxyInstancePINVOKE.ILeaderboardEntriesRetrieveListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x00010A04 File Offset: 0x0000EC04
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ILeaderboardEntriesRetrieveListener));
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x00010A3A File Offset: 0x0000EC3A
		[MonoPInvokeCallback(typeof(ILeaderboardEntriesRetrieveListener.SwigDelegateILeaderboardEntriesRetrieveListener_0))]
		private static void SwigDirectorOnLeaderboardEntriesRetrieveSuccess(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string name, uint entryCount)
		{
			if (ILeaderboardEntriesRetrieveListener.listeners.ContainsKey(cPtr))
			{
				ILeaderboardEntriesRetrieveListener.listeners[cPtr].OnLeaderboardEntriesRetrieveSuccess(name, entryCount);
			}
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x00010A5E File Offset: 0x0000EC5E
		[MonoPInvokeCallback(typeof(ILeaderboardEntriesRetrieveListener.SwigDelegateILeaderboardEntriesRetrieveListener_1))]
		private static void SwigDirectorOnLeaderboardEntriesRetrieveFailure(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string name, int failureReason)
		{
			if (ILeaderboardEntriesRetrieveListener.listeners.ContainsKey(cPtr))
			{
				ILeaderboardEntriesRetrieveListener.listeners[cPtr].OnLeaderboardEntriesRetrieveFailure(name, (ILeaderboardEntriesRetrieveListener.FailureReason)failureReason);
			}
		}

		// Token: 0x040001C8 RID: 456
		private static Dictionary<IntPtr, ILeaderboardEntriesRetrieveListener> listeners = new Dictionary<IntPtr, ILeaderboardEntriesRetrieveListener>();

		// Token: 0x040001C9 RID: 457
		private HandleRef swigCPtr;

		// Token: 0x040001CA RID: 458
		private ILeaderboardEntriesRetrieveListener.SwigDelegateILeaderboardEntriesRetrieveListener_0 swigDelegate0;

		// Token: 0x040001CB RID: 459
		private ILeaderboardEntriesRetrieveListener.SwigDelegateILeaderboardEntriesRetrieveListener_1 swigDelegate1;

		// Token: 0x040001CC RID: 460
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(string),
			typeof(uint)
		};

		// Token: 0x040001CD RID: 461
		private static Type[] swigMethodTypes1 = new Type[]
		{
			typeof(string),
			typeof(ILeaderboardEntriesRetrieveListener.FailureReason)
		};

		// Token: 0x02000108 RID: 264
		// (Invoke) Token: 0x06000AA8 RID: 2728
		public delegate void SwigDelegateILeaderboardEntriesRetrieveListener_0(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string name, uint entryCount);

		// Token: 0x02000109 RID: 265
		// (Invoke) Token: 0x06000AAC RID: 2732
		public delegate void SwigDelegateILeaderboardEntriesRetrieveListener_1(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string name, int failureReason);

		// Token: 0x0200010A RID: 266
		public enum FailureReason
		{
			// Token: 0x040001CF RID: 463
			FAILURE_REASON_UNDEFINED,
			// Token: 0x040001D0 RID: 464
			FAILURE_REASON_NOT_FOUND,
			// Token: 0x040001D1 RID: 465
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
