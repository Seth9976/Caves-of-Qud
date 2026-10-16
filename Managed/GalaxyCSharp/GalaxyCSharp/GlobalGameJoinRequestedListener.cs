using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200007D RID: 125
	public abstract class GlobalGameJoinRequestedListener : IGameJoinRequestedListener
	{
		// Token: 0x06000766 RID: 1894 RVA: 0x000105EB File Offset: 0x0000E7EB
		internal GlobalGameJoinRequestedListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalGameJoinRequestedListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00010607 File Offset: 0x0000E807
		public GlobalGameJoinRequestedListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerGameJoinRequested.GetListenerType(), this);
			}
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x00010629 File Offset: 0x0000E829
		internal static HandleRef getCPtr(GlobalGameJoinRequestedListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x00010648 File Offset: 0x0000E848
		~GlobalGameJoinRequestedListener()
		{
			this.Dispose();
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x00010678 File Offset: 0x0000E878
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerGameJoinRequested.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalGameJoinRequestedListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x04000097 RID: 151
		private HandleRef swigCPtr;
	}
}
