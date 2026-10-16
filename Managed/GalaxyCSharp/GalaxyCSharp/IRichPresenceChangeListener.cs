using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200014C RID: 332
	public abstract class IRichPresenceChangeListener : GalaxyTypeAwareListenerRichPresenceChange
	{
		// Token: 0x06000C79 RID: 3193 RVA: 0x00013F14 File Offset: 0x00012114
		internal IRichPresenceChangeListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IRichPresenceChangeListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IRichPresenceChangeListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x00013F3C File Offset: 0x0001213C
		public IRichPresenceChangeListener()
			: this(GalaxyInstancePINVOKE.new_IRichPresenceChangeListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x00013F60 File Offset: 0x00012160
		internal static HandleRef getCPtr(IRichPresenceChangeListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x00013F80 File Offset: 0x00012180
		~IRichPresenceChangeListener()
		{
			this.Dispose();
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x00013FB0 File Offset: 0x000121B0
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IRichPresenceChangeListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IRichPresenceChangeListener.listeners.ContainsKey(handle))
					{
						IRichPresenceChangeListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000C7E RID: 3198
		public abstract void OnRichPresenceChangeSuccess();

		// Token: 0x06000C7F RID: 3199
		public abstract void OnRichPresenceChangeFailure(IRichPresenceChangeListener.FailureReason failureReason);

		// Token: 0x06000C80 RID: 3200 RVA: 0x00014060 File Offset: 0x00012260
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnRichPresenceChangeSuccess", IRichPresenceChangeListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IRichPresenceChangeListener.SwigDelegateIRichPresenceChangeListener_0(IRichPresenceChangeListener.SwigDirectorOnRichPresenceChangeSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnRichPresenceChangeFailure", IRichPresenceChangeListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new IRichPresenceChangeListener.SwigDelegateIRichPresenceChangeListener_1(IRichPresenceChangeListener.SwigDirectorOnRichPresenceChangeFailure);
			}
			GalaxyInstancePINVOKE.IRichPresenceChangeListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x000140D4 File Offset: 0x000122D4
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IRichPresenceChangeListener));
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x0001410A File Offset: 0x0001230A
		[MonoPInvokeCallback(typeof(IRichPresenceChangeListener.SwigDelegateIRichPresenceChangeListener_0))]
		private static void SwigDirectorOnRichPresenceChangeSuccess(IntPtr cPtr)
		{
			if (IRichPresenceChangeListener.listeners.ContainsKey(cPtr))
			{
				IRichPresenceChangeListener.listeners[cPtr].OnRichPresenceChangeSuccess();
			}
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x0001412C File Offset: 0x0001232C
		[MonoPInvokeCallback(typeof(IRichPresenceChangeListener.SwigDelegateIRichPresenceChangeListener_1))]
		private static void SwigDirectorOnRichPresenceChangeFailure(IntPtr cPtr, int failureReason)
		{
			if (IRichPresenceChangeListener.listeners.ContainsKey(cPtr))
			{
				IRichPresenceChangeListener.listeners[cPtr].OnRichPresenceChangeFailure((IRichPresenceChangeListener.FailureReason)failureReason);
			}
		}

		// Token: 0x04000268 RID: 616
		private static Dictionary<IntPtr, IRichPresenceChangeListener> listeners = new Dictionary<IntPtr, IRichPresenceChangeListener>();

		// Token: 0x04000269 RID: 617
		private HandleRef swigCPtr;

		// Token: 0x0400026A RID: 618
		private IRichPresenceChangeListener.SwigDelegateIRichPresenceChangeListener_0 swigDelegate0;

		// Token: 0x0400026B RID: 619
		private IRichPresenceChangeListener.SwigDelegateIRichPresenceChangeListener_1 swigDelegate1;

		// Token: 0x0400026C RID: 620
		private static Type[] swigMethodTypes0 = new Type[0];

		// Token: 0x0400026D RID: 621
		private static Type[] swigMethodTypes1 = new Type[] { typeof(IRichPresenceChangeListener.FailureReason) };

		// Token: 0x0200014D RID: 333
		// (Invoke) Token: 0x06000C86 RID: 3206
		public delegate void SwigDelegateIRichPresenceChangeListener_0(IntPtr cPtr);

		// Token: 0x0200014E RID: 334
		// (Invoke) Token: 0x06000C8A RID: 3210
		public delegate void SwigDelegateIRichPresenceChangeListener_1(IntPtr cPtr, int failureReason);

		// Token: 0x0200014F RID: 335
		public enum FailureReason
		{
			// Token: 0x0400026F RID: 623
			FAILURE_REASON_UNDEFINED,
			// Token: 0x04000270 RID: 624
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
