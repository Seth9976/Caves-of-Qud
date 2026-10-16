using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000056 RID: 86
	public abstract class GameServerGlobalAccessTokenListener : IAccessTokenListener
	{
		// Token: 0x0600069D RID: 1693 RVA: 0x00007F83 File Offset: 0x00006183
		internal GameServerGlobalAccessTokenListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GameServerGlobalAccessTokenListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x00007F9F File Offset: 0x0000619F
		public GameServerGlobalAccessTokenListener()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Register(GalaxyTypeAwareListenerAccessToken.GetListenerType(), this);
			}
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x00007FC1 File Offset: 0x000061C1
		internal static HandleRef getCPtr(GameServerGlobalAccessTokenListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x00007FE0 File Offset: 0x000061E0
		~GameServerGlobalAccessTokenListener()
		{
			this.Dispose();
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x00008010 File Offset: 0x00006210
		public override void Dispose()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Unregister(GalaxyTypeAwareListenerAccessToken.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GameServerGlobalAccessTokenListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0400006A RID: 106
		private HandleRef swigCPtr;
	}
}
