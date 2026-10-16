using System;

namespace UnityEngine.Bindings
{
	// Token: 0x0200001E RID: 30
	[AttributeUsage(AttributeTargets.Method)]
	[VisibleToOtherModules]
	internal class ThreadSafeAttribute : NativeMethodAttribute
	{
		// Token: 0x0600005C RID: 92 RVA: 0x000024C8 File Offset: 0x000006C8
		public ThreadSafeAttribute()
		{
			base.IsThreadSafe = true;
		}
	}
}
