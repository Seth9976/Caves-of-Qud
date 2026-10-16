using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000011 RID: 17
	public sealed class AssemblyLinkedResource : Resource
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000088 RID: 136 RVA: 0x000030CF File Offset: 0x000012CF
		// (set) Token: 0x06000089 RID: 137 RVA: 0x000030D7 File Offset: 0x000012D7
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

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600008A RID: 138 RVA: 0x000030E0 File Offset: 0x000012E0
		public override ResourceType ResourceType
		{
			get
			{
				return ResourceType.AssemblyLinked;
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000030E3 File Offset: 0x000012E3
		public AssemblyLinkedResource(string name, ManifestResourceAttributes flags)
			: base(name, flags)
		{
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000030ED File Offset: 0x000012ED
		public AssemblyLinkedResource(string name, ManifestResourceAttributes flags, AssemblyNameReference reference)
			: base(name, flags)
		{
			this.reference = reference;
		}

		// Token: 0x04000021 RID: 33
		private AssemblyNameReference reference;
	}
}
