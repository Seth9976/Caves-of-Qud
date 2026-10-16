using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200010F RID: 271
	public abstract class ILeaderboardScoreUpdateListener : GalaxyTypeAwareListenerLeaderboardScoreUpdate
	{
		// Token: 0x06000AC3 RID: 2755 RVA: 0x00010FD4 File Offset: 0x0000F1D4
		internal ILeaderboardScoreUpdateListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ILeaderboardScoreUpdateListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			ILeaderboardScoreUpdateListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x00010FFC File Offset: 0x0000F1FC
		public ILeaderboardScoreUpdateListener()
			: this(GalaxyInstancePINVOKE.new_ILeaderboardScoreUpdateListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x00011020 File Offset: 0x0000F220
		internal static HandleRef getCPtr(ILeaderboardScoreUpdateListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x00011040 File Offset: 0x0000F240
		~ILeaderboardScoreUpdateListener()
		{
			this.Dispose();
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x00011070 File Offset: 0x0000F270
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ILeaderboardScoreUpdateListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (ILeaderboardScoreUpdateListener.listeners.ContainsKey(handle))
					{
						ILeaderboardScoreUpdateListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000AC8 RID: 2760
		public abstract void OnLeaderboardScoreUpdateSuccess(string name, int score, uint oldRank, uint newRank);

		// Token: 0x06000AC9 RID: 2761
		public abstract void OnLeaderboardScoreUpdateFailure(string name, int score, ILeaderboardScoreUpdateListener.FailureReason failureReason);

		// Token: 0x06000ACA RID: 2762 RVA: 0x00011120 File Offset: 0x0000F320
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnLeaderboardScoreUpdateSuccess", ILeaderboardScoreUpdateListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new ILeaderboardScoreUpdateListener.SwigDelegateILeaderboardScoreUpdateListener_0(ILeaderboardScoreUpdateListener.SwigDirectorOnLeaderboardScoreUpdateSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnLeaderboardScoreUpdateFailure", ILeaderboardScoreUpdateListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new ILeaderboardScoreUpdateListener.SwigDelegateILeaderboardScoreUpdateListener_1(ILeaderboardScoreUpdateListener.SwigDirectorOnLeaderboardScoreUpdateFailure);
			}
			GalaxyInstancePINVOKE.ILeaderboardScoreUpdateListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x00011194 File Offset: 0x0000F394
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ILeaderboardScoreUpdateListener));
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x000111CA File Offset: 0x0000F3CA
		[MonoPInvokeCallback(typeof(ILeaderboardScoreUpdateListener.SwigDelegateILeaderboardScoreUpdateListener_0))]
		private static void SwigDirectorOnLeaderboardScoreUpdateSuccess(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string name, int score, uint oldRank, uint newRank)
		{
			if (ILeaderboardScoreUpdateListener.listeners.ContainsKey(cPtr))
			{
				ILeaderboardScoreUpdateListener.listeners[cPtr].OnLeaderboardScoreUpdateSuccess(name, score, oldRank, newRank);
			}
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x000111F1 File Offset: 0x0000F3F1
		[MonoPInvokeCallback(typeof(ILeaderboardScoreUpdateListener.SwigDelegateILeaderboardScoreUpdateListener_1))]
		private static void SwigDirectorOnLeaderboardScoreUpdateFailure(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string name, int score, int failureReason)
		{
			if (ILeaderboardScoreUpdateListener.listeners.ContainsKey(cPtr))
			{
				ILeaderboardScoreUpdateListener.listeners[cPtr].OnLeaderboardScoreUpdateFailure(name, score, (ILeaderboardScoreUpdateListener.FailureReason)failureReason);
			}
		}

		// Token: 0x040001DB RID: 475
		private static Dictionary<IntPtr, ILeaderboardScoreUpdateListener> listeners = new Dictionary<IntPtr, ILeaderboardScoreUpdateListener>();

		// Token: 0x040001DC RID: 476
		private HandleRef swigCPtr;

		// Token: 0x040001DD RID: 477
		private ILeaderboardScoreUpdateListener.SwigDelegateILeaderboardScoreUpdateListener_0 swigDelegate0;

		// Token: 0x040001DE RID: 478
		private ILeaderboardScoreUpdateListener.SwigDelegateILeaderboardScoreUpdateListener_1 swigDelegate1;

		// Token: 0x040001DF RID: 479
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(string),
			typeof(int),
			typeof(uint),
			typeof(uint)
		};

		// Token: 0x040001E0 RID: 480
		private static Type[] swigMethodTypes1 = new Type[]
		{
			typeof(string),
			typeof(int),
			typeof(ILeaderboardScoreUpdateListener.FailureReason)
		};

		// Token: 0x02000110 RID: 272
		// (Invoke) Token: 0x06000AD0 RID: 2768
		public delegate void SwigDelegateILeaderboardScoreUpdateListener_0(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string name, int score, uint oldRank, uint newRank);

		// Token: 0x02000111 RID: 273
		// (Invoke) Token: 0x06000AD4 RID: 2772
		public delegate void SwigDelegateILeaderboardScoreUpdateListener_1(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string name, int score, int failureReason);

		// Token: 0x02000112 RID: 274
		public enum FailureReason
		{
			// Token: 0x040001E2 RID: 482
			FAILURE_REASON_UNDEFINED,
			// Token: 0x040001E3 RID: 483
			FAILURE_REASON_NO_IMPROVEMENT,
			// Token: 0x040001E4 RID: 484
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
