using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200003F RID: 63
	public abstract class GalaxyTypeAwareListenerLobbyOwnerChange : IGalaxyListener
	{
		// Token: 0x06000613 RID: 1555 RVA: 0x000061E0 File Offset: 0x000043E0
		internal GalaxyTypeAwareListenerLobbyOwnerChange(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLobbyOwnerChange_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x000061FC File Offset: 0x000043FC
		public GalaxyTypeAwareListenerLobbyOwnerChange()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerLobbyOwnerChange(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x0000621A File Offset: 0x0000441A
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerLobbyOwnerChange obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00006238 File Offset: 0x00004438
		~GalaxyTypeAwareListenerLobbyOwnerChange()
		{
			this.Dispose();
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x00006268 File Offset: 0x00004468
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerLobbyOwnerChange(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x000062F0 File Offset: 0x000044F0
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLobbyOwnerChange_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000053 RID: 83
		private HandleRef swigCPtr;
	}
}
