using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000A1 RID: 161
	public abstract class GlobalUserInformationRetrieveListener : IUserInformationRetrieveListener
	{
		// Token: 0x0600081D RID: 2077 RVA: 0x00015FF4 File Offset: 0x000141F4
		internal GlobalUserInformationRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalUserInformationRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			GlobalUserInformationRetrieveListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x0001601C File Offset: 0x0001421C
		public GlobalUserInformationRetrieveListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerUserInformationRetrieve.GetListenerType(), this);
			}
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x0001603E File Offset: 0x0001423E
		internal static HandleRef getCPtr(GlobalUserInformationRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x0001605C File Offset: 0x0001425C
		~GlobalUserInformationRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x0001608C File Offset: 0x0001428C
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerUserInformationRetrieve.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalUserInformationRetrieveListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (GlobalUserInformationRetrieveListener.listeners.ContainsKey(handle))
					{
						GlobalUserInformationRetrieveListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000BE RID: 190
		private static Dictionary<IntPtr, GlobalUserInformationRetrieveListener> listeners = new Dictionary<IntPtr, GlobalUserInformationRetrieveListener>();

		// Token: 0x040000BF RID: 191
		private HandleRef swigCPtr;
	}
}
