using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200009F RID: 159
	public abstract class GlobalUserDataListener : IUserDataListener
	{
		// Token: 0x06000813 RID: 2067 RVA: 0x00015AE7 File Offset: 0x00013CE7
		internal GlobalUserDataListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalUserDataListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x00015B03 File Offset: 0x00013D03
		public GlobalUserDataListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerUserData.GetListenerType(), this);
			}
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x00015B25 File Offset: 0x00013D25
		internal static HandleRef getCPtr(GlobalUserDataListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x00015B44 File Offset: 0x00013D44
		~GlobalUserDataListener()
		{
			this.Dispose();
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x00015B74 File Offset: 0x00013D74
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerUserData.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalUserDataListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000BC RID: 188
		private HandleRef swigCPtr;
	}
}
