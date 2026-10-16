using System;

namespace UnityEngine.Bindings
{
	// Token: 0x02000020 RID: 32
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Property)]
	[VisibleToOtherModules]
	internal class StaticAccessorAttribute : Attribute, IBindingsAttribute
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600005D RID: 93 RVA: 0x000024DA File Offset: 0x000006DA
		// (set) Token: 0x0600005E RID: 94 RVA: 0x000024E2 File Offset: 0x000006E2
		public string Name { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600005F RID: 95 RVA: 0x000024EB File Offset: 0x000006EB
		// (set) Token: 0x06000060 RID: 96 RVA: 0x000024F3 File Offset: 0x000006F3
		public StaticAccessorType Type { get; set; }

		// Token: 0x06000061 RID: 97 RVA: 0x00002078 File Offset: 0x00000278
		public StaticAccessorAttribute()
		{
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000024FC File Offset: 0x000006FC
		[VisibleToOtherModules]
		internal StaticAccessorAttribute(string name)
		{
			this.Name = name;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000250E File Offset: 0x0000070E
		public StaticAccessorAttribute(StaticAccessorType type)
		{
			this.Type = type;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002520 File Offset: 0x00000720
		public StaticAccessorAttribute(string name, StaticAccessorType type)
		{
			this.Name = name;
			this.Type = type;
		}
	}
}
