using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000E4 RID: 228
	public abstract class IFriendAddListener : GalaxyTypeAwareListenerFriendAdd
	{
		// Token: 0x060009A2 RID: 2466 RVA: 0x0000E5B8 File Offset: 0x0000C7B8
		internal IFriendAddListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IFriendAddListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IFriendAddListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0000E5E0 File Offset: 0x0000C7E0
		public IFriendAddListener()
			: this(GalaxyInstancePINVOKE.new_IFriendAddListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x0000E604 File Offset: 0x0000C804
		internal static HandleRef getCPtr(IFriendAddListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0000E624 File Offset: 0x0000C824
		~IFriendAddListener()
		{
			this.Dispose();
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x0000E654 File Offset: 0x0000C854
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IFriendAddListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IFriendAddListener.listeners.ContainsKey(handle))
					{
						IFriendAddListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x060009A7 RID: 2471
		public abstract void OnFriendAdded(GalaxyID userID, IFriendAddListener.InvitationDirection invitationDirection);

		// Token: 0x060009A8 RID: 2472 RVA: 0x0000E704 File Offset: 0x0000C904
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnFriendAdded", IFriendAddListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IFriendAddListener.SwigDelegateIFriendAddListener_0(IFriendAddListener.SwigDirectorOnFriendAdded);
			}
			GalaxyInstancePINVOKE.IFriendAddListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x0000E740 File Offset: 0x0000C940
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IFriendAddListener));
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x0000E776 File Offset: 0x0000C976
		[MonoPInvokeCallback(typeof(IFriendAddListener.SwigDelegateIFriendAddListener_0))]
		private static void SwigDirectorOnFriendAdded(IntPtr cPtr, IntPtr userID, int invitationDirection)
		{
			if (IFriendAddListener.listeners.ContainsKey(cPtr))
			{
				IFriendAddListener.listeners[cPtr].OnFriendAdded(new GalaxyID(new GalaxyID(userID, false).ToUint64()), (IFriendAddListener.InvitationDirection)invitationDirection);
			}
		}

		// Token: 0x04000177 RID: 375
		private static Dictionary<IntPtr, IFriendAddListener> listeners = new Dictionary<IntPtr, IFriendAddListener>();

		// Token: 0x04000178 RID: 376
		private HandleRef swigCPtr;

		// Token: 0x04000179 RID: 377
		private IFriendAddListener.SwigDelegateIFriendAddListener_0 swigDelegate0;

		// Token: 0x0400017A RID: 378
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(GalaxyID),
			typeof(IFriendAddListener.InvitationDirection)
		};

		// Token: 0x020000E5 RID: 229
		// (Invoke) Token: 0x060009AD RID: 2477
		public delegate void SwigDelegateIFriendAddListener_0(IntPtr cPtr, IntPtr userID, int invitationDirection);

		// Token: 0x020000E6 RID: 230
		public enum InvitationDirection
		{
			// Token: 0x0400017C RID: 380
			INVITATION_DIRECTION_INCOMING,
			// Token: 0x0400017D RID: 381
			INVITATION_DIRECTION_OUTGOING
		}
	}
}
