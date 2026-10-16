using System;

namespace System.Web
{
	// Token: 0x0200006B RID: 107
	internal struct DynamicModuleInfo
	{
		// Token: 0x06000448 RID: 1096 RVA: 0x00008C82 File Offset: 0x00006E82
		public DynamicModuleInfo(Type type, string name)
		{
			this.Name = name;
			this.Type = type;
		}

		// Token: 0x04000E61 RID: 3681
		public readonly string Name;

		// Token: 0x04000E62 RID: 3682
		public readonly Type Type;
	}
}
