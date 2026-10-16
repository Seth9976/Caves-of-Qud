using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000074 RID: 116
	public abstract class GlobalFileShareListener : IFileShareListener
	{
		// Token: 0x06000734 RID: 1844 RVA: 0x0000E489 File Offset: 0x0000C689
		internal GlobalFileShareListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalFileShareListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0000E4A5 File Offset: 0x0000C6A5
		public GlobalFileShareListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerFileShare.GetListenerType(), this);
			}
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0000E4C7 File Offset: 0x0000C6C7
		internal static HandleRef getCPtr(GlobalFileShareListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0000E4E8 File Offset: 0x0000C6E8
		~GlobalFileShareListener()
		{
			this.Dispose();
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0000E518 File Offset: 0x0000C718
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerFileShare.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalFileShareListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x04000089 RID: 137
		private HandleRef swigCPtr;
	}
}
