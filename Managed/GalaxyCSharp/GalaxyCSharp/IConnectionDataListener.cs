using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000D3 RID: 211
	public abstract class IConnectionDataListener : GalaxyTypeAwareListenerConnectionData
	{
		// Token: 0x06000943 RID: 2371 RVA: 0x0000D9AC File Offset: 0x0000BBAC
		internal IConnectionDataListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IConnectionDataListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IConnectionDataListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x0000D9D4 File Offset: 0x0000BBD4
		public IConnectionDataListener()
			: this(GalaxyInstancePINVOKE.new_IConnectionDataListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x0000D9F8 File Offset: 0x0000BBF8
		internal static HandleRef getCPtr(IConnectionDataListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x0000DA18 File Offset: 0x0000BC18
		~IConnectionDataListener()
		{
			this.Dispose();
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x0000DA48 File Offset: 0x0000BC48
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IConnectionDataListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IConnectionDataListener.listeners.ContainsKey(handle))
					{
						IConnectionDataListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000948 RID: 2376
		public abstract void OnConnectionDataReceived(ulong connectionID, uint dataSize);

		// Token: 0x06000949 RID: 2377 RVA: 0x0000DAF8 File Offset: 0x0000BCF8
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnConnectionDataReceived", IConnectionDataListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IConnectionDataListener.SwigDelegateIConnectionDataListener_0(IConnectionDataListener.SwigDirectorOnConnectionDataReceived);
			}
			GalaxyInstancePINVOKE.IConnectionDataListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x0000DB34 File Offset: 0x0000BD34
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IConnectionDataListener));
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x0000DB6A File Offset: 0x0000BD6A
		[MonoPInvokeCallback(typeof(IConnectionDataListener.SwigDelegateIConnectionDataListener_0))]
		private static void SwigDirectorOnConnectionDataReceived(IntPtr cPtr, ulong connectionID, uint dataSize)
		{
			if (IConnectionDataListener.listeners.ContainsKey(cPtr))
			{
				IConnectionDataListener.listeners[cPtr].OnConnectionDataReceived(connectionID, dataSize);
			}
		}

		// Token: 0x0400014E RID: 334
		private static Dictionary<IntPtr, IConnectionDataListener> listeners = new Dictionary<IntPtr, IConnectionDataListener>();

		// Token: 0x0400014F RID: 335
		private HandleRef swigCPtr;

		// Token: 0x04000150 RID: 336
		private IConnectionDataListener.SwigDelegateIConnectionDataListener_0 swigDelegate0;

		// Token: 0x04000151 RID: 337
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(ulong),
			typeof(uint)
		};

		// Token: 0x020000D4 RID: 212
		// (Invoke) Token: 0x0600094E RID: 2382
		public delegate void SwigDelegateIConnectionDataListener_0(IntPtr cPtr, ulong connectionID, uint dataSize);
	}
}
