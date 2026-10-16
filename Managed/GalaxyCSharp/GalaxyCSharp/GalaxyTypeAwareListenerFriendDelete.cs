using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000028 RID: 40
	public abstract class GalaxyTypeAwareListenerFriendDelete : IGalaxyListener
	{
		// Token: 0x06000589 RID: 1417 RVA: 0x00004634 File Offset: 0x00002834
		internal GalaxyTypeAwareListenerFriendDelete(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerFriendDelete_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00004650 File Offset: 0x00002850
		public GalaxyTypeAwareListenerFriendDelete()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerFriendDelete(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0000466E File Offset: 0x0000286E
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerFriendDelete obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0000468C File Offset: 0x0000288C
		~GalaxyTypeAwareListenerFriendDelete()
		{
			this.Dispose();
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x000046BC File Offset: 0x000028BC
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerFriendDelete(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00004744 File Offset: 0x00002944
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerFriendDelete_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x0400003C RID: 60
		private HandleRef swigCPtr;
	}
}
