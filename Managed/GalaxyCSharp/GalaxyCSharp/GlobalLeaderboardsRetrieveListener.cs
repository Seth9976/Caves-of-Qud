using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000082 RID: 130
	public abstract class GlobalLeaderboardsRetrieveListener : ILeaderboardsRetrieveListener
	{
		// Token: 0x0600077F RID: 1919 RVA: 0x00011636 File Offset: 0x0000F836
		internal GlobalLeaderboardsRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalLeaderboardsRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x00011652 File Offset: 0x0000F852
		public GlobalLeaderboardsRetrieveListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerLeaderboardsRetrieve.GetListenerType(), this);
			}
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x00011674 File Offset: 0x0000F874
		internal static HandleRef getCPtr(GlobalLeaderboardsRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x00011694 File Offset: 0x0000F894
		~GlobalLeaderboardsRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x000116C4 File Offset: 0x0000F8C4
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerLeaderboardsRetrieve.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalLeaderboardsRetrieveListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0400009C RID: 156
		private HandleRef swigCPtr;
	}
}
