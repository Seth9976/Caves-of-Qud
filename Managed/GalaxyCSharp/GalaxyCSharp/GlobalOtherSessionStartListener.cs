using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000092 RID: 146
	public abstract class GlobalOtherSessionStartListener : IOtherSessionStartListener
	{
		// Token: 0x060007CF RID: 1999 RVA: 0x000133D3 File Offset: 0x000115D3
		internal GlobalOtherSessionStartListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalOtherSessionStartListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x000133EF File Offset: 0x000115EF
		public GlobalOtherSessionStartListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerOtherSessionStart.GetListenerType(), this);
			}
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x00013411 File Offset: 0x00011611
		internal static HandleRef getCPtr(GlobalOtherSessionStartListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x00013430 File Offset: 0x00011630
		~GlobalOtherSessionStartListener()
		{
			this.Dispose();
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x00013460 File Offset: 0x00011660
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerOtherSessionStart.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalOtherSessionStartListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000AC RID: 172
		private HandleRef swigCPtr;
	}
}
