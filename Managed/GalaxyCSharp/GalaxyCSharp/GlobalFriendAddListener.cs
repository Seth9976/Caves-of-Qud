using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000075 RID: 117
	public abstract class GlobalFriendAddListener : IFriendAddListener
	{
		// Token: 0x06000739 RID: 1849 RVA: 0x0000E7DB File Offset: 0x0000C9DB
		internal GlobalFriendAddListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalFriendAddListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x0000E7F7 File Offset: 0x0000C9F7
		public GlobalFriendAddListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerFriendAdd.GetListenerType(), this);
			}
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x0000E819 File Offset: 0x0000CA19
		internal static HandleRef getCPtr(GlobalFriendAddListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x0000E838 File Offset: 0x0000CA38
		~GlobalFriendAddListener()
		{
			this.Dispose();
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x0000E868 File Offset: 0x0000CA68
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerFriendAdd.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalFriendAddListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0400008A RID: 138
		private HandleRef swigCPtr;
	}
}
