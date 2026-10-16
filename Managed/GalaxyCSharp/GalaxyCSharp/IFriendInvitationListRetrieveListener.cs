using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000ED RID: 237
	public abstract class IFriendInvitationListRetrieveListener : GalaxyTypeAwareListenerFriendInvitationListRetrieve
	{
		// Token: 0x060009D2 RID: 2514 RVA: 0x0000F078 File Offset: 0x0000D278
		internal IFriendInvitationListRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IFriendInvitationListRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IFriendInvitationListRetrieveListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x0000F0A0 File Offset: 0x0000D2A0
		public IFriendInvitationListRetrieveListener()
			: this(GalaxyInstancePINVOKE.new_IFriendInvitationListRetrieveListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x0000F0C4 File Offset: 0x0000D2C4
		internal static HandleRef getCPtr(IFriendInvitationListRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x0000F0E4 File Offset: 0x0000D2E4
		~IFriendInvitationListRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x0000F114 File Offset: 0x0000D314
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IFriendInvitationListRetrieveListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IFriendInvitationListRetrieveListener.listeners.ContainsKey(handle))
					{
						IFriendInvitationListRetrieveListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x060009D7 RID: 2519
		public abstract void OnFriendInvitationListRetrieveSuccess();

		// Token: 0x060009D8 RID: 2520
		public abstract void OnFriendInvitationListRetrieveFailure(IFriendInvitationListRetrieveListener.FailureReason failureReason);

		// Token: 0x060009D9 RID: 2521 RVA: 0x0000F1C4 File Offset: 0x0000D3C4
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnFriendInvitationListRetrieveSuccess", IFriendInvitationListRetrieveListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IFriendInvitationListRetrieveListener.SwigDelegateIFriendInvitationListRetrieveListener_0(IFriendInvitationListRetrieveListener.SwigDirectorOnFriendInvitationListRetrieveSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnFriendInvitationListRetrieveFailure", IFriendInvitationListRetrieveListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new IFriendInvitationListRetrieveListener.SwigDelegateIFriendInvitationListRetrieveListener_1(IFriendInvitationListRetrieveListener.SwigDirectorOnFriendInvitationListRetrieveFailure);
			}
			GalaxyInstancePINVOKE.IFriendInvitationListRetrieveListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0000F238 File Offset: 0x0000D438
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IFriendInvitationListRetrieveListener));
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x0000F26E File Offset: 0x0000D46E
		[MonoPInvokeCallback(typeof(IFriendInvitationListRetrieveListener.SwigDelegateIFriendInvitationListRetrieveListener_0))]
		private static void SwigDirectorOnFriendInvitationListRetrieveSuccess(IntPtr cPtr)
		{
			if (IFriendInvitationListRetrieveListener.listeners.ContainsKey(cPtr))
			{
				IFriendInvitationListRetrieveListener.listeners[cPtr].OnFriendInvitationListRetrieveSuccess();
			}
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0000F290 File Offset: 0x0000D490
		[MonoPInvokeCallback(typeof(IFriendInvitationListRetrieveListener.SwigDelegateIFriendInvitationListRetrieveListener_1))]
		private static void SwigDirectorOnFriendInvitationListRetrieveFailure(IntPtr cPtr, int failureReason)
		{
			if (IFriendInvitationListRetrieveListener.listeners.ContainsKey(cPtr))
			{
				IFriendInvitationListRetrieveListener.listeners[cPtr].OnFriendInvitationListRetrieveFailure((IFriendInvitationListRetrieveListener.FailureReason)failureReason);
			}
		}

		// Token: 0x0400018B RID: 395
		private static Dictionary<IntPtr, IFriendInvitationListRetrieveListener> listeners = new Dictionary<IntPtr, IFriendInvitationListRetrieveListener>();

		// Token: 0x0400018C RID: 396
		private HandleRef swigCPtr;

		// Token: 0x0400018D RID: 397
		private IFriendInvitationListRetrieveListener.SwigDelegateIFriendInvitationListRetrieveListener_0 swigDelegate0;

		// Token: 0x0400018E RID: 398
		private IFriendInvitationListRetrieveListener.SwigDelegateIFriendInvitationListRetrieveListener_1 swigDelegate1;

		// Token: 0x0400018F RID: 399
		private static Type[] swigMethodTypes0 = new Type[0];

		// Token: 0x04000190 RID: 400
		private static Type[] swigMethodTypes1 = new Type[] { typeof(IFriendInvitationListRetrieveListener.FailureReason) };

		// Token: 0x020000EE RID: 238
		// (Invoke) Token: 0x060009DF RID: 2527
		public delegate void SwigDelegateIFriendInvitationListRetrieveListener_0(IntPtr cPtr);

		// Token: 0x020000EF RID: 239
		// (Invoke) Token: 0x060009E3 RID: 2531
		public delegate void SwigDelegateIFriendInvitationListRetrieveListener_1(IntPtr cPtr, int failureReason);

		// Token: 0x020000F0 RID: 240
		public enum FailureReason
		{
			// Token: 0x04000192 RID: 402
			FAILURE_REASON_UNDEFINED,
			// Token: 0x04000193 RID: 403
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
