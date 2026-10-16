using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000047 RID: 71
	public abstract class GalaxyTypeAwareListenerPersonaDataChanged : IGalaxyListener
	{
		// Token: 0x06000643 RID: 1603 RVA: 0x00006B80 File Offset: 0x00004D80
		internal GalaxyTypeAwareListenerPersonaDataChanged(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerPersonaDataChanged_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00006B9C File Offset: 0x00004D9C
		public GalaxyTypeAwareListenerPersonaDataChanged()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerPersonaDataChanged(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x00006BBA File Offset: 0x00004DBA
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerPersonaDataChanged obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x00006BD8 File Offset: 0x00004DD8
		~GalaxyTypeAwareListenerPersonaDataChanged()
		{
			this.Dispose();
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x00006C08 File Offset: 0x00004E08
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerPersonaDataChanged(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x00006C90 File Offset: 0x00004E90
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerPersonaDataChanged_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x0400005B RID: 91
		private HandleRef swigCPtr;
	}
}
