using System;
using System.Threading;
using Mono.Collections.Generic;

namespace Mono.Cecil.Cil
{
	// Token: 0x020002DF RID: 735
	public sealed class ImportDebugInformation : DebugInformation
	{
		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x060012B5 RID: 4789 RVA: 0x0003CB20 File Offset: 0x0003AD20
		public bool HasTargets
		{
			get
			{
				return !this.targets.IsNullOrEmpty<ImportTarget>();
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x060012B6 RID: 4790 RVA: 0x0003CB30 File Offset: 0x0003AD30
		public Collection<ImportTarget> Targets
		{
			get
			{
				if (this.targets == null)
				{
					Interlocked.CompareExchange<Collection<ImportTarget>>(ref this.targets, new Collection<ImportTarget>(), null);
				}
				return this.targets;
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x060012B7 RID: 4791 RVA: 0x0003CB52 File Offset: 0x0003AD52
		// (set) Token: 0x060012B8 RID: 4792 RVA: 0x0003CB5A File Offset: 0x0003AD5A
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

		// Token: 0x060012B9 RID: 4793 RVA: 0x0003CB63 File Offset: 0x0003AD63
		public ImportDebugInformation()
		{
			this.token = new MetadataToken(TokenType.ImportScope);
		}

		// Token: 0x04000980 RID: 2432
		internal ImportDebugInformation parent;

		// Token: 0x04000981 RID: 2433
		internal Collection<ImportTarget> targets;
	}
}
