using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000064 RID: 100
	public abstract class GameServerGlobalOperationalStateChangeListener : IOperationalStateChangeListener
	{
		// Token: 0x060006E3 RID: 1763 RVA: 0x0000B135 File Offset: 0x00009335
		internal GameServerGlobalOperationalStateChangeListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GameServerGlobalOperationalStateChangeListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x0000B151 File Offset: 0x00009351
		public GameServerGlobalOperationalStateChangeListener()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Register(GalaxyTypeAwareListenerOperationalStateChange.GetListenerType(), this);
			}
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x0000B173 File Offset: 0x00009373
		internal static HandleRef getCPtr(GameServerGlobalOperationalStateChangeListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x0000B194 File Offset: 0x00009394
		~GameServerGlobalOperationalStateChangeListener()
		{
			this.Dispose();
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x0000B1C4 File Offset: 0x000093C4
		public override void Dispose()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Unregister(GalaxyTypeAwareListenerOperationalStateChange.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GameServerGlobalOperationalStateChangeListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x04000078 RID: 120
		private HandleRef swigCPtr;
	}
}
