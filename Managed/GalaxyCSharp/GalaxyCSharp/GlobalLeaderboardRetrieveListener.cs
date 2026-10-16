using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000080 RID: 128
	public abstract class GlobalLeaderboardRetrieveListener : ILeaderboardRetrieveListener
	{
		// Token: 0x06000775 RID: 1909 RVA: 0x00010EA8 File Offset: 0x0000F0A8
		internal GlobalLeaderboardRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalLeaderboardRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x00010EC4 File Offset: 0x0000F0C4
		public GlobalLeaderboardRetrieveListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerLeaderboardRetrieve.GetListenerType(), this);
			}
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x00010EE6 File Offset: 0x0000F0E6
		internal static HandleRef getCPtr(GlobalLeaderboardRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x00010F04 File Offset: 0x0000F104
		~GlobalLeaderboardRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x00010F34 File Offset: 0x0000F134
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerLeaderboardRetrieve.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalLeaderboardRetrieveListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0400009A RID: 154
		private HandleRef swigCPtr;
	}
}
