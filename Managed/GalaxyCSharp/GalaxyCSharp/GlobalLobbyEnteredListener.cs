using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000087 RID: 135
	public abstract class GlobalLobbyEnteredListener : ILobbyEnteredListener
	{
		// Token: 0x06000798 RID: 1944 RVA: 0x00011C14 File Offset: 0x0000FE14
		internal GlobalLobbyEnteredListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalLobbyEnteredListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x00011C30 File Offset: 0x0000FE30
		public GlobalLobbyEnteredListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerLobbyEntered.GetListenerType(), this);
			}
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x00011C52 File Offset: 0x0000FE52
		internal static HandleRef getCPtr(GlobalLobbyEnteredListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x00011C70 File Offset: 0x0000FE70
		~GlobalLobbyEnteredListener()
		{
			this.Dispose();
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x00011CA0 File Offset: 0x0000FEA0
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerLobbyEntered.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalLobbyEnteredListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000A1 RID: 161
		private HandleRef swigCPtr;
	}
}
