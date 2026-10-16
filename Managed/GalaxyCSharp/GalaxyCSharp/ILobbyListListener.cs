using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000129 RID: 297
	public abstract class ILobbyListListener : GalaxyTypeAwareListenerLobbyList
	{
		// Token: 0x06000B51 RID: 2897 RVA: 0x00011E6C File Offset: 0x0001006C
		internal ILobbyListListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ILobbyListListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			ILobbyListListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x00011E94 File Offset: 0x00010094
		public ILobbyListListener()
			: this(GalaxyInstancePINVOKE.new_ILobbyListListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x00011EB8 File Offset: 0x000100B8
		internal static HandleRef getCPtr(ILobbyListListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x00011ED8 File Offset: 0x000100D8
		~ILobbyListListener()
		{
			this.Dispose();
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x00011F08 File Offset: 0x00010108
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ILobbyListListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (ILobbyListListener.listeners.ContainsKey(handle))
					{
						ILobbyListListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000B56 RID: 2902
		public abstract void OnLobbyList(uint lobbyCount, LobbyListResult _result);

		// Token: 0x06000B57 RID: 2903 RVA: 0x00011FB8 File Offset: 0x000101B8
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnLobbyList", ILobbyListListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new ILobbyListListener.SwigDelegateILobbyListListener_0(ILobbyListListener.SwigDirectorOnLobbyList);
			}
			GalaxyInstancePINVOKE.ILobbyListListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x00011FF4 File Offset: 0x000101F4
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ILobbyListListener));
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x0001202A File Offset: 0x0001022A
		[MonoPInvokeCallback(typeof(ILobbyListListener.SwigDelegateILobbyListListener_0))]
		private static void SwigDirectorOnLobbyList(IntPtr cPtr, uint lobbyCount, int _result)
		{
			if (ILobbyListListener.listeners.ContainsKey(cPtr))
			{
				ILobbyListListener.listeners[cPtr].OnLobbyList(lobbyCount, (LobbyListResult)_result);
			}
		}

		// Token: 0x04000219 RID: 537
		private static Dictionary<IntPtr, ILobbyListListener> listeners = new Dictionary<IntPtr, ILobbyListListener>();

		// Token: 0x0400021A RID: 538
		private HandleRef swigCPtr;

		// Token: 0x0400021B RID: 539
		private ILobbyListListener.SwigDelegateILobbyListListener_0 swigDelegate0;

		// Token: 0x0400021C RID: 540
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(uint),
			typeof(LobbyListResult)
		};

		// Token: 0x0200012A RID: 298
		// (Invoke) Token: 0x06000B5C RID: 2908
		public delegate void SwigDelegateILobbyListListener_0(IntPtr cPtr, uint lobbyCount, int _result);
	}
}
