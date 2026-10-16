using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000FF RID: 255
	public abstract class IGameInvitationReceivedListener : GalaxyTypeAwareListenerGameInvitationReceived
	{
		// Token: 0x06000A69 RID: 2665 RVA: 0x00010038 File Offset: 0x0000E238
		internal IGameInvitationReceivedListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IGameInvitationReceivedListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IGameInvitationReceivedListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x00010060 File Offset: 0x0000E260
		public IGameInvitationReceivedListener()
			: this(GalaxyInstancePINVOKE.new_IGameInvitationReceivedListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x00010084 File Offset: 0x0000E284
		internal static HandleRef getCPtr(IGameInvitationReceivedListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x000100A4 File Offset: 0x0000E2A4
		~IGameInvitationReceivedListener()
		{
			this.Dispose();
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x000100D4 File Offset: 0x0000E2D4
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IGameInvitationReceivedListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IGameInvitationReceivedListener.listeners.ContainsKey(handle))
					{
						IGameInvitationReceivedListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000A6E RID: 2670
		public abstract void OnGameInvitationReceived(GalaxyID userID, string connectionString);

		// Token: 0x06000A6F RID: 2671 RVA: 0x00010184 File Offset: 0x0000E384
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnGameInvitationReceived", IGameInvitationReceivedListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IGameInvitationReceivedListener.SwigDelegateIGameInvitationReceivedListener_0(IGameInvitationReceivedListener.SwigDirectorOnGameInvitationReceived);
			}
			GalaxyInstancePINVOKE.IGameInvitationReceivedListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x000101C0 File Offset: 0x0000E3C0
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IGameInvitationReceivedListener));
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x000101F6 File Offset: 0x0000E3F6
		[MonoPInvokeCallback(typeof(IGameInvitationReceivedListener.SwigDelegateIGameInvitationReceivedListener_0))]
		private static void SwigDirectorOnGameInvitationReceived(IntPtr cPtr, IntPtr userID, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string connectionString)
		{
			if (IGameInvitationReceivedListener.listeners.ContainsKey(cPtr))
			{
				IGameInvitationReceivedListener.listeners[cPtr].OnGameInvitationReceived(new GalaxyID(new GalaxyID(userID, false).ToUint64()), connectionString);
			}
		}

		// Token: 0x040001BA RID: 442
		private static Dictionary<IntPtr, IGameInvitationReceivedListener> listeners = new Dictionary<IntPtr, IGameInvitationReceivedListener>();

		// Token: 0x040001BB RID: 443
		private HandleRef swigCPtr;

		// Token: 0x040001BC RID: 444
		private IGameInvitationReceivedListener.SwigDelegateIGameInvitationReceivedListener_0 swigDelegate0;

		// Token: 0x040001BD RID: 445
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(GalaxyID),
			typeof(string)
		};

		// Token: 0x02000100 RID: 256
		// (Invoke) Token: 0x06000A74 RID: 2676
		public delegate void SwigDelegateIGameInvitationReceivedListener_0(IntPtr cPtr, IntPtr userID, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string connectionString);
	}
}
