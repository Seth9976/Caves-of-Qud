using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000076 RID: 118
	public abstract class GlobalFriendDeleteListener : IFriendDeleteListener
	{
		// Token: 0x0600073E RID: 1854 RVA: 0x0000EBBC File Offset: 0x0000CDBC
		internal GlobalFriendDeleteListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalFriendDeleteListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0000EBD8 File Offset: 0x0000CDD8
		public GlobalFriendDeleteListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerFriendDelete.GetListenerType(), this);
			}
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0000EBFA File Offset: 0x0000CDFA
		internal static HandleRef getCPtr(GlobalFriendDeleteListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x0000EC18 File Offset: 0x0000CE18
		~GlobalFriendDeleteListener()
		{
			this.Dispose();
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0000EC48 File Offset: 0x0000CE48
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerFriendDelete.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalFriendDeleteListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0400008B RID: 139
		private HandleRef swigCPtr;
	}
}
