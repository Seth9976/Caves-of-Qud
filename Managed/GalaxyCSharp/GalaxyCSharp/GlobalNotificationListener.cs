using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000090 RID: 144
	public abstract class GlobalNotificationListener : INotificationListener
	{
		// Token: 0x060007C5 RID: 1989 RVA: 0x00012F81 File Offset: 0x00011181
		internal GlobalNotificationListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalNotificationListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00012F9D File Offset: 0x0001119D
		public GlobalNotificationListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerNotification.GetListenerType(), this);
			}
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x00012FBF File Offset: 0x000111BF
		internal static HandleRef getCPtr(GlobalNotificationListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x00012FE0 File Offset: 0x000111E0
		~GlobalNotificationListener()
		{
			this.Dispose();
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x00013010 File Offset: 0x00011210
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerNotification.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalNotificationListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000AA RID: 170
		private HandleRef swigCPtr;
	}
}
