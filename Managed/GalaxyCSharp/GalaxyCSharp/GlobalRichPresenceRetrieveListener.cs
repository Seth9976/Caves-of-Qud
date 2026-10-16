using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000098 RID: 152
	public abstract class GlobalRichPresenceRetrieveListener : IRichPresenceRetrieveListener
	{
		// Token: 0x060007EE RID: 2030 RVA: 0x000145F0 File Offset: 0x000127F0
		internal GlobalRichPresenceRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalRichPresenceRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x0001460C File Offset: 0x0001280C
		public GlobalRichPresenceRetrieveListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerRichPresenceRetrieve.GetListenerType(), this);
			}
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x0001462E File Offset: 0x0001282E
		internal static HandleRef getCPtr(GlobalRichPresenceRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x0001464C File Offset: 0x0001284C
		~GlobalRichPresenceRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x0001467C File Offset: 0x0001287C
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerRichPresenceRetrieve.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalRichPresenceRetrieveListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000B3 RID: 179
		private HandleRef swigCPtr;
	}
}
