using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001E5 RID: 485
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class GeneratorSyntaxWalker : SyntaxWalker
	{
		// Token: 0x0600140E RID: 5134 RVA: 0x0003F123 File Offset: 0x0003D323
		internal GeneratorSyntaxWalker(ISyntaxContextReceiver syntaxReceiver)
			: base(SyntaxWalkerDepth.Node)
		{
			this._syntaxReceiver = syntaxReceiver;
		}

		// Token: 0x0600140F RID: 5135 RVA: 0x0003F133 File Offset: 0x0003D333
		public void VisitWithModel(SemanticModel model, SyntaxNode node)
		{
			this._semanticModel = model;
			this.Visit(node);
			this._semanticModel = null;
		}

		// Token: 0x06001410 RID: 5136 RVA: 0x0003F14A File Offset: 0x0003D34A
		public override void Visit(SyntaxNode node)
		{
			this._syntaxReceiver.OnVisitSyntaxNode(new GeneratorSyntaxContext(node, this._semanticModel));
			base.Visit(node);
		}

		// Token: 0x04000624 RID: 1572
		private readonly ISyntaxContextReceiver _syntaxReceiver;

		// Token: 0x04000625 RID: 1573
		[Nullable(2)]
		private SemanticModel _semanticModel;
	}
}
