using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000EB RID: 235
	public abstract class IFriendInvitationListener : GalaxyTypeAwareListenerFriendInvitation
	{
		// Token: 0x060009C4 RID: 2500 RVA: 0x0000ECE8 File Offset: 0x0000CEE8
		internal IFriendInvitationListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IFriendInvitationListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IFriendInvitationListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x0000ED10 File Offset: 0x0000CF10
		public IFriendInvitationListener()
			: this(GalaxyInstancePINVOKE.new_IFriendInvitationListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x0000ED34 File Offset: 0x0000CF34
		internal static HandleRef getCPtr(IFriendInvitationListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x0000ED54 File Offset: 0x0000CF54
		~IFriendInvitationListener()
		{
			this.Dispose();
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0000ED84 File Offset: 0x0000CF84
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IFriendInvitationListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IFriendInvitationListener.listeners.ContainsKey(handle))
					{
						IFriendInvitationListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x060009C9 RID: 2505
		public abstract void OnFriendInvitationReceived(GalaxyID userID, uint sendTime);

		// Token: 0x060009CA RID: 2506 RVA: 0x0000EE34 File Offset: 0x0000D034
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnFriendInvitationReceived", IFriendInvitationListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IFriendInvitationListener.SwigDelegateIFriendInvitationListener_0(IFriendInvitationListener.SwigDirectorOnFriendInvitationReceived);
			}
			GalaxyInstancePINVOKE.IFriendInvitationListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x0000EE70 File Offset: 0x0000D070
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IFriendInvitationListener));
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x0000EEA6 File Offset: 0x0000D0A6
		[MonoPInvokeCallback(typeof(IFriendInvitationListener.SwigDelegateIFriendInvitationListener_0))]
		private static void SwigDirectorOnFriendInvitationReceived(IntPtr cPtr, IntPtr userID, uint sendTime)
		{
			if (IFriendInvitationListener.listeners.ContainsKey(cPtr))
			{
				IFriendInvitationListener.listeners[cPtr].OnFriendInvitationReceived(new GalaxyID(new GalaxyID(userID, false).ToUint64()), sendTime);
			}
		}

		// Token: 0x04000187 RID: 391
		private static Dictionary<IntPtr, IFriendInvitationListener> listeners = new Dictionary<IntPtr, IFriendInvitationListener>();

		// Token: 0x04000188 RID: 392
		private HandleRef swigCPtr;

		// Token: 0x04000189 RID: 393
		private IFriendInvitationListener.SwigDelegateIFriendInvitationListener_0 swigDelegate0;

		// Token: 0x0400018A RID: 394
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(GalaxyID),
			typeof(uint)
		};

		// Token: 0x020000EC RID: 236
		// (Invoke) Token: 0x060009CF RID: 2511
		public delegate void SwigDelegateIFriendInvitationListener_0(IntPtr cPtr, IntPtr userID, uint sendTime);
	}
}
