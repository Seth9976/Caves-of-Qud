using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200003A RID: 58
	public abstract class GalaxyTypeAwareListenerLobbyLeft : IGalaxyListener
	{
		// Token: 0x060005F5 RID: 1525 RVA: 0x00005BDC File Offset: 0x00003DDC
		internal GalaxyTypeAwareListenerLobbyLeft(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLobbyLeft_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00005BF8 File Offset: 0x00003DF8
		public GalaxyTypeAwareListenerLobbyLeft()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerLobbyLeft(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00005C16 File Offset: 0x00003E16
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerLobbyLeft obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x00005C34 File Offset: 0x00003E34
		~GalaxyTypeAwareListenerLobbyLeft()
		{
			this.Dispose();
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00005C64 File Offset: 0x00003E64
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerLobbyLeft(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x00005CEC File Offset: 0x00003EEC
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLobbyLeft_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x0400004E RID: 78
		private HandleRef swigCPtr;
	}
}
