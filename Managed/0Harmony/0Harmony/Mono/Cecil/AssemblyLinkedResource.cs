using System;

namespace Mono.Cecil
{
	// Token: 0x020001B6 RID: 438
	internal sealed class AssemblyLinkedResource : Resource
	{
		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060007CE RID: 1998 RVA: 0x0001AE72 File Offset: 0x00019072
		// (set) Token: 0x060007CF RID: 1999 RVA: 0x0001AE7A File Offset: 0x0001907A
		public AssemblyNameReference Assembly
		{
			get
			{
				return this.reference;
			}
			set
			{
				this.reference = value;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060007D0 RID: 2000 RVA: 0x0001AE83 File Offset: 0x00019083
		public override ResourceType ResourceType
		{
			get
			{
				return ResourceType.AssemblyLinked;
			}
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x0001AE86 File Offset: 0x00019086
		public AssemblyLinkedResource(string name, ManifestResourceAttributes flags)
			: base(name, flags)
		{
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0001AE90 File Offset: 0x00019090
		public AssemblyLinkedResource(string name, ManifestResourceAttributes flags, AssemblyNameReference reference)
			: base(name, flags)
		{
			this.reference = reference;
		}

		// Token: 0x04000283 RID: 643
		private AssemblyNameReference reference;
	}
}
