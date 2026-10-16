using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000022 RID: 34
	public abstract class GalaxyTypeAwareListenerConnectionClose : IGalaxyListener
	{
		// Token: 0x06000565 RID: 1381 RVA: 0x00003EFC File Offset: 0x000020FC
		internal GalaxyTypeAwareListenerConnectionClose(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerConnectionClose_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00003F18 File Offset: 0x00002118
		public GalaxyTypeAwareListenerConnectionClose()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerConnectionClose(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00003F36 File Offset: 0x00002136
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerConnectionClose obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00003F54 File Offset: 0x00002154
		~GalaxyTypeAwareListenerConnectionClose()
		{
			this.Dispose();
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00003F84 File Offset: 0x00002184
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerConnectionClose(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0000400C File Offset: 0x0000220C
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerConnectionClose_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000036 RID: 54
		private HandleRef swigCPtr;
	}
}
