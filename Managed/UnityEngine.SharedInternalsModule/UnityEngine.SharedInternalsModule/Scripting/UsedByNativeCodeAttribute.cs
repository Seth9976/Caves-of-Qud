using System;
using UnityEngine.Bindings;

namespace UnityEngine.Scripting
{
	// Token: 0x02000028 RID: 40
	[VisibleToOtherModules]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Interface, Inherited = false)]
	internal class UsedByNativeCodeAttribute : Attribute
	{
		// Token: 0x0600007C RID: 124 RVA: 0x00002078 File Offset: 0x00000278
		public UsedByNativeCodeAttribute()
		{
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000025D5 File Offset: 0x000007D5
		public UsedByNativeCodeAttribute(string name)
		{
			this.Name = name;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600007E RID: 126 RVA: 0x000025E7 File Offset: 0x000007E7
		// (set) Token: 0x0600007F RID: 127 RVA: 0x000025EF File Offset: 0x000007EF
		public string Name { get; set; }
	}
}
