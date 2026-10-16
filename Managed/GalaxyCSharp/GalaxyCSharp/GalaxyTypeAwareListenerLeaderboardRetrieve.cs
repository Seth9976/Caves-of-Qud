using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000032 RID: 50
	public abstract class GalaxyTypeAwareListenerLeaderboardRetrieve : IGalaxyListener
	{
		// Token: 0x060005C5 RID: 1477 RVA: 0x0000523C File Offset: 0x0000343C
		internal GalaxyTypeAwareListenerLeaderboardRetrieve(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLeaderboardRetrieve_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00005258 File Offset: 0x00003458
		public GalaxyTypeAwareListenerLeaderboardRetrieve()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerLeaderboardRetrieve(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00005276 File Offset: 0x00003476
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerLeaderboardRetrieve obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00005294 File Offset: 0x00003494
		~GalaxyTypeAwareListenerLeaderboardRetrieve()
		{
			this.Dispose();
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x000052C4 File Offset: 0x000034C4
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerLeaderboardRetrieve(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0000534C File Offset: 0x0000354C
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLeaderboardRetrieve_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000046 RID: 70
		private HandleRef swigCPtr;
	}
}
