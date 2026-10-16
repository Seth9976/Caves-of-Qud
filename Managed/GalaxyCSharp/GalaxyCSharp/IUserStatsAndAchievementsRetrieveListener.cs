using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200017C RID: 380
	public abstract class IUserStatsAndAchievementsRetrieveListener : GalaxyTypeAwareListenerUserStatsAndAchievementsRetrieve
	{
		// Token: 0x06000E0A RID: 3594 RVA: 0x00016160 File Offset: 0x00014360
		internal IUserStatsAndAchievementsRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IUserStatsAndAchievementsRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IUserStatsAndAchievementsRetrieveListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x00016188 File Offset: 0x00014388
		public IUserStatsAndAchievementsRetrieveListener()
			: this(GalaxyInstancePINVOKE.new_IUserStatsAndAchievementsRetrieveListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x000161AC File Offset: 0x000143AC
		internal static HandleRef getCPtr(IUserStatsAndAchievementsRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x000161CC File Offset: 0x000143CC
		~IUserStatsAndAchievementsRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x000161FC File Offset: 0x000143FC
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IUserStatsAndAchievementsRetrieveListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IUserStatsAndAchievementsRetrieveListener.listeners.ContainsKey(handle))
					{
						IUserStatsAndAchievementsRetrieveListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000E0F RID: 3599
		public abstract void OnUserStatsAndAchievementsRetrieveSuccess(GalaxyID userID);

		// Token: 0x06000E10 RID: 3600
		public abstract void OnUserStatsAndAchievementsRetrieveFailure(GalaxyID userID, IUserStatsAndAchievementsRetrieveListener.FailureReason failureReason);

		// Token: 0x06000E11 RID: 3601 RVA: 0x000162AC File Offset: 0x000144AC
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnUserStatsAndAchievementsRetrieveSuccess", IUserStatsAndAchievementsRetrieveListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IUserStatsAndAchievementsRetrieveListener.SwigDelegateIUserStatsAndAchievementsRetrieveListener_0(IUserStatsAndAchievementsRetrieveListener.SwigDirectorOnUserStatsAndAchievementsRetrieveSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnUserStatsAndAchievementsRetrieveFailure", IUserStatsAndAchievementsRetrieveListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new IUserStatsAndAchievementsRetrieveListener.SwigDelegateIUserStatsAndAchievementsRetrieveListener_1(IUserStatsAndAchievementsRetrieveListener.SwigDirectorOnUserStatsAndAchievementsRetrieveFailure);
			}
			GalaxyInstancePINVOKE.IUserStatsAndAchievementsRetrieveListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x00016320 File Offset: 0x00014520
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IUserStatsAndAchievementsRetrieveListener));
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x00016356 File Offset: 0x00014556
		[MonoPInvokeCallback(typeof(IUserStatsAndAchievementsRetrieveListener.SwigDelegateIUserStatsAndAchievementsRetrieveListener_0))]
		private static void SwigDirectorOnUserStatsAndAchievementsRetrieveSuccess(IntPtr cPtr, IntPtr userID)
		{
			if (IUserStatsAndAchievementsRetrieveListener.listeners.ContainsKey(cPtr))
			{
				IUserStatsAndAchievementsRetrieveListener.listeners[cPtr].OnUserStatsAndAchievementsRetrieveSuccess(new GalaxyID(new GalaxyID(userID, false).ToUint64()));
			}
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x00016389 File Offset: 0x00014589
		[MonoPInvokeCallback(typeof(IUserStatsAndAchievementsRetrieveListener.SwigDelegateIUserStatsAndAchievementsRetrieveListener_1))]
		private static void SwigDirectorOnUserStatsAndAchievementsRetrieveFailure(IntPtr cPtr, IntPtr userID, int failureReason)
		{
			if (IUserStatsAndAchievementsRetrieveListener.listeners.ContainsKey(cPtr))
			{
				IUserStatsAndAchievementsRetrieveListener.listeners[cPtr].OnUserStatsAndAchievementsRetrieveFailure(new GalaxyID(new GalaxyID(userID, false).ToUint64()), (IUserStatsAndAchievementsRetrieveListener.FailureReason)failureReason);
			}
		}

		// Token: 0x040002DE RID: 734
		private static Dictionary<IntPtr, IUserStatsAndAchievementsRetrieveListener> listeners = new Dictionary<IntPtr, IUserStatsAndAchievementsRetrieveListener>();

		// Token: 0x040002DF RID: 735
		private HandleRef swigCPtr;

		// Token: 0x040002E0 RID: 736
		private IUserStatsAndAchievementsRetrieveListener.SwigDelegateIUserStatsAndAchievementsRetrieveListener_0 swigDelegate0;

		// Token: 0x040002E1 RID: 737
		private IUserStatsAndAchievementsRetrieveListener.SwigDelegateIUserStatsAndAchievementsRetrieveListener_1 swigDelegate1;

		// Token: 0x040002E2 RID: 738
		private static Type[] swigMethodTypes0 = new Type[] { typeof(GalaxyID) };

		// Token: 0x040002E3 RID: 739
		private static Type[] swigMethodTypes1 = new Type[]
		{
			typeof(GalaxyID),
			typeof(IUserStatsAndAchievementsRetrieveListener.FailureReason)
		};

		// Token: 0x0200017D RID: 381
		// (Invoke) Token: 0x06000E17 RID: 3607
		public delegate void SwigDelegateIUserStatsAndAchievementsRetrieveListener_0(IntPtr cPtr, IntPtr userID);

		// Token: 0x0200017E RID: 382
		// (Invoke) Token: 0x06000E1B RID: 3611
		public delegate void SwigDelegateIUserStatsAndAchievementsRetrieveListener_1(IntPtr cPtr, IntPtr userID, int failureReason);

		// Token: 0x0200017F RID: 383
		public enum FailureReason
		{
			// Token: 0x040002E5 RID: 741
			FAILURE_REASON_UNDEFINED,
			// Token: 0x040002E6 RID: 742
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
