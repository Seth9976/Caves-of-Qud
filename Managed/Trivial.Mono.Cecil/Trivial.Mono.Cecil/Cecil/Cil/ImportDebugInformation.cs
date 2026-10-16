using System;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil.Cil
{
	// Token: 0x02000113 RID: 275
	public sealed class ImportDebugInformation : DebugInformation
	{
		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000A8D RID: 2701 RVA: 0x00022E29 File Offset: 0x00021029
		public bool HasTargets
		{
			get
			{
				return !this.targets.IsNullOrEmpty<ImportTarget>();
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000A8E RID: 2702 RVA: 0x00022E3C File Offset: 0x0002103C
		public Collection<ImportTarget> Targets
		{
			get
			{
				Collection<ImportTarget> collection;
				if ((collection = this.targets) == null)
				{
					collection = (this.targets = new Collection<ImportTarget>());
				}
				return collection;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000A8F RID: 2703 RVA: 0x00022E61 File Offset: 0x00021061
		// (set) Token: 0x06000A90 RID: 2704 RVA: 0x00022E69 File Offset: 0x00021069
		public ImportDebugInformation Parent
		{
			get
			{
				return this.parent;
			}
			set
			{
				this.parent = value;
			}
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x00022E72 File Offset: 0x00021072
		public ImportDebugInformation()
		{
			this.token = new MetadataToken(TokenType.ImportScope);
		}

		// Token: 0x04000564 RID: 1380
		internal ImportDebugInformation parent;

		// Token: 0x04000565 RID: 1381
		internal Collection<ImportTarget> targets;
	}
}
