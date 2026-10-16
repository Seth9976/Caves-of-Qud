using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200008F RID: 143
	public abstract class GlobalNetworkingListener : INetworkingListener
	{
		// Token: 0x060007C0 RID: 1984 RVA: 0x00012C34 File Offset: 0x00010E34
		internal GlobalNetworkingListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalNetworkingListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x00012C50 File Offset: 0x00010E50
		public GlobalNetworkingListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerNetworking.GetListenerType(), this);
			}
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x00012C72 File Offset: 0x00010E72
		internal static HandleRef getCPtr(GlobalNetworkingListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x00012C90 File Offset: 0x00010E90
		~GlobalNetworkingListener()
		{
			this.Dispose();
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00012CC0 File Offset: 0x00010EC0
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerNetworking.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalNetworkingListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000A9 RID: 169
		private HandleRef swigCPtr;
	}
}
