using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000131 RID: 305
	public abstract class ILobbyMessageListener : GalaxyTypeAwareListenerLobbyMessage
	{
		// Token: 0x06000B81 RID: 2945 RVA: 0x0000A858 File Offset: 0x00008A58
		internal ILobbyMessageListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ILobbyMessageListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			ILobbyMessageListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x0000A880 File Offset: 0x00008A80
		public ILobbyMessageListener()
			: this(GalaxyInstancePINVOKE.new_ILobbyMessageListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x0000A8A4 File Offset: 0x00008AA4
		internal static HandleRef getCPtr(ILobbyMessageListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0000A8C4 File Offset: 0x00008AC4
		~ILobbyMessageListener()
		{
			this.Dispose();
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0000A8F4 File Offset: 0x00008AF4
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ILobbyMessageListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (ILobbyMessageListener.listeners.ContainsKey(handle))
					{
						ILobbyMessageListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000B86 RID: 2950
		public abstract void OnLobbyMessageReceived(GalaxyID lobbyID, GalaxyID senderID, uint messageID, uint messageLength);

		// Token: 0x06000B87 RID: 2951 RVA: 0x0000A9A4 File Offset: 0x00008BA4
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnLobbyMessageReceived", ILobbyMessageListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new ILobbyMessageListener.SwigDelegateILobbyMessageListener_0(ILobbyMessageListener.SwigDirectorOnLobbyMessageReceived);
			}
			GalaxyInstancePINVOKE.ILobbyMessageListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x0000A9E0 File Offset: 0x00008BE0
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ILobbyMessageListener));
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x0000AA18 File Offset: 0x00008C18
		[MonoPInvokeCallback(typeof(ILobbyMessageListener.SwigDelegateILobbyMessageListener_0))]
		private static void SwigDirectorOnLobbyMessageReceived(IntPtr cPtr, IntPtr lobbyID, IntPtr senderID, uint messageID, uint messageLength)
		{
			if (ILobbyMessageListener.listeners.ContainsKey(cPtr))
			{
				ILobbyMessageListener.listeners[cPtr].OnLobbyMessageReceived(new GalaxyID(new GalaxyID(lobbyID, false).ToUint64()), new GalaxyID(new GalaxyID(senderID, false).ToUint64()), messageID, messageLength);
			}
		}

		// Token: 0x0400022B RID: 555
		private static Dictionary<IntPtr, ILobbyMessageListener> listeners = new Dictionary<IntPtr, ILobbyMessageListener>();

		// Token: 0x0400022C RID: 556
		private HandleRef swigCPtr;

		// Token: 0x0400022D RID: 557
		private ILobbyMessageListener.SwigDelegateILobbyMessageListener_0 swigDelegate0;

		// Token: 0x0400022E RID: 558
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(GalaxyID),
			typeof(GalaxyID),
			typeof(uint),
			typeof(uint)
		};

		// Token: 0x02000132 RID: 306
		// (Invoke) Token: 0x06000B8C RID: 2956
		public delegate void SwigDelegateILobbyMessageListener_0(IntPtr cPtr, IntPtr lobbyID, IntPtr senderID, uint messageID, uint messageLength);
	}
}
