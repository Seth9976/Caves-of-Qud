using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200003B RID: 59
	public abstract class GalaxyTypeAwareListenerLobbyList : IGalaxyListener
	{
		// Token: 0x060005FB RID: 1531 RVA: 0x00005D10 File Offset: 0x00003F10
		internal GalaxyTypeAwareListenerLobbyList(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLobbyList_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00005D2C File Offset: 0x00003F2C
		public GalaxyTypeAwareListenerLobbyList()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerLobbyList(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x00005D4A File Offset: 0x00003F4A
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerLobbyList obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00005D68 File Offset: 0x00003F68
		~GalaxyTypeAwareListenerLobbyList()
		{
			this.Dispose();
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00005D98 File Offset: 0x00003F98
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerLobbyList(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00005E20 File Offset: 0x00004020
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLobbyList_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x0400004F RID: 79
		private HandleRef swigCPtr;
	}
}
