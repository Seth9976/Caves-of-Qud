using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000031 RID: 49
	public abstract class GalaxyTypeAwareListenerLeaderboardEntriesRetrieve : IGalaxyListener
	{
		// Token: 0x060005BF RID: 1471 RVA: 0x00005108 File Offset: 0x00003308
		internal GalaxyTypeAwareListenerLeaderboardEntriesRetrieve(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLeaderboardEntriesRetrieve_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00005124 File Offset: 0x00003324
		public GalaxyTypeAwareListenerLeaderboardEntriesRetrieve()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerLeaderboardEntriesRetrieve(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00005142 File Offset: 0x00003342
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerLeaderboardEntriesRetrieve obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00005160 File Offset: 0x00003360
		~GalaxyTypeAwareListenerLeaderboardEntriesRetrieve()
		{
			this.Dispose();
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00005190 File Offset: 0x00003390
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerLeaderboardEntriesRetrieve(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00005218 File Offset: 0x00003418
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLeaderboardEntriesRetrieve_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000045 RID: 69
		private HandleRef swigCPtr;
	}
}
