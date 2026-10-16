using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000064 RID: 100
	public sealed class LinkedResource : Resource
	{
		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060002CF RID: 719 RVA: 0x0000741A File Offset: 0x0000561A
		public byte[] Hash
		{
			get
			{
				return this.hash;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x00007422 File Offset: 0x00005622
		// (set) Token: 0x060002D1 RID: 721 RVA: 0x0000742A File Offset: 0x0000562A
		public string File
		{
			get
			{
				return this.file;
			}
			set
			{
				this.file = value;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x000025EF File Offset: 0x000007EF
		public override ResourceType ResourceType
		{
			get
			{
				return ResourceType.Linked;
			}
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x000030E3 File Offset: 0x000012E3
		public LinkedResource(string name, ManifestResourceAttributes flags)
			: base(name, flags)
		{
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00007433 File Offset: 0x00005633
		public LinkedResource(string name, ManifestResourceAttributes flags, string file)
			: base(name, flags)
		{
			this.file = file;
		}

		// Token: 0x040000A7 RID: 167
		internal byte[] hash;

		// Token: 0x040000A8 RID: 168
		private string file;
	}
}
