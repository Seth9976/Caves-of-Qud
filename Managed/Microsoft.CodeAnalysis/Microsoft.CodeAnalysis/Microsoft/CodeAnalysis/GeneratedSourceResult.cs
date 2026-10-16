using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Text;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001F1 RID: 497
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct GeneratedSourceResult
	{
		// Token: 0x06001434 RID: 5172 RVA: 0x0003F31F File Offset: 0x0003D51F
		internal GeneratedSourceResult(SyntaxTree tree, SourceText text, string hintName)
		{
			this.SyntaxTree = tree;
			this.SourceText = text;
			this.HintName = hintName;
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06001435 RID: 5173 RVA: 0x0003F336 File Offset: 0x0003D536
		public SyntaxTree SyntaxTree { get; }

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06001436 RID: 5174 RVA: 0x0003F33E File Offset: 0x0003D53E
		public SourceText SourceText { get; }

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06001437 RID: 5175 RVA: 0x0003F346 File Offset: 0x0003D546
		public string HintName { get; }
	}
}
