using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000A3 RID: 163
	public abstract class GlobalUserTimePlayedRetrieveListener : IUserTimePlayedRetrieveListener
	{
		// Token: 0x06000828 RID: 2088 RVA: 0x000167F4 File Offset: 0x000149F4
		internal GlobalUserTimePlayedRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalUserTimePlayedRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x00016810 File Offset: 0x00014A10
		public GlobalUserTimePlayedRetrieveListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerUserTimePlayedRetrieve.GetListenerType(), this);
			}
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x00016832 File Offset: 0x00014A32
		internal static HandleRef getCPtr(GlobalUserTimePlayedRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x00016850 File Offset: 0x00014A50
		~GlobalUserTimePlayedRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x00016880 File Offset: 0x00014A80
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerUserTimePlayedRetrieve.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalUserTimePlayedRetrieveListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000C1 RID: 193
		private HandleRef swigCPtr;
	}
}
