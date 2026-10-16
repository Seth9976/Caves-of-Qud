using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200001B RID: 27
	public abstract class GalaxyTypeAwareListenerChatRoomMessageSend : IGalaxyListener
	{
		// Token: 0x0600053B RID: 1339 RVA: 0x00003690 File Offset: 0x00001890
		internal GalaxyTypeAwareListenerChatRoomMessageSend(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerChatRoomMessageSend_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x000036AC File Offset: 0x000018AC
		public GalaxyTypeAwareListenerChatRoomMessageSend()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerChatRoomMessageSend(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x000036CA File Offset: 0x000018CA
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerChatRoomMessageSend obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x000036E8 File Offset: 0x000018E8
		~GalaxyTypeAwareListenerChatRoomMessageSend()
		{
			this.Dispose();
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00003718 File Offset: 0x00001918
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerChatRoomMessageSend(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x000037A0 File Offset: 0x000019A0
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerChatRoomMessageSend_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x0400002F RID: 47
		private HandleRef swigCPtr;
	}
}
