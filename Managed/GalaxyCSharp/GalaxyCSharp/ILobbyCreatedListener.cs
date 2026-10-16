using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000118 RID: 280
	public abstract class ILobbyCreatedListener : GalaxyTypeAwareListenerLobbyCreated
	{
		// Token: 0x06000AF1 RID: 2801 RVA: 0x00008B8C File Offset: 0x00006D8C
		internal ILobbyCreatedListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ILobbyCreatedListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			ILobbyCreatedListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x00008BB4 File Offset: 0x00006DB4
		public ILobbyCreatedListener()
			: this(GalaxyInstancePINVOKE.new_ILobbyCreatedListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x00008BD8 File Offset: 0x00006DD8
		internal static HandleRef getCPtr(ILobbyCreatedListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x00008BF8 File Offset: 0x00006DF8
		~ILobbyCreatedListener()
		{
			this.Dispose();
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x00008C28 File Offset: 0x00006E28
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ILobbyCreatedListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (ILobbyCreatedListener.listeners.ContainsKey(handle))
					{
						ILobbyCreatedListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000AF6 RID: 2806
		public abstract void OnLobbyCreated(GalaxyID lobbyID, LobbyCreateResult _result);

		// Token: 0x06000AF7 RID: 2807 RVA: 0x00008CD8 File Offset: 0x00006ED8
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnLobbyCreated", ILobbyCreatedListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new ILobbyCreatedListener.SwigDelegateILobbyCreatedListener_0(ILobbyCreatedListener.SwigDirectorOnLobbyCreated);
			}
			GalaxyInstancePINVOKE.ILobbyCreatedListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x00008D14 File Offset: 0x00006F14
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ILobbyCreatedListener));
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x00008D4A File Offset: 0x00006F4A
		[MonoPInvokeCallback(typeof(ILobbyCreatedListener.SwigDelegateILobbyCreatedListener_0))]
		private static void SwigDirectorOnLobbyCreated(IntPtr cPtr, IntPtr lobbyID, int _result)
		{
			if (ILobbyCreatedListener.listeners.ContainsKey(cPtr))
			{
				ILobbyCreatedListener.listeners[cPtr].OnLobbyCreated(new GalaxyID(new GalaxyID(lobbyID, false).ToUint64()), (LobbyCreateResult)_result);
			}
		}

		// Token: 0x040001F0 RID: 496
		private static Dictionary<IntPtr, ILobbyCreatedListener> listeners = new Dictionary<IntPtr, ILobbyCreatedListener>();

		// Token: 0x040001F1 RID: 497
		private HandleRef swigCPtr;

		// Token: 0x040001F2 RID: 498
		private ILobbyCreatedListener.SwigDelegateILobbyCreatedListener_0 swigDelegate0;

		// Token: 0x040001F3 RID: 499
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(GalaxyID),
			typeof(LobbyCreateResult)
		};

		// Token: 0x02000119 RID: 281
		// (Invoke) Token: 0x06000AFC RID: 2812
		public delegate void SwigDelegateILobbyCreatedListener_0(IntPtr cPtr, IntPtr lobbyID, int _result);
	}
}
