using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000071 RID: 113
	public abstract class GlobalConnectionDataListener : IConnectionDataListener
	{
		// Token: 0x06000725 RID: 1829 RVA: 0x0000DBBF File Offset: 0x0000BDBF
		internal GlobalConnectionDataListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalConnectionDataListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0000DBDB File Offset: 0x0000BDDB
		public GlobalConnectionDataListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerConnectionData.GetListenerType(), this);
			}
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0000DBFD File Offset: 0x0000BDFD
		internal static HandleRef getCPtr(GlobalConnectionDataListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0000DC1C File Offset: 0x0000BE1C
		~GlobalConnectionDataListener()
		{
			this.Dispose();
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0000DC4C File Offset: 0x0000BE4C
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerConnectionData.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalConnectionDataListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x04000086 RID: 134
		private HandleRef swigCPtr;
	}
}
