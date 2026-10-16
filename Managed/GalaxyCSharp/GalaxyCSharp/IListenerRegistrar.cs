using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000117 RID: 279
	public class IListenerRegistrar : IDisposable
	{
		// Token: 0x06000AEB RID: 2795 RVA: 0x000190F0 File Offset: 0x000172F0
		internal IListenerRegistrar(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x0001910C File Offset: 0x0001730C
		internal static HandleRef getCPtr(IListenerRegistrar obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0001912C File Offset: 0x0001732C
		~IListenerRegistrar()
		{
			this.Dispose();
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x0001915C File Offset: 0x0001735C
		public virtual void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IListenerRegistrar(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x000191DC File Offset: 0x000173DC
		public virtual void Register(ListenerType listenerType, IGalaxyListener listener)
		{
			GalaxyInstancePINVOKE.IListenerRegistrar_Register(this.swigCPtr, (int)listenerType, IGalaxyListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x00019200 File Offset: 0x00017400
		public virtual void Unregister(ListenerType listenerType, IGalaxyListener listener)
		{
			GalaxyInstancePINVOKE.IListenerRegistrar_Unregister(this.swigCPtr, (int)listenerType, IGalaxyListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x040001EE RID: 494
		private HandleRef swigCPtr;

		// Token: 0x040001EF RID: 495
		protected bool swigCMemOwn;
	}
}
