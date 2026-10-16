using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000094 RID: 148
	public abstract class GlobalOverlayVisibilityChangeListener : IOverlayVisibilityChangeListener
	{
		// Token: 0x060007DA RID: 2010 RVA: 0x00013A95 File Offset: 0x00011C95
		internal GlobalOverlayVisibilityChangeListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalOverlayVisibilityChangeListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x00013AB1 File Offset: 0x00011CB1
		public GlobalOverlayVisibilityChangeListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerOverlayVisibilityChange.GetListenerType(), this);
			}
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00013AD3 File Offset: 0x00011CD3
		internal static HandleRef getCPtr(GlobalOverlayVisibilityChangeListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x00013AF4 File Offset: 0x00011CF4
		~GlobalOverlayVisibilityChangeListener()
		{
			this.Dispose();
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00013B24 File Offset: 0x00011D24
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerOverlayVisibilityChange.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalOverlayVisibilityChangeListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000AF RID: 175
		private HandleRef swigCPtr;
	}
}
