using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001DE RID: 478
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct GeneratorSyntaxContext
	{
		// Token: 0x060013D8 RID: 5080 RVA: 0x0003E020 File Offset: 0x0003C220
		internal GeneratorSyntaxContext(SyntaxNode node, SemanticModel semanticModel)
		{
			this.Node = node;
			this.SemanticModel = semanticModel;
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x060013D9 RID: 5081 RVA: 0x0003E030 File Offset: 0x0003C230
		public SyntaxNode Node { get; }

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x060013DA RID: 5082 RVA: 0x0003E038 File Offset: 0x0003C238
		public SemanticModel SemanticModel { get; }
	}
}
