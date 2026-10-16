using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200009C RID: 156
	public abstract class GlobalSpecificUserDataListener : ISpecificUserDataListener
	{
		// Token: 0x06000804 RID: 2052 RVA: 0x00015300 File Offset: 0x00013500
		internal GlobalSpecificUserDataListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalSpecificUserDataListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x0001531C File Offset: 0x0001351C
		public GlobalSpecificUserDataListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerSpecificUserData.GetListenerType(), this);
			}
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x0001533E File Offset: 0x0001353E
		internal static HandleRef getCPtr(GlobalSpecificUserDataListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x0001535C File Offset: 0x0001355C
		~GlobalSpecificUserDataListener()
		{
			this.Dispose();
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0001538C File Offset: 0x0001358C
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerSpecificUserData.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalSpecificUserDataListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000B9 RID: 185
		private HandleRef swigCPtr;
	}
}
