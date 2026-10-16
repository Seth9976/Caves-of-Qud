using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200003E RID: 62
	public abstract class GalaxyTypeAwareListenerLobbyMessage : IGalaxyListener
	{
		// Token: 0x0600060D RID: 1549 RVA: 0x000060AC File Offset: 0x000042AC
		internal GalaxyTypeAwareListenerLobbyMessage(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLobbyMessage_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x000060C8 File Offset: 0x000042C8
		public GalaxyTypeAwareListenerLobbyMessage()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerLobbyMessage(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x000060E6 File Offset: 0x000042E6
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerLobbyMessage obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x00006104 File Offset: 0x00004304
		~GalaxyTypeAwareListenerLobbyMessage()
		{
			this.Dispose();
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00006134 File Offset: 0x00004334
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerLobbyMessage(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x000061BC File Offset: 0x000043BC
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLobbyMessage_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000052 RID: 82
		private HandleRef swigCPtr;
	}
}
