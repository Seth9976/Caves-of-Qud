using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000D0 RID: 208
	public abstract class IConnectionCloseListener : GalaxyTypeAwareListenerConnectionClose
	{
		// Token: 0x06000935 RID: 2357 RVA: 0x0000D66C File Offset: 0x0000B86C
		internal IConnectionCloseListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IConnectionCloseListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IConnectionCloseListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x0000D694 File Offset: 0x0000B894
		public IConnectionCloseListener()
			: this(GalaxyInstancePINVOKE.new_IConnectionCloseListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x0000D6B8 File Offset: 0x0000B8B8
		internal static HandleRef getCPtr(IConnectionCloseListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0000D6D8 File Offset: 0x0000B8D8
		~IConnectionCloseListener()
		{
			this.Dispose();
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x0000D708 File Offset: 0x0000B908
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IConnectionCloseListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IConnectionCloseListener.listeners.ContainsKey(handle))
					{
						IConnectionCloseListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0600093A RID: 2362
		public abstract void OnConnectionClosed(ulong connectionID, IConnectionCloseListener.CloseReason closeReason);

		// Token: 0x0600093B RID: 2363 RVA: 0x0000D7B8 File Offset: 0x0000B9B8
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnConnectionClosed", IConnectionCloseListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IConnectionCloseListener.SwigDelegateIConnectionCloseListener_0(IConnectionCloseListener.SwigDirectorOnConnectionClosed);
			}
			GalaxyInstancePINVOKE.IConnectionCloseListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x0000D7F4 File Offset: 0x0000B9F4
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IConnectionCloseListener));
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x0000D82A File Offset: 0x0000BA2A
		[MonoPInvokeCallback(typeof(IConnectionCloseListener.SwigDelegateIConnectionCloseListener_0))]
		private static void SwigDirectorOnConnectionClosed(IntPtr cPtr, ulong connectionID, int closeReason)
		{
			if (IConnectionCloseListener.listeners.ContainsKey(cPtr))
			{
				IConnectionCloseListener.listeners[cPtr].OnConnectionClosed(connectionID, (IConnectionCloseListener.CloseReason)closeReason);
			}
		}

		// Token: 0x04000148 RID: 328
		private static Dictionary<IntPtr, IConnectionCloseListener> listeners = new Dictionary<IntPtr, IConnectionCloseListener>();

		// Token: 0x04000149 RID: 329
		private HandleRef swigCPtr;

		// Token: 0x0400014A RID: 330
		private IConnectionCloseListener.SwigDelegateIConnectionCloseListener_0 swigDelegate0;

		// Token: 0x0400014B RID: 331
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(ulong),
			typeof(IConnectionCloseListener.CloseReason)
		};

		// Token: 0x020000D1 RID: 209
		// (Invoke) Token: 0x06000940 RID: 2368
		public delegate void SwigDelegateIConnectionCloseListener_0(IntPtr cPtr, ulong connectionID, int closeReason);

		// Token: 0x020000D2 RID: 210
		public enum CloseReason
		{
			// Token: 0x0400014D RID: 333
			CLOSE_REASON_UNDEFINED
		}
	}
}
