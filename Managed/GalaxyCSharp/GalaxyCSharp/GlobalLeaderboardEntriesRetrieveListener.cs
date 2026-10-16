using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200007F RID: 127
	public abstract class GlobalLeaderboardEntriesRetrieveListener : ILeaderboardEntriesRetrieveListener
	{
		// Token: 0x06000770 RID: 1904 RVA: 0x00010AE5 File Offset: 0x0000ECE5
		internal GlobalLeaderboardEntriesRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalLeaderboardEntriesRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x00010B01 File Offset: 0x0000ED01
		public GlobalLeaderboardEntriesRetrieveListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerLeaderboardEntriesRetrieve.GetListenerType(), this);
			}
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x00010B23 File Offset: 0x0000ED23
		internal static HandleRef getCPtr(GlobalLeaderboardEntriesRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00010B44 File Offset: 0x0000ED44
		~GlobalLeaderboardEntriesRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x00010B74 File Offset: 0x0000ED74
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerLeaderboardEntriesRetrieve.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalLeaderboardEntriesRetrieveListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x04000099 RID: 153
		private HandleRef swigCPtr;
	}
}
