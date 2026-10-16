using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200009B RID: 155
	public abstract class GlobalSharedFileDownloadListener : ISharedFileDownloadListener
	{
		// Token: 0x060007FF RID: 2047 RVA: 0x000151D1 File Offset: 0x000133D1
		internal GlobalSharedFileDownloadListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalSharedFileDownloadListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x000151ED File Offset: 0x000133ED
		public GlobalSharedFileDownloadListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerSharedFileDownload.GetListenerType(), this);
			}
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x0001520F File Offset: 0x0001340F
		internal static HandleRef getCPtr(GlobalSharedFileDownloadListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x00015230 File Offset: 0x00013430
		~GlobalSharedFileDownloadListener()
		{
			this.Dispose();
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x00015260 File Offset: 0x00013460
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerSharedFileDownload.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalSharedFileDownloadListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000B8 RID: 184
		private HandleRef swigCPtr;
	}
}
