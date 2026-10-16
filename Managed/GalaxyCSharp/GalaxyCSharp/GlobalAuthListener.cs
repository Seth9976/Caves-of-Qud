using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200006B RID: 107
	public abstract class GlobalAuthListener : IAuthListener
	{
		// Token: 0x06000707 RID: 1799 RVA: 0x0000C62C File Offset: 0x0000A82C
		internal GlobalAuthListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalAuthListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0000C648 File Offset: 0x0000A848
		public GlobalAuthListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerAuth.GetListenerType(), this);
			}
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0000C66A File Offset: 0x0000A86A
		internal static HandleRef getCPtr(GlobalAuthListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0000C688 File Offset: 0x0000A888
		~GlobalAuthListener()
		{
			this.Dispose();
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0000C6B8 File Offset: 0x0000A8B8
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerAuth.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalAuthListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x04000080 RID: 128
		private HandleRef swigCPtr;
	}
}
