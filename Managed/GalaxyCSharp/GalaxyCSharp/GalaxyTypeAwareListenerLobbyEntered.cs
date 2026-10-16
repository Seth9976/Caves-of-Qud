using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000039 RID: 57
	public abstract class GalaxyTypeAwareListenerLobbyEntered : IGalaxyListener
	{
		// Token: 0x060005EF RID: 1519 RVA: 0x00005AA8 File Offset: 0x00003CA8
		internal GalaxyTypeAwareListenerLobbyEntered(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLobbyEntered_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00005AC4 File Offset: 0x00003CC4
		public GalaxyTypeAwareListenerLobbyEntered()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerLobbyEntered(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x00005AE2 File Offset: 0x00003CE2
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerLobbyEntered obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x00005B00 File Offset: 0x00003D00
		~GalaxyTypeAwareListenerLobbyEntered()
		{
			this.Dispose();
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00005B30 File Offset: 0x00003D30
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerLobbyEntered(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00005BB8 File Offset: 0x00003DB8
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLobbyEntered_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x0400004D RID: 77
		private HandleRef swigCPtr;
	}
}
