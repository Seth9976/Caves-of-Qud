using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200007B RID: 123
	public abstract class GlobalFriendListListener : IFriendListListener
	{
		// Token: 0x0600075B RID: 1883 RVA: 0x0000FF0A File Offset: 0x0000E10A
		internal GlobalFriendListListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalFriendListListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x0000FF26 File Offset: 0x0000E126
		public GlobalFriendListListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerFriendList.GetListenerType(), this);
			}
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0000FF48 File Offset: 0x0000E148
		internal static HandleRef getCPtr(GlobalFriendListListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0000FF68 File Offset: 0x0000E168
		~GlobalFriendListListener()
		{
			this.Dispose();
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0000FF98 File Offset: 0x0000E198
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerFriendList.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalFriendListListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x04000094 RID: 148
		private HandleRef swigCPtr;
	}
}
