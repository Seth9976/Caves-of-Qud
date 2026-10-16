using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000097 RID: 151
	public abstract class GlobalRichPresenceListener : IRichPresenceListener
	{
		// Token: 0x060007E9 RID: 2025 RVA: 0x000144C1 File Offset: 0x000126C1
		internal GlobalRichPresenceListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalRichPresenceListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x000144DD File Offset: 0x000126DD
		public GlobalRichPresenceListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerRichPresence.GetListenerType(), this);
			}
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x000144FF File Offset: 0x000126FF
		internal static HandleRef getCPtr(GlobalRichPresenceListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x00014520 File Offset: 0x00012720
		~GlobalRichPresenceListener()
		{
			this.Dispose();
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x00014550 File Offset: 0x00012750
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerRichPresence.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalRichPresenceListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000B2 RID: 178
		private HandleRef swigCPtr;
	}
}
