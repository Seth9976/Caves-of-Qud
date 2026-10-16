using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000166 RID: 358
	public abstract class IStatsAndAchievementsStoreListener : GalaxyTypeAwareListenerStatsAndAchievementsStore
	{
		// Token: 0x06000D3A RID: 3386 RVA: 0x0001542C File Offset: 0x0001362C
		internal IStatsAndAchievementsStoreListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IStatsAndAchievementsStoreListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IStatsAndAchievementsStoreListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x00015454 File Offset: 0x00013654
		public IStatsAndAchievementsStoreListener()
			: this(GalaxyInstancePINVOKE.new_IStatsAndAchievementsStoreListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x00015478 File Offset: 0x00013678
		internal static HandleRef getCPtr(IStatsAndAchievementsStoreListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x00015498 File Offset: 0x00013698
		~IStatsAndAchievementsStoreListener()
		{
			this.Dispose();
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x000154C8 File Offset: 0x000136C8
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IStatsAndAchievementsStoreListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IStatsAndAchievementsStoreListener.listeners.ContainsKey(handle))
					{
						IStatsAndAchievementsStoreListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000D3F RID: 3391
		public abstract void OnUserStatsAndAchievementsStoreSuccess();

		// Token: 0x06000D40 RID: 3392
		public abstract void OnUserStatsAndAchievementsStoreFailure(IStatsAndAchievementsStoreListener.FailureReason failureReason);

		// Token: 0x06000D41 RID: 3393 RVA: 0x00015578 File Offset: 0x00013778
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnUserStatsAndAchievementsStoreSuccess", IStatsAndAchievementsStoreListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IStatsAndAchievementsStoreListener.SwigDelegateIStatsAndAchievementsStoreListener_0(IStatsAndAchievementsStoreListener.SwigDirectorOnUserStatsAndAchievementsStoreSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnUserStatsAndAchievementsStoreFailure", IStatsAndAchievementsStoreListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new IStatsAndAchievementsStoreListener.SwigDelegateIStatsAndAchievementsStoreListener_1(IStatsAndAchievementsStoreListener.SwigDirectorOnUserStatsAndAchievementsStoreFailure);
			}
			GalaxyInstancePINVOKE.IStatsAndAchievementsStoreListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x000155EC File Offset: 0x000137EC
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IStatsAndAchievementsStoreListener));
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x00015622 File Offset: 0x00013822
		[MonoPInvokeCallback(typeof(IStatsAndAchievementsStoreListener.SwigDelegateIStatsAndAchievementsStoreListener_0))]
		private static void SwigDirectorOnUserStatsAndAchievementsStoreSuccess(IntPtr cPtr)
		{
			if (IStatsAndAchievementsStoreListener.listeners.ContainsKey(cPtr))
			{
				IStatsAndAchievementsStoreListener.listeners[cPtr].OnUserStatsAndAchievementsStoreSuccess();
			}
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x00015644 File Offset: 0x00013844
		[MonoPInvokeCallback(typeof(IStatsAndAchievementsStoreListener.SwigDelegateIStatsAndAchievementsStoreListener_1))]
		private static void SwigDirectorOnUserStatsAndAchievementsStoreFailure(IntPtr cPtr, int failureReason)
		{
			if (IStatsAndAchievementsStoreListener.listeners.ContainsKey(cPtr))
			{
				IStatsAndAchievementsStoreListener.listeners[cPtr].OnUserStatsAndAchievementsStoreFailure((IStatsAndAchievementsStoreListener.FailureReason)failureReason);
			}
		}

		// Token: 0x040002A7 RID: 679
		private static Dictionary<IntPtr, IStatsAndAchievementsStoreListener> listeners = new Dictionary<IntPtr, IStatsAndAchievementsStoreListener>();

		// Token: 0x040002A8 RID: 680
		private HandleRef swigCPtr;

		// Token: 0x040002A9 RID: 681
		private IStatsAndAchievementsStoreListener.SwigDelegateIStatsAndAchievementsStoreListener_0 swigDelegate0;

		// Token: 0x040002AA RID: 682
		private IStatsAndAchievementsStoreListener.SwigDelegateIStatsAndAchievementsStoreListener_1 swigDelegate1;

		// Token: 0x040002AB RID: 683
		private static Type[] swigMethodTypes0 = new Type[0];

		// Token: 0x040002AC RID: 684
		private static Type[] swigMethodTypes1 = new Type[] { typeof(IStatsAndAchievementsStoreListener.FailureReason) };

		// Token: 0x02000167 RID: 359
		// (Invoke) Token: 0x06000D47 RID: 3399
		public delegate void SwigDelegateIStatsAndAchievementsStoreListener_0(IntPtr cPtr);

		// Token: 0x02000168 RID: 360
		// (Invoke) Token: 0x06000D4B RID: 3403
		public delegate void SwigDelegateIStatsAndAchievementsStoreListener_1(IntPtr cPtr, int failureReason);

		// Token: 0x02000169 RID: 361
		public enum FailureReason
		{
			// Token: 0x040002AE RID: 686
			FAILURE_REASON_UNDEFINED,
			// Token: 0x040002AF RID: 687
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
