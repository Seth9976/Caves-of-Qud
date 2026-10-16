using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000037 RID: 55
	public abstract class GalaxyTypeAwareListenerLobbyDataRetrieve : IGalaxyListener
	{
		// Token: 0x060005E3 RID: 1507 RVA: 0x00005840 File Offset: 0x00003A40
		internal GalaxyTypeAwareListenerLobbyDataRetrieve(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLobbyDataRetrieve_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0000585C File Offset: 0x00003A5C
		public GalaxyTypeAwareListenerLobbyDataRetrieve()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerLobbyDataRetrieve(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0000587A File Offset: 0x00003A7A
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerLobbyDataRetrieve obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00005898 File Offset: 0x00003A98
		~GalaxyTypeAwareListenerLobbyDataRetrieve()
		{
			this.Dispose();
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x000058C8 File Offset: 0x00003AC8
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerLobbyDataRetrieve(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00005950 File Offset: 0x00003B50
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLobbyDataRetrieve_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x0400004B RID: 75
		private HandleRef swigCPtr;
	}
}
