using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000091 RID: 145
	public abstract class GlobalOperationalStateChangeListener : IOperationalStateChangeListener
	{
		// Token: 0x060007CA RID: 1994 RVA: 0x000130B0 File Offset: 0x000112B0
		internal GlobalOperationalStateChangeListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalOperationalStateChangeListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x000130CC File Offset: 0x000112CC
		public GlobalOperationalStateChangeListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerOperationalStateChange.GetListenerType(), this);
			}
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x000130EE File Offset: 0x000112EE
		internal static HandleRef getCPtr(GlobalOperationalStateChangeListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x0001310C File Offset: 0x0001130C
		~GlobalOperationalStateChangeListener()
		{
			this.Dispose();
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0001313C File Offset: 0x0001133C
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerOperationalStateChange.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalOperationalStateChangeListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000AB RID: 171
		private HandleRef swigCPtr;
	}
}
