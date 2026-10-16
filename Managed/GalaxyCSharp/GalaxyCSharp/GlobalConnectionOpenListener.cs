using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000072 RID: 114
	public abstract class GlobalConnectionOpenListener : IConnectionOpenListener
	{
		// Token: 0x0600072A RID: 1834 RVA: 0x0000DF8D File Offset: 0x0000C18D
		internal GlobalConnectionOpenListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalConnectionOpenListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x0000DFA9 File Offset: 0x0000C1A9
		public GlobalConnectionOpenListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerConnectionOpen.GetListenerType(), this);
			}
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x0000DFCB File Offset: 0x0000C1CB
		internal static HandleRef getCPtr(GlobalConnectionOpenListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0000DFEC File Offset: 0x0000C1EC
		~GlobalConnectionOpenListener()
		{
			this.Dispose();
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0000E01C File Offset: 0x0000C21C
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerConnectionOpen.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalConnectionOpenListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x04000087 RID: 135
		private HandleRef swigCPtr;
	}
}
