using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000A0 RID: 160
	public abstract class GlobalUserFindListener : IUserFindListener
	{
		// Token: 0x06000818 RID: 2072 RVA: 0x00015EC5 File Offset: 0x000140C5
		internal GlobalUserFindListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalUserFindListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x00015EE1 File Offset: 0x000140E1
		public GlobalUserFindListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerUserFind.GetListenerType(), this);
			}
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x00015F03 File Offset: 0x00014103
		internal static HandleRef getCPtr(GlobalUserFindListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x00015F24 File Offset: 0x00014124
		~GlobalUserFindListener()
		{
			this.Dispose();
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x00015F54 File Offset: 0x00014154
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerUserFind.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalUserFindListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000BD RID: 189
		private HandleRef swigCPtr;
	}
}
