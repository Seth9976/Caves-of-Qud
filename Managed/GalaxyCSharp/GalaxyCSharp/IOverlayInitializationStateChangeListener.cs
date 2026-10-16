using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000145 RID: 325
	public abstract class IOverlayInitializationStateChangeListener : GalaxyTypeAwareListenerOverlayInitializationStateChange
	{
		// Token: 0x06000C4F RID: 3151 RVA: 0x00013500 File Offset: 0x00011700
		internal IOverlayInitializationStateChangeListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IOverlayInitializationStateChangeListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IOverlayInitializationStateChangeListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x00013528 File Offset: 0x00011728
		public IOverlayInitializationStateChangeListener()
			: this(GalaxyInstancePINVOKE.new_IOverlayInitializationStateChangeListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x0001354C File Offset: 0x0001174C
		internal static HandleRef getCPtr(IOverlayInitializationStateChangeListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x0001356C File Offset: 0x0001176C
		~IOverlayInitializationStateChangeListener()
		{
			this.Dispose();
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x0001359C File Offset: 0x0001179C
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IOverlayInitializationStateChangeListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IOverlayInitializationStateChangeListener.listeners.ContainsKey(handle))
					{
						IOverlayInitializationStateChangeListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000C54 RID: 3156
		public abstract void OnOverlayStateChanged(OverlayState overlayState);

		// Token: 0x06000C55 RID: 3157 RVA: 0x0001364C File Offset: 0x0001184C
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnOverlayStateChanged", IOverlayInitializationStateChangeListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IOverlayInitializationStateChangeListener.SwigDelegateIOverlayInitializationStateChangeListener_0(IOverlayInitializationStateChangeListener.SwigDirectorOnOverlayStateChanged);
			}
			GalaxyInstancePINVOKE.IOverlayInitializationStateChangeListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x00013688 File Offset: 0x00011888
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IOverlayInitializationStateChangeListener));
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x000136BE File Offset: 0x000118BE
		[MonoPInvokeCallback(typeof(IOverlayInitializationStateChangeListener.SwigDelegateIOverlayInitializationStateChangeListener_0))]
		private static void SwigDirectorOnOverlayStateChanged(IntPtr cPtr, int overlayState)
		{
			if (IOverlayInitializationStateChangeListener.listeners.ContainsKey(cPtr))
			{
				IOverlayInitializationStateChangeListener.listeners[cPtr].OnOverlayStateChanged((OverlayState)overlayState);
			}
		}

		// Token: 0x04000254 RID: 596
		private static Dictionary<IntPtr, IOverlayInitializationStateChangeListener> listeners = new Dictionary<IntPtr, IOverlayInitializationStateChangeListener>();

		// Token: 0x04000255 RID: 597
		private HandleRef swigCPtr;

		// Token: 0x04000256 RID: 598
		private IOverlayInitializationStateChangeListener.SwigDelegateIOverlayInitializationStateChangeListener_0 swigDelegate0;

		// Token: 0x04000257 RID: 599
		private static Type[] swigMethodTypes0 = new Type[] { typeof(OverlayState) };

		// Token: 0x02000146 RID: 326
		// (Invoke) Token: 0x06000C5A RID: 3162
		public delegate void SwigDelegateIOverlayInitializationStateChangeListener_0(IntPtr cPtr, int overlayState);
	}
}
