using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Emit
{
	// Token: 0x0200047B RID: 1147
	[NullableContext(1)]
	[Nullable(0)]
	internal readonly struct EmitContext
	{
		// Token: 0x17000D13 RID: 3347
		// (get) Token: 0x06002D0A RID: 11530 RVA: 0x0007E70D File Offset: 0x0007C90D
		public bool IncludePrivateMembers
		{
			get
			{
				return (this._flags & EmitContext.Flags.IncludePrivateMembers) > EmitContext.Flags.None;
			}
		}

		// Token: 0x17000D14 RID: 3348
		// (get) Token: 0x06002D0B RID: 11531 RVA: 0x0007E71A File Offset: 0x0007C91A
		public bool MetadataOnly
		{
			get
			{
				return (this._flags & EmitContext.Flags.MetadataOnly) > EmitContext.Flags.None;
			}
		}

		// Token: 0x17000D15 RID: 3349
		// (get) Token: 0x06002D0C RID: 11532 RVA: 0x0007E727 File Offset: 0x0007C927
		public bool IsRefAssembly
		{
			get
			{
				return this.MetadataOnly && !this.IncludePrivateMembers;
			}
		}

		// Token: 0x06002D0D RID: 11533 RVA: 0x0007E73C File Offset: 0x0007C93C
		public EmitContext(CommonPEModuleBuilder module, [Nullable(2)] SyntaxNode syntaxNode, DiagnosticBag diagnostics, bool metadataOnly, bool includePrivateMembers)
		{
			this = new EmitContext(module, diagnostics, metadataOnly, includePrivateMembers, syntaxNode, null);
		}

		// Token: 0x06002D0E RID: 11534 RVA: 0x0007E74C File Offset: 0x0007C94C
		public EmitContext(CommonPEModuleBuilder module, DiagnosticBag diagnostics, bool metadataOnly, bool includePrivateMembers, [Nullable(2)] SyntaxNode syntaxNode = null, [Nullable(2)] RebuildData rebuildData = null)
		{
			this.RebuildData = rebuildData;
			this.Module = module;
			this.SyntaxNode = syntaxNode;
			this.RebuildData = rebuildData;
			this.Diagnostics = diagnostics;
			EmitContext.Flags flags = EmitContext.Flags.None;
			if (metadataOnly)
			{
				flags |= EmitContext.Flags.MetadataOnly;
			}
			if (includePrivateMembers)
			{
				flags |= EmitContext.Flags.IncludePrivateMembers;
			}
			this._flags = flags;
		}

		// Token: 0x040011D7 RID: 4567
		public readonly CommonPEModuleBuilder Module;

		// Token: 0x040011D8 RID: 4568
		[Nullable(2)]
		public readonly SyntaxNode SyntaxNode;

		// Token: 0x040011D9 RID: 4569
		[Nullable(2)]
		public readonly RebuildData RebuildData;

		// Token: 0x040011DA RID: 4570
		public readonly DiagnosticBag Diagnostics;

		// Token: 0x040011DB RID: 4571
		private readonly EmitContext.Flags _flags;

		// Token: 0x0200072E RID: 1838
		[NullableContext(0)]
		[Flags]
		private enum Flags
		{
			// Token: 0x04001CA8 RID: 7336
			None = 0,
			// Token: 0x04001CA9 RID: 7337
			MetadataOnly = 1,
			// Token: 0x04001CAA RID: 7338
			IncludePrivateMembers = 2
		}
	}
}
