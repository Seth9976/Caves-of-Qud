using System;
using UnityEngine.Bindings;

namespace UnityEngine.Scripting
{
	// Token: 0x02000029 RID: 41
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Interface, Inherited = false)]
	[VisibleToOtherModules]
	internal class RequiredByNativeCodeAttribute : Attribute
	{
		// Token: 0x06000080 RID: 128 RVA: 0x00002078 File Offset: 0x00000278
		public RequiredByNativeCodeAttribute()
		{
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000025F8 File Offset: 0x000007F8
		public RequiredByNativeCodeAttribute(string name)
		{
			this.Name = name;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000260A File Offset: 0x0000080A
		public RequiredByNativeCodeAttribute(bool optional)
		{
			this.Optional = optional;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000261C File Offset: 0x0000081C
		public RequiredByNativeCodeAttribute(string name, bool optional)
		{
			this.Name = name;
			this.Optional = optional;
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00002636 File Offset: 0x00000836
		// (set) Token: 0x06000085 RID: 133 RVA: 0x0000263E File Offset: 0x0000083E
		public string Name { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00002647 File Offset: 0x00000847
		// (set) Token: 0x06000087 RID: 135 RVA: 0x0000264F File Offset: 0x0000084F
		public bool Optional { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00002658 File Offset: 0x00000858
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00002660 File Offset: 0x00000860
		public bool GenerateProxy { get; set; }
	}
}
