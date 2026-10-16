using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000053 RID: 83
	public abstract class GalaxyTypeAwareListenerUserInformationRetrieve : IGalaxyListener
	{
		// Token: 0x0600068B RID: 1675 RVA: 0x000079F0 File Offset: 0x00005BF0
		internal GalaxyTypeAwareListenerUserInformationRetrieve(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerUserInformationRetrieve_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x00007A0C File Offset: 0x00005C0C
		public GalaxyTypeAwareListenerUserInformationRetrieve()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerUserInformationRetrieve(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00007A2A File Offset: 0x00005C2A
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerUserInformationRetrieve obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x00007A48 File Offset: 0x00005C48
		~GalaxyTypeAwareListenerUserInformationRetrieve()
		{
			this.Dispose();
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x00007A78 File Offset: 0x00005C78
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerUserInformationRetrieve(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x00007B00 File Offset: 0x00005D00
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerUserInformationRetrieve_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000067 RID: 103
		private HandleRef swigCPtr;
	}
}
