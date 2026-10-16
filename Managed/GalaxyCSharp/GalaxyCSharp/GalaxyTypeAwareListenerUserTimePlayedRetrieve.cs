using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000055 RID: 85
	public abstract class GalaxyTypeAwareListenerUserTimePlayedRetrieve : IGalaxyListener
	{
		// Token: 0x06000697 RID: 1687 RVA: 0x00007C58 File Offset: 0x00005E58
		internal GalaxyTypeAwareListenerUserTimePlayedRetrieve(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerUserTimePlayedRetrieve_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x00007C74 File Offset: 0x00005E74
		public GalaxyTypeAwareListenerUserTimePlayedRetrieve()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerUserTimePlayedRetrieve(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x00007C92 File Offset: 0x00005E92
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerUserTimePlayedRetrieve obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x00007CB0 File Offset: 0x00005EB0
		~GalaxyTypeAwareListenerUserTimePlayedRetrieve()
		{
			this.Dispose();
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x00007CE0 File Offset: 0x00005EE0
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerUserTimePlayedRetrieve(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x00007D68 File Offset: 0x00005F68
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerUserTimePlayedRetrieve_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000069 RID: 105
		private HandleRef swigCPtr;
	}
}
