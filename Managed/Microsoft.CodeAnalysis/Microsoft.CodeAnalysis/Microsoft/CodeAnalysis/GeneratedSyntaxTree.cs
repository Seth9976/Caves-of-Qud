using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Text;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001DA RID: 474
	[NullableContext(1)]
	[Nullable(0)]
	internal readonly struct GeneratedSyntaxTree
	{
		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x060013BD RID: 5053 RVA: 0x0003DDA5 File Offset: 0x0003BFA5
		public SourceText Text { get; }

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x060013BE RID: 5054 RVA: 0x0003DDAD File Offset: 0x0003BFAD
		public string HintName { get; }

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x060013BF RID: 5055 RVA: 0x0003DDB5 File Offset: 0x0003BFB5
		public SyntaxTree Tree { get; }

		// Token: 0x060013C0 RID: 5056 RVA: 0x0003DDBD File Offset: 0x0003BFBD
		public GeneratedSyntaxTree(string hintName, SourceText text, SyntaxTree tree)
		{
			this.Text = text;
			this.HintName = hintName;
			this.Tree = tree;
		}
	}
}
