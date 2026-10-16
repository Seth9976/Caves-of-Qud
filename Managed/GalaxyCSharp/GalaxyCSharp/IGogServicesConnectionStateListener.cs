using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000103 RID: 259
	public abstract class IGogServicesConnectionStateListener : GalaxyTypeAwareListenerGogServicesConnectionState
	{
		// Token: 0x06000A85 RID: 2693 RVA: 0x00008858 File Offset: 0x00006A58
		internal IGogServicesConnectionStateListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IGogServicesConnectionStateListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IGogServicesConnectionStateListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x00008880 File Offset: 0x00006A80
		public IGogServicesConnectionStateListener()
			: this(GalaxyInstancePINVOKE.new_IGogServicesConnectionStateListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x000088A4 File Offset: 0x00006AA4
		internal static HandleRef getCPtr(IGogServicesConnectionStateListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x000088C4 File Offset: 0x00006AC4
		~IGogServicesConnectionStateListener()
		{
			this.Dispose();
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x000088F4 File Offset: 0x00006AF4
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IGogServicesConnectionStateListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IGogServicesConnectionStateListener.listeners.ContainsKey(handle))
					{
						IGogServicesConnectionStateListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000A8A RID: 2698
		public abstract void OnConnectionStateChange(GogServicesConnectionState connectionState);

		// Token: 0x06000A8B RID: 2699 RVA: 0x000089A4 File Offset: 0x00006BA4
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnConnectionStateChange", IGogServicesConnectionStateListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IGogServicesConnectionStateListener.SwigDelegateIGogServicesConnectionStateListener_0(IGogServicesConnectionStateListener.SwigDirectorOnConnectionStateChange);
			}
			GalaxyInstancePINVOKE.IGogServicesConnectionStateListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x000089E0 File Offset: 0x00006BE0
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IGogServicesConnectionStateListener));
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x00008A16 File Offset: 0x00006C16
		[MonoPInvokeCallback(typeof(IGogServicesConnectionStateListener.SwigDelegateIGogServicesConnectionStateListener_0))]
		private static void SwigDirectorOnConnectionStateChange(IntPtr cPtr, int connectionState)
		{
			if (IGogServicesConnectionStateListener.listeners.ContainsKey(cPtr))
			{
				IGogServicesConnectionStateListener.listeners[cPtr].OnConnectionStateChange((GogServicesConnectionState)connectionState);
			}
		}

		// Token: 0x040001C2 RID: 450
		private static Dictionary<IntPtr, IGogServicesConnectionStateListener> listeners = new Dictionary<IntPtr, IGogServicesConnectionStateListener>();

		// Token: 0x040001C3 RID: 451
		private HandleRef swigCPtr;

		// Token: 0x040001C4 RID: 452
		private IGogServicesConnectionStateListener.SwigDelegateIGogServicesConnectionStateListener_0 swigDelegate0;

		// Token: 0x040001C5 RID: 453
		private static Type[] swigMethodTypes0 = new Type[] { typeof(GogServicesConnectionState) };

		// Token: 0x02000104 RID: 260
		// (Invoke) Token: 0x06000A90 RID: 2704
		public delegate void SwigDelegateIGogServicesConnectionStateListener_0(IntPtr cPtr, int connectionState);
	}
}
