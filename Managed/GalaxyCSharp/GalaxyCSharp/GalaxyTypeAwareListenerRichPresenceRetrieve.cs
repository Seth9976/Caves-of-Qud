using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200004A RID: 74
	public abstract class GalaxyTypeAwareListenerRichPresenceRetrieve : IGalaxyListener
	{
		// Token: 0x06000655 RID: 1621 RVA: 0x00006F1C File Offset: 0x0000511C
		internal GalaxyTypeAwareListenerRichPresenceRetrieve(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerRichPresenceRetrieve_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00006F38 File Offset: 0x00005138
		public GalaxyTypeAwareListenerRichPresenceRetrieve()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerRichPresenceRetrieve(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x00006F56 File Offset: 0x00005156
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerRichPresenceRetrieve obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00006F74 File Offset: 0x00005174
		~GalaxyTypeAwareListenerRichPresenceRetrieve()
		{
			this.Dispose();
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00006FA4 File Offset: 0x000051A4
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerRichPresenceRetrieve(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x0000702C File Offset: 0x0000522C
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerRichPresenceRetrieve_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x0400005E RID: 94
		private HandleRef swigCPtr;
	}
}
