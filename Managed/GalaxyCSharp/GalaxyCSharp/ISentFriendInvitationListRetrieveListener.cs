using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200015B RID: 347
	public abstract class ISentFriendInvitationListRetrieveListener : GalaxyTypeAwareListenerSentFriendInvitationListRetrieve
	{
		// Token: 0x06000CC7 RID: 3271 RVA: 0x00014B58 File Offset: 0x00012D58
		internal ISentFriendInvitationListRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ISentFriendInvitationListRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			ISentFriendInvitationListRetrieveListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x00014B80 File Offset: 0x00012D80
		public ISentFriendInvitationListRetrieveListener()
			: this(GalaxyInstancePINVOKE.new_ISentFriendInvitationListRetrieveListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x00014BA4 File Offset: 0x00012DA4
		internal static HandleRef getCPtr(ISentFriendInvitationListRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x00014BC4 File Offset: 0x00012DC4
		~ISentFriendInvitationListRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x00014BF4 File Offset: 0x00012DF4
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ISentFriendInvitationListRetrieveListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (ISentFriendInvitationListRetrieveListener.listeners.ContainsKey(handle))
					{
						ISentFriendInvitationListRetrieveListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000CCC RID: 3276
		public abstract void OnSentFriendInvitationListRetrieveSuccess();

		// Token: 0x06000CCD RID: 3277
		public abstract void OnSentFriendInvitationListRetrieveFailure(ISentFriendInvitationListRetrieveListener.FailureReason failureReason);

		// Token: 0x06000CCE RID: 3278 RVA: 0x00014CA4 File Offset: 0x00012EA4
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnSentFriendInvitationListRetrieveSuccess", ISentFriendInvitationListRetrieveListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new ISentFriendInvitationListRetrieveListener.SwigDelegateISentFriendInvitationListRetrieveListener_0(ISentFriendInvitationListRetrieveListener.SwigDirectorOnSentFriendInvitationListRetrieveSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnSentFriendInvitationListRetrieveFailure", ISentFriendInvitationListRetrieveListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new ISentFriendInvitationListRetrieveListener.SwigDelegateISentFriendInvitationListRetrieveListener_1(ISentFriendInvitationListRetrieveListener.SwigDirectorOnSentFriendInvitationListRetrieveFailure);
			}
			GalaxyInstancePINVOKE.ISentFriendInvitationListRetrieveListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x00014D18 File Offset: 0x00012F18
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ISentFriendInvitationListRetrieveListener));
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x00014D4E File Offset: 0x00012F4E
		[MonoPInvokeCallback(typeof(ISentFriendInvitationListRetrieveListener.SwigDelegateISentFriendInvitationListRetrieveListener_0))]
		private static void SwigDirectorOnSentFriendInvitationListRetrieveSuccess(IntPtr cPtr)
		{
			if (ISentFriendInvitationListRetrieveListener.listeners.ContainsKey(cPtr))
			{
				ISentFriendInvitationListRetrieveListener.listeners[cPtr].OnSentFriendInvitationListRetrieveSuccess();
			}
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x00014D70 File Offset: 0x00012F70
		[MonoPInvokeCallback(typeof(ISentFriendInvitationListRetrieveListener.SwigDelegateISentFriendInvitationListRetrieveListener_1))]
		private static void SwigDirectorOnSentFriendInvitationListRetrieveFailure(IntPtr cPtr, int failureReason)
		{
			if (ISentFriendInvitationListRetrieveListener.listeners.ContainsKey(cPtr))
			{
				ISentFriendInvitationListRetrieveListener.listeners[cPtr].OnSentFriendInvitationListRetrieveFailure((ISentFriendInvitationListRetrieveListener.FailureReason)failureReason);
			}
		}

		// Token: 0x0400028D RID: 653
		private static Dictionary<IntPtr, ISentFriendInvitationListRetrieveListener> listeners = new Dictionary<IntPtr, ISentFriendInvitationListRetrieveListener>();

		// Token: 0x0400028E RID: 654
		private HandleRef swigCPtr;

		// Token: 0x0400028F RID: 655
		private ISentFriendInvitationListRetrieveListener.SwigDelegateISentFriendInvitationListRetrieveListener_0 swigDelegate0;

		// Token: 0x04000290 RID: 656
		private ISentFriendInvitationListRetrieveListener.SwigDelegateISentFriendInvitationListRetrieveListener_1 swigDelegate1;

		// Token: 0x04000291 RID: 657
		private static Type[] swigMethodTypes0 = new Type[0];

		// Token: 0x04000292 RID: 658
		private static Type[] swigMethodTypes1 = new Type[] { typeof(ISentFriendInvitationListRetrieveListener.FailureReason) };

		// Token: 0x0200015C RID: 348
		// (Invoke) Token: 0x06000CD4 RID: 3284
		public delegate void SwigDelegateISentFriendInvitationListRetrieveListener_0(IntPtr cPtr);

		// Token: 0x0200015D RID: 349
		// (Invoke) Token: 0x06000CD8 RID: 3288
		public delegate void SwigDelegateISentFriendInvitationListRetrieveListener_1(IntPtr cPtr, int failureReason);

		// Token: 0x0200015E RID: 350
		public enum FailureReason
		{
			// Token: 0x04000294 RID: 660
			FAILURE_REASON_UNDEFINED,
			// Token: 0x04000295 RID: 661
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
