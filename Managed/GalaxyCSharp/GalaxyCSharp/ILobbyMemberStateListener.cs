using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200012F RID: 303
	public abstract class ILobbyMemberStateListener : GalaxyTypeAwareListenerLobbyMemberState
	{
		// Token: 0x06000B73 RID: 2931 RVA: 0x0000A4DC File Offset: 0x000086DC
		internal ILobbyMemberStateListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ILobbyMemberStateListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			ILobbyMemberStateListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x0000A504 File Offset: 0x00008704
		public ILobbyMemberStateListener()
			: this(GalaxyInstancePINVOKE.new_ILobbyMemberStateListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x0000A528 File Offset: 0x00008728
		internal static HandleRef getCPtr(ILobbyMemberStateListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x0000A548 File Offset: 0x00008748
		~ILobbyMemberStateListener()
		{
			this.Dispose();
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x0000A578 File Offset: 0x00008778
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ILobbyMemberStateListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (ILobbyMemberStateListener.listeners.ContainsKey(handle))
					{
						ILobbyMemberStateListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000B78 RID: 2936
		public abstract void OnLobbyMemberStateChanged(GalaxyID lobbyID, GalaxyID memberID, LobbyMemberStateChange memberStateChange);

		// Token: 0x06000B79 RID: 2937 RVA: 0x0000A628 File Offset: 0x00008828
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnLobbyMemberStateChanged", ILobbyMemberStateListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new ILobbyMemberStateListener.SwigDelegateILobbyMemberStateListener_0(ILobbyMemberStateListener.SwigDirectorOnLobbyMemberStateChanged);
			}
			GalaxyInstancePINVOKE.ILobbyMemberStateListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0000A664 File Offset: 0x00008864
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ILobbyMemberStateListener));
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0000A69C File Offset: 0x0000889C
		[MonoPInvokeCallback(typeof(ILobbyMemberStateListener.SwigDelegateILobbyMemberStateListener_0))]
		private static void SwigDirectorOnLobbyMemberStateChanged(IntPtr cPtr, IntPtr lobbyID, IntPtr memberID, int memberStateChange)
		{
			if (ILobbyMemberStateListener.listeners.ContainsKey(cPtr))
			{
				ILobbyMemberStateListener.listeners[cPtr].OnLobbyMemberStateChanged(new GalaxyID(new GalaxyID(lobbyID, false).ToUint64()), new GalaxyID(new GalaxyID(memberID, false).ToUint64()), (LobbyMemberStateChange)memberStateChange);
			}
		}

		// Token: 0x04000227 RID: 551
		private static Dictionary<IntPtr, ILobbyMemberStateListener> listeners = new Dictionary<IntPtr, ILobbyMemberStateListener>();

		// Token: 0x04000228 RID: 552
		private HandleRef swigCPtr;

		// Token: 0x04000229 RID: 553
		private ILobbyMemberStateListener.SwigDelegateILobbyMemberStateListener_0 swigDelegate0;

		// Token: 0x0400022A RID: 554
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(GalaxyID),
			typeof(GalaxyID),
			typeof(LobbyMemberStateChange)
		};

		// Token: 0x02000130 RID: 304
		// (Invoke) Token: 0x06000B7E RID: 2942
		public delegate void SwigDelegateILobbyMemberStateListener_0(IntPtr cPtr, IntPtr lobbyID, IntPtr memberID, int memberStateChange);
	}
}
