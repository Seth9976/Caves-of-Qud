using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000073 RID: 115
	public abstract class GlobalEncryptedAppTicketListener : IEncryptedAppTicketListener
	{
		// Token: 0x0600072F RID: 1839 RVA: 0x0000E0BC File Offset: 0x0000C2BC
		internal GlobalEncryptedAppTicketListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalEncryptedAppTicketListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x0000E0D8 File Offset: 0x0000C2D8
		public GlobalEncryptedAppTicketListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerEncryptedAppTicket.GetListenerType(), this);
			}
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x0000E0FA File Offset: 0x0000C2FA
		internal static HandleRef getCPtr(GlobalEncryptedAppTicketListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0000E118 File Offset: 0x0000C318
		~GlobalEncryptedAppTicketListener()
		{
			this.Dispose();
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0000E148 File Offset: 0x0000C348
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerEncryptedAppTicket.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalEncryptedAppTicketListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x04000088 RID: 136
		private HandleRef swigCPtr;
	}
}
