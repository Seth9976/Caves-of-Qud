using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000124 RID: 292
	public abstract class ILobbyEnteredListener : GalaxyTypeAwareListenerLobbyEntered
	{
		// Token: 0x06000B35 RID: 2869 RVA: 0x00009A08 File Offset: 0x00007C08
		internal ILobbyEnteredListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ILobbyEnteredListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			ILobbyEnteredListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x00009A30 File Offset: 0x00007C30
		public ILobbyEnteredListener()
			: this(GalaxyInstancePINVOKE.new_ILobbyEnteredListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x00009A54 File Offset: 0x00007C54
		internal static HandleRef getCPtr(ILobbyEnteredListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x00009A74 File Offset: 0x00007C74
		~ILobbyEnteredListener()
		{
			this.Dispose();
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x00009AA4 File Offset: 0x00007CA4
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ILobbyEnteredListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (ILobbyEnteredListener.listeners.ContainsKey(handle))
					{
						ILobbyEnteredListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000B3A RID: 2874
		public abstract void OnLobbyEntered(GalaxyID lobbyID, LobbyEnterResult _result);

		// Token: 0x06000B3B RID: 2875 RVA: 0x00009B54 File Offset: 0x00007D54
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnLobbyEntered", ILobbyEnteredListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new ILobbyEnteredListener.SwigDelegateILobbyEnteredListener_0(ILobbyEnteredListener.SwigDirectorOnLobbyEntered);
			}
			GalaxyInstancePINVOKE.ILobbyEnteredListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x00009B90 File Offset: 0x00007D90
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ILobbyEnteredListener));
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x00009BC6 File Offset: 0x00007DC6
		[MonoPInvokeCallback(typeof(ILobbyEnteredListener.SwigDelegateILobbyEnteredListener_0))]
		private static void SwigDirectorOnLobbyEntered(IntPtr cPtr, IntPtr lobbyID, int _result)
		{
			if (ILobbyEnteredListener.listeners.ContainsKey(cPtr))
			{
				ILobbyEnteredListener.listeners[cPtr].OnLobbyEntered(new GalaxyID(new GalaxyID(lobbyID, false).ToUint64()), (LobbyEnterResult)_result);
			}
		}

		// Token: 0x0400020C RID: 524
		private static Dictionary<IntPtr, ILobbyEnteredListener> listeners = new Dictionary<IntPtr, ILobbyEnteredListener>();

		// Token: 0x0400020D RID: 525
		private HandleRef swigCPtr;

		// Token: 0x0400020E RID: 526
		private ILobbyEnteredListener.SwigDelegateILobbyEnteredListener_0 swigDelegate0;

		// Token: 0x0400020F RID: 527
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(GalaxyID),
			typeof(LobbyEnterResult)
		};

		// Token: 0x02000125 RID: 293
		// (Invoke) Token: 0x06000B40 RID: 2880
		public delegate void SwigDelegateILobbyEnteredListener_0(IntPtr cPtr, IntPtr lobbyID, int _result);
	}
}
