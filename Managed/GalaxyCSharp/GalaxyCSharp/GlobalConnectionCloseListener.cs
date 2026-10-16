using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000070 RID: 112
	public abstract class GlobalConnectionCloseListener : IConnectionCloseListener
	{
		// Token: 0x06000720 RID: 1824 RVA: 0x0000D87F File Offset: 0x0000BA7F
		internal GlobalConnectionCloseListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalConnectionCloseListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x0000D89B File Offset: 0x0000BA9B
		public GlobalConnectionCloseListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerConnectionClose.GetListenerType(), this);
			}
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0000D8BD File Offset: 0x0000BABD
		internal static HandleRef getCPtr(GlobalConnectionCloseListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x0000D8DC File Offset: 0x0000BADC
		~GlobalConnectionCloseListener()
		{
			this.Dispose();
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x0000D90C File Offset: 0x0000BB0C
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerConnectionClose.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalConnectionCloseListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x04000085 RID: 133
		private HandleRef swigCPtr;
	}
}
