using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200006A RID: 106
	public abstract class GlobalAchievementChangeListener : IAchievementChangeListener
	{
		// Token: 0x06000702 RID: 1794 RVA: 0x0000C4FD File Offset: 0x0000A6FD
		internal GlobalAchievementChangeListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalAchievementChangeListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0000C519 File Offset: 0x0000A719
		public GlobalAchievementChangeListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerAchievementChange.GetListenerType(), this);
			}
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x0000C53B File Offset: 0x0000A73B
		internal static HandleRef getCPtr(GlobalAchievementChangeListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x0000C55C File Offset: 0x0000A75C
		~GlobalAchievementChangeListener()
		{
			this.Dispose();
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0000C58C File Offset: 0x0000A78C
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerAchievementChange.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalAchievementChangeListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0400007F RID: 127
		private HandleRef swigCPtr;
	}
}
