using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000093 RID: 147
	public abstract class GlobalOverlayInitializationStateChangeListener : IOverlayInitializationStateChangeListener
	{
		// Token: 0x060007D4 RID: 2004 RVA: 0x00013705 File Offset: 0x00011905
		internal GlobalOverlayInitializationStateChangeListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalOverlayInitializationStateChangeListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			GlobalOverlayInitializationStateChangeListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0001372D File Offset: 0x0001192D
		public GlobalOverlayInitializationStateChangeListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerOverlayInitializationStateChange.GetListenerType(), this);
			}
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x0001374F File Offset: 0x0001194F
		internal static HandleRef getCPtr(GlobalOverlayInitializationStateChangeListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x00013770 File Offset: 0x00011970
		~GlobalOverlayInitializationStateChangeListener()
		{
			this.Dispose();
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x000137A0 File Offset: 0x000119A0
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerOverlayInitializationStateChange.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalOverlayInitializationStateChangeListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (GlobalOverlayInitializationStateChangeListener.listeners.ContainsKey(handle))
					{
						GlobalOverlayInitializationStateChangeListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000AD RID: 173
		private static Dictionary<IntPtr, GlobalOverlayInitializationStateChangeListener> listeners = new Dictionary<IntPtr, GlobalOverlayInitializationStateChangeListener>();

		// Token: 0x040000AE RID: 174
		private HandleRef swigCPtr;
	}
}
