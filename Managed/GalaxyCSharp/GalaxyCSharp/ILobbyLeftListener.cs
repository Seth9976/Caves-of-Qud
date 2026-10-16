using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000126 RID: 294
	public abstract class ILobbyLeftListener : GalaxyTypeAwareListenerLobbyLeft
	{
		// Token: 0x06000B43 RID: 2883 RVA: 0x00009D58 File Offset: 0x00007F58
		internal ILobbyLeftListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ILobbyLeftListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			ILobbyLeftListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x00009D80 File Offset: 0x00007F80
		public ILobbyLeftListener()
			: this(GalaxyInstancePINVOKE.new_ILobbyLeftListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x00009DA4 File Offset: 0x00007FA4
		internal static HandleRef getCPtr(ILobbyLeftListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x00009DC4 File Offset: 0x00007FC4
		~ILobbyLeftListener()
		{
			this.Dispose();
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x00009DF4 File Offset: 0x00007FF4
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ILobbyLeftListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (ILobbyLeftListener.listeners.ContainsKey(handle))
					{
						ILobbyLeftListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000B48 RID: 2888
		public abstract void OnLobbyLeft(GalaxyID lobbyID, ILobbyLeftListener.LobbyLeaveReason leaveReason);

		// Token: 0x06000B49 RID: 2889 RVA: 0x00009EA4 File Offset: 0x000080A4
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnLobbyLeft", ILobbyLeftListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new ILobbyLeftListener.SwigDelegateILobbyLeftListener_0(ILobbyLeftListener.SwigDirectorOnLobbyLeft);
			}
			GalaxyInstancePINVOKE.ILobbyLeftListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x00009EE0 File Offset: 0x000080E0
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ILobbyLeftListener));
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x00009F16 File Offset: 0x00008116
		[MonoPInvokeCallback(typeof(ILobbyLeftListener.SwigDelegateILobbyLeftListener_0))]
		private static void SwigDirectorOnLobbyLeft(IntPtr cPtr, IntPtr lobbyID, int leaveReason)
		{
			if (ILobbyLeftListener.listeners.ContainsKey(cPtr))
			{
				ILobbyLeftListener.listeners[cPtr].OnLobbyLeft(new GalaxyID(new GalaxyID(lobbyID, false).ToUint64()), (ILobbyLeftListener.LobbyLeaveReason)leaveReason);
			}
		}

		// Token: 0x04000210 RID: 528
		private static Dictionary<IntPtr, ILobbyLeftListener> listeners = new Dictionary<IntPtr, ILobbyLeftListener>();

		// Token: 0x04000211 RID: 529
		private HandleRef swigCPtr;

		// Token: 0x04000212 RID: 530
		private ILobbyLeftListener.SwigDelegateILobbyLeftListener_0 swigDelegate0;

		// Token: 0x04000213 RID: 531
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(GalaxyID),
			typeof(ILobbyLeftListener.LobbyLeaveReason)
		};

		// Token: 0x02000127 RID: 295
		// (Invoke) Token: 0x06000B4E RID: 2894
		public delegate void SwigDelegateILobbyLeftListener_0(IntPtr cPtr, IntPtr lobbyID, int leaveReason);

		// Token: 0x02000128 RID: 296
		public enum LobbyLeaveReason
		{
			// Token: 0x04000215 RID: 533
			LOBBY_LEAVE_REASON_UNDEFINED,
			// Token: 0x04000216 RID: 534
			LOBBY_LEAVE_REASON_USER_LEFT,
			// Token: 0x04000217 RID: 535
			LOBBY_LEAVE_REASON_LOBBY_CLOSED,
			// Token: 0x04000218 RID: 536
			LOBBY_LEAVE_REASON_CONNECTION_LOST
		}
	}
}
