using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000133 RID: 307
	public abstract class ILobbyOwnerChangeListener : GalaxyTypeAwareListenerLobbyOwnerChange
	{
		// Token: 0x06000B8F RID: 2959 RVA: 0x00012530 File Offset: 0x00010730
		internal ILobbyOwnerChangeListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ILobbyOwnerChangeListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			ILobbyOwnerChangeListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x00012558 File Offset: 0x00010758
		public ILobbyOwnerChangeListener()
			: this(GalaxyInstancePINVOKE.new_ILobbyOwnerChangeListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x0001257C File Offset: 0x0001077C
		internal static HandleRef getCPtr(ILobbyOwnerChangeListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x0001259C File Offset: 0x0001079C
		~ILobbyOwnerChangeListener()
		{
			this.Dispose();
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x000125CC File Offset: 0x000107CC
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ILobbyOwnerChangeListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (ILobbyOwnerChangeListener.listeners.ContainsKey(handle))
					{
						ILobbyOwnerChangeListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000B94 RID: 2964
		public abstract void OnLobbyOwnerChanged(GalaxyID lobbyID, GalaxyID newOwnerID);

		// Token: 0x06000B95 RID: 2965 RVA: 0x0001267C File Offset: 0x0001087C
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnLobbyOwnerChanged", ILobbyOwnerChangeListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new ILobbyOwnerChangeListener.SwigDelegateILobbyOwnerChangeListener_0(ILobbyOwnerChangeListener.SwigDirectorOnLobbyOwnerChanged);
			}
			GalaxyInstancePINVOKE.ILobbyOwnerChangeListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x000126B8 File Offset: 0x000108B8
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ILobbyOwnerChangeListener));
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x000126F0 File Offset: 0x000108F0
		[MonoPInvokeCallback(typeof(ILobbyOwnerChangeListener.SwigDelegateILobbyOwnerChangeListener_0))]
		private static void SwigDirectorOnLobbyOwnerChanged(IntPtr cPtr, IntPtr lobbyID, IntPtr newOwnerID)
		{
			if (ILobbyOwnerChangeListener.listeners.ContainsKey(cPtr))
			{
				ILobbyOwnerChangeListener.listeners[cPtr].OnLobbyOwnerChanged(new GalaxyID(new GalaxyID(lobbyID, false).ToUint64()), new GalaxyID(new GalaxyID(newOwnerID, false).ToUint64()));
			}
		}

		// Token: 0x0400022F RID: 559
		private static Dictionary<IntPtr, ILobbyOwnerChangeListener> listeners = new Dictionary<IntPtr, ILobbyOwnerChangeListener>();

		// Token: 0x04000230 RID: 560
		private HandleRef swigCPtr;

		// Token: 0x04000231 RID: 561
		private ILobbyOwnerChangeListener.SwigDelegateILobbyOwnerChangeListener_0 swigDelegate0;

		// Token: 0x04000232 RID: 562
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(GalaxyID),
			typeof(GalaxyID)
		};

		// Token: 0x02000134 RID: 308
		// (Invoke) Token: 0x06000B9A RID: 2970
		public delegate void SwigDelegateILobbyOwnerChangeListener_0(IntPtr cPtr, IntPtr lobbyID, IntPtr newOwnerID);
	}
}
