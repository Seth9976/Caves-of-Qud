using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000096 RID: 150
	public abstract class GlobalRichPresenceChangeListener : IRichPresenceChangeListener
	{
		// Token: 0x060007E4 RID: 2020 RVA: 0x0001417E File Offset: 0x0001237E
		internal GlobalRichPresenceChangeListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalRichPresenceChangeListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x0001419A File Offset: 0x0001239A
		public GlobalRichPresenceChangeListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerRichPresenceChange.GetListenerType(), this);
			}
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x000141BC File Offset: 0x000123BC
		internal static HandleRef getCPtr(GlobalRichPresenceChangeListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x000141DC File Offset: 0x000123DC
		~GlobalRichPresenceChangeListener()
		{
			this.Dispose();
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x0001420C File Offset: 0x0001240C
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerRichPresenceChange.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalRichPresenceChangeListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000B1 RID: 177
		private HandleRef swigCPtr;
	}
}
