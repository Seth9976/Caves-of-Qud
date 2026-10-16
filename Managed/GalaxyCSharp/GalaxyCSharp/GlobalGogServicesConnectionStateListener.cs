using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200007E RID: 126
	public abstract class GlobalGogServicesConnectionStateListener : IGogServicesConnectionStateListener
	{
		// Token: 0x0600076B RID: 1899 RVA: 0x00010718 File Offset: 0x0000E918
		internal GlobalGogServicesConnectionStateListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalGogServicesConnectionStateListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x00010734 File Offset: 0x0000E934
		public GlobalGogServicesConnectionStateListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerGogServicesConnectionState.GetListenerType(), this);
			}
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x00010756 File Offset: 0x0000E956
		internal static HandleRef getCPtr(GlobalGogServicesConnectionStateListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x00010774 File Offset: 0x0000E974
		~GlobalGogServicesConnectionStateListener()
		{
			this.Dispose();
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x000107A4 File Offset: 0x0000E9A4
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerGogServicesConnectionState.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalGogServicesConnectionStateListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x04000098 RID: 152
		private HandleRef swigCPtr;
	}
}
