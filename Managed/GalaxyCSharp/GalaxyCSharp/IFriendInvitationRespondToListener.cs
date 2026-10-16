using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000F1 RID: 241
	public abstract class IFriendInvitationRespondToListener : GalaxyTypeAwareListenerFriendInvitationRespondTo
	{
		// Token: 0x060009E6 RID: 2534 RVA: 0x0000F450 File Offset: 0x0000D650
		internal IFriendInvitationRespondToListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IFriendInvitationRespondToListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IFriendInvitationRespondToListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x0000F478 File Offset: 0x0000D678
		public IFriendInvitationRespondToListener()
			: this(GalaxyInstancePINVOKE.new_IFriendInvitationRespondToListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x0000F49C File Offset: 0x0000D69C
		internal static HandleRef getCPtr(IFriendInvitationRespondToListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x0000F4BC File Offset: 0x0000D6BC
		~IFriendInvitationRespondToListener()
		{
			this.Dispose();
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x0000F4EC File Offset: 0x0000D6EC
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IFriendInvitationRespondToListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IFriendInvitationRespondToListener.listeners.ContainsKey(handle))
					{
						IFriendInvitationRespondToListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x060009EB RID: 2539
		public abstract void OnFriendInvitationRespondToSuccess(GalaxyID userID, bool accept);

		// Token: 0x060009EC RID: 2540
		public abstract void OnFriendInvitationRespondToFailure(GalaxyID userID, IFriendInvitationRespondToListener.FailureReason failureReason);

		// Token: 0x060009ED RID: 2541 RVA: 0x0000F59C File Offset: 0x0000D79C
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnFriendInvitationRespondToSuccess", IFriendInvitationRespondToListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IFriendInvitationRespondToListener.SwigDelegateIFriendInvitationRespondToListener_0(IFriendInvitationRespondToListener.SwigDirectorOnFriendInvitationRespondToSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnFriendInvitationRespondToFailure", IFriendInvitationRespondToListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new IFriendInvitationRespondToListener.SwigDelegateIFriendInvitationRespondToListener_1(IFriendInvitationRespondToListener.SwigDirectorOnFriendInvitationRespondToFailure);
			}
			GalaxyInstancePINVOKE.IFriendInvitationRespondToListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x0000F610 File Offset: 0x0000D810
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IFriendInvitationRespondToListener));
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x0000F646 File Offset: 0x0000D846
		[MonoPInvokeCallback(typeof(IFriendInvitationRespondToListener.SwigDelegateIFriendInvitationRespondToListener_0))]
		private static void SwigDirectorOnFriendInvitationRespondToSuccess(IntPtr cPtr, IntPtr userID, bool accept)
		{
			if (IFriendInvitationRespondToListener.listeners.ContainsKey(cPtr))
			{
				IFriendInvitationRespondToListener.listeners[cPtr].OnFriendInvitationRespondToSuccess(new GalaxyID(new GalaxyID(userID, false).ToUint64()), accept);
			}
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x0000F67A File Offset: 0x0000D87A
		[MonoPInvokeCallback(typeof(IFriendInvitationRespondToListener.SwigDelegateIFriendInvitationRespondToListener_1))]
		private static void SwigDirectorOnFriendInvitationRespondToFailure(IntPtr cPtr, IntPtr userID, int failureReason)
		{
			if (IFriendInvitationRespondToListener.listeners.ContainsKey(cPtr))
			{
				IFriendInvitationRespondToListener.listeners[cPtr].OnFriendInvitationRespondToFailure(new GalaxyID(new GalaxyID(userID, false).ToUint64()), (IFriendInvitationRespondToListener.FailureReason)failureReason);
			}
		}

		// Token: 0x04000194 RID: 404
		private static Dictionary<IntPtr, IFriendInvitationRespondToListener> listeners = new Dictionary<IntPtr, IFriendInvitationRespondToListener>();

		// Token: 0x04000195 RID: 405
		private HandleRef swigCPtr;

		// Token: 0x04000196 RID: 406
		private IFriendInvitationRespondToListener.SwigDelegateIFriendInvitationRespondToListener_0 swigDelegate0;

		// Token: 0x04000197 RID: 407
		private IFriendInvitationRespondToListener.SwigDelegateIFriendInvitationRespondToListener_1 swigDelegate1;

		// Token: 0x04000198 RID: 408
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(GalaxyID),
			typeof(bool)
		};

		// Token: 0x04000199 RID: 409
		private static Type[] swigMethodTypes1 = new Type[]
		{
			typeof(GalaxyID),
			typeof(IFriendInvitationRespondToListener.FailureReason)
		};

		// Token: 0x020000F2 RID: 242
		// (Invoke) Token: 0x060009F3 RID: 2547
		public delegate void SwigDelegateIFriendInvitationRespondToListener_0(IntPtr cPtr, IntPtr userID, bool accept);

		// Token: 0x020000F3 RID: 243
		// (Invoke) Token: 0x060009F7 RID: 2551
		public delegate void SwigDelegateIFriendInvitationRespondToListener_1(IntPtr cPtr, IntPtr userID, int failureReason);

		// Token: 0x020000F4 RID: 244
		public enum FailureReason
		{
			// Token: 0x0400019B RID: 411
			FAILURE_REASON_UNDEFINED,
			// Token: 0x0400019C RID: 412
			FAILURE_REASON_USER_DOES_NOT_EXIST,
			// Token: 0x0400019D RID: 413
			FAILURE_REASON_FRIEND_INVITATION_DOES_NOT_EXIST,
			// Token: 0x0400019E RID: 414
			FAILURE_REASON_USER_ALREADY_FRIEND,
			// Token: 0x0400019F RID: 415
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
