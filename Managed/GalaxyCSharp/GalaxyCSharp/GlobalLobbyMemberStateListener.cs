using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200008B RID: 139
	public abstract class GlobalLobbyMemberStateListener : ILobbyMemberStateListener
	{
		// Token: 0x060007AC RID: 1964 RVA: 0x000122D8 File Offset: 0x000104D8
		internal GlobalLobbyMemberStateListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalLobbyMemberStateListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x000122F4 File Offset: 0x000104F4
		public GlobalLobbyMemberStateListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerLobbyMemberState.GetListenerType(), this);
			}
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x00012316 File Offset: 0x00010516
		internal static HandleRef getCPtr(GlobalLobbyMemberStateListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x00012334 File Offset: 0x00010534
		~GlobalLobbyMemberStateListener()
		{
			this.Dispose();
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x00012364 File Offset: 0x00010564
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerLobbyMemberState.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalLobbyMemberStateListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000A5 RID: 165
		private HandleRef swigCPtr;
	}
}
