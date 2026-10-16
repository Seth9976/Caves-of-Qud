using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000088 RID: 136
	public abstract class GlobalLobbyLeftListener : ILobbyLeftListener
	{
		// Token: 0x0600079D RID: 1949 RVA: 0x00011D40 File Offset: 0x0000FF40
		internal GlobalLobbyLeftListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalLobbyLeftListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x00011D5C File Offset: 0x0000FF5C
		public GlobalLobbyLeftListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerLobbyLeft.GetListenerType(), this);
			}
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x00011D7E File Offset: 0x0000FF7E
		internal static HandleRef getCPtr(GlobalLobbyLeftListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x00011D9C File Offset: 0x0000FF9C
		~GlobalLobbyLeftListener()
		{
			this.Dispose();
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x00011DCC File Offset: 0x0000FFCC
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerLobbyLeft.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalLobbyLeftListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000A2 RID: 162
		private HandleRef swigCPtr;
	}
}
