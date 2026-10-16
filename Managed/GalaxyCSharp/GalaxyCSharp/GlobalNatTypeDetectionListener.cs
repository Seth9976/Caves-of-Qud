using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200008E RID: 142
	public abstract class GlobalNatTypeDetectionListener : INatTypeDetectionListener
	{
		// Token: 0x060007BB RID: 1979 RVA: 0x00012B06 File Offset: 0x00010D06
		internal GlobalNatTypeDetectionListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalNatTypeDetectionListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x00012B22 File Offset: 0x00010D22
		public GlobalNatTypeDetectionListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerNatTypeDetection.GetListenerType(), this);
			}
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x00012B44 File Offset: 0x00010D44
		internal static HandleRef getCPtr(GlobalNatTypeDetectionListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x00012B64 File Offset: 0x00010D64
		~GlobalNatTypeDetectionListener()
		{
			this.Dispose();
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x00012B94 File Offset: 0x00010D94
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerNatTypeDetection.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalNatTypeDetectionListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000A8 RID: 168
		private HandleRef swigCPtr;
	}
}
