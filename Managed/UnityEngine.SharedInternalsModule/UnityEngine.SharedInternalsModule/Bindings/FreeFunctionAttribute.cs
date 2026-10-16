using System;

namespace UnityEngine.Bindings
{
	// Token: 0x0200001D RID: 29
	[AttributeUsage(AttributeTargets.Method)]
	[VisibleToOtherModules]
	internal class FreeFunctionAttribute : NativeMethodAttribute
	{
		// Token: 0x06000059 RID: 89 RVA: 0x0000249D File Offset: 0x0000069D
		public FreeFunctionAttribute()
		{
			base.IsFreeFunction = true;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000024AF File Offset: 0x000006AF
		public FreeFunctionAttribute(string name)
			: base(name, true)
		{
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000024BB File Offset: 0x000006BB
		public FreeFunctionAttribute(string name, bool isThreadSafe)
			: base(name, true, isThreadSafe)
		{
		}
	}
}
