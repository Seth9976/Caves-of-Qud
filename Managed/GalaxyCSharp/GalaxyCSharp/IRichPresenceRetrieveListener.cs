using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000152 RID: 338
	public abstract class IRichPresenceRetrieveListener : GalaxyTypeAwareListenerRichPresenceRetrieve
	{
		// Token: 0x06000C9B RID: 3227 RVA: 0x0000B264 File Offset: 0x00009464
		internal IRichPresenceRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IRichPresenceRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IRichPresenceRetrieveListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x0000B28C File Offset: 0x0000948C
		public IRichPresenceRetrieveListener()
			: this(GalaxyInstancePINVOKE.new_IRichPresenceRetrieveListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x0000B2B0 File Offset: 0x000094B0
		internal static HandleRef getCPtr(IRichPresenceRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x0000B2D0 File Offset: 0x000094D0
		~IRichPresenceRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x0000B300 File Offset: 0x00009500
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IRichPresenceRetrieveListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IRichPresenceRetrieveListener.listeners.ContainsKey(handle))
					{
						IRichPresenceRetrieveListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x0000B3B0 File Offset: 0x000095B0
		public virtual void OnRichPresenceRetrieveSuccess(GalaxyID userID)
		{
			GalaxyInstancePINVOKE.IRichPresenceRetrieveListener_OnRichPresenceRetrieveSuccess(this.swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x0000B3D3 File Offset: 0x000095D3
		public virtual void OnRichPresenceRetrieveFailure(GalaxyID userID, IRichPresenceRetrieveListener.FailureReason failureReason)
		{
			GalaxyInstancePINVOKE.IRichPresenceRetrieveListener_OnRichPresenceRetrieveFailure(this.swigCPtr, GalaxyID.getCPtr(userID), (int)failureReason);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x0000B3F8 File Offset: 0x000095F8
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnRichPresenceRetrieveSuccess", IRichPresenceRetrieveListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IRichPresenceRetrieveListener.SwigDelegateIRichPresenceRetrieveListener_0(IRichPresenceRetrieveListener.SwigDirectorOnRichPresenceRetrieveSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnRichPresenceRetrieveFailure", IRichPresenceRetrieveListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new IRichPresenceRetrieveListener.SwigDelegateIRichPresenceRetrieveListener_1(IRichPresenceRetrieveListener.SwigDirectorOnRichPresenceRetrieveFailure);
			}
			GalaxyInstancePINVOKE.IRichPresenceRetrieveListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x0000B46C File Offset: 0x0000966C
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IRichPresenceRetrieveListener));
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x0000B4A2 File Offset: 0x000096A2
		[MonoPInvokeCallback(typeof(IRichPresenceRetrieveListener.SwigDelegateIRichPresenceRetrieveListener_0))]
		private static void SwigDirectorOnRichPresenceRetrieveSuccess(IntPtr cPtr, IntPtr userID)
		{
			if (IRichPresenceRetrieveListener.listeners.ContainsKey(cPtr))
			{
				IRichPresenceRetrieveListener.listeners[cPtr].OnRichPresenceRetrieveSuccess(new GalaxyID(new GalaxyID(userID, false).ToUint64()));
			}
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x0000B4D5 File Offset: 0x000096D5
		[MonoPInvokeCallback(typeof(IRichPresenceRetrieveListener.SwigDelegateIRichPresenceRetrieveListener_1))]
		private static void SwigDirectorOnRichPresenceRetrieveFailure(IntPtr cPtr, IntPtr userID, int failureReason)
		{
			if (IRichPresenceRetrieveListener.listeners.ContainsKey(cPtr))
			{
				IRichPresenceRetrieveListener.listeners[cPtr].OnRichPresenceRetrieveFailure(new GalaxyID(new GalaxyID(userID, false).ToUint64()), (IRichPresenceRetrieveListener.FailureReason)failureReason);
			}
		}

		// Token: 0x04000275 RID: 629
		private static Dictionary<IntPtr, IRichPresenceRetrieveListener> listeners = new Dictionary<IntPtr, IRichPresenceRetrieveListener>();

		// Token: 0x04000276 RID: 630
		private HandleRef swigCPtr;

		// Token: 0x04000277 RID: 631
		private IRichPresenceRetrieveListener.SwigDelegateIRichPresenceRetrieveListener_0 swigDelegate0;

		// Token: 0x04000278 RID: 632
		private IRichPresenceRetrieveListener.SwigDelegateIRichPresenceRetrieveListener_1 swigDelegate1;

		// Token: 0x04000279 RID: 633
		private static Type[] swigMethodTypes0 = new Type[] { typeof(GalaxyID) };

		// Token: 0x0400027A RID: 634
		private static Type[] swigMethodTypes1 = new Type[]
		{
			typeof(GalaxyID),
			typeof(IRichPresenceRetrieveListener.FailureReason)
		};

		// Token: 0x02000153 RID: 339
		// (Invoke) Token: 0x06000CA8 RID: 3240
		public delegate void SwigDelegateIRichPresenceRetrieveListener_0(IntPtr cPtr, IntPtr userID);

		// Token: 0x02000154 RID: 340
		// (Invoke) Token: 0x06000CAC RID: 3244
		public delegate void SwigDelegateIRichPresenceRetrieveListener_1(IntPtr cPtr, IntPtr userID, int failureReason);

		// Token: 0x02000155 RID: 341
		public enum FailureReason
		{
			// Token: 0x0400027C RID: 636
			FAILURE_REASON_UNDEFINED,
			// Token: 0x0400027D RID: 637
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
