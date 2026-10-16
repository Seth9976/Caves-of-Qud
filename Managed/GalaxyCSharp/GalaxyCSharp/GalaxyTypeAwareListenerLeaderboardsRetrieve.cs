using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000034 RID: 52
	public abstract class GalaxyTypeAwareListenerLeaderboardsRetrieve : IGalaxyListener
	{
		// Token: 0x060005D1 RID: 1489 RVA: 0x000054A4 File Offset: 0x000036A4
		internal GalaxyTypeAwareListenerLeaderboardsRetrieve(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLeaderboardsRetrieve_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x000054C0 File Offset: 0x000036C0
		public GalaxyTypeAwareListenerLeaderboardsRetrieve()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerLeaderboardsRetrieve(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x000054DE File Offset: 0x000036DE
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerLeaderboardsRetrieve obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x000054FC File Offset: 0x000036FC
		~GalaxyTypeAwareListenerLeaderboardsRetrieve()
		{
			this.Dispose();
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x0000552C File Offset: 0x0000372C
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerLeaderboardsRetrieve(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x000055B4 File Offset: 0x000037B4
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLeaderboardsRetrieve_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000048 RID: 72
		private HandleRef swigCPtr;
	}
}
