using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000095 RID: 149
	public abstract class GlobalPersonaDataChangedListener : IPersonaDataChangedListener
	{
		// Token: 0x060007DF RID: 2015 RVA: 0x00013DE7 File Offset: 0x00011FE7
		internal GlobalPersonaDataChangedListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalPersonaDataChangedListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x00013E03 File Offset: 0x00012003
		public GlobalPersonaDataChangedListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerPersonaDataChanged.GetListenerType(), this);
			}
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x00013E25 File Offset: 0x00012025
		internal static HandleRef getCPtr(GlobalPersonaDataChangedListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00013E44 File Offset: 0x00012044
		~GlobalPersonaDataChangedListener()
		{
			this.Dispose();
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00013E74 File Offset: 0x00012074
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerPersonaDataChanged.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalPersonaDataChangedListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000B0 RID: 176
		private HandleRef swigCPtr;
	}
}
