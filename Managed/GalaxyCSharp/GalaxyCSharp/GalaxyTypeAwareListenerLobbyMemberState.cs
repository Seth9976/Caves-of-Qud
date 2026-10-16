using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200003D RID: 61
	public abstract class GalaxyTypeAwareListenerLobbyMemberState : IGalaxyListener
	{
		// Token: 0x06000607 RID: 1543 RVA: 0x00005F78 File Offset: 0x00004178
		internal GalaxyTypeAwareListenerLobbyMemberState(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLobbyMemberState_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00005F94 File Offset: 0x00004194
		public GalaxyTypeAwareListenerLobbyMemberState()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerLobbyMemberState(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00005FB2 File Offset: 0x000041B2
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerLobbyMemberState obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00005FD0 File Offset: 0x000041D0
		~GalaxyTypeAwareListenerLobbyMemberState()
		{
			this.Dispose();
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00006000 File Offset: 0x00004200
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerLobbyMemberState(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00006088 File Offset: 0x00004288
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLobbyMemberState_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000051 RID: 81
		private HandleRef swigCPtr;
	}
}
