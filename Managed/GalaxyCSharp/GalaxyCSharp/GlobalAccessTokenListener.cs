using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000069 RID: 105
	public abstract class GlobalAccessTokenListener : IAccessTokenListener
	{
		// Token: 0x060006FD RID: 1789 RVA: 0x0000C1CC File Offset: 0x0000A3CC
		internal GlobalAccessTokenListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalAccessTokenListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0000C1E8 File Offset: 0x0000A3E8
		public GlobalAccessTokenListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerAccessToken.GetListenerType(), this);
			}
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0000C20A File Offset: 0x0000A40A
		internal static HandleRef getCPtr(GlobalAccessTokenListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x0000C228 File Offset: 0x0000A428
		~GlobalAccessTokenListener()
		{
			this.Dispose();
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x0000C258 File Offset: 0x0000A458
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerAccessToken.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalAccessTokenListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0400007E RID: 126
		private HandleRef swigCPtr;
	}
}
