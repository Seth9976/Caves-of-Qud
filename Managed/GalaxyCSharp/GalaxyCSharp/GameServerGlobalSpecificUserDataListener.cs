using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000066 RID: 102
	public abstract class GameServerGlobalSpecificUserDataListener : ISpecificUserDataListener
	{
		// Token: 0x060006ED RID: 1773 RVA: 0x0000B8A1 File Offset: 0x00009AA1
		internal GameServerGlobalSpecificUserDataListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GameServerGlobalSpecificUserDataListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0000B8BD File Offset: 0x00009ABD
		public GameServerGlobalSpecificUserDataListener()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Register(GalaxyTypeAwareListenerSpecificUserData.GetListenerType(), this);
			}
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0000B8DF File Offset: 0x00009ADF
		internal static HandleRef getCPtr(GameServerGlobalSpecificUserDataListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x0000B900 File Offset: 0x00009B00
		~GameServerGlobalSpecificUserDataListener()
		{
			this.Dispose();
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x0000B930 File Offset: 0x00009B30
		public override void Dispose()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Unregister(GalaxyTypeAwareListenerSpecificUserData.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GameServerGlobalSpecificUserDataListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0400007A RID: 122
		private HandleRef swigCPtr;
	}
}
