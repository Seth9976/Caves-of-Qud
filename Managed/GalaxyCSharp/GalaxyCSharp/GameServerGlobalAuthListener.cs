using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000057 RID: 87
	public abstract class GameServerGlobalAuthListener : IAuthListener
	{
		// Token: 0x060006A2 RID: 1698 RVA: 0x00008373 File Offset: 0x00006573
		internal GameServerGlobalAuthListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GameServerGlobalAuthListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0000838F File Offset: 0x0000658F
		public GameServerGlobalAuthListener()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Register(GalaxyTypeAwareListenerAuth.GetListenerType(), this);
			}
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x000083B1 File Offset: 0x000065B1
		internal static HandleRef getCPtr(GameServerGlobalAuthListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x000083D0 File Offset: 0x000065D0
		~GameServerGlobalAuthListener()
		{
			this.Dispose();
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x00008400 File Offset: 0x00006600
		public override void Dispose()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Unregister(GalaxyTypeAwareListenerAuth.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GameServerGlobalAuthListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0400006B RID: 107
		private HandleRef swigCPtr;
	}
}
