using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000F5 RID: 245
	public abstract class IFriendInvitationSendListener : GalaxyTypeAwareListenerFriendInvitationSend
	{
		// Token: 0x060009FA RID: 2554 RVA: 0x0000F880 File Offset: 0x0000DA80
		internal IFriendInvitationSendListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IFriendInvitationSendListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IFriendInvitationSendListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x0000F8A8 File Offset: 0x0000DAA8
		public IFriendInvitationSendListener()
			: this(GalaxyInstancePINVOKE.new_IFriendInvitationSendListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x0000F8CC File Offset: 0x0000DACC
		internal static HandleRef getCPtr(IFriendInvitationSendListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x0000F8EC File Offset: 0x0000DAEC
		~IFriendInvitationSendListener()
		{
			this.Dispose();
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x0000F91C File Offset: 0x0000DB1C
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IFriendInvitationSendListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IFriendInvitationSendListener.listeners.ContainsKey(handle))
					{
						IFriendInvitationSendListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x060009FF RID: 2559
		public abstract void OnFriendInvitationSendSuccess(GalaxyID userID);

		// Token: 0x06000A00 RID: 2560
		public abstract void OnFriendInvitationSendFailure(GalaxyID userID, IFriendInvitationSendListener.FailureReason failureReason);

		// Token: 0x06000A01 RID: 2561 RVA: 0x0000F9CC File Offset: 0x0000DBCC
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnFriendInvitationSendSuccess", IFriendInvitationSendListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IFriendInvitationSendListener.SwigDelegateIFriendInvitationSendListener_0(IFriendInvitationSendListener.SwigDirectorOnFriendInvitationSendSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnFriendInvitationSendFailure", IFriendInvitationSendListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new IFriendInvitationSendListener.SwigDelegateIFriendInvitationSendListener_1(IFriendInvitationSendListener.SwigDirectorOnFriendInvitationSendFailure);
			}
			GalaxyInstancePINVOKE.IFriendInvitationSendListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x0000FA40 File Offset: 0x0000DC40
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IFriendInvitationSendListener));
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x0000FA76 File Offset: 0x0000DC76
		[MonoPInvokeCallback(typeof(IFriendInvitationSendListener.SwigDelegateIFriendInvitationSendListener_0))]
		private static void SwigDirectorOnFriendInvitationSendSuccess(IntPtr cPtr, IntPtr userID)
		{
			if (IFriendInvitationSendListener.listeners.ContainsKey(cPtr))
			{
				IFriendInvitationSendListener.listeners[cPtr].OnFriendInvitationSendSuccess(new GalaxyID(new GalaxyID(userID, false).ToUint64()));
			}
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x0000FAA9 File Offset: 0x0000DCA9
		[MonoPInvokeCallback(typeof(IFriendInvitationSendListener.SwigDelegateIFriendInvitationSendListener_1))]
		private static void SwigDirectorOnFriendInvitationSendFailure(IntPtr cPtr, IntPtr userID, int failureReason)
		{
			if (IFriendInvitationSendListener.listeners.ContainsKey(cPtr))
			{
				IFriendInvitationSendListener.listeners[cPtr].OnFriendInvitationSendFailure(new GalaxyID(new GalaxyID(userID, false).ToUint64()), (IFriendInvitationSendListener.FailureReason)failureReason);
			}
		}

		// Token: 0x040001A0 RID: 416
		private static Dictionary<IntPtr, IFriendInvitationSendListener> listeners = new Dictionary<IntPtr, IFriendInvitationSendListener>();

		// Token: 0x040001A1 RID: 417
		private HandleRef swigCPtr;

		// Token: 0x040001A2 RID: 418
		private IFriendInvitationSendListener.SwigDelegateIFriendInvitationSendListener_0 swigDelegate0;

		// Token: 0x040001A3 RID: 419
		private IFriendInvitationSendListener.SwigDelegateIFriendInvitationSendListener_1 swigDelegate1;

		// Token: 0x040001A4 RID: 420
		private static Type[] swigMethodTypes0 = new Type[] { typeof(GalaxyID) };

		// Token: 0x040001A5 RID: 421
		private static Type[] swigMethodTypes1 = new Type[]
		{
			typeof(GalaxyID),
			typeof(IFriendInvitationSendListener.FailureReason)
		};

		// Token: 0x020000F6 RID: 246
		// (Invoke) Token: 0x06000A07 RID: 2567
		public delegate void SwigDelegateIFriendInvitationSendListener_0(IntPtr cPtr, IntPtr userID);

		// Token: 0x020000F7 RID: 247
		// (Invoke) Token: 0x06000A0B RID: 2571
		public delegate void SwigDelegateIFriendInvitationSendListener_1(IntPtr cPtr, IntPtr userID, int failureReason);

		// Token: 0x020000F8 RID: 248
		public enum FailureReason
		{
			// Token: 0x040001A7 RID: 423
			FAILURE_REASON_UNDEFINED,
			// Token: 0x040001A8 RID: 424
			FAILURE_REASON_USER_DOES_NOT_EXIST,
			// Token: 0x040001A9 RID: 425
			FAILURE_REASON_USER_ALREADY_INVITED,
			// Token: 0x040001AA RID: 426
			FAILURE_REASON_USER_ALREADY_FRIEND,
			// Token: 0x040001AB RID: 427
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
