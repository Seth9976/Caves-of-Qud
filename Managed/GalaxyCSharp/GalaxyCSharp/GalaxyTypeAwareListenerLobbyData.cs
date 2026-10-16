using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000036 RID: 54
	public abstract class GalaxyTypeAwareListenerLobbyData : IGalaxyListener
	{
		// Token: 0x060005DD RID: 1501 RVA: 0x0000570C File Offset: 0x0000390C
		internal GalaxyTypeAwareListenerLobbyData(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLobbyData_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00005728 File Offset: 0x00003928
		public GalaxyTypeAwareListenerLobbyData()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerLobbyData(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x00005746 File Offset: 0x00003946
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerLobbyData obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x00005764 File Offset: 0x00003964
		~GalaxyTypeAwareListenerLobbyData()
		{
			this.Dispose();
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00005794 File Offset: 0x00003994
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerLobbyData(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0000581C File Offset: 0x00003A1C
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLobbyData_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x0400004A RID: 74
		private HandleRef swigCPtr;
	}
}
