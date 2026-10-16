using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000147 RID: 327
	public abstract class IOverlayVisibilityChangeListener : GalaxyTypeAwareListenerOverlayVisibilityChange
	{
		// Token: 0x06000C5D RID: 3165 RVA: 0x00013874 File Offset: 0x00011A74
		internal IOverlayVisibilityChangeListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IOverlayVisibilityChangeListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IOverlayVisibilityChangeListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x0001389C File Offset: 0x00011A9C
		public IOverlayVisibilityChangeListener()
			: this(GalaxyInstancePINVOKE.new_IOverlayVisibilityChangeListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x000138C0 File Offset: 0x00011AC0
		internal static HandleRef getCPtr(IOverlayVisibilityChangeListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x000138E0 File Offset: 0x00011AE0
		~IOverlayVisibilityChangeListener()
		{
			this.Dispose();
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x00013910 File Offset: 0x00011B10
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IOverlayVisibilityChangeListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IOverlayVisibilityChangeListener.listeners.ContainsKey(handle))
					{
						IOverlayVisibilityChangeListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x000139C0 File Offset: 0x00011BC0
		public virtual void OnOverlayVisibilityChanged(bool overlayVisible)
		{
			GalaxyInstancePINVOKE.IOverlayVisibilityChangeListener_OnOverlayVisibilityChanged(this.swigCPtr, overlayVisible);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x000139DE File Offset: 0x00011BDE
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnOverlayVisibilityChanged", IOverlayVisibilityChangeListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IOverlayVisibilityChangeListener.SwigDelegateIOverlayVisibilityChangeListener_0(IOverlayVisibilityChangeListener.SwigDirectorOnOverlayVisibilityChanged);
			}
			GalaxyInstancePINVOKE.IOverlayVisibilityChangeListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x00013A18 File Offset: 0x00011C18
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IOverlayVisibilityChangeListener));
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x00013A4E File Offset: 0x00011C4E
		[MonoPInvokeCallback(typeof(IOverlayVisibilityChangeListener.SwigDelegateIOverlayVisibilityChangeListener_0))]
		private static void SwigDirectorOnOverlayVisibilityChanged(IntPtr cPtr, bool overlayVisible)
		{
			if (IOverlayVisibilityChangeListener.listeners.ContainsKey(cPtr))
			{
				IOverlayVisibilityChangeListener.listeners[cPtr].OnOverlayVisibilityChanged(overlayVisible);
			}
		}

		// Token: 0x04000258 RID: 600
		private static Dictionary<IntPtr, IOverlayVisibilityChangeListener> listeners = new Dictionary<IntPtr, IOverlayVisibilityChangeListener>();

		// Token: 0x04000259 RID: 601
		private HandleRef swigCPtr;

		// Token: 0x0400025A RID: 602
		private IOverlayVisibilityChangeListener.SwigDelegateIOverlayVisibilityChangeListener_0 swigDelegate0;

		// Token: 0x0400025B RID: 603
		private static Type[] swigMethodTypes0 = new Type[] { typeof(bool) };

		// Token: 0x02000148 RID: 328
		// (Invoke) Token: 0x06000C68 RID: 3176
		public delegate void SwigDelegateIOverlayVisibilityChangeListener_0(IntPtr cPtr, bool overlayVisible);
	}
}
