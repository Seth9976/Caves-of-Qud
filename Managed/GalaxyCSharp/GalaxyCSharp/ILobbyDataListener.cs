using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200011A RID: 282
	public abstract class ILobbyDataListener : GalaxyTypeAwareListenerLobbyData
	{
		// Token: 0x06000AFF RID: 2815 RVA: 0x00008EDC File Offset: 0x000070DC
		internal ILobbyDataListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ILobbyDataListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			ILobbyDataListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x00008F04 File Offset: 0x00007104
		public ILobbyDataListener()
			: this(GalaxyInstancePINVOKE.new_ILobbyDataListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x00008F28 File Offset: 0x00007128
		internal static HandleRef getCPtr(ILobbyDataListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x00008F48 File Offset: 0x00007148
		~ILobbyDataListener()
		{
			this.Dispose();
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x00008F78 File Offset: 0x00007178
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ILobbyDataListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (ILobbyDataListener.listeners.ContainsKey(handle))
					{
						ILobbyDataListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000B04 RID: 2820
		public abstract void OnLobbyDataUpdated(GalaxyID lobbyID, GalaxyID memberID);

		// Token: 0x06000B05 RID: 2821 RVA: 0x00009028 File Offset: 0x00007228
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnLobbyDataUpdated", ILobbyDataListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new ILobbyDataListener.SwigDelegateILobbyDataListener_0(ILobbyDataListener.SwigDirectorOnLobbyDataUpdated);
			}
			GalaxyInstancePINVOKE.ILobbyDataListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x00009064 File Offset: 0x00007264
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ILobbyDataListener));
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x0000909C File Offset: 0x0000729C
		[MonoPInvokeCallback(typeof(ILobbyDataListener.SwigDelegateILobbyDataListener_0))]
		private static void SwigDirectorOnLobbyDataUpdated(IntPtr cPtr, IntPtr lobbyID, IntPtr memberID)
		{
			if (ILobbyDataListener.listeners.ContainsKey(cPtr))
			{
				ILobbyDataListener.listeners[cPtr].OnLobbyDataUpdated(new GalaxyID(new GalaxyID(lobbyID, false).ToUint64()), new GalaxyID(new GalaxyID(memberID, false).ToUint64()));
			}
		}

		// Token: 0x040001F4 RID: 500
		private static Dictionary<IntPtr, ILobbyDataListener> listeners = new Dictionary<IntPtr, ILobbyDataListener>();

		// Token: 0x040001F5 RID: 501
		private HandleRef swigCPtr;

		// Token: 0x040001F6 RID: 502
		private ILobbyDataListener.SwigDelegateILobbyDataListener_0 swigDelegate0;

		// Token: 0x040001F7 RID: 503
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(GalaxyID),
			typeof(GalaxyID)
		};

		// Token: 0x0200011B RID: 283
		// (Invoke) Token: 0x06000B0A RID: 2826
		public delegate void SwigDelegateILobbyDataListener_0(IntPtr cPtr, IntPtr lobbyID, IntPtr memberID);
	}
}
