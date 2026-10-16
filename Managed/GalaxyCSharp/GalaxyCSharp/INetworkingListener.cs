using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200013B RID: 315
	public abstract class INetworkingListener : GalaxyTypeAwareListenerNetworking
	{
		// Token: 0x06000C02 RID: 3074 RVA: 0x0000ABF0 File Offset: 0x00008DF0
		internal INetworkingListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.INetworkingListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			INetworkingListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x0000AC18 File Offset: 0x00008E18
		public INetworkingListener()
			: this(GalaxyInstancePINVOKE.new_INetworkingListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x0000AC3C File Offset: 0x00008E3C
		internal static HandleRef getCPtr(INetworkingListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x0000AC5C File Offset: 0x00008E5C
		~INetworkingListener()
		{
			this.Dispose();
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x0000AC8C File Offset: 0x00008E8C
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_INetworkingListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (INetworkingListener.listeners.ContainsKey(handle))
					{
						INetworkingListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000C07 RID: 3079
		public abstract void OnP2PPacketAvailable(uint msgSize, byte channel);

		// Token: 0x06000C08 RID: 3080 RVA: 0x0000AD3C File Offset: 0x00008F3C
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnP2PPacketAvailable", INetworkingListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new INetworkingListener.SwigDelegateINetworkingListener_0(INetworkingListener.SwigDirectorOnP2PPacketAvailable);
			}
			GalaxyInstancePINVOKE.INetworkingListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x0000AD78 File Offset: 0x00008F78
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(INetworkingListener));
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x0000ADAE File Offset: 0x00008FAE
		[MonoPInvokeCallback(typeof(INetworkingListener.SwigDelegateINetworkingListener_0))]
		private static void SwigDirectorOnP2PPacketAvailable(IntPtr cPtr, uint msgSize, byte channel)
		{
			if (INetworkingListener.listeners.ContainsKey(cPtr))
			{
				INetworkingListener.listeners[cPtr].OnP2PPacketAvailable(msgSize, channel);
			}
		}

		// Token: 0x0400023F RID: 575
		private static Dictionary<IntPtr, INetworkingListener> listeners = new Dictionary<IntPtr, INetworkingListener>();

		// Token: 0x04000240 RID: 576
		private HandleRef swigCPtr;

		// Token: 0x04000241 RID: 577
		private INetworkingListener.SwigDelegateINetworkingListener_0 swigDelegate0;

		// Token: 0x04000242 RID: 578
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(uint),
			typeof(byte)
		};

		// Token: 0x0200013C RID: 316
		// (Invoke) Token: 0x06000C0D RID: 3085
		public delegate void SwigDelegateINetworkingListener_0(IntPtr cPtr, uint msgSize, byte channel);
	}
}
