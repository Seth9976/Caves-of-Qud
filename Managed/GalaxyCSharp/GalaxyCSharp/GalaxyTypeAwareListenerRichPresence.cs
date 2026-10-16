using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000048 RID: 72
	public abstract class GalaxyTypeAwareListenerRichPresence : IGalaxyListener
	{
		// Token: 0x06000649 RID: 1609 RVA: 0x00006CB4 File Offset: 0x00004EB4
		internal GalaxyTypeAwareListenerRichPresence(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerRichPresence_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x00006CD0 File Offset: 0x00004ED0
		public GalaxyTypeAwareListenerRichPresence()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerRichPresence(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x00006CEE File Offset: 0x00004EEE
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerRichPresence obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x00006D0C File Offset: 0x00004F0C
		~GalaxyTypeAwareListenerRichPresence()
		{
			this.Dispose();
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x00006D3C File Offset: 0x00004F3C
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerRichPresence(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x00006DC4 File Offset: 0x00004FC4
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerRichPresence_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x0400005C RID: 92
		private HandleRef swigCPtr;
	}
}
